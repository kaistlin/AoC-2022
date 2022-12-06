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
        byte[] test = new byte[14];
        int i;
        byte[] test1;
        [Benchmark]
        public void part1()
        {
            i = -1;
            do
            {
                i++;
            } while (signal[i] == signal[i + 1] || signal[i] == signal[i + 2] || signal[i] == signal[i + 3] || signal[i + 1] == signal[i + 2] || signal[i + 1] == signal[i + 3] || signal[i + 2] == signal[i + 3]);

            //   Console.Write(i + 4);
        }
        [Benchmark]
        public void part1Alternative()
        {
            i = 0;
            do
            {
                test1 = signal[i..((i++)+4)];
            } while (test1.Distinct().Count() != test1.Length);

               Console.Write(i + 3);
        }
        [Benchmark]
        public void part2(){
            i = 0;
            do
            {
                test[i % 14] = signal[i++];
            }while(test.Distinct().Count() != test.Length);
          //  Console.Write(i);
        
        }
        
    }
}
