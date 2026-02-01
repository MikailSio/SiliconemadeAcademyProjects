namespace OkulYonetimUygulamasi
{
    internal class Okul
    {
        //##################################################################
        //----------------------Okuldaki tüm öğrenciler---------------------
        //##################################################################
        public List<Ogrenci> Ogrenciler { get; set; } = new List<Ogrenci>();




        //##################################################################
        //-------------------------Öğrenci bul------------------------------
        //##################################################################
        public Ogrenci? OgrenciBul(int no)
        {   /*
                Ogrenciler listesi üzerinde dolaşılır
                x => listedeki her bir Ogrenci nesnesini temsil eder
                x.No == no → öğrencinin numarası, metoda gönderilen numaraya eşit mi?
                FirstOrDefault:
                - Şarta uyan İLK öğrenciyi döner
                - Hiç eşleşme yoksa null döner          */
            return Ogrenciler.FirstOrDefault(x => x.No == no);
        }



        //##################################################################
        //------Öğrenci ekle:--No çakışıyorsa: max+1 verip geri döndürür.---
        //##################################################################
        public int OgrenciEkle(int no, string ad, string soyad, DateTime dogumTarihi, CINSIYET cinsiyet, SUBE sube)
        {   /*
                Girilen öğrenci numarası daha önce kullanılmış mı?
                Any():
                - Listede EN AZ BİR tane x.No == no varsa true döner
                - Hiç yoksa false döner         */
            bool cakisti = Ogrenciler.Any(x => x.No == no);

            /*      Eğer sistemde hiç öğrenci yoksa → numara 1 ver
                    Eğer öğrenci varsa:
                    - Mevcut öğrenciler arasındaki EN BÜYÜK numarayı bul
                    - +1 ekle   */

            int yeniNo = no;    // yeniNo başlangıçta girilen no olsun
            if (cakisti)        // çakışma varsa
                yeniNo = (Ogrenciler.Count == 0) ? 1 : Ogrenciler.Max(x => x.No) + 1;// max+1

            Ogrenciler.Add(new Ogrenci  // yeni öğrenci ekle
            {
                No = yeniNo,
                Ad = ad,
                Soyad = soyad,
                DogumTarihi = dogumTarihi,
                Cinsiyet = cinsiyet,
                Subesi = sube
            });

            return yeniNo; // eklenen öğrencinin numarasını döner
        }


        //##################################################################
        //--------------------Öğrenci güncelle------------------------------
        //##################################################################
        public void OgrenciGuncelle(int no, string ad, string soyad, DateTime dogumTarihi, CINSIYET cinsiyet, SUBE sube)
        {
            var o = OgrenciBul(no);         // öğrenciyi bul
            if (o == null) return;          // yoksa çık

            // bilgileri güncelle
            o.Ad = ad;                      
            o.Soyad = soyad;               
            o.DogumTarihi = dogumTarihi;
            o.Cinsiyet = cinsiyet;
            o.Subesi = sube;
        }


        //##################################################################
        //---------------------------Öğrenci sil----------------------------
        //##################################################################
        public void OgrenciSil(int no)      // öğrenci numarasına göre
        {
            var o = OgrenciBul(no);         //öğrenciyi bul
            if (o == null) return;          // yoksa çık

            Ogrenciler.Remove(o);           // öğrenciyi sil
        }


        //##################################################################
        //----------------------Adres ekle/güncelle-------------------------
        //##################################################################
        public void AdresEkle(int no, string il, string ilce, string mahalle)
        {
            var o = OgrenciBul(no);         // öğrenciyi bul
            if (o == null) return;          // yoksa çık

            o.Adresi = new Adres            // adresi ekle/güncelle
            {
                Il = il,
                Ilce = ilce,
                Mahalle = mahalle
            };
        }


        //##################################################################
        //-------------------------Kitap ekle-------------------------------
        //##################################################################
        public void KitapEkle(int no, string kitapAdi) // öğrenci numarasına göre
        {
            var o = OgrenciBul(no);                     // öğrenciyi bul
            if (o == null) return;                      // yoksa çık

            o.Kitaplar.Add(new Kitap(kitapAdi));        //kitabı ekle
        }


        //##################################################################
        //-------------------Not ekle (ders adı + tek not)------------------
        //##################################################################
        // 
        public void NotEkle(int no, string dersAdi, int not)    // öğrenci numarasına göre
        {
            var o = OgrenciBul(no);                             // öğrenciyi bul
            if (o == null) return;                              // yoksa çık

            o.Notlar.Add(new DersNotu(dersAdi, not));           // notu ekle
        }


        //##################################################################
        //----------------------Listeleme / Sıralama------------------------
        //##################################################################
        public IEnumerable<Ogrenci> OgrencilerSirali()  //Sube ve numara sıralı
        {
            // Önce şube, sonra numara
            return Ogrenciler
                .OrderBy(o => o.Subesi)         // şube sıralı
                .ThenBy(o => o.No);             // numara sıralı
        }


        //##################################################################
        //------------------------Sube filtreli------------------------------
        //##################################################################
        public IEnumerable<Ogrenci> OgrencilerSube(SUBE sube)       // belirli şube
        {
            return OgrencilerSirali().Where(o => o.Subesi == sube); // şube filtreli
        }


        //##################################################################
        //-----------------------Cinsiyet filtreli----------------------------
        //##################################################################
        public IEnumerable<Ogrenci> OgrencilerCinsiyet(CINSIYET c)  // belirli cinsiyet
        {
            return OgrencilerSirali().Where(o => o.Cinsiyet == c);  // cinsiyet filtreli
        }


        //##################################################################
        //----------------------dogum tarih sıralı--------------------------
        //##################################################################
        public IEnumerable<Ogrenci> OgrencilerDogumSonra(DateTime tarih)    // belirli tarihten sonra
        {
            return OgrencilerSirali().Where(o => o.DogumTarihi > tarih);    // tarih filtreli
        }


        //##################################################################
        //------------------------il sıralı---------------------------------
        //##################################################################
        public IEnumerable<Ogrenci> OgrencilerIlSirali()                    // il sıralı
        {
            // Adresi olmayanlar en alta düşsün diye büyük değer verdik
            return Ogrenciler
                .OrderBy(o => o.Adresi?.Il ?? "ZZZ")                        // il sıralı
                .ThenBy(o => o.Adresi?.Mahalle ?? "ZZZ")                    // mahalle sıralı
                .ThenBy(o => o.Subesi)                                      // şube sıralı
                .ThenBy(o => o.No);                                         // numara sıralı
        }
        //##################################################################
        //--------------------Okul en başarılı N----------------------------
        //##################################################################
        public IEnumerable<Ogrenci> EnBasarili(int n)       // en yüksek not ortalaması
        {
            return Ogrenciler
                .OrderByDescending(o => o.NotOrtalamasi)    // not ortalaması sıralı
                .ThenBy(o => o.Subesi)                      // şube sıralı
                .ThenBy(o => o.No)                          // numara sıralı
                .Take(n);                                   // ilk n öğrenciyi al
        }


        //##################################################################
        //----------------------Okul en başarısız N-------------------------
        //##################################################################
        public IEnumerable<Ogrenci> EnBasarisiz(int n)          // en düşük not ortalaması
        {
            return Ogrenciler                   // öğrenciler listesi
                .OrderBy(o => o.NotOrtalamasi)  // not ortalaması sıralı
                .ThenBy(o => o.Subesi)          // şube sıralı
                .ThenBy(o => o.No)              // numara sıralı
                .Take(n);                       // Sıralanmış listenin sadece ilk n elemanını alır.
        }
        //##################################################################
        //-----------------------Şube en başarılı N-------------------------
        //##################################################################
        // 
        public IEnumerable<Ogrenci> SubeEnBasarili(SUBE sube, int n)    // belirli şube
        {
            return Ogrenciler                                   // tüm öğrenciler listesi.
                .Where(o => o.Subesi == sube)                   // girilen şube
                .OrderByDescending(o => o.NotOrtalamasi)        // OrderByDescending büyükten küçüğe sıralar
                .ThenBy(o => o.No)                              // eşit notta öğrenci numarasına göre sıralar.
                .Take(n);                                       // Sıralanmış listenin sadece ilk n elemanını alır.
        }
        //##################################################################
        //---------------------Şube en başarısız N--------------------------
        //##################################################################
        public IEnumerable<Ogrenci> SubeEnBasarisiz(SUBE sube, int n)   // belirli şube
        {
            return Ogrenciler                           // tüm öğrenciler listesi.
                .Where(o => o.Subesi == sube)           // girilen şube.
                .OrderBy(o => o.NotOrtalamasi)          // OrderBy küçükten büyüğe sıralar.
                .ThenBy(o => o.No)                      // eşit notta öğrenci numarasına göre sıralar.
                .Take(n);                               // Sıralanmış listenin sadece ilk n elemanını alır.
        }


        //##################################################################
        //Şube ortalaması (şubedeki öğrencilerin ortalamalarının ortalaması)
        //##################################################################
        public float SubeOrtalamasi(SUBE sube)      // seçilen şube
        {
            var liste = Ogrenciler.Where(o => o.Subesi == sube).ToList();       // şube öğrencileri
            if (liste.Count == 0) return 0f;                                    // öğrenci yoksa 0 döner

            return (float)liste.Average(o => o.NotOrtalamasi);                  // ortalama
        }
        //------------------------------------------------------------------
    }
}
