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
        static public int boardWidth = 604;
        static public int boardHeight = 831;
        
        static public int blockTextureSize = 10;

        static public string blockTextureI = "Blocks/BlockI";
        static public string blockTextureJ = "Blocks/BlockJ";
        static public string blockTextureL = "Blocks/BlockL";
        static public string blockTextureO = "Blocks/BlockO";
        static public string blockTextureS = "Blocks/BlockS";
        static public string blockTextureT = "Blocks/BlockT";
        static public string blockTextureZ = "Blocks/BlockZ";



    }
}
