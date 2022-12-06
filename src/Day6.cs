using BenchmarkDotNet.Attributes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_2.src
{
    [MemoryDiagnoser]
    public class AoCDay6
    {
        
        byte[] signal = File.ReadAllBytes(@"C:\Users\kaist\source\repos\AoC Day 2\input\day6input.txt");
        [Benchmark]
        public void part1()
        {
            byte[] test1 = new byte[4];
            int i = 3;
            while (signal[i] == signal[i - 1] ||
                     signal[i] == signal[i - 2] ||
                     signal[i] == signal[i - 3] ||
                     signal[i - 1] == signal[i - 2]||
                     signal[i - 1] == signal[i - 3]||
                     signal[i - 2] == signal[i - 3]) { i++; }

           // Console.WriteLine(i+1);
        }
        [Benchmark]
        public void part1BooleanAddition()
        {
            byte[] test1 = new byte[4];
            int i = 0;
            while ( (((signal[i] == signal[i + 1]) ? 1 : 0) +
                    ((signal[i] == signal[i + 2]) ? 1 : 0) +
                    ((signal[i] == signal[i + 3]) ? 1 : 0) +
                    ((signal[i + 1] == signal[i + 2]) ? 1 : 0) +
                    ((signal[i + 1] == signal[i + 3]) ? 1 : 0) +
                    ((signal[i + 2] == signal[i + 3]) ? 1 : 0)) > 0) {i++; }

           //Console.WriteLine(i + 4);

        }
        [Benchmark]
        public void part1Alternative()
        {
            byte[] test1 = new byte[4];
            int i = 0;
            test1[i] = signal[i++];
            test1[i] = signal[i++];
            test1[i] = signal[i++];
            do
            {
                test1[i%4] = signal[i++];
            } while (test1.Distinct().Count() != test1.Length);

               //Console.WriteLine(i);
        }
        [Benchmark]
        public void part2()
        {
            byte[] test = new byte[14];
            int i = 0;
            for (int j = 0; j < 13; j++)
                test[j] = signal[i++];
            do
            {
                test[i % 14] = signal[i++];
            } while(test.Distinct().Count() != test.Length);
            //Console.WriteLine(i);
        
        }
        [Benchmark]
        public void part2HashSet()
        {
            HashSet<byte> uniqueHash = new HashSet<byte>(14);
            byte[] test = new byte[14];
            bool unique = false;
            int i = 0;
            for (int j = 0; j < 13; j++)
                test[j] = signal[i++];
            do
            {
                unique = true;
                uniqueHash.Clear();
                test[i % 14] = signal[i++];

                foreach (byte b in test)
                    if (uniqueHash.Add(b))
                        continue;
                    else
                    {
                        unique = false;
                        break;
                    }
            } while (!unique);
            //Console.WriteLine(i);

        }
        [Benchmark]
        public void part2HashMap()
        {
            Dictionary<byte, byte?> uniqueHash = new Dictionary<byte, byte?>(14);
            byte[] test = new byte[14];
            bool unique = false;
            int i = 0;
            for (int j = 0; j < 13; j++)
                test[j] = signal[i++];
            do
            {
                unique = true;
                uniqueHash.Clear();
                test[i % 14] = signal[i++];

                foreach (byte b in test)
                    if (uniqueHash.TryAdd(b, null))
                        continue;
                    else
                    {
                        unique = false;
                        break;
                    }
            } while (!unique);
            //Console.WriteLine(i);

        }
        [Benchmark]
        public void part2HashMapDiffCheck()
        {
            Dictionary<byte, byte?> uniqueHash = new Dictionary<byte, byte?>(14);
            byte[] test = new byte[14];
            bool unique = false;
            int i = 0;
            for (int j = 0; j < 13; j++)
                test[j] = signal[i++];
            do
            {
                unique = true; 
                uniqueHash.Clear();
                test[i % 14] = signal[i++];

                foreach (byte b in test)
                    if (uniqueHash.ContainsKey(b))
                    {
                        unique = false;
                        break;
                    }
                    else
                    {
                        uniqueHash.Add(b, null);
                    }
                    
                        
                       
            } while (!unique);
            //Console.WriteLine(i);

        }

    }
}
