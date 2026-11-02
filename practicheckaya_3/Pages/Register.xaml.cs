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
using practicheckaya_3.Models; 
using PasswordHasherLibrary;
using System.Text.RegularExpressions;

namespace practicheckaya_3.Pages
{
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Password;
            string passwordRepeat = txtPasswordRepeat.Password;

            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Введите логин!");
                return;
            }

            if (!Regex.IsMatch(login, @"^[a-zA-Z0-9_]{4,20}$"))
            {
                MessageBox.Show("Логин должен содержать только латинские буквы, цифры и _ (от 4 до 20 символов)!");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите пароль!");
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать минимум 6 символов!");
                return;
            }

            // Проверка: хотя бы одна заглавная, одна строчная и одна цифра
            if (!Regex.IsMatch(password, @"[A-Z]") || !Regex.IsMatch(password, @"[a-z]") || !Regex.IsMatch(password, @"[0-9]"))
            {
                MessageBox.Show("Пароль должен содержать хотя бы одну заглавную букву, одну строчную букву и одну цифру!");
                return;
            }

            if (password != passwordRepeat)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }


            try
            {
                var db = Helper.GetContext();

                var existingUser = db.UserAuth.FirstOrDefault(u => u.login == login);
                if (existingUser != null)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!");
                    return;
                }

                Hasher hasher = new Hasher();
                string hashedPassword = hasher.GetHash(password);

                var newUser = new UserAuth
                {
                    login = login,
                    password_hash = hashedPassword
                };

                db.UserAuth.Add(newUser);
                db.SaveChanges();

                MessageBox.Show("Учетная запись успешно создана!");

                NavigationService.Navigate(new Autho());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Autho());
        }
    }
}
