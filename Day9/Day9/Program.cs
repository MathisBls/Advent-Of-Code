using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

internal class FreeSpace
{
    public int StartBlock { get; private set; }
    public int Length { get; private set; }

    public FreeSpace(int startBlock, int length)
    {
        StartBlock = startBlock;
        Length = length;
    }

    public void Allocate(int fileSize)
    {
        StartBlock += fileSize;
        Length -= fileSize;
    }
}

internal class FileInfo
{
    public int StartBlock { get; set; }
    public int FileID { get; }
    public int FileSize { get; }

    public FileInfo(int startBlock, int fileId, int fileSize)
    {
        StartBlock = startBlock;
        FileID = fileId;
        FileSize = fileSize;
    }

    public long CheckSum
    {
        get
        {
            var checksum = 0L;
            for (var i = 0; i < FileSize; i++)
            {
                checksum += FileID * (StartBlock + i);
            }
            return checksum;
        }
    }
}

internal static class Program
{
    private static void Main()
    {
        string filePath = @"G:\projet\C#\AdventOfCode\Advent-Of-Code-CSharp\input.txt";
        var input = File.ReadAllText(filePath).Trim();

        var part1 = Part1(input);
        Console.WriteLine($"Part 1: {part1}");

        var part2 = Part2(input);
        Console.WriteLine($"Part 2: {part2}");
    }

    private static long Part1(string input)
    {
        var blocks = GetBlocks(input);

        var nextFree = input[0] - '0' - 1;
        for (var i = blocks.Length - 1; i >= 0; i--)
        {
            while (blocks[nextFree] != -1) nextFree++;
            if (nextFree >= i) break;

            var fileID = blocks[i];
            if (fileID == -1) continue;

            blocks[i] = -1;
            blocks[nextFree++] = fileID;
        }

        return ComputeChecksum(blocks);
    }

    private static int[] GetBlocks(string input)
    {
        var blockCount = input.Select(c => c - '0').Sum();
        var blocks = new int[blockCount];
        var blockIndex = 0;
        var fileID = 0;

        for (var i = 0; i < input.Length; i += 2)
        {
            for (var j = 0; j < input[i] - '0'; j++) blocks[blockIndex++] = fileID;

            if (i + 1 < input.Length)
            {
                for (var j = 0; j < input[i + 1] - '0'; j++) blocks[blockIndex++] = -1;
            }
            fileID++;
        }

        return blocks;
    }

    private static long ComputeChecksum(int[] blocks)
    {
        var checksum = 0L;
        for (var i = 0; i < blocks.Length; i++)
        {
            if (blocks[i] != -1)
            {
                checksum += (long)i * blocks[i];
            }
        }
        return checksum;
    }

    private static long Part2(string input)
    {
        var freeBlocks = new List<FreeSpace>();
        var files = new List<FileInfo>();

        var startBlock = 0;
        var fileID = 0;

        for (var i = 0; i < input.Length; i += 2)
        {
            var fileSize = input[i] - '0';
            var fileInfo = new FileInfo(startBlock, fileID++, fileSize);
            files.Add(fileInfo);
            startBlock += fileSize;

            if (i < input.Length - 1)
            {
                var length = input[i + 1] - '0';
                var freeSpace = new FreeSpace(startBlock, length);
                freeBlocks.Add(freeSpace);
                startBlock += length;
            }
        }

        for (var i = files.Count - 1; i >= 0; i--)
        {
            var fileInfo = files[i];
            var freeSpaceIndex = FindFreeSpace(freeBlocks, fileInfo);
            if (freeSpaceIndex == -1) continue;

            var freeSpace = freeBlocks[freeSpaceIndex];
            fileInfo.StartBlock = freeSpace.StartBlock;
            freeSpace.Allocate(fileInfo.FileSize);

            if (freeSpace.Length == 0)
            {
                freeBlocks.RemoveAt(freeSpaceIndex);
            }
        }

        return files.Sum(file => file.CheckSum);
    }

    private static int FindFreeSpace(List<FreeSpace> freeBlocks, FileInfo fileInfo)
    {
        for (var i = 0; i < freeBlocks.Count; i++)
        {
            var freeSpace = freeBlocks[i];
            if (freeSpace.StartBlock >= fileInfo.StartBlock) break;

            if (freeSpace.Length >= fileInfo.FileSize) return i;
        }
        return -1;
    }
}
