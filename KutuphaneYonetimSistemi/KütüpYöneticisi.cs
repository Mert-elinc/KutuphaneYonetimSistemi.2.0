using KayitSistemi;
using System.IO;  // Dosya okuma/yazma işlemleri için gerekli kütüphanedir
using System;
using System.Collections.Generic;

namespace kütüphaneYönetimSistemi
{
    public class kütüphaneYöneticisi
    {
        // static listeler:program boyunca kitap ve kullanıcı bilgilerini gecici RAM'de tutmak için
        public static List<Kullanici> kullaniciListesi = new List<Kullanici>();
        public static List<Kitap> kitapListesi = new List<Kitap>();

        // Program açılırken mainde tek seferde her şeyi yüklemek için burayı çalıştırıyoruz
        public static void tümSistemiYükle()
        {
            Console.WriteLine("- Sistem Verileri Yükleniyor -");
            // burda dikkat etmemiz gereken sıralamadır önce üyeleri yüklemeliyiz çünkü kitapları yüklerken bu kitap kimde diye bakacağız
            uyeleriYükle();
            // üyeler yüklendikten sonra kitapları yükleyip eşleştirme yapıcağız
            kitaplariYükle();
        }
        // şimdi program kapanırken case 0 da her şeyi kaydetmek için burayı çalıştırıyoruz
        public static void tümSistemiKaydet()
        {
            uyeleriKaydet();
            kitaplariKaydet();
           

        }
        // şimdi en önemli kısım metodlarımız bunları private yapıyoruz sadece bu sınıfın içinden kullanılabilsin diye
        private static void uyeleriKaydet()
        {
            using (StreamWriter yazici = new StreamWriter("uyeler.txt"))
            {
                foreach (var u in kullaniciListesi)
                {
                    // üyeleri aralarında | ile ayırarak kayıt ediyorum
                    yazici.WriteLine($"{u.Ad}|{u.Soyad}|{u.TC}");// ufak bir bilgi: normalde konsola yazmak icin console.writeline kullanırız
                                                               // ama burada yazici.writeline nesnesi ile dosyaya yazıyoruz

                }
            }
        }

        private static void uyeleriYükle()
        {
            // dosya yoksa hata vermemesi için kontrol ediyoruz var mı diye 
            if (File.Exists("uyeler.txt"))
            {
                string[] satirlar = File.ReadAllLines("uyeler.txt");// tüm satırları bir dizi gibi okuyoruz

                foreach (string satir in satirlar)
                {

                    string[] p = satir.Split('|');// satırı | işaretinden parçalayarak ayırıyoruz yine bir diziye atıyoruz

                    if (p.Length == 3)// parçaların sayısı 3 mü diye kontrol ediyoruz hata olmaması icin
                    {
                        // 3 ise verileri parçalarından alıp yeni oluşturduğumuz "k" nesnesine dolduruyoruz
                        Kullanici k = new Kullanici();
                        k.Ad = p[0];
                        k.Soyad = p[1];
                        k.TC = long.Parse(p[2]);// tc cok buyuk bır rakam olduğu ıcın longla sayıya cevrıyoruz
                        kullaniciListesi.Add(k);// son olarakta oluşturduğumuz nesneyi kullanıcı listesine ekliyoruz
                    }
                }
            }
        }
        // Şimdi sıra kitapları kayıt etmede yine private yapıyoruz 
        private static void kitaplariKaydet()
        {
            using (StreamWriter yazici = new StreamWriter("kutuphane.txt"))
            {

                foreach (var k in kitapListesi)
                {
                    // önce kitap kimde ? dosyaya bunu yazmamız lazım
                    // eğer kitap hiçkimsede değilse oduncAlan null olur , kimsenin tcsini yazamayız
                    // bu yüzden önce gecici bir değişken olan alanKisiTC yi tanımlıyoruz
                    long alanKisiTC = 0; // eğer kitap kimseye ödünç verilmemişse 0 kalacak
                    if (k.oduncAlan != null)// eğer kitap birindeyse
                    {
                        alanKisiTC = k.oduncAlan.TC; // o kişinin tcsini alıyoruz
                    }
                    // dosyaya yazarken formatımız : KitapAdi|Yazar|KitapKodu|oduncAlanTC
                    // örneğin : "Suç ve Ceza|Dostoyevski|12345|98765432100" gibi olcak
                    yazici.WriteLine($"{k.KitapAdi}|{k.Yazar}|{k.KitapKodu}|{alanKisiTC}");
                }
            }
        }


        // ŞİMDİ ZOR OLAN KISIM KİTAPLARI YÜKLEME VE BUNLARI EŞLEŞTİRME

        private static void kitaplariYükle()
        {
            if (File.Exists("kutuphane.txt"))
            {
                string[] satirlar = File.ReadAllLines("kutuphane.txt");
                foreach (string satir in satirlar)
                {
                    string[] p = satir.Split('|');// satırı | işaretinden parçalayarak ayırıyoruz yine bir diziye atıyoruz
                    if (p.Length >= 3)// en az 3 parca olmalı "ad,yazar,kod" diye
                    {
                        Kitap k = new Kitap();
                        k.KitapAdi = p[0];
                        k.Yazar = p[1];
                        k.KitapKodu = Convert.ToInt32(p[2]);


                        // BU KISIMDA EŞLEŞTİRME YAPACAĞIZ :  p 4 eleamanlı olursa demekki 4. eleman ödünç alan kişinin tc si demektir

                        if (p.Length == 4)
                        {
                            long okunanTC = Convert.ToInt64(p[3]);
                            // eğer okunanTC 0 dan farklı ise bu kitap birindeymiş demektir bunu bulmamız gerekır 
                            if (okunanTC != 0)
                            {
                                foreach (var u in kullaniciListesi)
                                {

                                    if (u.TC == okunanTC)
                                    {
                                        // eşleşme bulundu
                                        k.oduncAlan = u; // kitabın ödünç alanını bu kullanıcı yapıyoruz
                                        break; // eşleşme bulunduğu için döngüden çıkıyoruz
                                    }

                                }
                            }
                        }

                        // eşleştirme tamamlandıktan sonra kitabı kitap listesine ekliyoruz
                        kitapListesi.Add(k);
                    }
                }
            }
        }
    }
}