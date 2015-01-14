using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class MainClass
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid();
            try
            {
                grid.initGrid('-');
                grid.showGrid();

            } catch(System.ArrayTypeMismatchException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            Console.ReadLine(); //TODO DEBUG ONLY - Just there to stop program before automaticly closing the window.
            /* Test, is this mess up now clean for REAL??? */
        }
    }
}
