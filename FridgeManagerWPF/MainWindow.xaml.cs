using FridgeManagerWPF.Modules;
using FridgeManagerWPF.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace FridgeManagerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 
    /// 
    /// 
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<GroceryItem> groceries = new ObservableCollection<GroceryItem>();
        private ObservableCollection<GroceryItem> search = new ObservableCollection<GroceryItem>();
        private string fridge = "data/Fridge.xml";
        AppSettings settings;

        public MainWindow()
        {
            InitializeComponent();

            //data grid shit
            DataGrid.ItemsSource = groceries;

            //loading shit
            GroceryManager.Load(fridge, ref groceries);
            loadSettings();

            //specific shit
            dtp_Expiration.PreviewTextInput += DateTimePicker_PreviewTextInput;
            addRandom(100);
            changeColor(12, Colors.Red);
        }


        

        #region buttons

        private void bt_Save_Click(object sender, RoutedEventArgs e)
        {
            GroceryManager.Save(fridge, groceries);
        }

        private void bt_AddItem_Click(object sender, RoutedEventArgs e)
        {

            GroceryItem item = GroceryManager.create(tb_Name.Text, tb_Amount.Text, tb_Description.Text, cb_CategoryAdd.SelectedItem.ToString(), dtp_Expiration.Value);

            if(item != null )
            {
                groceries.Add(item);
            }
            else
            {
                MessageBox.Show("didnt add item");
            }
        }

        private void bt_AddCategory_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(cb_CategoryAdd.Text))
            {
                MessageBox.Show("Not a valid text");
            }

            addCategory(cb_CategoryAdd.Text);

            cb_CategoryAdd.Items.Add(cb_CategoryAdd.Text);
            cb_CategoryAdd.Text = string.Empty;
        }

        #endregion



        #region functionality

        private void loadSettings()
        {
            settings = Settings.LoadSettings();

            foreach(string category in settings.categories)
            {
                cb_CategoryAdd.Items.Add (category);
                cb_SearchCategories.Items.Add(category);
            }

            foreach(string category in settings.searchSpecific)
            {
                cb_SearchCategories.Items.Add(category);
            }
        }

        private void addCategory(string category)
        {
            if (!settings.categories.Contains(category))
            {
                settings.categories.Add(category);
                Settings.save(settings);
            }
        }

        private void changeColor(int days, Color color)
        {
            foreach (var item in DataGrid.Items)
            {
                if (item is GroceryItem)
                {
                    var dateTimeProperty = ((GroceryItem)item).Expiration;
                    if (dateTimeProperty != null)
                    {
                        if ((DateTime.Now - dateTimeProperty).Days <= days)
                        {
                            DataGridRow row = (DataGridRow)DataGrid.ItemContainerGenerator.ContainerFromItem(item);
                            if (row != null)
                            {
                                row.Background = new SolidColorBrush(color);
                            }
                        }
                    }
                }
            }
        }

        private void GenerateColumns()
        {
            DataGrid.Columns.Add(new DataGridTextColumn { Header = "Name", Binding = new Binding("Name") });
            DataGrid.Columns.Add(new DataGridTextColumn { Header = "Amount", Binding = new Binding("Amount") });
            DataGrid.Columns.Add(new DataGridTextColumn { Header = "Description", Binding = new Binding("Description") });
            DataGrid.Columns.Add(new DataGridTextColumn { Header = "Category", Binding = new Binding("Category") });
            DataGrid.Columns.Add(new DataGridTextColumn { Header = "Expiration", Binding = new Binding("Expiration") });
        }
        
        private void DateTimePicker_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private void addRandom(int n)
        {
            Random rand = new Random();

            for (int i = 0; i < n; i++)
            {
                string[] names = { "Apple", "Banana", "Orange", "Milk", "Bread", "Eggs" };
                string[] descriptions = { "Fresh", "Organic", "Imported", "Local" };
                string[] categories = { "Fruit", "Dairy", "Bakery" };

                string name = names[rand.Next(names.Length)];
                float amount = (float)(rand.NextDouble() * 100); // Random amount between 0 and 100
                string description = descriptions[rand.Next(descriptions.Length)];
                string category = categories[rand.Next(categories.Length)];
                DateTime expiration = DateTime.Now.AddDays(rand.Next(-5, 60)); // Random expiration within 1-30 days from now

                groceries.Add(new GroceryItem(name, amount, description, category, expiration));
            }
        }

        #endregion

    }
}
