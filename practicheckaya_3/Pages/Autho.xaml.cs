using Microsoft.Win32;
using PasswordHasherLibrary;
using practicheckaya_3.Models;
using practicheckaya_3.Services;
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
        private int click = 0;
        public Autho()
        {
            InitializeComponent();
        }
        private void GenerateCaptcha()
        {
            captchaPanel.Visibility = Visibility.Visible;

            string captchaText = CaptchaGenerator.GenerateCaptchaText(6);
            tblCaptcha.Text = captchaText;

            txtCaptcha.Clear();
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

                if (click == 1)
                {
                    if (user != null)
                    {
                        MessageBox.Show("Вы вошли под: " + user.login);
                        NavigationService.Navigate(new Client());
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль!");
                        txtPassword.Clear();
                        GenerateCaptcha();
                    }
                }
                else
                {
                    if (user != null && txtCaptcha.Text.Trim() == tblCaptcha.Text)
                    {
                        MessageBox.Show("Вы вошли под: " + user.login);
                        NavigationService.Navigate(new Client());
                    }
                    else
                    {
                        MessageBox.Show("Введите данные заново!");
                        txtPassword.Clear();
                        GenerateCaptcha();
                    }
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
