using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Block
{
    class BlockT : ABlock
    {
        public BlockT(int x, int y) : base(x , y)
        {
            Index = 3;
            Texture = Common.blockTextureT;
        }
        public override string Texture { get; set; }
    }
}