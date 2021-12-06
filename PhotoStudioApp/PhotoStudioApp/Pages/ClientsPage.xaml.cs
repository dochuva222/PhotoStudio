using PhotoStudioApp.Model;
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

namespace PhotoStudioApp.Pages
{
    public partial class ClientsPage : Page
    {
        public ClientsPage()
        {
            InitializeComponent();
            MainWindow.CurrentMainWindow.Title = "Клиенты";
        }

        private void bAddClient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientPage(new Client()));
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            var searchText = tbSearch.Text;
            if (string.IsNullOrWhiteSpace(searchText))
            {
                dgClients.ItemsSource = MainWindow.DB.Client.ToList();
                return;
            }
            dgClients.ItemsSource = MainWindow.DB.Client.Where(c => c.Lastname.ToLower().Contains(searchText.ToLower()) ||
                                                                    c.Firstname.ToLower().Contains(searchText.ToLower()) ||
                                                                    c.Middlename.ToLower().Contains(searchText.ToLower())).ToList();
        }

        private void dgClients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedClient = dgClients.SelectedItem as Client;
            if (selectedClient == null)
                return;
            NavigationService.Navigate(new ClientPage(selectedClient));
        }
    }
}
