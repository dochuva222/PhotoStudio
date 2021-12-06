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
    /// Interaction logic for RequisitePage.xaml
    /// </summary>
    public partial class RequisitePage : Page
    {
        Requisite contextRequisite;
        public RequisitePage(Requisite postRequisite)
        {
            InitializeComponent();
            contextRequisite = postRequisite;
            DataContext = contextRequisite;
            if (postRequisite.Image == null)
                requisiteImage.Source = Tools.Tools.BytesToImage(File.ReadAllBytes(new Uri("Resources/requisite.png", UriKind.Relative).ToString()));
        }

        private void requisiteImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var dialog = new OpenFileDialog() { Filter = ".png, .jpg, .jpeg|*.png; *.jpg; *.jpeg" };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                requisiteImage.Source = Tools.Tools.BytesToImage(File.ReadAllBytes(dialog.FileName));
                contextRequisite.Image = File.ReadAllBytes(dialog.FileName);
            }
        }

        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = "";
            if (string.IsNullOrWhiteSpace(contextRequisite.Name))
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
                contextRequisite.Cost = decimal.Parse(tbCost.Text);
                if (contextRequisite.Cost <= 0)
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
            if (contextRequisite.Id == 0)
                MainWindow.DB.Requisite.Add(contextRequisite);
            MainWindow.DB.SaveChanges();
            NavigationService.GoBack();
        }

        private void bBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
