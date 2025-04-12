using AutoMapper.Enums;
using System.Collections.Generic;

namespace AutoMapper.Dtos
{
    internal class StudentDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ClassTag ClassTag { get; set; }

        public List<ClassTag> ClassTags { get; set; }

        public double Chinese { get; set; }
        public double English { get; set; }
        public double Math { get; set; }
        public string Category { get; set; }
        public double Score { get; set; }

        //public ClassRoom ClassRoom { get; set; }
    }
}
