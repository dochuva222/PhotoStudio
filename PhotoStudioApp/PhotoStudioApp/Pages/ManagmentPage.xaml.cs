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
    /// <summary>
    /// Interaction logic for ManagmentPage.xaml
    /// </summary>
    public partial class ManagmentPage : Page
    {
        public ManagmentPage()
        {
            InitializeComponent();
            MainWindow.CurrentMainWindow.Title = "Управление";
        }

        private void bAddStudio_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StudioPage(new Studio()));
        }
        private void dgStudios_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedStudio = dgStudios.SelectedItem as Studio;
            if (selectedStudio == null)
                return;
            NavigationService.Navigate(new StudioPage(selectedStudio));
        }

        private void bDeletesStudio_Click(object sender, RoutedEventArgs e)
        {
            var selectedStudio = dgStudios.SelectedItem as Studio;
            if (selectedStudio == null)
            {
                MessageBox.Show("Выберите студию");
                return;
            }
            MainWindow.DB.Studio.Remove(selectedStudio);
            MainWindow.DB.SaveChanges();
            Refresh();
        }

        private void bAddRequisite_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RequisitePage(new Requisite()));
        }

        private void dgRequisites_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedRequisite = dgRequisites.SelectedItem as Requisite;
            if (selectedRequisite == null)
                return;
            NavigationService.Navigate(new RequisitePage(selectedRequisite));
        }

        private void bDeleteRequisite_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequisite = dgRequisites.SelectedItem as Requisite;
            if (selectedRequisite == null)
            {
                MessageBox.Show("Выберите реквизит");
                return;
            }
            MainWindow.DB.Requisite.Remove(selectedRequisite);
            MainWindow.DB.SaveChanges();
            Refresh();
        }

        private void bAddPhotograph_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PhotographPage(new Photograph()));
        }

        private void dgPhotographers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedPhotograph = dgPhotographers.SelectedItem as Photograph;
            if (selectedPhotograph == null)
                return;
            NavigationService.Navigate(new PhotographPage(selectedPhotograph));
        }

        private void bDeletePhotograph_Click(object sender, RoutedEventArgs e)
        {
            var selectedPhotograph = dgPhotographers.SelectedItem as Photograph;
            if (selectedPhotograph == null)
            {
                MessageBox.Show("Выберите фотографа");
                return;
            }
            MainWindow.DB.Photograph.Remove(selectedPhotograph);
            MainWindow.DB.SaveChanges();
            Refresh();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        
        private void Refresh()
        {
            dgStudios.ItemsSource = MainWindow.DB.Studio.ToList();
            dgRequisites.ItemsSource = MainWindow.DB.Requisite.ToList();
            dgPhotographers.ItemsSource = MainWindow.DB.Photograph.ToList();
        }

        
    }
}
