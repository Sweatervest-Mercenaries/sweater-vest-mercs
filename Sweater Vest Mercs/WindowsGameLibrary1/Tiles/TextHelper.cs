using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SVMLib.Entities;
using SVMLib.Visuals;

namespace SVMLib.Tiles
{
  class TextHelper : Entity
  {
    private String _text;
    public Entity[] letters;
    private const float TEXT_SCALE = 1.0f;
    private const int ASCII_OFFSET = 32;

    public String Text
    {
      get { return _text; }
      set
      {
        _text = value;
        populateLetters();
      }
    }

    public TextHelper(Texture2D texture) : base(texture)
    {
    }

    public override void Draw(SpriteBatch batch)
    {
      foreach (Entity e in letters)
      {
        e.Draw(batch);
      }
    }

    private void populateLetters()
    {
      letters = new Entity[_text.Length];
      int index = 0;

      foreach (Char c in _text)
      {
        letters[index] = new Entity(SpriteSheet.getSpriteSheet(Sheet.Alphabet).Texture, SpriteSheet.getSpriteSheet(Sheet.Alphabet).getSourceRect(c - ASCII_OFFSET));
        letters[index].Scale = TEXT_SCALE;
        letters[index].Position = Position + new Vector2(index * GameConstants.NUM_PIXEL * TEXT_SCALE, 0);
        letters[index].Layer = Layer;
        letters[index].Color = Color.Black;
        index++;
      }
    }
  }
}
