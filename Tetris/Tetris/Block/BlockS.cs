using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Block
{
    class BlockS : ABlock
    {
        public BlockS(int x, int y) : base(x , y)
        {
            Index = 8;
            Texture = Common.blockTextureS;
        }
        public override string Texture { get; set; }
    }
}