// using System;
// using System.Collections.Generic;

// class AntinodeCalculator
// {
//     static void Main(string[] args)
//     {
//         string filePath = @"G:\projet\C#\AdventOfCode\Advent-Of-Code-CSharp\input.txt";
//         string[] map = File.ReadAllLines(filePath);
//         int result = CountAntinodes(map);
//         Console.WriteLine($"Total unique antinode locations: {result}");
//     }

//     static int CountAntinodes(string[] map)
//     {
//         int rows = map.Length;
//         int cols = map[0].Length;

//         Dictionary<char, List<(int, int)>> antennas = new Dictionary<char, List<(int, int)>>();

//         for (int row = 0; row < rows; row++)
//         {
//             for (int col = 0; col < cols; col++)
//             {
//                 char c = map[row][col];
//                 if (c != '.')
//                 {
//                     if (!antennas.ContainsKey(c))
//                         antennas[c] = new List<(int, int)>();
//                     antennas[c].Add((row, col));
//                 }
//             }
//         }

//         // Set pour stocker les positions uniques des antinœuds
//         HashSet<(int, int)> antinodePositions = new HashSet<(int, int)>();

//         // Calculer les antinœuds pour chaque fréquence
//         foreach (var kvp in antennas)
//         {
//             List<(int, int)> positions = kvp.Value;

//             // Comparer chaque paire d'antennes de la même fréquence
//             for (int i = 0; i < positions.Count; i++)
//             {
//                 for (int j = 0; j < positions.Count; j++)
//                 {
//                     if (i == j) continue;

//                     var p1 = positions[i];
//                     var p2 = positions[j];

//                     // Calculer les positions des antinœuds
//                     int dRow = p2.Item1 - p1.Item1;
//                     int dCol = p2.Item2 - p1.Item2;

//                     // Antinœud côté 1 (proche de p1)
//                     int antinode1Row = p1.Item1 - dRow;
//                     int antinode1Col = p1.Item2 - dCol;

//                     // Antinœud côté 2 (loin de p2)
//                     int antinode2Row = p2.Item1 + dRow;
//                     int antinode2Col = p2.Item2 + dCol;

//                     // Vérifier si les antinœuds sont dans les limites de la carte
//                     if (IsValidPosition(antinode1Row, antinode1Col, rows, cols))
//                         antinodePositions.Add((antinode1Row, antinode1Col));

//                     if (IsValidPosition(antinode2Row, antinode2Col, rows, cols))
//                         antinodePositions.Add((antinode2Row, antinode2Col));
//                 }
//             }
//         }

//         return antinodePositions.Count;
//     }

//     static bool IsValidPosition(int row, int col, int rows, int cols)
//     {
//         return row >= 0 && row < rows && col >= 0 && col < cols;
//     }
// }


using System;
using System.Collections.Generic;
using System.IO;

class AntinodeCalculatorPart2
{
    static void Main(string[] args)
    {
        string filePath = @"G:\projet\C#\AdventOfCode\Advent-Of-Code-CSharp\input.txt";
        string[] map = File.ReadAllLines(filePath);

        int result = CountAntinodes(map);
        Console.WriteLine($"Total unique antinode locations: {result}");
    }

    static int CountAntinodes(string[] map)
    {
        int rows = map.Length;
        int cols = map[0].Length;

        Dictionary<char, List<(int, int)>> antennas = new Dictionary<char, List<(int, int)>>();

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                char c = map[row][col];
                if (c != '.')
                {
                    if (!antennas.ContainsKey(c))
                        antennas[c] = new List<(int, int)>();
                    antennas[c].Add((row, col));
                }
            }
        }

        HashSet<(int, int)> antinodePositions = new HashSet<(int, int)>();

        foreach (var kvp in antennas)
        {
            List<(int, int)> positions = kvp.Value;

            foreach (var antenna in positions)
            {
                antinodePositions.Add(antenna);
            }

            for (int i = 0; i < positions.Count; i++)
            {
                for (int j = i + 1; j < positions.Count; j++)
                {
                    var p1 = positions[i];
                    var p2 = positions[j];

                    AddAlignedAntinodes(p1, p2, antinodePositions, rows, cols);
                }
            }
        }

        return antinodePositions.Count;
    }

    static void AddAlignedAntinodes((int, int) p1, (int, int) p2, HashSet<(int, int)> antinodePositions, int rows, int cols)
    {
        int dRow = p2.Item1 - p1.Item1;
        int dCol = p2.Item2 - p1.Item2;

        int gcd = GCD(Math.Abs(dRow), Math.Abs(dCol));
        dRow /= gcd;
        dCol /= gcd;

        int r = p1.Item1;
        int c = p1.Item2;

        while (r >= 0 && r < rows && c >= 0 && c < cols)
        {
            antinodePositions.Add((r, c));
            r += dRow;
            c += dCol;
        }

        r = p2.Item1;
        c = p2.Item2;

        while (r >= 0 && r < rows && c >= 0 && c < cols)
        {
            antinodePositions.Add((r, c));
            r -= dRow;
            c -= dCol;
        }
    }

    static int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
}
