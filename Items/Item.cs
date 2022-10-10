using System;
using System.Data;

namespace SFPCalculator.Items
{
    public class Items
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }

        public Items(int ID, string Name, Category Category)
        {
            this.ID = ID;
            this.Name = Name;
            this.Category = Category;
        }

        public string Select(string Property) => Select(Prop.ToProperty(Property));
        public string Select(Property Prop)
        {
            switch (Prop)
            {
                case Property.ID: return ID.ToString();
                case Property.Name: return Name.ToString();
                case Property.Category: return Category.ToString();
                default: return null;
            }
        }
    }
}
