using Perfolizer.Mathematics.Thresholds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC
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
        public void part1()
        {
            string[] pairs;
            string[] pair1;
            string[] pair2;
            int count = 0; ;
            foreach (string line in System.IO.File.ReadLines(@"C:\Users\kaist\source\repos\AoC Day 2\day4sample.txt"))
            {
                pairs = line.Split(',');
                pair1 = pairs[0].Split('-');
                pair2 = pairs[1].Split('-');
                int pair1Range = System.Convert.ToInt32(pair1[1]) - System.Convert.ToInt32(pair1[0]);
                int pair2Range = System.Convert.ToInt32(pair2[1]) - System.Convert.ToInt32(pair2[0]);
                if (pair1Range <= pair2Range)
                {
                    if (System.Convert.ToInt32(pair1[0]) >= System.Convert.ToInt32(pair2[0]) && System.Convert.ToInt32(pair1[1]) <= System.Convert.ToInt32(pair2[1]))
                        count++;
                }

                else if (pair1Range >= pair2Range)
                {
                    if (System.Convert.ToInt32(pair1[0]) <= System.Convert.ToInt32(pair2[0]) && System.Convert.ToInt32(pair1[1]) >= System.Convert.ToInt32(pair2[1]))
                        count++;
                }
                else if (pair1Range == pair2Range)
                {
                    if (System.Convert.ToInt32(pair1[0]) == System.Convert.ToInt32(pair2[0]) && System.Convert.ToInt32(pair1[1]) == System.Convert.ToInt32(pair2[1]))
                        count++;
                }

            }
            Console.WriteLine("Total pairs is " + count);

        }
        public void part2()
        {
            string[] pairs;
            string[] pair1;
            string[] pair2;
            int count = 0; ;
            foreach (string line in System.IO.File.ReadLines(@"C:\Users\kaist\source\repos\AoC Day 2\day4input.txt"))
            {
                pairs = line.Split(',');
                pair1 = pairs[0].Split('-');
                pair2 = pairs[1].Split('-');

                if (System.Convert.ToInt32(pair1[0]) >= System.Convert.ToInt32(pair2[0]) && System.Convert.ToInt32(pair1[0]) <= System.Convert.ToInt32(pair2[1]))
                    count++;
                else if (System.Convert.ToInt32(pair2[0]) >= System.Convert.ToInt32(pair1[0]) && System.Convert.ToInt32(pair2[0]) <= System.Convert.ToInt32(pair1[1]))
                    count++;
            }
            Console.WriteLine("Total dupes is " + count);

        }
    }
}
