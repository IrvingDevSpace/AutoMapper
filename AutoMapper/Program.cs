using AutoMapper.Dtos;
using AutoMapper.Enums;
using AutoMapper.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

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

            //Student2 student2 = new Student2();
            //student2.ClassRooms = new List<ClassRoom<int>>
            //{
            //    new ClassRoom<int>
            //    {
            //        Name =  new Item<bool, int> { Count = 77},
            //    },
            //    new ClassRoom<int>
            //    {
            //        Name =  new Item<bool, int> { Count = 25 },
            //    },
            //    new ClassRoom<int>
            //    {
            //        Name =  new Item<bool, int> { Count = 100 },
            //    },
            //};

            //var dto2 = Mapper<Student2, StudentDto2<string>>.Map(student2);

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

            Student student = new Student()
            {
                Id = 9,
                Name = "Leo",
                ClassTag = ClassTag.G,
                SignIn = 6,
                Chinese = "70",
                English = "90.25",
                Math = "60",
                Nums = new List<int>() { 1, 6, 4 }
            };

            //Student student2 = new Student()
            //{
            //    Name = "Kelly",
            //    SignIn = 8,
            //    English = 90
            //};

            //Student student3 = new Student()
            //{
            //    Name = "Bob",
            //    SignIn = 2,
            //    Math = 60
            //};

            StudentDto studentDto = new StudentDto();
            //var dto = GetStudentWeightScore<Student>(student, x => x.SignIn, x => x.Math2);

            //GetStudentWeightScore(student3, x => x.Name.Length > 5 ? x.Score * 1.3 : x.Score);

            var dto = Mapper<Student, StudentDto>.Map(student, x => x
                .ForMember(src => src.Id, dest => dest.English)
                .ForMember(src => src.ClassTag.ToString(), dest => dest.Name)
                .ForMember(src => Convert.ToInt32(src.Chinese), dest => dest.Chinese)
                .ForMember(src => int.Parse(123465.ToString()), dest => dest.English)
                .ForMember(src => (object)src.English, dest => dest.Math)
                .ForMember(src => src.SignIn + 1, dest => dest.Description)
                .ForMember(src => src.SignIn / 2, dest => dest.ClassTag)
                .ForMember(src => "132", dest => dest.Score)
                .ForMember(src => src.Id > 18 ? "Adult" : "Child", dest => dest.Category)
            );
        }

        private static double CalcWeightScore(int signinCount, double score)
        {
            if (signinCount > 5)
                return score * 1.1;
            else if (signinCount < 3)
                return score -= 10;
            else if (signinCount >= 8)
                return score * 1.2;
            else
                return signinCount;
        }

        // 根據使用者傳入的任意條件 以及指定的分數 計算期末成績
        // Where(emp=> emp.Salary >  empList.First(y=>y.Loc ==emp)    )
        private static void GetStudentWeightScore<T>(T student, Expression<Func<T, int>> condition, Expression<Func<T, double>> calcField)
        {
            MemberExpression memberExpression = calcField.Body as MemberExpression;
            if (memberExpression.Member is PropertyInfo propertyInfo)
            {
                var temp = propertyInfo.GetValue(student);
            }
            else if (memberExpression.Member is FieldInfo fieldInfo)
            {
                var temp = fieldInfo.GetValue(student);
            }


            //int signinCount = condition.Compile().Invoke(student);
            //double score = calcField.Compile().Invoke(student);
            //double finalScore = CalcWeightScore(signinCount, score);
            //student.Score = finalScore;
            //Console.WriteLine($"學生:{student.Name} 期末成績:{student.Score}");
        }



        class Test<T1, T2>
        {

        }

        class Vaild<T1, T2>
        {

        }
    }
}
