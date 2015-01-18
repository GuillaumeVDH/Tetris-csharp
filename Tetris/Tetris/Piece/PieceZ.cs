using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Piece
{
    class PieceZ : APiece
    {
        protected override void initShape()
        {
            this.shape = new Shape.ShapeZ();
        }
    }
}
