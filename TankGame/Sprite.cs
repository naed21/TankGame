using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TankGame
{
	public class Sprite
	{
		Rectangle? _sourceRect { get; set; }
		Texture2D _texture { get; set; }

		public Rectangle Position { get; set; }
		public Color Tint { get; set; }

		public Sprite()
		{
			Tint = Color.White;
			Position = Rectangle.Empty;
		}

		public virtual void Load(Texture2D texture, Rectangle? sourceRect = null)
		{
			_texture = texture;
			_sourceRect = sourceRect;
		}

		public virtual void Update(GameTime gameTime)
		{

		}

		public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if(_texture != null)
				spriteBatch.Draw(_texture, Position, _sourceRect, Tint);
		}

		public virtual Sprite Clone()
		{
			Sprite newSprite = new Sprite();
			//texture is by ref, but that's good
			//Don't want to have multiple copies of the same texture
			newSprite._texture = this._texture;
			newSprite._sourceRect = this._sourceRect;
			newSprite.Position = this.Position;
			newSprite.Tint = this.Tint;

			return newSprite;
		}
	}
}
