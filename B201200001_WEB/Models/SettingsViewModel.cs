using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B201200001_WEB.Models
{
    public class SettingsViewModel
    {

        public Kullanici User { get; set; }
        public AdresViewModel Adres { get; set; }
        
        public List<Sehir> Cities { get; set; }
        public List<ilce> Ilceler { get; set; }
        public List<Mahalle> Mahalleler { get; set; }
        public List<Sokak> Sokaklar { get; set; }
    }
}