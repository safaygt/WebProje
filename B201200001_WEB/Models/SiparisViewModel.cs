using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B201200001_WEB.Models
{
    public class SiparisViewModel
    {
        public int Kullanici_ID { get; set; }
        public int Oyuncak_ID { get; set; }
        public int Fatura_ID { get; set; }
        public int Magaza_ID { get; set; }
        public int OyuncakAdet { get; set; }

        // Oyuncak özellikleri
        public int Oyuncak_Tur_ID { get; set; }
        public double? Oyuncak_Agirlik { get; set; }
        public float? Oyuncak_Boy { get; set; }
        public int Oyuncak_Renk_ID { get; set; }
        public string Oyuncak_Resim { get; set; }
        public float? Oyuncak_Fiyat { get; set; }

        // Diğer sipariş bilgileri
        // Örneğin: public string TeslimatAdresi { get; set; }
        public DateTime SiparisTarihi { get; set; }

        public float? SiparisTutar {  get; set; }

        public int Siparis_ID { get; set; }
    }
}