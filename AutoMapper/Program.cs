using AutoMapper.Dtos;
using AutoMapper.Models;
using System.Collections.Generic;

namespace AutoMapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var students = new List<Student>
            //{
            //   new Student { Id = 1, Name = "John Doe", Description = 1, ClassRoom = new ClassRoom { Name = "A"} },
            //   new Student { Id = 2, Name = "A Doe", Description = 2, ClassRoom = new ClassRoom { Name = "B"} },
            //   new Student { Id = 3, Name = "B Doe", Description = 0, ClassRoom = new ClassRoom { Name = "C"} },
            //};

            //var student = new Student { Id = 1, Name = "John Doe", Description = 1, ClassTag = 0, Nums = new List<int> { 0, 1, 0 }, ClassRoom = new ClassRoom { Name = "T" } };
            //var student2 = new Student { Id = 1, Name = "John Doe", Description = 1, ClassTag = 0, Nums = new List<int> { 0, 1, 0 }, ClassRoom = new ClassRoom { Name = "T" } };

            //var dto = Mapper.Map<Student, StudentDto>(student);
            //var dtos = Mapper.Map<Student, StudentDto>(students);
            //var dto2 = Mapper.Map<Student, StudentDto2>(student2);

            //student.ClassRoom.Name = "C";

            //Student2 student2 = new Student2();
            //student2.ClassRooms = new List<ClassRoom<string>>
            //{
            //    new ClassRoom<string>
            //    {
            //        Name = "500",
            //        Items = new List<Item>
            //        {
            //            new Item { Count = 100},
            //            new Item { Count = 200 },
            //            new Item { Count = 300},
            //        }
            //    },
            //    new ClassRoom
            //    {
            //        Name = "999",
            //        Items = new List<Item < int >>
            //        {
            //            new Item<int> { Count = 2000},
            //            new Item<int> { Count = 5000 },
            //            new Item<int> { Count = 9},
            //        }
            //    },
            //    new ClassRoom
            //    {
            //        Name = "77",
            //        Items = new List<Item<int>>
            //        {
            //            new Item<int> { Count = 1},
            //            new Item<int> { Count = 2 },
            //            new Item<int> { Count = 999},
            //        }
            //    },
            //};

            Student2 student2 = new Student2();
            student2.ClassRooms = new List<ClassRoom<int>>
            {
                new ClassRoom<int>
                {
                    Name =  new Item<bool, int> { Count = 77},
                },
                new ClassRoom<int>
                {
                    Name =  new Item<bool, int> { Count = 25 },
                },
                new ClassRoom<int>
                {
                    Name =  new Item<bool, int> { Count = 100 },
                },
            };

            var dto2 = Mapper<Student2, StudentDto2<string>>.Map(student2);

            //Item<string> item = new Item<string>
            //{
            //    Count = "95"
            //};
            //var folder = Mapper<Item<string>, Folder<int>>.Map(item);


            //Test<string, int> test = new Test<string, int>();

            ////var a = test.GetType().IsGenericType;
            ////var types = test.GetType().GetGenericArguments();

            ////// test.GetType() => Test<string, int>
            //////IEnumberable`2
            ////var temp = test.GetType().GetGenericTypeDefinition();

            ////Type newType = temp.MakeGenericType(new Type[] { typeof(bool), typeof(float) });
            ////object obj = Activator.CreateInstance(newType);

            //Type type = typeof(Vaild<,>);
            //object o = type;

            //var types = test.GetType().GetGenericArguments();

            //var newType = type.GetGenericTypeDefinition().MakeGenericType(types);

            //object obj = Activator.CreateInstance(newType);
        }

        class Test<T1, T2>
        {

        }

        class Vaild<T1, T2>
        {

        }
    }
}
