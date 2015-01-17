using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Piece
{
    class PieceT : Piece
    {
        protected override void initShape()
        {
            this.shape = new Shape.ShapeT();
        }
    }
}
