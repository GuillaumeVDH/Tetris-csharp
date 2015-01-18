using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Block
{
    abstract class ABlock : Sprite
    {

        private int x_axis;

        public int X_axis
        {
            get { return x_axis; }
            set { x_axis = value; }
        }
        private int y_axis;

        public int Y_axis
        {
            get { return y_axis; }
            set { y_axis = value; }
        }

        public ABlock(int x, int y)
        {
            x_axis = x;
            y_axis = y;
        }
    }
}
