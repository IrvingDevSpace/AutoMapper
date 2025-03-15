using System;

namespace AutoMapper.Factories.Implementations
{
    internal class EnumMapping : MappingBase
    {
        public override object Map<TSource>(TSource source, Type destinationType)
        {
            string name = Enum.GetName(destinationType, source);
            return Enum.Parse(destinationType, name);
        }
    }
}
