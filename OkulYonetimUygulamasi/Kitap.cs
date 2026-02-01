using System;

namespace OkulYonetimUygulamasi
{
    internal class Kitap
    {
        public string Ad { get; set; }
        public DateTime EklenmeTarihi { get; set; }

        public Kitap(string ad)
        {
            Ad = ad;
            EklenmeTarihi = DateTime.Now;
        }
    }
}
