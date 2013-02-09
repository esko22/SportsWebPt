using System;
using System.ComponentModel;

namespace Illumina.Common.Utilities.ServiceApi
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ApiOperationAttribute : DescriptionAttribute
    {

    }
}
