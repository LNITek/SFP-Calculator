using System;

namespace SFPCalculator.Buildings
{
    public static class Prop
    {
        public static string ToString(Property Value) => Value.ToString();
        public static string ToString(int Value) => ((Property)Value).ToString();

        public static Property ToProperty(int Value) => (Property)Value;
        public static Property ToProperty(string Value) => (Property)Enum.Parse(typeof(Property), Value);
    }

    public enum Property
    {
        ID,
        Name,
        Category,
        PowerUsed
    }
}
