using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tetris.Block;

namespace Tetris
{
    class Board
    {
        public int GameHeight { get; set; }

        public Board()
        {
            GameHeight = 0;
            Width = Common.boardSizeX;
            Height = Common.boardSizeY;
            Blocks = new ABlock[Height,Width];
            reset();
        }

        public void reset()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (j == 0 || j == Width - 1 || i == Height - 1)
                    {
                        Blocks[i, j] = new BlockFixed(i, j);
                    }
                    else
                    {
                        Blocks[i, j] = new BlockEmpty(i, j);
                    }
                }
            }
        }

        public int Width { get; set; }
        public int Height { get; set; }

        public ABlock[,] Blocks { get; set; }

        private void addBlock(Block.ABlock block, int piece_x, int piece_y)
        {
            if ((piece_x + block.X_axis >= 0 && piece_x + block.X_axis <= Common.boardSizeX-1) && (piece_y + block.Y_axis >= 0 && piece_y + block.Y_axis <= Common.boardSizeY - 1))
                Blocks[(piece_y + block.Y_axis)+4, (piece_x + block.X_axis)+1] = block;
            else
                throw new Exception("AddBlock - Out of bounds coordinates (x/y):" + (piece_x + block.X_axis) + "/" + (piece_y + block.Y_axis));
        }

        private void removeBlock(Block.ABlock block, int piece_x, int piece_y){
            if ((piece_x + block.X_axis >= 0 && piece_x + block.X_axis <= Common.boardSizeX) && (piece_y + block.Y_axis >= 0 && piece_y + block.Y_axis <= Common.boardSizeY))
                Blocks[piece_y + block.Y_axis, piece_x + block.X_axis] = null;
            else
                throw new Exception("removeBlock - Out of bounds coordinates (x/y):" + (piece_x + block.X_axis) + "/" + (piece_y + block.Y_axis));
        }

        public void addPiece(Piece.APiece piece, ContentManager content) {
            Console.WriteLine("AP:" + piece.X_axis + "/" + piece.Y_axis);
            foreach (Block.ABlock block in piece.Blocks)
            {
                try {
                    this.addBlock(block, piece.X_axis, piece.Y_axis);
                    block.Position = new Vector2(Common.boardStartX + ((piece.X_axis + block.X_axis)) * Common.blockTextureSize, Common.boardStartY + ((piece.Y_axis + block.Y_axis)) * Common.blockTextureSize);
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
            this.countGameHeight();
        }

        public bool canMoveDown(Piece.APiece piece)
        {
            piece.print();
            bool result = true;
            int sum, i = 0, j;

            while (i < 4 && result)
            {
                j = 3;
                sum = 0;
                while (sum == 0 && j >= 0 && result && i < 4) //Added i<4 as it grow out of the array limits
                {
                    Console.WriteLine("i/j: " + i + j);
                    
                    if (piece.Shape.Shape[i,j] != 0)
                    {
                        sum += piece.Shape.Shape[i, j];
                    }
                    else
                    {
                        j--;
                    }
                    if (sum != 0)
                    {
                        Console.WriteLine("Actual:" + (piece.Y_axis + 4 +  i) + "/" + (piece.X_axis + j));
                        Console.WriteLine("Bottom:" + (piece.Y_axis + 4 + i+1) + "/" + (piece.X_axis + j));
                        if (Blocks[piece.Y_axis +5 + i, piece.X_axis + j+1].Index != 0)
                        {
                            result = false;
                        }
                    }
                    i++;
                }
            }   

            return result;
        }

        public bool canMoveRight(Piece.APiece piece)
        {
            bool result = true;
            int sum, i = 0, j;

            while(i < 4 && result)
            {
                j = 3;
                sum = 0;

                while (sum == 0 && j >= 0 && result)
                {
                    if (piece.Shape.Shape[i, j] != 0)
                    {
                        sum += piece.Shape.Shape[i, j];
                    }
                    else
                    {
                        j--;
                    }
                    if (sum != 0)
                    {
                        if (Blocks[piece.Y_axis+4 + i, piece.X_axis + j + 2].Index != 0)
                        {
                            result = false;
                        }
                    }
                }
                i++;
            }

            return result;
        }

        public bool canMoveLeft(Piece.APiece piece){
            bool result = true;
            int sum, i = 0, j;

            while (i < 4 && result)
            {
                j = 0;
                sum = 0;

                while (sum == 0 && j < 4 && result)
                {
                    if (piece.Shape.Shape[i, j] != 0)
                    {
                        sum += piece.Shape.Shape[i, j];
                    }
                    else
                    {
                        j++;
                    }
                    if (sum != 0)
                    {
                        if (Blocks[piece.Y_axis+4 + i, piece.X_axis + j].Index != 0)
                        {
                            result = false;
                        }
                    }
                }
                i++;
            }

            return result;
        }

        public void drawBoard(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int y = 4; y < Blocks.GetUpperBound(0); y++)
            {
                for (int x = 1; x < Blocks.GetUpperBound(1); x++)
                {
                    if (Blocks[y, x] != null && Blocks[y, x].Texture != null)
                    {
                        Blocks[y, x].Draw(spriteBatch, gameTime);
                    }
                }
            }
        }

        public bool canRotate(Piece.APiece piece)
        {
            Piece.APiece pieceTemp = piece;
            bool result = true;

            pieceTemp.rotate();
            foreach(Block.ABlock block in pieceTemp.Blocks ){
                if (Blocks[pieceTemp.Y_axis + block.Y_axis, pieceTemp.X_axis + block.X_axis].Index != 0)
                {
                    result = false;
                }
            }

            return result;
        }
        public void print()
        {
            Utils.TabUtils.print<Block.ABlock>(Blocks);
        }

        private void countGameHeight()
        {
            int calculatedHeight = 0;
            for (int y = 4; y < Blocks.GetUpperBound(0); y++)
            {
                for (int x = 1; x < Blocks.GetUpperBound(1); x++)
                {
                    if(Blocks[y,x].Index != 0)
                    {
                        calculatedHeight = Common.boardSizeY - (y + 1);
                        if (calculatedHeight >= GameHeight)
                            GameHeight = calculatedHeight;
                    }
                }
            }
            Console.WriteLine("res " + GameHeight);

        }

    }
}
