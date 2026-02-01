using System;

namespace OkulYonetimUygulamasi
{
    internal class DersNotu
    {
        public string DersAdi { get; set; }
        public int Not { get; set; }
        public DateTime Tarih { get; set; }

        public DersNotu(string dersAdi, int not)
        {
            DersAdi = dersAdi;
            Not = not;
            Tarih = DateTime.Now;
        }
    }
}
