using AutoMapper.Strategies.Abstract;
using System;
using System.Linq.Expressions;

namespace AutoMapper.Strategies.Implementations
{
    internal class ConstantExpressionStrategy : ExpressionStrategyBase
    {
        public override void SetMemberArg(LambdaExpression expression, MemberArg memberArg)
        {
            var constExpr = expression.Body as ConstantExpression;

            memberArg.StaticValue = constExpr.Value;
        }

        public override (object SrcValue, Type SrcPropertyType) GetMapValueAndType(MemberArg memberArg, object source)
        {
            return (memberArg.StaticValue, memberArg.Type);
        }
    }
}
