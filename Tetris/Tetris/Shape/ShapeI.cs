﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Shape
{
    class ShapeI : Shape
    {
        protected override void init()
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
