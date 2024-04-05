using FridgeManagerWPF.Modules;
using FridgeManagerWPF.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Xceed.Wpf.Toolkit.Core.Converters;
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
        SerializableColor yellow = new SerializableColor(Colors.Yellow);
        SerializableColor red= new SerializableColor(Colors.Red);

        public MainWindow()
        {
            InitializeComponent();

            //loading shit
            GroceryManager.Load(fridge, ref groceries);

            //data grid shit
            DataGrid.ItemsSource = groceries;
            loadSettings();
            setRowColors(15, yellow);

            //specific shit
            dtp_Expiration.PreviewTextInput += DateTimePicker_PreviewTextInput;
        }


        

        #region buttons

        private void bt_Save_Click(object sender, RoutedEventArgs e)
        {
            GroceryManager.Save(fridge, groceries);
        }

        private void bt_AddItem_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(tb_Name.Text) || string.IsNullOrEmpty(tb_Amount.Text) || dtp_Expiration.Value == null)
            {
                return;
            }

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


            //set categories
            foreach(string category in settings.categories)
            {
                cb_CategoryAdd.Items.Add (category);
                cb_SearchCategories.Items.Add(category);
            }

            //set search categories
            foreach(string category in settings.searchSpecific)
            {
                cb_SearchCategories.Items.Add(category);
            }

            //set maxDays
            tb_Days.Text = settings.maxDays.ToString();
        }

        private void addCategory(string category)
        {
            if (!settings.categories.Contains(category))
            {
                settings.categories.Add(category);
                Settings.save(settings);
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

        private void setRowColors(int days, SerializableColor color)
        {
            foreach(GroceryItem item in groceries)
            {
                TimeSpan difference = item.Expiration - DateTime.Now;

                if(difference.Days <= days && difference.Days > 0)
                {
                    item.RowColor = color;
                }
            }
            DataGrid.Items.Refresh();
        }

        #endregion

        private void tb_Days_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
