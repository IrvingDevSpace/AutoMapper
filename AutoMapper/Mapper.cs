using AutoMapper.Enums;
using AutoMapper.Factories.Implementations;
using AutoMapper.Factories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AutoMapper
{
    internal class Mapper
    {
        public static TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            if (source == null)
                throw new ArgumentNullException();

            var dests = Map<TSource, TDestination>(new List<TSource>() { source });
            return dests.First();
        }

        public static IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> sources) where TDestination : new()
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

        private static object RecursiveMap(Type srcType, object obj, Type destType)
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
                var srcValue = srcPropInfo.GetValue(obj);
                if (srcValue == null)
                    continue;

                // 判斷是否為class，且非string(class類須再遞迴拆解)
                if (destPropInfo.PropertyType.IsClass && destPropInfo.PropertyType != typeof(string))
                {
                    if (typeof(IEnumerable).IsAssignableFrom(destPropInfo.PropertyType))
                    {
                        // 判斷是否為IEnumerable並做轉換
                        if (!(srcValue is IEnumerable sourceEnumerable))
                            continue;

                        // 取得dest IEnumerable<T>，T的型別
                        var destElementType = destPropInfo.PropertyType.GetGenericArguments().FirstOrDefault();
                        // 用T的型別取得List<T>的型別
                        var destListType = typeof(List<>).MakeGenericType(destElementType);
                        // 建立新的 dest List
                        var destList = (IList)Activator.CreateInstance(destListType);

                        foreach (var source in sourceEnumerable)
                        {
                            // 取得要轉換的目標type tag
                            MappingTag mappingTag = GetMappingTag(destElementType);
                            // 依照type tag取得Mapping實作
                            MappingBase mapping = mappingFactory.CreateMapping(mappingTag);
                            // 將src的值轉為dest的型別
                            object destValue = mapping.Map(source, destElementType);
                            // 加入dest List
                            destList.Add(destValue);
                        }
                        // 轉換完整陣列後設定dest的值
                        destPropInfo.SetValue(dest, destList);
                    }
                    else // 若為class須再遞迴拆解
                    {
                        var mappedClass = RecursiveMap(srcPropInfo.PropertyType, srcValue, destPropInfo.PropertyType);

                        if (mappedClass == null)
                            continue;

                        destPropInfo.SetValue(dest, mappedClass);
                    }
                }
                else // 普通型別轉換
                {
                    // 取得要轉換的目標type tag
                    MappingTag mappingTag = GetMappingTag(destPropInfo.PropertyType);
                    // 依照type tag取得Mapping實作
                    MappingBase mapping = mappingFactory.CreateMapping(mappingTag);
                    // 將src的值轉為dest的型別
                    object destValue = mapping.Map(srcValue, destPropInfo.PropertyType);
                    // 設定dest的值
                    destPropInfo.SetValue(dest, destValue);
                }
            }

            return dest;
        }

        private static MappingTag GetMappingTag(Type destPropType)
        {
            if (destPropType == typeof(int))
                return MappingTag.Int;
            else if (destPropType == typeof(long))
                return MappingTag.Long;
            else if (destPropType == typeof(float))
                return MappingTag.Float;
            else if (destPropType == typeof(double))
                return MappingTag.Double;
            else if (destPropType == typeof(char))
                return MappingTag.Char;
            else if (destPropType == typeof(string))
                return MappingTag.String;
            else if (destPropType == typeof(bool))
                return MappingTag.Bool;
            else if (destPropType.IsEnum)
                return MappingTag.Enum;
            else
                throw new ArgumentOutOfRangeException($"不支援的轉換型別 : {destPropType}");
        }
    }
}