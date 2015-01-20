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
            Blocks = new ABlock[10,20];
        }

        public int Width { get; set; }
        public int Height { get; set; }

        public ABlock[,] Blocks { get; set; }

        public void addBlock(Block.ABlock block)
        {
            Blocks[block.X_axis, block.Y_axis] = block;
        }

        private void removeBlock(Block.ABlock block){
            Blocks[block.X_axis, block.Y_axis] = null;
        }

        public void addPiece(Piece.APiece piece, ContentManager content){
            foreach (Block.ABlock block in piece.Blocks)
            {
                block.LoadContent(content, "BlockI");
                this.addBlock(block);
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

        /*
         * Load all the graphic blocks contened by the Board to the Content
         * @param: The ContentManager object
         */
        /*
        public void loadBoard(ContentManager content)
        {
            for(int x=0; x < Blocks.GetUpperBound(0); x++)
            {
                for(int y=0; y < Blocks.GetUpperBound(1); y++)
                {
                    if(Blocks[x,y] != null)
                        Blocks[x, y].LoadContent(content, "BlockI");
                }
            }
        }*/

        public void drawBoard(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int x = 0; x < Blocks.GetUpperBound(0); x++)
            {
                for (int y = 0; y < Blocks.GetUpperBound(1); y++)
                {
                    if (Blocks[x, y] != null)
                        Blocks[x, y].Draw(spriteBatch, gameTime);
                }
            }
        }

    }
}
