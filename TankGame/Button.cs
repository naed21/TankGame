using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TankGame
{
	public class Button : Sprite
	{
		public int Id { get; set; }
		string _text = "";
		Color _color = Color.Black;
		SpriteFont _font = null;
		Vector2 _textPos = Vector2.Zero;

		public Button(int id, SpriteFont font, string text, Color textColor)
			: base()
		{
			Id = id;
			_text = text;
			_color = textColor;
			_font = font;

			
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			//draw the background first
			base.Draw(spriteBatch, gameTime);

			//Assumes Vector2.Zero means the pos was never set
			if(_textPos == Vector2.Zero)
			{
				var size = _font.MeasureString(_text);
				_textPos = new Vector2(this.Position.X, this.Position.Y);
				_textPos += new Vector2(this.Position.Width / 2, this.Position.Height / 2);
				_textPos -= size / 2;
			}

			if (_text != null && _text != "")
			{
				spriteBatch.DrawString(_font, _text, _textPos, _color);
			}
		}
	}
}
