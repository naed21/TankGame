using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace TankGame
{
	public class PlayerController
	{
		//TODO:
		/*
		 * Add turret that points towards the mouse cursor
		 * Press left click to shoot -> play audio -> spawn bullet
		 * Rotate the tank so it's heading towards the direct it moved
		 * Tank leaves treads in the tile it was just at
		 * Have a target to shoot at
		 */

		Tank _player = null;
		TileMap _map = null;

		Point _startTilePos = Point.Zero;

		public bool IsSetup
		{
			get
			{
				return _player != null;
			}
		}

		public PlayerController()
		{

		}

		/// <summary>
		/// Setup the playercontroller, call after the tile map is created
		/// </summary>
		/// <param name="player"></param>
		/// <param name="map"></param>
		/// <param name="startingTile"></param>
		public void Setup(Tank player, TileMap map, Point startingTile)
		{
			_player = player;
			_map = map;
			_startTilePos = startingTile;

			var vect2 = map.GetTilePos(startingTile.X, startingTile.Y);
			_player.VectPos = vect2;
		}

		KeyboardState? prevKeyState = null;
		TimeSpan prevMove = new TimeSpan();
		public void Update(MouseState mouseState, KeyboardState keyboardState, GameTime gametime)
		{
			bool allowMove = false;
			if (prevMove.TotalMilliseconds < 500)
			{
				prevMove = prevMove.Add(gametime.ElapsedGameTime);
			}
			else
				allowMove = true;
			
			if (!prevKeyState.HasValue)
				prevKeyState = keyboardState;

			if(allowMove && keyboardState.IsKeyDown(Keys.W) && !prevKeyState.Value.IsKeyDown(Keys.W))
			{
				MovePlayer(0, -1);
			}
			if (allowMove && keyboardState.IsKeyDown(Keys.A) && !prevKeyState.Value.IsKeyDown(Keys.A))
			{
				MovePlayer(-1, 0);
			}
			if (allowMove && keyboardState.IsKeyDown(Keys.S) && !prevKeyState.Value.IsKeyDown(Keys.S))
			{
				MovePlayer(0, 1);
			}
			if (allowMove && keyboardState.IsKeyDown(Keys.D) && !prevKeyState.Value.IsKeyDown(Keys.D))
			{
				MovePlayer(1, 0);
			}

			if(keyboardState.IsKeyDown(Keys.Back) && !prevKeyState.Value.IsKeyDown(Keys.Back))
			{
				ResetPlayer();
			}

			prevKeyState = keyboardState;
		}

		private void ResetPlayer()
		{
			_player.VectPos = _map.GetTilePos(_startTilePos.X, _startTilePos.Y);
		}

		/// <summary>
		/// take the player's current tile pos and change that pos based on input
		/// </summary>
		/// <param name="columnChange"></param>
		/// <param name="rowChange"></param>
		private void MovePlayer(int columnChange, int rowChange)
		{
			var currentTile = _map.GetTile(_player.VectPos);
			currentTile.X += columnChange;
			currentTile.Y += rowChange;

			var newPos = _map.GetTilePos(currentTile.X, currentTile.Y);
			_player.VectPos = newPos;

			//reset
			prevMove = prevMove.Subtract(prevMove);
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			//Draw the player and any game menu
			_player.Draw(spriteBatch, gameTime);
		}
	}
}
