using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoGaleriUygulamasi
{
    internal class Uygulama
    {
        // Program boyunca tek bir galeri nesnesi kullanacağız.
        // static => Program çalıştığı sürece aynı Galeri nesnesi tutulur.
        static Galeri OtoGaleri = new Galeri();
        //-----------------------------------------------------------------------
        public void Baslat()
        {
            Kurulum();
            Calistir();
        }

        static void Kurulum()
        {
            /* Ödev örneğindeki gibi 3 araçlık başlangıç listesi oluşturuyoruz.
               Normal ArabaEkle metodu "eklendi" mesajı basıyordu.
               Başlangıçta mesaj basılmasın diye ArabaEkleKurulum metodunu kullanıyoruz. */
            OtoGaleri.ArabaEkleKurulum("34ARB3434", "FIAT", 70, "Sedan");
            OtoGaleri.ArabaEkleKurulum("35ARB3535", "KIA", 60, "SUV");
            OtoGaleri.ArabaEkleKurulum("34US2342", "OPEL", 50, "Hatchback");
        }
        //-----------------------------------------------------------------------



        static void Menu()
        {
            // Kullanıcıya menü seçeneklerini yazdırıyoruz.
            Console.WriteLine("Galeri Otomasyon");
            Console.WriteLine("1- Araba Kirala (K)");
            Console.WriteLine("2- Araba Teslim Al (T)");
            Console.WriteLine("3- Kiradaki Arabaları Listele (R)");
            Console.WriteLine("4- Galerideki Arabaları Listele (M)");
            Console.WriteLine("5- Tüm Arabaları Listele (A)");
            Console.WriteLine("6- Kiralama İptali (I)");
            Console.WriteLine("7- Araba Ekle (Y)");
            Console.WriteLine("8- Araba Sil (S)");
            Console.WriteLine("9- Bilgileri Göster (G)");
        }
        //-----------------------------------------------------------------------



        static void Calistir()
        {
            /* Menü başlangıçta 1 kere görünsün istiyoruz.
               O yüzden while döngüsünün dışına yazdık. */
            Menu();

            /* Sürekli çalışan ana döngü:
               Kullanıcı seçim yapar, switch-case ile ilgili metoda gider.
               Kullanıcı X girerse continue ile başa döner (menü akışı bozulmaz). */
            while (true)
            {
                Console.WriteLine("");

                // Kullanıcıdan geçerli bir menü seçimi alıyoruz.
                string secim = SecimAl();

                // Seçime göre ilgili işlemi çağırıyoruz.
                switch (secim)
                {
                    case "1": case "K": ArabaKirala(); break;                // Kiralama
                    case "2": case "T": ArabaTeslimAl(); break;              // Teslim alma
                    case "3": case "R": KiradakiArabalariListele(); break;   // Kiradakiler
                    case "4": case "M": GaleridekiArabalariListele(); break; // Galeridekiler
                    case "5": case "A": TumArabalariListele(); break;        // Tüm araçlar
                    case "6": case "İ": KiralamaIptali(); break;             // Kiralama iptali
                    case "7": case "Y": ArabaEkle(); break;                  // Araç ekleme
                    case "8": case "S": ArabaSil(); break;                   // Araç silme
                    case "9": case "G": BilgileriGoster(); break;            // İstatistikler
                    case "X": continue; ; // Hiçbir şey yapma, döngünün başına dön
                }
            }
        }
        //-----------------------------------------------------------------------



        static string SecimAl()
        {
            // Kullanıcının girebileceği geçerli karakterleri tek bir stringte tutuyoruz.
            // Hem 1-9 rakamları hem de harf kısayolları var.
            string karakterler = "123456789KTRMAIYSGX";

            // Hatalı giriş sayacı (10 olunca program kapanacak)
            int sayac = 0;

            // Doğru seçim gelene kadar sonsuz döngü
            while (true)
            {
                // Her denemede sayaç artar
                sayac++;

                Console.Write("Seçiminiz: ");
                string giris = Console.ReadLine();

                // null gelirse hata olurdu, ama burada ToUpper çağırıyoruz.
                // (Genelde ReadLine null olmaz, ama güvenlik için istersen kontrol eklenebilirdi.)
                giris = giris.ToUpper();

                // IndexOf: giris karakterler içinde varsa 0 veya pozitif bir index döner.
                // Yoksa -1 döner.
                int index = karakterler.IndexOf(giris);

                if (index >= 0)
                {
                    // Geçerli bir seçim: ana döngüye geri gönderiyoruz.
                    return giris;
                }
                else
                {
                    // Hatalı seçim
                    if (sayac == 10)
                    {
                        // 10 kez hatalı girişte programı sonlandırıyoruz.
                        Console.WriteLine("Üzgünüm sizi anlamıyorum. Program sonlandırılıyor");
                        Environment.Exit(0);
                    }

                    // 10 olmadan önce kullanıcıyı uyarıyoruz.
                    Console.WriteLine("Hatalı işlem gerçekleştirildi. Tekrar deneyin.");
                }

                // Görsel boşluk
                Console.WriteLine();
            }
        }
        //-----------------------------------------------------------------------



        static bool PlakaAl(string mesaj, out string plaka, bool xIleCikis = true)
        {
            /* Bu metodun amacı:
               - Kullanıcıdan plaka istemek
               - X girerse menüye dönmek
               - Plaka formatını PlakaGecerliMi ile doğrulamak
               - Doğruysa true, vazgeçildiyse false döndürmek */

            while (true)
            {
                Console.Write(mesaj);
                plaka = Console.ReadLine();

                // Kullanıcı hiçbir şey girmediyse tekrar sor
                if (plaka == null)
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                    continue;
                }

                // Boşlukları silip, harfleri büyütüyoruz.
                plaka = plaka.Trim().ToUpper();

                // Kullanıcı X girerse işlemi iptal edip menüye dönsün.
                if (xIleCikis && plaka == "X")
                    return false;

                // Plaka formatı doğru değilse tekrar sor.
                if (!PlakaGecerliMi(plaka))
                {
                    Console.WriteLine("Bu şekilde plaka girişi yapamazsınız. Tekrar deneyin.");
                    continue;
                }

                // Buraya geldiysek plaka formatı doğrudur.
                return true;
            }
        }
        //-----------------------------------------------------------------------



        static bool PlakaGecerliMi(string plaka)
        {
            /* Bu metod, Türkiye plaka formatına benzer bir doğrulama yapar.
               Amaç: Kullanıcı saçma/yanlış plaka girmesin, program hatasız çalışsın. */

            // null ise direkt geçersiz
            if (plaka == null) return false;

            /* Uzunluk kontrolü:
               En kısa plaka: 01 + A + 9999 = 7 karakter
               En uzun plaka: 99 + ABC + 9999 = 9 karakter */
            if (plaka.Length < 7 || plaka.Length > 9) return false;

            // İlk iki karakter il kodu: mutlaka sayı olmalı.
            string ilStr = plaka.Substring(0, 2);

            int ilKodu;

            // Harf girilirse TryParse false döndürür, program çökmez.
            if (!int.TryParse(ilStr, out ilKodu)) return false;

            // 01-99 arası olmalı (00 kabul değil)
            if (ilKodu < 1 || ilKodu > 99) return false;

            // İl kodundan sonra kaç harf var sayıyoruz (1 ile 3 arası)
            int i = 2;
            int harfSayisi = 0;

            while (i < plaka.Length && char.IsLetter(plaka[i]))
            {
                harfSayisi++;
                i++;

                // 3'ten fazla harf olamaz
                if (harfSayisi > 3) return false;
            }

            // En az 1 harf olmak zorunda
            if (harfSayisi < 1) return false;

            // Harflerden sonra kalan kısım sayı olmalı
            string sayiStr = plaka.Substring(i);

            int sonSayi;

            // Tamamı sayı değilse geçersiz
            if (!int.TryParse(sayiStr, out sonSayi)) return false;

            // Boş olamaz
            if (sayiStr.Length == 0) return false;

            // Ekstra güvenlik: her karakter rakam mı?
            foreach (char c in sayiStr)
                if (!char.IsDigit(c)) return false;

            /* Hane sayısı kuralı:
               - 1 harf varsa sayı kısmı 4 haneli
               - 2-3 harf varsa sayı kısmı 3 veya 4 haneli */
            if (harfSayisi == 1)
            {
                if (sayiStr.Length != 4) return false;
            }
            else if (harfSayisi == 2 || harfSayisi == 3)
            {
                if (sayiStr.Length != 3 && sayiStr.Length != 4) return false;
            }

            // Tüm kontroller geçti: plaka geçerli
            return true;
        }
        //-----------------------------------------------------------------------



        static void ArabaKirala() // 1, K
        {
            // Kullanıcıya hangi işlemde olduğunu gösteren başlık
            Console.WriteLine();
            Console.WriteLine("-Araba Kirala-");
            Console.WriteLine();

            // Galeride kiralanabilir araç var mı?
            if (OtoGaleri.GaleridekiAracSayisi == 0)
            {
                Console.WriteLine("Tüm araçlar kirada.");
                return;
            }
            string plaka;

            // Geçerli ve kiralanabilir plaka gelene kadar döner
            while (true)
            {
                // PlakaAl: format doğrular, X ile çıkış sağlar
                if (!PlakaAl("Kiralanacak arabanın plakası: ", out plaka))
                    return;

                // KiralanabilirMi: iş kuralını Galeri sınıfı kontrol eder
                if (!OtoGaleri.KiralanabilirMi(plaka, out string mesaj))
                {
                    // Galeri mesajı üretir, Program sadece ekrana basar
                    Console.WriteLine(mesaj);
                    continue;
                }

                // Buraya geldiyse: plaka var ve araç galeride
                break;
            }

            int sure;

            // Süre doğru girilene kadar sorulur
            while (true)
            {
                Console.Write("Kiralama Süresi: ");
                string giris = Console.ReadLine();
                Console.WriteLine();

                // Kullanıcı X girerse iptal edip menüye döner
                if (giris != null && giris.Trim().ToUpper() == "X") return;

                // Sayı ve pozitif kontrolü
                if (!int.TryParse(giris, out sure) || sure <= 0)
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                    continue;
                }
                break;
            }

            // Kiralama işlemini Galeri yapar (başarı mesajı da Galeri'de basılır)
            OtoGaleri.ArabaKirala(plaka, sure);
        }
        //------------------------------------------------------------------------



        static void ArabaTeslimAl() // 2, T
        {
            Console.WriteLine();
            Console.WriteLine("-Araba Teslim Al-");
            Console.WriteLine();

            // Kirada hiç araba yoksa, kullanıcıdan plaka almak gereksiz
            if (OtoGaleri.KiradakiAracSayisi == 0)
            {
                Console.WriteLine("Kirada hiç araba yok.");
                return;
            }

            string plaka;

            // Geçerli ve teslim alınabilir plaka gelene kadar döner
            while (true)
            {
                if (!PlakaAl("Teslim edilecek arabanın plakası: ", out plaka))
                    return;

                // TeslimAlinabilirMi: araç var mı, kirada mı? iş kuralı Galeri'de
                if (!OtoGaleri.TeslimAlinabilirMi(plaka, out string mesaj))
                {
                    Console.WriteLine(mesaj);
                    continue;
                }

                break; // teslim alınabilir
            }

            // Teslim alma işlemini Galeri yapar
            OtoGaleri.ArabaTeslimAl(plaka);
        }
        //------------------------------------------------------------------------



        static void KiradakiArabalariListele() // 3, R
        {
            // Başlık
            Console.WriteLine();
            Console.WriteLine("-Kiradaki Arabalar-");
            Console.WriteLine();

            // Listeleme işini Galeri sınıfı yapar (tablo basımı dahil)
            OtoGaleri.KiradakiArabalariListele();
        }
        //------------------------------------------------------------------------



        static void GaleridekiArabalariListele() // 4, M
        {
            Console.WriteLine();
            Console.WriteLine("-Galerideki Arabalar-");
            Console.WriteLine();

            OtoGaleri.GaleridekiArabalariListele();
        }
        //------------------------------------------------------------------------



        static void TumArabalariListele() // 5, A
        {
            Console.WriteLine();
            Console.WriteLine("-Tüm Arabalar-");
            Console.WriteLine();

            OtoGaleri.TumArabalariListele();
        }
        //------------------------------------------------------------------------



        static void KiralamaIptali() // 6, İ
        {
            Console.WriteLine();
            Console.WriteLine("-Kiralama İptali-");
            Console.WriteLine();

            if (OtoGaleri.KiradakiAracSayisi == 0)
            {
                Console.WriteLine("Kirada araba yok.");
                return;
            }

            string plaka;

            // Geçerli ve iptal edilebilir plaka gelene kadar döner
            while (true)
            {
                if (!PlakaAl("Kiralaması iptal edilecek arabanın plakası: ", out plaka))
                    return;

                // KiralamaIptalEdilebilirMi: araç var mı, kirada mı? karar Galeri'de
                if (!OtoGaleri.KiralamaIptalEdilebilirMi(plaka, out string mesaj))
                {
                    Console.WriteLine(mesaj);
                    continue;
                }

                break;
            }

            // İptal işlemini Galeri yapar
            OtoGaleri.KiralamaIptali(plaka);
        }
        //------------------------------------------------------------------------



        static void ArabaEkle() // 7, Y
        {
            Console.WriteLine();
            Console.WriteLine("-Araba Ekle-");
            Console.WriteLine();

            string plaka;

            // Plaka al + sistemde var mı kontrol et
            while (true)
            {
                if (!PlakaAl("Plaka: ", out plaka))
                    return;

                // EklenebilirMi: plaka daha önce eklenmiş mi? kontrol Galeri'de
                if (!OtoGaleri.EklenebilirMi(plaka, out string mesaj))
                {
                    Console.WriteLine(mesaj);
                    continue;
                }

                break; // plaka uygundur ve sistemde yoktur
            }

            // Marka al
            string marka;

            while (true)
            {
                Console.Write("Marka: ");
                marka = Console.ReadLine();

                if (marka == null)
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                    continue;
                }

                marka = marka.Trim();

                if (marka.ToUpper() == "X") return;

                if (marka.Length == 0)
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                    continue;
                }

                bool harfVarMi = false;
                bool sadeceHarfRakam = true;

                foreach (char c in marka)
                {
                    if (char.IsLetter(c)) harfVarMi = true;
                    if (!char.IsLetterOrDigit(c)) { sadeceHarfRakam = false; break; }
                }

                if (!sadeceHarfRakam || !harfVarMi)
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                    continue;
                }

                marka = marka.ToUpper();
                break;
            }


            // Kiralama bedeli al
            int kiralamaBedeli;
            while (true)
            {
                Console.Write("Kiralama bedeli: ");
                string giris = Console.ReadLine();

                // X ile iptal
                if (giris != null && giris.Trim().ToUpper() == "X") return;

                // Sayısal ve pozitif kontrol
                if (!int.TryParse(giris, out kiralamaBedeli) || kiralamaBedeli <= 0)
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                    continue;
                }

                break;
            }

            // Araç tipi seçimi
            string aTipi = "";

            while (true)
            {
                Console.WriteLine("Araç tipi:");
                Console.WriteLine("SUV için 1");
                Console.WriteLine("Hatchback için 2");
                Console.WriteLine("Sedan için 3");

                Console.Write("Araba Tipi: ");
                string secim = Console.ReadLine();

                if (secim != null && secim.Trim().ToUpper() == "X") return;

                // Kullanıcının seçimine göre araç tipini belirliyoruz
                if (secim == "1") { aTipi = "SUV"; break; }
                if (secim == "2") { aTipi = "Hatchback"; break; }
                if (secim == "3") { aTipi = "Sedan"; break; }

                // Geçersiz girişte tekrar sor
                Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                Console.WriteLine();
            }

            // Aracı ekleme işini Galeri yapar
            OtoGaleri.ArabaEkle(plaka, marka, kiralamaBedeli, aTipi);
        }

        //------------------------------------------------------------------------



        static void ArabaSil() // 8, S
        {
            Console.WriteLine();
            Console.WriteLine("-Araba Sil-");
            Console.WriteLine();

            // Galeride hiç araç yoksa silme yapılmaz
            if (OtoGaleri.GaleridekiAracSayisi == 0)
            {
                Console.WriteLine("Galeride silinecek araba yok.");
                return;
            }

            string plaka;

            // Silinebilir plaka gelene kadar döner
            while (true)
            {
                if (!PlakaAl("Silmek istediğiniz arabanın plakasını giriniz: ", out plaka))
                    return;

                // SilinebilirMi: araç var mı ve galeride mi? kontrol Galeri'de
                if (!OtoGaleri.SilinebilirMi(plaka, out string mesaj))
                {
                    Console.WriteLine(mesaj);
                    continue;
                }

                break;
            }

            // Silme işini Galeri yapar
            OtoGaleri.ArabaSil(plaka);
        }
        //------------------------------------------------------------------------



        static void BilgileriGoster() // 9,G
        {
            // Başlık
            Console.WriteLine();
            Console.WriteLine("-Galeri Bilgileri-");
            Console.WriteLine();

            // İstatistik hesaplama ve yazdırma işi Galeri'de
            OtoGaleri.BilgileriGoster();
        }
        //------------------------------------------------------------------------
    }
}
