# 📚 Kütüphane Yönetim Sistemi / Library Management System

## 🇹🇷 Türkçe (Turkish)

### Proje Hakkında
Bu proje, C# programlama dilini ve Nesne Yönelimli Programlama (OOP) mantığını öğrenme sürecimde geliştirdiğim bir **Konsol Tabanlı Kütüphane Otomasyonu**dur.

Daha önce geliştirdiğim basit kütüphane uygulamasının eksiklerini gidererek, bu versiyonda **veri kalıcılığı (Data Persistence)** ve **ilişkisel takip** üzerine odaklandım. Artık program kapatılsa bile veriler kaybolmuyor, yerel metin dosyalarında (`.txt`) güvenle saklanıyor.

### 🚀 Özellikler
Bu projede kendimi geliştirmek adına eklediğim temel özellikler şunlardır:

* **Veri Kalıcılığı:** Tüm üye ve kitap verileri `uyeler.txt` ve `kutuphane.txt` dosyalarında saklanır. Program açıldığında veriler otomatik yüklenir.
* **Üye Yönetimi:** Yeni üye ekleme, silme ve listeleme (TC Kimlik ve mükerrer kayıt kontrolü ile).
* **Kitap Yönetimi:** Kitap ekleme, silme ve listeleme (Benzersiz kitap kodu kontrolü ile).
* **Ödünç/İade Sistemi:** Bir kitabı bir üyeye zimmetleme (ödünç verme) ve geri alma (iade) işlemleri.
* **Akıllı Kontroller:** Borcu (teslim etmediği kitabı) olan üyenin silinmesini engelleme, olmayan kitabı ödünç verememe gibi mantıksal hata kontrolleri.

### 🛠️ Kullanılan Teknolojiler
* C# (.NET 6.0+)
* Dosya İşlemleri (System.IO)
* OOP (Class, Encapsulation, List Structures)

---

## 🇬🇧 English

### About the Project
This project is a **Console-Based Library Automation System** that I developed during my journey of learning C# and Object-Oriented Programming (OOP).

Building upon a simpler library application I wrote previously, I focused on **Data Persistence** and **Relational Tracking** in this version. Data is no longer lost when the program is closed; it is securely stored in local text files (`.txt`).

### 🚀 Features
Here are the key features I implemented to challenge myself:

* **Data Persistence:** All user and book data is stored in `uyeler.txt` and `kutuphane.txt`. Data loads automatically upon startup.
* **User Management:** Add, delete, and list users (with ID validation and duplicate checks).
* **Book Management:** Add, delete, and list books (with unique book code validation).
* **Borrowing/Returning System:** Assign books to users (borrowing) and process returns.
* **Smart Validations:** Logical error checks, such as preventing the deletion of a user who still has a borrowed book.

### 🛠️ Technologies Used
* C# (.NET 6.0+)
* File Handling (System.IO)
* OOP (Class, Encapsulation, List Structures)
