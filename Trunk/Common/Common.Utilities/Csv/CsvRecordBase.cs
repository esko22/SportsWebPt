using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    /// <summary>
    /// A base class for CSV record types.
    /// </summary>
    /// <remarks>
    /// The CSV record types <see cref="CsvHeaderRecord"/> and <see cref="CsvDataRecord"/> obtain common functionality by inheriting from this class.
    /// </remarks>
    [Serializable]
    public abstract class CsvRecordBase
    {
        #region Fields

        /// <summary>
        /// The character used to separator values in the <see cref="ToString"/> implementation
        /// </summary>
        public const char ValueSeparator = (char)0x2022;

        /// <summary>
        /// See <see cref="Values"/>.
        /// </summary>
        private IList<string> values;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialises an instance of <c>RecordBase</c> with no values.
        /// </summary>
        protected CsvRecordBase()
        {
            values = new List<string>();
        }

        /// <summary>
        /// Initialises an instance of the <c>RecordBase</c> class with the values specified.
        /// </summary>
        /// <param name="values">
        /// The values for the CSV record.
        /// </param>
        protected CsvRecordBase(IEnumerable<string> values)
            : this(values, false)
        {
        }

        /// <summary>
        /// Initialises an instance of the <c>RecordBase</c> class with the values specified, optionally making the value collection read-only.
        /// </summary>
        /// <param name="values">
        /// The values for the CSV record.
        /// </param>
        /// <param name="readOnly">
        /// If <see langword="true"/>, the value collection will be read-only.
        /// </param>
        protected CsvRecordBase(IEnumerable<string> values, bool readOnly)
        {
            Check.Argument.IsNotNull(values, "values");
            this.values = new List<string>(values);

            if (readOnly)
            {
                //just use the wrapper readonly collection
                this.values = new ReadOnlyCollection<string>(this.values);
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a collection of values in this CSV record.
        /// </summary>
        public IList<string> Values
        {
            get
            {
                return values;
            }
        }

        #endregion Properties

        #region Indexers

        /// <summary>
        /// Gets the value at the specified index for this CSV record.
        /// </summary>
        public string this[int index]
        {
            get
            {
                return values[index];
            }
            set
            {
                values[index] = value;
            }
        }

        #endregion Indexers

        #region Methods

        /// <summary>
        /// Determines whether this <c>RecordBase</c> is equal to <paramref name="obj"/>.
        /// </summary>
        /// <remarks>
        /// Two <c>RecordBase</c> instances are considered equal if they contain the same number of values, and each of their corresponding values are also
        /// equal.
        /// </remarks>
        /// <param name="obj">
        /// The object to compare to this <c>RecordBase</c>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="obj"/> is equal to this <c>RecordBase</c>, otherwise <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            var record = obj as CsvRecordBase;

            if (record != null)
            {
                if (values.Count == record.values.Count)
                {
                    for (int i = 0; i < values.Count; ++i)
                    {
                        if (values[i] != record.values[i])
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets a hash code for this <c>RecordBase</c>.
        /// </summary>
        /// <returns>
        /// A hash code for this <c>RecordBase</c>.
        /// </returns>
        public override int GetHashCode()
        {
            int retVal = 17;

            for (int i = 0; i < values.Count; ++i)
            {
                retVal += values[i].GetHashCode();
            }

            return retVal;
        }

        /// <summary>
        /// Returns a <c>string</c> representation of this CSV record.
        /// </summary>
        /// <remarks>
        /// This method is provided for debugging and diagnostics only. Each value in the record is present in the returned string, with a bullet
        /// character (<c>U+2022</c>) separating them.
        /// </remarks>
        /// <returns>
        /// A <c>string</c> representation of this record.
        /// </returns>
        public override sealed string ToString()
        {
            var retVal = new StringBuilder();

            foreach (string val in values)
            {
                retVal.Append(val).Append(ValueSeparator);
            }

            return retVal.ToString();
        }

        #endregion Methods
    }
}