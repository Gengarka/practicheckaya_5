using Microsoft.Win32;
using PasswordHasherLibrary;
using practicheckaya_3.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace practicheckaya_3.Pages
{
    public partial class Autho : Page
    {
        public Autho()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }

            try
            {
                var db = Helper.GetContext();
                Hasher hasher = new Hasher();
                string hashedInputPassword = hasher.GetHash(password);

                var user = db.UserAuth.FirstOrDefault(u => u.login == login && u.password_hash == hashedInputPassword);

                if (user != null)
                {
                    NavigationService.Navigate(new Client()); 
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка БД: " + ex.Message);
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Register()); 
        }
    }
}
