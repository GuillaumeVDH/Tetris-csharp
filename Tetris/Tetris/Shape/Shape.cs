using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Shape
{
    abstract class Shape
    {
        int[,] shape;

        public Shape(){
            this.shape = new int[4,4];
        }
    }
}
