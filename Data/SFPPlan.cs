using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPCalculator
{
    public class MacroProcess
    {
        public string Recipe { get; set; }
        public double PerMin { get; set; }
        public bool Production { get; set; }
        public List<MacroProcess> Children { get; set; }
    }
}
