﻿using System.ComponentModel;
using System.Runtime.Serialization;

namespace SportsWebPt.Common.ServiceStack.Infrastructure
{
    [DataContract]
    public abstract class ApiResourceRequest<TTypeOfResource>
    {
        [DataMember]
        [Description("The Id of the resource")]
        public string Id { get; set; }

        [DataMember]
        [Description("The resource to be updated / created ")]
        public TTypeOfResource Resource { get; set; }

        public long? IdAsLong
        {
            get
            {
                if (Id == null)
                {
                    return null;
                }
                long ret;
                return long.TryParse(Id, out ret) ? ret : (long?)null;
            }
        }

        public int IdAsInt
        {
            get
            {
                if (Id == null)
                {
                    return 0;
                }
                int ret;
                return int.TryParse(Id, out ret) ? ret : 0;
            }
        }

        public bool IsIdCurrent
        {
            get
            {
                return !string.IsNullOrEmpty(Id) && Id.ToLower() == "current";
            }
        }

    }
}
