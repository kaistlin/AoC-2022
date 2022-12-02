using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes;
namespace AoC
{
    [MemoryDiagnoser]
    public class AoCDay2
    {
        [Benchmark]
        public void ifsonly()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\kaist\source\repos\AoC Day 2\day2input.txt");
            int[] scores = new int[10];
            foreach (string line in lines)
            {

                if (line == "A Y")
                {
                    scores[4] += 1;
                }
                if (line == "A X")
                {
                    scores[3] += 1;
                }
                if (line == "A Z")
                {
                    scores[8] += 1;
                }
                if (line == "B Y")
                {
                    scores[5] += 1;
                }
                if (line == "B X")
                {
                    scores[1] += 1;
                }
                if (line == "B Z")
                {
                    scores[9] += 1;
                }
                if (line == "C Y")
                {
                    scores[6] += 1;
                }
                if (line == "C X")
                {
                    scores[2] += 1;
                }
                if (line == "C Z")
                {
                    scores[7] += 1;
                }

            }
            //Console.WriteLine("\t" + "Done adding scores" + "\t");

            int n = 0;
            for (int j = 1; j < 10; j++)
            {
                n += scores[j] * j;
            }

             Console.WriteLine("The total score is " + n + " points");

        }
        [Benchmark]
        public void ifElse()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\kaist\source\repos\AoC Day 2\day2input.txt");
            int[] scores = new int[10];
            foreach (string line in lines)
            {

                if (line == "A Y")
                {
                    scores[4] += 1;
                }
                else if (line == "A X")
                {
                    scores[3] += 1;
                }
                else if (line == "A Z")
                {
                    scores[8] += 1;
                }
                else if (line == "B Y")
                {
                    scores[5] += 1;
                }
                else if (line == "B X")
                {
                    scores[1] += 1;
                }
                else if (line == "B Z")
                {
                    scores[9] += 1;
                }
                else if (line == "C Y")
                {
                    scores[6] += 1;
                }
                else if (line == "C X")
                {
                    scores[2] += 1;
                }
                if (line == "C Z")
                {
                    scores[7] += 1;
                }

            }
            //Console.WriteLine("\t" + "Done adding scores" + "\t");

            int n = 0;
            for (int j = 1; j < 10; j++)
            {
                n += scores[j] * j;
            }

             Console.WriteLine("The total score is " + n + " points");

        }
        [Benchmark]
        public void linq()
        {
            string[] games = System.IO.File.ReadAllLines(@"C:\Users\kaist\source\repos\AoC Day 2\day2input.txt");
            int[] scores = new int[10];
            var gameSet = from game in games where game.ToString() == "A Y" select game;
            scores[4] = gameSet.Count();
            gameSet = from game in games where game.ToString() == "A X" select game;
            scores[3] = gameSet.Count();
            gameSet = from game in games where game.ToString() == "A Z" select game;
            scores[8] = gameSet.Count();
            gameSet = from game in games where game.ToString() == "B Y" select game;
            scores[5] = gameSet.Count();
            gameSet = from game in games where game.ToString() == "B X" select game;
            scores[1] = gameSet.Count();
            gameSet = from game in games where game.ToString() == "B Z" select game;
            scores[9] = gameSet.Count();
            gameSet = from game in games where game.ToString() == "C Y" select game;
            scores[6] = gameSet.Count();
            gameSet = from game in games where game.ToString() == "C X" select game;
            scores[2] = gameSet.Count();
            gameSet = from game in games where game.ToString() == "C Z" select game;
            scores[7] = gameSet.Count();

            //Console.WriteLine("\t" + "Done adding scores" + "\t");

            int n = 0;
            for (int j = 1; j < 10; j++)
            {
                n += scores[j] * j;
            }

             Console.WriteLine("The total score is " + n + " points");
            
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
              var summary = BenchmarkRunner.Run<AoCDay2>();
            // Console.WriteLine(summary);
            //AoCDay2 day2 = new AoCDay2();
            //day2.linq();
        }
    }
    
}