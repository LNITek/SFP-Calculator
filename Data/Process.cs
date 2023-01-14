using System;
using System.Collections.Generic;
using System.Linq;

namespace SFPCalculator
{
    /// <summary>
    /// A Precess Has All The Data You Need
    /// </summary>
    public class Process
    {
        /// <summary>
        /// ID Of Primery Output Item
        /// </summary>
        public int MainID { get; set; }
        /// <summary>
        /// Whether To Produce The Item And Its Children
        /// </summary>
        public bool Production { get; set; } = true;
        /// <summary>
        /// The Primery Output Item
        /// </summary>
        public Items.ItemPair Product { get { if (Outputs.Count() == 0) return null; return Outputs.FirstOrDefault(); } }
        /// <summary>
        /// The Used Resipe
        /// </summary>
        public Recipes.Recipes Recipe { get; set; }
        /// <summary>
        /// All The Inputs
        /// </summary>
        public IEnumerable<Items.ItemPair> Inputs { get; set; } = new List<Items.ItemPair>();
        /// <summary>
        /// All The Outputs
        /// </summary>
        public IEnumerable<Items.ItemPair> Outputs { get; set; } = new List<Items.ItemPair>();

        /// <summary>
        /// The Building Used For Production
        /// </summary>
        public Buildings.Buildings Building {get; set;}
        /// <summary>
        /// Its Children Production
        /// </summary>
        public IEnumerable<Process> Children { get; set; }

        /// <summary>
        /// Add A Input To Production
        /// </summary>
        /// <param name="Item">The Item</param>
        /// <param name="PerMin">Units Per Min</param>
        public void AddInput(Items.Items Item, double PerMin) => Inputs.ToList().Add(new Items.ItemPair(Item, PerMin));
        /// <summary>
        /// Add A Output To Production
        /// </summary>
        /// <param name="Item">The Item</param>
        /// <param name="PerMin">Units Per Min</param>
        public void AddOuput(Items.Items Item, double PerMin) => Outputs.ToList().Add(new Items.ItemPair(Item, PerMin));

        /// <summary>
        /// Remove A Input From Production
        /// </summary>
        /// <param name="Item">The Item</param>
        public void RemoveInput(Items.Items Item) => Inputs.ToList().Remove(Inputs.First(x => x.Item == Item));
        /// <summary>
        /// Remove A Output From Production
        /// </summary>
        /// <param name="Item">The Item</param>
        public void RemoveOuput(Items.Items Item) => Outputs.ToList().Remove(Inputs.First(x => x.Item == Item));

        /// <summary>
        /// A Precess Has All The Data You Need
        /// </summary>
        public Process(int MainID, IEnumerable<Items.ItemPair> Inputs, IEnumerable<Items.ItemPair> Outputs,
            Buildings.Buildings Building, IEnumerable<Process> Children)
        {
            this.MainID = MainID;
            this.Inputs = Inputs;
            this.Outputs = Outputs;
            this.Building = Building;
            this.Children = Children ?? new List<Process>();
        }
    }
}
