using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Piece
{
    abstract class Piece
    {
        protected Shape.Shape shape;
        private int x_axis;
        private int y_axis;

        public Piece()
        {
            this.initShape();
            //init the piece outside the board
            this.x_axis = 4;
            this.y_axis = -3;
        }

        public void rotate()
        {
            this.shape.rotate();
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
    }
}
