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
    /// <summary>
    /// Interaction logic for StudioPage.xaml
    /// </summary>
    public partial class StudioPage : Page
    {
        Studio contextStudio;
        public StudioPage(Studio postStudio)
        {
            InitializeComponent();
            contextStudio = postStudio;
            DataContext = contextStudio;
            if (postStudio.Image == null)
                studioImage.Source = Tools.Tools.BytesToImage(File.ReadAllBytes(new Uri("Resources/studio.png", UriKind.Relative).ToString()));
        }

        private void studioImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var dialog = new OpenFileDialog() { Filter = ".png, .jpg, .jpeg|*.png; *.jpg; *.jpeg" };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                studioImage.Source = Tools.Tools.BytesToImage(File.ReadAllBytes(dialog.FileName));
                contextStudio.Image = File.ReadAllBytes(dialog.FileName);
            }
        }

        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = "";
            if (string.IsNullOrWhiteSpace(contextStudio.Name))
                errorMessage += "Введите название\n";
            if (string.IsNullOrWhiteSpace(tbCost.Text))
                errorMessage += "Введите стоимость\n";
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                MessageBox.Show(errorMessage);
                return;
            }
            try
            {
                contextStudio.Cost = decimal.Parse(tbCost.Text);
                if(contextStudio.Cost <= 0)
                {
                    MessageBox.Show("Введите корректную стоимость");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Введите корректную стоимость");
                return;
            }
            if (contextStudio.Id == 0)
                MainWindow.DB.Studio.Add(contextStudio);
            MainWindow.DB.SaveChanges();
            NavigationService.GoBack();
        }

        private void bBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
