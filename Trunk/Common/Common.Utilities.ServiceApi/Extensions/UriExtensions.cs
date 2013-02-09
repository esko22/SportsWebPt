using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using SportsWebPt.Common.Utilities;

namespace SportsWebPt.Common.Utilities.ServiceApi
{
    public static class UriExtensions
    {
        public static Uri WithQuery(this Uri uri, string key, string value)
        {
            if (uri == null)
            {
                return null;
            }
            var qnvc = HttpUtility.ParseQueryString(uri.Query);
            qnvc.Set(key, value);

            var querySb = new StringBuilder();
            bool first = true;
            foreach (string k in qnvc.AllKeys)
            {
                string v = qnvc.Get(k);
                querySb.AppendFormat("{0}{1}={2}", first ? string.Empty : "&", k, v);
                first = false;
            }
            var ub = new UriBuilder(uri);
            ub.Query = querySb.ToString();
            return ub.Uri;
        }

        public static Uri At(this Uri uri, params string[] segments)
        {
            if (segments == null)
            {
                return null;
            }

            Uri ret;
            if (uri.IsAbsoluteUri)
            {
                var newSegments = uri.Segments.Select(x => x.TrimStart('/').TrimEnd('/')).Where(x => !x.IsNullOrEmpty()).ToList();
                newSegments.AddRange(segments.Select(x => x.TrimStart('/').TrimEnd('/')).Where(x => !x.IsNullOrEmpty()));
                string newPath = string.Join("/", newSegments);
                string query = uri.Query ?? string.Empty;
                string newUri = uri.Scheme + "://" + uri.Host;
                if (!uri.IsDefaultPort)
                    newUri += ":" + uri.Port;
                newUri += "/";

                ret = new Uri(newUri + string.Join("/", newSegments) + query);
            }
            else
            {
                var newSegments = new List<string>(uri.OriginalString.Split('/'));
                newSegments.AddRange(segments);
                string path = string.Join("/", newSegments).TrimStart('/');
                ret = new Uri(path, UriKind.Relative);
            }
            return ret;
        }
    }
}
