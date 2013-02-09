using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public static class DoubleMint
    {
        public static class CurrencyCode
        {
            public const string USA = "USD";
            public const string Canada = "CAD";
            public const string UK = "GBP";
            public const string Australia = "AUD";
            public const string Europe = "EUR";
            public const string NewZealand = "NZD";
            public const string Singapore = "SGD";
            public const string Japan = "JPY";

        }
        /// <summary>
        /// Converts the number to a currency format the currency.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="currencyCode">The currency code.</param>
        /// <returns></returns>
        public static string ToCurrency(this double value, string currencyCode){
            var culture = CurrencyCodeToCulture(currencyCode);
            return value.ToString("C", culture.NumberFormat);
        }

        public static string ToCurrency(this double value, CultureInfo culture)
        {
            return value.ToString("C", culture.NumberFormat);
        }



        /// <summary>
        /// Converts the number to a currency format the currency.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="currencyCode">The currency code.</param>
        /// <returns></returns>
        public static string ToCurrency(this decimal value, string currencyCode)
        {
            var culture = CurrencyCodeToCulture(currencyCode);
            return value.ToString("C", culture.NumberFormat);
        }

        public static string ToCurrency(this decimal value, CultureInfo culture)
        {
            return value.ToString("C", culture.NumberFormat);
        }


        private static CultureInfo CurrencyCodeToCulture(string currencyCode){
            switch (currencyCode.ToUpper().Trim()){
                case CurrencyCode.USA: //USA
                    return CultureInfo.CreateSpecificCulture(CultureCodes.USA);
                   
                case CurrencyCode.Canada:
                    return CultureInfo.CreateSpecificCulture(CultureCodes.CANADA);
                   
                case CurrencyCode.UK:
                    return CultureInfo.CreateSpecificCulture(CultureCodes.UK);
                   
                case CurrencyCode.Australia:
                    return CultureInfo.CreateSpecificCulture(CultureCodes.AUSTRALIA);
                case CurrencyCode.Europe:
                    return CultureInfo.CreateSpecificCulture(CultureCodes.EUROPE);
                case CurrencyCode.NewZealand:
                    return CultureInfo.CreateSpecificCulture(CultureCodes.NEWZEALAND);
                case CurrencyCode.Singapore:
                    return CultureInfo.CreateSpecificCulture(CultureCodes.SINGAPORE);
                case CurrencyCode.Japan:
                    return CultureInfo.CreateSpecificCulture(CultureCodes.JAPAN);
                default:
                    return CultureInfo.CreateSpecificCulture(CultureCodes.USA);
                    
            }
        }


        public static double ConvertCurrencyFromUs(this double input, double rate ){
            return input*rate;
        }
    }
}
