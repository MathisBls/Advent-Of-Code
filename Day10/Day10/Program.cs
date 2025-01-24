// using System;
// using System.Collections.Generic;
// using System.IO;

// class LavaIsland
// {
//     static void Main(string[] args)
//     {
//         string filePath = @"G:\projet\C#\AdventOfCode\Advent-Of-Code-CSharp\input.txt";
//         var map = ReadTopographicMap(filePath);

//         int totalScore = CalculateTrailheadScores(map);
//         Console.WriteLine($"Total Score of all Trailheads: {totalScore}");
//     }

//     static int[,] ReadTopographicMap(string filePath)
//     {
//         var lines = File.ReadAllLines(filePath);
//         int rows = lines.Length;
//         int cols = lines[0].Length;

//         int[,] map = new int[rows, cols];
//         for (int r = 0; r < rows; r++)
//         {
//             for (int c = 0; c < cols; c++)
//             {
//                 map[r, c] = lines[r][c] - '0';
//             }
//         }

//         return map;
//     }

//     static int CalculateTrailheadScores(int[,] map)
//     {
//         int rows = map.GetLength(0);
//         int cols = map.GetLength(1);
//         int totalScore = 0;

//         for (int r = 0; r < rows; r++)
//         {
//             for (int c = 0; c < cols; c++)
//             {
//                 if (map[r, c] == 0)
//                 {
//                     totalScore += CalculateTrailheadScore(map, r, c);
//                 }
//             }
//         }

//         return totalScore;
//     }

//     static int CalculateTrailheadScore(int[,] map, int startRow, int startCol)
//     {
//         int rows = map.GetLength(0);
//         int cols = map.GetLength(1);
//         var visited = new bool[rows, cols];
//         var reachableNines = new HashSet<(int, int)>();
//         var directions = new (int dr, int dc)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

//         var queue = new Queue<(int row, int col)>();
//         queue.Enqueue((startRow, startCol));
//         visited[startRow, startCol] = true;

//         while (queue.Count > 0)
//         {
//             var (currentRow, currentCol) = queue.Dequeue();

//             foreach (var (dr, dc) in directions)
//             {
//                 int newRow = currentRow + dr;
//                 int newCol = currentCol + dc;

//                 if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols &&
//                     !visited[newRow, newCol] &&
//                     map[newRow, newCol] == map[currentRow, currentCol] + 1)
//                 {
//                     visited[newRow, newCol] = true;
//                     queue.Enqueue((newRow, newCol));

//                     if (map[newRow, newCol] == 9)
//                     {
//                         reachableNines.Add((newRow, newCol));
//                     }
//                 }
//             }
//         }

//         return reachableNines.Count;
//     }
// }


using System;
using System.Collections.Generic;
using System.IO;

class LavaIslandRating
{
    static void Main(string[] args)
    {
        string filePath = @"G:\projet\C#\AdventOfCode\Advent-Of-Code-CSharp\input.txt";
        var map = ReadTopographicMap(filePath);

        int totalRating = CalculateTrailheadRatings(map);
        Console.WriteLine($"Total Rating of all Trailheads: {totalRating}");
    }

    static int[,] ReadTopographicMap(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        int rows = lines.Length;
        int cols = lines[0].Length;

        int[,] map = new int[rows, cols];
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                map[r, c] = lines[r][c] - '0';
            }
        }

        return map;
    }

    static int CalculateTrailheadRatings(int[,] map)
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);
        var cache = new Dictionary<(int, int), int>();
        int totalRating = 0;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (map[r, c] == 0)
                {
                    totalRating += CalculateTrailheadRating(map, r, c, cache);
                }
            }
        }

        return totalRating;
    }

    static int CalculateTrailheadRating(int[,] map, int row, int col, Dictionary<(int, int), int> cache)
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);
        var directions = new (int dr, int dc)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

        if (map[row, col] == 9)
        {
            return 1;
        }

        if (cache.ContainsKey((row, col)))
        {
            return cache[(row, col)];
        }

        int rating = 0;

        foreach (var (dr, dc) in directions)
        {
            int newRow = row + dr;
            int newCol = col + dc;

            if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols &&
                map[newRow, newCol] == map[row, col] + 1)
            {
                rating += CalculateTrailheadRating(map, newRow, newCol, cache);
            }
        }

        cache[(row, col)] = rating;
        return rating;
    }
}
