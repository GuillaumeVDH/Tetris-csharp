using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Tetris
{
    class Sprite
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _direction;
        private float _speed;

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 Direction
        {
            get { return _direction; }
            set { _direction = Vector2.Normalize(value); }
        }

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public virtual void Initialize()
        {
            _position = Vector2.Zero;
            _direction = Vector2.Zero;
            _speed = 0;
        }

        //Load the picture
        public virtual void LoadContent(ContentManager content, string assetName)
        {
            _texture = content.Load<Texture2D>(assetName);
        }

        //Update the sprite
        public virtual void Update(GameTime gameTime)
        {
            _position += _direction * _speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        //Handle player inputs
        public virtual void HandleInput(KeyboardState keyboardState, MouseState mouseState, GameTime gameTime)
        {
            //TODO DBG TEST
            if (keyboardState.IsKeyDown(Keys.Up))
                _position.Y--;
            else if (keyboardState.IsKeyDown(Keys.Down))
                _position.Y++;
            else if (keyboardState.IsKeyDown(Keys.Left))
                _position.X--;
            else if (keyboardState.IsKeyDown(Keys.Right))
                _position.X++;
        }

        //Draw sprite
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
