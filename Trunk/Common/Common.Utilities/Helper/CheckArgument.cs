using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;


namespace SportsWebPt.Common.Utilities
{
    public partial class Check
    {
        internal Check()
        {
        }

        public class Argument
        {
            internal Argument()
            {
            }

            [DebuggerStepThrough]
            public static void IsNotNullOrEmpty(string parameter, string argumentName)
            {
                if (string.IsNullOrWhiteSpace(parameter))
                {
                    throw new ArgumentException(TextResources.CannotBeEmpty.FormatWith(argumentName), argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotEmpty(Guid argument, string argumentName)
            {
                if (argument == Guid.Empty)
                {
                    throw new ArgumentException(TextResources.CannotBeEmptyGuid.FormatWith(argumentName), argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotEmpty(string argument, string argumentName)
            {
                if (string.IsNullOrEmpty((argument ?? string.Empty).Trim()))
                {
                    throw new ArgumentException(TextResources.CannotBeEmpty.FormatWith(argumentName), argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotOutOfLength(string argument, int length, string argumentName)
            {
                if (argument.Trim().Length > length)
                {
                    throw new ArgumentException(TextResources.InvalidLength.FormatWith(argumentName, length), argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNull(object argument, string argumentName)
            {
                if (argument == null)
                {
                    throw new ArgumentNullException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNegative(int argument, string argumentName)
            {
                if (argument < 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNegativeOrZero(int argument, string argumentName)
            {
                if (argument <= 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNegative(long argument, string argumentName)
            {
                if (argument < 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNegativeOrZero(long argument, string argumentName)
            {
                if (argument <= 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNegative(float argument, string argumentName)
            {
                if (argument < 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNegativeOrZero(float argument, string argumentName)
            {
                if (argument <= 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNegative(decimal argument, string argumentName)
            {
                if (argument < 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNegativeOrZero(decimal argument, string argumentName)
            {
                if (argument <= 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotInvalidDate(DateTime argument, string argumentName)
            {
                if (!argument.IsValid())
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotInPast(DateTime argument, string argumentName)
            {
                if (argument < DateTime.UtcNow)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotInFuture(DateTime argument, string argumentName)
            {
                if (argument > DateTime.UtcNow)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNegative(TimeSpan argument, string argumentName)
            {
                if (argument < TimeSpan.Zero)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNegativeOrZero(TimeSpan argument, string argumentName)
            {
                if (argument <= TimeSpan.Zero)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotEmpty<T>(ICollection<T> argument, string argumentName)
            {
                IsNotNull(argument, argumentName);

                if (argument.Count == 0)
                {
                    throw new ArgumentException(TextResources.CollectionCannotBeEmpty, argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotOutOfRange(int argument, int min, int max, string argumentName)
            {
                if ((argument < min) || (argument > max))
                {
                    throw new ArgumentOutOfRangeException(argumentName, TextResources.OutOfRange.FormatWith(argumentName, min, max));
                }
            }

            [DebuggerStepThrough]
            public static void IsNotInvalidEmail(string argument, string argumentName)
            {
                IsNotEmpty(argument, argumentName);

                if (!argument.IsEmail())
                {
                    throw new ArgumentException(TextResources.InvalidEmail.FormatWith(argumentName), argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotInvalidWebUrl(string argument, string argumentName)
            {
                IsNotEmpty(argument, argumentName);

                if (!argument.IsWebUrl())
                {
                    throw new ArgumentException(TextResources.InvalidWebUrl.FormatWith(argumentName), argumentName);
                }
            }

            /// <summary>
            /// If check is not true then it will throw.
            /// </summary>
            [DebuggerStepThrough]
            public static void IsNotTrue(bool check, string message){
                if(check){
                    throw new ArgumentException(message);
                }
            }

              [DebuggerStepThrough]
            public static void IsNotEnumMember<TEnum>(TEnum enumValue, string argName)
                where TEnum : struct, IConvertible
            {
                if (Attribute.IsDefined(typeof(TEnum), typeof(FlagsAttribute), false))
                {
                    //flag enumeration - we can only get here if TEnum is a valid enumeration type, since the FlagsAttribute can
                    //only be applied to enumerations
                    bool throwEx;
                    long longValue = enumValue.ToInt64(CultureInfo.InvariantCulture);

                    if (longValue == 0)
                    {
                        //only throw if zero isn't defined in the enum - we have to convert zero to the underlying type of the enum
                        throwEx = !Enum.IsDefined(typeof(TEnum), ((IConvertible)0).ToType(Enum.GetUnderlyingType(typeof(TEnum)), CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
                        {
                            longValue &= ~value.ToInt64(CultureInfo.InvariantCulture);
                        }

                        //throw if there is a value left over after removing all valid values
                        throwEx = (longValue != 0);
                    }

                    if (throwEx)
                    {
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                            TextResources.InvalidEnumForFlags,
                            enumValue, typeof(TEnum).FullName), argName);
                    }
                }
                else
                {
                    //not a flag enumeration
                    if (!Enum.IsDefined(typeof(TEnum), enumValue))
                    {
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                                TextResources.InvalidEnum,
                                enumValue, typeof(TEnum).FullName), argName);
                    }
                }
            }

            [DebuggerStepThrough]
            public static void IsNotEnumMember<TEnum>(TEnum enumValue, string argName, params TEnum[] validValues)
                where TEnum : struct, IConvertible
            {
                IsNotNull(validValues, "validValues");

                if (Attribute.IsDefined(typeof(TEnum), typeof(FlagsAttribute), false))
                {
                    //flag enumeration
                    bool throwEx;
                    long longValue = enumValue.ToInt64(CultureInfo.InvariantCulture);

                    if (longValue == 0)
                    {
                        //only throw if zero isn't permitted by the valid values
                        throwEx = true;

                        foreach (TEnum value in validValues)
                        {
                            if (value.ToInt64(CultureInfo.InvariantCulture) == 0)
                            {
                                throwEx = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (TEnum value in validValues)
                        {
                            longValue &= ~value.ToInt64(CultureInfo.InvariantCulture);
                        }

                        //throw if there is a value left over after removing all valid values
                        throwEx = (longValue != 0);
                    }

                    if (throwEx)
                    {
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                            TextResources.InvalidEnumForFlags,
                            enumValue, typeof(TEnum).FullName), argName);
                    }
                }
                else
                {
                    //not a flag enumeration
                    foreach (TEnum value in validValues)
                    {
                        if (enumValue.Equals(value))
                        {
                            return;
                        }
                    }

                    //at this point we know an exception is required - however, we want to tailor the message based on whether the
                    //specified value is undefined or simply not allowed
                    if (!Enum.IsDefined(typeof(TEnum), enumValue))
                    {
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                                TextResources.InvalidEnum,
                                enumValue, typeof(TEnum).FullName), argName);
                    }
                    else
                    {
                        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                                TextResources.InvalidEnumPermission,
                                enumValue, typeof(TEnum).FullName), argName);
                    }
                }
            }

        }
    }
}
