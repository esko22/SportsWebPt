using System;
using System.Diagnostics;

namespace SportsWebPt.Common.Utilities
{
    public class SubscriptionToken : IEquatable<SubscriptionToken>
    {
        private readonly Guid _token = Guid.NewGuid();

        [DebuggerStepThrough]
        public bool Equals(SubscriptionToken other)
        {
            return (other != null) && Equals(_token, other._token);
        }

        [DebuggerStepThrough]
        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || Equals(obj as SubscriptionToken);
        }

        [DebuggerStepThrough]
        public override int GetHashCode()
        {
            return _token.GetHashCode();
        }

        [DebuggerStepThrough]
        public override string ToString()
        {
            return _token.ToString();
        }
    }
}