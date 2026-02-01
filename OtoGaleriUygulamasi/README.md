# 🚗 Oto Galeri Uygulaması

Bu proje, araç kiralama süreçlerini yöneten **console tabanlı bir galeri otomasyon sistemidir**.

---

## 🎯 Amaç
- Gerçek hayata yakın iş kuralları oluşturmak
- Kullanıcı hatalarına karşı güvenli girişler sağlamak
- İş mantığı ile kullanıcı arayüzünü ayırmak

---

## 🔧 Özellikler

### 🚘 Araç Yönetimi
- Araç ekleme
- Araç silme
- Galerideki araçları listeleme

### 🔑 Kiralama İşlemleri
- Araç kiralama
- Araç teslim alma
- Kiralama iptali

### 📋 Listeleme
- Kiradaki araçları listeleme
- Galerideki araçları listeleme
- Tüm araçları listeleme

### 🧠 Kontroller
- Türkiye plaka formatı doğrulaması
- Araç kiralanabilir / silinebilir kontrolü
- Hatalı girişlerde kullanıcı yönlendirme

---

## 🧱 Mimari Yapı
- `Program.cs` → Giriş noktası (Main)
- `Uygulama.cs` → Menü ve kullanıcı etkileşimi
- `Galeri.cs` → İş kuralları
- `Araba.cs` → Veri modeli

---

## ▶️ Çalıştırma
1. Visual Studio ile `OtoGaleriUygulamasi.csproj` dosyasını açın
2. Start diyerek uygulamayı çalıştırın

---

## 📌 Not
Bu proje, console uygulamalarında **iş kuralı odaklı tasarım** yaklaşımını öğretmeyi amaçlamaktadır.
