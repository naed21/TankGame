using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TankGame
{
	public class Tank : Sprite
	{
		public Vector2 VectPos
		{
			get
			{
				return new Vector2(this.Position.X, this.Position.Y);
			}
			set
			{
				Position = new Rectangle((int)value.X, (int)value.Y, this.Position.Width, this.Position.Height);
			}
		}

		public override Sprite Clone(Sprite newSprite = null)
		{
			//Allow inherited classes to create the obj
			if (newSprite == null)
				newSprite = new Tank();

			//This will fill in the sprite specific fields/properties
			base.Clone(newSprite);

			return newSprite;
		}
	}
}
