//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;

//class Day4
//{
//    static void Main(string[] args)
//    {
//        string filePath = @"G:\projet\C#\AdventOfCode\Advent-Of-Code-CSharp\input.txt";
//        string[] grid = File.ReadAllLines(filePath);
//        string word = "XMAS";

//        int occurrences = CountAndHighlightWordOccurrences(grid, word, out string[] highlightedGrid);
//        Console.WriteLine($"Total occurrences of '{word}': {occurrences}");
//    }

//    static int CountAndHighlightWordOccurrences(string[] grid, string word, out string[] highlightedGrid)
//    {
//        int rows = grid.Length;
//        int cols = grid[0].Length;
//        int wordLength = word.Length;

//        bool[,] isPartOfWord = new bool[rows, cols];
//        int totalOccurrences = 0;

//        int[][] directions = {
//            new[] { 0, 1 },
//            new[] { 0, -1 },
//            new[] { 1, 0 },
//            new[] { -1, 0 },
//            new[] { 1, 1 },
//            new[] { 1, -1 },
//            new[] { -1, 1 },
//            new[] { -1, -1 }
//        };

//        for (int row = 0; row < rows; row++)
//        {
//            for (int col = 0; col < cols; col++)
//            {
//                foreach (var dir in directions)
//                {
//                    int dRow = dir[0];
//                    int dCol = dir[1];
//                    if (IsWordInDirection(grid, row, col, dRow, dCol, word))
//                    {
//                        for (int i = 0; i < wordLength; i++)
//                        {
//                            int newRow = row + i * dRow;
//                            int newCol = col + i * dCol;
//                            isPartOfWord[newRow, newCol] = true;
//                        }
//                        totalOccurrences++;
//                    }
//                }
//            }
//        }

//        highlightedGrid = new string[rows];
//        for (int row = 0; row < rows; row++)
//        {
//            char[] line = grid[row].ToCharArray();
//            for (int col = 0; col < cols; col++)
//            {
//                if (!isPartOfWord[row, col])
//                {
//                    line[col] = '.';
//                }
//            }
//            highlightedGrid[row] = new string(line);
//        }

//        return totalOccurrences;
//    }

//    static bool IsWordInDirection(string[] grid, int row, int col, int dRow, int dCol, string word)
//    {
//        int rows = grid.Length;
//        int cols = grid[0].Length;
//        int wordLength = word.Length;

//        for (int i = 0; i < wordLength; i++)
//        {
//            int newRow = row + i * dRow;
//            int newCol = col + i * dCol;

//            if (newRow < 0 || newRow >= rows || newCol < 0 || newCol >= cols)
//            {
//                return false;
//            }

//            if (grid[newRow][newCol] != word[i])
//            {
//                return false;
//            }
//        }

//        return true;
//    }
//}


using System;
using System.IO;

class XMASFinder
{
    static void Main(string[] args)
    {
        string filePath = @"C:\Users\mboulais\Documents\perso\AdventOfCode\input.txt";
        string[] grid = File.ReadAllLines(filePath);
        int count = CountXMAS(grid);
        Console.WriteLine($"Total X-MAS patterns: {count}");
    }

    static int CountXMAS(string[] grid)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;
        int totalCount = 0;

        var directions = new (int RowDelta, int ColDelta)[]
        {
            (-1, -1), // Haut gauche
            (-1, 1),  // Haut droite
            (1, -1),  // Bas gauche
            (1, 1)    // Bas droite
        };

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                int diagonalCount = 0;

                foreach (var direction in directions)
                {
                    var oppositeDirection = (-direction.RowDelta, -direction.ColDelta);
                    var startingPoint = (row + oppositeDirection.Item1, col + oppositeDirection.Item2);

                    if (SearchWord(grid, startingPoint, direction, "MAS"))
                    {
                        diagonalCount++;
                    }
                }

                if (diagonalCount == 2)
                {
                    totalCount++;
                }
            }
        }

        return totalCount;
    }

    static bool SearchWord(string[] grid, (int Row, int Col) startingPoint, (int RowDelta, int ColDelta) direction, string word)
    {
        for (int i = 0; i < word.Length; i++)
        {
            int newRow = startingPoint.Row + direction.RowDelta * i;
            int newCol = startingPoint.Col + direction.ColDelta * i;

            if (newRow < 0 || newRow >= grid.Length || newCol < 0 || newCol >= grid[0].Length)
            {
                return false;
            }

            if (grid[newRow][newCol] != word[i])
            {
                return false;
            }
        }

        return true;
    }
}