using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace SFPCalculator
{
    internal class ModsManager
    {
        #if (DEBUG)
            static readonly bool bRelease = false;
        #else
            static readonly bool bRelease = true;
        #endif

        public static List<string> ModList = new List<string>();

        public static bool ModsLoaded = false;
        public static List<Items.ModItem> Items = new List<Items.ModItem>();
        public static List<Buildings.ModBuilding> Buildings = new List<Buildings.ModBuilding>();
        public static List<Recipes.ModRecipe> Recipes = new List<Recipes.ModRecipe>(), AltRecipes = new List<Recipes.ModRecipe>(),
                GenRecipes = new List<Recipes.ModRecipe>();

        static int ItemIndex = DBManager.GetData<Items.Items>(Properties.Resources.DBItems).Last().ID;
        static int BuildingIndex = DBManager.GetData<Buildings.Buildings>(Properties.Resources.DBBuildings).Last().ID;
        static int RPIndex = DBManager.GetData<Recipes.Recipes>(Properties.Resources.DBRecipes).Last().ID;
        static int AltRPIndex = DBManager.GetData<Recipes.Recipes>(Properties.Resources.DBAltRecipes).Last().ID;
        static int GenRPIndex = DBManager.GetData<Recipes.Recipes>(Properties.Resources.DBPowerGen).Last().ID;
        static readonly int ModIIndex = 0, ModBIndex = 0;

        public static void LoadMods()
        { 
            ReadMods();
            foreach (string Mod in ModList)
                LoadMods(Mod);
            ModsLoaded = true;
        }

        static void LoadMods(string ModName)
        {
            string DR = GetDefPath() + $"\\{ModName.Replace(".zip", "").Split('\\').Last()}";

            if (File.Exists(DR + "\\ItemsDB.csv"))
                Items = DBManager.GetData<Items.ModItem>(DR + "\\ItemsDB.csv");
            if (File.Exists(DR + "\\BuildingsDB.csv"))
                Buildings = DBManager.GetData<Buildings.ModBuilding>(DR + "\\BuildingsDB.csv");
            if (File.Exists(DR + "\\RecipesDB.csv"))
                Recipes = DBManager.GetData<Recipes.ModRecipe>(DR + "\\RecipesDB.csv");
            if (File.Exists(DR + "\\AltRecipesDB.csv"))
                AltRecipes = DBManager.GetData<Recipes.ModRecipe>(DR + "\\AltRecipesDB.csv");
            if (File.Exists(DR + "\\PowerGenDB.csv"))
                GenRecipes = DBManager.GetData<Recipes.ModRecipe>(DR + "\\PowerGenDB.csv");

            AltRecipes.ForEach(x => x.Alt = true);
            GenRecipes.ForEach(x => x.PowerGen = true);

            FormatModID();
        }

        static void FormatModID()
        {
            for (int I = ModIIndex; I < Items.Count; I++)
            {
                ItemIndex++;

                Recipes.ForEach(x => x.Input = x.Input.Replace(Items[I].ID + "|", ItemIndex.ToString() + "|"));
                Recipes.ForEach(x => x.Output = x.Output.Replace(Items[I].ID + "|", ItemIndex.ToString() + "|"));

                AltRecipes.ForEach(x => x.Input = x.Input.Replace(Items[I].ID + "|", ItemIndex.ToString() + "|"));
                AltRecipes.ForEach(x => x.Output = x.Output.Replace(Items[I].ID + "|", ItemIndex.ToString() + "|"));

                GenRecipes.ForEach(x => x.Input = x.Input.Replace(Items[I].ID + "|", ItemIndex.ToString() + "|"));
                GenRecipes.ForEach(x => x.Output = x.Output.Replace(Items[I].ID + "|", ItemIndex.ToString() + "|"));

                Items[I].ID = ItemIndex.ToString();
            }

            for (int I = ModBIndex; I < Buildings.Count; I++)
            {
                BuildingIndex++;

                Recipes.ForEach(x => x.Factory = x.Factory.Replace(Buildings[I].ID + "|", BuildingIndex.ToString() + "|"));
                AltRecipes.ForEach(x => x.Factory = x.Factory.Replace(Buildings[I].ID + "|", BuildingIndex.ToString() + "|"));
                GenRecipes.ForEach(x => x.Factory = x.Factory.Replace(Buildings[I].ID + "|", BuildingIndex.ToString() + "|"));

                Buildings[I].ID = BuildingIndex.ToString();
            }

            Recipes.ForEach(x => { RPIndex++; x.ID = RPIndex.ToString(); });
            AltRecipes.ForEach(x => { AltRPIndex++; x.ID = AltRPIndex.ToString(); });
            GenRecipes.ForEach(x => { GenRPIndex++; x.ID = GenRPIndex.ToString(); });
        }

        public static void RemoveMod(string ModName)
        {
            ReadMods();
            if (!ModList.Contains(ModName)) throw new Exception("Mod Does Not Exist.");
            ModsLoaded = false;
            ModList.Remove(ModName);
            Directory.Delete(GetDefPath() + "\\" + ModName, true);
            SaveMods();
        }

        public static void ImportMod(string[] FilePath)
        {
            foreach (string Mod in FilePath)
                ImportMod(Mod);
        }

        public static void ImportMod(string FilePath)
        {
            ModsLoaded = false;
            if (!File.Exists(FilePath)) throw new FileNotFoundException(
                $"File '{FilePath.Split('\\').Last()}' Was Not Found At '{FilePath}'");
            string DR = GetDefPath();// + $"\\{FilePath.Replace(".zip","").Split('\\').Last()}"
            Directory.CreateDirectory(DR);
            ZipFile.ExtractToDirectory(FilePath, DR);
            ModList.Add(FilePath.Replace(".zip", "").Split('\\').Last());
            SaveMods();
        }

        static string GetDefPath()
        {
            string Path;
            if (bRelease)
                Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SFP Planner\\Mods";
            else
                Path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var s = Path.Split('\\').ToList();
            s.Remove(s.Last());
            Path = string.Join("\\", s) + "\\Mods\\";
            Directory.CreateDirectory(Path);
            return Path;
        }

        static void SaveMods()
        {
            string DataFile = GetDefPath() + "\\Mods.dat"; ;
            var Writer = new StreamWriter(DataFile);
            Writer.WriteLine(string.Join("\r\n", ModList));
            Writer.Close();
        }

        static void ReadMods()
        {
            if (!File.Exists(GetDefPath() + "\\Mods.dat")) SaveMods();
            ModList.Clear();
            string DataFile = GetDefPath() + "\\Mods.dat";
            var Reader = new StreamReader(DataFile);
            while (!Reader.EndOfStream)
                ModList.Add(Reader.ReadLine() ?? "");
            Reader.Close();
            ModList.Remove("");
        }
    }
}
