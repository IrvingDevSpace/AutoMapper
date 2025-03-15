using System;

namespace AutoMapper.Factories.Implementations
{
    internal class LongMapping : MappingBase
    {
        public override object Map<TSource>(TSource source, Type destinationType)
        {
            return Convert.ToInt64(source);
        }
    }
}
