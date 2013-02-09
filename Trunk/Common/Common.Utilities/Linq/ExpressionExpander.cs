using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;

namespace SportsWebPt.Common.Utilities
{
    /// <summary>
    /// Custom expresssion visitor for ExpandableQuery. This expands calls to Expression.Compile() and
    /// collapses captured lambda references in subqueries which LINQ to SQL can't otherwise handle.
    /// </summary>
    internal class ExpressionExpander : ExpressionVisitor
    {
        // Replacement parameters - for when invoking a lambda expression.
        private readonly Dictionary<ParameterExpression, Expression> replacementParams;

        internal ExpressionExpander() {}

        private ExpressionExpander(Dictionary<ParameterExpression, Expression> replacementParams){
            this.replacementParams = replacementParams;
        }

        protected override Expression VisitParameter(ParameterExpression p){
            if ((this.replacementParams != null) && (this.replacementParams.ContainsKey(p))){
                return this.replacementParams[p];
            }

            return base.VisitParameter(p);
        }

        /// <summary>
        /// Flatten calls to Invoke so that Entity Framework can understand it. Calls to Invoke are generated
        /// by PredicateBuilder.
        /// </summary>
        protected override Expression VisitInvocation(InvocationExpression iv){
            Expression target = iv.Expression;
            if (target is MemberExpression){
                target = TransformExpr((MemberExpression) target);
            }
            if (target is ConstantExpression){
                target = ((ConstantExpression) target).Value as Expression;
            }

            var lambda = (LambdaExpression) target;
            if (lambda == null){
                throw new InvalidOperationException("Expression target cannot be null");
            }

            Dictionary<ParameterExpression, Expression> tempVars;
            if (this.replacementParams == null){
                tempVars = new Dictionary<ParameterExpression, Expression>();
            }
            else{
                tempVars = new Dictionary<ParameterExpression, Expression>(this.replacementParams);
            }

            try{
                for (int i = 0; i < lambda.Parameters.Count; i++)
                    tempVars.Add(lambda.Parameters[i], iv.Arguments[i]);
            }
            catch (ArgumentException ex){
                throw new InvalidOperationException(
                        "Invoke cannot be called recursively - try using a temporary variable.", ex);
            }

            return new ExpressionExpander(tempVars).Visit(lambda.Body);
        }

        protected override Expression VisitMethodCall(MethodCallExpression m){
            if (m.Method.Name == "Invoke" && m.Method.DeclaringType == typeof (Extensions)){
                Expression target = m.Arguments[0];
                if (target is MemberExpression){
                    target = TransformExpr((MemberExpression) target);
                }
                if (target is ConstantExpression){
                    target = ((ConstantExpression) target).Value as Expression;
                }

                var lambda = (LambdaExpression) target;
                if (lambda == null){
                    throw new InvalidOperationException("Expression target cannot be null");
                }

                Dictionary<ParameterExpression, Expression> tempVars;
                if (this.replacementParams == null){
                    tempVars = new Dictionary<ParameterExpression, Expression>();
                }
                else{
                    tempVars = new Dictionary<ParameterExpression, Expression>(this.replacementParams);
                }

                try{
                    for (int i = 0; i < lambda.Parameters.Count; i++)
                        tempVars.Add(lambda.Parameters[i], m.Arguments[i + 1]);
                }
                catch (ArgumentException ex){
                    throw new InvalidOperationException(
                            "Invoke cannot be called recursively - try using a temporary variable.", ex);
                }

                return new ExpressionExpander(tempVars).Visit(lambda.Body);
            }

            // Expand calls to an expression's Compile() method:
            if (m.Method.Name == "Compile" && m.Object is MemberExpression){
                var me = (MemberExpression) m.Object;
                Expression newExpr = TransformExpr(me);
                if (newExpr != me){
                    return newExpr;
                }
            }

            // Strip out any nested calls to AsExpandable():
            if (m.Method.Name == "AsExpandable" && m.Method.DeclaringType == typeof (Extensions)){
                return m.Arguments[0];
            }

            return base.VisitMethodCall(m);
        }

        protected override Expression VisitMemberAccess(MemberExpression memberExpression){
            // Strip out any references to expressions captured by outer variables - LINQ to SQL can't handle these:
            if (memberExpression.Member.DeclaringType.Name.StartsWith("<>")){
                return TransformExpr(memberExpression);
            }

            return base.VisitMemberAccess(memberExpression);
        }

        private Expression TransformExpr(MemberExpression input){
            // Collapse captured outer variables
            if (input == null
                || !(input.Member is FieldInfo)
                || !input.Member.ReflectedType.IsNestedPrivate
                || !input.Member.ReflectedType.Name.StartsWith("<>")) // captured outer variable
            {
                return input;
            }

            if (input.Expression is ConstantExpression){
                object obj = ((ConstantExpression) input.Expression).Value;
                if (obj == null){
                    return input;
                }
                Type t = obj.GetType();
                if (!t.IsNestedPrivate || !t.Name.StartsWith("<>")){
                    return input;
                }
                var fi = (FieldInfo) input.Member;
                object result = fi.GetValue(obj);
                if (result is Expression){
                    return Visit((Expression) result);
                }
            }
            return input;
        }
    }
}