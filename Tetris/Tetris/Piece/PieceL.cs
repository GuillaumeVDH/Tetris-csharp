using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Piece
{
    class PieceL : APiece
    {
        public PieceL()
            : base()
        {

        }

        public PieceL(int x, int y)
            : base(x, y)
        {

        }

        protected override Shape.AShape getInitShape()
        {
            return new Shape.ShapeL();
        }

        protected override Block.ABlock createBlock(int x, int y)
        {
            return new Block.BlockL(x, y);
        }
    }
}
