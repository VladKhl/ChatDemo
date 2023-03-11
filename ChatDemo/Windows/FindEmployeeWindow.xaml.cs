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

namespace ChatDemo.Windows
{
    /// <summary>
    /// Логика взаимодействия для FindEmployeeWindow.xaml
    /// </summary>
    public partial class FindEmployeeWindow : Window
    {
        public FindEmployeeWindow()
        {
            InitializeComponent(); 
            DepartamentsLstView.ItemsSource = App.db.Department.ToList();
        }

        private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            EmployeeLstView.ItemsSource = App.db.Employee.Where(x => x.Name.ToLower().Contains(SearchTxt.Text.ToLower())).ToList();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var checkBoxItem = (CheckBox)sender;
            if (checkBoxItem.IsChecked == true)
                EmployeeLstView.ItemsSource = App.db.Employee.Where(x => x.Name.ToLower().Contains(SearchTxt.Text.ToLower()) && x.Department.Name == checkBoxItem.Content.ToString()).ToList();
        }
    }
}
