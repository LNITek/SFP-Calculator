using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPCalculator.Items
{
    public static class Categories
    {
        public static string ToString(Category Value) => Value.ToString();
        public static string ToString(int Value) => ((Category)Value).ToString();

        public static Category ToCategory(int Value) => (Category)Value;
        public static Category ToCategory(string Value) => (Category)Enum.Parse(typeof(Category), Value);
    }

    public enum Category
    {
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
    }
}
