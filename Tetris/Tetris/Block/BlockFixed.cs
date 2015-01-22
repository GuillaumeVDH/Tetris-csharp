using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Block
{
    class BlockFixed : ABlock
    {
        public BlockFixed(int x, int y) : base(x , y)
        {
            Index = 1;
        }

        public override string Texture { get; set; }
    }
}
