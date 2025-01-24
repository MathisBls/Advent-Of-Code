/*using System;
using System.IO;
using System.Linq;

class Day1
{
    static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("C:\\Users\\mboulais\\Documents\\perso\\AdventOfCode\\input.txt");

        int[][] lists = lines
            .Select(line => line.Split(' ').Where(s => !string.IsNullOrEmpty(s)).Select(int.Parse).ToArray())
            .ToArray();

        int[] leftList = lists.Select(pair => pair[0]).ToArray();
        int[] rightList = lists.Select(pair => pair[1]).ToArray();

        Array.Sort(leftList);
        Array.Sort(rightList);

        int totalDistance = 0;
        for (int i = 0; i < leftList.Length; i++)
        {
            totalDistance += Math.Abs(leftList[i] - rightList[i]);
        }

        Console.WriteLine($"Total Distance: {totalDistance}");
    }
}*/

//PART 2

using System;
using System.IO;
using System.Linq;

class SimilarityCalculator
{
    static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("C:\\Users\\mboulais\\Documents\\perso\\AdventOfCode\\input.txt");

        List<int> leftList = new List<int>();
        List<int> rightList = new List<int>();

        foreach (string line in lines)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                leftList.Add(int.Parse(parts[0]));
                rightList.Add(int.Parse(parts[1]));
            }
        }

        Dictionary<int, int> rightListCounts = new Dictionary<int, int>();
        foreach (int number in rightList)
        {
            if (!rightListCounts.ContainsKey(number))
            {
                rightListCounts[number] = 0;
            }
            rightListCounts[number]++;
        }

        int similarityScore = 0;
        foreach (int number in leftList)
        {
            if (rightListCounts.ContainsKey(number))
            {
                similarityScore += number * rightListCounts[number];
            }
        }

        Console.WriteLine(similarityScore);
    }
}
