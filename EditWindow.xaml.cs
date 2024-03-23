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
using static GutKaz19.MainWindow;

namespace GutKaz19
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Phone phoneToUpdate;
        public EditWindow(Phone phone)
        {
            InitializeComponent();
            companyView.ItemsSource = DatabaseControl.GetCompanies();
            titleView.Text = phone.Title;
            companyView.SelectedValue = phone.CompanyEntity.Id;
            priceView.Text = phone.Price.ToString();
            phoneToUpdate = phone;
        }


        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (phoneToUpdate != null)
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

                        phoneToUpdate.Title = titleView.Text;
                        phoneToUpdate.CompanyId = (int)companyView.SelectedValue;
                        phoneToUpdate.Price = Convert.ToDecimal(priceView.Text);
                        DatabaseControl.UpdatePhone(phoneToUpdate);
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
}
