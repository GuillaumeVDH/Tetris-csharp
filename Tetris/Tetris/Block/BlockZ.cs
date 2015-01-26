using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Block
{
    class BlockZ : ABlock
    {
        public BlockZ(int x, int y) : base(x , y)
        {
            Index = 10;
            Texture = Common.blockTextureZ;
        }
        public override string Texture { get; set; }
    }
}