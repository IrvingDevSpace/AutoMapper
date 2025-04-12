using AutoMapper.Enums;
using AutoMapper.Factories.Implementations;
using AutoMapper.Factories.Interfaces;
using AutoMapper.Strategies.Abstract;
using AutoMapper.Strategies.Implementations;
using AutoMapper.Strategies.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoMapper
{
    internal class Mapper<TSource, TDestination>
    {
        public static TDestination Map(TSource source)
        {
            if (source == null)
                throw new ArgumentNullException();

            var dests = Map(new List<TSource>() { source });
            return dests.First();
        }

        public static TDestination Map(
            TSource source,
            Action<MappingExpression<TSource, TDestination>> action)
        {
            if (source == null)
                throw new ArgumentNullException();

            var mappingExpression = new MappingExpression<TSource, TDestination>();
            action.Invoke(mappingExpression);

            var dests = Map(new List<TSource>() { source }, mappingExpression);
            return dests.First();
        }


        public static IEnumerable<TDestination> Map(IEnumerable<TSource> sources)
        {
            if (sources == null)
                throw new ArgumentNullException();

            Type srcType = typeof(TSource);
            Type destType = typeof(TDestination);

            List<TDestination> dests = new List<TDestination>();
            foreach (var source in sources)
            {
                dests.Add((TDestination)RecursiveMap(srcType, source, destType));
            }

            return dests;
        }

        public static IEnumerable<TDestination> Map(
            IEnumerable<TSource> sources,
            MappingExpression<TSource, TDestination> mappingExpression)
        {
            if (sources == null)
                throw new ArgumentNullException();

            IMappingFactory mappingFactory = new MappingFactory();

            Type srcType = typeof(TSource);
            Type destType = typeof(TDestination);

            List<TDestination> dests = new List<TDestination>();
            foreach (var source in sources)
            {
                var dest = (TDestination)RecursiveMap(srcType, source, destType);

                foreach (var mappingExpress in mappingExpression.MemberInfoDict)
                {
                    object srcValue = null;
                    Type srcPropertyType = null;

                    var strategyType = ExpressionStrategyBase.GetBaseTypeName(mappingExpress.Key.Expression.GetType());

                    var strategy = (IExpressionStrategy)Activator.CreateInstance(strategyType)
                        ?? throw new NotSupportedException($"No strategy supports: {strategyType.Name}");

                    var result = strategy.GetMapValueAndType(mappingExpress.Key, source);

                    srcValue = result.SrcValue;
                    srcPropertyType = result.SrcPropertyType;


                    //if (mappingExpress.Key.StaticValue != null)
                    //{
                    //    srcValue = mappingExpress.Key.StaticValue;
                    //    srcPropertyType = mappingExpress.Key.Type;
                    //}
                    //else if (mappingExpress.Key.ValueDelegate != null)
                    //{
                    //    srcValue = mappingExpress.Key.ValueDelegate.DynamicInvoke(source);
                    //    srcPropertyType = mappingExpress.Key.Type;
                    //}
                    //else if (mappingExpress.Key.MemberInfo is PropertyInfo srcPropInfo)
                    //{
                    //    srcValue = srcPropInfo.GetValue(source);
                    //    srcPropertyType = srcPropInfo.PropertyType;
                    //}
                    //else if (mappingExpress.Key.MemberInfo is FieldInfo srcFieldInfo)
                    //{
                    //    srcValue = srcFieldInfo.GetValue(source);
                    //    srcPropertyType = srcFieldInfo.FieldType;
                    //}

                    if (mappingExpress.Value is PropertyInfo destPropInfo)
                    {
                        // 取得要轉換的目標type tag
                        MappingTag mappingTag = GetMappingTag(destPropInfo.PropertyType);
                        // 依照type tag取得Mapping實作
                        MappingBase mapping = mappingFactory.CreateMapping(mappingTag);
                        // 將src的值轉為dest的型別
                        object destValue = mapping.Map(srcPropertyType, srcValue, destPropInfo.PropertyType, RecursiveMap);
                        // 設定dest的值
                        destPropInfo.SetValue(dest, destValue);
                    }
                    else if (mappingExpress.Value is FieldInfo destFieldInfo)
                    {
                        // 取得要轉換的目標type tag
                        MappingTag mappingTag = GetMappingTag(destFieldInfo.FieldType);
                        // 依照type tag取得Mapping實作
                        MappingBase mapping = mappingFactory.CreateMapping(mappingTag);
                        // 將src的值轉為dest的型別
                        object destValue = mapping.Map(srcPropertyType, srcValue, destFieldInfo.FieldType, RecursiveMap);
                        // 設定dest的值
                        destFieldInfo.SetValue(dest, destValue);
                    }
                }

                dests.Add(dest);
            }

            return dests;
        }

        //private static object RecursiveMap(Type srcType, object source, Type destType)
        //{
        //    // src的屬性名稱對應PropInfo 的Dict
        //    var srcPropInfosDict = srcType.GetProperties().ToDictionary(x => x.Name, x => x);
        //    // 建立 dest物件
        //    var dest = Activator.CreateInstance(destType);
        //    // dest所有屬性Info
        //    var destPropInfos = destType.GetProperties().ToList();
        //    IMappingFactory mappingFactory = new MappingFactory();

        //    // 依照dest prop的數量跑迴圈
        //    foreach (var destPropInfo in destPropInfos)
        //    {
        //        // 檢查srcPropInfosDict是否存在與dest相同屬性名稱的propInfo
        //        if (!srcPropInfosDict.TryGetValue(destPropInfo.Name, out var srcPropInfo))
        //            continue;
        //        // 取得src這個屬性的value
        //        var srcValue = srcPropInfo.GetValue(source);
        //        if (srcValue == null)
        //            continue;

        //        // 取得要轉換的目標type tag
        //        MappingTag mappingTag = GetMappingTag(destPropInfo.PropertyType);
        //        // 依照type tag取得Mapping實作
        //        MappingBase mapping = mappingFactory.CreateMapping(mappingTag);
        //        // 將src的值轉為dest的型別
        //        object destValue = mapping.Map(srcPropInfo.PropertyType, srcValue, destPropInfo.PropertyType, RecursiveMap);
        //        // 設定dest的值
        //        destPropInfo.SetValue(dest, destValue);
        //    }

        //    return dest;
        //}

        private static object RecursiveMap(Type srcType, object source, Type destType)
        {
            IMappingFactory mappingFactory = new MappingFactory();

            if (IsSimpleType(srcType))
            {
                // 取得要轉換的目標type tag
                MappingTag mappingTag = GetMappingTag(destType);
                // 依照type tag取得Mapping實作
                MappingBase mapping = mappingFactory.CreateMapping(mappingTag);
                // 將src的值轉為dest的型別
                object destValue = mapping.Map(srcType, source, destType, RecursiveMap);
                // 設定dest的值
                return destValue;
            }

            // src的屬性名稱對應PropInfo 的Dict
            var srcPropInfosDict = srcType.GetProperties().ToDictionary(x => x.Name, x => x);

            // 建立 dest物件
            var dest = Activator.CreateInstance(destType);
            // dest所有屬性Info
            var destPropInfos = destType.GetProperties().ToList();

            // 依照dest prop的數量跑迴圈
            foreach (var destPropInfo in destPropInfos)
            {
                object srcValue = null;
                // 嘗試從 srcPropInfosDict 中取得與 destPropInfo.Name 相同的屬性資訊
                if (srcPropInfosDict.TryGetValue(destPropInfo.Name, out var srcPropInfo))
                    // 從屬性取得 src 的值
                    srcValue = srcPropInfo.GetValue(source);
                else
                    continue;

                // 如果取得的值為 null，則跳過此次迴圈
                if (srcValue == null)
                    continue;

                // 取得要轉換的目標type tag
                MappingTag mappingTag = GetMappingTag(destPropInfo.PropertyType);
                // 依照type tag取得Mapping實作
                MappingBase mapping = mappingFactory.CreateMapping(mappingTag);
                // 將src的值轉為dest的型別
                object destValue = mapping.Map(srcPropInfo.PropertyType, srcValue, destPropInfo.PropertyType, RecursiveMap);
                // 設定dest的值
                destPropInfo.SetValue(dest, destValue);
            }

            return dest;
        }

        // 判斷是否為基本或簡單型別
        private static bool IsSimpleType(Type type)
        {
            return type.IsPrimitive
                || type.IsEnum
                || type.Equals(typeof(string))
                || type.Equals(typeof(decimal))
                || type.Equals(typeof(DateTime))
                || type.Equals(typeof(DateTimeOffset))
                || type.Equals(typeof(TimeSpan))
                || type.Equals(typeof(Guid));
        }

        private static MappingTag GetMappingTag(Type destPropType)
        {
            switch (destPropType)
            {
                case Type t when t.IsClass && t != typeof(string):
                    return typeof(IEnumerable).IsAssignableFrom(destPropType)
                        ? MappingTag.Enumerable
                        : MappingTag.Class;
                case Type t when t == typeof(int):
                    return MappingTag.Int;
                case Type t when t == typeof(long):
                    return MappingTag.Long;
                case Type t when t == typeof(float):
                    return MappingTag.Float;
                case Type t when t == typeof(double):
                    return MappingTag.Double;
                case Type t when t == typeof(char):
                    return MappingTag.Char;
                case Type t when t == typeof(string):
                    return MappingTag.String;
                case Type t when t == typeof(bool):
                    return MappingTag.Bool;
                case Type t when t.IsEnum:
                    return MappingTag.Enum;
                default:
                    throw new ArgumentOutOfRangeException($"不支援的轉換型別 : {destPropType}");
            }
        }

        private readonly static List<IExpressionStrategy> _expressionStrategies = new List<IExpressionStrategy>()
        {
            new ConstantExpressionStrategy(),
            new MemberExpressionStrategy(),
            new ExecutableExpressionStrategy()
        };
    }
}