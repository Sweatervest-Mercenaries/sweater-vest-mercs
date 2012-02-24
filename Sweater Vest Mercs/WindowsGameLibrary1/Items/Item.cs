using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SVMLib.Menus;
using SVMLib.Entities;
using SVMLib.Actions;

namespace SVMLib.Items
{
    public class Item : MenuItem, Displayable
    {
        public Texture2D Icon { get; set; }
        public String Description { get; set; }
        public Texture2D Texture { get; set; }
        public bool Visable { get; set; }
        public String Name { get; set; }
        public Color ItemColor { get; set; }
        public Stats Stats { get; set; }

        public Item(String name)
        {
            Name = name;
            Icon = null;
            Description = "No Description";
            Texture = null;
            Visable = false;
            ItemColor = Color.White;
        }

        public bool EntityMeetsRequirements(LivingEntity ent)
        {
          return Stats <= ent.Stats;
        }

        #region MenuItem Members

        bool MenuItem.Click()
        {
          return GameConstants.PLAYER.Equip(this);
        }

        Rectangle MenuItem.GetItemRect()
        {
          throw new NotImplementedException();
        }

        Displayable MenuItem.GetObject()
        {
          throw new NotImplementedException();
        }

        Texture2D MenuItem.GetItemBackground()
        {
          throw new NotImplementedException();
        }

        string MenuItem.GetTitle()
        {
          throw new NotImplementedException();
        }

        void MenuItem.SetItemRect(Rectangle rect)
        {
          throw new NotImplementedException();
        }

        void MenuItem.SetObject(Displayable obj)
        {
          throw new NotImplementedException();
        }

        void MenuItem.SetItemBackground(Texture2D tex)
        {
          throw new NotImplementedException();
        }

        void MenuItem.SetTitle(string title)
        {
          throw new NotImplementedException();
        }

        void MenuItem.ItemDraw(SpriteBatch batch)
        {
          throw new NotImplementedException();
        }

        #endregion

        #region Displayable Members

        Texture2D Displayable.GetIcon()
        {
            return Icon;
        }

        string Displayable.GetDescription()
        {
            return Description;
        }

        Color Displayable.GetItemColor()
        {
            return ItemColor;
        }

        #endregion
    }
}
