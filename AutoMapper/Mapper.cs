using AutoMapper.Enums;
using AutoMapper.Factories.Implementations;
using AutoMapper.Factories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        private static object RecursiveMap(Type srcType, object source, Type destType)
        {
            // src的屬性名稱對應PropInfo 的Dict
            var srcPropInfosDict = srcType.GetProperties().ToDictionary(x => x.Name, x => x);
            // 建立 dest物件
            var dest = Activator.CreateInstance(destType);
            // dest所有屬性Info
            var destPropInfos = destType.GetProperties().ToList();
            IMappingFactory mappingFactory = new MappingFactory();

            // 依照dest prop的數量跑迴圈
            foreach (var destPropInfo in destPropInfos)
            {
                // 檢查srcPropInfosDict是否存在與dest相同屬性名稱的propInfo
                if (!srcPropInfosDict.TryGetValue(destPropInfo.Name, out var srcPropInfo))
                    continue;
                // 取得src這個屬性的value
                var srcValue = srcPropInfo.GetValue(source);
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
    }
}