using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Piece
{
    class PieceS : APiece
    {
        public PieceS()
            : base()
        {

        }

        public PieceS(int x, int y)
            : base(x, y)
        {

        }

        protected override Shape.AShape getInitShape()
        {
            return new Shape.ShapeS();
        }

        protected override Block.ABlock createBlock(int x, int y)
        {
            return new Block.BlockS(x, y);
        }
    }
}
