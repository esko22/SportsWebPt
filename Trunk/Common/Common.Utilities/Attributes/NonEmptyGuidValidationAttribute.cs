using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public class NonEmptyGuidAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if(value == null)
            {
                return true;
            }

            var guid = (Guid) value;
            return guid != Guid.Empty;

        }

        public override string FormatErrorMessage(string name)
        {
            var message = TextResources.InvalidGuid;
            return message.FormatWith(name);
           
        }
    }
}
