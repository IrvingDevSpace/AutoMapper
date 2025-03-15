using System;

namespace AutoMapper.Factories.Implementations
{
    internal class CharMapping : MappingBase
    {
        public override object Map<TSource>(TSource source, Type destinationType)
        {
            return Convert.ToChar(source);
        }
    }
}
