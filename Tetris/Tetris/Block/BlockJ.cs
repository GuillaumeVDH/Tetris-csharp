using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Block
{
    class BlockJ : ABlock
    {
        public BlockJ(int x, int y) : base(x , y)
        {
            Index = 6;
            Texture = Common.blockTextureJ;
        }
        public override string Texture { get; set; }
    }
}