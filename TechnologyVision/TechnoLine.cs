using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechnologyVision
{
    public class TechnoLine
    {
        int id;

        public int ID
        {
            set { id = value; }
            get { return id; }
        }

        string name;

        public string Name
        {
            set { name = value; }
            get { return name; }
        }

        public List<YearItem> YearItems;

        internal void UpdateCurrentYear(string YearLabel)
        {
            foreach (YearItem yi in YearItems)
            {
                yi.UpdateCurrentYear(YearLabel);
            }
        }
    }
}
