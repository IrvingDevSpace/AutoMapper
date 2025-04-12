using AutoMapper.Strategies.Abstract;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoMapper.Strategies.Implementations
{
    internal class MemberExpressionStrategy : ExpressionStrategyBase
    {
        public override void SetMemberArg(LambdaExpression expression, MemberArg memberArg)
        {
            var memberExpr = expression.Body as MemberExpression;
            // Member src 只能為欄位或屬性

            if (memberExpr.Member is PropertyInfo propInfo)
                memberArg.MemberInfo = propInfo;
            else if (memberExpr.Member is FieldInfo fieldInfo)
                memberArg.MemberInfo = fieldInfo;
            else
                throw new InvalidOperationException("只能為欄位或屬性");
        }

        public override (object SrcValue, Type SrcPropertyType) GetMapValueAndType(MemberArg memberArg, object source)
        {
            object srcValue = null;
            Type srcPropertyType = null;

            if (memberArg.MemberInfo is PropertyInfo srcPropInfo)
            {
                srcValue = srcPropInfo.GetValue(source);
                srcPropertyType = srcPropInfo.PropertyType;
            }
            else if (memberArg.MemberInfo is FieldInfo srcFieldInfo)
            {
                srcValue = srcFieldInfo.GetValue(source);
                srcPropertyType = srcFieldInfo.FieldType;
            }

            return (srcValue, srcPropertyType);
        }
    }
}
