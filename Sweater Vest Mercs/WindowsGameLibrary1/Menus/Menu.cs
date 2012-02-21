using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SVMLib.Menus
{
    public enum MenuType
    {
        Main, InGame, Combat, Inventory, Skills, Sub
    };

    public class Menu
    {
        public MenuType Type;
        public String Title;
        public Rectangle Rect;
        public List<MenuItem> Items;
        public Texture2D Background;
        public int Width;
        public bool Visable;

        public Menu(MenuType type)
        {
            Type = type;
            Title = "This is a Title";
            Items = new List<MenuItem>();
            Width = 100;
            Visable = false;
            Rect = new Rectangle( 0, 0, Width, GameConstants.SCREEN_MAX_Y );
        }

        public void Open()
        {
            Visable = true;
            GameConstants.PAUSED = true;
        }

        public void Close()
        {
            Visable = false;

            foreach ( MenuItem i in Items )
            {
                if ( i is SubMenu )
                    ( (SubMenu) i ).Close();
            }

            if ( !( this is SubMenu ) )
                GameConstants.PAUSED = false;
        }

        public bool Contains(int x, int y)
        {
            bool result =  ( Rect.Contains( x, y ) );

            if ( !result )
            {
                foreach ( MenuItem i in Items )
                {
                    if ( i is SubMenu )
                        if ( ( (SubMenu) i ).Visable )
                            result = ( (SubMenu) i ).Contains( x, y );

                    if ( result )
                        return true;
                }
            }

            return result;
        }

        public bool Click(int x, int y)
        {
            foreach ( MenuItem item in Items )
            {
                Console.WriteLine( "Checking Item... " + item.GetTitle() + "\t" + item.GetItemRect() );

                if ( item.GetItemRect().Contains( x, y ) )
                {
                    // This is the correct menu
                    // DEBUG
                    Console.WriteLine( item.GetTitle() + " was clicked!" );
                    item.Click();

                    if ( item is SubMenu )
                    {
                        foreach ( MenuItem i in Items )
                        {
                            if ( i is SubMenu && !i.Equals( item ) )
                                ( (SubMenu) i ).Close();
                        }
                    }

                    return true;
                }
                else if ( item is SubMenu )
                {
                    if ( ( (SubMenu) item ).Contains( x, y ) )
                        if ( ( (SubMenu) item ).Visable )
                            return ( (SubMenu) item ).Click( x, y );
                }
            }

            return false;
        }

        public void Draw(SpriteBatch batch)
        {
            if ( Visable )
            {
                int yOffset = 0;
                int Y_STEP = (int) Math.Ceiling(GameConstants.FONT.LineSpacing * GameConstants.TEXT_SCALE);

                // Background
                if ( Background != null )
                {
                    batch.Draw( Background, Rect,
                        null, Color.White, 0, Vector2.Zero, SpriteEffects.None, (float) GameConstants.LAYER_RANGE_MENU_BACKGROUND );
                }

                Vector2 maxPos = new Vector2(Rect.Width,Rect.Height);

                // Menu Title
                GameConstants.DrawString( batch, Title, Vector2.Subtract( maxPos, new Vector2( Width, yOffset ) ),
                    (float) ( GameConstants.LAYER_RANGE_MENU - GameConstants.LAYER_TEXT_OFFSET ) );

                Vector2 itemPos;

                foreach ( MenuItem item in Items )
                {
                    yOffset += Y_STEP;

                    itemPos = Vector2.Subtract( maxPos, new Vector2( Width - 10, yOffset ) );

                    if ( item.GetItemRect().IsEmpty )
                    {
                        item.SetItemRect( new Rectangle( Rect.X, Rect.Height + Rect.Y - yOffset, Width, Y_STEP ) );
                    }

                    item.ItemDraw( batch );
                }
            }
        }
    }

}
