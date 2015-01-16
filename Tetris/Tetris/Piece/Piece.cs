using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Pieces
{
    abstract class Piece
    {
        private int[,] shape;
        private int indice_rotation;
        private int coordonnee_x;
        private int coordonnee_y;

        public Piece()
        {
            this.shape = new int[4, 4];

        }

        public void placerDansCoin()
        {

        }

        abstract private void initShape();
    }
}
