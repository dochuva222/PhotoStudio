using PhotoStudioApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class PhotoSessionPage : Page
    {
        PhotoSession contextPhotoSession;
        ObservableCollection<PhotoSessionRequisite> currentRequisites;
        public PhotoSessionPage(PhotoSession postPhotoSession)
        {
            InitializeComponent();
            cbRequisites.ItemsSource = MainWindow.DB.Requisite.ToList();
            cbThemes.ItemsSource = MainWindow.DB.Theme.ToList();
            List<string> times = new List<string>();
            for (int i = 9; i <= 20; i += 3)
            {
                times.Add($"{TimeSpan.FromHours(i)} - {TimeSpan.FromHours(i).Add(TimeSpan.FromHours(3))}");
            }
            cbTimes.ItemsSource = times;
            //dpDate.BlackoutDates.AddDatesInPast();
            currentRequisites = new ObservableCollection<PhotoSessionRequisite>(MainWindow.DB.PhotoSessionRequisite.Where(r => r.PhotoSessionId == postPhotoSession.Id).ToList());
            dgRequisites.ItemsSource = currentRequisites;
            contextPhotoSession = postPhotoSession;
            DataContext = contextPhotoSession;
            if (contextPhotoSession.Id != 0)
            {
                spPhotoSessionInfo.IsEnabled = false;
                dpRequisites.IsEnabled = false;
                if (contextPhotoSession.Studio != null)
                    rbInStudio.IsChecked = true;
                else
                    rbOutStudio.IsChecked = true;
                cbTimes.SelectedItem = $"{contextPhotoSession.PhotoSessionTime} - {contextPhotoSession.PhotoSessionTime.Add(TimeSpan.FromHours(3))}";
                cbPhotographers.ItemsSource = MainWindow.DB.Photograph.ToList();
                cbStudios.ItemsSource = MainWindow.DB.Studio.ToList();
                cbClients.ItemsSource = MainWindow.DB.Client.ToList();
                cbPhotographers.SelectedItem = contextPhotoSession.Photograph;
                cbClients.SelectedItem = contextPhotoSession.Client;
                cbStudios.SelectedItem = contextPhotoSession.Studio;
                LoadPreviews();
            }

        }

        private void LoadPreviews()
        {
            if (!Directory.Exists(contextPhotoSession.Catalog))
                return;
            var files = Directory.GetFiles(contextPhotoSession.Catalog);
            for (int i = 0; i < 5; i++)
            {
                if (files.Length <= i)
                    break;
                var image = new Image() { Height = 150, Width = 200 };
                image.Source = Tools.Tools.BytesToImage(File.ReadAllBytes(files[i]));
                spPreviews.Children.Add(image);
            }
        }

        private void bAddRequisite_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequisite = cbRequisites.SelectedItem as Requisite;
            if (selectedRequisite == null)
            {
                MessageBox.Show("Выберите реквизит");
                return;
            }
            int amount = 0;
            try
            {
                amount = int.Parse(tbAmount.Text);
                if (amount <= 0)
                {
                    MessageBox.Show("Введите корректное количество");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Введите корректное количество");
                return;
            }
            var addedRequisite = new PhotoSessionRequisite() { Requisite = selectedRequisite, Amount = amount, PhotoSessionId = contextPhotoSession.Id };
            currentRequisites.Add(addedRequisite);
            MainWindow.DB.PhotoSessionRequisite.Add(addedRequisite);
        }

        private void bDeleteRequisite_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequisite = (sender as Button).DataContext as PhotoSessionRequisite;
            if (selectedRequisite == null)
            {
                MessageBox.Show("Выберите реквизит");
                return;
            }
            currentRequisites.Remove(selectedRequisite);
            MainWindow.DB.PhotoSessionRequisite.Remove(selectedRequisite);
        }

        private void rbInStudio_Checked(object sender, RoutedEventArgs e)
        {
            spStudio.Visibility = Visibility.Visible;
            spAddress.Visibility = Visibility.Collapsed;
            tbAdress.Text = null;
        }

        private void rbOutStudio_Checked(object sender, RoutedEventArgs e)
        {
            spStudio.Visibility = Visibility.Collapsed;
            cbStudios.SelectedItem = null;
            spAddress.Visibility = Visibility.Visible;
        }

        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            var errorMessage = "";
            if (contextPhotoSession.Client == null)
                errorMessage += "Выберите клиента\n";
            if (contextPhotoSession.PhotoSessionDate == null || contextPhotoSession.PhotoSessionDate < DateTime.Now.Date)
                errorMessage += "Выберите корректную дату\n";
            if (contextPhotoSession.PhotoSessionTime == null)
                errorMessage += "Выберите время\n";
            if (contextPhotoSession.PhotoSessionTime != null && contextPhotoSession.PhotoSessionDate.Date == DateTime.Now.Date && contextPhotoSession.PhotoSessionTime < DateTime.Now.TimeOfDay)
                errorMessage += "Выберите корректное время\n";
            if (contextPhotoSession.Photograph == null)
                errorMessage += "Выберите фотографа\n";
            if (contextPhotoSession.Studio == null && string.IsNullOrWhiteSpace(contextPhotoSession.Address))
                errorMessage += "Выберите место фотосессии\n";
            if (contextPhotoSession.Theme == null)
                errorMessage += "Выберите тему фотосессии";
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                MessageBox.Show(errorMessage);
                return;
            }
            contextPhotoSession.StatusId = 4;
            var lastPhotoSession = MainWindow.DB.PhotoSession.OrderByDescending(p => p.Id).FirstOrDefault();
            contextPhotoSession.Catalog = $@"c:\photos\{lastPhotoSession.Id + 1}{contextPhotoSession.Client.Id} {contextPhotoSession.PhotoSessionDate.ToShortDateString()}";
            if (contextPhotoSession.Id == 0)
                MainWindow.DB.PhotoSession.Add(contextPhotoSession);
            MainWindow.DB.SaveChanges();
            NavigationService.GoBack();
        }

        private void bBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void dpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (contextPhotoSession.Id != 0)
            //    return;
            //Refresh();
        }

        private void cbTimes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (contextPhotoSession.Id != 0)
                return;
            var selectedTime = cbTimes.SelectedItem.ToString();
            contextPhotoSession.PhotoSessionTime = TimeSpan.FromHours(int.Parse($"{selectedTime[0]}{selectedTime[1]}"));
            Refresh();
        }

        private void Refresh()
        {
            cbPhotographers.SelectedItem = null;
            cbStudios.SelectedItem = null;
            cbClients.SelectedItem = null;
            if (dpDate.SelectedDate == null || cbTimes.SelectedItem == null)
                return;
            var todayPhotosessions = MainWindow.DB.PhotoSession.Where(p => p.PhotoSessionDate == dpDate.SelectedDate && p.PhotoSessionTime == contextPhotoSession.PhotoSessionTime).ToList();
            var photographs = todayPhotosessions.Select(t => t.Photograph);
            var studios = todayPhotosessions.Select(t => t.Studio);
            var clients = todayPhotosessions.Select(t => t.Client);
            cbPhotographers.ItemsSource = Tools.Tools.MyExcept(MainWindow.DB.Photograph, photographs).ToList();
            cbStudios.ItemsSource = Tools.Tools.MyExcept(MainWindow.DB.Studio, studios).ToList();
            cbClients.ItemsSource = Tools.Tools.MyExcept(MainWindow.DB.Client, clients).ToList();

        }
    }
}
