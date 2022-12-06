using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_2.src
{
    [MemoryDiagnoser]
    public class Day6
    {
        string inputPath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day6input.txt";
        string samplePath = "@C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day6sample.txt";
        int i = 14;
        string[] signalString = File.ReadAllLines(@"C:\Users\kaist\source\repos\AoC Day 2\input\day6input.txt");
        byte[] signal = File.ReadAllBytes(@"C:\Users\kaist\source\repos\AoC Day 2\input\day6input.txt");
        byte[] test = new byte[14];
        [Benchmark]
        public void part1()
        {    do
                {
                    if (signal[i] != signal[i + 1] && signal[i] != signal[i + 2] && signal[i] != signal[i + 3] && signal[i + 1] != signal[i + 2] && signal[i + 1] != signal[i + 3] && signal[i + 2] != signal[i + 3])
                    {
                        goto UniqueFound;
                    }
                    i++;
                } while (i != signal.Length);

            UniqueFound:
                Console.Write(i + 4);

           
        }
        [Benchmark]
        public void part2(){ 
           for(int j = 0; j < 14; j++)
            {
                test[j] = signal[j];
            }
            do
            {
                if (test.Distinct().Count() == test.Length)
                    goto uniqueFourteenFound;
                test[i % 14] = signal[i++];
            }while(i != signal.Length);

        uniqueFourteenFound:
            Console.Write(i);
        
        }
        
    }
}
