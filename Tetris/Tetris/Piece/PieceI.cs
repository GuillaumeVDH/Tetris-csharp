using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Piece
{
    class PieceI : APiece
    {
        protected override void initShape()
        {
            this.shape = new Shape.ShapeI();
        }

        protected override Block.ABlock createBlock(int x, int y)
        {
            return new Block.BlockI(x, y);
        }
    }
}
