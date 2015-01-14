using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{

    //TODO
     /* 
      * @GuillaumeVDH: 
      * Une pièce est un tableau de char à 2 dimensions (4*4)
      * Une pièce possède une position dans le tableau (x,y)
      * Une pièce possède un type (à définir dans un header rassemblent les paramètres globaux de ce type)
      * Une pièce possède une taille (le "L" fait 3 de haut, le carré seulement 2)
      * Une pièce possède une orientation
      * Optionel, une pièce pourra possèder une couleur (lié au type) [pas la prio, ça sera implémenté plus tard]
      * 
      * Une pièce peut être déplacée (droite, gauche, bas)
      */
    class Pieces
    {
        private char[,] m_piece = new char [Common.PIECE_WIDTH,Common.PIECE_HEIGHT];

        public int X { get; set; }
        public int Y { get; set; } //TODO speed write, I let you change if needed
        


    }
}
