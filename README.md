# 📚 Kütüphane Yönetim Paneli (Admin Panel)

Bu proje, kütüphane yönetimi için geliştirilmiş bir admin panelidir. Projede kitap, yazar, yayınevi (publisher), admin gibi varlıkların eklenmesi, silinmesi ve güncellenmesi işlemleri yapılabilmektedir.

## ✨ Özellikler

- **🔐 Identity**
- **🛠 ORM Aracı**: Entity Framework Core
- **🔔 Bildirimler**: AspNetCoreHero.ToastNotification
- **🔄 EntityFramework Proxies**
- **💾 Veritabanı**: Sql Server
- **🗺 Mapster**
- **🌐 UI**: MVC ve API
- **🏗 Katmanlı Mimari**
- **📚 Generic Repository Pattern**
- **🛠 Utilities Yapısı**
- **🔄 Auditable Entity ve Base Entity**: Hard delete veya soft delete işlemleri yönetilmektedir.
- **📦 Dependency Injection**
- **🌱 Seed Data**

## 🚀 Proje Özellikleri

- **📚 Kitap Yönetimi**: Kitap ekleme, silme ve güncelleme
- **✍️ Yazar Yönetimi**: Yazar ekleme, silme ve güncelleme
- **🏢 Yayınevi Yönetimi**: Yayınevi ekleme, silme ve güncelleme
- **👨‍💼 Admin Yönetimi**: Admin ekleme, silme ve güncelleme

## 📦 Kullanılan Paketler

- **Identity**
- **Entity Framework Core**
- **AspNetCoreHero.ToastNotification**
- **EntityFramework Proxies**
- **Sql Server**
- **Mapster**
- **Dependency Injection**

## 📂 Proje Yapısı

### 🏗 Katmanlı Mimari

Projede katmanlı mimari dizayn edilmiştir ve aşağıdaki katmanlar bulunmaktadır:

- **Domain Katmanı**: Varlıklar (Entities) burada tanımlanmıştır.
- **Infrastructure Katmanı**: Veri erişim işlemleri ve konfigürasyonlar burada gerçekleştirilmiştir.
- **Application Katmanı**: İş mantığı (Business Logic) burada uygulanmıştır.
- **UI Katmanı**: Kullanıcı arayüzü ve API'ler burada yer almaktadır.

### 🛠 Generic Repository Pattern

Projede Generic Repository Pattern uygulanmıştır. Bu yapı, veri erişim işlemlerinin yönetilmesini sağlar.

### 🔄 Auditable Entity ve Base Entity

Projede `AuditableEntity` ve `BaseEntity` sınıfları kullanılarak hard delete ve soft delete işlemleri yönetilmektedir.

### 📦 Dependency Injection

Projede Dependency Injection kullanılarak bağımlılıkların yönetimi sağlanmıştır.

### 🌱 Seed Data

Projede başlangıç verileri (seed data) kullanılarak veritabanı başlangıçta belirli verilerle doldurulmuştur.



