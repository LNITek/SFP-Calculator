using System;

namespace SFPCalculator.Recipes
{
    /// <summary>
    /// Property Converter
    /// </summary>
    public static class Prop
    {
        /// <summary>
        /// Converts Property To String
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string ToString(Property Value) => Value.ToString();
        /// <summary>
        /// Converts Property To String
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string ToString(int Value) => ((Property)Value).ToString();

        /// <summary>
        /// Convert To Property
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static Property ToProperty(int Value) => (Property)Value;
        /// <summary>
        /// Convert To Property
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static Property ToProperty(string Value) => (Property)Enum.Parse(typeof(Property), Value);
    }

    /// <summary>
    /// Recipe Properties
    /// </summary>
    public enum Property
    {
        #pragma warning disable CS1591
        ID,
        Name,
        Description,
        Category
        #pragma warning restore CS1591
    }
}
