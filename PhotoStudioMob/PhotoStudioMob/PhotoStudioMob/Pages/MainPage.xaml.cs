using PhotoStudioMob.Model;
using PhotoStudioMob.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Plugin.Calendar.Models;

namespace PhotoStudioMob.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public static Photograph LoggedPhotograph;
        public EventCollection photoSessions;
        public MainPage()
        {
            InitializeComponent();
            Refresh();
        }

        private void refreshView_Refreshing(object sender, EventArgs e)
        {
            Refresh();
        }

        private async void Refresh()
        {
            refreshView.IsRefreshing = true;
            var photographPhotoSessions = await APIManager.GetData<List<PhotoSession>>($"GetPhotographPhotoSessions/{MainPage.LoggedPhotograph.Id}");
            photoSessions = new EventCollection();
            foreach (var photoSession in photographPhotoSessions.GroupBy(g => g.PhotoSessionDate))
            {
                photoSessions.Add(photoSession.Key, photoSession.ToList());
            }
            photoSessionsCalendar.Events = photoSessions;
            refreshView.IsRefreshing = false;
        }

        private async void bDelete_Clicked(object sender, EventArgs e)
        {
            var selectedPhotoSession = (sender as Button).BindingContext as PhotoSession;
            selectedPhotoSession.StatusId = 3;
            await APIManager.PutData($"PutPhotosesion/{selectedPhotoSession.Id}", selectedPhotoSession);
            Refresh();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage());
        }

        private async void bAccept_Clicked(object sender, EventArgs e)
        {
            var selectedPhotoSession = (sender as Button).BindingContext as PhotoSession;
            selectedPhotoSession.StatusId = 1;
            await APIManager.PutData($"PutPhotosesion/{selectedPhotoSession.Id}", selectedPhotoSession);
            Refresh();
        }

        private async void bFinish_Clicked(object sender, EventArgs e)
        {
            var selectedPhotoSession = (sender as Button).BindingContext as PhotoSession;
            selectedPhotoSession.StatusId = 2;
            await APIManager.PutData($"PutPhotosesion/{selectedPhotoSession.Id}", selectedPhotoSession);
            Refresh();
        }

        private void bRefresh_Clicked(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}