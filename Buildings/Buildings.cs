using System;
using System.Data;

namespace SFPCalculator.Buildings
{
    /// <summary>
    /// A Production Building
    /// </summary>
    public class Buildings
    {
        /// <summary>
        /// DB ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Building Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Production Category
        /// </summary>
        public Category Category { get; set; }
        /// <summary>
        /// Building Power Useage Or Production
        /// </summary>
        public double PowerUsed { get; set; }

        /// <summary>
        /// A Production Building
        /// </summary>
        /// <param name="ID">DB ID</param>
        /// <param name="Name">Name</param>
        /// <param name="Category">Category</param>
        /// <param name="PowerUsed">Power Used Or Produce</param>
        public Buildings(int ID, string Name, Category Category, double PowerUsed)
        {
            this.ID = ID;
            this.Name = Name;
            this.Category = Category;
            this.PowerUsed = PowerUsed;
        }

        /// <summary>
        /// Returns Property Value By Name
        /// </summary>
        /// <param name="Property">Property Name</param>
        /// <returns></returns>
        public object Select(string Property) => Select(Prop.ToProperty(Property));
        /// <summary>
        /// Returns Property Value By enum
        /// </summary>
        /// <param name="Property">Property enum</param>
        /// <returns></returns>
        public object Select(Property Property)
        {
            switch (Property)
            {
                case Property.ID: return ID;
                case Property.Name: return Name;
                case Property.Category: return Category;
                case Property.PowerUsed: return PowerUsed;
                default: return null;
            }
        }
    }
}
