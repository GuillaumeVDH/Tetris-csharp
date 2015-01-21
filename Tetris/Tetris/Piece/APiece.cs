using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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

        public void moveRight(ContentManager content)
        {
            this.x_axis += 1;
            this.updatePiece(content);
        }

        public void moveLeft(ContentManager content)
        {
            this.x_axis -= 1;
            this.updatePiece(content);
        }

        public void moveDown(ContentManager content)
        {
            this.y_axis += 1;
            this.updatePiece(content);
        }

        private void updatePiece(ContentManager content)
        {
            foreach (Block.ABlock block in this.Blocks)
            {
                block.Position = new Vector2((this.X_axis + block.X_axis)*Common.blockTextureSize, (this.Y_axis + block.Y_axis)*Common.blockTextureSize);
            }
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
