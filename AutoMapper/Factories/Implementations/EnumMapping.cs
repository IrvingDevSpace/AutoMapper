using System;

namespace AutoMapper.Factories.Implementations
{
    internal class EnumMapping : MappingBase
    {
        public override object Map(Type srcType, object source, Type destType, Func<Type, object, Type, object> mappingFunc)
        {
            string name = Enum.GetName(destType, source);
            return Enum.Parse(destType, name);
        }
    }
}
