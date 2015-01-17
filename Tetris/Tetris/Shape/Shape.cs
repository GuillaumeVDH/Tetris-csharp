using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Shape
{
    abstract class Shape
    {
        protected int[,] shape;

        public Shape(){
            this.shape = new int[4,4];
            this.init();
            this.placeInCorner();
        }

        abstract protected void init();

        public void rotate()
        {
            this.print();
        }

        public void placeInCorner()
        {
            int nb_rows = 0, nb_col = 0;
            int[,] temp;

            nb_rows = Tetris.Utils.TabUtils.countEmptyRows(this.shape);
            nb_col = Tetris.Utils.TabUtils.countEmptyColumns(this.shape);

            if (nb_col > 0)
            {
                temp = new int[this.shape.GetLength(0), this.shape.GetLength(1)];
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if ((j + nb_col) < 4)
                        {
                            temp[i, j] = this.shape[i, j + nb_col];
                        }
                    }
                }
                this.shape = temp;
            }

            if (nb_rows > 0)
            {
                temp = new int[this.shape.GetLength(0), this.shape.GetLength(1)];
                for (int i = 3; i >= 0; i--)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if ((i - nb_rows) >= 0)
                        {
                            temp[i, j] = this.shape[i - nb_rows, j];
                        }
                    }
                }
                this.shape = temp;
            }
        }

        public void print()
        {
            Tetris.Utils.TabUtils.print(this.shape);
        }
    }
}
