using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Shape
{
    class ShapeL : AShape
    {
        protected override void init()
        {
            this.shape = new int[,]
                {
                    {0,0,0,0},
                    {5,0,0,0},
                    {5,0,0,0},
                    {5,5,0,0}
                };
        }
    }
}
