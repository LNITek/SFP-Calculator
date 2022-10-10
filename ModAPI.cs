namespace SFPCalculator
{
    /// <summary>
    /// The API To Handel All Of Your Moding Needs
    /// </summary>
    public class SFPMods
    {
        /// <summary>
        /// Add A Mod
        /// </summary>
        /// <param name="FilePath">The Path To The Mod File</param>
        public static void AddMods(string FilePath)=>
            ModsManager.ImportMod(FilePath);

        /// <summary>
        /// Add Multabel Mods
        /// </summary>
        /// <param name="FilePaths">A List Of The Paths To The Mod Files</param>
        public static void AddMods(string[] FilePaths) =>
            ModsManager.ImportMod(FilePaths);

        /// <summary>
        /// Remove A Mod
        /// </summary>
        /// <param name="ModName">The Name Of The Mod</param>
        public static void DeleteMods(string ModName) =>
            ModsManager.RemoveMod(ModName);

        /// <summary>
        /// Remove Multable Mods
        /// </summary>
        /// <param name="ModNames">List Of The Mod Names</param>
        public static void DeleteMods(string[] ModNames)
        {
            foreach (var FP in ModNames)
                ModsManager.RemoveMod(FP);
        }

        /// <summary>
        /// Returns List Of Names For All Active Mods
        /// </summary>
        /// <returns></returns>
        public static string[] ListMods() =>
            ModsManager.ModList.ToArray();
    }
}
