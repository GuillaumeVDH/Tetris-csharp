using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Piece
{
    class PieceJ : APiece
    {
        public PieceJ()
            : base()
        {

        }

        public PieceJ(int x, int y)
            : base(x, y)
        {

        }

        protected override Shape.AShape getInitShape()
        {
            return new Shape.ShapeJ();
        }

        protected override Block.ABlock createBlock(int x, int y)
        {
            return new Block.BlockJ(x, y);
        }
    }
}
