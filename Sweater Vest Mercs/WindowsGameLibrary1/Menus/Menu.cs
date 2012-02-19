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
            Background = null;
            Rect = new Rectangle( 0, 0, Width, GameConstants.SCREEN_MAX_Y );
        }

        public void Open()
        {
            Visable = true;
        }

        public void Close()
        {
            Visable = false;
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
                    batch.Draw( Background, new Rectangle( Rect.X, Rect.Y, Width, Rect.Height ),
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

                    // Draw the item background
                    if ( item.ItemBackground != null )
                    {
                        batch.Draw( item.ItemBackground, new Rectangle( Rect.X, Rect.Height + Rect.Y - yOffset, Width, Y_STEP ),
                            null, Color.White, 0, Vector2.Zero, SpriteEffects.None, (float) GameConstants.LAYER_RANGE_MENU );
                    }

                    // Draw the item text
                    GameConstants.DrawString( batch, item.Title, itemPos, (float) ( GameConstants.LAYER_RANGE_MENU - GameConstants.LAYER_TEXT_OFFSET ) );
                }
            }
        }
    }

    public class MenuItem
    {
        public String Title;
        public Texture2D ItemBackground;
        public Displayable Obj;

        public MenuItem(String t, Displayable o)
        {
            Title = t;
            Obj = o;
        }
    }
}
