using AutoMapper.Dtos;
using AutoMapper.Models;
using System.Collections.Generic;

namespace AutoMapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var students = new List<Student>
            {
               new Student { Id = 1, Name = "John Doe", Description = 1, ClassRoom = new ClassRoom { Name = "A"} },
               new Student { Id = 2, Name = "A Doe", Description = 2, ClassRoom = new ClassRoom { Name = "B"} },
               new Student { Id = 3, Name = "B Doe", Description = 0, ClassRoom = new ClassRoom { Name = "C"} },
            };

            var student = new Student { Id = 1, Name = "John Doe", Description = 1, ClassTag = 0, Nums = new List<int> { 0, 1, 0 }, ClassRoom = new ClassRoom { Name = "T" } };
            var student2 = new Student { Id = 1, Name = "John Doe", Description = 1, ClassTag = 0, Nums = new List<int> { 0, 1, 0 }, ClassRoom = new ClassRoom { Name = "T" } };

            var dto = Mapper.Map<Student, StudentDto>(student);
            var dtos = Mapper.Map<Student, StudentDto>(students);
            var dto2 = Mapper.Map<Student, StudentDto2>(student2);

            student.ClassRoom.Name = "C";
        }
    }
}
