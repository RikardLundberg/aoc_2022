using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_10
{
    class Program
    {
        static void Main(string[] args)
        {
            first();
            second();
        }

        static void first()
        {
            var lines = File.ReadAllLines("../../input.txt");
            int cycles = 0;
            int breakInterval = 20;
            int registerX = 1;
            List<int> intervals = new List<int>();
            foreach (var line in lines)
            {
                if (line == "noop")
                {
                    cycles++;
                    if (cycles == breakInterval)
                    {
                        intervals.Add(registerX);
                        breakInterval += 40;
                    }
                }
                else
                {
                    cycles++;
                    if (cycles == breakInterval)
                    {
                        intervals.Add(registerX);
                        breakInterval += 40;
                    }
                    cycles++;
                    if (cycles == breakInterval)
                    {
                        intervals.Add(registerX);
                        breakInterval += 40;
                    }
                    registerX += int.Parse(line.Split(' ')[1]);
                }
            }

            int factor = 20;
            int sum = 0;
            bool skipNext = false;
            foreach (var interval in intervals)
            {
                if (factor == 260)
                    break;

                sum += interval * factor;
                factor += 40;
            }
            Console.WriteLine("Totalscore: " + sum);
        }

        static void second()
        {
            var lines = File.ReadAllLines("../../input.txt");
            int cycles = 0;
            int registerX = 1;
            int crtPos = 0;
            string crtStr = "";
            foreach (var line in lines)
            {
                if (line == "noop")
                {
                    cycles++;
                    if (crtPos == registerX || crtPos == registerX - 1 || crtPos == registerX + 1)
                        crtStr += "#";
                    else
                        crtStr += ".";
                    crtPos++;
                    crtPos = crtPos % 40;
                }
                else
                {
                    cycles++;
                    if (crtPos == registerX || crtPos == registerX - 1 || crtPos == registerX + 1)
                        crtStr += "#";
                    else
                        crtStr += ".";
                    crtPos++;
                    crtPos = crtPos % 40;
                    cycles++;
                    if (crtPos == registerX || crtPos == registerX - 1 || crtPos == registerX + 1)
                        crtStr += "#";
                    else
                        crtStr += ".";
                    crtPos++;
                    crtPos = crtPos % 40;
                    registerX += int.Parse(line.Split(' ')[1]);
                }
            }
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine(crtStr.Substring(i * 40, 40));
            }
        }
    }
}
