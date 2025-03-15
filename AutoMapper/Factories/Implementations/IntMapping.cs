using System;

namespace AutoMapper.Factories.Implementations
{
    internal class IntMapping : MappingBase
    {
        public override object Map<TSource>(TSource source, Type destinationType)
        {
            return Convert.ToInt32(source);
        }
    }
}
