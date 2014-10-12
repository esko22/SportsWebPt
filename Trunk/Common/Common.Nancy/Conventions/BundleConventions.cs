using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Nancy;
using Nancy.Helpers;
using Nancy.Responses;

namespace SportsWebPt.Common.Nancy
{
    public static class StaticContentBundle
    {
        private static readonly ConcurrentDictionary<int, AssetBundle> BundleCache = new ConcurrentDictionary<int, AssetBundle>();
        
        public static Response ResponseFactory(IEnumerable<string> files, string contentType, NancyContext context, string applicationRootPath)
        {
            var paths = files.Select(file => Path.Combine(applicationRootPath, file));
            var hash = BuildConsolidatedBundle(paths);
            var bundle = BundleCache[hash];

            return (CacheHelpers.ReturnNotModified(bundle.ETag, bundle.LastUpdate, context))
                ? ResponseNotModified()
                : ResponseFromBundle(bundle, contentType);
        }

        private static int BuildConsolidatedBundle(IEnumerable<string> paths)
        {
            var hash = BundleHash(paths);

            if (BundleCache.ContainsKey(hash) == false)
            {
                var assetBundle = new AssetBundle
                {
                    ETag = Convert.ToString(hash),
                    LastUpdate = paths.Max(p => new FileInfo(p).LastAccessTimeUtc),
                    Bytes = Encoding.UTF8.GetBytes(string.Join(Environment.NewLine, paths.Select(File.ReadAllText)))
                };
                BundleCache.TryAdd(hash, assetBundle);
            }

            return hash;
        }

        private static int BundleHash(IEnumerable<string> files)
        {
            return files
                .Select(f => new FileInfo(f).LastWriteTimeUtc.GetHashCode() ^ f.GetHashCode())
                .Aggregate((h1, h2) => h1 ^ h2);
        }

        private static Response ResponseNotModified()
        {
            return new Response
            {
                StatusCode = HttpStatusCode.NotModified,
                ContentType = null,
                Contents = Response.NoBody
            };
        }

        private static Response ResponseFromBundle(AssetBundle assetBundle, string contentType)
        {
            var stream = new MemoryStream(assetBundle.Bytes);
            var response = new StreamResponse(() => stream, contentType);
            response.Headers["ETag"] = assetBundle.ETag;
            response.Headers["Last-Modified"] = assetBundle.LastUpdate.ToString("R");
            return response;
        }

        private class AssetBundle
        {
            public DateTime LastUpdate { get; set; }
            public string ETag { get; set; }
            public byte[] Bytes { get; set; }
        }
    }



    public static class StaticContentBundleConventionBuilder
    {
        public static Func<NancyContext, string, Response> AddBundle(string requestedFile, string contentType, IEnumerable<string> files)
        {
            if (requestedFile.StartsWith("/") == false)
                requestedFile = string.Concat("/", requestedFile);

            return (context, applicationRootPath) =>
            {
                var path = context.Request.Path;
                if (path.Equals(requestedFile, StringComparison.OrdinalIgnoreCase) == false)
                {
                    context.Trace.TraceLog.WriteLog(x => x.AppendLine(
                        string.Concat(
                            "[BundleStaticContentConventionBuilder] The requested resource '",
                            path, "' does not match convention mapped to '", requestedFile, "'")));
                    return null;
                }

                return StaticContentBundle.ResponseFactory(files, contentType, context,applicationRootPath);
            };
        }
    }



    public static class StaticContentBundleConventionsExtensions
    {
        public static void AddStylesBundle(this IList<Func<NancyContext, string, Response>> conventions, string requestedPath, IEnumerable<string> files)
        {
            conventions.AddBundle(requestedPath, "text/css", files);
        }

        public static void AddScriptsBundle(this IList<Func<NancyContext, string, Response>> conventions, string requestedPath, IEnumerable<string> files)
        {
            conventions.AddBundle(requestedPath, "application/x-javascript", files);
        }

        public static void AddBundle(this IList<Func<NancyContext, string, Response>> conventions,string requestedPath, string contentType, IEnumerable<string> files)
        {
            conventions.Add(StaticContentBundleConventionBuilder.AddBundle(requestedPath, contentType, files));
        }
    }
}