using AutoMapper.Strategies.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoMapper.Strategies.Abstract
{
    internal abstract class ExpressionStrategyBase : IExpressionStrategy
    {
        public abstract void SetMemberArg(LambdaExpression expression, MemberArg memberArg);

        public abstract (object SrcValue, Type SrcPropertyType) GetMapValueAndType(MemberArg memberArg, object source);

        protected MemberInfo FindMemberInfo(Expression expr)
        {
            if (expr is MemberExpression member)
                return member.Member;

            if (expr is UnaryExpression unary)
                return FindMemberInfo(unary.Operand);

            if (expr is MethodCallExpression methodCall)
            {
                if (methodCall.Object != null)
                    return FindMemberInfo(methodCall.Object);
                if (methodCall.Arguments.Count > 0)
                    return FindMemberInfo(methodCall.Arguments.First());
            }

            if (expr is BinaryExpression binary)
                return FindMemberInfo(binary.Left);

            if (expr is ConditionalExpression conditional)
                return FindMemberInfo(conditional.Test);

            return null;
        }

        public static Type GetBaseTypeName(Type type)
        {
            var exprName = type.Name;

            var strategyType = Type.GetType($"AutoMapper.Strategies.Implementations.{exprName}Strategy");

            if (strategyType != null)
                return strategyType;

            return GetBaseTypeName(type.BaseType);
        }
    }
}
