using BenchmarkDotNet.Attributes;

namespace AoC
{
    public class AoCDay3
    {
        [Benchmark]
        public void run()
        {
            int sum = 0;
            char[]? prevline1 = null;
            char[]? prevline2 = null;
            char[]? prevline3 = null;
            int i = 0;
            foreach (string line in System.IO.File.ReadLines(@"C:\Users\kaist\source\repos\AoC Day 2\day3input.txt"))
            {
                if (i%3 == 0)
                    prevline1 = line.ToCharArray();
                else if (i%3 == 1)
                    prevline2 = line.ToCharArray();
                else if (i%3 == 2)
                {
                    prevline3 = line.ToCharArray();
                    char unique = prevline1.Intersect(prevline2).Intersect(prevline3).ToArray()[0];
                    if (char.IsUpper(unique))
                    {
                        sum += System.Convert.ToInt32(unique) - 38;
                    }
                    else if (char.IsLower(unique))
                    {
                        sum += System.Convert.ToInt32(unique) - 96;
                    }   
                }
                i++;
            }
         //   Console.WriteLine(sum);
        }
    }
}
