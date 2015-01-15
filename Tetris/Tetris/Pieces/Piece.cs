using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Pieces
{
    class Piece : Sprite
    {
        private int _screenHeight;
        private int _screenWidth;
        private int _type;

        public Piece(Common.PieceType type, int screenWidth, int screenHeight)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
        }

        public override void Initialize()
        {
            base.Initialize();
            Direction = new Vector2(1,1);
            Speed = 0;
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content, string assetName)
        {
            base.LoadContent(content, assetName);
            Position = new Vector2(0,0);
        }

        public override void Update(GameTime gameTime)
        {
            if ( (Position.Y <= 0 && Direction.Y < 0) || (Position.Y > _screenHeight - Texture.Height && Direction.Y > 0) )
            {
                Direction = new Vector2(Direction.X, -Direction.Y);
            }
            if ((Position.X <= 0 && Direction.X < 0) || (Position.X > _screenWidth - Texture.Width && Direction.X > 0))
            {
                Direction = new Vector2(-Direction.X, Direction.Y);
            }
            base.Update(gameTime);
        }

        public int Type
        {
            get { return _type; }
        }

    }
}
