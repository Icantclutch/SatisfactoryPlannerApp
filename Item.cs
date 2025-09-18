using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryBuildPlanner
{
    public class Item
    {
        public int ItemID { get; set; }
        public double ItemQuantity { get; set; }

        public Item(int id, double qty)
        {
            ItemID = id;
            ItemQuantity = qty;
        }
    }
}
