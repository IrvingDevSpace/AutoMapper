using System;

namespace AutoMapper.Factories.Implementations
{
    internal class BoolMapping : MappingBase
    {
        public override object Map(Type srcType, object source, Type destType, Func<Type, object, Type, object> mappingFunc)
        {
            return Convert.ToBoolean(source);
        }
    }
}
