using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SVMLib.Tiles;

namespace MapHelper
{
    class Section
    {
        public String Name { get; set; }
        public List<TileData> Elements { get; set; }

        public Section(String name)
        {
            Name = name;
            Elements = new List<TileData>();
        }
    }
}
