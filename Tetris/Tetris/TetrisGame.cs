using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Tetris
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class TetrisGame : Microsoft.Xna.Framework.Game
    {
        //Graphic
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D _background;
        private Piece.APiece _currentPiece;
        private Piece.APiece _nextPiece;

        //Board
        Board _board;

        //Player
        private KeyboardState _keyboardState;
        private KeyboardState _previousKeyboardState;
        private MouseState _mouseState;

        //Screen
        private int _screenHeight;
        private int _screenWidth;

        //Sound
        private Song _tetrisMusic;

        //Fonts
        private SpriteFont _informationsFont;

        //Gameplay
        private int _points;

        public TetrisGame()
        {
            IsFixedTimeStep = true;
            
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Common.screenWidth;
            graphics.PreferredBackBufferHeight = Common.screenHeight;
            graphics.ApplyChanges();
            graphics.SynchronizeWithVerticalRetrace = false;
            _board = new Board();

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Get window size
            _screenWidth = Window.ClientBounds.Width;
            _screenHeight = Window.ClientBounds.Height;

            _background = Content.Load<Texture2D>(Common.backgroundTexture);

            //Init sound background music
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;

            //Init the current piece
            _currentPiece = new Piece.PieceI(4,0);
            foreach (Block.ABlock block in _currentPiece.Blocks)
            {
                block.Position = new Vector2(Common.boardStartX + (_currentPiece.X_axis + block.X_axis) * Common.blockTextureSize, Common.boardStartY + ((_currentPiece.Y_axis + block.Y_axis)) * Common.blockTextureSize);
                block.LoadContent(Content, block.Texture);
            }
            
            //Init the next piece for preview
            _nextPiece = new Piece.PieceL(0, 0);
            foreach (Block.ABlock block in _nextPiece.Blocks)
            {
                block.Position = new Vector2(Common.previewNextStartX+50 + (_nextPiece.X_axis + block.X_axis) * Common.blockTextureSize, Common.previewNextStartY + ((_nextPiece.Y_axis + block.Y_axis)) * Common.blockTextureSize);
                block.LoadContent(Content, block.Texture);
            }

            _points = 0;

            base.Initialize();
            MediaPlayer.Play(_tetrisMusic);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _tetrisMusic = Content.Load<Song>("Tetris");
            _informationsFont = Content.Load<SpriteFont>("Fonts/Infos");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();

            //Player interactions
            if (_keyboardState.IsKeyDown(Keys.Down) && !_previousKeyboardState.IsKeyDown(Keys.Down))
            {
                if(_board.canMoveDown(_currentPiece))
                    _currentPiece.moveDown(Content);
            }
            else if (_keyboardState.IsKeyDown(Keys.Left) && !_previousKeyboardState.IsKeyDown(Keys.Left))
            {
                if(_board.canMoveLeft(_currentPiece))
                    _currentPiece.moveLeft(Content);
            }   
            else if (_keyboardState.IsKeyDown(Keys.Right) && !_previousKeyboardState.IsKeyDown(Keys.Right))
            {
                if(_board.canMoveRight(_currentPiece))
                    _currentPiece.moveRight(Content);
            }
            else if (_keyboardState.IsKeyDown(Keys.T) && !_previousKeyboardState.IsKeyDown(Keys.T))
            {
                if(_board.canRotate(_currentPiece))
                    _currentPiece.rotate();
            }
            else if (_keyboardState.IsKeyDown(Keys.Escape))
                this.Exit();
            
            else if (_keyboardState.IsKeyDown(Keys.Space) && !_previousKeyboardState.IsKeyDown(Keys.Space))
            {
                _board.addPiece(_currentPiece, Content);
                _board.print();

                //_currentPiece = new Piece.PieceL(4,0);
                this.randomPiece();
                foreach (Block.ABlock block in _currentPiece.Blocks)
                {
                    block.Position = new Vector2(Common.boardStartX + (_currentPiece.X_axis + block.X_axis) * Common.blockTextureSize, Common.boardStartY + ((_currentPiece.Y_axis + block.Y_axis)) * Common.blockTextureSize);
                    block.LoadContent(Content, block.Texture);
                }
                //TODO update the next piece preview
            }
            _previousKeyboardState = _keyboardState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_background, Vector2.Zero, Color.White);
            //DRAW the next piece preview
            foreach (Block.ABlock block in _nextPiece.Blocks)
            {
                block.Draw(spriteBatch, gameTime);
            }

            //DRAW the piece
            foreach(Block.ABlock block in _currentPiece.Blocks)
            {
                block.Draw(spriteBatch, gameTime);
            }
            
            //DRAW the board
            _board.drawBoard(spriteBatch, gameTime);
            
            //DRAW informations
            spriteBatch.DrawString(_informationsFont, + _points + " points", new Vector2(Common.informationsStartX, Common.informationsStartY), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void randomPiece()
        {
            Random rnd = new Random();
            int random = rnd.Next(1, 8); // create number between 1 to 7
            Console.WriteLine("numero de random : " + random);
            if (random == 1)
                _currentPiece = new Piece.PieceI(5, 0);
            else if (random == 2)
                _currentPiece = new Piece.PieceL(5, 0);
            else if (random == 3)
                _currentPiece = new Piece.PieceJ(5, 0);
            else if (random == 4)
                _currentPiece = new Piece.PieceO(5, 0);
            else if (random == 5)
                _currentPiece = new Piece.PieceS(5, 0);
            else if (random == 6)
                _currentPiece = new Piece.PieceT(5, 0);
            else
                _currentPiece = new Piece.PieceZ(5, 0);
        }

    }
}
