using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Block
{
    class BlockEmpty : ABlock
    {
        public BlockEmpty(int x, int y) : base(x , y)
        {
            Index = 0;
            Texture = null;
        }

        public override string Texture { get; set; }
    }
}
