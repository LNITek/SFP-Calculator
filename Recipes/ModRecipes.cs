using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SFPCalculator.Recipes
{
    /// <summary>
    /// Moded Recipes
    /// </summary>
    public class ModRecipe
    {
        /// <summary>
        /// Mod DB ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Building That Uses This Recipe
        /// </summary>
        public string Factory { get; set; }
        /// <summary>
        /// Inputs As String Format
        /// </summary>
        public string Input { get; set; }
        /// <summary>
        /// Outputs As String Format
        /// </summary>
        public string Output { get; set; }
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
        /// Moded Recipes
        /// </summary>
        public ModRecipe(string ID, string Name, string Factory, string Input, string Output)
        {
            this.ID = ID;
            this.Name = Name;
            this.Factory = Factory;
            this.Input = Input;
            this.Output = Output;
        }

        /// <summary>
        /// Converts Mod Recipe To A Useabel Recipe Object
        /// </summary>
        /// <param name="NewID">Main DB ID</param>
        /// <returns></returns>
        public Recipes ToRecipes(int NewID) => new Recipes(NewID, Name, Convert.ToInt32(Factory), Input, Output)
        { PowerGen = PowerGen, Alt = Alt };
    }
}
