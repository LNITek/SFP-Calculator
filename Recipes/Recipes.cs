using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SFPCalculator.Recipes
{
    public class Recipes
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Factory { get; set; }
        Dictionary<int, double> In = new Dictionary<int, double>();
        Dictionary<int, double> Out = new Dictionary<int, double>();
        public string Input
        {
            get
            {
                if (In != null)
                    return $"{string.Join(";", In.Select(x => $"{x.Key}|{Math.Round(x.Value, 2)}"))}";
                return "";
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    In = null;
                    return;
                }

                var List = value.Replace("\"", string.Empty).Split(';').ToList();
                foreach (var item in List)
                {
                    var Items = item.Split('|').ToList();
                    if (Items.Count >= 2)
                        In.Add(Convert.ToInt32(Items[0]), Convert.ToDouble(Items[1]));
                }
            }
        }
        public string Output
        {
            get
            {
                if (Out != null)
                    return $"{string.Join(";", Out.Select(x => $"{x.Key}|{Math.Round(x.Value, 2)}"))}";
                return "";
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;

                var List = value.Replace("\"",string.Empty).Split(';').ToList();
                foreach (var item in List)
                {
                    var Items = item.Split('|').ToList();
                    if (Items.Count >= 2)
                        Out.Add(Convert.ToInt32(Items[0]), Convert.ToDouble(Items[1]));
                }
            }
        }
        [Ignore]
        public bool PowerGen { get; set; } = false;
        [Ignore]
        public bool Alt { get; set; } = false;

        public Recipes(int ID, string Name, int Factory, string Input, string Output)
        {
            this.ID = ID;
            this.Name = Name;
            this.Factory = Factory;
            this.Input = Input;
            this.Output = Output;
        }

        public Recipes(int ID, string Name, int Factory, Dictionary<int, double> Input, Dictionary<int, double> Output)
        {
            this.ID = ID;
            this.Name = Name;
            this.Factory = Factory;
            this.SetInput(Input);
            this.SetOutput(Output);
        }

        public Dictionary<int, double> GetInput() => In;
        public Dictionary<int, double> GetOutput() => Out;
        public KeyValuePair<int, double> GetPrimery() => Out.First();
        public void SetInput(Dictionary<int, double> Value) => In = Value;
        public void SetOutput(Dictionary<int, double> Value) => Out = Value;

        public string Select(string Property) => Select(Prop.ToProperty(Property));
        public string Select(Property Prop)
        {
            switch (Prop)
            {
                case Property.ID: return ID.ToString();
                case Property.Name: return Name.ToString();
                default: return null;
            }
        }

        public override string ToString() =>
            (PowerGen ? "Power : " : Alt ? "Alt : " : "") + Name;
    }
}
