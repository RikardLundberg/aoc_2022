using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace day_8
{
    class coord
    {
        public coord(int _val, int _x, int _y)
        {
            val = _val;
            x = _x;
            y = _y;
        }
        public int val { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public int scenic_row { get; set; } = 0;
        public int scenic_col { get; set; } = 0;

        public static bool operator ==(coord obj1, coord obj2)
        {
            return (obj1.x == obj2.x && obj1.y == obj2.y);
        }
        public static bool operator !=(coord obj1, coord obj2)
        {
            return !(obj1.x == obj2.x && obj1.y == obj2.y);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            first();
            second();
        }

        static void second()
        {
            var lines = File.ReadAllLines("../../input.txt");
            int rowCounter = 0;
            List<List<coord>> rows = new List<List<coord>>();
            //List<List<coord>> cols = new List<List<coord>>();

            List<List<coord>> cols = new List<List<coord>>();
            List<coord> allCoords = new List<coord>();

            foreach (string line in lines)
            {
                List<coord> row = new List<coord>();
                for (int i = 0; i < line.Length; i++)
                {
                    var chInt = line[i] - '0';
                    var c = new coord(chInt, i, rowCounter);
                    row.Add(c);
                    if (cols.Count() < i + 1)
                        cols.Add(new List<coord>());
                    cols[i].Add(c);
                    allCoords.Add(c);
                }
                rows.Add(row);
                rowCounter++;
            }

            for (int i = 0; i < rows.Count(); i++)
            {
                for (int j = 0; j < rows[i].Count(); j++)
                {
                    rows[i][j].scenic_row = calculateScenicVal(rows[i][j], rows[i]);
                    var tmpRow = new List<coord>();
                    foreach (var coo in rows[i])
                        tmpRow.Add(coo);
                    tmpRow.Reverse();
                    rows[i][j].scenic_row *= calculateScenicVal(rows[i][j], tmpRow);

                }
            }

            for (int i = 0; i < cols.Count(); i++)
            {
                for (int j = 0; j < cols[i].Count(); j++)
                {
                    cols[i][j].scenic_col = calculateScenicVal(cols[i][j], cols[i]);
                    var tmpCol = new List<coord>();
                    foreach (var coo in cols[i])
                        tmpCol.Add(coo);
                    tmpCol.Reverse();
                    cols[i][j].scenic_col *= calculateScenicVal(cols[i][j], tmpCol);
                }
            }

            int highest = -1;
            foreach(var row in rows)
            {
                foreach(var coord in row)
                {
                    int scenic = coord.scenic_col * coord.scenic_row;
                    if (scenic > highest)
                        highest = scenic;
                }
            }

        }

        static int calculateScenicVal(coord c, List<coord> coords)
        {
            int val = -1;
            int scenicVal = 0;
            foreach (var coord in coords)
            {
                if (val > -1)
                {
                    //if (coord.val >= val)
                    {
                        scenicVal++;
                        val = coord.val;
                    }
                }
                if (coord == c)
                    val = 0;
                if (val >= c.val)
                    break;
            }
            return scenicVal;
        }

        static void first()
        {
            var lines = File.ReadAllLines("../../input.txt");
            int rowCounter = 0;
            List<List<coord>> rows = new List<List<coord>>();
            //List<List<coord>> cols = new List<List<coord>>();

            List<List<coord>> cols = new List<List<coord>>();
            foreach (string line in lines)
            {
                List<coord> row = new List<coord>();
                for (int i = 0; i < line.Length; i++)
                {
                    var chInt = line[i] - '0';
                    row.Add(new coord(chInt, i, rowCounter));
                    if (cols.Count() < i + 1)
                        cols.Add(new List<coord>());
                    cols[i].Add(new coord(chInt, i, rowCounter));
                }
                rows.Add(row);
                rowCounter++;
            }

            List<coord> visibleCoords = new List<coord>();
            foreach (var row in rows)
            {
                addListIfNotExisting(row, visibleCoords);
                row.Reverse();
                addListIfNotExisting(row, visibleCoords);
            }
            foreach (var col in cols)
            {
                addListIfNotExisting(col, visibleCoords);
                col.Reverse();
                addListIfNotExisting(col, visibleCoords);
            }
        }

        static void addListIfNotExisting(List<coord> view, List<coord> visibleCoords)
        {
            int prevVal = -1;
            foreach (coord c in view)
            {
                if (c.val > prevVal)
                {
                    bool found = false;
                    foreach (coord cc in visibleCoords)
                        if (cc == c)
                        {
                            found = true;
                            break;
                        }
                    if (!found)
                        visibleCoords.Add(c);
                    prevVal = c.val;
                }
            }
        }
    }
}
