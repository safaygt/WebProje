using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B201200001_WEB.Models
{
    public class AdresViewModel
    {
        public int Adres_ID { get; set; }
        public int Kullanici_ID { get; set; }
        public string Sehir { get; set; }
        public string Ilce { get; set; }
        public string Mahalle { get; set; }
        public string Sokak { get; set; }
        public int? BinaNo { get; set; }


        public string Adres
        {
            get
            {
                // İlgili özellikleri birleştirerek tam adresi oluştur
                return $"{Sehir}, {Ilce}, {Mahalle}, {Sokak}, {BinaNo}, {Kullanici_ID}";
            }
        }

    }
}