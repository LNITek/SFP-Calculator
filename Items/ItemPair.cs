
namespace SFPCalculator.Items
{
    /// <summary>
    /// A Extended Items Object
    /// </summary>
    public class ItemPair
    {
        /// <summary>
        /// The Item
        /// </summary>
        public Items Item { get; set; }
        /// <summary>
        /// Units Per Min
        /// </summary>
        public double PerMin { get; set; }
        /// <summary>
        /// Whether Its In Production
        /// </summary>
        public bool Production { get; set; } = true;
        /// <summary>
        /// Extra Data Slot
        /// </summary>
        public object Tag { get; set; } = null;

        /// <summary>
        /// A Extended Items Object
        /// </summary>
        public ItemPair(Items item, double perMin)
        {
            Item = item;
            PerMin = perMin;
        }
    }
}
