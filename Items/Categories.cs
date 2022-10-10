using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPCalculator.Items
{
    /// <summary>
    /// Category Converter
    /// </summary>
    public static class Categories
    {
        /// <summary>
        /// Converts Category To String
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string ToString(Category Value) => Value.ToString();
        /// <summary>
        /// Converts Category To String
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string ToString(int Value) => ((Category)Value).ToString();

        /// <summary>
        /// Convert To Category
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static Category ToCategory(int Value) => (Category)Value;
        /// <summary>
        /// Convert To Category
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static Category ToCategory(string Value) => (Category)Enum.Parse(typeof(Category), Value);
    }

    /// <summary>
    /// Items Categories
    /// </summary>
    public enum Category
    {
        #pragma warning disable CS1591
        Ores,
        Ingots,
        Material,
        Liquids,
        Gas,
        StdParts,
        IndParts,
        AdvParts,
        Nuclear,
        Consumed,
        Container,
        Special,
        FICSMAS,
        Other
        #pragma warning restore CS1591
    }
}
