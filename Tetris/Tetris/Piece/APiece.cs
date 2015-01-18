﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Piece
{
    abstract class APiece
    {
        private Shape.AShape shape;

        protected Shape.AShape Shape
        {
            get { return shape; }
            set { shape = value; }
        }
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
            initShape();
            setBlocks();
            //init the piece outside the board
            X_axis = 4;
            Y_axis = -3;
        }

        public void rotate()
        {
            this.shape.rotate();
            setBlocks();
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

        protected void setBlocks()
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

        public List<Block.ABlock> Blocks
        {
            get
            {
                return blocks;
            }
        }
    }
}