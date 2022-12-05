using Dia2Lib;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC
{
    public class AoCDay5
    {
        public void part1()
        {
            Stack[] stacks = new Stack[10];
            for (int i = 0; i < stacks.Length; i++)
            {
                stacks[i] = new Stack();
            }
            String[] board = new string[10];
            int k = 0;
            bool boardFinished = false;
            foreach (string line in System.IO.File.ReadLines(@"C:\Users\kaist\source\repos\AoC Day 2\day5input.txt"))
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

                            char test = lineChar[(j) * 4 + 1];
                            if (test != ' ')
                                stacks[j + 1].Push(lineChar[(j) * 4 + 1]);
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
                        for (int i = System.Convert.ToInt32(instr[1]); i > 0; i--)
                        {
                            stacks[System.Convert.ToInt32(instr[5])].Push(stacks[System.Convert.ToInt32(instr[3])].Pop());

                        }
                        
                    }

                }
            }

            Console.WriteLine("Done Moving!");

        }
        public void part2()
        {
            Stack[] stacks = new Stack[10];
            for (int i = 0; i < stacks.Length; i++)
            {
                stacks[i] = new Stack();
            }
            String[] board = new string[10];
            int k = 0;
            bool boardFinished = false;
            foreach (string line in System.IO.File.ReadLines(@"C:\Users\kaist\source\repos\AoC Day 2\day5input.txt"))
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

                            char test = lineChar[(j) * 4 + 1];
                            if (test != ' ')
                                stacks[j + 1].Push(lineChar[(j) * 4 + 1]);
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
                        for (int i = System.Convert.ToInt32(instr[1]); i > 0; i--)
                        {
                            stacks[0].Push(stacks[System.Convert.ToInt32(instr[3])].Pop());

                        }
                        for (int i = System.Convert.ToInt32(instr[1]); i > 0; i--)
                        {
                            stacks[System.Convert.ToInt32(instr[5])].Push(stacks[0].Pop());

                        }
                    }

                }
            }

            Console.WriteLine("Done Moving!");

        }
       
    }
}
