using System;
using System.Linq.Expressions;

namespace AutoMapper.Strategies.Interfaces
{
    internal interface IExpressionStrategy
    {
        void SetMemberArg(LambdaExpression expression, MemberArg memberArg);

        (object SrcValue, Type SrcPropertyType) GetMapValueAndType(MemberArg memberArg, object source);
    }
}
