﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Piece
{
    class PieceO : APiece
    {
        protected override void initShape()
        {
            this.shape = new Shape.ShapeO();
        }

        protected override Block.ABlock createBlock(int x, int y)
        {
            return new Block.BlockI(x, y);
        }
    }
}
