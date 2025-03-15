using System;

namespace AutoMapper.Factories.Implementations
{
    internal class DoubleMapping : MappingBase
    {
        public override object Map<TSource>(TSource source, Type destinationType)
        {
            return Convert.ToDouble(source);
        }
    }
}
