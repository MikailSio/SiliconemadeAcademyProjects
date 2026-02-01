using System;
using System.Collections.Generic;
using System.Linq;
using OkulYonetimUygulamasi;

namespace OkulYonetimUygulamasi
{
    internal class Ogrenci
    {
        // Alanları property yaptık (daha düzenli)
        // { get; set; } = okunabilir + değiştirilebilir
        public int No { get; set; }
        public string Ad { get; set; } = "";
        public string Soyad { get; set; } = "";
        public DateTime DogumTarihi { get; set; }
        public CINSIYET Cinsiyet { get; set; }
        public SUBE Subesi { get; set; }

        // Adres: null olabilir (girilmeyebilir)
        public Adres? Adresi { get; set; }

        // Kitaplar: okuma sırası önemli olduğu için liste
        public List<Kitap> Kitaplar { get; set; } = new List<Kitap>();

        // Notlar: ders adı + not + tarih
        public List<DersNotu> Notlar { get; set; } = new List<DersNotu>();

        // Tam ad sürekli lazım olduğu için hesaplanan property
        public string TamAd => $"{Ad} {Soyad}";


        //##################################################################
        //---Genel not ortalaması:-----Not yoksa 0 döner.-------------------
        //##################################################################
        public float NotOrtalamasi
        {
            get
            {
                if (Notlar.Count == 0) return 0f;
                return (float)Notlar.Average(n => n.Not);
            }
        }

        //##################################################################
        //---------Ders bazlı not özetleri: (DersAdi, Ortalama)-------------
        //##################################################################
        public List<DersNotOzeti> DersNotOzetleri()
        {
            // Aynı ders birden çok kez girilirse ortalamasını alır
            return Notlar
                .GroupBy(n => n.DersAdi.Trim().ToLower())
                .Select(g =>
                {
                    string dersAdiGuzel = g.First().DersAdi; // ilk girilen ders adı
                    float ort = (float)g.Average(x => x.Not);
                    return new DersNotOzeti(dersAdiGuzel, ort);
                })
                .OrderBy(x => x.DersAdi)
                .ToList();
        }

        //##################################################################
        //-----------------Son kitap (en son eklenen)-----------------------
        //##################################################################
        public Kitap? SonKitap()
        {
            if (Kitaplar.Count == 0) return null;
            return Kitaplar.Last();
        }
    }


    // record: Sadece veri taşımak için kullanılan hafif sınıf
    // Constructor + property’ler otomatik oluşur
    internal record DersNotOzeti(string DersAdi, float Ortalama);


    //Enumlar
    public enum SUBE
    {
        A, B, C
    }

    public enum CINSIYET
    {
        Kiz, Erkek
    }
}
