using Microsoft.Win32;
using PhotoStudioApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class ClientPage : Page
    {
        Client contextClient;
        public ClientPage(Client postClient)
        {
            InitializeComponent();
            contextClient = postClient;
            DataContext = postClient;
            if (postClient.Image == null)
                clientImage.Source = Tools.Tools.BytesToImage(File.ReadAllBytes(new Uri("Resources/user.png", UriKind.Relative).ToString()));
        }

        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = "";
            if (string.IsNullOrWhiteSpace(contextClient.Lastname))
                errorMessage += "Введите фамилию\n";
            if(string.IsNullOrWhiteSpace(contextClient.Firstname))
                errorMessage += "Введите имя\n";
            if (string.IsNullOrWhiteSpace(contextClient.Phone) || !Regex.IsMatch(contextClient.Phone, "[0-9]{11}") || MainWindow.DB.Client.FirstOrDefault(c => c.Phone == contextClient.Phone) != null)
                errorMessage += "Номер телефона неккоректный или уже используется";
            if (string.IsNullOrWhiteSpace(contextClient.Email) || IsValidEmail(contextClient.Email) == false || MainWindow.DB.Client.FirstOrDefault(c => c.Email == contextClient.Email) != null)
                errorMessage += "Email неккоректный или уже используется";
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                MessageBox.Show(errorMessage);
                return;
            }
            if (contextClient.Id == 0)
                MainWindow.DB.Client.Add(contextClient);
            MainWindow.DB.SaveChanges();
            NavigationService.GoBack();
        }

        public bool IsValidEmail(string validatingEmail)
        {
            try
            {
                MailAddress mail = new MailAddress(validatingEmail);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void bBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void clientImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var dialog = new OpenFileDialog() { Filter = ".png, .jpg, .jpeg|*.png; *.jpg; *.jpeg"};
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                clientImage.Source = Tools.Tools.BytesToImage(File.ReadAllBytes(dialog.FileName));
                contextClient.Image = File.ReadAllBytes(dialog.FileName);
            }
        }
    }
}
