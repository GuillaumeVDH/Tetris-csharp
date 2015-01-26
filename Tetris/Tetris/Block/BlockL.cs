using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Block
{
    class BlockL : ABlock
    {
        public BlockL(int x, int y) : base(x , y)
        {
            Index = 6;
            Texture = Common.blockTextureL;
        }
        public override string Texture { get; set; }
    }
}