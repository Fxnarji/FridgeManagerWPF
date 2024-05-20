using FridgeManagerWPF.Modules;
using FridgeManagerWPF.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interop;

namespace FridgeManagerWPF
{
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<GroceryItem> _groceries = new ObservableCollection<GroceryItem>();
        private ObservableCollection<GroceryItem> _search = new ObservableCollection<GroceryItem>();
        private readonly string _fridge = "data/Fridge.xml";
        AppSettings _settings;
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }



        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            //loading shit
            GroceryManager.Load(_fridge, ref _groceries);

            //data grid shit
            DataGrid.ItemsSource = _groceries;
            LoadSettings();

            //specific shit
            dtp_Expiration.PreviewTextInput += DateTimePicker_PreviewTextInput;

            DeleteCommand = new RelayCommand<GroceryItem>(DeleteItem);
            EditCommand = new RelayCommand<GroceryItem>(EditItem);
        }
        

        #region buttons

        private void bt_Save_Click(object sender, RoutedEventArgs e)
        {
            GroceryManager.Save(_fridge, _groceries);
        }

        private void bt_AddItem_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(tb_Name.Text) || string.IsNullOrEmpty(tb_Amount.Text) || dtp_Expiration.Value == null)
            {
                return;
            }

            GroceryItem item = GroceryManager.Create(tb_Name.Text, tb_Amount.Text, tb_Description.Text, cb_CategoryAdd.SelectedItem.ToString(), dtp_Expiration.Value);

            if(item != null )
            {
                _groceries.Add(item);
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

            AddCategory(cb_CategoryAdd.Text);

            cb_CategoryAdd.Items.Add(cb_CategoryAdd.Text);
            cb_CategoryAdd.Text = string.Empty;


        }

        #endregion
        
        #region functionality

        private void LoadSettings()
        {
            _settings = Settings.LoadSettings();


            //set categories
            foreach(string category in _settings.categories)
            {
                cb_CategoryAdd.Items.Add (category);
                cb_SearchCategories.Items.Add(category);
            }

            //set search categories
            foreach(string category in _settings.searchSpecific)
            {
                cb_SearchCategories.Items.Add(category);
            }

            //set maxDays
            tb_Days.Text = _settings.maxDays.ToString();
        }

        private void AddCategory(string category)
        {
            if (!_settings.categories.Contains(category))
            {
                _settings.categories.Add(category);
                Settings.save(_settings);
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
        
        private static void DateTimePicker_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private void AddRandom(int n)
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

                _groceries.Add(new GroceryItem(name, amount, description, category, expiration));
            }
        }



        private void DeleteItem(GroceryItem item)
        {
            if(item == null) return;
            _groceries.Remove(item);

        }

        private void EditItem(GroceryItem item)
        {
            MessageBox.Show("Hiii~");
        }


        #endregion

        private void tb_Days_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

































        public GroceryItem CanBeChopped(bool UsingAxe)
        {
            throw new NotImplementedException();
        }
    }
}
