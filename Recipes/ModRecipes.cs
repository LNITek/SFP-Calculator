using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SFPCalculator.Recipes
{
    public class ModRecipe
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Factory { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        [Ignore]
        public bool PowerGen { get; set; } = false;
        [Ignore]
        public bool Alt { get; set; } = false;

        public ModRecipe(string ID, string Name, string Factory, string Input, string Output)
        {
            this.ID = ID;
            this.Name = Name;
            this.Factory = Factory;
            this.Input = Input;
            this.Output = Output;
        }

        public Recipes ToRecipes(int NewID) => new Recipes(NewID, Name, Convert.ToInt32(Factory), Input, Output)
        { PowerGen = PowerGen, Alt = Alt };
    }
}
