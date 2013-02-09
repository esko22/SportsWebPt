using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

///This is ms-pl (open source) originally from http://kbcsv.codeplex.com/
namespace SportsWebPt.Common.Utilities
{
    /// <summary>
    /// Provides a mechanism via which CSV data can be easily written.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <c>CsvWriter</c> class allows CSV data to be written to any stream-based destination. By default, CSV values are separated by commas
    /// (<c>,</c>) and delimited by double quotes (<c>"</c>). If necessary, custom characters can be specified when creating the <c>CsvWriter</c>.
    /// </para>
    /// <para>
    /// The number of records that have been written so far is exposed via the <see cref="RecordNumber"/> property. Writing a header record does not
    /// increment this property.
    /// </para>
    /// <para>
    /// A CSV header record can be optionally written by the <c>CsvWriter</c>. If a header record is to be written, it must be done first thing with
    /// the <see cref="WriteHeaderRecord"/> method. If a header record is written, it is exposed via the <see cref="HeaderRecord"/> property.
    /// </para>
    /// <para>
    /// Data records can be written with the <see cref="WriteDataRecord"/> or <see cref="WriteDataRecords"/> methods. These methods are overloaded to
    /// accept either instances of <see cref="DataRecord"/> or an array of <c>string</c>s. In addition, the <see cref="WriteAll"/> overloads can
    /// be used to write data from a <see cref="DataTable"/> or <see cref="DataSet"/> instance to the <c>CsvWriter</c>.
    /// </para>
    /// </remarks>
    /// <threadsafety>
    /// The <c>CsvWriter</c> class does not lock internally. Therefore, it is unsafe to share an instance across threads without implementing your own
    /// synchronisation solution.
    /// </threadsafety>
    /// <example>
    /// <para>
    /// The following example writes some simple CSV data to a file:
    /// </para>
    /// <para>
    /// <code lang="C#">
    /// <![CDATA[
    /// using (CsvWriter writer = new CsvWriter(@"C:\Temp\data.csv")) {
    ///		writer.WriteHeaderRecord("Name", "Age", "Gender");
    ///		writer.WriteDataRecord("Kent", 25, Gender.Male);
    ///		writer.WriteDataRecord("Belinda", 26, Gender.Female);
    ///		writer.WriteDataRecord("Tempany", 0, Gender.Female);
    /// }
    /// ]]>
    /// </code>
    /// </para>
    /// <para>
    /// <code lang="vb">
    /// <![CDATA[
    /// Dim writer As CsvWriter = Nothing
    /// 
    /// Try
    ///		writer = New CsvWriter("C:\Temp\data.csv")
    ///		writer.WriteHeaderRecord("Name", "Age", "Gender")
    ///		writer.WriteDataRecord("Kent", 25, Gender.Male)
    ///		writer.WriteDataRecord("Belinda", 26, Gender.Female)
    ///		writer.WriteDataRecord("Tempany", 0, Gender.Female)
    /// Finally
    ///		If (Not writer Is Nothing) Then
    ///			writer.Close()
    ///		End If
    /// End Try
    /// ]]>
    /// </code>
    /// </para>
    /// </example>
    /// <example>
    /// <para>
    /// The following example writes the contents of a <see cref="DataTable"/> to a <see cref="MemoryStream"/>. CSV values are separated by tabs and
    /// delimited by the pipe characters (<c>|</c>). Linux-style line breaks are written by the <c>CsvWriter</c>, regardless of the hosting platform:
    /// </para>
    /// <para>
    /// <code lang="C#">
    /// <![CDATA[
    /// DataTable table = GetDataTable();
    /// MemoryStream memStream = new MemoryStream();
    /// 
    /// using (CsvWriter writer = new CsvWriter(memStream)) {
    ///		writer.NewLine = "\r";
    ///		writer.ValueSeparator = '\t';
    ///		writer.ValueDelimiter = '|';
    ///		writer.WriteAll(table, true);
    /// }
    /// ]]>
    /// </code>
    /// </para>
    /// <para>
    /// <code lang="vb">
    /// <![CDATA[
    /// Dim table As DataTable = GetDataTable
    /// Dim memStream As MemoryStream = New MemoryStream
    /// Dim writer As CsvWriter = Nothing
    /// 
    /// Try
    ///		writer = New CsvWriter(memStream)
    ///		writer.NewLine = vbLf
    ///		writer.ValueSeparator = vbTab
    ///		writer.ValueDelimiter = "|"c
    ///		writer.WriteAll(table, True)
    /// Finally
    ///		If (Not writer Is Nothing) Then
    ///			writer.Close()
    ///		End If
    /// End Try
    /// ]]>
    /// </code>
    /// </para>
    /// </example>
    public class CsvWriter : IDisposable
    {
        #region Fields

        /// <summary>
        /// The <see cref="TextWriter"/> used to output CSV data.
        /// </summary>
        private readonly TextWriter writer;

        /// <summary>
        /// The carriage return character. Escape code is <c>\r</c>.
        /// </summary>
        private const char CR = (char)0x0d;

        /// <summary>
        /// The line-feed character. Escape code is <c>\n</c>.
        /// </summary>
        private const char LF = (char)0x0a;

        /// <summary>
        /// The space character.
        /// </summary>
        private const char SPACE = ' ';

        /// <summary>
        /// See <see cref="AlwaysDelimit"/>.
        /// </summary>
        private bool alwaysDelimit;

        /// <summary>
        /// Set to <see langword="true"/> when this <c>CsvWriter</c> is disposed.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// See <see cref="HeaderRecord"/>.
        /// </summary>
        private CsvHeaderRecord headerRecord;

        /// <summary>
        /// Set to <see langword="true"/> once the first record is written.
        /// </summary>
        private bool passedFirstRecord;

        /// <summary>
        /// See <see cref="RecordNumber"/>.
        /// </summary>
        private long recordNumber;

        /// <summary>
        /// The buffer of characters containing the current value.
        /// </summary>
        private char[] valueBuffer;

        /// <summary>
        /// The last valid index into <see cref="valueBuffer"/>.
        /// </summary>
        private int valueBufferEndIndex;

        /// <summary>
        /// See <see cref="ValueDelimiter"/>.
        /// </summary>
        private char valueDelimiter;

        /// <summary>
        /// See <see cref="ValueSeparator"/>.
        /// </summary>
        private char valueSeparator;

        private static IPropertyCache propertyCache = new PropertyCache();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructs and initialises an instance of <c>CsvWriter</c> based on the information provided.
        /// </summary>
        /// <remarks>
        /// If the specified file already exists, it will be overwritten.
        /// </remarks>
        /// <param name="stream">
        /// The stream to which CSV data will be written.
        /// </param>
        public CsvWriter(Stream stream)
            : this(stream, Encoding.Default)
        {
        }

        /// <summary>
        /// Constructs and initialises an instance of <c>CsvWriter</c> based on the information provided.
        /// </summary>
        /// <remarks>
        /// If the specified file already exists, it will be overwritten.
        /// </remarks>
        /// <param name="stream">
        /// The stream to which CSV data will be written.
        /// </param>
        /// <param name="encoding">
        /// The encoding for the data in <paramref name="stream"/>.
        /// </param>
        public CsvWriter(Stream stream, Encoding encoding)
            : this(new StreamWriter(stream, encoding))
        {
        }

        /// <summary>
        /// Constructs and initialises an instance of <c>CsvWriter</c> based on the information provided.
        /// </summary>
        /// <remarks>
        /// If the specified file already exists, it will be overwritten.
        /// </remarks>
        /// <param name="path">
        /// The full path to the file to which CSV data will be written.
        /// </param>
        public CsvWriter(string path)
            : this(path, false, Encoding.Default)
        {
        }

        /// <summary>
        /// Constructs and initialises an instance of <c>CsvWriter</c> based on the information provided.
        /// </summary>
        /// <remarks>
        /// If the specified file already exists, it will be overwritten.
        /// </remarks>
        /// <param name="path">
        /// The full path to the file to which CSV data will be written.
        /// </param>
        /// <param name="encoding">
        /// The encoding for the data in <paramref name="path"/>.
        /// </param>
        public CsvWriter(string path, Encoding encoding)
            : this(path, false, encoding)
        {
        }

        /// <summary>
        /// Constructs and initialises an instance of <c>CsvWriter</c> based on the information provided.
        /// </summary>
        /// <remarks>
        /// If the specified file already exists, it will be overwritten.
        /// </remarks>
        /// <param name="path">
        /// The full path to the file to which CSV data will be written.
        /// </param>
        /// <param name="append">
        /// If <c>true</c>, data will be appended to the specified file.
        /// </param>
        public CsvWriter(string path, bool append)
            : this(path, append, Encoding.Default)
        {
        }

        /// <summary>
        /// Constructs and initialises an instance of <c>CsvWriter</c> based on the information provided.
        /// </summary>
        /// <remarks>
        /// If the specified file already exists, it will be overwritten.
        /// </remarks>
        /// <param name="path">
        /// The full path to the file to which CSV data will be written.
        /// </param>
        /// <param name="append">
        /// If <c>true</c>, data will be appended to the specified file.
        /// </param>
        /// <param name="encoding">
        /// The encoding for the data in <paramref name="path"/>.
        /// </param>
        public CsvWriter(string path, bool append, Encoding encoding)
            : this(new StreamWriter(path, append, encoding))
        {
        }

        /// <summary>
        /// Constructs and initialises an instance of <c>CsvWriter</c> based on the information provided.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="TextWriter"/> to which CSV data will be written.
        /// </param>
        public CsvWriter(TextWriter writer)
        {
            Check.Argument.IsNotNull(writer, "writer");
            this.writer = writer;
            valueSeparator = CsvParser.DefaultValueSeparator;
            valueDelimiter = CsvParser.DefaultValueDelimiter;
            valueBuffer = new char[128];
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether values should always be delimited.
        /// </summary>
        /// <remarks>
        /// By default the <c>CsvWriter</c> will only delimit values that require delimiting. Setting this property to <c>true</c> will ensure that all written values are
        /// delimited.
        /// </remarks>
        public bool AlwaysDelimit
        {
            get
            {
                EnsureNotDisposed();
                return alwaysDelimit;
            }
            set
            {
                EnsureNotDisposed();
                alwaysDelimit = value;
            }
        }

        /// <summary>
        /// Gets the encoding of the underlying writer for this <c>CsvWriter</c>.
        /// </summary>
        public Encoding Encoding
        {
            get
            {
                EnsureNotDisposed();
                return writer.Encoding;
            }
        }

        /// <summary>
        /// Gets the CSV header for this writer.
        /// </summary>
        /// <value>
        /// The CSV header record for this writer, or <see langword="null"/> if no header record applies.
        /// </value>
        /// <remarks>
        /// This property can be used to retrieve the <see cref="HeaderRecord"/> that represents the header record for this <c>CsvWriter</c>. If a
        /// header record has been written (using, for example, <see cref="WriteHeaderRecord"/>) then this property will retrieve the details of the
        /// header record. If a header record has not been written, this property will return <see langword="null"/>.
        /// </remarks>
        public CsvHeaderRecord HeaderRecord
        {
            get
            {
                EnsureNotDisposed();
                return headerRecord;
            }
        }

        /// <summary>
        /// Gets or sets the line terminator for this <c>CsvWriter</c>.
        /// </summary>
        /// <remarks>
        /// This property gets or sets the line terminator for the underlying <c>TextWriter</c> used by this <c>CsvWriter</c>. If this is set to <see langword="null"/> the
        /// default newline string is used instead.
        /// </remarks>
        public string NewLine
        {
            get
            {
                EnsureNotDisposed();
                return writer.NewLine;
            }
            set
            {
                EnsureNotDisposed();
                writer.NewLine = value;
            }
        }

        /// <summary>
        /// Gets the current record number.
        /// </summary>
        /// <remarks>
        /// This property gives the number of records that the <c>CsvWriter</c> has written. The CSV header does not count. That is, calling
        /// <see cref="WriteHeaderRecord"/> will not increment this property. Only successful calls to <see cref="WriteDataRecord"/> (and related methods)
        /// will increment this property.
        /// </remarks>
        public long RecordNumber
        {
            get
            {
                EnsureNotDisposed();
                return recordNumber;
            }
        }

        /// <summary>
        /// Gets the character possibly placed around values in the CSV data.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property retrieves the character that this <c>CsvWriter</c> will use to demarcate values in the CSV data. The default value of this
        /// property is a double quote (<c>"</c>).
        /// </para>
        /// <para>
        /// If <see cref="AlwaysDelimit"/> is <c>true</c>, then values written by this <c>CsvWriter</c> will always be delimited with this character. Otherwise, the
        /// <c>CsvWriter</c> will decide whether values must be delimited based on their content.
        /// </para>
        /// </remarks>
        public char ValueDelimiter
        {
            get
            {
                EnsureNotDisposed();
                return valueDelimiter;
            }
            set
            {
                EnsureNotDisposed();
               Check.Argument.IsNotTrue(value == valueSeparator, TextResources.CsvValueSeparatorCannotBeSameAsDelimiter);
                Check.Argument.IsNotTrue(value == SPACE, TextResources.CsvValueDelimiterCannotBeSpace);
                valueDelimiter = value;
            }
        }

        /// <summary>
        /// Gets or sets the character placed between values in the CSV data.
        /// </summary>
        /// <remarks>
        /// This property retrieves the character that this <c>CsvWriter</c> will use to separate distinct values in the CSV data. The default value
        /// of this property is a comma (<c>,</c>).
        /// </remarks>
        public char ValueSeparator
        {
            get
            {
                EnsureNotDisposed();
                return valueSeparator;
            }
            set
            {
                EnsureNotDisposed();
                Check.Argument.IsNotTrue(value == valueDelimiter, TextResources.CsvValueSeparatorCannotBeSameAsDelimiter);
                Check.Argument.IsNotTrue(value == SPACE, TextResources.CsvValueSeparatorCannotBeSpace);
                valueSeparator = value;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Closes this <c>CsvWriter</c> instance and releases all resources acquired by it.
        /// </summary>
        /// <remarks>
        /// Once an instance of <c>CsvWriter</c> is no longer needed, call this method to immediately release any resources. Closing a <c>CsvWriter</c> is equivalent to
        /// disposing of it in a C# <c>using</c> block.
        /// </remarks>
        public void Close()
        {
            if (writer != null)
            {
                writer.Close();
            }

            disposed = true;
        }

        /// <summary>
        /// Flushes the underlying buffer of this <c>CsvWriter</c>.
        /// </summary>
        /// <remarks>
        /// This method can be used to flush the underlying <c>Stream</c> that this <c>CsvWriter</c> writes to.
        /// </remarks>
        public void Flush()
        {
            EnsureNotDisposed();
            writer.Flush();
        }

        /// <summary>
        /// Disposes of this <c>CsvWriter</c> instance.
        /// </summary>
        void IDisposable.Dispose()
        {
            Close();
            Dispose(true);
        }

        /// <summary>
        /// Writes the data in <paramref name="table"/> as CSV data.
        /// </summary>
        /// <remarks>
        /// This method writes all the data in <paramref name="table"/> to this <c>CsvWriter</c>, including a header record. If a header record has already
        /// been written to this <c>CsvWriter</c> this method will throw an exception. That being the case, you should use <see cref="WriteAll(DataTable, bool)"/>
        /// instead, specifying <see langword="false"/> for the second parameter.
        /// </remarks>
        /// <param name="table">
        /// The <c>DataTable</c> whose data is to be written as CSV data.
        /// </param>
        public void WriteAll(DataTable table)
        {
            WriteAll(table, true);
        }

        public void WriteAll<TData>(IList<TData> list, bool writeHeaderRecord) where TData: class{
            WriteAll(list, null, writeHeaderRecord);
        }

        public void WriteAll<TData>(IList<TData> list) where TData : class
        {
            WriteAll(list, null, true);
        }

        public void WriteAll<TData>(IList<TData> list,IList<ColumnData<TData>> columns, bool writeHeaderRecord) where TData: class{
           
            EnsureNotDisposed();

            if (columns == null || columns.Count() == 0)
            {
                var generator = new ColumnDataGenerator(propertyCache);
                columns = generator.GetColumns<TData>().ToList();
            }

            Check.Argument.IsNotNull(list, "list");

            if (writeHeaderRecord)
            {
                var header = new CsvHeaderRecord();

                foreach (var col in columns )
                {
                    header.Values.Add(col.Title);
                }

                WriteHeaderRecord(header);
            }

            foreach (var row in list)
            {
                var dataRecord = new CsvDataRecord(headerRecord);

                        var colCount = columns.Count();
                        var r = new string[colCount];
                        for(int i = 0; i < colCount; ++i ){
                            string val = columns[i].Value(row) == null
                                                 ? string.Empty
                                                 : columns[i].Value(row).ToString();
                            if (string.IsNullOrEmpty(columns[i].Format)){
                                dataRecord.Values.Add(val);
                            }
                            else{
                              dataRecord.Values.Add(columns[i].Format.FormatWith(val));
                            }
                        }
               
                WriteDataRecord(dataRecord);
            }
        }

        /// <summary>
        /// Writes the data in <paramref name="table"/> as CSV data.
        /// </summary>
        /// <remarks>
        /// This method writes all the data in <paramref name="table"/> to this <c>CsvWriter</c>, optionally writing a header record based on the columns in the
        /// table.
        /// </remarks>
        /// <param name="table">
        /// The <c>DataTable</c> whose data is to be written as CSV data.
        /// </param>
        /// <param name="writeHeaderRecord">
        /// If <see langword="true"/>, a CSV header will be written based on the column names for the table.
        /// </param>
        public void WriteAll(DataTable table, bool writeHeaderRecord)
        {
            EnsureNotDisposed();
            Check.Argument.IsNotNull(table, "table");

            if (writeHeaderRecord)
            {
                var header = new CsvHeaderRecord();

                foreach (DataColumn column in table.Columns)
                {
                    header.Values.Add(column.ColumnName);
                }

                WriteHeaderRecord(header);
            }

            foreach (DataRow row in table.Rows)
            {
                var dataRecord = new CsvDataRecord(headerRecord);

                foreach (object item in row.ItemArray)
                {
                    dataRecord.Values.Add((item == null) ? string.Empty : item.ToString());
                }

                WriteDataRecord(dataRecord);
            }
        }

        /// <summary>
        /// Writes the first <see cref="DataTable"/> in <paramref name="dataSet"/> as CSV data.
        /// </summary>
        /// <remarks>
        /// This method writes all the data in the first table of <paramref name="dataSet"/> to this <c>CsvWriter</c>, including a header record.
        /// If a header record has already been written to this <c>CsvWriter</c> this method will throw an exception. That being the case, you
        /// should use <see cref="WriteAll(DataSet, bool)"/> instead, specifying <see langword="false"/> for the second parameter.
        /// </remarks>
        /// <param name="dataSet">
        /// The <c>DataSet</c> whose first table is to be written as CSV data.
        /// </param>
        public void WriteAll(DataSet dataSet)
        {
            WriteAll(dataSet, true);
        }

        /// <summary>
        /// Writes the first <see cref="DataTable"/> in <paramref name="dataSet"/> as CSV data.
        /// </summary>
        /// <remarks>
        /// This method writes all the data in the first table of <paramref name="dataSet"/> to this <c>CsvWriter</c>, optionally writing a header
        /// record based on the columns in the table.
        /// </remarks>
        /// <param name="dataSet">
        /// The <c>DataSet</c> whose first table is to be written as CSV data.
        /// </param>
        /// <param name="writeHeaderRecord">
        /// If <see langword="true"/>, a CSV header will be written based on the column names for the table.
        /// </param>
        public void WriteAll(DataSet dataSet, bool writeHeaderRecord)
        {
            EnsureNotDisposed();
               Check.Argument.IsNotNull( dataSet, "dataSet");
               Check.Argument.IsNotTrue(dataSet.Tables.Count == 0, TextResources.CsvDatasetIsEmpty);

            WriteAll(dataSet.Tables[0], writeHeaderRecord);
        }

        /// <summary>
        /// Writes the specified record to this <c>CsvWriter</c>.
        /// </summary>
        /// <remarks>
        /// This method writes a single data record to this <c>CsvWriter</c>. The <see cref="RecordNumber"/> property is incremented upon successfully writing
        /// the record.
        /// </remarks>
        /// <param name="dataRecord">
        /// The record to be written.
        /// </param>
        public void WriteDataRecord(CsvDataRecord dataRecord)
        {
            EnsureNotDisposed();
               Check.Argument.IsNotNull(dataRecord,"dataRecord");

            WriteRecord(dataRecord.Values.ToArray());
        }

        /// <summary>
        /// Writes a data record with the specified values.
        /// </summary>
        /// <remarks>
        /// Each item in <paramref name="dataRecord"/> is converted to a <c>string</c> via its <c>ToString</c> implementation. If any item is <see langword="null"/>, it is substituted
        /// for an empty <c>string</c> (<see cref="string.Empty"/>).
        /// </remarks>
        /// <param name="dataRecord">
        /// An array of data values.
        /// </param>
        public void WriteDataRecord(params object[] dataRecord)
        {
            Check.Argument.IsNotNull(dataRecord,"dataRecord");

            var dataRecordAsStrings = new string[dataRecord.Length];

            for (int i = 0; i < dataRecordAsStrings.Length; ++i){
                object o = dataRecord[i];
                dataRecordAsStrings[i] = o != null ? o.ToString() : string.Empty;
            }

            WriteDataRecord(dataRecordAsStrings);
        }

        /// <summary>
        /// Writes the specified record to this <c>CsvWriter</c>.
        /// </summary>
        /// <remarks>
        /// This method writes a single data record to this <c>CsvWriter</c>. The <see cref="RecordNumber"/> property is incremented upon successfully writing
        /// the record.
        /// </remarks>
        /// <param name="dataRecord">
        /// The record to be written.
        /// </param>
        public void WriteDataRecord(string[] dataRecord)
        {
            EnsureNotDisposed();
               Check.Argument.IsNotNull(dataRecord,"dataRecord");

            WriteRecord(dataRecord);
            ++recordNumber;
        }

        /// <summary>
        /// Writes all records specified by <paramref name="dataRecords"/> to this <c>CsvWriter</c>.
        /// </summary>
        /// <remarks>
        /// This method writes all data records in <paramref name="dataRecords"/> to this <c>CsvWriter</c> and increments the <see cref="RecordNumber"/> property
        /// as records are written.
        /// </remarks>
        /// <param name="dataRecords">
        /// The records to be written.
        /// </param>
        public void WriteDataRecords(ICollection<CsvDataRecord> dataRecords)
        {
            EnsureNotDisposed();
            Check.Argument.IsNotNull(dataRecords, "dataRecords");

            foreach (CsvDataRecord dataRecord in dataRecords)
            {
                WriteRecord(dataRecord.Values);
            }
        }

        /// <summary>
        /// Writes all records specified by <paramref name="dataRecords"/> to this <c>CsvWriter</c>.
        /// </summary>
        /// <remarks>
        /// This method writes all data records in <paramref name="dataRecords"/> to this <c>CsvWriter</c> and increments the <see cref="RecordNumber"/> property
        /// as records are written.
        /// </remarks>
        /// <param name="dataRecords">
        /// The records to be written.
        /// </param>
        public void WriteDataRecords(ICollection<string[]> dataRecords)
        {
            EnsureNotDisposed();
            Check.Argument.IsNotNull(dataRecords, "dataRecords");

            foreach (string[] dataRecord in dataRecords)
            {
                WriteRecord(dataRecord);
            }
        }

        /// <summary>
        /// Writes the specified header record.
        /// </summary>
        /// <remarks>
        /// This method writes the specified header record to the underlying <c>Stream</c>. Once successfully written, the header record is exposed via
        /// the <see cref="HeaderRecord"/> property.
        /// </remarks>
        /// <param name="header">
        /// The CSV header record to be written.
        /// </param>
        public void WriteHeaderRecord(CsvHeaderRecord header)
        {
            Check.Argument.IsNotNull(header, "headerRecord");
            Check.Argument.IsNotTrue(passedFirstRecord, TextResources.CsvAlreadyWroteFirstRecord);
            this.headerRecord = header;
            WriteRecord(headerRecord.Values.ToArray());
        }

        /// <summary>
        /// Writes a header record with the specified columns.
        /// </summary>
        /// <remarks>
        /// Each item in <paramref name="header"/> is converted to a <c>string</c> via its <c>ToString</c> implementation. If any item is <see langword="null"/>,
        /// it is substituted for an empty <c>string</c> (<see cref="string.Empty"/>).
        /// </remarks>
        /// <param name="header">
        /// An array of header column names.
        /// </param>
        public void WriteHeaderRecord(params object[] header)
        {
            Check.Argument.IsNotNull(header, "headerRecord");

            var headerRecordAsStrings = new string[header.Length];

            for (int i = 0; i < headerRecordAsStrings.Length; ++i){
                object o = header[i];

                headerRecordAsStrings[i] = o != null ? o.ToString() : string.Empty;
            }

            WriteHeaderRecord(headerRecordAsStrings);
        }

        /// <summary>
        /// Writes the specified header record.
        /// </summary>
        /// <remarks>
        /// This method writes the specified header record to the underlying <c>Stream</c>. Once successfully written, the header record is exposed via
        /// the <see cref="HeaderRecord"/> property.
        /// </remarks>
        /// <param name="header">
        /// The CSV header record to be written.
        /// </param>
        public void WriteHeaderRecord(string[] header)
        {
            EnsureNotDisposed();
               Check.Argument.IsNotNull(header, "header");
            if(passedFirstRecord){
                throw new InvalidOperationException(TextResources.CsvAlreadyWroteFirstRecord);
            }
               
            headerRecord = new CsvHeaderRecord(header, true);
            WriteRecord(headerRecord.Values);
        }

        /// <summary>
        /// Allows sub classes to implement disposing logic.
        /// </summary>
        /// <param name="disposing">
        /// <see langword="true"/> if this method is being called in response to a <see cref="Dispose"/> call, or <see langword="false"/> if
        /// it is being called during finalization.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// Appends the specified character onto the end of the current value.
        /// </summary>
        /// <param name="c">
        /// The character to append.
        /// </param>
        private void AppendToValue(char c)
        {
            EnsureValueBufferCapacity(1);
            valueBuffer[valueBufferEndIndex++] = c;
        }

        /// <summary>
        /// Makes sure the object isn't disposed and, if so, throws an exception.
        /// </summary>
        private void EnsureNotDisposed()
        {
            if(disposed){
                throw new ObjectDisposedException("Object is disposed");
            }
        }

        /// <summary>
        /// Ensures the value buffer contains enough space for <paramref name="count"/> more characters.
        /// </summary>
        private void EnsureValueBufferCapacity(int count)
        {
            if ((valueBufferEndIndex + count) > valueBuffer.Length)
            {
                var newBuffer = new char[Math.Max(valueBuffer.Length * 2, (count >> 1) << 2)];

                //profiling revealed a loop to be faster than Array.Copy, despite Array.Copy having an internal implementation
                for (int i = 0; i < valueBufferEndIndex; ++i)
                {
                    newBuffer[i] = valueBuffer[i];
                }

                valueBuffer = newBuffer;
            }
        }

        /// <summary>
        /// Writes the specified record to the target <see cref="TextWriter"/>, ensuring all values are correctly separated and escaped.
        /// </summary>
        /// <remarks>
        /// This method is used internally by the <c>CsvWriter</c> to write CSV records.
        /// </remarks>
        /// <param name="record">
        /// The record to be written.
        /// </param>
        private void WriteRecord(IEnumerable<string> record)
        {
            bool firstValue = true;

            foreach (string value in record)
            {
                if (!firstValue)
                {
                    writer.Write(valueSeparator);
                }
                else
                {
                    firstValue = false;
                }

                WriteValue(value);
            }

            //uses the underlying TextWriter.NewLine property
            writer.WriteLine();
            passedFirstRecord = true;
        }

        /// <summary>
        /// Writes the specified value to the target <see cref="TextWriter"/>, ensuring it is correctly escaped.
        /// </summary>
        /// <remarks>
        /// This method is used internally by the <c>CsvWriter</c> to write individual CSV values.
        /// </remarks>
        /// <param name="val">
        /// The value to be written.
        /// </param>
        private void WriteValue(string val)
        {
            valueBufferEndIndex = 0;
            bool delimit = alwaysDelimit;

            if (!string.IsNullOrEmpty(val))
            {
                //delimit to preserve white-space at the beginning or end of the value
                if ((val[0] == SPACE) || (val[val.Length - 1] == SPACE))
                {
                    delimit = true;
                }

                for (int i = 0; i < val.Length; ++i)
                {
                    char c = val[i];

                    if ((c == valueSeparator) || (c == CR) || (c == LF))
                    {
                        //all these characters require the value to be delimited
                        AppendToValue(c);
                        delimit = true;
                    }
                    else if (c == valueDelimiter)
                    {
                        //escape the delimiter by writing it twice
                        AppendToValue(valueDelimiter);
                        AppendToValue(valueDelimiter);
                        delimit = true;
                    }
                    else
                    {
                        AppendToValue(c);
                    }
                }
            }

            if (delimit)
            {
                writer.Write(valueDelimiter);
            }

            //write the value
            writer.Write(valueBuffer, 0, valueBufferEndIndex);

            if (delimit)
            {
                writer.Write(valueDelimiter);
            }

            valueBufferEndIndex = 0;
        }

        #endregion Methods
    }
}