using AutoMapper.Enums;
using AutoMapper.Factories.Implementations;

namespace AutoMapper.Factories.Interfaces
{
    internal interface IMappingFactory
    {
        MappingBase CreateMapping(MappingTag mappingTag);
    }
}
