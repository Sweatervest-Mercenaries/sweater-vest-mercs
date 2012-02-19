using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SVMLib.Menus;

namespace SVMLib.Items
{
    public class Item : Displayable
    {
        public Texture2D Icon { get; set; }
        public String Description { get; set; }
        public Texture2D Texture { get; set; }
        public bool Visable { get; set; }
        public String Name { get; set; }
        public Color ItemColor { get; set; }

        public Item(String name)
        {
            Name = name;
            Icon = null;
            Description = "No Description";
            Texture = null;
            Visable = false;
            ItemColor = Color.White;
        }

        public Texture2D GetIcon()
        {
            return Icon;
        }

        public String GetDescription()
        {
            return Description;
        }

        public Color GetItemColor()
        {
            return ItemColor;
        }
    }
}
