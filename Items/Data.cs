using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SFPCalculator.Items
{
    /// <summary>
    /// DB API
    /// </summary>
    public static class Data
    {
        /// <summary>
        /// Returns A List Of All Items
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Items> GetItems() => GetAllData();

        static IEnumerable<Items> GetAllData()
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

        /// <summary>
        /// Get Item By Property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key">Property</param>
        /// <param name="Value">Property Value</param>
        /// <returns>List Of Buildings</returns>
        public static List<Items> GetItems<T>(Property Key, T Value) =>
            GetAllData().ToList().FindAll(el => el.Select(Key).ToString().ToLower() == Value.ToString().ToLower());

        /// <summary>
        /// Get Item By Property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key">Property</param>
        /// <param name="Value">Property Value</param>
        /// <returns>List Of Buildings</returns>
        public static List<Items> GetItems<T>(string Key, T Value) =>
            GetAllData().ToList().FindAll(el => el.Select(Key).ToString().ToLower() == Value.ToString().ToLower());

        /// <summary>
        /// Get Item By Property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key">Property</param>
        /// <param name="Values">List OF Property Values</param>
        /// <returns>List Of Buildings</returns>
        public static List<Items> GetItems<T>(Property Key, IEnumerable<T> Values) =>
            GetAllData().ToList().FindAll(el => Values.Select(x => x.ToString().ToLower())
            .Contains(el.Select(Key).ToString().ToLower()));
    }
}
