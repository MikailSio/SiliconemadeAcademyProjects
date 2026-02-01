using System.Globalization;

namespace OkulYonetimUygulamasi
{
    internal class Uygulama
    {
        private Okul okul = new Okul();

        public void Baslat()
        {
            CultureInfo.CurrentCulture = new CultureInfo("tr-TR");

            SahteVeriGir();
            Calistir();
        }

        private void Calistir()
        {
            MenuYaz();

            while (true)
            {

                Console.Write("\nYapmak istediginiz islemi seçiniz: ");
                string secim = (Console.ReadLine() ?? "").Trim().ToLower();

                if (secim == "çıkış" || secim == "cikis")
                    break;

                if (secim == "liste")
                {
                    Console.Clear();
                    continue;
                }

                //Console.Clear();

                switch (secim)
                {
                    case "1": TumOgrencileriListele(); break;
                    case "2": SubeyeGoreListele(); break;
                    case "3": CinsiyeteGoreListele(); break;
                    case "4": DogumTarihineGoreListele(); break;
                    case "5": IllereGoreListele(); break;
                    case "6": OgrencininNotlariniListele(); break;
                    case "7": OgrencininKitaplariniListele(); break;
                    case "8": OkulEnYuksek5(); break;
                    case "9": OkulEnDusuk3(); break;
                    case "10": SubeEnYuksek5(); break;
                    case "11": SubeEnDusuk3(); break;
                    case "12": OgrenciOrtalamaGor(); break;
                    case "13": SubeOrtalamaGor(); break;
                    case "14": OgrenciSonKitapGor(); break;
                    case "15": OgrenciEkle(); break;
                    case "16": OgrenciGuncelle(); break;
                    case "17": OgrenciSil(); break;
                    case "18": OgrenciAdresGir(); break;
                    case "19": OgrenciKitapGir(); break;
                    case "20": NotGir(); break;
                    default:
                        Console.WriteLine("Hatalı işlem gerçekleştirildi. Tekrar deneyin.");
                        break;
                }

            }
        }

        private void MenuYaz()
        {
            Console.WriteLine("------  Okul Yönetim Uygulamasi  -----\n");
            Console.WriteLine("1 - Bütün öğrencileri listele");
            Console.WriteLine("2 - Şubeye göre öğrencileri listele");
            Console.WriteLine("3 - Cinsiyetine göre öğrencileri listele");
            Console.WriteLine("4 - Şu tarihten sonra doğan öğrencileri listele");
            Console.WriteLine("5 - İllere göre sıralayarak öğrencileri listele");
            Console.WriteLine("6 - Öğrencinin tüm notlarını listele");
            Console.WriteLine("7 - Öğrencinin okuduğu kitapları listele");
            Console.WriteLine("8 - Okuldaki en yüksek notlu 5 öğrenciyi listele");
            Console.WriteLine("9 - Okuldaki en düşük notlu 3 öğrenciyi listele");
            Console.WriteLine("10 - Şubedeki en yüksek notlu 5 öğrenciyi listele");
            Console.WriteLine("11 - Şubedeki en düşük notlu 3 öğrenciyi listele");
            Console.WriteLine("12 - Öğrencinin not ortalamasını gör");
            Console.WriteLine("13 - Şubenin not ortalamasını gör");
            Console.WriteLine("14 - Öğrencinin okuduğu son kitabı gör");
            Console.WriteLine("15 - Öğrenci ekle");
            Console.WriteLine("16 - Öğrenci güncelle");
            Console.WriteLine("17 - Öğrenci sil");
            Console.WriteLine("18 - Öğrencinin adresini gir");
            Console.WriteLine("19 - Öğrencinin okuduğu kitabı gir");
            Console.WriteLine("20 - Öğrencinin notunu gir\n");
            Console.WriteLine("çıkış yapmak için \"çıkış\" yazıp \"enter\"a basın.");
        }

        // ------------------ Menü işlemleri ------------------

        private void TumOgrencileriListele()
        {
            Console.WriteLine("1-Bütün Ögrencileri Listele --------------------------------------------------\n");
            YazOgrenciTabloBaslik();

            foreach (var o in okul.OgrencilerSirali())
                YazOgrenciTabloSatir(o);
        }

        private void SubeyeGoreListele()
        {
            Console.WriteLine("2-Subeye Göre Ögrencileri Listele --------------------------------------------");
            SUBE sube = ReadSube("Listelemek istediğiniz şubeyi girin (A/B/C): ");

            Console.WriteLine();
            YazOgrenciTabloBaslik();

            foreach (var o in okul.OgrencilerSube(sube))
                YazOgrenciTabloSatir(o);
        }

        private void CinsiyeteGoreListele()
        {
            Console.WriteLine("3-Cinsiyete Göre Öğrencileri Listele -----------------------------------------");
            CINSIYET c = ReadCinsiyet("Listelemek istediğiniz cinsiyeti girin (K/E): ");

            Console.WriteLine();
            YazOgrenciTabloBaslik();

            foreach (var o in okul.OgrencilerCinsiyet(c))
                YazOgrenciTabloSatir(o);
        }

        private void DogumTarihineGoreListele()
        {
            Console.WriteLine("4-Dogum Tarihine Göre Ögrencileri Listele ------------------------------------");
            DateTime dt = ReadDate("Hangi tarihten sonraki ögrencileri listelemek istersiniz (gg.aa.yyyy): ");

            Console.WriteLine();
            YazOgrenciTabloBaslik();

            foreach (var o in okul.OgrencilerDogumSonra(dt))
                YazOgrenciTabloSatir(o);
        }

        private void IllereGoreListele()
        {
            Console.WriteLine("5-Illere Göre Ögrencileri Listele --------------------------------------------\n");
            Console.WriteLine("Sube      No        Adı Soyadı           Sehir          Semt");
            Console.WriteLine("-------------------------------------------------------------------------------");

            foreach (var o in okul.OgrencilerIlSirali())
            {
                string sehir = o.Adresi?.Il ?? "-";
                string semt = o.Adresi?.Mahalle ?? "-";
                Console.WriteLine($"{o.Subesi,-9}{o.No,-10}{o.TamAd,-23}{sehir,-15}{semt,-10}");
            }
        }

        private void OgrencininNotlariniListele()
        {
            Console.WriteLine("6-Ögrencinin notlarını görüntüle ---------------------------------------------");
            int no = ReadInt("Ögrencinin numarasi: ");

            var o = okul.OgrenciBul(no);
            if (o == null) { Console.WriteLine("Öğrenci bulunamadı."); return; }

            Console.WriteLine($"\nÖgrencinin Adı Soyadı: {o.TamAd}");
            Console.WriteLine($"Ögrencinin Subesi: {o.Subesi}\n");

            Console.WriteLine("Dersin Adi     Notu");
            Console.WriteLine("--------------------");

            foreach (var item in o.DersNotOzetleri())
                Console.WriteLine($"{item.DersAdi,-13}{item.Ortalama:0.##}");
        }

        private void OgrencininKitaplariniListele()
        {
            Console.WriteLine("7-Ögrencinin okudugu kitapları listele ---------------------------------------");
            int no = ReadInt("Ögrencinin numarasi: ");

            var o = okul.OgrenciBul(no);
            if (o == null) { Console.WriteLine("Öğrenci bulunamadı."); return; }

            Console.WriteLine($"\nÖgrencinin Adı Soyadı: {o.TamAd}");
            Console.WriteLine($"Ögrencinin Subesi: {o.Subesi}\n");

            Console.WriteLine("Okudugu Kitaplar");
            Console.WriteLine("-----------------");

            if (o.Kitaplar.Count == 0) { Console.WriteLine("(Kitap yok)"); return; }

            foreach (var k in o.Kitaplar)
                Console.WriteLine(k.Ad);
        }

        private void OkulEnYuksek5()
        {
            Console.WriteLine("8-Okuldaki en basarılı 5 ögrenciyi listele -----------------------------------\n");
            YazOgrenciTabloBaslik();

            foreach (var o in okul.EnBasarili(5))
                YazOgrenciTabloSatir(o);
        }

        private void OkulEnDusuk3()
        {
            Console.WriteLine("9-Okuldaki en basarısız 3 ögrenciyi listele ----------------------------------\n");
            YazOgrenciTabloBaslik();

            foreach (var o in okul.EnBasarisiz(3))
                YazOgrenciTabloSatir(o);
        }

        private void SubeEnYuksek5()
        {
            Console.WriteLine("10-Subedeki en basarılı 5 ögrenciyi listele -----------------------------------");
            SUBE sube = ReadSube("Listelemek istediğiniz şubeyi girin (A/B/C): ");

            Console.WriteLine();
            YazOgrenciTabloBaslik();

            foreach (var o in okul.SubeEnBasarili(sube, 5))
                YazOgrenciTabloSatir(o);
        }

        private void SubeEnDusuk3()
        {
            Console.WriteLine("11-Subedeki en basarısız 3 ögrenciyi listele ----------------------------------");
            SUBE sube = ReadSube("Listelemek istediğiniz şubeyi girin (A/B/C): ");

            Console.WriteLine();
            YazOgrenciTabloBaslik();

            foreach (var o in okul.SubeEnBasarisiz(sube, 3))
                YazOgrenciTabloSatir(o);
        }

        private void OgrenciOrtalamaGor()
        {
            Console.WriteLine("12-Ögrencinin Not Ortalamasını Gör ----------------------------------");
            int no = ReadInt("Ögrencinin numarasi: ");

            var o = okul.OgrenciBul(no);
            if (o == null) { Console.WriteLine("Öğrenci bulunamadı."); return; }

            Console.WriteLine($"\nÖgrencinin Adı Soyadı: {o.TamAd}");
            Console.WriteLine($"Ögrencinin Subesi: {o.Subesi}\n");
            Console.WriteLine($"Ögrencinin not ortalaması: {o.NotOrtalamasi:0.##}");
        }

        private void SubeOrtalamaGor()
        {
            Console.WriteLine("13-Şubenin Not Ortalamasını Gör -------------------------------------");
            SUBE sube = ReadSube("Bir şube seçin (A/B/C): ");

            float ort = okul.SubeOrtalamasi(sube);
            Console.WriteLine($"\n{sube} subesinin not ortalaması: {ort:0.##}");
        }

        private void OgrenciSonKitapGor()
        {
            Console.WriteLine("14-Ögrencinin okudugu son kitabı listele ----------------------------");
            int no = ReadInt("Ögrencinin numarasi: ");

            var o = okul.OgrenciBul(no);
            if (o == null) { Console.WriteLine("Öğrenci bulunamadı."); return; }

            Console.WriteLine($"\nÖgrencinin Adı Soyadı: {o.TamAd}");
            Console.WriteLine($"Ögrencinin Subesi: {o.Subesi}\n");

            var son = o.SonKitap();

            Console.WriteLine("Ögrencinin Okudugu Son Kitap");
            Console.WriteLine("-----------------------------");
            Console.WriteLine(son == null ? "(Kitap yok)" : $" {son.Ad}");
        }

        private void OgrenciEkle()
        {
            Console.WriteLine("15-Öğrenci Ekle -----------------------------------------------------");

            int no = ReadInt("Ögrencinin numarası: ");
            string ad = ReadText("Ögrencinin adı: ");
            string soyad = ReadText("Ögrencinin soyadı: ");
            DateTime dt = ReadDate("Ögrencinin dogum tarihi (gg.aa.yyyy): ");
            CINSIYET c = ReadCinsiyet("Ögrencinin cinsiyeti (K/E): ");
            SUBE sube = ReadSube("Ögrencinin subesi (A/B/C): ");

            int eklenenNo = okul.OgrenciEkle(no, ad, soyad, dt, c, sube);

            Console.WriteLine($"\n{eklenenNo} numaralı ögrenci sisteme basarılı bir sekilde eklenmistir.");
            if (eklenenNo != no)
                Console.WriteLine($"Sistemde {no} numaralı öğrenci olduğu için verdiğiniz öğrenci no {eklenenNo} olarak değiştirildi.");
        }

        private void OgrenciGuncelle()
        {
            Console.WriteLine("16-Ögrenci Güncelle -----------------------------------------------------------");
            int no = ReadInt("Ögrencinin numarasi: ");

            var o = okul.OgrenciBul(no);
            if (o == null) { Console.WriteLine("Öğrenci bulunamadı."); return; }

            string ad = ReadText("Ögrencinin adı: ");
            string soyad = ReadText("Ögrencinin soyadı: ");
            DateTime dt = ReadDate("Ögrencinin dogum tarihi (gg.aa.yyyy): ");
            CINSIYET c = ReadCinsiyet("Ögrencinin cinsiyeti (K/E): ");
            SUBE sube = ReadSube("Ögrencinin subesi (A/B/C): ");

            okul.OgrenciGuncelle(no, ad, soyad, dt, c, sube);
            Console.WriteLine("\nOgrenci güncellendi.");
        }

        private void OgrenciSil()
        {
            Console.WriteLine("17-Ögrenci sil ----------------------------------------------------------------");
            int no = ReadInt("Ögrencinin numarasi: ");

            var o = okul.OgrenciBul(no);
            if (o == null) { Console.WriteLine("Öğrenci bulunamadı."); return; }

            Console.WriteLine($"\nÖgrencinin Adı Soyadı: {o.TamAd}");
            Console.WriteLine($"Ögrencinin Subesi: {o.Subesi}\n");

            Console.Write("Ögrenciyi silmek istediginize emin misiniz (E/H): ");
            string onay = (Console.ReadLine() ?? "").Trim().ToLower();

            if (onay == "e")
            {
                okul.OgrenciSil(no);
                Console.WriteLine("Ögrenci basarılı bir sekilde silindi.");
            }
            else
            {
                Console.WriteLine("Silme iptal edildi.");
            }
        }

        private void OgrenciAdresGir()
        {
            Console.WriteLine("18-Ögrencinin Adresini Gir ------------------------------------------");
            int no = ReadInt("Ögrencinin numarasi: ");

            var o = okul.OgrenciBul(no);
            if (o == null) { Console.WriteLine("Öğrenci bulunamadı."); return; }

            Console.WriteLine($"\nÖgrencinin Adı Soyadı: {o.TamAd}");
            Console.WriteLine($"Ögrencinin Subesi: {o.Subesi}\n");

            string il = ReadText("Il: ");
            string ilce = ReadText("Ilce: ");
            string mahalle = ReadText("Mahalle: ");

            okul.AdresEkle(no, il, ilce, mahalle);
            Console.WriteLine("\nBilgiler sisteme girilmistir.");
        }

        private void OgrenciKitapGir()
        {
            Console.WriteLine("19-Ögrencinin okudugu kitabı gir ------------------------------------");
            int no = ReadInt("Ögrencinin numarasi: ");

            var o = okul.OgrenciBul(no);
            if (o == null) { Console.WriteLine("Öğrenci bulunamadı."); return; }

            Console.WriteLine($"\nÖgrencinin Adı Soyadı: {o.TamAd}");
            Console.WriteLine($"Ögrencinin Subesi: {o.Subesi}\n");

            string kitapAdi = ReadText("Eklenecek Kitabin Adı: ");
            okul.KitapEkle(no, kitapAdi);

            Console.WriteLine("Bilgiler sisteme girilmistir.");
        }

        private void NotGir()
        {
            Console.WriteLine("20-Not Gir ----------------------------------------------------------");
            int no = ReadInt("Ögrencinin numarasi: ");

            var o = okul.OgrenciBul(no);
            if (o == null) { Console.WriteLine("Öğrenci bulunamadı."); return; }

            Console.WriteLine($"\nÖgrencinin Adı Soyadı: {o.TamAd}");
            Console.WriteLine($"Ögrencinin Subesi: {o.Subesi}\n");

            string dersAdi = ReadText("Not eklemek istediğiniz dersi giriniz: ");
            int adet = ReadInt("Eklemek istediginiz not adedi: ", 1, 50);

            for (int i = 0; i < adet; i++)
            {
                int not = ReadIntAllowExit($"{i + 1}. Notu girin: ", 0, 100, out bool cikis);
                if (cikis)
                {
                    Console.WriteLine("Not girişi iptal edildi.");
                    return;
                }
                okul.NotEkle(no, dersAdi, not);
            }

            Console.WriteLine("\nNotlar eklendi.");
        }

        // ------------------ Tablo yazdırmalar ------------------

        private void YazOgrenciTabloBaslik()
        {
            Console.WriteLine("Sube      No        Adı Soyadı               Not Ort.       Okuduğu Kitap Say.");
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        private void YazOgrenciTabloSatir(Ogrenci o)
        {
            Console.WriteLine($"{o.Subesi,-9}{o.No,-10}{o.TamAd,-25}{o.NotOrtalamasi,-13:0.##}{o.Kitaplar.Count,5}");
        }

        // ------------------ Güvenli input ------------------

        private string ReadText(string mesaj)
        {
            while (true)
            {
                Console.Write(mesaj);
                string s = (Console.ReadLine() ?? "").Trim();
                if (!string.IsNullOrWhiteSpace(s)) return s;
                Console.WriteLine("Hatali giris yapildi. Tekrar deneyin");
            }
        }

        private DateTime ReadDate(string mesaj)
        {
            while (true)
            {
                Console.Write(mesaj);
                string s = (Console.ReadLine() ?? "").Trim();

                if (DateTime.TryParseExact(
                    s, "dd.MM.yyyy", new CultureInfo("tr-TR"),
                    DateTimeStyles.None, out DateTime dt))
                    return dt;

                Console.WriteLine("Hatali giris yapildi. Tekrar deneyin");
            }
        }

        private int ReadInt(string mesaj, int? min = null, int? max = null)
        {
            while (true)
            {
                Console.Write(mesaj);
                string s = (Console.ReadLine() ?? "").Trim();

                if (int.TryParse(s, out int val))
                {
                    if (min != null && val < min) { Console.WriteLine("Hatali giris yapildi. Tekrar deneyin"); continue; }
                    if (max != null && val > max) { Console.WriteLine("Hatali giris yapildi. Tekrar deneyin"); continue; }
                    return val;
                }

                Console.WriteLine("Hatali giris yapildi. Tekrar deneyin");
            }
        }

        private int ReadIntAllowExit(string mesaj, int min, int max, out bool cikis)
        {
            while (true)
            {
                Console.Write(mesaj);
                string s = (Console.ReadLine() ?? "").Trim().ToLower();

                if (s == "çıkış" || s == "cikis")
                {
                    cikis = true;
                    return 0;
                }

                if (int.TryParse(s, out int val) && val >= min && val <= max)
                {
                    cikis = false;
                    return val;
                }

                Console.WriteLine("Hatali giris yapildi. Tekrar deneyin");
            }
        }

        private SUBE ReadSube(string mesaj)
        {
            while (true)
            {
                Console.Write(mesaj);
                string s = (Console.ReadLine() ?? "").Trim().ToUpper();

                if (s == "A") return SUBE.A;
                if (s == "B") return SUBE.B;
                if (s == "C") return SUBE.C;

                Console.WriteLine("Hatali giris yapildi. Tekrar deneyin");
            }
        }

        private CINSIYET ReadCinsiyet(string mesaj)
        {
            while (true)
            {
                Console.Write(mesaj);
                string s = (Console.ReadLine() ?? "").Trim().ToUpper();

                if (s == "K") return CINSIYET.Kiz;
                if (s == "E") return CINSIYET.Erkek;

                Console.WriteLine("Hatali giris yapildi. Tekrar deneyin");
            }
        }

        // ------------------ Sahte veri ------------------
        private void SahteVeriGir()
        {
            // Öğrenciler
            okul.OgrenciEkle(1, "Elif", "Selçuk", new DateTime(2001, 11, 10), CINSIYET.Kiz, SUBE.A);
            okul.OgrenciEkle(2, "Betül", "Yılmaz", new DateTime(2000, 6, 2), CINSIYET.Kiz, SUBE.B);
            okul.OgrenciEkle(3, "Hakan", "Çelik", new DateTime(2002, 1, 1), CINSIYET.Erkek, SUBE.C);
            okul.OgrenciEkle(4, "Kerem", "Akay", new DateTime(2001, 5, 12), CINSIYET.Erkek, SUBE.A);
            okul.OgrenciEkle(5, "Hatice", "Çınar", new DateTime(2000, 12, 20), CINSIYET.Kiz, SUBE.B);
            okul.OgrenciEkle(6, "Selim", "İleri", new DateTime(1999, 9, 9), CINSIYET.Erkek, SUBE.B);
            okul.OgrenciEkle(7, "Selin", "Kamış", new DateTime(2003, 3, 3), CINSIYET.Kiz, SUBE.C);
            okul.OgrenciEkle(8, "Sinan", "Avcı", new DateTime(2001, 10, 10), CINSIYET.Erkek, SUBE.A);
            okul.OgrenciEkle(9, "Deniz", "Çoban", new DateTime(1998, 7, 7), CINSIYET.Erkek, SUBE.C);
            okul.OgrenciEkle(10, "Selda", "Kavak", new DateTime(2000, 8, 8), CINSIYET.Kiz, SUBE.B);

            // Adresler (il, ilçe, mahalle/semt)
            okul.AdresEkle(1, "Ankara", "Çankaya", "Çankaya");
            okul.AdresEkle(2, "Ankara", "Keçiören", "Keçiören");
            okul.AdresEkle(3, "Ankara", "Çankaya", "Çankaya");
            okul.AdresEkle(8, "İstanbul", "Arnavutköy", "Arnavutköy");
            okul.AdresEkle(9, "İstanbul", "Beykoz", "Beykoz");
            okul.AdresEkle(10, "İstanbul", "Ataşehir", "Ataşehir");
            okul.AdresEkle(4, "İzmir", "Karşıyaka", "Karşıyaka");
            okul.AdresEkle(5, "İzmir", "Gaziemir", "Gaziemir");
            okul.AdresEkle(6, "İzmir", "Gaziemir", "Gaziemir");
            okul.AdresEkle(7, "İzmir", "Bayraklı", "Bayraklı");

            // Kitaplar
            okul.KitapEkle(1, "Küçük Prens");
            okul.KitapEkle(2, "Simyacı");
            okul.KitapEkle(3, "Sefiller");
            okul.KitapEkle(4, "Tutunamayanlar");
            okul.KitapEkle(5, "Bir Ses Böler Geceyi");
            okul.KitapEkle(6, "Beyaz Diş");
            okul.KitapEkle(7, "Gurur ve Ön Yargı");
            okul.KitapEkle(8, "Martı");
            okul.KitapEkle(9, "Kürk Mantolu Madonna");
            okul.KitapEkle(10, "Kuyucaklı Yusuf");

            // Notlar (örnek çıktıdaki gibi 4 ders tek not)
            // 10: Türkçe 59, Matematik 34, Fen 67, Sosyal 41 (ort 50,25)
            okul.NotEkle(10, "Türkçe", 59);
            okul.NotEkle(10, "Matematik", 34);
            okul.NotEkle(10, "Fen", 67);
            okul.NotEkle(10, "Sosyal", 41);

            okul.NotEkle(7, "Türkçe", 20);
            okul.NotEkle(7, "Matematik", 30);
            okul.NotEkle(7, "Fen", 25);
            okul.NotEkle(7, "Sosyal", 20);

            okul.NotEkle(4, "Türkçe", 60);
            okul.NotEkle(4, "Matematik", 80);
            okul.NotEkle(4, "Fen", 55);
            okul.NotEkle(4, "Sosyal", 55);

            okul.NotEkle(1, "Türkçe", 40);
            okul.NotEkle(1, "Matematik", 42);
            okul.NotEkle(1, "Fen", 41);
            okul.NotEkle(1, "Sosyal", 41);

            okul.NotEkle(5, "Türkçe", 50);
            okul.NotEkle(5, "Matematik", 55);
            okul.NotEkle(5, "Fen", 60);
            okul.NotEkle(5, "Sosyal", 63);

            okul.NotEkle(6, "Türkçe", 55);
            okul.NotEkle(6, "Matematik", 50);
            okul.NotEkle(6, "Fen", 55);
            okul.NotEkle(6, "Sosyal", 55);

            okul.NotEkle(2, "Türkçe", 48);
            okul.NotEkle(2, "Matematik", 49);
            okul.NotEkle(2, "Fen", 47);
            okul.NotEkle(2, "Sosyal", 50);

            okul.NotEkle(3, "Türkçe", 50);
            okul.NotEkle(3, "Matematik", 52);
            okul.NotEkle(3, "Fen", 55);
            okul.NotEkle(3, "Sosyal", 49);

            okul.NotEkle(8, "Türkçe", 25);
            okul.NotEkle(8, "Matematik", 30);
            okul.NotEkle(8, "Fen", 27);
            okul.NotEkle(8, "Sosyal", 30);

            okul.NotEkle(9, "Türkçe", 48);
            okul.NotEkle(9, "Matematik", 49);
            okul.NotEkle(9, "Fen", 50);
            okul.NotEkle(9, "Sosyal", 44);
        }
    }
}
