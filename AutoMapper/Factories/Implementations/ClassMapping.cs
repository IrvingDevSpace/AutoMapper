using System;

namespace AutoMapper.Factories.Implementations
{
    internal class ClassMapping : MappingBase
    {
        public override object Map(Type srcType, object source, Type destType, Func<Type, object, Type, object> mappingFunc)
        {
            var newType = destType;
            //if (destType.IsGenericType)
            //{
            //    var destGenericArgs = destType.GetGenericArguments();
            //    var definition = destType.GetGenericTypeDefinition();

            //    newType = definition.MakeGenericType(destGenericArgs);
            //}

            var mappedClass = mappingFunc(srcType, source, newType);

            return mappedClass;
        }
    }
}
