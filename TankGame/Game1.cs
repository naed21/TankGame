using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using TankGameLibrary;

namespace TankGame
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		TextureAtlas _textureAtlas = null;

		GameState _gameState = GameState.Menu;

		Texture2D _tileSheet = null;
		Sprite[] _tileSprites = null;
		TileMap _map = null;

		Menu _mainMenu = null;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;

			_graphics.PreferredBackBufferHeight = 720;
			_graphics.PreferredBackBufferWidth = 1280;
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			_map = new TileMap(70, 80, 9, 18, 64);

			_mainMenu = new Menu();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			//TankGameLibrary.SubTexture[] textureAtlas2 = null;
			
			// TODO: use this.Content to load your game content here
			using (var reader = new StreamReader(Path.Combine(Content.RootDirectory, "allSprites_default.xml")))
			{
				var serializer = new XmlSerializer(typeof(TextureAtlas));
				_textureAtlas = (TextureAtlas)serializer.Deserialize(reader);
			}

			_tileSheet = Content.Load<Texture2D>("allSprites_default");

			SpriteFont font = Content.Load<SpriteFont>("myFont");
			//textureAtlas2 = Content.Load<TankGameLibrary.SubTexture[]>("allSprites_modified");

			//XmlWriterSettings settings = new XmlWriterSettings();
			//settings.Indent = true;

			//using (XmlWriter writer = XmlWriter.Create("example.xml", settings))
			//{
			//	IntermediateSerializer.Serialize(writer, textureAtlas, null);
			//}

			_tileSprites = new Sprite[8];
			_tileSprites[0] = new Sprite();
			_tileSprites[0].Load(_tileSheet, _textureAtlas.GetRectangle("tileGrass1"));
			_tileSprites[1] = new Sprite();
			_tileSprites[1].Load(_tileSheet, _textureAtlas.GetRectangle("tileGrass2"));
			_tileSprites[2] = new Sprite();
			_tileSprites[2].Load(_tileSheet, _textureAtlas.GetRectangle("tileSand1"));
			_tileSprites[3] = new Sprite();
			_tileSprites[3].Load(_tileSheet, _textureAtlas.GetRectangle("tileSand2"));
			_tileSprites[4] = new Sprite();
			_tileSprites[4].Load(_tileSheet, _textureAtlas.GetRectangle("tileGrass_transitionN"));
			_tileSprites[5] = new Sprite();
			_tileSprites[5].Load(_tileSheet, _textureAtlas.GetRectangle("tileGrass_transitionE"));
			_tileSprites[6] = new Sprite();
			_tileSprites[6].Load(_tileSheet, _textureAtlas.GetRectangle("tileGrass_transitionS"));
			_tileSprites[7] = new Sprite();
			_tileSprites[7].Load(_tileSheet, _textureAtlas.GetRectangle("tileGrass_transitionW"));

			var editorButton = new Button((int)MainMenuButton.MapEditor, font, "Map Editor", Color.Black);
			var gameButton = new Button((int)MainMenuButton.StartGame, font, "Start Game", Color.Black);
			var quitButton = new Button((int)MainMenuButton.Quit, font, "Quit", Color.Black);
			editorButton.Load(_tileSheet, _textureAtlas.GetRectangle("treeGreen_large"));
			gameButton.Load(_tileSheet, _textureAtlas.GetRectangle("treeGreen_large"));
			quitButton.Load(_tileSheet, _textureAtlas.GetRectangle("treeGreen_large"));

			editorButton.Position = new Rectangle(100, 100, 120, 80);
			gameButton.Position = new Rectangle(100, 200, 120, 80);
			quitButton.Position = new Rectangle(100, 300, 120, 80);

			_mainMenu.AddButton(editorButton);
			_mainMenu.AddButton(gameButton);
			_mainMenu.AddButton(quitButton);
		}

		MouseState? _prevMouseState = null;
		KeyboardState? _prevKeyState = null;
		Sprite _activeSprite = null;
		bool defaultFillMap = true;

		protected override void Update(GameTime gameTime)
		{
			//if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			//	Exit();

			// TODO: Add your update logic here
			var mouseState = Mouse.GetState();
			var keyState = Keyboard.GetState();

			if (!_prevMouseState.HasValue)
				_prevMouseState = mouseState;
			if (!_prevKeyState.HasValue)
				_prevKeyState = keyState;

			if(_gameState == GameState.MapEditor)
			{
				if(defaultFillMap)
				{
					defaultFillMap = false;
					_map.Fill(_tileSprites[0].Clone());
				}
				
				if (keyState.IsKeyDown(Keys.D1) && _prevKeyState.Value.IsKeyUp(Keys.D1))
					_activeSprite = _tileSprites[0];
				if (keyState.IsKeyDown(Keys.D2) && _prevKeyState.Value.IsKeyUp(Keys.D2))
					_activeSprite = _tileSprites[1];
				if (keyState.IsKeyDown(Keys.D3) && _prevKeyState.Value.IsKeyUp(Keys.D3))
					_activeSprite = _tileSprites[2];
				if (keyState.IsKeyDown(Keys.D4) && _prevKeyState.Value.IsKeyUp(Keys.D4))
					_activeSprite = _tileSprites[3];
				if (keyState.IsKeyDown(Keys.D5) && _prevKeyState.Value.IsKeyUp(Keys.D5))
					_activeSprite = _tileSprites[4];
				if (keyState.IsKeyDown(Keys.D6) && _prevKeyState.Value.IsKeyUp(Keys.D6))
					_activeSprite = _tileSprites[5];
				if (keyState.IsKeyDown(Keys.D7) && _prevKeyState.Value.IsKeyUp(Keys.D7))
					_activeSprite = _tileSprites[6];
				if (keyState.IsKeyDown(Keys.D8) && _prevKeyState.Value.IsKeyUp(Keys.D8))
					_activeSprite = _tileSprites[7];

				if (_activeSprite != null && mouseState.LeftButton == ButtonState.Pressed && _prevMouseState.Value.LeftButton == ButtonState.Released)
					_map.PlaceTile(_activeSprite.Clone(), mouseState.Position);

				if (keyState.IsKeyDown(Keys.Escape))
					_gameState = GameState.Menu;
			}
			else if(_gameState == GameState.Menu)
			{
				if (mouseState.LeftButton == ButtonState.Pressed && _prevMouseState.Value.LeftButton == ButtonState.Released)
				{
					int buttonPressed = _mainMenu.CheckButtonPressed(new Rectangle(mouseState.X, mouseState.Y, 1, 1));

					if (buttonPressed == (int)MainMenuButton.Quit)
						this.Exit();
					else if (buttonPressed == (int)MainMenuButton.MapEditor)
						_gameState = GameState.MapEditor;
					else if (buttonPressed == (int)MainMenuButton.StartGame)
						_gameState = GameState.Game;
				}
			}
			else if(_gameState == GameState.Game)
			{

			}

			_prevMouseState = mouseState;
			_prevKeyState = keyState;

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			_spriteBatch.Begin();
			// TODO: Add your drawing code here
			if (_gameState == GameState.MapEditor)
			{
				_map.Draw(_spriteBatch, gameTime);
			}
			else if (_gameState == GameState.Menu)
			{
				_mainMenu.Draw(_spriteBatch, gameTime);
			}

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
