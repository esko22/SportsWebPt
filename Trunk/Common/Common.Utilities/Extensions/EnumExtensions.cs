using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace SportsWebPt.Common.Utilities
{
    public static class EnumExtensions
    {
    
        /// <summary>
        /// Will get the string value for a given enums value, this will
        /// only work if you assign the StringValue attribute to
        /// the items in your enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStringValue(this Enum value){
            var type = value.GetType();
            var fieldInfo = type.GetField(value.ToString());

            var attributes =
                    fieldInfo.GetCustomAttributes(typeof (StringValueAttribute), false) as StringValueAttribute[];

            if ((attributes != null) && (attributes.Length > 0)){
                return attributes[0].StringValue;
            }

            try{
                return Enum.GetName(value.GetType(), value);
            }
            catch{}


            return null;
        }

        public static bool HasFlags(this Enum value){
            return value.GetType().GetCustomAttributes(typeof(FlagsAttribute),false).Length > 0;
        }

      

    }
}
