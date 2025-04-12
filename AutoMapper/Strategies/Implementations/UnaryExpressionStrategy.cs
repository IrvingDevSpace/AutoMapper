using AutoMapper.Strategies.Abstract;
using System;
using System.Linq.Expressions;

namespace AutoMapper.Strategies.Implementations
{
    internal class UnaryExpressionStrategy : ExpressionStrategyBase
    {
        public override void SetMemberArg(LambdaExpression expression, MemberArg memberArg)
        {
            var memberInfo = FindMemberInfo(expression.Body);

            Delegate dele = expression.Compile();

            memberArg.MemberInfo = memberInfo;

            memberArg.ValueDelegate = dele;
        }

        public override (object SrcValue, Type SrcPropertyType) GetMapValueAndType(MemberArg memberArg, object source)
        {
            var srcValue = memberArg.ValueDelegate.DynamicInvoke(source);
            var srcPropertyType = memberArg.Type;

            return (srcValue, srcPropertyType);
        }
    }
}
