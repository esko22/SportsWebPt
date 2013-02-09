using System;

namespace SportsWebPt.Common.Utilities
{
    public class Lazy<T>
    {

        #region Fields
        
        private T _value;
        private Func<T> _loader; 
        
        #endregion

        #region Properties
        
        public T Value
        {
            get
            {
                if (_loader != null)
                {
                    _value = _loader();
                    _loader = null;
                }

                return _value;
            }
        } 

        #endregion

        #region Construction
        
        public Lazy(T value)
        {
            _value = value;
        }

        public Lazy(Func<T> loader)
        {
            _loader = loader;
        } 

        #endregion

        #region Methods
        
        public static implicit operator T(Lazy<T> lazy)
        {
            return lazy.Value;
        }

        public static implicit operator Lazy<T>(T value)
        {
            return new Lazy<T>(value);
        } 

        #endregion

    }
}
