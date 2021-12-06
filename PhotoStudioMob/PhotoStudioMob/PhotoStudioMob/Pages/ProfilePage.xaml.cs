using PhotoStudioMob.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhotoStudioMob.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = MainPage.LoggedPhotograph;
        }

        private async void bSave_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ePassword.Text))
            {
                await DisplayAlert("Ошибка", "Введите пароль", "Ок");
                return;
            }
            MainPage.LoggedPhotograph.Password = ePassword.Text;
            await APIManager.PutData($"Photographs/{MainPage.LoggedPhotograph.Id}", MainPage.LoggedPhotograph);
            ePassword.Text = "";
            await DisplayAlert("", "Данные успешно изменены", "Ок");
        }

        private async void toPhotoSessions(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}