using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SVMLib.Entities;

namespace SVMLib.Tiles
{
    public class TileEntity : Entity
    {
        public bool Spawn { get; set; }
        public int PathStartID { get; set; }
        public int PathEndID { get; set; }
        public Color ColorData { get; set; }
        public int TeleportID { get; set; }

        public TileEntity()
            : base()
        {

        }

        public TileEntity( Texture2D tex )
            : base( tex )
        {

        }

        public TileEntity( Texture2D tex, Rectangle rec )
            : base( tex, rec )
        {

        }

        public TileEntity( Texture2D tex, Rectangle rec, Vector2 pos )
            : base( tex, rec, pos )
        {

        }
    }
}
