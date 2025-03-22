using AutoMapper.Enums;
using System.Collections.Generic;

namespace AutoMapper.Dtos
{
    internal class StudentDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Description { get; set; }
        public ClassTag ClassTag { get; set; }

        public List<bool> Nums { get; set; }

        //public ClassRoom ClassRoom { get; set; }
    }
}
