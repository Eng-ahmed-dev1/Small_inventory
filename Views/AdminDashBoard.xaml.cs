using Inventory.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Inventory.Views
{
    /// <summary>
    /// Interaction logic for AdminDashBoard.xaml
    /// </summary>
    public partial class AdminDashBoard : Window
    {
        public AdminDashBoard()
        {
            InitializeComponent();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new InventoryDB();

                if (string.IsNullOrWhiteSpace(TxtName.Text) ||
                    string.IsNullOrWhiteSpace(TxtDescription.Text) ||
                    string.IsNullOrWhiteSpace(TxtPrice.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                if (!decimal.TryParse(TxtPrice.Text, out decimal price))
                {
                    MessageBox.Show("Price must be a decimal number.");
                    return;
                }

                var product = new Product
                {
                    Name = TxtName.Text,
                    Description = TxtDescription.Text,
                    Price = price
                  
                };

                db.Product.Add(product);
                db.SaveChanges();

                MessageBox.Show("Product added successfully!");
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
            }
        }

        private void ClearFields()
        {
            TxtName.Clear();
            TxtDescription.Clear();
            TxtPrice.Clear();
        }
    }
}