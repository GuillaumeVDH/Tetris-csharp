using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Shape
{
    class ShapeS : AShape
    {
        protected override void init()
        {
            this.shape = new int[,]
                {
                    {0,0,0,0},
                    {0,8,8,0},
                    {8,8,0,0},
                    {0,0,0,0}
                };
        }
    }
}
