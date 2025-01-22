// // using System;
// // using System.Linq;

// // class Day5
// // {
// //     static void Main(string[] args)
// //     {
// //         string[] lines = File.ReadAllLines("C:\\Users\\mboulais\\Documents\\perso\\AdventOfCode\\input.txt");

// //         List<(int, int)> rules = new List<(int, int)>();
// //         List<List<int>> updates = new List<List<int>>();
// //         bool isUpdateSection = false;

// //         foreach (string line in lines)
// //         {
// //             if (string.IsNullOrWhiteSpace(line))
// //             {
// //                 isUpdateSection = true;
// //                 continue;
// //             }
// //             if(!isUpdateSection)
// //             {
// //                 var parts = line.Split('|').Select(int.Parse).ToArray();
// //                 rules.Add((parts[0], parts[1]));
// //             } else
// //             {
// //                 var update = line.Split(',').Select(int.Parse).ToList();
// //                 updates.Add(update);
// //             }
// //         }

// //         int totalMiddleSum = 0;
// //         foreach (var update in updates)
// //         {
// //             if(IsValidUpdate(update, rules))
// //             {
// //                 int middleIndex = update.Count / 2;
// //                 totalMiddleSum += update[middleIndex];
// //             }
// //         }

// //         Console.WriteLine(totalMiddleSum);
// //     }

// //     static bool IsValidUpdate(List<int> update, List<(int, int)> rules)
// //     {
// //         Dictionary<int, int> positions = update
// //             .Select((page, index) => (page, index))
// //             .ToDictionary(x => x.page, x => x.index);

// //         foreach (var (x, y) in rules)
// //         {
// //             if (positions.ContainsKey(x) && positions.ContainsKey(y))
// //             {
// //                 if (positions[x] >= positions[y])
// //                 {
// //                     return false;
// //                 }
// //             }
// //         }

// //         return true;
// //     }
// // }



// using System;
// using System.Linq;

// class Day5
// {
//     static void Main(string[] args)
//     {
//         string[] lines = File.ReadAllLines("C:\\Users\\mboulais\\Documents\\perso\\AdventOfCode\\input.txt");

//         List<(int, int)> rules = new List<(int, int)>();
//         List<List<int>> updates = new List<List<int>>();
//         bool isUpdateSection = false;

//         foreach (string line in lines)
//         {
//             if (string.IsNullOrWhiteSpace(line))
//             {
//                 isUpdateSection = true;
//                 continue;
//             }
//             if(!isUpdateSection)
//             {
//                 var parts = line.Split('|').Select(int.Parse).ToArray();
//                 rules.Add((parts[0], parts[1]));
//             } else
//             {
//                 var update = line.Split(',').Select(int.Parse).ToList();
//                 updates.Add(update);
//             }
//         }

//         int totalMiddleSum = 0;
//         foreach (var update in updates)
//         {
//             if(IsValidUpdate(update, rules))
//             {
//                 int middleIndex = update.Count / 2;
//                 totalMiddleSum += update[middleIndex];
//             }
//         }

//         Console.WriteLine(totalMiddleSum);
//     }

//     static bool IsValidUpdate(List<int> update, List<(int, int)> rules)
//     {
//         Dictionary<int, int> positions = update
//             .Select((page, index) => (page, index))
//             .ToDictionary(x => x.page, x => x.index);

//         foreach (var (x, y) in rules)
//         {
//             if (positions.ContainsKey(x) && positions.ContainsKey(y))
//             {
//                 if (positions[x] >= positions[y])
//                 {
//                     return false;
//                 }
//             }
//         }

//         return true;
//     }
// }


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class PrintQueuePart2
{
    static void Main(string[] args)
    {
        // Lire le fichier
        string[] lines = File.ReadAllLines("C:\\Users\\mboulais\\Documents\\perso\\AdventOfCode\\input.txt");

        // Séparer les règles et les mises à jour
        List<(int, int)> rules = new List<(int, int)>();
        List<List<int>> updates = new List<List<int>>();
        bool isUpdateSection = false;

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                isUpdateSection = true;
                continue;
            }

            if (!isUpdateSection)
            {
                var parts = line.Split('|').Select(int.Parse).ToArray();
                rules.Add((parts[0], parts[1]));
            }
            else
            {
                var update = line.Split(',').Select(int.Parse).ToList();
                updates.Add(update);
            }
        }

        int totalMiddleSum = 0;
        foreach (var update in updates)
        {
            if (!IsValidUpdate(update, rules))
            {
                var correctedUpdate = CorrectUpdate(update, rules);

                int middleIndex = correctedUpdate.Count / 2;
                totalMiddleSum += correctedUpdate[middleIndex];
            }
        }

        Console.WriteLine($"Total sum of middle page numbers for corrected updates: {totalMiddleSum}");
    }

    static bool IsValidUpdate(List<int> update, List<(int, int)> rules)
    {
        // Créer un dictionnaire des positions dans la mise à jour
        Dictionary<int, int> positions = update
            .Select((page, index) => (page, index))
            .ToDictionary(x => x.page, x => x.index);

        foreach (var (x, y) in rules)
        {
            if (positions.ContainsKey(x) && positions.ContainsKey(y))
            {
                if (positions[x] >= positions[y])
                {
                    return false;
                }
            }
        }

        return true;
    }

    static List<int> CorrectUpdate(List<int> update, List<(int, int)> rules)
    {
        Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
        Dictionary<int, int> inDegree = new Dictionary<int, int>();

        foreach (var page in update)
        {
            graph[page] = new List<int>();
            inDegree[page] = 0;
        }

        foreach (var (x, y) in rules)
        {
            if (update.Contains(x) && update.Contains(y))
            {
                graph[x].Add(y);
                inDegree[y]++;
            }
        }

        Queue<int> queue = new Queue<int>();
        foreach (var page in update)
        {
            if (inDegree[page] == 0)
            {
                queue.Enqueue(page);
            }
        }

        List<int> sortedUpdate = new List<int>();
        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            sortedUpdate.Add(current);

            foreach (var neighbor in graph[current])
            {
                inDegree[neighbor]--;
                if (inDegree[neighbor] == 0)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }

        return sortedUpdate;
    }
}
