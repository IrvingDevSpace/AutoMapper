using System;

namespace AutoMapper.Factories.Implementations
{
    internal abstract class MappingBase
    {
        public abstract object Map(Type srcType, object source, Type destType, Func<Type, object, Type, object> mappingFunc);
    }
}
