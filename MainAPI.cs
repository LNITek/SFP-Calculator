using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SFPCalculator
{
    public class SFPPlanner
    {
        public static string MK { get; } = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0.0";

        public SFPPlanner()
        {
            Data.ModsManager.LoadMods();
        }

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
                Item = GetItem(RecipeName);
                PerMin = UnitPerMin == default ? GetRecipe(Item).GetOutput().First(x => x.Key == Item.ID).Value : UnitPerMin;
            }
            return GenProcess();

            Process GenProcess()
            {
                var InPerMin = new List<double>();
                var OutPerMin = new List<double>();
                var Inputs = new List<Items.Items>();
                var Outputs = new List<Items.Items>();
                var RP = Alt == null ? GetRecipe(Item) : Alt;
                var Mulit = PerMin / (Item.ID == 0 ? GetBuilding(RP).PowerUsed : RP.GetOutput().First(x => x.Key == Item.ID).Value);
                var Building = GetBuilding(RP);

                var Dummy = RP.GetInput();
                if (Dummy != null)
                    foreach (var item in Dummy)
                    {
                        Inputs.Add(Items.Data.GetItems(Items.Property.ID, item.Key.ToString()).First());
                        InPerMin.Add(item.Value * Mulit);
                    }
                if (Inputs.Contains(null)) throw new Exception($"Inputs[{Inputs.IndexOf(null)}] = NULL Ex-02");

                Dummy = RP.GetOutput();
                if (Dummy != null)
                    foreach (var item in Dummy)
                    {
                        Outputs.Add(Items.Data.GetItems(Items.Property.ID, item.Key.ToString()).First());
                        OutPerMin.Add(item.Value * Mulit);
                    }
                if (Outputs.Contains(null)) throw new Exception($"Outputs[{Outputs.IndexOf(null)}] = NULL Ex-02");
                if (Outputs.Count <= 0 && RP.PowerGen)
                {
                    Outputs.Add(Item);
                    OutPerMin.Add(PerMin);
                }

                return new Process(Item.ID, Inputs, InPerMin, Outputs, OutPerMin, Building, new List<Process>()) { Recipe = RP };
            }
        }

        public Task<Process> Produce(string Name, double UnitPerMin = default)
        {
            if (UnitPerMin < 0)
                UnitPerMin = default;

            Recipes.Recipes Alt = null;
            Items.Items Item = null;
            double PerMin = 1;
            bool bDefult = true;

            if (Name.ToUpper().Contains("ALT"))
            {
                Name = Name.ToUpper().Replace("ALTERNATE", "").Replace("ALT", "").Replace(":", "").Trim();
                Alt = GetAltRecipe(Name);
                Item = Items.Data.GetItems(Items.Property.ID, Alt.GetOutput().First().Key).FirstOrDefault(x => x != null);
                if (Item == null)
                    throw new Exception($"No Item With The Alt Recipe Name ({Name}) Exist. Ex-01");
                PerMin = UnitPerMin == default ? Alt.GetOutput().First(x => x.Key == Item.ID).Value : UnitPerMin;
                bDefult = false;
            }
            if (Name.ToUpper().Contains("POWER"))
            {
                Name = Name.ToUpper().Replace("POWER", "").Replace(":", "").Trim();
                Alt = GetGenRecipe(Name);
                Item = Items.Data.GetItems(Items.Property.ID, 0).FirstOrDefault(x => x != null);
                if (Item == null)
                    throw new Exception($"No Item With The Power Gen Recipe Name ({Name}) Exist. Ex-01");
                if (Alt == null)
                    throw new Exception($"No Power Gen Recipe With The Name ({Name}) Exist. Ex-01");
                PerMin = UnitPerMin == default ? GetBuilding(Alt).PowerUsed : UnitPerMin;
                bDefult = false;
            }
            if (bDefult)
            {
                Item = GetItem(Name);
                PerMin = UnitPerMin == default ? GetRecipe(Item).GetOutput().First(x => x.Key == Item.ID).Value : UnitPerMin;
            }

            return Task.FromResult(Planer(Item, PerMin, Alt));
        }

        Process Planer(Items.Items Item, double PerMin, Recipes.Recipes Alt = null)
        {
            var Children = new List<Process>();
            var InPerMin = new List<double>();
            var OutPerMin = new List<double>();
            var Inputs = new List<Items.Items>();
            var Outputs = new List<Items.Items>();
            var RP = Alt == null ? GetRecipe(Item) : Alt;
            var Mulit = PerMin / (Item.ID == 0 ? GetBuilding(RP).PowerUsed : RP.GetOutput().First(x => x.Key == Item.ID).Value);
            var Building = GetBuilding(RP);

            var Dummy = RP.GetInput();
            if (Dummy != null)
                foreach (var item in Dummy)
                {
                    Inputs.Add(Items.Data.GetItems(Items.Property.ID, item.Key.ToString()).First());
                    InPerMin.Add(item.Value * Mulit);
                }
            if (Inputs.Contains(null)) throw new Exception($"Inputs[{Inputs.IndexOf(null)}] = NULL Ex-01");

            Dummy = RP.GetOutput();
            if (Dummy != null)
                foreach (var item in Dummy)
                {
                    Outputs.Add(Items.Data.GetItems(Items.Property.ID, item.Key.ToString()).First());
                    OutPerMin.Add(item.Value * Mulit);
                }
            if (Outputs.Contains(null)) throw new Exception($"Outputs[{Outputs.IndexOf(null)}] = NULL Ex-01");
            if (Outputs.Count <= 0 && RP.PowerGen)
            {
                Outputs.Add(Item);
                OutPerMin.Add(PerMin);
            }

            foreach (var I in Inputs)
                Children.Add(Planer(I, InPerMin[Inputs.IndexOf(I)]));

            return new Process(Item.ID, Inputs, InPerMin, Outputs, OutPerMin, Building, Children) { Recipe = RP };
        }

        internal static Items.Items GetItem (string Name)
        {
            var Product = Items.Data.GetItems(Items.Property.Name, Name).FirstOrDefault(x => x != null);
            if (Product == null)
                throw new Exception($"No Item With The Name ({Name}) Exist.");
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
