using System;
using System.Data;

namespace SFPCalculator.Items
{
    /// <summary>
    /// Items For Production
    /// </summary>
    public class Items
    {
        /// <summary>
        /// DB ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Category
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Items Used In Resipes
        /// </summary>
        /// <param name="ID">DB ID</param>
        /// <param name="Name">Name</param>
        /// <param name="Category">Category</param>
        public Items(int ID, string Name, Category Category)
        {
            this.ID = ID;
            this.Name = Name;
            this.Category = Category;
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
                default: return null;
            }
        }
    }
}
