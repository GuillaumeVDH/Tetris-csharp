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
            if ((piece_x + block.X_axis >= 1 && piece_x + block.X_axis <= Common.boardSizeX-1) && (piece_y - block.Y_axis >= 0 && piece_y - block.Y_axis <= Common.boardSizeY - 1))
                Blocks[(piece_y - block.Y_axis), (piece_x + block.X_axis)] = block;
            else
                throw new Exception("AddBlock - Out of bounds coordinates (x/y):" + (piece_x + block.X_axis) + "/" + (piece_y - block.Y_axis));
        }

        private void removeBlock(Block.ABlock block, int piece_x, int piece_y){
            if ((piece_x + block.X_axis >= 0 && piece_x + block.X_axis <= Common.boardSizeX) && (piece_y + block.Y_axis >= 0 && piece_y + block.Y_axis <= Common.boardSizeY))
                Blocks[piece_y - block.Y_axis, piece_x + block.X_axis] = null;
            else
                throw new Exception("removeBlock - Out of bounds coordinates (x/y):" + (piece_x + block.X_axis) + "/" + (piece_y - block.Y_axis));
        }

        public void addPiece(Piece.APiece piece, ContentManager content, TetrisGame tetrisgame) {
            foreach (Block.ABlock block in piece.Blocks)
            {
                try {
                    this.addBlock(block, piece.X_axis, piece.Y_axis);
                    block.Position = new Vector2(Common.boardStartX + ((piece.X_axis + block.X_axis)-1) * Common.blockTextureSize, Common.boardStartY + ((piece.Y_axis - block.Y_axis)-4) * Common.blockTextureSize);
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
            this.deleteFullRows(tetrisgame);
            this.countGameHeight();
        }

        /*
         * This function will analyse the piece passed and check if one of the bottom blocks is next to another
         * Return: It will return true if we can move down, otherwise it will returne false
         */ 
        public bool canMoveDown(Piece.APiece piece)
        {
            bool result = true;
            int sum, i, j = 0;

            while (j < 4 && result)
            {
                i = 3;
                sum = 0;
                while (sum == 0 && i >= 0 && result)
                {
                    if (piece.Shape.Shape[i, j] != 0)  //A block is detected
                    {
                        sum += piece.Shape.Shape[i, j];
                    }
                    else
                    {
                        i--;
                    }
                    if (sum != 0) //If we've detected a block at the previous step, check if there is someone below
                    {
                        if (Blocks[piece.Y_axis - 2 + i, piece.X_axis + j].Index != 0)
                        {
                            //Someone is below us, we can't move down anymore.
                            result = false;
                        }
                    }
                }
                j++;
            }

            return result;
        }

        /*
         * This function will analyse the piece passed and check if one of the right blocks is next to another
         * Return: It will return true if we can do the move, otherwise it will returne false

         */
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
                        if (Blocks[piece.Y_axis - 3 + i, piece.X_axis + j + 1].Index != 0)
                        {
                            result = false;
                        }
                    }
                }
                i++;
            }

            return result;
        }

        /*
         * This function will analyse the piece passed and check if one of the left blocks is next to another
         * Return: It will return true if we can do the move, otherwise it will returne false
         */
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
                        if (Blocks[piece.Y_axis - 3 + i, piece.X_axis + j - 1].Index != 0)
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
                for (int x = 0; x < Blocks.GetUpperBound(1); x++)
                {
                    if (Blocks[y, x] != null && Blocks[y, x].Texture != null)
                    {
                        Blocks[y, x].Draw(spriteBatch, gameTime);
                    }
                }
            }
        }

        /*
         * This function will analyse the piece passed and check if we are able to rotate without collision with other elements
         * Return: It will return true if we can do the move, otherwise it will returne false
         */
        public bool canRotate(Piece.APiece piece)
        {
            Piece.APiece pieceTemp = piece;

            bool result = true;
            pieceTemp.rotate(null);

            foreach(Block.ABlock block in pieceTemp.Blocks ){
                try
                {
                    if (Blocks[pieceTemp.Y_axis - block.Y_axis, pieceTemp.X_axis + block.X_axis].Index != 0)
                    {
                        result = false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            pieceTemp.rotate(null);
            pieceTemp.rotate(null);
            pieceTemp.rotate(null);

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
        }

        private void deleteRows(int y, int nbRows){
            for (int j = 0; j < nbRows; j++)
            {
                for (int i = 1; i < Common.boardSizeX - 1; i++)
                {
                    Vector2 position = new Vector2(Blocks[j + y, i].Position.X, Blocks[j + y, i].Position.Y);
                    Blocks[j + y, i] = new BlockEmpty(0, 0);
                    Blocks[j + y, i].Position = position;
                }
            }
            
           for (int j = y + nbRows - 1; j > nbRows; j--)
           {
                for (int i = 1; i < Common.boardSizeX - 1; i++)
                {
                    Blocks[j, i] = Blocks[j - nbRows, i];
                    Vector2 position = new Vector2(Blocks[j-nbRows, i].Position.X, Blocks[j-nbRows, i].Position.Y+20*nbRows);
                    
                    Blocks[j, i].Position = position;
                }
            }
        }

        public bool isFullRow(int y)
        {
            int sum = 0;
            for (int i = 1; i < Common.boardSizeX - 1; i++)
            {
                if (Blocks[y, i].Index == 0)
                {
                    sum++;
                }
            }

            return (sum == 0);
        }

        public void deleteFullRows(TetrisGame tetrisgame)
        {
            int j = 0, y, nbRows = 0;
            while (j < Common.boardSizeY - 1)
            {
                if (isFullRow(j))
                {
                    y = j;
                    nbRows++;
                    while (j != Common.boardSizeY - 2 && isFullRow(j + 1))
                    {
                        nbRows++;
                        j++;
                    }
                    deleteRows(y, nbRows);
                }
                j++;
            }
            tetrisgame.scoreCounter(nbRows);
        }
    }
}
