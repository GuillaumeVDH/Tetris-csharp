using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Grid
    {
        private char[,] m_grid = new char[Common.GRID_WIDTH,Common.GRID_HEIGHT]; //TODO @GuillaumeVDH: May be changed to [12,24]. I have to check something
        
        public int Score{ get; set; }
        public int Height { get; set; }
        public int TotalLines { get; set; }

        public void initGrid(char caracter)
        {
            Score = 0;
            Height = 0;
            TotalLines = 0;
            if (m_grid.Rank == 2)
            {
                for (int x = m_grid.GetLowerBound(0); x <= m_grid.GetUpperBound(0); x++)
                    for (int y = m_grid.GetLowerBound(1); y <= m_grid.GetUpperBound(1); y++)
                        m_grid.SetValue(caracter, x, y);
            }
            else
                throw new System.ArrayTypeMismatchException("Error, the grid is not properly readable.");
        }

        public void showGrid()
        {
            for (int y = m_grid.GetLowerBound(1); y <= m_grid.GetUpperBound(1); y++)
            {
                Console.Write("|");
                for (int x = m_grid.GetLowerBound(0); x <= m_grid.GetUpperBound(0); x++)
                {
                    Console.Write(m_grid.GetValue(x, y));
                }
                Console.Write("| "+y+"\n");
            }
        }

        /**
         * This method will show in console the block (not linked to the grid)
         * @param: A 2d char array, must be 4*4. If not, an exception is reased
         */
        public void showBlock(char[,] block)
        {
            if (!(block.Rank == 2) || !(block.GetUpperBound(0) == 4) || !(block.GetUpperBound(4) == 4))
                throw new System.ArgumentException("showBlock() - bloc passed must be 4*4");

            for (int x = block.GetLowerBound(0); x <= block.GetUpperBound(0); x++)
            {
                for (int y = block.GetLowerBound(1); y <= block.GetUpperBound(1); y++)
                    Console.Write(block.GetValue(x, y));
                Console.Write("\n");
            }
        }

        public void fillInGrid(Pieces piece)
        {
            
        }
    }
}
