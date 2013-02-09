using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace SportsWebPt.Common.Utilities
{
    /// <summary>
    /// Represents a column in a collection that can be used to dynamically display
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ColumnData<T> where T: class
    {
        private static readonly Regex NameExpression;
       
        static ColumnData()
        {
            NameExpression = new Regex("([A-Z]+(?=$|[A-Z][a-z])|[A-Z]?[a-z]+)", RegexOptions.Compiled);
        }

        public ColumnData(Action<T> template):this(){
            Check.Argument.IsNotNull(template, "template");
            this.Template = template;
        }
        public ColumnData(Expression<Func<T, object>> value): this(){
            Check.Argument.IsNotNull(value, "value");
            this.Name = value.MemberWithoutInstance();
            this.DataType =TypeFromMemberExpression(value.ToMemberExpression());
            this.Title = !string.IsNullOrEmpty(this.Name) ? NameExpression.Replace(value.LastMemberName(), " $1").Trim() : null;
            this.Value = value.Compile();

        }
        private ColumnData(){
            this.Visible = true;
        }

        public string Title { get; set; }
        public string Name { get; set; }
        public Func<T, object> Value { get; set; }
        public string Format { get; set; }
        public Type DataType { get; set; }
        public Action<T> Template { get; set; }
        public bool Visible { get; set; }
       
        private static Type TypeFromMemberExpression(MemberExpression memberExpression)
        {
            if (memberExpression == null)
            {
                return null;
            }
            MemberInfo member = memberExpression.Member;
            if (member.MemberType == MemberTypes.Property)
            {
                return ((PropertyInfo)member).PropertyType;
            }
            if (member.MemberType != MemberTypes.Field)
            {
                throw new NotSupportedException();
            }
            return ((FieldInfo)member).FieldType;
        }

 

    }
}
