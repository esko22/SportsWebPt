using System;
using System.Diagnostics;
using System.IO;

namespace SportsWebPt.Common.Utilities
{
    /// <summary>
    /// Implements the CSV parser.
    /// </summary>
    /// <remarks>
    /// This class implements the CSV parsing capabilities of KBCsv.
    /// </remarks>
    internal sealed class CsvParser : IDisposable
    {
        #region Fields

        /// <summary>
        /// The default value delimiter.
        /// </summary>
        public const char DefaultValueDelimiter = '"';

        /// <summary>
        /// The default value separator.
        /// </summary>
        public const char DefaultValueSeparator = ',';

        /// <summary>
        /// Buffers CSV data.
        /// </summary>
        private readonly char[] buffer;

        /// <summary>
        /// The source of the CSV data.
        /// </summary>
        private readonly TextReader reader;

        /// <summary>
        /// One char less than the size of the internal buffer. The extra char is used to support a faster peek operation.
        /// </summary>
        private const int BUFFER_SIZE = 2047;

        /// <summary>
        /// The carriage return character. Escape code is <c>\r</c>.
        /// </summary>
        private const char CR = (char) 0x0d;

        /// <summary>
        /// The line-feed character. Escape code is <c>\n</c>.
        /// </summary>
        private const char LF = (char) 0x0a;

        /// <summary>
        /// The space character.
        /// </summary>
        private const char SPACE = ' ';

        /// <summary>
        /// The tab character.
        /// </summary>
        private const char TAB = '\t';

        /// <summary>
        /// The last valid index into <see cref="buffer"/>.
        /// </summary>
        private int bufferEndIndex;

        /// <summary>
        /// The current index into <see cref="buffer"/>.
        /// </summary>
        private int bufferIndex;

        /// <summary>
        /// <see langword="true"/> if the current value is delimited and the parser is in the delimited area.
        /// </summary>
        private bool inDelimitedArea;

        /// <summary>
        /// Set to <see langword="true"/> once the first record is passed (or the <see cref="CsvReader"/> decides that the first record has been passed.
        /// </summary>
        private bool passedFirstRecord;

        /// <summary>
        /// See <see cref="PreserveLeadingWhiteSpace"/>.
        /// </summary>
        private bool preserveLeadingWhiteSpace;

        /// <summary>
        /// See <see cref="PreserveTrailingWhiteSpace"/>.
        /// </summary>
        private bool preserveTrailingWhiteSpace;

        /// <summary>
        /// Used to quickly recognise whether a character is potentially special or not.
        /// </summary>
        private int specialCharacterMask;

        /// <summary>
        /// The buffer of characters containing the current value.
        /// </summary>
        private char[] valueBuffer;

        /// <summary>
        /// The last valid index into <see cref="valueBuffer"/>.
        /// </summary>
        private int valueBufferEndIndex;

        /// <summary>
        /// An index into <see cref="valueBuffer"/> indicating the first character that might be removed if it is leading white-space.
        /// </summary>
        private int valueBufferFirstEligibleLeadingWhiteSpace;

        /// <summary>
        /// An index into <see cref="valueBuffer"/> indicating the first character that might be removed if it is trailing white-space.
        /// </summary>
        private int valueBufferFirstEligibleTrailingWhiteSpace;

        /// <summary>
        /// See <see cref="ValueDelimiter"/>.
        /// </summary>
        private char valueDelimiter;

        /// <summary>
        /// The list of values currently parsed by the parser.
        /// </summary>
        private string[] valueList;

        /// <summary>
        /// The starting index of the current value part.
        /// </summary>
        private int valuePartStartIndex;

        /// <summary>
        /// See <see cref="ValueSeparator"/>.
        /// </summary>
        private char valueSeparator;

        /// <summary>
        /// The last valid index into <see cref="valueList"/>.
        /// </summary>
        private int valuesListEndIndex;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructs and inititialises an instance of <c>CsvParser</c> with the details provided.
        /// </summary>
        /// <param name="reader">
        /// The instance of <see cref="TextReader"/> from which CSV data will be read.
        /// </param>
        public CsvParser(TextReader reader)
        {
            Check.Argument.IsNotNull(reader, "reader");

            this.reader = reader;
            //the extra char is used to facilitate a faster peek operation
            this.buffer = new char[BUFFER_SIZE + 1];
            this.valueList = new string[16];
            this.valueBuffer = new char[128];
            this.valuePartStartIndex = -1;
            //set defaults
            this.valueSeparator = DefaultValueSeparator;
            this.valueDelimiter = DefaultValueDelimiter;
            //get the default special character mask
            UpdateSpecialCharacterMask();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the parser's buffer contains more records in addition to those already parsed.
        /// </summary>
        public bool HasMoreRecords
        {
            get
            {
                if (this.bufferIndex < this.bufferEndIndex)
                {
                    //the buffer isn't empty so there must be more records
                    return true;
                }

                //the buffer is empty so peek into the reader to see whether there is more data
                return (this.reader.Peek() != -1);

            }
        }

        /// <summary>
        /// Gets a value indicating whether the parser has passed the first record in the input source.
        /// </summary>
        public bool PassedFirstRecord
        {
            get { return this.passedFirstRecord; }
            set { this.passedFirstRecord = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether leading whitespace is to be preserved.
        /// </summary>
        public bool PreserveLeadingWhiteSpace
        {
            get { return this.preserveLeadingWhiteSpace; }
            set { this.preserveLeadingWhiteSpace = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether trailing whitespace is to be preserved.
        /// </summary>
        public bool PreserveTrailingWhiteSpace
        {
            get { return this.preserveTrailingWhiteSpace; }
            set { this.preserveTrailingWhiteSpace = value; }
        }

        /// <summary>
        /// Gets or sets the character that optionally delimits values in the CSV data.
        /// </summary>
        public char ValueDelimiter
        {
            get { return this.valueDelimiter; }
            set{
                Check.Argument.IsNotTrue(value == this.valueSeparator,
                                         TextResources.CsvValueSeparatorCannotBeSameAsDelimiter);
                Check.Argument.IsNotTrue(value == SPACE, TextResources.CsvValueDelimiterCannotBeSpace);

                this.valueDelimiter = value;
                UpdateSpecialCharacterMask();
            }
        }

        /// <summary>
        /// Gets or sets the character that separates values in the CSV data.
        /// </summary>
        public char ValueSeparator
        {
            get { return this.valueSeparator; }
            set{
                Check.Argument.IsNotTrue(value == this.valueDelimiter,
                                         TextResources.CsvValueSeparatorCannotBeSameAsDelimiter);
                Check.Argument.IsNotTrue(value == SPACE, TextResources.CsvValueSeparatorCannotBeSpace);
                this.valueSeparator = value;
                UpdateSpecialCharacterMask();
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Closes this <c>CsvParser</c> instance and releases all resources acquired by it.
        /// </summary>
        public void Close()
        {
            if (this.reader != null){
                this.reader.Close();
            }
        }

        /// <summary>
        /// Disposes of this <c>CsvParser</c> instance.
        /// </summary>
        void IDisposable.Dispose()
        {
            Close();
        }

        /// <summary>
        /// Reads and parses the CSV into a <c>string</c> array containing the values contained in a single CSV record.
        /// </summary>
        /// <returns>
        /// An array of field values for the record, or <see langword="null"/> if no record was found.
        /// </returns>
        public string[] ParseRecord()
        {
            this.valuesListEndIndex = 0;
            char c = char.MinValue;
            //taking a local copy allows optimisations that otherwise could not be performed because the CLR knows that no other thread
            //can touch our local reference
            char[] charBuffer = this.buffer;

            while (true){
                if (this.bufferIndex != this.bufferEndIndex){
                    if (this.valuePartStartIndex == -1){
                        this.valuePartStartIndex = this.bufferIndex;
                    }

                    c = charBuffer[this.bufferIndex++];

                    if ((c & this.specialCharacterMask) == c){
                        if (!this.inDelimitedArea){
                            if (c == this.valueDelimiter){
                                //found a delimiter so enter delimited area and set the start index for the value
                                this.inDelimitedArea = true;
                                CloseValuePartExcludeCurrent();
                                this.valuePartStartIndex = this.bufferIndex;

                                //we have to make sure that delimited text isn't stripped if it is white-space
                                if (this.valueBufferFirstEligibleLeadingWhiteSpace == 0){
                                    this.valueBufferFirstEligibleLeadingWhiteSpace = this.valueBufferEndIndex;
                                }
                            }
                            else if (c == this.valueSeparator){
                                CloseValue(false);
                            }
                            else if (c == CR){
                                CloseValue(false);
                                if (charBuffer[this.bufferIndex] == LF){
                                    SwallowChar();
                                }
                                break;
                            }
                            else if (c == LF){
                                CloseValue(false);
                                break;
                            }
                        }
                        else if (c == this.valueDelimiter){
                            if (charBuffer[this.bufferIndex] == this.valueDelimiter){
                                CloseValuePart();
                                SwallowChar();
                                this.valuePartStartIndex = this.bufferIndex;
                            }
                            else{
                                //delimiter isn't escaped so we are no longer in a delimited area
                                this.inDelimitedArea = false;
                                CloseValuePartExcludeCurrent();
                                this.valuePartStartIndex = this.bufferIndex;
                                //we have to make sure that delimited text isn't stripped if it is white-space
                                this.valueBufferFirstEligibleTrailingWhiteSpace = this.valueBufferEndIndex;
                            }
                        }
                    }
                }
                else if (!FillBuffer()){
                    //special case: if the last character was a separator we need to add a blank value. eg. CSV "Value," will result in "Value", ""
                    if (c == this.valueSeparator){
                        AddValue(string.Empty);
                    }

                    //data exhausted - get out of loop
                    break;
                }
            }

            //this will return null if there are no values
            return GetValues();
        }

        /// <summary>
        /// Efficiently skips the next CSV record.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if a record was successfully skipped, otherwise <see langword="false"/>.
        /// </returns>
        public bool SkipRecord(){
            if (HasMoreRecords){
                //taking a local copy allows optimisations that otherwise could not be performed because the CLR knows that no other thread
                //can touch our local reference
                char[] charBuffer = this.buffer;

                while (true){
                    if (this.bufferIndex != this.bufferEndIndex){
                        char c = charBuffer[this.bufferIndex++];

                        if ((c & this.specialCharacterMask) == c){
                            if (!this.inDelimitedArea){
                                if (c == this.valueDelimiter){
                                    //found a delimiter so enter delimited area and set the start index for the value
                                    this.inDelimitedArea = true;
                                }
                                else if (c == CR){
                                    if (charBuffer[this.bufferIndex] == LF){
                                        SwallowChar();
                                    }
                                    return true;
                                }
                                else if (c == LF){
                                    return true;
                                }
                            }
                            else if (c == this.valueDelimiter){
                                if (charBuffer[this.bufferIndex] != this.valueDelimiter){
                                    //delimiter isn't escaped so we are no longer in a delimited area
                                    this.inDelimitedArea = false;
                                }
                            }
                        }
                    }
                    else if (!FillBufferIgnoreValues()){
                        //data exhausted - get out of here
                        return true;
                    }
                }
            }

            //no more records - can't skip
            return false;

        }

        /// <summary>
        /// Determines whether <paramref name="c"/> is white-space.
        /// </summary>
        /// <param name="c">
        /// The character to check.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="c"/> is white-space, otherwise <see langword="false"/>.
        /// </returns>
        internal static bool IsWhiteSpace(char c)
        {
            return ((c == SPACE) || (c == TAB));
        }

        /// <summary>
        /// Adds a value to the value list.
        /// </summary>
        /// <param name="val">
        /// The value to add.
        /// </param>
        private void AddValue(string val)
        {
            EnsureValueListCapacity();
            this.valueList[this.valuesListEndIndex++] = val;
        }

        /// <summary>
        /// Appends the specified characters from <see cref="buffer"/> onto the end of the current value.
        /// </summary>
        /// <param name="startIndex">
        /// The index at which to begin copying.
        /// </param>
        /// <param name="endIndex">
        /// The index at which to cease copying. The character at this index is not copied.
        /// </param>
        private void AppendToValue(int startIndex, int endIndex)
        {
            EnsureValueBufferCapacity(endIndex - startIndex);
            char[] charValueBuffer = this.valueBuffer;
            char[] charBuffer = this.buffer;

            //profiling revealed a loop to be faster than Array.Copy
            //in addition, profiling revealed that taking a local copy of the _buffer reference impedes performance here
            for (int i = startIndex; i < endIndex; ++i){
                charValueBuffer[this.valueBufferEndIndex++] = charBuffer[i];
            }
        }

        /// <summary>
        /// Closes the current value by adding it to the list of values in the current record. Assumes that there is actually a value to add, either in <c>_value</c> or in
        /// <see cref="buffer"/> starting at <see cref="valuePartStartIndex"/> and ending at <see cref="bufferIndex"/>.
        /// </summary>
        /// <param name="includeCurrentChar">
        /// If <see langword="true"/>, the current character is included in the value. Otherwise, it is excluded.
        /// </param>
        private void CloseValue(bool includeCurrentChar)
        {
            int endIndex = this.bufferIndex;

            if ((!includeCurrentChar) && (endIndex > this.valuePartStartIndex)){
                endIndex -= 1;
            }

            Debug.Assert(this.valuePartStartIndex >= 0, "valuePartStartIndex must be > 0");
            Debug.Assert(this.valuePartStartIndex <= this.bufferIndex,
                         "valuePartStartIndex must be less than or equal to _bufferIndex (" + this.valuePartStartIndex +
                         " > " + this.bufferIndex + ")");
            Debug.Assert(this.valuePartStartIndex <= endIndex,
                         "valuePartStartIndex must be less than or equal to endIndex (" + this.valuePartStartIndex +
                         " > " + endIndex + ")");

            if (this.valueBufferEndIndex == 0){
                if (endIndex == 0){
                    AddValue(string.Empty);
                }
                else{
                    //the value did not require the use of the ValueBuilder
                    int startIndex = this.valuePartStartIndex;
                    //taking a local copy allows optimisations that otherwise could not be performed because the CLR knows that no other thread
                    //can touch our local reference
                    char[] charBuffer = this.buffer;

                    if (!this.preserveLeadingWhiteSpace){
                        //strip all leading white-space
                        while ((startIndex < endIndex) && (IsWhiteSpace(charBuffer[startIndex]))){
                            ++startIndex;
                        }
                    }

                    if (!this.preserveTrailingWhiteSpace){
                        //strip all trailing white-space
                        while ((endIndex > startIndex) && (IsWhiteSpace(charBuffer[endIndex - 1]))){
                            --endIndex;
                        }
                    }

                    AddValue(new string(charBuffer, startIndex, endIndex - startIndex));
                }
            }
            else{
                //we needed the ValueBuilder to compose the value
                AppendToValue(this.valuePartStartIndex, endIndex);

                if (!this.preserveLeadingWhiteSpace || !this.preserveTrailingWhiteSpace){
                    //strip all white-space prior to _valueBufferFirstEligibleLeadingWhiteSpace and after _valueBufferFirstEligibleTrailingWhiteSpace
                    AddValue(GetValue(this.valueBufferFirstEligibleLeadingWhiteSpace,
                                      this.valueBufferFirstEligibleTrailingWhiteSpace));
                }
                else{
                    AddValue(GetValue());
                }

                this.valueBufferEndIndex = 0;
                this.valueBufferFirstEligibleLeadingWhiteSpace = 0;
            }

            this.valuePartStartIndex = -1;
        }

        /// <summary>
        /// Closes the current value part.
        /// </summary>
        private void CloseValuePart()
        {
            AppendToValue(this.valuePartStartIndex, this.bufferIndex);
            this.valuePartStartIndex = -1;
        }

        /// <summary>
        /// Closes the current value part, but excludes the current character from the value part.
        /// </summary>
        private void CloseValuePartExcludeCurrent()
        {
            AppendToValue(this.valuePartStartIndex, this.bufferIndex - 1);
        }

        /// <summary>
        /// Ensures the value buffer contains enough space for <paramref name="count"/> more characters.
        /// </summary>
        private void EnsureValueBufferCapacity(int count)
        {
            if ((this.valueBufferEndIndex + count) > this.valueBuffer.Length){
                var newBuffer = new char[Math.Max(this.valueBuffer.Length*2, (count >> 1) << 2)];

                //profiling revealed a loop to be faster than Array.Copy, despite Array.Copy having an internal implementation
                for (int i = 0; i < this.valueBufferEndIndex; ++i){
                    newBuffer[i] = this.valueBuffer[i];
                }

                this.valueBuffer = newBuffer;
            }
        }

        /// <summary>
        /// Ensures the value list contains enough space for another value, and increases its size if not.
        /// </summary>
        private void EnsureValueListCapacity()
        {
            if (this.valuesListEndIndex == this.valueList.Length){
                var newBuffer = new string[this.valueList.Length*2];

                for (int i = 0; i < this.valuesListEndIndex; ++i){
                    newBuffer[i] = this.valueList[i];
                }

                this.valueList = newBuffer;
            }
        }

        /// <summary>
        /// Fills that data buffer. Assumes that the buffer is already exhausted.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if data was read into the buffer, otherwise <see langword="false"/>.
        /// </returns>
        private bool FillBuffer()
        {
            Debug.Assert(this.bufferIndex == this.bufferEndIndex);

            //may need to close a value or value part depending on the state of the stream
            if (this.valuePartStartIndex != -1){
                if (this.reader.Peek() != -1){
                    CloseValuePart();
                    this.valuePartStartIndex = 0;
                }
                else{
                    CloseValue(true);
                }
            }

            this.bufferEndIndex = this.reader.Read(this.buffer, 0, BUFFER_SIZE);
            //this is possible because the buffer is one char bigger than BUFFER_SIZE. This fact is used to implement a faster peek operation
            this.buffer[this.bufferEndIndex] = (char) this.reader.Peek();
            this.bufferIndex = 0;
            this.passedFirstRecord = true;
            return (this.bufferEndIndex > 0);
        }

        /// <summary>
        /// Fills the buffer with data, but does not bother with closing values. This is used from the <see cref="SkipRecord"/> method,
        /// since that does not concern itself with values.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if data was read into the buffer, otherwise <see langword="false"/>.
        /// </returns>
        private bool FillBufferIgnoreValues()
        {
            Debug.Assert(this.bufferIndex == this.bufferEndIndex);
            this.bufferEndIndex = this.reader.Read(this.buffer, 0, BUFFER_SIZE);
            //this is possible because the buffer is one char bigger than BUFFER_SIZE. This fact is used to implement a faster peek operation
            this.buffer[this.bufferEndIndex] = (char) this.reader.Peek();
            this.bufferIndex = 0;
            this.passedFirstRecord = true;
            return (this.bufferEndIndex > 0);
        }

        /// <summary>
        /// Gets the current value.
        /// </summary>
        /// <returns></returns>
        private string GetValue()
        {
            return new string(this.valueBuffer, 0, this.valueBufferEndIndex);
        }

        /// <summary>
        /// Gets the current value, optionally removing trailing white-space.
        /// </summary>
        /// <param name="firstEligibleLeadingWhiteSpace">
        /// The index of the first character that cannot possibly be leading white-space.
        /// </param>
        /// <param name="firstEligibleTrailingWhiteSpace">
        /// The index of the first character that may be trailing white-space.
        /// </param>
        /// <returns>
        /// An instance of <c>string</c> containing the resultant value.
        /// </returns>
        private string GetValue(int firstEligibleLeadingWhiteSpace, int firstEligibleTrailingWhiteSpace)
        {
            int startIndex = 0;
            int endIndex = this.valueBufferEndIndex - 1;

            if (!this.preserveLeadingWhiteSpace){
                while ((startIndex < firstEligibleLeadingWhiteSpace) && (IsWhiteSpace(this.valueBuffer[startIndex]))){
                    ++startIndex;
                }
            }

            if (!this.preserveTrailingWhiteSpace){
                while ((endIndex >= firstEligibleTrailingWhiteSpace) && (IsWhiteSpace(this.valueBuffer[endIndex]))){
                    --endIndex;
                }
            }

            return new string(this.valueBuffer, startIndex, endIndex - startIndex + 1);
        }

        /// <summary>
        /// Gets an array of values that have been added to <see cref="valueList"/>.
        /// </summary>
        /// <returns>
        /// An array of type <c>string</c> containing all the values in the value list, or <see langword="null"/> if there are no values in the list.
        /// </returns>
        private string[] GetValues(){
            if (this.valuesListEndIndex > 0){
                var retVal = new string[this.valuesListEndIndex];

                for (int i = 0; i < this.valuesListEndIndex; ++i){
                    retVal[i] = this.valueList[i];
                }

                return retVal;
            }

            return null;

        }

        /// <summary>
        /// Swallows the current character in the data buffer. Assumes that there is a character to swallow, but refills the buffer if necessary.
        /// </summary>
        private void SwallowChar()
        {
            if (this.bufferIndex < BUFFER_SIZE){
                //in this case there are still unread chars in the buffer so just skip one
                ++this.bufferIndex;
            }
            else if (this.bufferIndex < this.bufferEndIndex){
                //in this case we are pointing to the second-to-last char in the buffer, so we need to refill the buffer since the last char is a peeked char
                FillBuffer();
            }
            else{
                //in this case we are pointing to the last char in the buffer, which is a peeked char. therefore, we need to refill and skip past that char
                FillBuffer();
                ++this.bufferIndex;
            }
        }

        /// <summary>
        /// Updates the mask used to quickly filter out non-special characters.
        /// </summary>
        private void UpdateSpecialCharacterMask()
        {
            this.specialCharacterMask = this.valueSeparator | this.valueDelimiter | CR | LF;
        }

        #endregion Methods
    }
}