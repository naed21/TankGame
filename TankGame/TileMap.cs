using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TankGame
{
	public class TileMap
	{
		List<Sprite> _spriteList;

		int _offsetX;
		int _offsetY;
		int _rows;
		int _columns;
		int _tileSize;

		public TileMap(int offsetX, int offsetY, int rows, int columns, int tileSize)
		{
			_offsetX = offsetX;
			_offsetY = offsetY;
			_rows = rows;
			_columns = columns;
			_tileSize = tileSize;

			_spriteList = new List<Sprite>();
		}

		public bool PlaceTile(Sprite tile, Point postion)
		{
			int targetColumn = (postion.X - _offsetX) / _tileSize;
			int targetRow = (postion.Y - _offsetY) / _tileSize;

			//Off the edge of the map
			if (targetColumn >= _columns || targetRow >= _rows)
				return false;

			if (targetColumn < 0 || targetRow < 0)
				return false;

			//check if sprite already located at this position
			int targetX = targetColumn * _tileSize + _offsetX;
			int targetY = targetRow * _tileSize + _offsetY;

			Sprite target = null;
			foreach (var sprite in _spriteList)
				if (sprite.Position.X == targetX && sprite.Position.Y == targetY)
				{
					target = sprite;
					break;
				}

			//replace with new tile
			if (target != null)
				_spriteList.Remove(target);

			tile.Position = new Rectangle(targetX, targetY, _tileSize, _tileSize);
			_spriteList.Add(tile);

			return true;
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			foreach (var sprite in _spriteList)
				sprite.Draw(spriteBatch, gameTime);
		}

		internal void Fill(Sprite sprite)
		{
			for(int x = 0; x < _columns; x++)
			{
				for(int y = 0; y < _rows; y++)
				{
					var tile = sprite.Clone();
					int targetX = x * _tileSize + _offsetX;
					int targetY = y * _tileSize +_offsetY;
					tile.Position = new Rectangle(targetX, targetY, _tileSize, _tileSize);
					_spriteList.Add(tile);
				}
			}
		}

		/// <summary>
		/// Get the tile pos based on the row and column provided
		/// </summary>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <returns></returns>
		public Vector2 GetTilePos(int column, int row)
		{
			//Make sure the values fit in the correct range
			if (column >= _columns)
				column = _columns - 1;
			else if (column < 0)
				column = 0;

			if (row >= _rows)
				row = _rows - 1;
			else if (row < 0)
				row = 0;

			float x = column * _tileSize + _offsetX;
			float y = row * _tileSize + _offsetY;

			return new Vector2(x, y);
		}

		/// <summary>
		/// Get the row and column closest to the Vector2 provided
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public Point GetTile(Vector2 pos)
		{
			int targetColumn = ((int)pos.X - _offsetX) / _tileSize;
			int targetRow = ((int)pos.Y - _offsetY) / _tileSize;

			//since we're starting at 0, need to be 1 less than max
			if (targetColumn >= _columns)
				targetColumn = _columns - 1;
			else if (targetColumn < 0)
				targetColumn = 0;

			if (targetRow >= _rows)
				targetRow = _rows - 1;
			else if (targetRow < 0)
				targetRow = 0;

			return new Point(targetColumn, targetRow);
		}
	}
}
