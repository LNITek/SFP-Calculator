using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SFPCalculator.Recipes
{
    /// <summary>
    /// Recipes The Main Point Of This Library
    /// </summary>
    public class Recipes
    {
        /// <summary>
        /// DB ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Building That Uses This Recipe
        /// </summary>
        public int Factory { get; set; }
        Dictionary<int, double> In = new Dictionary<int, double>();
        Dictionary<int, double> Out = new Dictionary<int, double>();
        /// <summary>
        /// Input As String Format
        /// </summary>
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
        /// <summary>
        /// Output As String Format
        /// </summary>
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
        /// <summary>
        /// Is Power Gen Recipe
        /// </summary>
        [Ignore]
        public bool PowerGen { get; set; } = false;
        /// <summary>
        /// Is Alt Recipe
        /// </summary>
        [Ignore]
        public bool Alt { get; set; } = false;

        /// <summary>
        /// Recipes The Main Point Of This Library
        /// </summary>
        public Recipes(int ID, string Name, int Factory, string Input, string Output)
        {
            this.ID = ID;
            this.Name = Name;
            this.Factory = Factory;
            this.Input = Input;
            this.Output = Output;
        }

        /// <summary>
        /// Recipes The Main Point Of This Library
        /// </summary>
        public Recipes(int ID, string Name, int Factory, Dictionary<int, double> Input, Dictionary<int, double> Output)
        {
            this.ID = ID;
            this.Name = Name;
            this.Factory = Factory;
            this.SetInput(Input);
            this.SetOutput(Output);
        }

        /// <summary>
        /// The Inputs
        /// Key = Item ID, Value = Units Per Min
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, double> GetInput() => In;
        /// <summary>
        /// The Outputs
        /// Key = Item ID, Value = Units Per Min
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, double> GetOutput() => Out;
        /// <summary>
        /// The Primery Output
        /// Key = Item ID, Value = Units Per Min
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<int, double> GetPrimery()
        {
            if (PowerGen) return new KeyValuePair<int, double>(0, Buildings.Data.GetBuildings().First(x => x.ID == Factory).PowerUsed);
            else return Out.First();
        }
        /// <summary>
        /// Set Input
        /// </summary>
        /// <param name="Value">Key = Item ID, Value = Units Per Min</param>
        public void SetInput(Dictionary<int, double> Value) => In = Value;
        /// <summary>
        /// Set Output
        /// </summary>
        /// <param name="Value">Key = Item ID, Value = Units Per Min</param>
        public void SetOutput(Dictionary<int, double> Value) => Out = Value;

        /// <summary>
        /// Returns Property Value By Name
        /// </summary>
        /// <param name="Property">Property Name</param>
        public object Select(string Property) => Select(Prop.ToProperty(Property));
        /// <summary>
        /// Returns Property Value By enum
        /// </summary>
        /// <param name="Property">Property enum</param>
        /// <returns></returns>
        public object Select(Property Property)
        {
            switch (Property)
            {
                case Property.ID: return ID;
                case Property.Name: return Name;
                default: return null;
            }
        }

        /// <summary>
        /// Returns The Full Recipe Name
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            (PowerGen ? "Power : " : Alt ? "Alt : " : "") + Name;
    }
}
