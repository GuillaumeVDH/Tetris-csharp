using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Shape
{
    class ShapeZ : AShape
    {
        protected override void init()
        {
            this.Shape = new int[,]
                {
                    {0,0,0,0},
                    {0,7,7,0},
                    {0,0,7,7},
                    {0,0,0,0}
                };
        }
    }
}
