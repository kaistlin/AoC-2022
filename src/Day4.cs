using BenchmarkDotNet.Attributes;
using Perfolizer.Mathematics.Thresholds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_2.src
{
    public class elf
    {
        int index;
        int start;
        int end;
        int range;

        public elf(int start, int end)
        {
            this.start = start;
            this.end = end;
        }
    }
    public class AoCDay4
    {
        [Benchmark]
        public void part1()
        {
            string[] pairs;
            string[] pair1;
            string[] pair2;
            int count = 0; ;
            foreach (string line in File.ReadLines(@"C:\Users\kaist\source\repos\AoC Day 2\input\day4input.txt"))
            {
                pairs = line.Split(',');
                pair1 = pairs[0].Split('-');
                pair2 = pairs[1].Split('-');
                int pair1Range = Convert.ToInt32(pair1[1]) - Convert.ToInt32(pair1[0]);
                int pair2Range = Convert.ToInt32(pair2[1]) - Convert.ToInt32(pair2[0]);
                if (pair1Range <= pair2Range)
                {
                    if (Convert.ToInt32(pair1[0]) >= Convert.ToInt32(pair2[0]) && Convert.ToInt32(pair1[1]) <= Convert.ToInt32(pair2[1]))
                        count++;
                }

                else if (pair1Range >= pair2Range)
                {
                    if (Convert.ToInt32(pair1[0]) <= Convert.ToInt32(pair2[0]) && Convert.ToInt32(pair1[1]) >= Convert.ToInt32(pair2[1]))
                        count++;
                }
                else if (pair1Range == pair2Range)
                {
                    if (Convert.ToInt32(pair1[0]) == Convert.ToInt32(pair2[0]) && Convert.ToInt32(pair1[1]) == Convert.ToInt32(pair2[1]))
                        count++;
                }

            }
            // Console.WriteLine("Total pairs is " + count);

        }
        [Benchmark]
        public void part2()
        {
            string[] pairs;
            string[] pair1;
            string[] pair2;
            int count = 0; ;
            foreach (string line in File.ReadLines(@"C:\Users\kaist\source\repos\AoC Day 2\input\day4input.txt"))
            {
                pairs = line.Split(',');
                pair1 = pairs[0].Split('-');
                pair2 = pairs[1].Split('-');

                if (Convert.ToInt32(pair1[0]) >= Convert.ToInt32(pair2[0]) && Convert.ToInt32(pair1[0]) <= Convert.ToInt32(pair2[1]))
                    count++;
                else if (Convert.ToInt32(pair2[0]) >= Convert.ToInt32(pair1[0]) && Convert.ToInt32(pair2[0]) <= Convert.ToInt32(pair1[1]))
                    count++;
            }
            //    Console.WriteLine("Total dupes is " + count);

        }
    }
}
