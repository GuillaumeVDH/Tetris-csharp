using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    class Common
    {
        public enum PieceType
        {
            pieceL=1,
            pieceO,
        }

        static public int boardStartX = 100;
        static public int boardStartY = 40;
        static public int boardWidth = 160;
        static public int boardHeight = 240;
    }
}
