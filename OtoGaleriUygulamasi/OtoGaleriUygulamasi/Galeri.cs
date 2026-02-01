using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoGaleriUygulamasi
{
    internal class Galeri
    {
        // Galerideki tüm arabaları tek bir listede tutuyoruz.
        // Bu liste hem galeride olanları hem de kiradakileri içerir.
        public List<Araba> Arabalar = new List<Araba>();

        // Toplam araç sayısı = listedeki eleman sayısı
        public int ToplamAracSayisi
        {
            get
            {
                return Arabalar.Count;
            }
        }
        //------------------------------------------------------------------------



        // Kirada olan araçların sayısını hesaplayan property
        public int KiradakiAracSayisi
        {
            get
            {
                int adet = 0;

                // Listede dolaş, durumu "Kirada" olanları say
                foreach (Araba item in Arabalar)
                {
                    if (item.Durum == "Kirada")
                    {
                        adet++;
                    }
                }
                return adet;
            }
        }
        //------------------------------------------------------------------------



        // Galeride bekleyen araçların sayısını hesaplayan property
        public int GaleridekiAracSayisi
        {
            get
            {
                int adet = 0;

                // Listede dolaş, durumu "Galeride" olanları say
                foreach (Araba item in Arabalar)
                {
                    if (item.Durum == "Galeride")
                    {
                        adet++;
                    }
                }

                return adet;
            }
        }
        //------------------------------------------------------------------------



        // Tüm araçların toplam kiralama süresi (saat) hesabı
        public int ToplamAracKiralamaSuresi
        {
            get
            {
                int toplam = 0;

                // Her aracın toplam kiralama süresini topla
                foreach (Araba item in Arabalar)
                {
                    toplam += item.ToplamKiralamaSuresi;
                }
                return toplam;
            }
        }
        //------------------------------------------------------------------------



        // Tüm araçların toplam kaç kez kiralandığı (kiralama adedi)
        public int ToplamAracKiralamaAdeti
        {
            get
            {
                int toplam = 0;

                // Her aracın kiralama sayısını topla
                foreach (Araba item in Arabalar)
                {
                    toplam += item.KiralamaSayisi;
                }

                return toplam;
            }
        }
        //------------------------------------------------------------------------



        // Ciro hesabı: (her kiralama süresi * o aracın kiralama bedeli) toplamı
        public float Ciro
        {
            get
            {
                float toplam = 0;

                // Tüm arabaları dolaş
                foreach (Araba item in Arabalar)
                {
                    // Her arabanın kiralama sürelerini dolaş
                    foreach (int sure in item.KiralamaSureleri)
                    {
                        // Saat * saatlik ücret
                        toplam += sure * item.KiralamaBedeli;
                    }
                }

                return toplam;
            }
        }
        //------------------------------------------------------------------------



        // Tablo başlığını her listede aynı basmak için yardımcı metot
        public void TabloBaslikYazdir()
        {
            Console.WriteLine();

            // PadRight: yazıları kolon gibi hizalı göstermek için kullanılır.
            Console.WriteLine(
                "Plaka".PadRight(12) +
                "Marka".PadRight(12) +
                "K. Bedeli".PadRight(11) +
                "Araba Tipi".PadRight(14) +
                "K. Sayısı".PadRight(11) +
                "Durum"
            );

            // Alt çizgi (tablo çizgisi)
            Console.WriteLine(new string('-', 75));
        }
        //-------------------------------------------------------------------------



        // Tek bir araba satırını kolon hizalı yazdırmak için yardımcı metot
        public void ArabaSatiriYazdir(Araba a)
        {
            Console.WriteLine(
                a.Plaka.PadRight(12) +
                a.Marka.PadRight(12) +
                a.KiralamaBedeli.ToString().PadRight(11) +
                a.AracTipi.PadRight(14) +
                a.KiralamaSayisi.ToString().PadRight(11) +
                a.Durum
            );
        }

        //**************************************************************************************************************
        //**************************************************************************************************************

        public int PlakaDurumKontrol(string plaka)
        {
            /* Bu metot plakanın sistemde olup olmadığını ve aracın durumunu tek sayı ile döndürür:
               0 => plaka yok (arabayı bul show)
               1 => plaka var ve araç GALERİDE
               2 => plaka var ama araç KİRADA */

            // null gelirse hata olmasın diye güvenli şekilde normalize ediyoruz.
            plaka = (plaka ?? "").Trim().ToUpper();

            Araba a = null;

            // Arabalar listesinde plaka eşleşen aracı arıyoruz.
            foreach (var item in Arabalar)
            {
                if (item.Plaka == plaka)
                {
                    a = item;
                    break; // bulunca döngüden çık
                }
            }

            // Araç bulunamadıysa 0
            if (a == null) return 0;

            // Araç bulunduysa durumuna bak
            if (a.Durum == "Kirada") return 2;

            // Bulundu ve galeride
            return 1;
        }

        //**************************************************************************************************************
        //**************************************************************************************************************

        public bool KiralanabilirMi(string plaka, out string mesaj)
        {
            // Kiralama için: araç var olmalı ve galeride olmalı
            int durum = PlakaDurumKontrol(plaka);

            if (durum == 0) { mesaj = "Galeriye ait bu plakada bir araba yok."; return false; }
            if (durum == 2) { mesaj = "Araba şu anda kirada. Farklı araba seçiniz."; return false; }

            mesaj = "";
            return true; // durum == 1
        }

        public bool TeslimAlinabilirMi(string plaka, out string mesaj)
        {
            // Teslim almak için: araç var olmalı ve kirada olmalı
            int durum = PlakaDurumKontrol(plaka);

            if (durum == 0) { mesaj = "Galeriye ait bu plakada bir araba yok."; return false; }
            if (durum == 1) { mesaj = "Araba zaten galeride."; return false; }

            mesaj = "";
            return true; // durum == 2
        }

        public bool SilinebilirMi(string plaka, out string mesaj)
        {
            // Silmek için: araç var olmalı ve galeride olmalı (kirada silinemez)
            int durum = PlakaDurumKontrol(plaka);

            if (durum == 0) { mesaj = "Galeriye ait bu plakada bir araba yok."; return false; }
            if (durum == 2) { mesaj = "Araba kirada olduğu için silinemez."; return false; }

            mesaj = "";
            return true; // durum == 1
        }

        public bool EklenebilirMi(string plaka, out string mesaj)
        {
            // Eklemek için: plaka sistemde hiç olmamalı
            int durum = PlakaDurumKontrol(plaka);

            if (durum != 0) { mesaj = "Bu plakada zaten kayıtlı bir araba var."; return false; }

            mesaj = "";
            return true;
        }

        public bool KiralamaIptalEdilebilirMi(string plaka, out string mesaj)
        {
            /* Kiralama iptali için:
               - araç var olmalı
               - araç kirada olmalı (galerideyse iptal edilecek kiralama yok) */

            plaka = (plaka ?? "").Trim().ToUpper();

            int durum = PlakaDurumKontrol(plaka);

            if (durum == 0)
            {
                mesaj = "Galeriye ait bu plakada bir araba yok.";
                return false;
            }

            if (durum == 1)
            {
                mesaj = "Hatalı giriş yapıldı. Araba zaten galeride.";
                return false;
            }

            mesaj = "";
            return true; // durum == 2
        }

        //**************************************************************************************************************
        //**************************************************************************************************************

        public bool ArabaKirala(string plaka, int sure) // 1, K
        {
            // Kullanıcıdan gelen plakayı standart hale getiriyoruz.
            plaka = (plaka ?? "").Trim().ToUpper();

            // Süre bir iş kuralıdır: 0 veya negatif olamaz
            if (sure <= 0)
            {
                Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                return false;
            }

            // Plaka ile arabayı buluyoruz.
            Araba a = null;
            foreach (Araba item in Arabalar)
            {
                if (item.Plaka == plaka)
                {
                    a = item;
                    break;
                }
            }

            /* Güvenlik kontrolü:
               Program.cs zaten KiralanabilirMi ile kontrol ediyor.
               Yine de burası koruyucu kalsın: araç yoksa veya kiradaysa işlem yapılmasın. */
            if (a == null || a.Durum == "Kirada")
                return false;

            // Aracı kiraya verdik => durum değişir
            a.Durum = "Kirada";

            // Kiralama süresini listeye ekleyerek geçmişi tutuyoruz.
            a.KiralamaSureleri.Add(sure);

            // Bilgi mesajı (kullanıcıya çıktı)
            Console.WriteLine(a.Plaka + " plakalı araba " + sure + " saatliğine kiralandı.");
            return true;
        }
        //------------------------------------------------------------------------



        public void ArabaTeslimAl(string plaka) // 2, T
        {
            // Gelen plakayı normalize ediyoruz.
            plaka = (plaka ?? "").Trim().ToUpper();

            Araba a = null;

            // Araç arama
            foreach (Araba item in Arabalar)
            {
                if (item.Plaka == plaka)
                {
                    a = item;
                    break;
                }
            }

            // Güvenlik: normalde TeslimAlinabilirMi kontrolü geçtiyse buraya düşmez.
            if (a == null || a.Durum == "Galeride")
                return;

            // Araç teslim alındı => tekrar galeride
            a.Durum = "Galeride";

            Console.WriteLine();
            Console.WriteLine("Araba galeride beklemeye alındı.");
        }
        //------------------------------------------------------------------------



        public void KiradakiArabalariListele() // 3, R
        {
            // Kirada hiç araç yoksa tablo basmadan mesaj bas.
            if (KiradakiAracSayisi == 0)
            {
                Console.WriteLine("Kirada hiç araba yok.");
                return;
            }

            // Tablo başlığı
            TabloBaslikYazdir();

            // Sadece "Kirada" olanları yazdır
            foreach (Araba item in Arabalar)
            {
                if (item.Durum == "Kirada")
                {
                    ArabaSatiriYazdir(item);
                }
            }
        }
        //------------------------------------------------------------------------



        public void GaleridekiArabalariListele() // 4, M
        {
            // Galeride hiç araç yoksa tablo basmadan mesaj bas.
            if (GaleridekiAracSayisi == 0)
            {
                Console.WriteLine("Listelenecek araç yok.");
                return;
            }

            // Tablo başlığı
            TabloBaslikYazdir();

            // Sadece "Galeride" olanları yazdır
            foreach (Araba item in Arabalar)
            {
                if (item.Durum == "Galeride")
                {
                    ArabaSatiriYazdir(item);
                }
            }
        }
        //------------------------------------------------------------------------



        public void TumArabalariListele() // 5, A
        {
            // Hepsini listeleyeceğimiz için tablo başlığını koşulsuz basıyoruz.
            TabloBaslikYazdir();

            foreach (Araba item in Arabalar)
            {
                ArabaSatiriYazdir(item);
            }
        }
        //------------------------------------------------------------------------



        public void KiralamaIptali(string plaka) // 6, I
        {
            plaka = (plaka ?? "").Trim().ToUpper();

            Araba a = null;
            foreach (Araba item in Arabalar)
            {
                if (item.Plaka == plaka)
                {
                    a = item;
                    break;
                }
            }

            if (a == null)
            {
                Console.WriteLine("Galeriye ait bu plakada bir araba yok.");
                return;
            }

            if (a.Durum == "Galeride")
            {
                Console.WriteLine("Hatalı giriş yapıldı. Araba zaten galeride.");
                return;
            }

            if (a.KiralamaSureleri.Count == 0)
                return;

            int sonIndex = a.KiralamaSureleri.Count - 1;
            a.KiralamaSureleri.RemoveAt(sonIndex);

            a.Durum = "Galeride";

            Console.WriteLine("İptal gerçekleştirildi.");
        }

        //------------------------------------------------------------------------



        public void ArabaEkle(string plaka, string marka, int kiralamaBedeli, string aTipi) // 7 , Y
        {
            /* Yeni araç ekleme:
               Program.cs plaka formatını ve eklenebilir mi kontrolünü yapıyor.
               Burada direkt Araba nesnesi üretip listeye ekliyoruz. */

            Araba a = new Araba(plaka, marka, kiralamaBedeli, aTipi);
            this.Arabalar.Add(a);

            Console.WriteLine("");
            Console.WriteLine("Araba başarılı bir şekilde eklendi.");
        }

        /* Kurulum aşamasında araç eklerken mesaj basmamak için ayrı metot.
           Başlangıç listesi sessizce oluşsun diye kullanılır. */
        public void ArabaEkleKurulum(string plaka, string marka, int kiralamaBedeli, string aTipi) // 7.1
        {
            Araba a = new Araba(plaka, marka, kiralamaBedeli, aTipi);
            this.Arabalar.Add(a);
        }
        //------------------------------------------------------------------------



        public void ArabaSil(string plaka) // 8, S
        {
            // Plakayı normalize et
            plaka = (plaka ?? "").Trim().ToUpper();

            Araba a = null;

            // Aracı bul
            foreach (Araba item in Arabalar)
            {
                if (item.Plaka == plaka)
                {
                    a = item;
                    break;
                }
            }

            // Güvenlik: normalde SilinebilirMi geçtiyse araç null olmaz ve galeride olur.
            if (a == null || a.Durum == "Kirada")
                return;

            // Listeden çıkar => silme işlemi
            Arabalar.Remove(a);

            Console.WriteLine();
            Console.WriteLine("Araba silindi.");
        }
        //------------------------------------------------------------------------



        public void BilgileriGoster() // 9, G
        {
            // Toplam araç sayısı
            Console.WriteLine("Toplam araba sayısı: " + ToplamAracSayisi);

            // Kiradaki araç sayısı
            Console.WriteLine("Kiradaki araba sayısı: " + KiradakiAracSayisi);

            // Galerideki araç sayısı (property ile de hesaplanıyor ama burada toplam-kirada şeklinde yazılmış)
            Console.WriteLine("Galerideki araba sayısı: " + (ToplamAracSayisi - KiradakiAracSayisi));

            // Toplam kiralama süresi
            Console.WriteLine("Toplam kiralama süresi: " + ToplamAracKiralamaSuresi);

            // Toplam kiralama adedi
            Console.WriteLine("Toplam kiralama adedi: " + ToplamAracKiralamaAdeti);

            // Ciro
            Console.WriteLine("Ciro: " + Ciro);
        }
        //------------------------------------------------------------------------
    }
}
