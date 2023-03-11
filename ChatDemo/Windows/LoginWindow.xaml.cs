using System;
using System.Collections.Generic;
using System.IO;
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

namespace ChatDemo.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            FillFromConfigFile();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (UsernameTB.Text == "" || PasswordTB.Password == "")
            {
                MessageBox.Show("Заполните данные");
                return;
            }

            App.user = App.db.Employee.Where(x => x.Username == UsernameTB.Text && x.Password == PasswordTB.Password).FirstOrDefault();
            if (App.user == null)
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }

            if ((bool)RememberMeCheckBox.IsChecked)
            {
                WriteToconfigFile($"Login: {UsernameTB.Text}");
                WriteToconfigFile($"Password: {PasswordTB.Password}");
            }

            new ChatsRoomWindow().Show();
            this.Close();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            var mes = MessageBox.Show("Выйти из приложения", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (mes == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void FillFromConfigFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(App.configPath))
                {
                    var str = sr.ReadLine();
                    if (String.IsNullOrEmpty(str))
                        return;

                    var str2 = sr.ReadLine();
                    if (String.IsNullOrEmpty(str2))
                        return;

                    UsernameTB.Text = str.Contains("Login") ? str.Replace("Login: ", "") : "";
                    PasswordTB.Password = str2.Contains("Password") ? str2.Replace("Password: ", "") : "";
                    sr.Close();
                }

                RememberMeCheckBox.IsChecked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void WriteToconfigFile(string str)
        {
            using (StreamWriter sw = new StreamWriter(App.configPath))
            {
                sw.WriteLine(str);
            }
        }

        private void RememberMeCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (RememberMeCheckBox.IsChecked == false)
            {
                UsernameTB.Clear();
                PasswordTB.Clear();
                WriteToconfigFile("");
            }
        }
    }
}
