using System;
using System.Data;

namespace SFPCalculator.Items
{
    public class ModItem
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }

        public ModItem(string ID, string Name, Category Category)
        {
            this.ID = ID;
            this.Name = Name;
            this.Category = Category;
        }

        public Items ToItems() => new Items(Convert.ToInt32(ID), Name, Category);
    }
}
