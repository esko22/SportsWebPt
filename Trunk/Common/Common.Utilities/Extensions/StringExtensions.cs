using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Xsl;


namespace SportsWebPt.Common.Utilities
{
    public static class StringExtensions
    {
     
        public static string IcomMarketingConversion(this string value){
            return CommonRegEx.FindICom.Replace(value, "iCom");
        }

        public static string AsNullIfEmpty(this string items)
        {
            if (String.IsNullOrEmpty(items))
            {
                return null;
            }
            return items;
        }

        public static string AsNullIfWhiteSpace(this string items)
        {
            if (String.IsNullOrWhiteSpace(items))
            {
                return null;
            }
            return items;
        }

        /// <summary>
        /// Finds a percentage of similarity between 2 strings. 1 == 100% 0 == none
        /// </summary>
        /// <param name="string1">The string1.</param>
        /// <param name="string2">The string2.</param>
        /// <returns></returns>
        public static double CompareSimilarity(this string string1, string string2){
            var similarity = new StringSimilarity();
            return similarity.CompareStrings(string1, string2);
        }

        /// <summary>
        /// Splits the word by capital letters.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="replace">The replacement string</param>
        /// <returns></returns>
        public static string SplitWordByCapitalLetters(this string value, string replace){
            return CommonRegEx.CapitalLettersExpression.Replace(value, replace + "$0");
        }

         public static Uri UrlCombineToUri(this string baseUrl, params string[] paths){
             var baseUri = new UriBuilder(baseUrl);
             Uri newUri = baseUri.Uri;

             if (paths.IsNotNullOrEmpty())
             {
                 foreach (var s in paths)
                 {
                     if(!newUri.ToString().EndsWith("/")){
                         newUri = new Uri(newUri + "/");
                     }

                     if (!Uri.TryCreate(newUri, s, out newUri)){
                         throw new ArgumentException("Unable to combine specified url values");
                     }
                 }
             }
            
             return newUri;
         }

        public static string UrlCombine(this string baseUrl, params string[] paths){
            return UrlCombineToUri(baseUrl, paths).ToString();
        }

        public static string CleanSearchText(this string value){
            string validCharacters = @"[^\d\w\s_</>.,?!@&=+()$;#%*-]";
            value = Regex.Replace(value, validCharacters, "", RegexOptions.IgnoreCase);
            return value;
        }

        public static string FirstNWords(this string input, int howManyToFind){
            
            string findWordEx = @"([\w]+\s+){" + howManyToFind + "}";
            return Regex.Match(input, findWordEx).Value;

        }

        public static int OccurencesOf(this string source, string term)
        {
            return source.Length - source.Replace(term, "").Length;
        }

        public static List<OrderByFilter> ParseOrderBy(this string orderBy, char delimeter, bool defaultIsDescending){
            //create the order by collection
            if (string.IsNullOrEmpty(orderBy)){
                return null;
            }
            var orderByFilters = new List<OrderByFilter>();
            var clauses = orderBy.Split(new[]{delimeter}, StringSplitOptions.RemoveEmptyEntries);
            int count = clauses.Length;
            for(int i = 0; i < count; i++){
                string phrase = clauses[i];
                string[] words = phrase.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
                if(words.IsNotNullOrEmpty()){
                    var x = new OrderByFilter();
                    x.Order = i;
                    x.OrderBy = words[0];
                    if(words.Length > 1){
                        var test = words[1];
                        if(CommonRegEx.IsDescendingExpression.IsMatch(test)){
                            x.IsDescending = true;
                        }
                        else if(CommonRegEx.IsAscendingExpression.IsMatch(test)){
                            x.IsDescending = false;
                        }
                        else{
                            x.IsDescending = defaultIsDescending;
                        }
                    }
                    else{
                        x.IsDescending = defaultIsDescending;
                    }

                    orderByFilters.Add(x);

                }

            }
            return orderByFilters;

        }

       

        /// <summary>
        /// Parses the domain from a URL string or returns the string if no URL was found
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string AsDomain(this string url)
        {
            if (string.IsNullOrEmpty(url))
                return url;

            var match = Regex.Match(url, @"^http[s]?[:/]+[^/]+");
            if (match.Success)
                return match.Captures[0].Value;
            else
                return url;
        }

        /// <summary>
        /// Parses the domain from a URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string AsDomain(this Uri url)
        {
            if (url == null)
                return null;

            return url.ToString().AsDomain();
        }


        /// <summary>
        /// Looks up the sid. and also outputs the actual login name
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="subDomainNames">The possible list sub domain names.</param>
        /// <param name="actualLoginName">Actual name of the login.</param>
        /// <returns></returns>
        /// <example>groberts</example>
        /// <exception cref="IdentityNotMappedException">Throws Identity Not Mapped Exception if can't be found</exception>
        public static SecurityIdentifier LookupSid(this string userName, IList<string> subDomainNames, out string actualLoginName)
        {
            string rawName = userName;
            actualLoginName = "";
            SecurityIdentifier sid = null;
            if (userName.Contains("\\")){
                 rawName = userName.Substring(userName.IndexOf("\\") + 1);
            }
            if(userName.Contains("@")){
                rawName = userName.Substring(0, userName.IndexOf("@"));
            }
            //first try to look it up
            int count = subDomainNames.Count;
            
            for (int i = 0; i < count; i++){
                var sub = subDomainNames[i];
                actualLoginName = string.Concat(sub, "\\", rawName);
                try{
                    var account = new NTAccount(actualLoginName);
                    sid = (SecurityIdentifier) account.Translate(typeof (SecurityIdentifier));
                    break;
                }
                catch (IdentityNotMappedException ex){
                    if(count == (i + 1)){
                        throw;
                    }
                }
            }
            return sid;
        }

        /// <summary>
        /// Converts string to the UTF8 byte array.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public static byte[] ToUtf8ByteArray(this string xml)
        {
            if (string.IsNullOrEmpty(xml))
                return null;

            var encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(xml);
            return byteArray;
        }


        /// <summary>
        /// Converts to the ASCII byte array.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte[] ToAsciiByteArray(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return Encoding.ASCII.GetBytes(value);
        }


        /// <summary>
        /// Converts to generic from XML.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public static TResult ToGenericFromXml<TResult>(this string xml) where TResult : class, new()
        {
            TResult result = default(TResult);
            return result.FromXml(xml);
        }


        /// <summary>
        /// Reverse the string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Reverse(this string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return chars.ToString();
        }


        /// <summary>
        /// Reduce string to shorter preview which is optionally ended by some string (...).
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="count">Length of returned string including endings.</param>
        /// <param name="endings">optional edings of reduced text</param>
        /// <returns></returns>
        /// <example>
        /// string description = "This is very long description of something";
        /// string preview = description.Reduce(20,"...");
        /// produce -&gt; "This is very long..."
        /// </example>
        /// <exception cref="T:System.InvalidOperationException">if count is less than the endings</exception>
        public static string Reduce(this string input, int count, string endings)
        {
            if (count < endings.Length)
                throw new InvalidOperationException("Failed to reduce to less then endings length.");
            int inputLen = input.Length;
            int len = inputLen;
            if (!string.IsNullOrEmpty(endings))
                len += endings.Length;
            if (count > inputLen)
                return input; //it's too short to reduce
            input = input.Substring(0, inputLen - len + count);
            if (!string.IsNullOrEmpty(endings))
                input += endings;
            return input;
        }


        /// <summary>
        /// Reduce string to shorter preview with the ... used at the end
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="count">Length of returned string including endings.</param>
        /// <returns></returns>
        /// <example>
        /// string description = "This is very long description of something";
        /// string preview = description.Reduce(20,"...");
        /// produce -&gt; "This is very long..."
        /// </example>
        /// <exception cref="T:System.InvalidOperationException">if count is less than the endings</exception>
        public static string Reduce(this string input, int count)
        {
            return Reduce(input, count, "...");
        }


        /// <summary>
        /// Replace \r\n or \n by <br />
        /// from http://weblogs.asp.net/gunnarpeipman/archive/2007/11/18/c-extension-methods.aspx
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Nl2Br(this string s)
        {
            return s.Replace("\r\n", "<br />").Replace("\n", "<br />");
        }


        static MD5CryptoServiceProvider md5Provider = null;
        /// <summary>
        /// from http://weblogs.asp.net/gunnarpeipman/archive/2007/11/18/c-extension-methods.aspx
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Md5(this string s)
        {
            if (md5Provider == null) //creating only when needed
                md5Provider = new MD5CryptoServiceProvider();
            Byte[] newdata = Encoding.Default.GetBytes(s);
            Byte[] encrypted = md5Provider.ComputeHash(newdata);
            return BitConverter.ToString(encrypted).Replace("-", "").ToLower();
        }


        /// <summary>
        /// This function takes a string an breaks it out into a list
        /// of search terms.  Phrases which are enclosed in quotes are preserved
        /// while other string are broken out by spaces.
        /// </summary>
        /// <param name="input">the search term to parse</param>
        /// <returns>a list of parsed search terms</returns>
        public static List<string> ExtractSearchTerms(this string input)
        {
            if (String.IsNullOrEmpty(input))
                return null;
            string keywords = @"(\band\b|\bor\b)";
            input = Regex.Replace(input, keywords, "", RegexOptions.IgnoreCase);
            
            var exitCode = new List<string>();

            var quoted = Regex.Matches(input, @"[""](.*?)[""]");

            if (quoted.Count > 0)
            {
                for (var x = 0; x < quoted.Count; x++)
                {
                    exitCode.Add(quoted[x].Captures[0].Value.Replace(@"""", ""));
                }
            }

            input = Regex.Replace(input, @"[""](.*?)[""]", "").Trim();

            var singles = input.Split(new[] { ' ', ',' });

            for (var x = 0; x < singles.Length; x++)
            {
                if (!String.IsNullOrEmpty(singles[x]))
                {
                    exitCode.Add(singles[x]);
                }
            }

            return exitCode;
        }

        /// <summary>
        /// Checks if the string is a valid URL
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static bool IsWebUrl(this string target)
        {
            return !string.IsNullOrEmpty(target) && CommonRegEx.WebUrlExpression.IsMatch(target);
        }
        /// <summary>
        /// Checks the string if target is semantically an email
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static bool IsEmail(this string target)
        {
            return !string.IsNullOrEmpty(target) && CommonRegEx.EmailExpression.IsMatch(target);
        }

        [DebuggerStepThrough]
        public static string NullSafe(this string target)
        {
            return (target ?? string.Empty).Trim();
        }

        [DebuggerStepThrough]
        public static string FormatWith(this string target, params object[] args)
        {
            Check.Argument.IsNotEmpty(target, "target");

            return string.Format(CultureInfo.CurrentCulture, target, args);
        }

        [DebuggerStepThrough]
        public static string StripHtml(this string target)
        {
            return CommonRegEx.StripHtmlLExpression.Replace(target, string.Empty);
        }

        [DebuggerStepThrough]
        public static Guid ToGuid(this string target)
        {
            Guid result = Guid.Empty;

            try{
                result = new Guid(target);
            }
            catch (Exception){
                
            }
            
            
            if (result.IsEmpty() || (!string.IsNullOrEmpty(target)) && (target.Trim().Length == 22))
            {
                string encoded = string.Concat(target.Trim().Replace("-", "+").Replace("_", "/"), "==");

                try
                {
                    byte[] base64 = Convert.FromBase64String(encoded);

                    result = new Guid(base64);
                }
                catch (Exception)
                {
                }
            }

            return result;
        }

        [DebuggerStepThrough]
        public static T ToEnum<T>(this string target, T defaultValue) where T : IComparable, IFormattable
        {
            T convertedValue = defaultValue;

            if (!string.IsNullOrEmpty(target))
            {
                try
                {
                    convertedValue = (T)Enum.Parse(typeof(T), target.Trim(), true);
                }
                catch (ArgumentException)
                {
                }
            }

            return convertedValue;
        }

       

        [DebuggerStepThrough]
        public static string ToLegalUrl(this string target)
        {
            if (string.IsNullOrEmpty(target))
            {
                return target;
            }

            target = target.Trim();

            if (target.IndexOfAny(CommonRegEx.IllegalUrlCharacters) > -1)
            {
                foreach (char character in CommonRegEx.IllegalUrlCharacters)
                {
                    target = target.Replace(character.ToString(CultureInfo.CurrentCulture), string.Empty);
                }
            }

            target = target.Replace(" ", "-");

            while (target.Contains("--"))
            {
                target = target.Replace("--", "-");
            }

            return target;
        }


      

        /// <summary>
        /// Converts the type of the file extension to a response.contenttype.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static string ToContentTypeFromFileExtension(this string target)
        {
            // Gets File Extension
            switch (target.Trim().ToLower().Replace(".", ""))
            {
                case "htm":
                case "html":
                case "log":
                    return "text/HTML";

                case "txt":
                    return "text/plain";


                case "doc":
                    return "application/ms-word";

                case "tiff":
                case "tif":
                    return "image/tiff";

                case "asf":
                    return "video/x-ms-asf";

                case "avi":
                    return "video/avi";

                case "zip":
                    return "application/zip";

                case "xls":
                case "csv":
                    return "application/vnd.ms-excel";

                case "gif":
                    return "image/gif";

                case "jpg":
                case "jpeg":
                    return "image/jpeg";

                case "bmp":
                    return "image/bmp";

                case "wav":
                    return "audio/wav";

                case "mp3":
                    return "audio/mpeg3";

                case "mpg":
                case "mpeg":
                    return "video/mpeg";

                case "rtf":
                    return "application/rtf";

                case "asp":
                    return "text/asp";

                case "pdf":
                    return "application/pdf";

                case "fdf":
                    return "application/vnd.fdf";

                case "ppt":
                    return "application/mspowerpoint";

                case "dwg":
                    return "image/vnd.dwg";

                case "msg":
                    return "application/msoutlook";

                case "xml":
                case "sdxl":
                    return "application/xml";

                case "xdp":
                    return "application/vnd.adobe.xdp+xml";

                default:
                    return "application/octet-stream";
            }



        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public static int ToSafeInteger(this string input, int defaultValue){
            try{
                return Convert.ToInt32(input);
            } catch{
                return defaultValue;
            }
        }
        public static int ToSafeInteger(this string input){
            return input.ToSafeInteger(0);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ToSafeDouble(this string input, double defaultValue){
            try{
                return Convert.ToDouble(input);
            }
            catch{
                return defaultValue;
            }
        }

        public static double ToSafeDouble(this string input){
            return input.ToSafeDouble(0.0);
        }



        public static string GetSafeISelectNamePart(this string input){
            var exitCode = String.Empty;

            exitCode = Regex.Replace(input, @"[^\d\w_-]", "");

            if (exitCode.Length > 10)
                exitCode = exitCode.Substring(0, 10);

            return exitCode;
        }

        /// <summary>
        /// Transforms the selected input with xsl
        /// </summary>
        /// <param name="xmlInput"></param>
        /// <param name="xslTranfsorm"></param>
        /// <exception cref="XmlException"></exception>
        /// <exception cref="XsltException"></exception>
        /// <returns></returns>
        public static string TransformWith(this string xmlInput, string xslTranfsorm)
        {
            var transformer = new XslTransformer();
            return transformer.Transform(xslTranfsorm, xmlInput);
        }

        public static String ToCsvString(this String[] strings)
        {
            var builder = new StringBuilder();
            foreach (var item in strings)
            {
                builder.Append(item);
                builder.Append(", ");
            }

            return builder.ToString().Remove(builder.Length - 2, 2);
        }
    }
}
