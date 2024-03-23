using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
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
using static GutKaz19.MainWindow;

namespace GutKaz19
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow(Phone phone)
        {
            InitializeComponent();
            companyView.ItemsSource = DatabaseControl.GetCompanies();
        }
        private void AddPhoneButton_Click(object sender, RoutedEventArgs e)
        {
                if (!string.IsNullOrWhiteSpace(titleView.Text) && !string.IsNullOrWhiteSpace(companyView.Text) && !string.IsNullOrWhiteSpace(priceView.Text))
                {
                    if (decimal.TryParse(priceView.Text, out decimal price))
                    {
                        if (price < 0)
                        {
                            MessageBox.Show("Стоимость не может быть отрицательной", "Меее");
                            return;
                        }

                        DatabaseControl.AddPhone(new Phone
                        {
                            Title = titleView.Text,
                            CompanyId = (int)companyView.SelectedValue,
                            Price = Convert.ToDecimal(priceView.Text)
                        });
                        (this.Owner as MainWindow).RefreshTable();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Введи значения в цифрах", "Меее");
                    }
                }
                else
                {
                    MessageBox.Show("Заполни все ячейки", "Меее");
                }
        }
    }
}
