using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    class Board
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Block.ABlock[,] Blocks { get; set; }

        public void addBlock(Block.ABlock block)
        {
            Blocks[block.X_axis, block.Y_axis] = block;
        }

        private void removeBlock(Block.ABlock block){
            Blocks[block.X_axis, block.Y_axis] = null;
        }

        public void addPiece(Piece.APiece piece){
            foreach (Block.ABlock block in piece.Blocks)
            {
                addBlock(block);
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

    }
}
