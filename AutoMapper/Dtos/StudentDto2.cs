using AutoMapper.Models;

namespace AutoMapper.Dtos
{
    internal class StudentDto2<T>
    {
        public Game<T>[] ClassRooms { get; set; }
    }
}
