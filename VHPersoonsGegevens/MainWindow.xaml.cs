using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.Win32;

// FIXME: All code should be in English!! Clean up this mess...

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

        ObservableCollection<Persoon> AllePersonen = new ObservableCollection<Persoon>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // get genders from GeslachtEnum
            lboxGender.ItemsSource = Enum.GetValues(typeof(GeslachtEnum));

            // fill combobox countries
            var landen = new[] { "Nederland", "Belgie", "Frankrijk" };
            cboxCountry.ItemsSource = landen;

            // DatePicker initialiseren
            dpBirthDate.SelectedDate = new DateTime(1980, 1, 1);
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

            //lbl.Content = $"Hallo {p.Voornaam} {p.Achternaam}. " + p.GeboorteDatum.ToString("dd-MM-yyyy");
            
            AllePersonen.Add(p); // Observable Collection 
            //Represents a dynamic data collection that provides notifications when items get
            //added, removed, or when the whole list is refreshed.
            dg.ItemsSource = AllePersonen;
        }

        private void Opslaan_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialoog = new SaveFileDialog();
            dialoog.ShowDialog();

            string csvData = "";

            foreach(Persoon p in AllePersonen)
            {
                // csvData += p.Voornaam + ", " + p.Achternaam + Environment.NewLine;
                csvData += string.Join(",", p.Voornaam, p.Achternaam, p.Land, p.GeboorteDatum, p.Geslacht) + Environment.NewLine;
            }

            //File.WriteAllText(@"c:\tmp\personen.csv", csvData);
            File.WriteAllText(dialoog.FileName, csvData);
        }

        private void Openen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialoog = new OpenFileDialog();
            dialoog.ShowDialog();

            string[] csvData;
            csvData = File.ReadAllLines(dialoog.FileName);

            foreach (string s in csvData)
            {
                Persoon p = new Persoon();
                string[] personfields = s.Split(',');
                p.Voornaam = personfields[0];
                p.Achternaam = personfields[1];
                p.Land = personfields[2];
                p.GeboorteDatum = DateTime.Parse(personfields[3]);
                p.Geslacht = (GeslachtEnum)Enum.Parse(typeof(GeslachtEnum), personfields[4]);
                AllePersonen.Add(p);
            }
            dg.ItemsSource = AllePersonen;
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("This application is about to close. You could lose data. Close Application?", "Close Application", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnShowMales_Click(object sender, RoutedEventArgs e)
        {
            List<Persoon> AlleMannen = new List<Persoon>();

            var MaleQuery = from Person in AllePersonen where Person.Geslacht == GeslachtEnum.Man select Person;

            AlleMannen = MaleQuery.ToList();

            dg.ItemsSource = AlleMannen;
        }

        private void btnShowFemales_Click(object sender, RoutedEventArgs e)
        {
            List<Persoon> AlleVrouwen = new List<Persoon>();

            var FemaleQuery = from Person in AllePersonen where Person.Geslacht == GeslachtEnum.Vrouw select Person;

            AlleVrouwen = FemaleQuery.ToList();

            dg.ItemsSource = AlleVrouwen;
        }

        private void btnSortAge_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
