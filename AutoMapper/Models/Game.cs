namespace AutoMapper.Models
{
    internal class Game<T>
    {
        public Folder<T, long> Name { get; set; }

        //public List<Folder<T>> Items { get; set; }
    }
}
