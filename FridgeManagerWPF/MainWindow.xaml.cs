﻿using FridgeManagerWPF.Modules;
using FridgeManagerWPF.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FridgeManagerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<GroceryItem> groceries = new ObservableCollection<GroceryItem>();
        private ObservableCollection<GroceryItem> search = new ObservableCollection<GroceryItem>();
        private string settings = "data/settings.json";
        private string fridge = "data/Fridge.xml";

        public MainWindow()
        {
            InitializeComponent();
            DataGrid.AutoGenerateColumns = true;
            DataGrid.ItemsSource = groceries;
            //GenerateColumns();

        }

        private void GenerateColumns()
        {
            DataGrid.Columns.Add(new DataGridTextColumn { Header = "Name", Binding = new Binding("Name") });
            DataGrid.Columns.Add(new DataGridTextColumn { Header = "Amount", Binding = new Binding("Amount") });
            DataGrid.Columns.Add(new DataGridTextColumn { Header = "Description", Binding = new Binding("Description") });
            DataGrid.Columns.Add(new DataGridTextColumn { Header = "Category", Binding = new Binding("Category") });
            DataGrid.Columns.Add(new DataGridTextColumn { Header = "Expiration", Binding = new Binding("Expiration") });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GroceryManager.addRandomItem(10, ref groceries);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GroceryManager.Save(fridge, groceries);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            GroceryManager.Clear(ref groceries);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            GroceryManager.Load(fridge, ref groceries);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string test = "";

            foreach (var item in groceries)
            {
                test += item.Name + "\n";
            }

            MessageBox.Show(test);
        }
    }
}
