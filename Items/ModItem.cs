using System;
using System.Data;

namespace SFPCalculator.Items
{
    /// <summary>
    /// Moded Item
    /// </summary>
    public class ModItem
    {
        /// <summary>
        /// Mod DB ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Category
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Moded Item
        /// </summary>
        public ModItem(string ID, string Name, Category Category)
        {
            this.ID = ID;
            this.Name = Name;
            this.Category = Category;
        }

        /// <summary>
        /// Converts Moded Item To An Item Object
        /// </summary>
        /// <returns></returns>
        public Items ToItems() => new Items(Convert.ToInt32(ID), Name, Category);
    }
}
