using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B201200001_WEB.Models
{
    public class CreateAdressViewModel
    {

        public List<Sehir> Cities { get; set; }
        public List<ilce> Ilceler { get; set; }
        public List<Mahalle> Mahalleler { get; set; }
        public List<Sokak> Sokaklar { get; set; }
        public AdresViewModel Adres { get; set; } = new AdresViewModel();

    }
}