using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Piece
{
    class PieceZ : APiece
    {
        public PieceZ()
            : base()
        {

        }

        public PieceZ(int x, int y)
            : base(x, y)
        {

        }

        protected override Shape.AShape getInitShape()
        {
            return new Shape.ShapeZ();
        }

        protected override Block.ABlock createBlock(int x, int y)
        {
            return new Block.BlockZ(x, y);
        }
    }
}
