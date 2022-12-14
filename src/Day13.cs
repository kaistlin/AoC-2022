using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_2.src;

[MemoryDiagnoser]
public class AoCDay13
{
    public readonly static string InputPath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day13input.txt";
    public readonly static string SamplePath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day13sample.txt";
    string[] InputLines = File.ReadAllLines(SamplePath);
   /** [Benchmark]
    public void Part1(){
        int i = 0;
        do
        {

            string[] LeftSide = InputLines[i].Split("]", StringSplitOptions.RemoveEmptyEntries);
            string[] RightSide = InputLines[i + 1].Split("]", StringSplitOptions.RemoveEmptyEntries);
            string[][] Left = new string[(LeftSide.Length)][];
            string[][] Right = new string[(RightSide.Length)][];

            for (int l = 0; l < LeftSide.Length; l++)
            {
                Left[l] = LeftSide[l].Split('[');
                
            }
            for(int r = 0; r<RightSide.Length; r++)
            {
                Right[r] = RightSide[r].Split('[');
            }
            bool RightOrder = true;
            for (int k = 0; k < Left.Length; k++)
            {
                for (int j = 0; j < Right[k].Length; j++)
                {
                    if (Left[k][j] == Right[k][j])
                    {
                        continue;
                    }
                    else
                    {
                        for (int m = 0; m < Left[k][j].Length; m++)
                        {
                            string[] leftValues = Left[k][j].Split(',', StringSplitOptions.RemoveEmptyEntries);
                            string[] rightValues = Right[k][j].Split(',', StringSplitOptions.RemoveEmptyEntries);
                            for (int n = 0; n < leftValues.Length; n++)
                            {
                                if (int.Parse(leftValues[n]) > int.Parse(rightValues[n]))
                                {
                                    break;
                                }
                            }
                        }    
                    }
                }
            }
            
            i += 3;
        }while (i < InputLines.Length);
    }
    [Benchmark]
    public void Part2(){

    }**/
}
