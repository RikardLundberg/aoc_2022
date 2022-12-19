using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_4
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
            int totalScore = 0;
            foreach (string line in lines)
            {
                var elves = line.Split(',');
                var elf1str = elves[0].Split('-');
                var elf2str = elves[1].Split('-');

                List<int> elf1 = new List<int>() { int.Parse(elf1str[0]), int.Parse(elf1str[1]) };
                List<int> elf2 = new List<int>() { int.Parse(elf2str[0]), int.Parse(elf2str[1]) };

                if ((elf1[1] - elf2[1] >= 0 && elf1[0] <= elf2[0]) || (elf2[1] - elf1[1] >= 0 && elf2[0] <= elf1[0]))
                    totalScore++;
            }
            Console.WriteLine("Totalscore: " + totalScore);
        }
        static void second()
        {
            var lines = File.ReadAllLines("../../input.txt");
            int totalScore = 0;
            foreach (string line in lines)
            {
                var elves = line.Split(',');
                var elf1str = elves[0].Split('-');
                var elf2str = elves[1].Split('-');

                List<int> elf1 = new List<int>() { int.Parse(elf1str[0]), int.Parse(elf1str[1]) };
                List<int> elf2 = new List<int>() { int.Parse(elf2str[0]), int.Parse(elf2str[1]) };

                //if ((elf1[1] - elf2[1] >= 0 && elf1[0] <= elf2[0]) || (elf2[1] - elf1[1] >= 0 && elf2[0] <= elf1[0]))
                if ((elf1[0] <= elf2[0] && elf2[0] <= elf1[1]) ||
                    (elf1[0] <= elf2[1] && elf2[1] <= elf1[1]) ||
                    (elf2[0] <= elf1[0] && elf1[0] <= elf2[1]) ||
                    (elf2[0] <= elf1[1] && elf1[1] <= elf2[1]))
                    totalScore++;
            }
            Console.WriteLine("Totalscore: " + totalScore);
        }
    }
}
