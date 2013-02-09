﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    [Flags]
    public enum CsvWriterOptions
    {
        /// <summary>
        /// No options defined.
        /// </summary>
        None = 0,
        /// <summary>
        /// Specifies that the CSV writer should always delimit values.
        /// </summary>
        /// <remarks>
        /// By default, the CSV writer only delimits those values that require delimiting. This option can be specified to ensure that all values
        /// are delimited, regardless of whether delimiting is required.
        /// </remarks>
        AlwaysDelimit = 1,
        /// <summary>
        /// Specifies that the CSV writer should always break lines with a CR/LF combination.
        /// </summary>
        /// <remarks>
        /// <para>
        /// By default, the CSV writer breaks lines with <see cref="Environment.NewLine"/>, which may differ depending on the hosting platform. This
        /// option forces the CSV writer to break lines with a carriage return and line-feed combination.
        /// </para>
        /// <para>
        /// Only one of <see cref="CrlfLineBreaks"/>, <see cref="LfLineBreaks"/> or <see cref="CrLineBreaks"/> may be specified. Specifying more than one will yield an error.
        /// </para>
        /// </remarks>
        CrlfLineBreaks = 2,
        /// <summary>
        /// Specifies that the CSV writer should always break lines with an LF character.
        /// </summary>
        /// <remarks>
        /// <para>
        /// By default, the CSV writer breaks lines with <see cref="Environment.NewLine"/>, which may differ depending on the hosting platform. This
        /// option forces the CSV writer to break lines with a line-feed character.
        /// </para>
        /// <para>
        /// Only one of <see cref="CrlfLineBreaks"/>, <see cref="LfLineBreaks"/> or <see cref="CrLineBreaks"/> may be specified. Specifying more than one will yield an error.
        /// </para>
        /// </remarks>
        LfLineBreaks = 4,
        /// <summary>
        /// Specifies that the CSV writer should always break lines with a CR character.
        /// </summary>
        /// <remarks>
        /// <para>
        /// By default, the CSV writer breaks lines with <see cref="Environment.NewLine"/>, which may differ depending on the hosting platform. This
        /// option forces the CSV writer to break lines with a carriage return character.
        /// </para>
        /// <para>
        /// Only one of <see cref="CrlfLineBreaks"/>, <see cref="LfLineBreaks"/> or <see cref="CrLineBreaks"/> may be specified. Specifying more than one will yield an error.
        /// </para>
        /// </remarks>
        CrLineBreaks = 8
    }
}
