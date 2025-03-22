using System.Collections.Generic;

namespace AutoMapper.Models
{
    internal class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Description { get; set; }
        public int ClassTag { get; set; }
        public List<int> Nums { get; set; }


        //public ClassRoom ClassRoom { get; set; }
    }
}
