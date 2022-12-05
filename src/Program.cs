using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes;

namespace AoC_Day_2.src
{
    [MemoryDiagnoser]
    public class AoCDay2
    {
        [Benchmark]
        public void ifsonly()
        {
            string[] lines = File.ReadAllLines(@"C:\Users\kaist\source\repos\AoC Day 2\input\day2input.txt");
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

            //   Console.WriteLine("The total score is " + n + " points");

        }
        [Benchmark]
        public void ifElse()
        {
            string[] lines = File.ReadAllLines(@"C:\Users\kaist\source\repos\AoC Day 2\input\day2input.txt");
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

            //    Console.WriteLine("The total score is " + n + " points");

        }
        [Benchmark]
        public void simplerIfElse()
        {
            int score = 0;
            string[] lines = File.ReadAllLines(@"C:\Users\kaist\source\repos\AoC Day 2\input\day2input.txt");
            foreach (string line in lines)
            {

                if (line == "A Y")
                {
                    score += 4;
                }
                else if (line == "A X")
                {
                    score += 3;
                }
                else if (line == "A Z")
                {
                    score += 8;
                }
                else if (line == "B Y")
                {
                    score += 5;
                }
                else if (line == "B X")
                {
                    score += 1;
                }
                else if (line == "B Z")
                {
                    score += 9;
                }
                else if (line == "C Y")
                {
                    score += 6;
                }
                else if (line == "C X")
                {
                    score += 2;
                }
                if (line == "C Z")
                {
                    score += 7;
                }

            }
            //Console.WriteLine("\t" + "Done adding scores" + "\t");



            //  Console.WriteLine("The total score is " + score + " points");

        }
        [Benchmark]
        public void linq()
        {
            string[] games = File.ReadAllLines(@"C:\Users\kaist\source\repos\AoC Day 2\input\day2input.txt");
            int[] scores = new int[10];
            int n = 0;
            n += games.Count(round => round == "A Y") * 4;
            n += games.Count(round => round == "A X") * 3;
            n += games.Count(round => round == "A Z") * 8;
            n += games.Count(round => round == "B Y") * 5;
            n += games.Count(round => round == "B X") * 1;
            n += games.Count(round => round == "B Z") * 9;
            n += games.Count(round => round == "C Y") * 6;
            n += games.Count(round => round == "C X") * 2;
            n += games.Count(round => round == "C Z") * 7;

            //Console.WriteLine("\t" + "Done adding scores" + "\t");



            // Console.WriteLine("The total score is " + n + " points");

        }
        public const byte L = 0, D = 3, W = 6;
        ReadOnlySpan<byte> SCORE_CHANGE => new byte[]
        {
        //L   D    W  <- My result
        3+L, 1+D, 2+W, 0, // They play rock
        1+L, 2+D, 3+W, 0, // They play paper
        2+L, 3+D, 1+W, 0, // They play scissors
        };
        [Benchmark]
        public void caisMethod()
        {
            byte[] Input = File.ReadAllBytes(@"C:\Users\kaist\source\repos\AoC Day 2\input\day2input.txt");
            int Score = 0;
            for (int Index = 0; Index < Input.Length; Index += 5)
            {
                int TheirMove = Input[Index] - 'A';
                int TurnResult = Input[Index + 2] - 'X';
                //int scoreChange = (TheirMove << 2 | TurnResult);
                Score += SCORE_CHANGE[TheirMove << 2 | TurnResult];
            }
            //Console.Write(Score);
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<AoCDay2>();
            // Console.WriteLine(summary);
            //AoCDay2 day2 = new AoCDay2();

            AoCDay5 test = new AoCDay5();
            test.part1();
            test.part2();
        }
    }

}

