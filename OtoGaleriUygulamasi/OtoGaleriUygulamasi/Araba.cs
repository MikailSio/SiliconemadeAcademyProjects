using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Bu sınıf tek bir arabayı temsil eder.
// Galerideki her araba bu sınıftan üretilmiş bir nesnedir.
namespace OtoGaleriUygulamasi
{
    internal class Araba
    {
        public string Plaka;
        public string Marka;
        public int KiralamaBedeli;
        public string AracTipi;
        public string Durum;

        // Constructor (yapıcı metot)
        // Yeni bir araba oluşturulduğunda ilk değerleri ayarlamak için kullanılır.
        public Araba(string plaka, string marka ,int kiralamaBedeli, string aTipi)
        {
            // Parametre olarak gelen değerler sınıfın alanlarına atanır.
            this.Plaka = plaka;
            this.Marka = marka;
            this.KiralamaBedeli = kiralamaBedeli;
            this.AracTipi = aTipi;

            // Yeni eklenen her araba varsayılan olarak galeride olur.
            this.Durum = "Galeride";
        }
        //-----------------------------------------------------------------------
        


        /* Arabanın tüm kiralama sürelerini tutan liste.
        Örnek: [3, 5, 2] → araba 3 saat, sonra 5 saat, sonra 2 saat kiralanmış*/
        public List<int> KiralamaSureleri = new List<int>();

        // Liste kaç elemanlıysa, araba o kadar kiralanmıştır.
        public int KiralamaSayisi
        {
            get
            {
                return KiralamaSureleri.Count;
            }
        }
        //------------------------------------------------------------------------




        // KiralamaSureleri listesindeki tüm değerleri toplar.
        public int ToplamKiralamaSuresi
        {
            get
            {
                return this.KiralamaSureleri.Sum();
            }
        }
    }
}
