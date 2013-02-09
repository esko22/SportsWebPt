using System;

namespace SportsWebPt.Common.Caching
{
    public interface ILastAccessAwareCacheMemeber : IDisposable
    {

        Boolean IsSticky { get;}

        DateTime LastAccessed { get; set; }

    }
}
