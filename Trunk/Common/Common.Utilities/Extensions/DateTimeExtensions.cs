using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SportsWebPt.Common.Utilities
{
    public static class DateTimeExtension
    {
        private static readonly DateTime MinDate = new DateTime(1900, 1, 1);
        private static readonly DateTime MaxDate = new DateTime(9999, 12, 31, 23, 59, 59, 999);
        private static string TIMESTAMP_FORMAT = "yyyy-MM-dd\\THH:mm:ss.fff\\Z";

        /// <summary>
        /// Converts to a javascript timestamp.
        /// </summary>
        /// <param name="utcInput">The UTC input.</param>
        /// <param name="offset">The offset.</param>
        /// <returns></returns>
        /// <remarks> A Javascript timestamp is the number of milliseconds since January 1,
        /// 1970 00:00:00 UTC. This is almost the same as Unix timestamps, except it's
        /// in milliseconds, so remember to multiply by 1000!</remarks>
        public static long ToJavascriptTimestamp(this DateTime utcInput, double offset){

            var span = new TimeSpan(DateTime.Parse("1/1/1970").Ticks);
            utcInput = utcInput.AddHours(offset);
            var time = utcInput.Subtract(span);
            return  (time.Ticks/10000);
        }

        /// <summary>
        ///  Converts to a javascript timestamp.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="useServerTime">if set to <c>true</c> [use server time].</param>
        /// <returns></returns>
        /// <remarks> A Javascript timestamp is the number of milliseconds since January 1,
        /// 1970 00:00:00 UTC. This is almost the same as Unix timestamps, except it's
        /// in milliseconds, so remember to multiply by 1000!</remarks>
        public static long ToJavascriptTimestamp(this DateTime input, bool useServerTime)
        {
            var span = new TimeSpan(DateTime.Parse("1/1/1970").Ticks);
            input = useServerTime ? input.ToLocalTime() : input;
            var time = input.Subtract(span);
            return (time.Ticks / 10000);
        }

        public static bool IsSameDay(this DateTime x, DateTime y){
            return x.Year == y.Year && x.Month == y.Month && x.Day == y.Day;
        }

        [DebuggerStepThrough]
        public static bool IsValid(this DateTime target)
        {
            return (target >= MinDate) && (target <= MaxDate);
        }

        /// <summary>
        /// Converts the date to an ISO-8601, resolved to milliseconds.
        /// </summary>
        public static string ToIso8601(this DateTime dateTime){
            return dateTime.ToUniversalTime().ToString(TIMESTAMP_FORMAT,
                              System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts to the server TZ display.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string ToServerTZDisplay(this DateTime dateTime){
            //todo: for now display in server time, but we need to get local time
            return dateTime.ToLocalTime().ToString();
        }

        /// <summary>
        /// Makes the signature used in an api call
        /// </summary>
        /// <param name="publicKey">The public key.</param>
        /// <param name="privateKey">The private key.</param>
        /// <returns></returns>
        public static string MakeSignature(this DateTime timestamp, Guid publicKey, string privateKey)
        {


            string canonicalString = publicKey + timestamp.ToIso8601();
            Encoding ae = new UTF8Encoding();
            var signature = new HMACSHA1(ae.GetBytes(privateKey));
            return Convert.ToBase64String(
                    signature.ComputeHash(ae.GetBytes(
                                                  canonicalString.ToCharArray()))
                    );
        }

        /// <summary>
        /// Gets time in the DateTime format, resolved to milliseconds.
        /// </summary>
        public static DateTime GetCurrentTimeResolvedToMillis()
        {
            DateTime dateTime = DateTime.Now;
           
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day,
                                 dateTime.Hour, dateTime.Minute, dateTime.Second,
                                 dateTime.Millisecond
                                 , DateTimeKind.Local   // COMMENT OUT THIS LINE FOR .NET 1.1
                               );
        }
    }
}
