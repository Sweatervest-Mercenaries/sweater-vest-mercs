using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SVMLib.Visuals;

namespace SVMLib.Tiles
{
    [Serializable]
    public class TileDataList
    {
        public List<TileData> tiles;
    }

    [Serializable]
    public class TileData
    {
        public String Name;
        public String Hex;
        public Sheet Sheet;
        public int SheetPosition;
        public bool Collide;
        public bool Spawn;
        public int PathStartID;
        public int PathEndID;
        public int TeleportID;

        public override string ToString()
        {
            return Name;
        }
    }
}