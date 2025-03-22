using System;

namespace AutoMapper.Factories.Implementations
{
    internal class StringMapping : MappingBase
    {
        public override object Map(Type srcType, object source, Type destType, Func<Type, object, Type, object> mappingFunc)
        {
            return Convert.ToString(source);
        }
    }
}
