using System;
using System.Collections.Generic;
using System.Linq;

namespace SFPCalculator
{
    public class Process
    {
        public int MainID { get; set; }
        public bool Production { get; set; } = true;
        public Items.ItemPair Product { get { return Outputs.First(); } }
        public Recipes.Recipes Recipe { get; set; }
        public List<Items.ItemPair> Inputs { get; set; } = new List<Items.ItemPair>();
        public List<Items.ItemPair> Outputs { get; set; } = new List<Items.ItemPair>();

        public Buildings.Buildings Building {get; set;}
        public List<Process> Children { get; set; }

        public void AddInput(Items.Items Item, double PerMin) => Inputs.Add(new Items.ItemPair(Item, PerMin));
        public void AddOuput(Items.Items Item, double PerMin) => Outputs.Add(new Items.ItemPair(Item, PerMin));

        public void RemoveInput(Items.Items Item) => Inputs.Remove(Inputs.First(x => x.Item == Item));
        public void RemoveOuput(Items.Items Item) => Outputs.Remove(Inputs.First(x => x.Item == Item));

        public Process(int MainID, List<Items.ItemPair> Inputs, List<Items.ItemPair> Outputs,
            Buildings.Buildings Building, List<Process> Children)
        {
            this.MainID = MainID;
            this.Inputs = Inputs;
            this.Outputs = Outputs;
            this.Building = Building;
            this.Children = Children ?? new List<Process>();
        }

        public Process(int MainID, List<Items.Items> InputItems, List<double> InputPerMin, 
            List<Items.Items> OutputItems, List<double> OutputPerMin, Buildings.Buildings Building,
            List<Process> Children)
        {
            if (InputItems.Count != InputPerMin.Count)
                throw new Exception("Input Lists Must Be Of The Same Size.");
            if (OutputItems.Count != OutputPerMin.Count)
                throw new Exception("Output Lists Must Be Of The Same Size.");

            for (int I = 0; I < InputItems.Count; I++)
                AddInput(InputItems[I], InputPerMin[I]);
            for (int I = 0; I < OutputItems.Count; I++)
                AddOuput(OutputItems[I], OutputPerMin[I]);
            this.MainID = MainID;;
            this.Building = Building;
            this.Children = Children ?? new List<Process>();
        }
    }
}
