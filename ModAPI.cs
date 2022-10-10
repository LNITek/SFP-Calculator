using SFPCalculator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPCalculator
{
    public class SFPMods
    {
        public static void AddMods(string FilePath)=>
            ModsManager.ImportMod(FilePath);

        public static void AddMods(string[] FilePath) =>
            ModsManager.ImportMod(FilePath);

        public static void DeleteMods(string FilePath) =>
            ModsManager.RemoveMod(FilePath);

        public static void DeleteMods(string[] FilePath)
        {
            foreach (var FP in FilePath)
                ModsManager.RemoveMod(FP);
        }

        public static string[] ListMods() =>
            ModsManager.ModList.ToArray();
    }
}
