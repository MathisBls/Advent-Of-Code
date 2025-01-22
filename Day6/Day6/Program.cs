// using System;
// using System.Collections.Generic;
// using System.IO;
// using static System.Net.Mime.MediaTypeNames;

// class GuardPatrol
// {
//     static void Main(string[] args)
//     {
//         string filePath = @"G:\projet\C#\AdventOfCode\Advent-Of-Code-CSharp\input.txt";
//         string[] map = File.ReadAllLines(filePath);

//         (int startX, int startY, char direction) = FindGuard(map);
//         int visitedCount = SimulatePatrol(map, startX, startY, direction);

//         Console.WriteLine(visitedCount);
//     }

//     static (int, int, char) FindGuard(string[] map)
//     {
//         for (int y = 0; y < map.Length; y++)
//         {
//             for (int x = 0; x < map[y].Length; x++)
//             {
//                 char cell = map[y][x];
//                 if ("^v<>".Contains(cell))
//                 {
//                     return (x, y, cell);
//                 }
//             }
//         }
//         throw new Exception("Guard not found on the map!");
//     }

//     static int SimulatePatrol(string[] map, int startX, int startY, char direction)
//     {
//         int rows = map.Length;
//         int cols = map[0].Length;
//         HashSet<(int, int)> visited = new HashSet<(int, int)>();
//         int x = startX, y = startY;

//         Dictionary<char, (int dx, int dy)> directions = new Dictionary<char, (int, int)>
//         {
//             { '^', (0, -1) },  // Haut
//             { 'v', (0, 1) },   // Bas
//             { '<', (-1, 0) },  // Gauche
//             { '>', (1, 0) }    // Droite
//         };
//         Dictionary<char, char> turnRight = new Dictionary<char, char>
//     {
//         { '^', '>' },
//         { '>', 'v' },
//         { 'v', '<' },
//         { '<', '^' }
//     };

//         visited.Add((x, y));

//         while (true)
//         {
//             var (dx, dy) = directions[direction];
//             int nextX = x + dx;
//             int nextY = y + dy;
//             if (nextX < 0 || nextX >= cols || nextY < 0 || nextY >= rows)
//             {
//                 break;
//             }
//             if (map[nextY][nextX] == '#')
//             {
//                 direction = turnRight[direction];
//             }
//             else
//             {
//                 x = nextX;
//                 y = nextY;
//                 visited.Add((x, y));
//             }

//         }
//         return visited.Count;
//     }
// }



using System;
using System.Collections.Generic;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

class GuardPatrol
{
    static void Main(string[] args)
    {
        string filePath = @"G:\projet\C#\AdventOfCode\Advent-Of-Code-CSharp\input.txt";
        string[] map = File.ReadAllLines(filePath);

        (int startX, int startY, char direction) = FindGuard(map);
        int validObstructionCount = CountValidObstructions(map, startX, startY, direction);

        Console.WriteLine(validObstructionCount);
    }

    static (int, int, char) FindGuard(string[] map)
    {
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                char cell = map[y][x];
                if ("^v<>".Contains(cell))
                {
                    return (x, y, cell);
                }
            }
        }
        throw new Exception("Guard not found on the map!");
    }

    static int CountValidObstructions(string[] map, int startX, int startY, char startDirection)
    {
        int rows = map.Length;
        int cols = map[0].Length;
        int validCount = 0;

        // Tester chaque position libre (.)
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                if (map[y][x] == '.' && !(x == startX && y == startY))
                {
                    // Ajouter un obstacle temporaire
                    char[][] modifiedMap = CreateModifiedMap(map, x, y, '#');

                    // Vérifier si cela crée une boucle
                    if (CreatesLoop(modifiedMap, startX, startY, startDirection))
                    {
                        validCount++;
                    }
                }
            }
        }

        return validCount;
    }

    static char[][] CreateModifiedMap(string[] map, int x, int y, char obstacle)
    {
        char[][] newMap = new char[map.Length][];
        for (int i = 0; i < map.Length; i++)
        {
            newMap[i] = map[i].ToCharArray();
        }
        newMap[y][x] = obstacle;
        return newMap;
    }

    static bool CreatesLoop(char[][] map, int startX, int startY, char startDirection)
    {
        int rows = map.Length;
        int cols = map[0].Length;

        HashSet<(int, int, char)> visitedStates = new HashSet<(int, int, char)>();
        int x = startX, y = startY;
        char direction = startDirection;

        // Directions : (dx, dy) pour chaque orientation
        Dictionary<char, (int dx, int dy)> directions = new Dictionary<char, (int, int)>
        {
            { '^', (0, -1) },  // Haut
            { 'v', (0, 1) },   // Bas
            { '<', (-1, 0) },  // Gauche
            { '>', (1, 0) }    // Droite
        };

        // Ordre de rotation à droite
        Dictionary<char, char> turnRight = new Dictionary<char, char>
        {
            { '^', '>' },
            { '>', 'v' },
            { 'v', '<' },
            { '<', '^' }
        };

        while (true)
        {
            if (!visitedStates.Add((x, y, direction)))
            {
                return true;
            }

            var (dx, dy) = directions[direction];
            int nextX = x + dx;
            int nextY = y + dy;

            if (nextX < 0 || nextX >= cols || nextY < 0 || nextY >= rows)
            {
                return false;
            }

            if (map[nextY][nextX] == '#')
            {
                direction = turnRight[direction];
            }
            else
            {
                x = nextX;
                y = nextY;
            }
        }
    }
}
