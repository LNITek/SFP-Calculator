using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SFPCalculator.Buildings
{
    /// <summary>
    /// DB API
    /// </summary>
    public static class Data
    {
        /// <summary>
        /// List Of Builings
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Buildings> GetBuildings() => GetAllData();

        static IEnumerable<Buildings> GetAllData()
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

        /// <summary>
        /// Get Buildings By Property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key">Property</param>
        /// <param name="Value">Property Value</param>
        /// <returns>List Of Buildings</returns>
        public static IEnumerable<Buildings> GetBuildings<T>(Property Key, T Value) =>
            GetAllData().ToList().FindAll(el => el.Select(Key).ToString().ToLower() == Value.ToString().ToLower());

        /// <summary>
        /// Get Buildings By Property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key">Property</param>
        /// <param name="Value">Property Value</param>
        /// <returns>List Of Buildings</returns>
        public static IEnumerable<Buildings> GetBuildings<T>(string Key, T Value) =>
            GetAllData().ToList().FindAll(el => el.Select(Key).ToString().ToLower() == Value.ToString().ToLower());

        /// <summary>
        /// Get Buildings By Property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key">Property</param>
        /// <param name="Values">List Of Property Values</param>
        /// <returns>List Of Buildings</returns>
        public static IEnumerable<Buildings> GetBuildings<T>(Property Key, IEnumerable<T> Values) =>
            GetAllData().ToList().FindAll(el => Values.Select(x => x.ToString().ToLower())
            .Contains(el.Select(Key).ToString().ToLower()));
    }
}
