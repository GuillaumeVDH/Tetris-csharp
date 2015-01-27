using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Block
{
    class BlockO : ABlock
    {
        public BlockO(int x, int y) : base(x , y)
        {
            Index = 2;
            Texture = Common.blockTextureO;
        }
        public override string Texture { get; set; }
    }
}