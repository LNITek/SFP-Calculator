using System;
using System.Data;

namespace SFPCalculator.Buildings
{
    public class Buildings
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public double PowerUsed { get; set; }

        public Buildings(int ID, string Name, Category Category, double PowerUsed)
        {
            this.ID = ID;
            this.Name = Name;
            this.Category = Category;
            this.PowerUsed = PowerUsed;
        }

        public string Select(string Property) => Select(Prop.ToProperty(Property));
        public string Select(Property Prop)
        {
            switch (Prop)
            {
                case Property.ID: return ID.ToString();
                case Property.Name: return Name.ToString();
                case Property.Category: return Category.ToString();
                case Property.PowerUsed: return PowerUsed.ToString();
                default: return null;
            }
        }
    }
}
