using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace day_9
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
            Point tail = new Point(0, 0);
            Point head = new Point(0, 0);
            List<string> visitedPoints = new List<string>() { "0;0" };

            var lines = File.ReadAllLines("../../input.txt");
            foreach(var line in lines)
            {
                var split = line.Split(' ');
                for (int i = 0; i < int.Parse(split[1]); i++)
                {
                    switch (split[0])
                    {
                        case "R":
                            head.X++;
                            break;
                        case "L":
                            head.X--;
                            break;
                        case "D":
                            head.Y--;
                            break;
                        case "U":
                            head.Y++;
                            break;
                    }
                    if(Math.Abs(head.X - tail.X) >= 2 || Math.Abs(head.Y - tail.Y) >= 2)
                    {
                        switch (split[0])
                        {
                            case "R":
                                tail.Y = head.Y;
                                tail.X = head.X - 1;
                                break;
                            case "L":
                                tail.Y = head.Y;
                                tail.X = head.X + 1;
                                break;
                            case "D":
                                tail.X = head.X;
                                tail.Y = head.Y + 1;
                                break;
                            case "U":
                                tail.X = head.X;
                                tail.Y = head.Y - 1;
                                break;
                        }
                        var visitedPoint = tail.X + ";" + tail.Y;
                        if (!visitedPoints.Contains(visitedPoint))
                            visitedPoints.Add(visitedPoint);
                    }
                }
            }

            Console.WriteLine("Totalscore: " + visitedPoints.Count);
        }

        static void second()
        {
            int startX = 11;
            int startY = 5;
            List<Point> tails = new List<Point>();
            for (int i = 0; i < 10; i++)
                tails.Add(new Point(startX, startY));
            Point head = new Point(startX, startY);
            List<string> visitedPoints = new List<string>() { "startX;startY" };

            var lines = File.ReadAllLines("../../input.txt");
            foreach (var line in lines)
            {
                var split = line.Split(' ');
                for (int i = 0; i < int.Parse(split[1]); i++)
                {
                    switch (split[0])
                    {
                        case "R":
                            head.X++;
                            break;
                        case "L":
                            head.X--;
                            break;
                        case "D":
                            head.Y--;
                            break;
                        case "U":
                            head.Y++;
                            break;
                    }
                    string lastInstruction = split[0];
                    for(int t = 0; t < tails.Count; t++)
                    {
                        var tail = tails[t];
                        var tmpHead = t > 0 ? tails[t - 1] : head;
                        bool checkVisit = false;
                        //if (Math.Abs(tmpHead.X - tails[t].X) >= 2 || Math.Abs(tmpHead.Y - tails[t].Y) >= 2)
                        {
                            /*switch (lastInstruction)
                            {
                                case "R":
                                    tail.Y = tmpHead.Y;
                                    tail.X = tmpHead.X - 1;
                                    break;
                                case "L":
                                    tail.Y = tmpHead.Y;
                                    tail.X = tmpHead.X + 1;
                                    break;
                                case "D":
                                    tail.X = tmpHead.X;
                                    tail.Y = tmpHead.Y + 1;
                                    break;
                                case "U":
                                    tail.X = tmpHead.X;
                                    tail.Y = tmpHead.Y - 1;
                                    break;
                            }*/

                            if(Math.Abs(tmpHead.X - tail.X) >= 2 && Math.Abs(tmpHead.Y - tail.Y) >= 2)
                            {
                                tail.X = tmpHead.X > tail.X ? tmpHead.X - 1 : tmpHead.X + 1;
                                tail.Y = tmpHead.Y > tail.Y ? tmpHead.Y - 1 : tmpHead.Y + 1;
                                checkVisit = true;
                            }
                            else if(Math.Abs(tmpHead.X - tail.X) >= 2)
                            {
                                tail.Y = tmpHead.Y;
                                tail.X = tmpHead.X > tail.X ? tmpHead.X - 1 : tmpHead.X + 1;
                                checkVisit = true;
                            }
                            else if(Math.Abs(tmpHead.Y - tail.Y) >= 2)
                            {
                                tail.X = tmpHead.X;
                                tail.Y = tmpHead.Y > tail.Y ? tmpHead.Y - 1 : tmpHead.Y + 1;
                                checkVisit = true;
                            }



                            if (t == 8 && checkVisit)
                            {
                                var visitedPoint = tail.X + ";" + tail.Y;
                                if (!visitedPoints.Contains(visitedPoint))
                                    visitedPoints.Add(visitedPoint);
                            }
                        }
                        tails[t] = tail;
                    }
                    //printNeat(head, tails);
                }
            }

            Console.WriteLine("Totalscore: " + visitedPoints.Count);
        }

        private static void printNeat(Point head, List<Point> tails)
        {
            for (int y = 21; y >= 0; y--)
            {
                string newstr = "";
                for (int x = 0; x < 26; x++)
                {
                    bool added = false;
                    if (head.X == x && y == head.Y)
                    {
                        newstr += "H";
                        added = true;
                    }
                    else
                    {
                        for (int t = 0; t < tails.Count; t++)
                        {
                            if (tails[t].X == x && tails[t].Y == y)
                            {
                                newstr += t + 1;
                                added = true;
                                break;
                            }
                        }
                    }
                    if (!added) newstr += ".";
                }
                Console.WriteLine(newstr);
            }
            Console.WriteLine("");
        }
    }
}
