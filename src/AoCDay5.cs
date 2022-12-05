using BenchmarkDotNet.Attributes;
using Dia2Lib;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_2.src
{
    [MemoryDiagnoser]
    public class AoCDay5
    {
        [Benchmark]
        public void part1()
        {
            Stack[] stacks = new Stack[10];
            for (int i = 0; i < stacks.Length; i++)
            {
                stacks[i] = new Stack();
            }
            string[] board = new string[10];
            int k = 0;
            var stackCount = 0;
            bool boardFinished = false;
            foreach (string line in File.ReadLines(@"C:\Users\kaist\source\repos\AoC Day 2\input\day5input.txt"))
            {
                //check we are still getting board input and copy string to array
                if (!boardFinished && !line.StartsWith(" 1"))
                {
                    board[k] = line;
                    k++;
                }
                //check if we've reached the end of the board input
                if (!boardFinished && line.StartsWith(" 1 "))
                {
                    //get # of stacks
                    stackCount = line.ToCharArray()[line.Length - 2] - '0';
                    for (int m = k - 1; m >= 0; m--)
                    {
                        char[] lineChar = board[m].ToCharArray();
                        for (int j = stackCount - 1; j >= 0; j--)
                        {

                            char test = lineChar[j * 4 + 1];
                            if (test != ' ')
                                stacks[j].Push(lineChar[j * 4 + 1]);
                        }
                    }
                    boardFinished = true;
                    continue;

                }

                if (boardFinished)
                {
                    if (line != "")
                    {
                        string[] instr = line.Split(' ');
                        for (int i = Convert.ToInt32(instr[1]); i > 0; i--)
                        {
                            stacks[Convert.ToInt32(instr[5])].Push(stacks[Convert.ToInt32(instr[3])].Pop());

                        }

                    }

                }
            }

            //Console.WriteLine("Done Moving!");

        }
        [Benchmark]
        public void part2()
        {
            Stack[] stacks = new Stack[10];
            for (int i = 0; i < stacks.Length; i++)
            {
                stacks[i] = new Stack();
            }
            string[] board = new string[10];
            int k = 0;
            bool boardFinished = false;
            foreach (string line in File.ReadLines(@"C:\Users\kaist\source\repos\AoC Day 2\input\day5input.txt"))
            {
                if (k < 10)
                {
                    board[k] = line;
                    k++;
                }
                if (k == 10)
                {
                    for (int m = 7; m >= 0; m--)
                    {
                        char[] lineChar = board[m].ToCharArray();
                        for (int j = 8; j >= 0; j--)
                        {

                            char test = lineChar[j * 4 + 1];
                            if (test != ' ')
                                stacks[j + 1].Push(lineChar[j * 4 + 1]);
                        }
                    }
                    boardFinished = true;
                    k++;
                }

                if (boardFinished)
                {
                    if (line != "")
                    {
                        string[] instr = line.Split(' ');
                        for (int i = Convert.ToInt32(instr[1]); i > 0; i--)
                        {
                            stacks[0].Push(stacks[Convert.ToInt32(instr[3])].Pop());

                        }
                        for (int i = Convert.ToInt32(instr[1]); i > 0; i--)
                        {
                            stacks[Convert.ToInt32(instr[5])].Push(stacks[0].Pop());

                        }
                    }

                }
            }

            //Console.WriteLine("Done Moving!");

        }

    }
}
