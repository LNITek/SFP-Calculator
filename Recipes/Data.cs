using SFPCalculator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SFPCalculator.Recipes
{
    public static class Data
    {
        static readonly string Path = Properties.Resources.DBRecipes;
        static readonly string AltPath = Properties.Resources.DBAltRecipes;
        static readonly string GenPath = Properties.Resources.DBPowerGen;
        public static List<Recipes> GetRecipes() => GetAllData();
        
        static List<Recipes> GetAllData()
        {
            int I = 0;
            while (!ModsManager.ModsLoaded)
            {
                I++;
                if (I >= 100) throw new Exception("Took To Long To Load Mods | Ex-R.D");
                Thread.Sleep(1000);
            }
            var RS = DBManager.GetData<Recipes>(Path);
            var Alt = DBManager.GetData<Recipes>(AltPath);
            var Gen = DBManager.GetData<Recipes>(GenPath);

            var ModRS = ModsManager.Recipes;
            var ModAlt = ModsManager.AltRecipes;
            var ModGen = ModsManager.GenRecipes;


            int Index = RS.Last().ID;
            ModRS.ForEach(x => { Index++; RS.Add(x.ToRecipes(Index)); });

            Alt.ForEach(x => x.Alt = true);
            RS.AddRange(Alt);
            Index = Alt.Last().ID;
            ModAlt.ForEach(x => { Index++; RS.Add(x.ToRecipes(Index)); });

            Gen.ForEach(x => x.PowerGen = true);
            RS.AddRange(Gen);
            Index = Gen.Last().ID;
            ModGen.ForEach(x => { Index++; RS.Add(x.ToRecipes(Index)); });

            return RS;
        }

        public static List<Recipes> GetRecipes<T>(Property Key, T Value) =>
            GetAllData().FindAll(el => el.Select(Key).ToLower() == Value.ToString().ToLower()).ToList();

        public static List<Recipes> GetRecipes<T>(string Key, T Value) =>
            GetAllData().FindAll(el => el.Select(Key).ToLower() == Value.ToString().ToLower()).ToList();

        public static List<Recipes> GetRecipes<T>(Property Key, List<T> Values) =>
            GetAllData().FindAll(el => Values.Select(x => x.ToString().ToLower()).Contains(el.Select(Key).ToLower())).ToList();

        public static List<Recipes> GetOutput(int ItemID) =>
            GetAllData().FindAll(x => { if (x.GetOutput().Count <= 0) return false; return x.GetOutput().Keys.First() == ItemID; });
    }
}
