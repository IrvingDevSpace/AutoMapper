using System;

namespace AutoMapper.Factories.Implementations
{
    internal class LongMapping : MappingBase
    {
        public override object Map(Type srcType, object source, Type destType, Func<Type, object, Type, object> mappingFunc)
        {
            return Convert.ToInt64(source);
        }
    }
}
