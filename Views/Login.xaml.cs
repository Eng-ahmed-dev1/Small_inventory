
using Inventory.Model;
using Inventory.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inventory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        private void Loginbtn(object sender, RoutedEventArgs e)
        {

            try
            {
                using var db = new InventoryDB();


                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    ErrorMessageLab.Content = "Enter Valied UserName";
                    txtUsername.Text = "";
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Password))
                {
                    ErrorMessageLab.Content = "Enter Valied Password";
                    txtPassword.Password = "";
                    return;
                }

                var user = db.Users.FirstOrDefault(x => x.UserName == txtUsername.Text);
                if (user == null)
                {
                    ErrorMessageLab.Content = "User Not Found !!";
                    txtUsername.Text = "";
                    txtPassword.Password = "";
                    return;
                }
                if (user?.Password != txtPassword.Password)
                {
                    ErrorMessageLab.Content = "Incorrect Password !!";
                    txtPassword.Password = "";
                    return;
                }
                if (user.Role == "Admin")
                {
                    new AdminDashBoard().Show();
                    this.Close();
                }
                if (user.Role == "Customer")
                {
                    
                        var newCart = new Cart
                        {
                            Cust_id = user.User_id,
                        };
                        db.Cart.Add(newCart);
                        db.SaveChanges();
                    

                    new CustomerDashBoard(user.User_id, newCart.Cart_id).Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
                return;
            }
        }
    }
}