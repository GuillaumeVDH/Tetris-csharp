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
        private Piece.PieceI _piece;

        //Board
        Board _board;

        //Player
        private KeyboardState _keyboardState;
        private MouseState _mouseState;

        //Screen
        private int _screenHeight;
        private int _screenWidth;

        public TetrisGame()
        {
            IsFixedTimeStep = false;
            
            graphics = new GraphicsDeviceManager(this);
            graphics.SynchronizeWithVerticalRetrace = false;
            _board = new Board();
            _board.print();

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

            //Test PIECE/SHAPE & BLOCK
            _piece = new Piece.PieceI();
            foreach (Block.ABlock block in _piece.Blocks)
            {
                block.Position = new Vector2(Common.boardStartX + _piece.X_axis + block.X_axis * Common.blockTextureSize, Common.boardStartY + _piece.Y_axis + block.Y_axis * Common.blockTextureSize);
                block.LoadContent(Content, block.Texture);
            }

            //TEST BOARD
            Piece.APiece piece1;
            piece1 = new Piece.PieceI();
            piece1.print();
            piece1.X_axis = 0;
            piece1.Y_axis = 0;
            //piece1.X_axis = Common.boardStartX+0*Common.blockTextureSize;
            //piece1.Y_axis = Common.boardStartY+0*Common.blockTextureSize;
            foreach (Block.ABlock block in piece1.Blocks)
            {
                block.Position = new Vector2(Common.boardStartX + piece1.X_axis + block.X_axis * Common.blockTextureSize, Common.boardStartY + piece1.Y_axis + block.Y_axis * Common.blockTextureSize);
                block.LoadContent(Content, block.Texture);
            }

            Piece.APiece piece2;
            piece2 = new Piece.PieceI();
            piece2.X_axis = 2;
            piece2.Y_axis = 1;
            foreach (Block.ABlock block in piece2.Blocks)
            {
                block.Position = new Vector2(Common.boardStartX + piece2.X_axis + block.X_axis * Common.blockTextureSize, Common.boardStartY + piece2.Y_axis + block.Y_axis * Common.blockTextureSize);
                block.LoadContent(Content, block.Texture);
            }
            _board.addPiece(piece1, Content);
            _board.addPiece(piece2, Content);
            
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
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
            // TODO: Add your update logic 
            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();

            // Allows the game to exit
            if (_keyboardState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            
            //DRAW the piece
            foreach(Block.ABlock block in _piece.Blocks)
            {
                //block.Draw(spriteBatch, gameTime);
            }

            //DRAW the board
            _board.drawBoard(spriteBatch, gameTime);

            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
