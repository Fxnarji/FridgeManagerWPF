using System;
using System.Windows.Media;

namespace FridgeManagerWPF.Modules
{
    public class GroceryItem
    {
        public string Name { get; set; }
        public float Amount { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime Expiration { get; set; }



        public GroceryItem(string Name, float Amount, string Description, string Category, DateTime Expiration, Color Color)
        {
            this.Name = Name;
            this.Amount = Amount;
            this.Description = Description;
            this.Category = Category;
            this.Expiration = Expiration;
        }

        public GroceryItem(string Name, float Amount, string Description, string Category, DateTime Expiration)
        {
            this.Name = Name;
            this.Amount = Amount;
            this.Description = Description;
            this.Category = Category;
            this.Expiration = Expiration;
        }

        public GroceryItem()
        {

        }
    }

}
