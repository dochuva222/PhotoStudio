using Microsoft.Win32;
using PhotoStudioApp.Model;
using System;
using System.Collections.Generic;
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
    public partial class PhotographPage : Page
    {
        Photograph contextPhotograph;
        public PhotographPage(Photograph postPhotograph)
        {
            InitializeComponent();
            cbSkills.ItemsSource = MainWindow.DB.Skill.ToList();
            contextPhotograph = postPhotograph;
            DataContext = contextPhotograph;
            if (postPhotograph.Image == null)
                photographImage.Source = Tools.Tools.BytesToImage(File.ReadAllBytes(new Uri("Resources/user.png", UriKind.Relative).ToString()));
        }

        private void photographImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var dialog = new OpenFileDialog() { Filter = ".png, .jpg, .jpeg|*.png; *.jpg; *.jpeg" };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                photographImage.Source = Tools.Tools.BytesToImage(File.ReadAllBytes(dialog.FileName));
                contextPhotograph.Image = File.ReadAllBytes(dialog.FileName);
            }
        }

        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = "";
            if (string.IsNullOrWhiteSpace(contextPhotograph.Lastname))
                errorMessage += "Введите Фамилию\n";
            if (string.IsNullOrWhiteSpace(contextPhotograph.Firstname))
                errorMessage += "Введите Имя\n";
            if (string.IsNullOrWhiteSpace(contextPhotograph.Login) || MainWindow.DB.Photograph.FirstOrDefault(p => p.Login == contextPhotograph.Login) != null)
                errorMessage += "Пустой логин или уже занят\n";
            if (string.IsNullOrWhiteSpace(contextPhotograph.Password))
                errorMessage += "Введите пароль\n";
            if (contextPhotograph.Skill == null)
                errorMessage += "Выберите квалификацию\n";
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                MessageBox.Show(errorMessage);
                return;
            }
            if (contextPhotograph.Id == 0)
                MainWindow.DB.Photograph.Add(contextPhotograph);
            MainWindow.DB.SaveChanges();
            NavigationService.GoBack();
        }

        private void bBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();

        }
    }
}
