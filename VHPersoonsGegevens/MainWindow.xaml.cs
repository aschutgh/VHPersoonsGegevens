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

namespace VHPersoonsGegevens
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // get genders from GeslachtEnum
            lboxGender.ItemsSource = Enum.GetValues(typeof(GeslachtEnum));

            // fill combobox countries
            var landen = new[] { "Nederland", "Belgie", "Frankrijk" };
            cboxCountry.ItemsSource = landen;
        }

        private void BtnShowPerson_Click(object sender, RoutedEventArgs e)
        {
            var p = new Persoon();
            p.Voornaam = txtbFirstName.Text;
            p.Achternaam = txtbLastName.Text;
            // Hoe werkt de volgende regel?? 
            p.Geslacht = (GeslachtEnum)Enum.Parse(typeof(GeslachtEnum), lboxGender.SelectedItem.ToString());
            p.Land = cboxCountry.Text;
            p.GeboorteDatum = dpBirthDate.SelectedDate.Value;

            lbl.Content = $"Hallo {p.Voornaam} {p.Achternaam}. " + p.GeboorteDatum.ToString("dd-MM-yyyy");
            List<Persoon> AllePersonen = new List<Persoon>();
            AllePersonen.Add(p);
            dg.ItemsSource = AllePersonen;
        }
    }
}
