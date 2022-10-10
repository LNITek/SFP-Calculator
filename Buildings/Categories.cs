using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPCalculator.Buildings
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
    /// Buildings Categories
    /// </summary>
    public enum Category
    {
        /// <summary>
        /// Extracts Items
        /// </summary>
        Extraction,
        /// <summary>
        /// Use Items To Make Items
        /// </summary>
        Production,
        /// <summary>
        /// Creates Power
        /// </summary>
        Generators,
        /// <summary>
        /// Special \ Non Useful
        /// </summary>
        Special,
        /// <summary>
        /// Other...
        /// </summary>
        Other
    }
}
