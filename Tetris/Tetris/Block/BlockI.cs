using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Block
{
    class BlockI : ABlock
    {
        public BlockI(int x, int y) : base(x , y)
        {
            Index = 4;
            Texture = Common.blockTextureI;
        }

        public override string Texture { get; set; }
    }
}
