using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Piece
{
    class PieceT : APiece
    {
        public PieceT()
            : base()
        {

        }

        public PieceT(int x, int y)
            : base(x, y)
        {

        }

        protected override Shape.AShape getInitShape()
        {
            return new Shape.ShapeT();
        }

        protected override Block.ABlock createBlock(int x, int y)
        {
            return new Block.BlockT(x, y);
        }
    }
}
