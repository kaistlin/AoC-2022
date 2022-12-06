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
            int i = 0;
            while (signal[i] == signal[i + 1] ||
                     signal[i] == signal[i + 2] ||
                     signal[i] == signal[i + 3] ||
                     signal[i + 1] == signal[i + 2]||
                     signal[i + 1] == signal[i + 3]||
                     signal[i + 2] == signal[i + 3]) { i++; }

           // Console.Write(i + 4);
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

         //  Console.Write(i + 4);

        }
        [Benchmark]
        public void part1Alternative()
        {
            byte[] test1 = new byte[4];
            int i = 0;
            do
            {
                test1[i%4] = signal[i++];
            } while (test1.Distinct().Count() != test1.Length);

              // Console.Write(i + 3);
        }
        [Benchmark]
        public void part2()
        {
            byte[] test = new byte[14];
            int i = 0;
            do
            {
                test[i % 14] = signal[i++];
            }while(test.Distinct().Count() != test.Length);
          //  Console.Write(i);
        
        }
        public void part2Hash()
        {

        }

        
    }
}
