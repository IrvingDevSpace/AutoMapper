using AutoMapper.Strategies.Abstract;
using AutoMapper.Strategies.Implementations;
using AutoMapper.Strategies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoMapper
{
    internal class MappingExpression<TSource, TDestination>
    {
        private readonly List<IExpressionStrategy> _expressionStrategies = new List<IExpressionStrategy>()
        {
            new ConstantExpressionStrategy(),
            new MemberExpressionStrategy(),
            new ExecutableExpressionStrategy()
        };

        public Dictionary<MemberArg, MemberInfo> MemberInfoDict { get; set; } = new Dictionary<MemberArg, MemberInfo>();

        public MappingExpression<TSource, TDestination> ForMember<TSourceKey, TDestinationKey>(
            Expression<Func<TSource, TSourceKey>> srcExpression,
            Expression<Func<TDestination, TDestinationKey>> destExpression)
        {
            // 儲存dest的欄位或屬性資料
            MemberInfo destMemberInfo = null;

            // dest只能為MemberExpression
            if (!(destExpression.Body is MemberExpression destMemberExpression))
                throw new InvalidOperationException("Custom configuration for members is only supported for top-level individual members on a type.");

            // dest只能為欄位或屬性
            if (destMemberExpression.Member is PropertyInfo destPropInfo)
                destMemberInfo = destPropInfo;
            else if (destMemberExpression.Member is FieldInfo destFieldInfo)
                destMemberInfo = destFieldInfo;
            else
                throw new InvalidOperationException("只能為欄位或屬性");

            // 
            MemberArg memberArg = new MemberArg
            {
                Type = srcExpression.Body.Type,
                Expression = srcExpression.Body
            };

            var strategyType = ExpressionStrategyBase.GetBaseTypeName(srcExpression.Body.GetType());

            var strategy = (IExpressionStrategy)Activator.CreateInstance(strategyType)
                ?? throw new NotSupportedException($"No strategy supports: {strategyType.Name}");

            strategy.SetMemberArg(srcExpression, memberArg);

            MemberInfoDict.Add(memberArg, destMemberInfo);
            return this;
        }
    }
}
