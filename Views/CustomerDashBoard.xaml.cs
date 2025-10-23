using Inventory.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
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
    /// Interaction logic for CustomerDashBoard.xaml
    /// </summary>
    public partial class CustomerDashBoard : Window
    {
        private int _CurrentUser;
        private int _UserCart;
        public CustomerDashBoard(int currentUser, int userCart)
        {
            InitializeComponent();
            _CurrentUser = currentUser;
            _UserCart = userCart;
            LoadProducts();
            LoadAllUsers();
        }
        private void LoadProducts()
        {
            using var db = new InventoryDB();

            var com = db.Product.Select(x => new
            {
                Display = x.Pro_id + "." + x.Name,
                x.Pro_id,
            }).ToList();

            Comb.ItemsSource = com;
            Comb.DisplayMemberPath = "Display";
            Comb.SelectedValuePath = "Pro_id";
            var name = db.Users.FirstOrDefault(x => x.User_id == _CurrentUser);
            if (name != null)
            {
                LabName.Content = name.UserName;
            }
        }
        private void LoadAllUsers()
        {
            using var db = new InventoryDB();

            var cartItems = db.Cart_item
                .Include(x => x.Cart)
                .Include(x => x.Cart.User)
                .Where(x => x.Cart.Cust_id == _CurrentUser)
                .Select(x => new
                {
                    CustId = x.Cart.Cust_id,
                    ProID = x.Pro_ID,
                    CartId = x.Cart_ID,
                    Quantity = x.Quantity
                })
                .ToList();

            DGD.ItemsSource = cartItems;
        }

        private void AddToCart(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new InventoryDB();

                var user = db.Users.Include(x => x.Carts).FirstOrDefault(x => x.User_id == _CurrentUser);
                if (user == null)
                {
                    MessageBox.Show("User not found!");
                    return;
                }

                if (Comb.SelectedValue == null)
                {
                    MessageBox.Show("Please select a product first.");
                    return;
                }

                var ChooseCombo = (int)Comb.SelectedValue;

                if (!int.TryParse(Quant.Text, out int quant))
                {
                    MessageBox.Show("Please enter quantity as a number.");
                    return;
                }

                var product = db.Product.FirstOrDefault(x => x.Pro_id == ChooseCombo);
                if (product == null)
                {
                    MessageBox.Show("Product not found!");
                    return;
                }

                if (product.InStockQuantity < quant)
                {
                    MessageBox.Show("Not enough stock for this product!");
                    return;
                }

                var NewCart_item = new Cart_item()
                {
                    Quantity = quant,
                    Pro_ID = ChooseCombo,
                    Cart_ID = _UserCart
                };

                db.Cart_item.Add(NewCart_item);
                product.InStockQuantity -= quant;
                db.SaveChanges();

                var cart = db.Cart
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                    .FirstOrDefault(c => c.Cart_id == _UserCart); 

                if (cart != null && cart.CartItems.Any())
                {
                    cart.TotalPrice = cart.CartItems.Sum(ci => (decimal)(ci.Quantity * ci.Product.Price));
                    db.SaveChanges();
                }

                MessageBox.Show("Item added to cart successfully!");
                LoadProducts();
                LoadAllUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}