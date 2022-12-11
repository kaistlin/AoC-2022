using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_2.src
{
    [MemoryDiagnoser]
    public class AoCDay9
    {
        string inputPath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day9input.txt";
        public static readonly string samplePath = @"C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day9input.txt";
        string[] Input = File.ReadAllLines(samplePath);
        class Knot
        {
           public int x = 0;
           public int y = 0;
        }
        [Benchmark]
        public void part1()
        {
            int headX = 0;
            int headY = 0;
            int tailX = 0;
            int tailY = 0;
            int index = 0;
            
            Span<string> currentLine;
            HashSet<String> tailLocations = new HashSet<String>();
            tailLocations.Add("" + tailX + "," + tailY);
            do
            {
                currentLine = Input[index].Split(" ");
                moveHead(currentLine);
                index++;
            } while (index < Input.Length);
            Console.WriteLine(tailLocations.Count);
            void moveHead(Span<string> currentMoves)
            {
                // char direction = currentMoves[0];
                int amount = int.Parse(currentMoves[1]);
                switch (char.Parse(currentMoves[0]))
                {
                    case 'U': //Moving Up
                        for (int i = 0; i < amount; i++)
                        {
                            headY++;
                            if (headY - tailY > 1)
                            {
                                if (headX > tailX || headX < tailX)//check to see if head and tail are not in the same column
                                    tailX = headX;
                                tailY = headY - 1;
                                tailLocations.Add("" + tailX + "," + tailY);
                            }
                        }
                        break;
                    case 'D': //Moving Down
                        for (int i = 0; i < amount; i++)
                        {
                            headY--;
                            if (tailY - headY > 1)
                            {
                                if (headX > tailX || headX < tailX)//check to see if head and tail are not in the same column
                                    tailX = headX;
                                tailY = headY + 1;
                                tailLocations.Add("" + tailX + "," + tailY);
                            }
                        }
                        break;
                    case 'L': //Moving Left
                        for (int i = 0; i < amount; i++)
                        {
                            headX--;
                            if (tailX - headX > 1)
                            {
                                if (headY > tailY || headY < tailY)//check to see if head and tail are not in the same row
                                    tailY = headY;
                                tailX = headX + 1;
                                tailLocations.Add("" + tailX + "," + tailY);
                            }
                        }
                        break;
                    case 'R': //Moving Right
                        for (int i = 0; i < amount; i++)
                        {
                            headX++;
                            if (headX - tailX > 1)
                            {
                                if (headY > tailY || headY < tailY)//check to see if head and tail are not in the same row
                                    tailY = headY;
                                tailX = headX - 1;
                                tailLocations.Add("" + tailX + "," + tailY);
                            }
                        }
                        break;
                }

            }
        }
        [Benchmark]
        public void part2()
        {
            int index = 0;
            
            Knot[] knots = new Knot[10];
            for (int i = 0; i < 10; i++)
            {
                knots[i] = new Knot();
            }
            Span<string> currentLine;
            HashSet<String> tailLocations = new HashSet<String>();
            tailLocations.Add("" + knots[9].x + "," + knots[9].y);
            do
            {
                currentLine = Input[index].Split(" ");
                moveHead(currentLine);
                index++;
            } while (index < Input.Length);
            Console.WriteLine(tailLocations.Count);
            void moveHead(Span<string> currentMoves)
            {
                bool diagonal = false;
                int amount = int.Parse(currentMoves[1]);
                for (int i = 0; i < amount; i++)
                {
                    switch (char.Parse(currentMoves[0]))
                    {
                        case 'U': //Moving Up
                            knots[0].y++;
                            break;
                        case 'D': //Moving Down
                            knots[0].y--;
                            break;
                        case 'L': //Moving Left
                            knots[0].x--;
                            break;
                        case 'R': //Moving Right
                            knots[0].x++;
                            break;
                    }
                    //Update all other knots
                    for (int j = 0; j < 9; j++)
                    {
                        if (Math.Abs(knots[j].y - knots[j + 1].y) > 1 || Math.Abs(knots[j].x - knots[j + 1].x) > 1)
                        {
                            if (knots[j].x == knots[j + 1].x)//check if they're in the sam column. If so, just move 1 closer in the column
                            {
                                if (knots[j].y > knots[j + 1].y)
                                    knots[j + 1].y++;
                                else { knots[j + 1].y--; }


                            }
                            else if (knots[j].y == knots[j + 1].y)//check to see if they're in the same row. If so, move 1 closer in the row.
                            {
                                if (knots[j].x > knots[j + 1].x)
                                    knots[j + 1].x++;
                                else { knots[j + 1].x--; }
                            }
                            else//we now have to move diagonal
                            {
                                if (knots[j].x > knots[j + 1].x)
                                    knots[j + 1].x++;
                                else { knots[j + 1].x--; }

                                if (knots[j].y > knots[j + 1].y)
                                    knots[j + 1].y++;
                                else { knots[j + 1].y--; }
                            }

                        }
                        else { break; }
                    }
                    tailLocations.Add("" + knots[9].x + "," + knots[9].y);

                }

            }
        }
    }
}
