using AutoMapper.Enums;
using AutoMapper.Factories.Interfaces;
using System;

namespace AutoMapper.Factories.Implementations
{
    internal class MappingFactory : IMappingFactory
    {
        public MappingBase CreateMapping(MappingTag mappingTag)
        {
            switch (mappingTag)
            {
                case MappingTag.Int:
                    return new IntMapping();
                case MappingTag.Long:
                    return new LongMapping();
                case MappingTag.Float:
                    return new FloatMapping();
                case MappingTag.Double:
                    return new DoubleMapping();
                case MappingTag.Char:
                    return new CharMapping();
                case MappingTag.String:
                    return new StringMapping();
                case MappingTag.Bool:
                    return new BoolMapping();
                case MappingTag.Enum:
                    return new EnumMapping();
                default:
                    throw new ArgumentOutOfRangeException($"不支援的轉換類型 : {mappingTag}");
            }
        }
    }
}
