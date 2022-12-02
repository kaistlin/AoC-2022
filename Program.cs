using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace AoC
{
    class AoCDay1
    {
        static void Main()
        {
            string[] lines = System.IO.File.ReadAllLines
            (@"C:\Users\kaist\source\repos\AoC Day 2\day2input.txt");
            int[] scores = new int[10];
            int i = 0;
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
            Console.WriteLine("\t" + "Done adding scores" + "\t");

            int n = 0;
            for(int j=1;j<10;j++)
            {
                n += scores[j] * j;
            }

            Console.WriteLine("The total score is " + n + " points");
            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }
    }
}