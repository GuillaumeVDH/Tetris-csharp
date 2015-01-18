using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Shape
{
    class ShapeO : AShape
    {
        protected override void init()
        {
            this.shape = new int[,]
                {
                    {0,0,0,0},
                    {0,2,2,0},
                    {0,2,2,0},
                    {0,0,0,0}
                };
        }
    }
}
