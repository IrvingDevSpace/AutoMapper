using System;

namespace AutoMapper.Factories.Implementations
{
    internal abstract class MappingBase
    {
        public abstract object Map<TSource>(TSource source, Type destinationType);
    }
}
