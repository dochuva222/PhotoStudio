using PhotoStudioMob.Model;
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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void bLogin_Clicked(object sender, EventArgs e)
        {
            var loggedPhotograph = await APIManager.GetData<Photograph>($"Photographs?login={eLogin.Text}");
            if (loggedPhotograph == null || loggedPhotograph.Password != ePassword.Text)
            {
                await DisplayAlert("Ошибка", "Неверный логин или пароль", "Ок");
                return;
            }
            MainPage.LoggedPhotograph = loggedPhotograph;
            await Navigation.PushAsync(new MainPage());
        }

        private void showPasswordSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (showPasswordSwitch.IsToggled == true)
                ePassword.IsPassword = false;
            else
                ePassword.IsPassword = true;
        }
    }
}