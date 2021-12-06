using PhotoStudioApp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhotoStudioApp.Pages
{
    public partial class PhotosessionsPage : Page
    {
        public PhotosessionsPage()
        {
            InitializeComponent();
            cbThemes.ItemsSource = MainWindow.DB.Theme.ToList();
            MainWindow.CurrentMainWindow.Title = "Фотосессии";
        }

        private void bClearFilter_Click(object sender, RoutedEventArgs e)
        {
            tbSearch.Text = null;
            cbThemes.SelectedItem = null;
            dpStartDate.SelectedDate = null;
            dpEndDate.SelectedDate = null;
            Refresh();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void bAddPhotoSession_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PhotoSessionPage(new PhotoSession() { CreatedDate = DateTime.Now, PhotoSessionDate = DateTime.Now.Date }));
        }

        private void dgOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedPhotoSession = dgPhotoSessions.SelectedItem as PhotoSession;
            if (selectedPhotoSession == null)
                return;
            NavigationService.Navigate(new PhotoSessionPage(selectedPhotoSession));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            MainWindow.DB = new PhotoStudioEntities();
            var filtred = MainWindow.DB.PhotoSession.ToList();
            if (!string.IsNullOrWhiteSpace(tbSearch.Text))
                filtred = filtred.Where(f => f.Client.Lastname.ToLower().Contains(tbSearch.Text.ToLower()) ||
                                             f.Client.Firstname.ToLower().Contains(tbSearch.Text.ToLower()) ||
                                             f.Client.Middlename.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            if (cbThemes.SelectedItem != null)
                filtred = filtred.Where(f => f.ThemeId == (cbThemes.SelectedItem as Theme).Id).ToList();
            if (dpStartDate.SelectedDate != null)
                filtred = filtred.Where(f => f.PhotoSessionDate >= dpStartDate.SelectedDate).ToList();
            if (dpEndDate.SelectedDate != null)
                filtred = filtred.Where(f => f.PhotoSessionDate <= dpEndDate.SelectedDate).ToList();
            dgPhotoSessions.ItemsSource = filtred;
        }

        private void cbThemes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void dpStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void dpEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void bPhotosPage_Click(object sender, RoutedEventArgs e)
        {
            var selectedPhotoSession = dgPhotoSessions.SelectedItem as PhotoSession;
            if (!Directory.Exists(selectedPhotoSession.Catalog))
            {
                Directory.CreateDirectory(selectedPhotoSession.Catalog);
            }
            Process.Start(selectedPhotoSession.Catalog);
        }

        private void bRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedPhotoSession = dgPhotoSessions.SelectedItem as PhotoSession;
            if (selectedPhotoSession == null)
                return;
            MainWindow.DB.PhotoSession.Remove(selectedPhotoSession);
            MainWindow.DB.SaveChanges();
            Refresh();
        }

        private void dgPhotoSessions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedPhotoSession = dgPhotoSessions.SelectedItem as PhotoSession;
            if (selectedPhotoSession == null)
            {
                bDelete.Visibility = Visibility.Collapsed;
                return;
            }
            if (selectedPhotoSession.StatusId == 3)
                bDelete.Visibility = Visibility.Visible;
            else
                bDelete.Visibility = Visibility.Collapsed;
        }
    }
}
