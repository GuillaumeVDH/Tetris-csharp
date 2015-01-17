using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Shape
{
    class ShapeI : Shape
    {
        protected override void init()
        {
            for (int i = 0; i < this.shape.GetLength(0); i++)
            {
                for (int j = 0; j < this.shape.GetLength(1); j++)
                {
                    this.shape = new int[,]
                    {
                        {4,0,0,0},
                        {4,0,0,0},
                        {4,0,0,0},
                        {4,0,0,0}
                    };
                }
            }
        }
    }
}
