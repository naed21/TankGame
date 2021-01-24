using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace TankGame
{
	public class Menu
	{
		List<Button> _Buttons = new List<Button>();

		public void AddButton(Button button)
		{
			_Buttons.Add(button);
		}

		public int CheckButtonPressed(Rectangle clickedRect)
		{
			foreach(var button in _Buttons)
			{
				if (button.Position.Intersects(clickedRect))
					return button.Id;
			}

			return -1;
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			foreach (var button in _Buttons)
			{
				button.Draw(spriteBatch, gameTime);
			}
		}
	}
}
