/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day2
{
    static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("C:\\Users\\mboulais\\Documents\\perso\\AdventOfCode\\input.txt");

        int safeCount = 0;

        foreach (string line in lines)
        {
            if (!string.IsNullOrWhiteSpace(line)) {

                int[] levels = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            if (IsSafeReport(levels))
            {
                safeCount++;
            }
        }
       }
        Console.WriteLine(safeCount);
    }

    static bool IsSafeReport(int[] levels)
    {
        bool isincreasing = true;
        bool isDecreasing = true;

        for(int i = 1; i < levels.Length; i++) {
            int difference = levels[i] - levels[i -1];

            if(Math.Abs(difference) < 1 ||  Math.Abs(difference) > 3)
            {
                return false;
            }

            if(difference > 0)
            {
                isDecreasing = false;
            }
            else if(difference < 0)
            {
                isincreasing = false;
            }
        }
        return isincreasing || isDecreasing;
    }
}
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day2
{
    static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("C:\\Users\\mboulais\\Documents\\perso\\AdventOfCode\\input.txt");

        int safeCount = 0;

        foreach (string line in lines)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                int[] levels = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                if (IsSafeReport(levels) || CanBecomeSafeWithDampener(levels))
                {
                    safeCount++;
                }
            }
        }

        Console.WriteLine($"Number of safe reports: {safeCount}");
    }

    static bool IsSafeReport(int[] levels)
    {
        bool isincreasing = true;
        bool isDecreasing = true;

        for (int i = 1; i < levels.Length; i++)
        {
            int difference = levels[i] - levels[i - 1];

            if (Math.Abs(difference) < 1 || Math.Abs(difference) > 3)
            {
                return false;
            }

            if (difference > 0)
            {
                isDecreasing = false;
            }
            else if (difference < 0)
            {
                isincreasing = false;
            }
        }

        return isincreasing || isDecreasing;
    }

    static bool CanBecomeSafeWithDampener(int[] levels)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            int[] modifedLevels = levels.Where((_, index) => index != i).ToArray();

            if (IsSafeReport(modifedLevels))
            {
                return true;
            }
        }
        return false;
    }

}

