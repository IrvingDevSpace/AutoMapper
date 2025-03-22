using System.Collections.Generic;

namespace AutoMapper.Models
{
    internal class Game
    {
        public int Name { get; set; }

        public List<Folder> Items { get; set; }
    }
}
