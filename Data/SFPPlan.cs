using System.Collections.Generic;

namespace SFPCalculator
{
    /// <summary>
    /// A Smaller Process Object
    /// </summary>
    public class MacroProcess
    {
        /// <summary>
        /// The Recipe Name
        /// </summary>
        public string Recipe { get; set; }
        /// <summary>
        /// Units Per Min
        /// </summary>
        public double PerMin { get; set; }
        /// <summary>
        /// Whether To Produce This Item
        /// </summary>
        public bool Production { get; set; }
        /// <summary>
        /// Its Children Productions
        /// </summary>
        public List<MacroProcess> Children { get; set; }
    }
}
