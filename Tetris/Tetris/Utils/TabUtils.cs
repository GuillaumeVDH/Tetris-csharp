﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris.Utils
{
    class TabUtils
    {
        internal static int[,] rotate(int[,] tab)
        {
            if (tab.Rank != 2)
            {
                throw new System.ArgumentException("Parameter must be a two dimensional array", "tab");
            }

            if (tab.GetLength(0) != tab.GetLength(1))
            {
                throw new System.ArgumentException("Dimensions must have the same length");
            }

            int[,] temp = new int[tab.GetLength(0), tab.GetLength(1)];

            //rotate the array
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    temp[i, j] = tab[tab.GetLength(1) - 1 - j, i];
                }
            }

            return temp;
        }

        internal static int countEmptyColumns(int[,] tab)
        {
            if (tab.Rank != 2)
            {
                throw new System.ArgumentException("Parameter must be a two dimensional array", "tab");
            }

            int count = 0, x;

            //we count the columns from the left
            for (int j = 0; j < tab.GetLength(1); j++)
            {
                x = 0;

                for (int i = 0; i < tab.GetLength(0); i++)
                {
                    x += tab[i, j];
                }
                if (x == 0 && count == j )
                {
                    count++;
                }
            }

            return count;
        }

        internal static int countEmptyRows(int[,] tab)
        {
            if (tab.Rank != 2)
            {
                throw new System.ArgumentException("Parameter must be a two dimensional array", "tab");
            }

            int count = 0, x;

            //we count the rows from the bottom
            for (int i = tab.GetLength(0) - 1; i >= 0; i--)
            {
                x = 0;

                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    x += tab[i, j];
                }
                if (x == 0 && count == tab.GetLength(0) - 1 - i)
                {
                    count++;
                }
            }

            return count;
        }

        internal static void print<T>(T[,] tab)
        {
            if (tab.Rank != 2)
            {
                throw new System.ArgumentException("Parameter must be a two dimensional array", "tab");
            }

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    Console.Write(tab[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
