using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Shape
{
    class ShapeJ : AShape
    {
        protected override void init()
        {

            this.Shape = new int[,]
                {
                    {0,0,0,0},
                    {0,6,0,0},
                    {0,6,0,0},
                    {6,6,0,0}
                };
        }
    }
}
