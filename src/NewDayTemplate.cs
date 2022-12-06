using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_2.src
{
    [MemoryDiagnoser]
    public class Day
    {
        string inputPath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day%input.txt";
        string samplePath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day%sample.txt";
        [Benchmark]
        public void part1(){ }
        [Benchmark]
        public void part2(){ }
    }
}
