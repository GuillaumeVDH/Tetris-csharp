using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Piece
{
    class PieceT : APiece
    {
        protected override void initShape()
        {
            Shape = new Shape.ShapeT();
        }

        protected override Block.ABlock createBlock(int x, int y)
        {
            return new Block.BlockI(x, y);
        }
    }
}
