using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHPersoonsGegevens
{
    class Persoon
    {
        // Properties
        // FIXME: Modify this stuff and use English
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public GeslachtEnum Geslacht { get; set; }
        public DateTime GeboorteDatum { get; set; } = new DateTime(1980, 1, 1);
        public string Land { get; set; }

        public int Leeftijd
        {
            get
            {
                int leeftijd = DateTime.Now.Year - GeboorteDatum.Year;

                if (GeboorteDatum.AddYears(leeftijd) > DateTime.Now)
                    leeftijd--;

                return leeftijd;
            }
        }
    }


    enum GeslachtEnum
    {
        Man, Vrouw, Onbekend
    }
}
