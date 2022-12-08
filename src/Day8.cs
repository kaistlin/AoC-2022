using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_2.src
{
    [MemoryDiagnoser]
    public class AoCDay8
    {
        string inputPath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day8input.txt";
        public readonly string samplePath = @"C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day8sample.txt";
        public byte[] Input = File.ReadAllBytes(@"C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day8input.txt");
        //
        [Benchmark]
        public void part1()
        {

            int index = 0;
            // int[] views = {(s-1),(s-1),(s-1),(s-1)};
            int s = (int)Math.Sqrt(Input.Length) + 1;
            int[,] treeHeightMap = new int[s + 1, s + 1];

            //buildmap           
            do
            {

                treeHeightMap[(int)((index / s) + 1), (index % s) + 1] = Input[index++] - 48;
                if ((index + 1) % (s) == 0)
                    index++;
            } while (index < Input.Length - 1);
            
            index = s + 1;
            int cell = 0;
            bool visible = true;
            int visibleSum = 4 * s - 8;
            

            for (int row = 2; row < 99; row++)
            {
                for (int col = 2; col < 99; col++)
                {

                    visible = true;
                    cell = treeHeightMap[row, col];
                    //check north
                    for (int i = 1; i < row + 1; i++)
                    {
                        if (treeHeightMap[row - i, col] >= cell)
                        {
                            visible = false;
                            break;
                        }
                    }
                    if (visible)
                    {
                        visibleSum++;
                        goto TreeSeen;
                    }

                    visible = true;
                    //check east
                    for (int i = 1; i < (s - col); i++)
                    {
                        if (treeHeightMap[row, (col) + i] >= cell)
                        {
                            visible = false;
                            break;
                        }
                    }
                    if (visible)
                    {
                        visibleSum++;
                        goto TreeSeen;
                    }

                    visible = true;
                    //check south
                    for (int i = 1; i < (s - row); i++)
                    {
                        if (treeHeightMap[row + i, col] >= cell)
                        {
                            visible = false;
                            break;
                        }
                    }
                    if (visible)
                    {
                        visibleSum++;
                        goto TreeSeen;
                    };
                    visible = true;
                    //check west
                    for (int i = 1; i < col; i++)
                    {
                        if (treeHeightMap[row, (col) - i] >= cell)
                        {
                            visible = false;
                            break;
                        }
                    }
                    if (visible)
                    {
                        visibleSum++;
                        goto TreeSeen;
                    }
                TreeSeen:
                    visible = true;
                }
            }
            
           // Console.WriteLine(visibleSum);
        }
        [Benchmark]
        public void part2()
        {

            int index = 0;
            // int[] views = {(s-1),(s-1),(s-1),(s-1)};
            int s = (int)Math.Sqrt(Input.Length) + 1;
            int[,] treeHeightMap = new int[s + 1, s + 1];

            //buildmap           
            do
            {
                treeHeightMap[(int)((index / s) + 1), (index % s) + 1] = Input[index++] - 48;
                if ((index + 1) % (s) == 0)
                    index++;
            } while (index < Input.Length - 1);

            int cell = 0;
            int[] visible = new int[4];
            int highVisible = 0; ;

            for (int row = 2; row < 99; row++)
            {
                for (int col = 2; col < 99; col++)
                {
                    cell = treeHeightMap[row, col];
                    //check north
                    for (int i = 1; i < row; i++)
                    {
                        visible[0] = i;
                        if (treeHeightMap[row - i, col] >= cell)
                            break;
                    }

                    //check east
                    for (int i = 1; i < (s - col); i++)
                    {
                        visible[1] = i;
                        if (treeHeightMap[row, (col) + i] >= cell)
                            break;
                    }




                    //check south
                    for (int i = 1; i < (s - row); i++)
                    {
                        visible[2] = i;
                        if (treeHeightMap[row + i, col] >= cell)
                            break;
                    }
                    //check west
                    for (int i = 1; i < col; i++)
                    {
                        visible[3] = i;
                        if (treeHeightMap[row, (col) - i] >= cell)
                            break;
                    }
                    if ((visible[0] * visible[1] * visible[2] * visible[3]) > highVisible)
                        highVisible = visible[0] * visible[1] * visible[2] * visible[3];


                }
            }

            //  Console.WriteLine(highVisible);
        }
        [Benchmark]
        public void part2Parallel()
        {

            int index = 0;
            // int[] views = {(s-1),(s-1),(s-1),(s-1)};
            int s = (int)Math.Sqrt(Input.Length) + 1;
            int[,] treeHeightMap = new int[s + 1, s + 1];

            //buildmap           
            do
            {
                treeHeightMap[(int)((index / s) + 1), (index % s) + 1] = Input[index++] - 48;
                if ((index + 1) % (s) == 0)
                    index++;
            } while (index < Input.Length - 1);

            int cell = 0;
            int[] visible = new int[4];
            int highVisible = 0; ;
            int visibleN = 0;
            int visibleE = 0;
            int visibleS = 0;
            int visibleW = 0;

            for (int row = 2; row < 99; row++)
            {
                for (int col = 2; col < 99; col++)
                {
                    cell = treeHeightMap[row, col];
                    Parallel.Invoke(() =>
                    { //check north
                        for (int i = 1; i < row; i++)
                        {
                            visibleN = i;
                            if (treeHeightMap[row - i, col] >= cell)
                                break;
                        }
                    },
                    () =>
                    {
                        //check east
                        for (int j = 1; j < (s - col); j++)
                        {
                            visibleE = j;
                            if (treeHeightMap[row, (col) + j] >= cell)
                                break;
                        }
                    }, () =>



                    { //check south
                        for (int k = 1; k < (s - row); k++)
                        {
                            visibleS = k;
                            if (treeHeightMap[row + k, col] >= cell)
                                break;
                        }
                    },
                    () =>
                    {
                        //check west
                        for (int l = 1; l < col; l++)
                        {
                            visibleW = l;
                            if (treeHeightMap[row, (col) - l] >= cell)
                                break;
                        }
                    }
                    );
                    visible[0] = visibleN;
                    visible[1] = visibleN;
                    visible[2] = visibleN;
                    visible[3] = visibleN;
                    if ((visible[0] * visible[1] * visible[2] * visible[3]) > highVisible)
                        highVisible = visible[0] * visible[1] * visible[2] * visible[3];


                }
            }

            //  Console.WriteLine(highVisible);
        }
        /**[Benchmark]
        public void part2Recursion()
        {

            int index = 0;
            // int[] views = {(s-1),(s-1),(s-1),(s-1)};
            int s = (int)Math.Sqrt(Input.Length) + 1;
            int[,] treeHeightMap = new int[s + 1, s + 1];

            //buildmap           
            do
            {
                treeHeightMap[(int)((index / s) + 1), (index % s) + 1] = Input[index++] - 48;
                if ((index + 1) % (s) == 0)
                    index++;
            } while (index < Input.Length - 1);

            int cell = 0;
            int[] visible = new int[4];
            int highVisible = 0; ;

            for (int row = 2; row < 99; row++)
            {
                for (int col = 2; col < 99; col++)
                {
                    cell = treeHeightMap[row, col];
                    //check north
                    for (int i = 1; i < row; i++)
                    {
                        visible[0] = i;
                        if (treeHeightMap[row - i, col] >= cell)
                            break;
                    }

                    //check east
                    for (int j = 1; i < (s - col); i++)
                    {
                        visible[1] = i;
                        if (treeHeightMap[row, (col) + i] >= cell)
                            break;
                    }




                    //check south
                    for (int k = 1; i < (s - row); i++)
                    {
                        visible[2] = i;
                        if (treeHeightMap[row + i, col] >= cell)
                            break;
                    }
                    //check west
                    for (int l = 1; i < col; i++)
                    {
                        visible[3] = i;
                        if (treeHeightMap[row, (col) - i] >= cell)
                            break;
                    }
                    if ((visible[0] * visible[1] * visible[2] * visible[3]) > highVisible)
                        highVisible = visible[0] * visible[1] * visible[2] * visible[3];


                }
            }

            //  Console.WriteLine(highVisible);
        }
        //[Benchmark]
        public void part2RecursionParallel()
        {

            int index = 0;
            // int[] views = {(s-1),(s-1),(s-1),(s-1)};
            int s = (int)Math.Sqrt(Input.Length) + 1;
            int[,] treeHeightMap = new int[s + 1, s + 1];

            //buildmap           
            do
            {
                treeHeightMap[(int)((index / s) + 1), (index % s) + 1] = Input[index++] - 48;
                if ((index + 1) % (s) == 0)
                    index++;
            } while (index < Input.Length - 1);

            int cell = 0;
            int[] visible = new int[4];
            int highVisible = 0; ;

            for (int row = 2; row < 99; row++)
            {
                for (int col = 2; col < 99; col++)
                {
                    cell = treeHeightMap[row, col];
                    //check north
                    for (int i = 1; i < row; i++)
                    {
                        visible[0] = i;
                        if (treeHeightMap[row - i, col] >= cell)
                            break;
                    }

                    //check east
                    for (int j = 1; i < (s - col); i++)
                    {
                        visible[1] = i;
                        if (treeHeightMap[row, (col) + i] >= cell)
                            break;
                    }




                    //check south
                    for (int k = 1; i < (s - row); i++)
                    {
                        visible[2] = i;
                        if (treeHeightMap[row + i, col] >= cell)
                            break;
                    }
                    //check west
                    for (int l = 1; i < col; i++)
                    {
                        visible[3] = i;
                        if (treeHeightMap[row, (col) - i] >= cell)
                            break;
                    }
                    if ((visible[0] * visible[1] * visible[2] * visible[3]) > highVisible)
                        highVisible = visible[0] * visible[1] * visible[2] * visible[3];


                }
            }

            //  Console.WriteLine(highVisible);
        }**/
    }
}
