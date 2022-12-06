using BenchmarkDotNet.Attributes;
using Perfolizer.Mathematics.Thresholds;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    [MemoryDiagnoser]

    public class AoCDay4
    {
        private readonly byte[] Input = File.ReadAllBytes(@"C:\Users\kaist\source\repos\AoC Day 2\input\day4download.txt");
        private readonly string[] inputStrings = File.ReadAllLines(@"C:\Users\kaist\source\repos\AoC Day 2\input\day4input.txt");
        [Benchmark]
        public void part1()
        {
            string[] pairs;
            string[] pair1;
            string[] pair2;
            int count = 0;
            foreach (string line in inputStrings)
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
        public void part1Optimized()
        {
            string[] pairs;
            string[] pair1;
            string[] pair2;
            int count = 0;
            foreach (string line in inputStrings)
            {
                pairs = line.Split(',');
                pair1 = pairs[0].Split('-');
                pair2 = pairs[1].Split('-');
                int r1 = int.Parse(pair1[0]);
                int r2 = int.Parse(pair1[1]);
                int r3 = int.Parse(pair2[0]);
                int r4 = int.Parse(pair2[1]);
                int pair1Range = r2 - r1;
                int pair2Range = r4 - r3;
                if (pair1Range <= pair2Range)
                {
                    if (r1 >= r3 && r2 <= r4)
                        count++;
                }

                else if (pair1Range >= pair2Range)
                {
                    if (r1 <= r3 && r2 >= r4)
                        count++;
                }
                else if (pair1Range == pair2Range)
                {
                    if (r1 == r3)
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
            int count = 0; 
            foreach (string line in inputStrings)
            {
                pairs = line.Split(',');
                pair1 = pairs[0].Split('-');
                pair2 = pairs[1].Split('-');

                if (Convert.ToInt32(pair1[0]) >= Convert.ToInt32(pair2[0]) && Convert.ToInt32(pair1[0]) <= Convert.ToInt32(pair2[1]))
                    count++;
                else if (Convert.ToInt32(pair2[0]) >= Convert.ToInt32(pair1[0]) && Convert.ToInt32(pair2[0]) <= Convert.ToInt32(pair1[1]))
                    count++;
            }
                //Console.WriteLine("Total dupes is " + count);

        }
        [Benchmark]
        public void part2Optimized()
        {
            string[] pairs;
            string[] pair1;
            string[] pair2;
            int count = 0; 
            foreach (string line in inputStrings)
            {
                pairs = line.Split(',');
                pair1 = pairs[0].Split('-');
                pair2 = pairs[1].Split('-');
                int r1 = int.Parse(pair1[0]);
                int r2 = int.Parse(pair1[1]);
                int r3 = int.Parse(pair2[0]);
                int r4 = int.Parse(pair2[1]);
                if (r2 >= r3 && r4 >= r1)
                    count++;
            }
               // Console.WriteLine("Total dupes is " + count);

        }
        [Benchmark]       
        public  void caisMethodPart1()
        {
            const byte HYPHEN = 0x2D;
            const byte COMMA = 0x2C;
            const byte NEWLINE = 0x0A;

            
            int OverlapsFound = 0;
            int Index = 0;
            do
            {
                int LeftMin = (Input[Index++] & 0xF);
                if (Input[Index] != HYPHEN) { LeftMin = (LeftMin * 10) + (Input[Index++] & 0xF); }
                Debug.Assert(Input[Index] == HYPHEN);
                Index++;

                int LeftMax = (Input[Index++] & 0xF);
                if (Input[Index] != COMMA) { LeftMax = (LeftMax * 10) + (Input[Index++] & 0xF); }
                Debug.Assert(Input[Index] == COMMA);
                Index++;

                int RightMin = (Input[Index++] & 0xF);
                if (Input[Index] != HYPHEN) { RightMin = (RightMin * 10) + (Input[Index++] & 0xF); }
                Debug.Assert(Input[Index] == HYPHEN);
                Index++;

                int RightMax = (Input[Index++] & 0xF);
                if (Input[Index] != NEWLINE) { RightMax = (RightMax * 10) + (Input[Index++] & 0xF); }
                Debug.Assert(Input[Index] == NEWLINE);
                Index++;

                if ((LeftMin <= RightMin && LeftMax >= RightMax) || (RightMin <= LeftMin && RightMax >= LeftMax)) { OverlapsFound++; }
            }
            while (Index != Input.Length);
            //Console.Write(OverlapsFound);
        }
        [Benchmark]
        public void caisMethodPart2()
        {
            const byte HYPHEN = 0x2D;
            const byte COMMA = 0x2C;
            const byte NEWLINE = 0x0A;

            int OverlapsFound = 0;
            int Index = 0;
            do
            {
                int LeftMin = (Input[Index++] & 0xF);
                if (Input[Index] != HYPHEN) { LeftMin = (LeftMin * 10) + (Input[Index++] & 0xF); }
                Debug.Assert(Input[Index] == HYPHEN);
                Index++;

                int LeftMax = (Input[Index++] & 0xF);
                if (Input[Index] != COMMA) { LeftMax = (LeftMax * 10) + (Input[Index++] & 0xF); }
                Debug.Assert(Input[Index] == COMMA);
                Index++;

                int RightMin = (Input[Index++] & 0xF);
                if (Input[Index] != HYPHEN) { RightMin = (RightMin * 10) + (Input[Index++] & 0xF); }
                Debug.Assert(Input[Index] == HYPHEN);
                Index++;

                int RightMax = (Input[Index++] & 0xF);
                if (Input[Index] != NEWLINE) { RightMax = (RightMax * 10) + (Input[Index++] & 0xF); }
                Debug.Assert(Input[Index] == NEWLINE);
                Index++;

                if (LeftMax >= RightMin && RightMax >= LeftMin) { OverlapsFound++; }
            }
            while (Index != Input.Length);
           // Console.Write(OverlapsFound);
        }
    }
    

}
