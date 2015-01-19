using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Shape
{
    abstract class AShape
    {
        public int[,] Shape { get; set; }

        public AShape(){
            this.Shape = new int[4,4];
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

            nb_rows = Tetris.Utils.TabUtils.countEmptyRows(this.Shape);
            nb_col = Tetris.Utils.TabUtils.countEmptyColumns(this.Shape);

            if (nb_col > 0)
            {
                temp = new int[this.Shape.GetLength(0), this.Shape.GetLength(1)];
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if ((j + nb_col) < 4)
                        {
                            temp[i, j] = this.Shape[i, j + nb_col];
                        }
                    }
                }
                this.Shape = temp;
            }

            if (nb_rows > 0)
            {
                temp = new int[this.Shape.GetLength(0), this.Shape.GetLength(1)];
                for (int i = 3; i >= 0; i--)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if ((i - nb_rows) >= 0)
                        {
                            temp[i, j] = this.Shape[i - nb_rows, j];
                        }
                    }
                }
                this.Shape = temp;
            }
        }

        public void print()
        {
            Tetris.Utils.TabUtils.print(this.Shape);
        }
    }
}
