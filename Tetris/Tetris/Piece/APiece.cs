using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Piece
{
    abstract class APiece
    {

        public Shape.AShape Shape {get; set;}

        protected List<Block.ABlock> blocks;
        private int x_axis;

        public int X_axis
        {
            get { return x_axis; }
            set { x_axis = value; }
        }
        private int y_axis;

        public int Y_axis
        {
            get { return y_axis; }
            set { y_axis = value; }
        }

        public APiece()
        {
            Shape = this.getInitShape();
            setBlocks();
            
            X_axis = 0;
            Y_axis = 0;
        }

        public void rotate()
        {
            Shape.rotate();
            setBlocks();
        }

        abstract protected Shape.AShape getInitShape();

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
            Shape.print();
        }

        abstract protected Block.ABlock createBlock(int x, int y);

        protected void setBlocks()
        {
            blocks = new List<Block.ABlock>();
            for (int i = 0; i < Shape.Shape.GetLength(0); i++)
            {
                for (int j = 0; j < Shape.Shape.GetLength(1); j++)
                {
                    if (Shape.Shape[i, j] > 0)
                    {
                        blocks.Add(createBlock(i, j));
                    }
                }
            }
        }

        public List<Block.ABlock> Blocks
        {
            get
            {
                return blocks;
            }
        }
    }
}
