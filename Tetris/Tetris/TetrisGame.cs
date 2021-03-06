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

        //Screen
        private int _screenHeight;
        private int _screenWidth;

        //Sound
        private Song _tetrisMusic;

        //Fonts
        private SpriteFont _informationsFont;
        private SpriteFont _gameOverFont;

        //Gameplay
        private int _timer;
        private int _points;
        private int _level;
        private bool _isAGoToGoDown;
        private bool _gameOver;

        //random
        private static Random rnd = new Random();

        public TetrisGame()
        {
            IsFixedTimeStep = true;
            
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Common.screenWidth;
            graphics.PreferredBackBufferHeight = Common.screenHeight;
            graphics.ApplyChanges();
            graphics.SynchronizeWithVerticalRetrace = false;
            _board = new Board();

            _timer = 1000;
            _isAGoToGoDown = false;
            _gameOver = false;

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
            MediaPlayer.Volume = 0.8f;

            //Gameplay
            _points = 0;
            _level = 1;

            //Init the current piece
            this.randomPiece(); //First, create a "_nextPiece" using the random
            _currentPiece = _nextPiece;
            _currentPiece.X_axis = 5;
            _currentPiece.Y_axis = 3;
            foreach (Block.ABlock block in _currentPiece.Blocks)
            {
                block.Position = new Vector2(Common.boardStartX + ((_currentPiece.X_axis + block.X_axis)-1) * Common.blockTextureSize, Common.boardStartY + ((_currentPiece.Y_axis - block.Y_axis)-4) * Common.blockTextureSize);
                block.LoadContent(Content, block.Texture);
            }
            
            //Init the next piece for preview
            this.randomPiece();
            foreach (Block.ABlock block in _nextPiece.Blocks)
            {
                block.Position = new Vector2(Common.previewNextStartX + 60 + ((_nextPiece.X_axis + block.X_axis) - 1) * Common.blockTextureSize, Common.previewNextStartY + 140 + ((_nextPiece.Y_axis - block.Y_axis) - 4) * Common.blockTextureSize);
                block.LoadContent(Content, block.Texture);
            }

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
            //Load the theme song
            _tetrisMusic = Content.Load<Song>("Tetris");
            //Load the font used to draw text on the screen
            _informationsFont = Content.Load<SpriteFont>("Fonts/Infos");
            _gameOverFont = Content.Load<SpriteFont>("Fonts/GameOver");
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

            if (_board.GameHeight < 20)
            {
                this.moveDown(gameTime, Content);

                //Player interactions
                if (_keyboardState.IsKeyDown(Keys.Down) && !_previousKeyboardState.IsKeyDown(Keys.Down))
                {
                    if (_board.canRotate(_currentPiece))
                    {
                        _currentPiece.rotate(Content);
                    }
                    foreach (Block.ABlock block in _currentPiece.Blocks)
                    {
                        block.Position = new Vector2(Common.boardStartX + ((_currentPiece.X_axis + block.X_axis) - 1) * Common.blockTextureSize, Common.boardStartY + ((_currentPiece.Y_axis - block.Y_axis) - 4) * Common.blockTextureSize);
                        block.LoadContent(Content, block.Texture);
                    }
                }
                else if (_keyboardState.IsKeyDown(Keys.Left) && !_previousKeyboardState.IsKeyDown(Keys.Left))
                {
                    if (_board.canMoveLeft(_currentPiece))
                        _currentPiece.moveLeft(Content);
                }
                else if (_keyboardState.IsKeyDown(Keys.Right) && !_previousKeyboardState.IsKeyDown(Keys.Right))
                {
                    if (_board.canMoveRight(_currentPiece))
                        _currentPiece.moveRight(Content);
                }
                else if (_keyboardState.IsKeyDown(Keys.Space) && !_previousKeyboardState.IsKeyDown(Keys.Space))
                {
                    _isAGoToGoDown = true;
                    this.moveDown(gameTime, Content);
                }
            }
            else
            {
                _gameOver = true;
            }
            if (_keyboardState.IsKeyDown(Keys.Escape))
              this.Exit();
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

            //DRAW the piece (but only if not hidden)
            foreach(Block.ABlock block in _currentPiece.Blocks)
            {
                if(_currentPiece.Y_axis - block.Y_axis >= 4)
                    block.Draw(spriteBatch, gameTime);
            }

            //DRAW the board
            _board.drawBoard(spriteBatch, gameTime);
            
            //DRAW informations
            spriteBatch.DrawString(_informationsFont, + _points + " points", new Vector2(Common.informationsStartX, Common.informationsStartY), Color.White);
            spriteBatch.DrawString(_informationsFont, _level.ToString(), new Vector2(Common.informationsStartX + 140, Common.informationsStartY + 50), Color.White);
            spriteBatch.DrawString(_informationsFont, _board.GameHeight.ToString(), new Vector2(Common.informationsStartX+140, Common.informationsStartY+100), Color.White);
            if(_gameOver)
                spriteBatch.DrawString(_gameOverFont, "GAME OVER", new Vector2(20, 200), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /*
         * Renew _nextPiece with a total random new piece
         */
        public void randomPiece()
        {
            int random = rnd.Next(1, 8);
            if (random == 1)
                _nextPiece = new Piece.PieceI(0, 0);
            else if (random == 2)
                _nextPiece = new Piece.PieceL(0, 0);
            else if (random == 3)
                _nextPiece = new Piece.PieceJ(0, 0);
            else if (random == 4)
                _nextPiece = new Piece.PieceO(0, 0);
            else if (random == 5)
                _nextPiece = new Piece.PieceS(0, 0);
            else if (random == 6)
                _nextPiece = new Piece.PieceT(0, 0);
            else
                _nextPiece = new Piece.PieceZ(0, 0);
        }

        public void scoreCounter(int lineDelete)
        {
            switch (lineDelete)
            {
                case 1:
                    _points = _points + (40 * (_level + 1));
                    break;
                case 2:
                    _points = _points + (100 * (_level + 1));
                    break;
                case 3:
                    _points = _points + (300 * (_level + 1));
                    break;
                case 4:
                    _points = _points + (1200 * (_level + 1));
                    break;
            }
        }

        private void moveDown(GameTime gameTime, ContentManager Content)
        {
            if (_isAGoToGoDown)
            {
                while (_board.canMoveDown(_currentPiece))
                {
                    _currentPiece.moveDown(Content);
                }
                _isAGoToGoDown = false;
        }
            else
            {
                double elapsed = gameTime.ElapsedGameTime.TotalMilliseconds;
                _timer -= (int)elapsed;

                if (_timer <= 0)
                {
                    _timer = 1000;   //Reset Timer
                    if (_board.canMoveDown(_currentPiece))
                        _currentPiece.moveDown(Content);
                    else
                        this.addPieceToBoard(Content);
                }
            }
        }

        private void addPieceToBoard(ContentManager Content)
        {
            //Add the piece to the board
            _board.addPiece(_currentPiece, Content, this);
            _board.print(); //TODO DEBUG ONLY (Logical view, only used for collisions checks)

            //Updating the player piece to be equal as the preview windows and set up center & outside of the view
            _currentPiece = _nextPiece;
            _currentPiece.X_axis = 5;
            _currentPiece.Y_axis = 3;
            foreach (Block.ABlock block in _currentPiece.Blocks)
            {
                block.Position = new Vector2(Common.boardStartX + ((_currentPiece.X_axis + block.X_axis) - 1) * Common.blockTextureSize, Common.boardStartY + ((_currentPiece.Y_axis - block.Y_axis) - 3) * Common.blockTextureSize);
                block.LoadContent(Content, block.Texture);
            }

            //Switching for a brand new next piece!
            this.randomPiece();
            foreach (Block.ABlock block in _nextPiece.Blocks)
            {
                block.Position = new Vector2(Common.previewNextStartX + 60 + ((_nextPiece.X_axis + block.X_axis) - 1) * Common.blockTextureSize, Common.previewNextStartY + 120 + ((_nextPiece.Y_axis - block.Y_axis) - 3) * Common.blockTextureSize);
                block.LoadContent(Content, block.Texture);
            }
        }
    }
}
