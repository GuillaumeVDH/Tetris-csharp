﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Block
{
    abstract class ABlock : Sprite
    {
        public int Index { get; set; }

        public int X_axis
        {
            get;
            set;
        }

        public int Y_axis
        {
            get;
            set;
        }

        public ABlock(int x, int y)
        {
            this.X_axis = x;
            this.Y_axis = y;
        }
    }
}
