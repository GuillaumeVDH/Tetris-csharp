using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Piece
{
    class PieceO : APiece
    {
        public PieceO()
            : base()
        {

        }

        public PieceO(int x, int y)
            : base(x, y)
        {

        }

        protected override Shape.AShape getInitShape()
        {
            return new Shape.ShapeO();
        }

        protected override Block.ABlock createBlock(int x, int y)
        {
            return new Block.BlockO(x, y);
        }
    }
}
