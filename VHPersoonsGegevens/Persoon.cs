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
        public DateTime GeboorteDatum { get; set; }
        public string Land { get; set; }
    }


    enum GeslachtEnum
    {
        Man, Vrouw, Onbekend
    }
}
