using BenchmarkDotNet.Attributes;

namespace AoC_Day_2.src;

[MemoryDiagnoser]
public class AoCDay3
{
    private const string inputFile = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day3input.txt";
    private readonly string[] inputStrings = File.ReadAllLines(inputFile);
    [Benchmark]
    public void looseArrays()
    {
        int sum = 0;
        char[]? prevline1 = null;
        char[]? prevline2 = null;
        char[]? prevline3 = null;
        int i = 0;
        foreach (string line in inputStrings)
        {
            if (i % 3 == 0)
                prevline1 = line.ToCharArray();
            else if (i % 3 == 1)
                prevline2 = line.ToCharArray();
            else if (i % 3 == 2)
            {
                prevline3 = line.ToCharArray();
                char unique = prevline1.Intersect(prevline2).Intersect(prevline3).ToArray()[0];
                if (char.IsUpper(unique))
                {
                    sum += Convert.ToInt32(unique) - 38;
                }
                else if (char.IsLower(unique))
                {
                    sum += Convert.ToInt32(unique) - 96;
                }
            }
            i++;
        }
        //   Console.WriteLine(sum);
    }
    [Benchmark]
    public void staticArrays()
    {
        int sum = 0;
        char[] prevline1 = new char[100];
        char[] prevline2 = new char[100];
        char[] prevline3 = new char[100];
        int i = 0;
        foreach (string line in inputStrings)
        {
            if (i % 3 == 0)
                prevline1 = line.ToCharArray();
            else if (i % 3 == 1)
                prevline2 = line.ToCharArray();
            else if (i % 3 == 2)
            {
                prevline3 = line.ToCharArray();
                char unique = prevline1.Intersect(prevline2).Intersect(prevline3).ToArray()[0];
                if (char.IsUpper(unique))
                {
                    sum += Convert.ToInt32(unique) - 38;
                }
                else if (char.IsLower(unique))
                {
                    sum += Convert.ToInt32(unique) - 96;
                }
            }
            i++;
        }
        //   Console.WriteLine(sum);
    }
}
