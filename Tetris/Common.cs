using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public static class Common
    {
        //GRID
        public static int GRID_WIDTH = 10;
        public static int GRID_HEIGHT = 22;
        public static char GRID_EMPTY = '.';

        //GAMEPLAY
        public static int GAME_SPEED = 100;
        public static int POINTS_1_LINE = 40;
        public static int POINTS_2_LINES = 100;
        public static int POINTS_3_LINES = 300;
        public static int POINTS_4_LINES = 1200;

        //PIECES 
        public static int PIECE_WIDTH = 4;
        public static int PIECE_HEIGHT = 4;
        public static char PIECE_FULL = '1';
        public static char PIECE_EMPTY = '0';


        //PIECES TYPE
        public static int PIECE_O = 0;
        public static int PIECE_L = 1;

    }
}
