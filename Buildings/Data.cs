using SFPCalculator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SFPCalculator.Buildings
{
    public static class Data
    {
        public static List<Buildings> GetBuildings() => GetAllData();

        static List<Buildings> GetAllData()
        {
            int I = 0;
            while (!ModsManager.ModsLoaded)
            {
                I++;
                if (I >= 100) throw new Exception("Took To Long To Load Mods | Ex-B.D");
                Thread.Sleep(1000);
            }
            var Buildings = DBManager.GetData<Buildings>(Properties.Resources.DBBuildings);
            var Mods = ModsManager.Buildings;
            Mods.ForEach(x => Buildings.Add(x.ToBuildings()));
            return Buildings;
        }

        public static List<Buildings> GetBuildings<T>(Property Key, T Value) => 
            GetAllData().FindAll(el => el.Select(Key).ToLower() == Value.ToString().ToLower()).ToList();

        public static List<Buildings> GetBuildings<T>(string Key, T Value) => 
            GetAllData().FindAll(el => el.Select(Key).ToLower() == Value.ToString().ToLower()).ToList();

        public static List<Buildings> GetBuildings<T>(Property Key, List<T> Values) =>
            GetAllData().FindAll(el => Values.Select(x => x.ToString().ToLower()).Contains(el.Select(Key).ToLower())).ToList();
    }
}
