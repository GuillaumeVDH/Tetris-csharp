using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    class Common
    {
        static public int screenWidth = 600;
        static public int screenHeight = 600;
        static public int boardStartX = 60; //The start position - in pixel - of the board for the Y axis
        static public int boardStartY = 60; //The start position - in pixel - of the board for the Y axis
        static public int boardWidth = 200; //The width - in pixel - of the board for the Y axis
        static public int boardHeight = 480; //The height - in pixel - of the board for the Y axis
        static public int boardSizeX = 12; //The size - in case - of the board for the X axis
        static public int boardSizeY = 25; //The size - in case - of the board for the Y axis
        
        static public int blockTextureSize = 20; //The size - in pixel - of a block texture (X == Y)

        //Texture full path for each block
        static public string blockTextureI = "Blocks/BlockI";
        static public string blockTextureJ = "Blocks/BlockJ";
        static public string blockTextureL = "Blocks/BlockL";
        static public string blockTextureO = "Blocks/BlockO";
        static public string blockTextureS = "Blocks/BlockS";
        static public string blockTextureT = "Blocks/BlockT";
        static public string blockTextureZ = "Blocks/BlockZ";

        static public string backgroundTexture = "background"; 
    }
}
