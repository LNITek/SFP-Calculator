using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPCalculator.Items
{
    public class ItemPair
    {
        public Items Item { get; set; }
        public double PerMin { get; set; }
        public bool Production { get; set; } = true;
        public object Tag { get; set; } = null;

        public ItemPair(Items item, double perMin)
        {
            Item = item;
            PerMin = perMin;
        }
    }
}
