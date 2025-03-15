using System;

namespace AutoMapper.Factories.Implementations
{
    internal class BoolMapping : MappingBase
    {
        public override object Map<TSource>(TSource source, Type destinationType)
        {
            return Convert.ToBoolean(source);
        }
    }
}
