using Microsoft.EntityFrameworkCore;
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
using static GutKaz19.MainWindow;

namespace GutKaz19
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Phone phoneToUpdate;

        public MainWindow()
        {
            InitializeComponent();
            mainDataGridView.ItemsSource = DatabaseControl.GetPhonesForView();
            mainDataGridView.SelectionChanged += mainDataGridView_SelectionChanged;
        }

        public string Error
        {
            get { return null; }
        }

        private void mainDataGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            phoneToUpdate = mainDataGridView.SelectedItem as Phone;
        }

        public class Phone
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int CompanyId { get; set; }
            public decimal Price { get; set; }
            public Company CompanyEntity { get; set; }
        }

        public class Company
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string CEO { get; set; }
            public decimal Capital { get; set; }
            public List<Phone> PhoneEntities { get; set; }
        }

        public class DbAppContext : DbContext
        {
            public DbSet<Phone> Phones { get; set; }

            public DbSet<Company> Company { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=root;Database=GutKaz18.1");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Phone>().HasOne(p => p.CompanyEntity)
                                            .WithMany(p => p.PhoneEntities);
            }
        }

        public static class DatabaseControl
        {
            public static List<Phone> GetPhonesForView()
            {
                using (DbAppContext ctx = new DbAppContext())
                {
                    return ctx.Phones.Include(p => p.CompanyEntity).ToList();
                }

            }

            public static List<Company> GetCompanies()
            {
                using (DbAppContext ctx = new DbAppContext())
                {
                    return ctx.Company.Include(p => p.PhoneEntities).ToList();
                }

            }

            public static void AddPhone(Phone phone)
            {
                using (DbAppContext ctx = new DbAppContext())
                {
                    ctx.Phones.Add(phone);
                    ctx.SaveChanges();
                }
            }

            public static void UpdatePhone(Phone phone)
            {
                using (DbAppContext ctx = new DbAppContext())
                {
                    Phone _phone = ctx.Phones.FirstOrDefault(p => p.Id == phone.Id);
                    _phone.Title = phone.Title;
                    _phone.Price = phone.Price;
                    _phone.CompanyId = phone.CompanyId;
                    ctx.SaveChanges();
                }
            }

            public static void DeletePhone(int phoneId)
            {
                using (DbAppContext ctx = new DbAppContext())
                {
                    Phone phone = ctx.Phones.FirstOrDefault(p => p.Id == phoneId);
                    if (phone != null)
                    {
                        ctx.Phones.Remove(phone);
                        ctx.SaveChanges();
                    }
                }
            }
        }


        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (phoneToUpdate != null)
            {
                MessageBoxResult result = MessageBox.Show("Ты уверен!?!?!?!", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    DatabaseControl.DeletePhone(phoneToUpdate.Id);
                    mainDataGridView.ItemsSource = null;
                    mainDataGridView.ItemsSource = DatabaseControl.GetPhonesForView();
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Phone p = mainDataGridView.SelectedItem as Phone;
            AddWindow win = new AddWindow(p);
            win.Owner = this;
            win.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Phone p = mainDataGridView.SelectedItem as Phone; 
            if (p != null)
            {
                EditWindow win = new EditWindow(p);
                win.Owner = this;
                win.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите элемент для изменения");
            }
        }
        public void RefreshTable()
        {
            mainDataGridView.ItemsSource = null;
            mainDataGridView.ItemsSource = DatabaseControl.GetPhonesForView();
        }

    }
}
