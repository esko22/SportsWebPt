using System;
using System.ComponentModel;

namespace Illumina.Common.ServiceStack.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ApiOperationAttribute : DescriptionAttribute
    {

    }
}
