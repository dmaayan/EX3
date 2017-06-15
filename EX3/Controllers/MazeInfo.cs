using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EX3.Controllers
{
    public class MazeInfo
    {
        string name;
        int rows;
        int cols;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Rows
        {
            get { return rows; }
            set { rows = value; }
        }

        public int Cols
        {
            get { return cols; }
            set { cols = value; }
        }
    }
}