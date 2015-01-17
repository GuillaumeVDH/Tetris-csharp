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
        private Pieces.Piece _pieceL;
        private Pieces.Piece _pieceO;
        private Shape.ShapeT _shapeT;

        //Player
        private KeyboardState _keyboardState;
        private MouseState _mouseState;

        //Screen
        private int _screenHeight;
        private int _screenWidth;

        Color[] pieceLTextureData;
        Color[] pieceOTextureData;


        private bool _collision = false;

        public TetrisGame()
        {
            IsFixedTimeStep = false;
            
            graphics = new GraphicsDeviceManager(this);
            graphics.SynchronizeWithVerticalRetrace = false;

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
            // TODO: Add your initialization logic here
            //Get window size
            _screenWidth = Window.ClientBounds.Width;
            _screenHeight = Window.ClientBounds.Height;

            //Init attributs
            _pieceL = new Pieces.Piece(Common.PieceType.pieceL ,_screenWidth, _screenHeight);
            _pieceL.Initialize();
            _pieceO = new Pieces.Piece(Common.PieceType.pieceO,_screenWidth, _screenHeight);
            _pieceO.Initialize();

            _shapeT = new Shape.ShapeT();
            _shapeT.rotate();
            _shapeT.rotate();
            _shapeT.rotate();
            _shapeT.rotate();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            _pieceL.LoadContent(Content, "pieces/L");
            _pieceO.LoadContent(Content, "pieces/O");

            _pieceO.Position = new Vector2(40, 80);

            // Extract collision data
            pieceLTextureData = new Color[_pieceL.Texture.Width * _pieceL.Texture.Height];
            _pieceL.Texture.GetData(pieceLTextureData);

            pieceOTextureData = new Color[_pieceO.Texture.Width * _pieceO.Texture.Height];
            _pieceO.Texture.GetData(pieceOTextureData);

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
            // Get the bounding rectangle of the L
            Rectangle pieceLRectangle =
                new Rectangle((int)_pieceL.Position.X, (int)_pieceL.Position.Y,
                _pieceL.Texture.Width, _pieceL.Texture.Height);

            // Get the bounding rectangle of the O
            Rectangle pieceORectangle =
                    new Rectangle((int)_pieceO.Position.X, (int)_pieceO.Position.Y,
                    _pieceO.Texture.Width, _pieceO.Texture.Height);

            if (IntersectPixels(pieceLRectangle, pieceLTextureData,
                                   pieceORectangle, pieceOTextureData))
            {
                _collision = true;
            }
            else
                _collision = false;

            _pieceL.HandleInput(_keyboardState, _mouseState, gameTime);
            _pieceL.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if(_collision)
             GraphicsDevice.Clear(Color.Pink);
            else
                GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            _pieceL.Draw(spriteBatch, gameTime);
            _pieceO.Draw(spriteBatch, gameTime);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }




        /// <summary>
        /// Determines if there is overlap of the non-transparent pixels
        /// between two sprites.
        /// </summary>
        /// <param name="rectangleA">Bounding rectangle of the first sprite</param>
        /// <param name="dataA">Pixel data of the first sprite</param>
        /// <param name="rectangleB">Bouding rectangle of the second sprite</param>
        /// <param name="dataB">Pixel data of the second sprite</param>
        /// <returns>True if non-transparent pixels overlap; false otherwise</returns>
        static bool IntersectPixels(Rectangle rectangleA, Color[] dataA,
                                    Rectangle rectangleB, Color[] dataB)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) +
                                         (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) +
                                         (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            // No intersection found
            return false;
        }

    }
}
