using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Shape
{
    class ShapeT : AShape
    {
        protected override void init()
        {
            this.shape = new int[,]
                {
                    {0,0,0,0},
                    {3,3,3,0},
                    {0,3,0,0},
                    {0,0,0,0}
                };
        }
    }
}
