﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    /// <summary>
    /// Represents a single CSV data record.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Instances of this class are used to represent a record of CSV data. Each record has any number of values in it, accessible via the indexers in
    /// this class.
    /// </para>
    /// <para>
    /// If the CSV data source that this record originates from had a header record initialised, it is exposed via the <see cref="HeaderRecord"/>
    /// property.
    /// </para>
    /// </remarks>
    [Serializable]
    public sealed class CsvDataRecord : CsvRecordBase
    {
        /// <summary>
        /// See <see cref="HeaderRecord"/>.
        /// </summary>
        private CsvHeaderRecord _headerRecord;

        /// <summary>
        /// Gets the header record for this CSV record, or <see langword="null"/> if no header record applies.
        /// </summary>
        /// <remarks>
        /// If no header record was initially read from the CSV data source, then this property yields <see langword="null"/>. Otherwise, it yields the
        /// <see cref="HeaderRecord"/> instance that contains the details of the header record.
        /// </remarks>
        public CsvHeaderRecord HeaderRecord
        {
            get
            {
                return _headerRecord;
            }
        }

        /// <summary>
        /// Gets a value in this CSV record by column name.
        /// </summary>
        /// <remarks>
        /// This indexer can be used to retrieve a record value by column name. It is only possible to do so if the header record was initialised in the
        /// CSV data source. If not, <see cref="HeaderRecord"/> will be <see langword="null"/> and this indexer will throw an exception if used.
        /// </remarks>
        public string this[string column]
        {
            get
            {
                return Values[_headerRecord[column]];
            }
        }

        /// <summary>
        /// Creates and returns an instance of <c>DataRecord</c> by parsing with the provided CSV parser.
        /// </summary>
        /// <param name="headerRecord">
        /// The header record for the parsed data record, or <see langword="null"/> if irrelevant.
        /// </param>
        /// <param name="parser">
        /// The CSV parser to use.
        /// </param>
        /// <returns>
        /// The CSV record, or <see langword="null"/> if no record was found in the reader provided.
        /// </returns>
        internal static CsvDataRecord FromParser(CsvHeaderRecord headerRecord, CsvParser parser)
        {
            CsvDataRecord retVal = null;
            string[] values = parser.ParseRecord();

            if (values != null)
            {
                retVal = new CsvDataRecord(headerRecord, values, true);
            }

            return retVal;
        }

        /// <summary>
        /// Constructs an instance of <c>DataRecord</c> with the header specified.
        /// </summary>
        /// <param name="headerRecord">
        /// The header record for this CSV record, or <see langword="null"/> if no header record applies.
        /// </param>
        public CsvDataRecord(CsvHeaderRecord headerRecord)
        {
            _headerRecord = headerRecord;
        }

        /// <summary>
        /// Constructs an instance of <c>DataRecord</c> with the header and values specified.
        /// </summary>
        /// <param name="headerRecord">
        /// The header record for this CSV record, or <see langword="null"/> if no header record applies.
        /// </param>
        /// <param name="values">
        /// The values for this CSV record.
        /// </param>
        public CsvDataRecord(CsvHeaderRecord headerRecord, string[] values)
            : this(headerRecord, values, false)
        {
        }

        /// <summary>
        /// Constructs an instance of <c>DataRecord</c> with the header and values specified, optionally making the values in this data record
        /// read-only.
        /// </summary>
        /// <param name="headerRecord">
        /// The header record for this CSV record, or <see langword="null"/> if no header record applies.
        /// </param>
        /// <param name="values">
        /// The values for this CSV record.
        /// </param>
        /// <param name="readOnly">
        /// If <see langword="true"/>, the values in this data record are read-only.
        /// </param>
        public CsvDataRecord(CsvHeaderRecord headerRecord, string[] values, bool readOnly)
            : base(values, readOnly)
        {
            _headerRecord = headerRecord;
        }

        /// <summary>
        /// Determines whether this <c>DataRecord</c> is equal to <paramref name="obj"/>.
        /// </summary>
        /// <remarks>
        /// Two <c>DataRecord</c> instances are considered equal if all their values are equal and their header records are equal.
        /// </remarks>
        /// <param name="obj">
        /// The object to compare to this <c>DataRecord</c>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if this <c>DataRecord</c> equals <paramref name="obj"/>, otherwise <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(obj, this))
            {
                return true;
            }

            CsvDataRecord record = obj as CsvDataRecord;

            if (obj != null)
            {
                //this checks that all values are equal
                if (base.Equals(obj))
                {
                    if ((_headerRecord == null) && (record._headerRecord == null))
                    {
                        //both have no header and equal values, therefore they are equal
                        return true;
                    }
                    else if (((_headerRecord != null) && (record._headerRecord != null)) && (_headerRecord.Equals(record._headerRecord)))
                    {
                        //both have equal headers and equal values, therefore they are equal
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Gets a hash code for this <c>DataRecord</c>.
        /// </summary>
        /// <returns>
        /// A hash code for this <c>DataRecord</c>.
        /// </returns>
        public override int GetHashCode()
        {
            int retVal = base.GetHashCode();

            if (_headerRecord != null)
            {
                retVal = (int)Math.Pow(retVal, _headerRecord.GetHashCode());
            }

            return retVal;
        }
    }
}
