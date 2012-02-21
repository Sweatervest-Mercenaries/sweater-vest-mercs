using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SVMLib.Menus
{
    public class SubMenu : Menu, MenuItem
    {
        public Rectangle ItemRect;
        public Displayable Obj;
        public Texture2D ItemBackground;
        public String ItemTitle;

        public SubMenu( String title ) :
            base( MenuType.Sub )
        {
            Title = title;
            ItemTitle = title;
        }

        bool MenuItem.Click()
        {
            if ( !Visable )
            {
                Rect = new Rectangle( ItemRect.X + ItemRect.Width, 0, ItemRect.Width, GameConstants.SCREEN_MAX_Y );

                Console.WriteLine( Title + " was clicked... " + Rect );
                Open();

                return true;
            }

            return false;
        }

        Rectangle MenuItem.GetItemRect()
        {
            return ItemRect;
        }

        Displayable MenuItem.GetObject()
        {
            return Obj;
        }

        Texture2D MenuItem.GetItemBackground()
        {
            return ItemBackground;
        }

        string MenuItem.GetTitle()
        {
            return ItemTitle;
        }

        void MenuItem.SetItemRect( Rectangle rect )
        {
            ItemRect = rect;
        }

        void MenuItem.SetObject( Displayable obj )
        {
            Obj = obj;
        }

        void MenuItem.SetItemBackground( Texture2D tex )
        {
            ItemBackground = tex;
        }

        void MenuItem.SetTitle( string title )
        {
            ItemTitle = title;
        }


        void MenuItem.ItemDraw( SpriteBatch batch )
        {
            // Draw the item background
            if ( ItemBackground != null )
            {
                batch.Draw( ItemBackground, ItemRect,
                    null, Color.White, 0, Vector2.Zero, SpriteEffects.None, (float) GameConstants.LAYER_RANGE_MENU );
            }

            // Draw the item text
            GameConstants.DrawString( batch, ItemTitle, new Vector2( ItemRect.X + 10, ItemRect.Y ), (float) ( GameConstants.LAYER_RANGE_MENU - GameConstants.LAYER_TEXT_OFFSET ) );

            // Check to see if submenu is open...
            if ( Visable )
            {
                Draw( batch );
            }
        }
    }
}
