using System;

namespace AutoMapper.Factories.Implementations
{
    internal class FloatMapping : MappingBase
    {
        public override object Map<TSource>(TSource source, Type destinationType)
        {
            return Convert.ToSingle(source);
        }
    }
}
