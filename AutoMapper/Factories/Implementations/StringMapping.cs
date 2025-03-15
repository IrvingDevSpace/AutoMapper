using System;

namespace AutoMapper.Factories.Implementations
{
    internal class StringMapping : MappingBase
    {
        public override object Map<TSource>(TSource source, Type destinationType)
        {
            return Convert.ToString(source);
        }
    }
}
