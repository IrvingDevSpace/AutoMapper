using AutoMapper.Enums;
using System.Collections.Generic;

namespace AutoMapper.Models
{
    internal class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Description { get; set; }
        public ClassTag ClassTag { get; set; }
        public List<int> Nums { get; set; }

        public int SignIn { get; set; }
        public double Score { get; set; }
        public string Chinese { get; set; }
        public string English { get; set; }
        public string Math { get; set; }

        //public ClassRoom ClassRoom { get; set; }
    }
}
