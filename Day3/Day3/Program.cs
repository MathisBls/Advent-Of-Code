//using System;
//using System.Text.RegularExpressions;

//class Day3
//{
//    static void Main(string[] args)
//    {
//        string filePath = @"G:\projet\C#\AdventOfCode\Advent-Of-Code-CSharp\input.txt";

//        string corruptedMemory = File.ReadAllText(filePath);

//        string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
//        Regex regex = new Regex(pattern);

//        int total = 0;

//        MatchCollection matches = regex.Matches(corruptedMemory);
//        foreach (Match match in matches)
//        {
//            int x = int.Parse(match.Groups[1].Value);
//            int y = int.Parse(match.Groups[2].Value);

//            total += x * y;

//            Console.WriteLine($"Valid multiplication: {match.Value} = {x} * {y} = {x * y}");
//        }

//        Console.WriteLine($"Total of valid multiplications: {total}");
//    }
//}

using System;
using System.Text.RegularExpressions;

class MemoryProcessor
{
    static void Main(string[] args)
    {
        string filePath = @"G:\projet\C#\AdventOfCode\Advent-Of-Code-CSharp\input.txt";

        string corruptedMemory = File.ReadAllText(filePath);

        string mulPattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        string controlPattern = @"\b(do|don't)\(\)";
        Regex mulRegex = new Regex(mulPattern);
        Regex controlRegex = new Regex(controlPattern);

        int total = 0;
        bool isEnabled = true;

        int position = 0;
        while (position < corruptedMemory.Length)
        {
            Match mulMatch = mulRegex.Match(corruptedMemory, position);
            Match controlMatch = controlRegex.Match(corruptedMemory, position);

            if (mulMatch.Success && (controlMatch.Success == false || mulMatch.Index < controlMatch.Index))
            {
                if (isEnabled)
                {
                    int x = int.Parse(mulMatch.Groups[1].Value);
                    int y = int.Parse(mulMatch.Groups[2].Value);
                    total += x * y;
                }

                position = mulMatch.Index + mulMatch.Length;
            }
            else if (controlMatch.Success)
            {
                if (controlMatch.Value == "do()")
                {
                    isEnabled = true;
                }
                else if (controlMatch.Value == "don't()")
                {
                    isEnabled = false;
                }

                position = controlMatch.Index + controlMatch.Length;
            }
            else
            {
                position++;
            }
        }

        Console.WriteLine($"Total of valid multiplications: {total}");
    }
}
