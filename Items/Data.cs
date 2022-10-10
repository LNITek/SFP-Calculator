using SFPCalculator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SFPCalculator.Items
{
    public static class Data
    {
        public static List<Items> GetItems() => GetAllData();

        static List<Items> GetAllData()
        {
            int I = 0;
            while (!ModsManager.ModsLoaded)
            {
                I++;
                if (I >= 100) throw new Exception("Took To Long To Load Mods | Ex-I.D");
                Thread.Sleep(1000);
            }
            var Items = DBManager.GetData<Items>(Properties.Resources.DBItems);
            var Mods = ModsManager.Items;
            Mods.ForEach(x => Items.Add(x.ToItems()));
            return Items;
        }

        public static List<Items> GetItems<T>(Property Key, T Value) =>
            GetAllData().FindAll(el => el.Select(Key).ToLower() == Value.ToString().ToLower()).ToList();

        public static List<Items> GetItems<T>(string Key, T Value) =>
            GetAllData().FindAll(el => el.Select(Key).ToLower() == Value.ToString().ToLower()).ToList();

        public static List<Items> GetItems<T>(Property Key, List<T> Values) =>
            GetAllData().FindAll(el =>  Values.Select(x => x.ToString().ToLower()).Contains(el.Select(Key).ToLower())).ToList();
    }
}
