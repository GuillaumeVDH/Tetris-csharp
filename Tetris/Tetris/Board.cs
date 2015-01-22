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
        public Board()
        {
            Blocks = new ABlock[Common.boardSizeX,Common.boardSizeY];
        }

        public int Width { get; set; }
        public int Height { get; set; }

        public ABlock[,] Blocks { get; set; }

        public void addBlock(Block.ABlock block, int piece_x, int piece_y)
        {
            if ((piece_x + block.X_axis >= 0 && piece_x + block.X_axis <= Common.boardSizeX) && (piece_y + block.Y_axis >= 0 && piece_y + block.Y_axis <= Common.boardSizeY))
                Blocks[piece_x + block.X_axis, piece_y + block.Y_axis] = block;
            else
                throw new Exception("AddBlock - Out of bounds coordinates (x/y):" + (piece_x + block.X_axis) + "/" + (piece_y + block.Y_axis));
        }

        private void removeBlock(Block.ABlock block, int piece_x, int piece_y){
            if ((piece_x + block.X_axis >= 0 && piece_x + block.X_axis <= Common.boardSizeX) && (piece_y + block.Y_axis >= 0 && piece_y + block.Y_axis <= Common.boardSizeY))
                Blocks[piece_x + block.X_axis, piece_y + block.Y_axis] = null;
            else
                throw new Exception("removeBlock - Out of bounds coordinates (x/y):" + (piece_x + block.X_axis) + "/" + (piece_y + block.Y_axis));
        }

        public void addPiece(Piece.APiece piece, ContentManager content) {
            foreach (Block.ABlock block in piece.Blocks)
            {
                try {
                    this.addBlock(block, piece.X_axis, piece.Y_axis);
                    block.Position = new Vector2(Common.boardStartX + (piece.X_axis + block.X_axis) * Common.blockTextureSize, Common.boardStartY + (piece.Y_axis + block.Y_axis) * Common.blockTextureSize);
                    //block.LoadContent(content, block.Texture); //TODO may fucked up things later as piece should already be loaded before being added to board.
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public bool canMoveDown(Piece.APiece piece)
        {
            bool result = true;
            int sum, j = 0, i;
            while (j < 4 && result)
            {
                i = 3;
                sum = 0;
                while (sum == 0 && i >= 0)
                {
                    if (piece.Shape.Shape[i,j] != 0)
                    {
                        sum += piece.Shape.Shape[i, j];
                    }
                    else
                    {
                        i--;
                    }
                    if (sum != 0)
                    {
                        if (Blocks[piece.Y_axis + i + 1, piece.X_axis + j].Index != 0)
                        {
                            result = false;
                        }
                    }
                    j++;
                }
            }   

            return result;
        }

        public void drawBoard(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int x = 0; x < Blocks.GetUpperBound(0); x++)
            {
                for (int y = 0; y < Blocks.GetUpperBound(1); y++)
                {
                    if (Blocks[x, y] != null)
                    {
                        Blocks[x, y].Draw(spriteBatch, gameTime);
                    }
                }
            }
        }

    }
}
