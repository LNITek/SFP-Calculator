using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SFPCalculator
{
    /// <summary>
    /// The Main API To Automated Production Planner
    /// </summary>
    public class SFPPlanner
    {
        /// <summary>
        /// The Version Of The Library
        /// </summary>
        public static string MK { get; } = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0.0";

        /// <summary>
        /// The Main API To Automated Production Planner
        /// </summary>
        public SFPPlanner()
        {
            ModsManager.LoadMods();
        }

        /// <summary>
        /// Produces A Single Process With No Child Production
        /// </summary>
        /// <param name="RecipeName">The Recipes Name</param>
        /// <param name="UnitPerMin">The Amount Per Min (Leave Blank To Use The Default Value)</param>
        /// <returns>One Process With No Children</returns>
        /// <exception cref="Exception"></exception>
        public static Process SingleProcess(string RecipeName, double UnitPerMin = default)
        {
            Recipes.Recipes Alt = null;
            Items.Items Item = null;
            double PerMin = 1;
            bool bDefult = true;

            if (RecipeName.ToUpper().Contains("ALT"))
            {
                RecipeName = RecipeName.ToUpper().Replace("ALTERNATE", "").Replace("ALT", "").Replace(":", "").Trim();
                Alt = GetAltRecipe(RecipeName);
                Item = Items.Data.GetItems(Items.Property.ID, Alt.GetOutput().First().Key).FirstOrDefault(x => x != null);
                if (Item == null)
                    throw new Exception($"No Item With The Alt Recipe Name ({RecipeName}) Exist. Ex-02");
                PerMin = UnitPerMin == default ? Alt.GetOutput().First(x => x.Key == Item.ID).Value : UnitPerMin;
                bDefult = false;
            }
            if (RecipeName.ToUpper().Contains("POWER"))
            {
                RecipeName = RecipeName.ToUpper().Replace("POWER", "").Replace(":", "").Trim();
                Alt = GetGenRecipe(RecipeName);
                Item = Items.Data.GetItems(Items.Property.ID, 0).FirstOrDefault(x => x != null);
                if (Item == null)
                    throw new Exception($"No Item With The Power Gen Recipe Name ({RecipeName}) Exist. Ex-02");
                if (Alt == null)
                    throw new Exception($"No Power Gen Recipe With The Name ({RecipeName}) Exist. Ex-02");
                PerMin = UnitPerMin == default ? GetBuilding(Alt).PowerUsed : UnitPerMin;
                bDefult = false;
            }
            if (bDefult)
            {
                RecipeName = RecipeName.ToUpper().Trim();
                Item = GetRecipe(RecipeName);
                PerMin = UnitPerMin == default ? GetRecipe(Item).GetOutput().First(x => x.Key == Item.ID).Value : UnitPerMin;
            }
            return GenProcess();

            Process GenProcess()
            {
                var Inputs = new List<Items.ItemPair>();
                var Outputs = new List<Items.ItemPair>();
                var RP = Alt ?? GetRecipe(Item);
                var Mulit = PerMin / (Item.ID == 0 ? GetBuilding(RP).PowerUsed : RP.GetOutput().First(x => x.Key == Item.ID).Value);
                var Building = GetBuilding(RP);

                var Dummy = RP.GetInput();
                if (Dummy != null)
                    foreach (var item in Dummy)
                        Inputs.Add(new Items.ItemPair(Items.Data.GetItems(Items.Property.ID, item.Key.ToString()).First(), 
                            item.Value * Mulit));
                if (Inputs.Contains(null)) throw new Exception($"Inputs[{Inputs.IndexOf(null)}] = NULL Ex-02");

                Dummy = RP.GetOutput();
                if (Dummy != null)
                    foreach (var item in Dummy)
                        Outputs.Add(new Items.ItemPair(Items.Data.GetItems(Items.Property.ID, item.Key.ToString()).First(), 
                            item.Value * Mulit));
                if (Outputs.Contains(null)) throw new Exception($"Outputs[{Outputs.IndexOf(null)}] = NULL Ex-02");
                if (Outputs.Count <= 0 && RP.PowerGen)
                    Outputs.Add(new Items.ItemPair(Item, PerMin));

                return new Process(Item.ID, Inputs, Outputs, Building, new List<Process>()) { Recipe = RP };
            }
        }

        /// <summary>
        /// Produces A Full Process With Of The Production
        /// </summary>
        /// <param name="RecipeName">The Recipes Name</param>
        /// <param name="UnitPerMin">The Amount Per Min (Leave Blank To Use The Default Value)</param>
        /// <returns>The Full Process To Produce The Production</returns>
        /// <exception cref="Exception"></exception>
        public Task<Process> Produce(string RecipeName, double UnitPerMin = default)
        {
            if (UnitPerMin < 0)
                UnitPerMin = default;

            Recipes.Recipes Alt = null;
            Items.Items Item = null;
            double PerMin = 1;
            bool bDefult = true;

            if (RecipeName.ToUpper().Contains("ALT"))
            {
                RecipeName = RecipeName.ToUpper().Replace("ALTERNATE", "").Replace("ALT", "").Replace(":", "").Trim();
                Alt = GetAltRecipe(RecipeName);
                Item = Items.Data.GetItems(Items.Property.ID, Alt.GetOutput().First().Key).FirstOrDefault(x => x != null);
                if (Item == null)
                    throw new Exception($"No Item With The Alt Recipe Name ({RecipeName}) Exist. Ex-01");
                PerMin = UnitPerMin == default ? Alt.GetOutput().First(x => x.Key == Item.ID).Value : UnitPerMin;
                bDefult = false;
            }
            if (RecipeName.ToUpper().Contains("POWER"))
            {
                RecipeName = RecipeName.ToUpper().Replace("POWER", "").Replace(":", "").Trim();
                Alt = GetGenRecipe(RecipeName);
                Item = Items.Data.GetItems(Items.Property.ID, 0).FirstOrDefault(x => x != null);
                if (Item == null)
                    throw new Exception($"No Item With The Power Gen Recipe Name ({RecipeName}) Exist. Ex-01");
                if (Alt == null)
                    throw new Exception($"No Power Gen Recipe With The Name ({RecipeName}) Exist. Ex-01");
                PerMin = UnitPerMin == default ? GetBuilding(Alt).PowerUsed : UnitPerMin;
                bDefult = false;
            }
            if (bDefult)
            {
                RecipeName = RecipeName.ToUpper().Trim();
                Item = GetRecipe(RecipeName);
                PerMin = UnitPerMin == default ? GetRecipe(Item).GetOutput().First(x => x.Key == Item.ID).Value : UnitPerMin;
            }

            return Task.FromResult(Planer(Item, PerMin, Alt));
        }

        /// <summary>
        /// Updates The Main 'Head' Process Units Per Min
        /// </summary>
        /// <param name="Process">The Process To Update</param>
        /// <param name="UnitPerMin">New Units Per Min</param>
        /// <returns>Updated Process</returns>
        public Task<Process> UpdatePerMin(Process Process, double UnitPerMin)
        {
            double Multiplier = UnitPerMin / Process.Recipe.GetPrimery().Value / 10;
            Process NewProcess = SingleProcess(Process.Recipe.ToString(), UnitPerMin);
            foreach(var Items in Process.Children)
                NewProcess.Children = NewProcess.Children.Concat(new[] { Loop(Items, NewProcess.Inputs.First(x => x.Item.ID == Items.Product.Item.ID).PerMin) });
            return Task.FromResult(NewProcess);
            Process Loop(Process Pro, double PerMin)
            {
                Process NewPro = SingleProcess(Pro.Recipe.ToString(), PerMin);
                foreach (var Items in Pro.Children)
                    NewPro.Children = NewPro.Children.Concat(new[] { Loop(Items, NewPro.Inputs.First(x => x.Item.ID == Items.Product.Item.ID).PerMin) });
                return NewPro;
            }
        }

        Process Planer(Items.Items Item, double PerMin, Recipes.Recipes Alt = null)
        {
            var Children = new List<Process>();
            var Inputs = new List<Items.ItemPair>();
            var Outputs = new List<Items.ItemPair>();
            var RP = Alt ?? GetRecipe(Item);
            var Mulit = PerMin / (Item.ID == 0 ? GetBuilding(RP).PowerUsed : RP.GetOutput().First(x => x.Key == Item.ID).Value);
            var Building = GetBuilding(RP);

            var Dummy = RP.GetInput();
            if (Dummy != null)
                foreach (var item in Dummy)
                    Inputs.Add(new Items.ItemPair(Items.Data.GetItems(Items.Property.ID, item.Key.ToString()).First(),
                        item.Value * Mulit));
            if (Inputs.Contains(null)) throw new Exception($"Inputs[{Inputs.IndexOf(null)}] = NULL Ex-01");

            Dummy = RP.GetOutput();
            if (Dummy != null)
                foreach (var item in Dummy) 
                    Outputs.Add(new Items.ItemPair(Items.Data.GetItems(Items.Property.ID, item.Key.ToString()).First(),
                            item.Value * Mulit));
            if (Outputs.Contains(null)) throw new Exception($"Outputs[{Outputs.IndexOf(null)}] = NULL Ex-01");
            if (Outputs.Count <= 0 && RP.PowerGen)
                Outputs.Add(new Items.ItemPair(Item, PerMin));

            foreach (var I in Inputs)
                Children.Add(Planer(I.Item, I.PerMin));

            return new Process(Item.ID, Inputs, Outputs, Building, Children) { Recipe = RP };
        }

        internal static Items.Items GetRecipe(string RecipeName)
        {
            var RP = Recipes.Data.GetRecipes(Recipes.Property.Name, RecipeName).FirstOrDefault();
            if (RP == null)
                throw new Exception($"No Recipe With The Name ({RecipeName}) Exist.");
            var Product = Items.Data.GetItems(Items.Property.ID, RP.GetOutput().FirstOrDefault().Key).FirstOrDefault();
            if (Product == null)
                throw new Exception($"No Item With The ID ({RP.GetOutput().FirstOrDefault().Key}) Exist.");
            return Product;
        }

        internal static Buildings.Buildings GetBuilding(Recipes.Recipes Recipe)
        {
            var Factory = Buildings.Data.GetBuildings(Buildings.Property.ID, Recipe.Factory).FirstOrDefault(x => x != null);
            if (Factory == null)
                throw new Exception($"No Factory To Produce The Recipe ({Recipe.Name}) Exist.");
            return Factory;
        }

        internal static Recipes.Recipes GetRecipe(Items.Items Item)
        {
            var RP = Recipes.Data.GetOutput(Item.ID).FirstOrDefault(x => x != null);
            if (RP == null)
                throw new Exception($"No Recipes For The Item With The Name ({Item.Name}) Exist.");
            return RP;
        }

        internal static Recipes.Recipes GetAltRecipe(string Item)
        {
            var RP = Recipes.Data.GetRecipes(Recipes.Property.Name, Item).Where(x => x.Alt).FirstOrDefault(x => x != null);
            if (RP == null)
                throw new Exception($"No Alt Recipes With The Name ({Item}) Exist.");
            return RP;
        }

        internal static Recipes.Recipes GetGenRecipe(string Item)
        {
            var RP = Recipes.Data.GetRecipes(Recipes.Property.Name, Item).Where(x => x.PowerGen).FirstOrDefault(x => x != null);
            if (RP == null)
                throw new Exception($"No Power Gen Recipes With The Name ({Item}) Exist.");
            return RP;
        }
    }
}
