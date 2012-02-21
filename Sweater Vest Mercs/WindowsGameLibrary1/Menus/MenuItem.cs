using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SVMLib.Menus
{
    public interface MenuItem
    {
        bool Click();

        Rectangle GetItemRect();
        Displayable GetObject();
        Texture2D GetItemBackground();
        String GetTitle();

        void SetItemRect( Rectangle rect );
        void SetObject( Displayable obj );
        void SetItemBackground( Texture2D tex );
        void SetTitle( String title );

        void ItemDraw( SpriteBatch batch );
    }
}
