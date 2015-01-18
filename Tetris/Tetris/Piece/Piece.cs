using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Piece
{
    abstract class APiece
    {
        protected Shape.AShape shape;
        protected List<Block.ABlock> blocks;
        private int x_axis;
        private int y_axis;

        public APiece()
        {
            this.initShape();
            //init the piece outside the board
            this.x_axis = 4;
            this.y_axis = -3;
        }

        public void rotate()
        {
            this.shape.rotate();
        }

        abstract protected void initShape();

        public void moveRight()
        {
            this.x_axis += 1;
        }

        public void moveLeft()
        {
            this.x_axis -= 1;
        }

        public void meveDown()
        {
            this.y_axis += 1;
        }

        public void print(){
            this.shape.print();
        }

        abstract protected Block.ABlock createBlock(int x, int y);

        public List<Block.ABlock> Blocks
        {
            get
            {
                return blocks;
            }
            set
            {
                blocks = new List<Block.ABlock>();
                for (int i = 0; i < shape.Shape.GetLength(0); i++)
                {
                    for (int j = 0; j < shape.Shape.GetLength(1); j++)
                    {
                        if (shape.Shape[i, j] > 0)
                        {
                            blocks.Add(createBlock(i, j));
                        }
                    }
                }
            }
        }
    }
}
