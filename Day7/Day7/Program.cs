// using System;
// using System.Collections.Generic;
// using System.Data;
// using System.IO;

// class BridgeRepair
// {
//     static void Main(string[] args)
//     {
//         string filePath = @"G:\projet\C#\AdventOfCode\Advent-Of-Code-CSharp\input.txt";
//         string[] input = File.ReadAllLines(filePath);

//         long totalCalibrationResult = 0;

//         foreach (string line in input)
//         {
//             var parts = line.Split(':');
//             long testValue = long.Parse(parts[0]);
//             long[] numbers = Array.ConvertAll(parts[1].Trim().Split(' '), long.Parse);

//             if (CanBeTrue(numbers, testValue))
//             {
//                 totalCalibrationResult += testValue;
//             }
//         }

//         Console.WriteLine($"Total calibration result: {totalCalibrationResult}");
//     }

//     static bool CanBeTrue(long[] numbers, long testValue)
//     {
//         int n = numbers.Length;
//         int operatorCount = n - 1;
//         int totalCombinations = (int)Math.Pow(2, operatorCount);

//         for (int i = 0; i < totalCombinations; i++)
//         {
//             List<string> operators = new List<string>();
//             int mask = i;

//             for (int j = 0; j < operatorCount; j++)
//             {
//                 operators.Add((mask & 1) == 1 ? "+" : "*");
//                 mask >>= 1;
//             }

//             if (EvaluateExpression(numbers, operators) == testValue)
//             {
//                 return true;
//             }

//         }

//         return false;
//     }

//     static long EvaluateExpression(long[] numbers, List<string> operators)
//     {
//         long result = numbers[0];

//         for (int i = 0; i < operators.Count; i++)
//         {
//             if (operators[i] == "+")
//             {
//                 result += numbers[i + 1];
//             }
//             else
//             {
//                 result *= numbers[i + 1];
//             }
//         }

//         return result;
//     }
// }



using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

class BridgeRepair
{
    static void Main(string[] args)
    {
        string filePath = @"G:\projet\C#\AdventOfCode\Advent-Of-Code-CSharp\input.txt";
        string[] input = File.ReadAllLines(filePath);

        long totalCalibrationResult = 0;

        foreach (string line in input)
        {
            var parts = line.Split(':');
            long testValue = long.Parse(parts[0]);
            long[] numbers = Array.ConvertAll(parts[1].Trim().Split(' '), long.Parse);

            if (CanBeTrue(numbers, testValue))
            {
                totalCalibrationResult += testValue;
            }
        }

        Console.WriteLine($"Total calibration result: {totalCalibrationResult}");
    }

    static bool CanBeTrue(long[] numbers, long testValue)
    {
        int n = numbers.Length;
        int operatorCount = n - 1;
        int totalCombinations = (int)Math.Pow(3, operatorCount);

        for (int i = 0; i < totalCombinations; i++)
        {
            List<string> operators = new List<string>();
            int mask = i;

            for (int j = 0; j < operatorCount; j++)
            {
                int opIndex = mask % 3;
                if (opIndex == 0)
                {
                    operators.Add("+");
                }
                else if (opIndex == 1)
                {
                    operators.Add("*");
                }
                else
                {
                    operators.Add("||");
                }
                mask /= 3;
            }

            if (EvaluateExpression(numbers, operators) == testValue)
            {
                return true;
            }
        }

        return false;
    }
    static long EvaluateExpression(long[] numbers, List<string> operators)
    {
        long result = numbers[0];

        for (int i = 0; i < operators.Count; i++)
        {
            if (operators[i] == "+")
            {
                result += numbers[i + 1];
            }
            else if (operators[i] == "*")
            {
                result *= numbers[i + 1];
            }
            else
            {
                result = long.Parse(result.ToString() + numbers[i + 1].ToString());
            }
        }

        return result;
    }
}
