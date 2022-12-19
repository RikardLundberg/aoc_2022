using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_11
{
    public class monkey
    {
        public int id { get; set; }
        public List<long> items { get; set; } = new List<long>();
        public string operation { get; set; }
        public int divTest { get; set; }
        public int throwTrue { get; set; }
        public int throwFalse { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            first();
            second();
        }

        public static List<monkey> readMonkeys()
        {
            var lines = File.ReadAllLines("../../input.txt");
            List<monkey> monkeys = new List<monkey>();
            monkey currentMonkey = new monkey();
            foreach (var line in lines)
            {
                if (line.StartsWith("Monkey"))
                {
                    currentMonkey.id = line[7] - '0';
                }
                else if (line.Contains("Starting items"))
                {
                    var val = line.Split(':')[1];
                    var split = val.Split(',');
                    foreach (var spl in split)
                    {
                        currentMonkey.items.Add(int.Parse(spl.Trim()));
                    }
                }
                else if (line.Contains("Operation"))
                {
                    currentMonkey.operation = line.Split('=')[1].Trim();
                }
                else if (line.Contains("Test"))
                {
                    currentMonkey.divTest = int.Parse(line.Split(' ').Last());
                }
                else if (line.Contains("If true"))
                {
                    currentMonkey.throwTrue = int.Parse(line.Split(' ').Last());
                }
                else if (line.Contains("If false"))
                {
                    currentMonkey.throwFalse = int.Parse(line.Split(' ').Last());
                    monkeys.Add(currentMonkey);
                    currentMonkey = new monkey() { };
                }
            }
            return monkeys;
        }

        public static void first()
        {
            var monkeys = readMonkeys();
            List<long> inspectionCount = new List<long>();
            foreach (var monkey in monkeys)
            {
                inspectionCount.Add(0);
            }
            
            for (int a = 0; a < 20; a++)
            {
                for (int i = 0; i < monkeys.Count(); i++)
                {
                    var currMonkey = monkeys[i];
                    foreach (var item in currMonkey.items)
                    {
                        var tmpItem = item;
                        var opSplit = currMonkey.operation.Split(' ');
                        long factor = 0;
                        if (opSplit[2] == "old")
                            factor = tmpItem;
                        else
                            factor = int.Parse(opSplit[2]);
                        if (opSplit[1] == "+")
                            tmpItem += factor;
                        else
                            tmpItem *= factor;

                        tmpItem /= 3;

                        if (tmpItem % currMonkey.divTest == 0)
                            monkeys[currMonkey.throwTrue].items.Add(tmpItem);
                        else
                            monkeys[currMonkey.throwFalse].items.Add(tmpItem);

                        inspectionCount[i]++;
                    }
                    currMonkey.items.Clear();
                }
            }

            inspectionCount.Sort();
            inspectionCount.Reverse();
            long monkeyBusiness = inspectionCount[0] * inspectionCount[1];

            Console.WriteLine("Monkey business: " + monkeyBusiness);
        }

        public static void second()
        {
            var monkeys = readMonkeys();
            List<long> inspectionCount = new List<long>();
            int monkeyFactor = 1;

            foreach (var monkey in monkeys)
            {
                inspectionCount.Add(0);
                monkeyFactor *= monkey.divTest;
            }

            for (int a = 0; a < 10000; a++)
            {
                for (int i = 0; i < monkeys.Count(); i++)
                {
                    var currMonkey = monkeys[i];
                    foreach (var item in currMonkey.items)
                    {
                        var tmpItem = item;
                        var opSplit = currMonkey.operation.Split(' ');
                        long factor = 0;
                        if (opSplit[2] == "old")
                            factor = tmpItem;
                        else
                            factor = int.Parse(opSplit[2]);
                        if (opSplit[1] == "+")
                            tmpItem += factor;
                        else
                            tmpItem *= factor;

                        tmpItem %= monkeyFactor;

                        if (tmpItem % currMonkey.divTest == 0)
                            monkeys[currMonkey.throwTrue].items.Add(tmpItem);
                        else
                            monkeys[currMonkey.throwFalse].items.Add(tmpItem);

                        inspectionCount[i]++;
                    }
                    currMonkey.items.Clear();
                }
            }

            inspectionCount.Sort();
            inspectionCount.Reverse();
            long monkeyBusiness = inspectionCount[0] * inspectionCount[1];

            Console.WriteLine("Monkey business: " + monkeyBusiness);
        }
    }
}
