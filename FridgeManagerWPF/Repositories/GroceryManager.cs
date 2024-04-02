using FridgeManagerWPF.Modules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace FridgeManagerWPF.Repositories
{
    public class GroceryManager
    {
        public static GroceryItem? create(string name, string amount, string desc, string cat, DateTime? date)
        {
            if (!float.TryParse(amount, out float amount_f))
            {
                MessageBox.Show("ERORR YOU PUT WRONG STUFF I MURDER UR FAMILY");
                return null;
            }

            DateTime nn_date;
            if (date.HasValue)
                nn_date = date.Value;
            else
                return null;

            return new GroceryItem(name, amount_f, desc, cat, nn_date);

        }

        public static void addRandomItem(int amount, ref ObservableCollection<GroceryItem> list)
        {
            List<string> names = new List<string>
        {
            "Apples",
            "Bananas",
            "Bread",
            "Milk",
            "Eggs",
            "Cheese",
            "Chicken",
            "Rice",
            "Pasta",
            "Tomatoes"
        };

            List<string> descriptions = new List<string>
        {
            "Fresh and juicy",
            "Organic",
            "Whole grain",
            "Low-fat",
            "Farm-fresh",
            "Locally sourced",
            "Free-range",
            "Gluten-free",
            "Vegan-friendly",
            "Non-GMO"
        };

            Random random = new Random();

            for (int i = 0; i < amount; i++)
            {
                string name = names[random.Next(names.Count)];
                string description = descriptions[random.Next(descriptions.Count)];
                float randomAmount = (float)(random.NextDouble() * 10); // Random amount between 0 and 10
                DateTime expiration = DateTime.Now.AddDays(random.Next(1, 30)); // Random expiration date within next 30 days

                GroceryItem item = new GroceryItem(name, randomAmount, description, "Uncategorized", expiration);
                list.Add(item);
            }
        }

        public static void Save(string filePath, ObservableCollection<GroceryItem> list)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<GroceryItem>));

                // Convert ObservableCollection to List
                List<GroceryItem> itemList = new List<GroceryItem>();
                foreach (var item in list)
                {
                    itemList.Add(item);
                }

                // Open the file with FileMode.Create to create a new file if it doesn't exist
                // Use FileMode.Append to append data to an existing file or create a new file
                using (StreamWriter writer = new StreamWriter(filePath)) // Specify FileMode.Append
                {
                    serializer.Serialize(writer, itemList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving groceries: {ex.Message}");
            }
        }

        public static void Load(string filePath, ref ObservableCollection<GroceryItem> loadedGroceries)
        {
            loadedGroceries.Clear();

            try
            {
                // Create an XmlSerializer for the GroceryItem type
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<GroceryItem>));

                // Check if the file exists
                if (File.Exists(filePath))
                {
                    // Create a StreamReader to read the XML from the file
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Deserialize the XML into a list of groceries
                        ObservableCollection<GroceryItem> loadedItems = (ObservableCollection<GroceryItem>)serializer.Deserialize(reader);
                        // Convert the list to ObservableCollection
                        foreach (var item in loadedItems)
                        {
                            loadedGroceries.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions, e.g., log or show an error message
                Console.WriteLine($"Error loading groceries: {ex.Message}");
            }
        }

        public static void Clear(ref ObservableCollection<GroceryItem> list)
        {
            list.Clear();
        }

    }
}
