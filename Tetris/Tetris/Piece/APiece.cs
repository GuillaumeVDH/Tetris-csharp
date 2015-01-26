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

        public List<Block.ABlock> Blocks {get; set;}

        public int X_axis { get; set; }
        public int Y_axis { get; set; }

        protected APiece()
        {
            Shape = this.getInitShape();
            setBlocks();
            
            X_axis = 0;
            Y_axis = 0;
        }

        protected APiece(int x, int y) : this()
        {
            Shape = this.getInitShape();
            setBlocks();
            
            X_axis = x;
            Y_axis = y;
        }

        public void rotate()
        {
            Shape.rotate();
            setBlocks();
            print();
        }

        abstract protected Shape.AShape getInitShape();

        public void moveRight(ContentManager content)
        {
            X_axis += 1;
            this.updatePiece(content);
        }

        public void moveLeft(ContentManager content)
        {
            X_axis -= 1;
            this.updatePiece(content);
        }

        public void moveDown(ContentManager content)
        {
            this.Y_axis += 1;
            this.updatePiece(content);
        }

        private void updatePiece(ContentManager content)
        {
            Console.WriteLine("UP:" + (this.X_axis) + "/" + (this.Y_axis));
            foreach (Block.ABlock block in this.Blocks)
            {
                block.Position = new Vector2(Common.boardStartX + (this.X_axis + block.X_axis) * Common.blockTextureSize, Common.boardStartY + ((this.Y_axis + block.Y_axis)) * Common.blockTextureSize);
            }
        }

        public void print(){
            Shape.print();
        }

        abstract protected Block.ABlock createBlock(int x, int y);

        protected void setBlocks()
        {
            Blocks = new List<Block.ABlock>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Shape.Shape[i, j] > 0)
                    {
                        Blocks.Add(createBlock(j, i));
                    }
                }
            }
        }
    }
}
