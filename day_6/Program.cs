using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace aoc_2022
{
    class Program
    {
        static void Main(string[] args)
        {
            int start_time, elapsed_time;

            start_time = DateTime.Now.Millisecond;
            first();
            second();
            elapsed_time = DateTime.Now.Millisecond - start_time;
            Console.WriteLine("Elapsed ms: " + elapsed_time);
        }

        static bool allUnequal(char [] seq)
        {
            Regex reg = new Regex("^(?:([a-z])(?!.*\\1)\\s*)*$");
            string str = "";
            foreach (char ch in seq)
                str += ch;
            if (reg.IsMatch(str))
                return true;
            return false;
        }

        static void first()
        {
            var lines = File.ReadAllLines("../../input.txt");
            foreach (string line in lines)
            {
                int totalScore = 0;
                char[] seq = new char[4] {'a', 'a', 'a', 'a'};
                int seqCounter = 0;
                for (int i = 0; i < line.Length; i++)
                {
                    seq[seqCounter] = line[i];
                    if (i > 2 && allUnequal(seq))
                    {
                        totalScore = i + 1;
                        break;
                    }
                    seqCounter = ++seqCounter % 4;
                }
                Console.WriteLine("Totalscore: " + totalScore);
            }
        }

        static void second()
        {
            var lines = File.ReadAllLines("../../input.txt");
            foreach (string line in lines)
            {
                int totalScore = 0;
                char[] seq = new char[14] { 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a' };
                int seqCounter = 0;
                for (int i = 0; i < line.Length; i++)
                {
                    seq[seqCounter] = line[i];
                    if (i > 2 && allUnequal(seq))
                    {
                        totalScore = i + 1;
                        break;
                    }
                    seqCounter = ++seqCounter % 14;
                }
                Console.WriteLine("Totalscore: " + totalScore);
            }
        }
    }
}
