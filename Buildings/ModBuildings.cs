using System;
using System.Data;

namespace SFPCalculator.Buildings
{
    public class ModBuilding
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public double PowerUsed { get; set; }

        public ModBuilding(string ID, string Name, Category Category, double PowerUsed)
        {
            this.ID = ID;
            this.Name = Name;
            this.Category = Category;
            this.PowerUsed = PowerUsed;
        }

        public Buildings ToBuildings() => new Buildings(Convert.ToInt32(ID), Name, Category, PowerUsed);
    }
}
