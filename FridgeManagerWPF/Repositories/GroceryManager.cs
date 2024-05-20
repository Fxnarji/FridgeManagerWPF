using FridgeManagerWPF.Modules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace FridgeManagerWPF.Repositories
{
    public class GroceryManager
    {
        public static GroceryItem? Create(string name, string amount, string desc, string cat, DateTime? date)
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

        public static void AddRandomItem(int amount, ref ObservableCollection<GroceryItem> list)
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
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<GroceryItem>));

                ObservableCollection<GroceryItem> itemList = new ObservableCollection<GroceryItem>(list);

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    serializer.Serialize(writer, itemList);
                }

                MessageBox.Show("saved successfully!");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it more gracefully
                MessageBox.Show($"Error saving groceries: {ex.Message}");
            }
        }

        public static void SaveCSV(string filePath, ObservableCollection<GroceryItem> list)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    // Write the header
                    writer.WriteLine("Name,Quantity,Price");

                    // Write each grocery item
                    foreach (GroceryItem item in list)
                    {
                        writer.WriteLine($"{item.Name},{item.Amount},{item.Description}, {item.Category}, {item.Expiration}");
                    }
                }

                MessageBox.Show("Saved successfully!");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it more gracefully
                MessageBox.Show($"Error saving groceries: {ex.Message}");
            }
        }

        public static void Load(string filePath, ref ObservableCollection<GroceryItem> loadedGroceries)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<GroceryItem>));

                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        ObservableCollection<GroceryItem> loadedItems = (ObservableCollection<GroceryItem>)serializer.Deserialize(reader);
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

        public static void LoadCSV(string filePath, ref ObservableCollection<GroceryItem> loadedGroceries)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Read the header line
                        reader.ReadLine();

                        // Read each subsequent line
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            // Split the line by commas
                            string[] parts = line.Split(',');

                            // Create a GroceryItem object
                            GroceryItem item = new GroceryItem
                            {
                                Name = parts[0],
                                Amount = int.Parse(parts[1]),
                                Description = parts[2],
                                Category = parts[3],
                                Expiration = DateTime.Parse(parts[4])
                            };

                            // Add the item to the collection
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
