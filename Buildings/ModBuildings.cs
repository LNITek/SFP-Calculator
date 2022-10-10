using System;
using System.Data;

namespace SFPCalculator.Buildings
{
    /// <summary>
    /// Moded Building
    /// </summary>
    public class ModBuilding
    {
        /// <summary>
        /// Mod DB ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Building Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Production Category
        /// </summary>
        public Category Category { get; set; }
        /// <summary>
        /// Power Used Or Produce
        /// </summary>
        public double PowerUsed { get; set; }

        /// <summary>
        /// Moded Builing
        /// </summary>
        /// <param name="ID">DB ID</param>
        /// <param name="Name">Name</param>
        /// <param name="Category">Category</param>
        /// <param name="PowerUsed">Power Used</param>
        public ModBuilding(string ID, string Name, Category Category, double PowerUsed)
        {
            this.ID = ID;
            this.Name = Name;
            this.Category = Category;
            this.PowerUsed = PowerUsed;
        }

        /// <summary>
        /// Converts Moded Building To A Useable Building Object
        /// </summary>
        /// <returns></returns>
        public Buildings ToBuildings() => new Buildings(Convert.ToInt32(ID), Name, Category, PowerUsed);
    }
}
