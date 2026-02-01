using KayitSistemi;
using kütüphaneYönetimSistemi;
using System.Collections.Generic;
namespace kütüphaneYönetimSistemi;
class Program
{

    // 1-) bu kısım kodun kullanıcıdan veri alma kısmıdır
    static int Main(string[] args)
    { 
        // Program başlarken tek komutla her şeyi geri getir:
        kütüphaneYöneticisi.tümSistemiYükle();
        Console.WriteLine("Kütüphane Yönetim Sistemine Hoşgeldiniz Yapmak İstediğini İşlemi Numaralardan Seçiniz :  ");

        while (true)
        {


            Console.WriteLine("Üye eklemek için        =  1 ");
            Console.WriteLine("Üye silmek için         =  2 ");
            Console.WriteLine("Üyeleri listelemek için =  3 ");
            Console.WriteLine("Kitap Ekle              =  4 ");
            Console.WriteLine("Kitapları Listele       =  5 ");
            Console.WriteLine("Kitap silmek için       =  6 ");
            Console.WriteLine("Kitap ödünç ver         =  7 ");
            Console.WriteLine("Kitap iade etmek için   =  8 ");
            Console.WriteLine("Çıkış yapmak için       =  0 ");
            Console.Write("Seçiminizi giriniz      :  ");
            string? secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    {
                        while (true)
                        {
                            try
                            {
                                Kullanici k1 = new Kullanici();

                                Console.Write("Lütfen adınızı giriniz : ");
                                k1.Ad = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(k1.Ad))
                                    throw new Exception("İsim boş bırakılamaz!");

                                if (!k1.Ad.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                                    throw new Exception("İsim sadece harf ve boşluk içerebilir!");

                                Console.Write("Soyadınızı giriniz : ");
                                k1.Soyad = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(k1.Soyad))
                                    throw new Exception("Soyad boş bırakılamaz!");

                                if (!k1.Soyad.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                                    throw new Exception("Soyad sadece harf ve boşluk içerebilir!");

                                Console.Write("11 haneli TC giriniz : ");
                                string tcNo = Console.ReadLine();

                                // 1. KONTROL: Uzunluk ve Sayı olma durumu
                                if (tcNo.Length != 11 || !long.TryParse(tcNo, out long _))
                                {
                                    throw new Exception("TC Kimlik Numarası geçersiz! Tam olarak 11 haneli ve rakamlardan oluşmalıdır.");
                                }
                                k1.TC = Convert.ToInt64(tcNo);// string olarak aldığımız TC yi int e çeviriyor

                                // girilen tc daha önceden kayıtlı mı kontrolunu yapalım 
                                bool tcVarMi = false;
                                foreach (var u in kütüphaneYöneticisi.kullaniciListesi)
                                {
                                    if (u.TC == k1.TC)
                                    {
                                        tcVarMi = true;
                                        break;
                                    }
                                }

                                if (tcVarMi)
                                {
                                    throw new Exception("Bu TC numarası zaten kayıtlı! Aynı kişiyi iki kez ekleyemezsiniz.");
                                }
                                
                                    kütüphaneYöneticisi.kullaniciListesi.Add(k1);// aldığımız bilgilerle kullanıcıyı listeye ekleme kısmı 
                                    kütüphaneYöneticisi.tümSistemiKaydet();
                                    Console.WriteLine(" Kayıt başarılı!");
                               
                                break; // burda kayıt tamamlanır menüye döndür
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Hata: " + ex.Message);
                                Console.WriteLine("Lütfen bilgileri tekrar giriniz.\n");
                                // 2.while döngüsü tekrar başlar 

                            }
                        }
                        break;// case 1 sonu
                    }
                case "2":
                    {
                        Console.WriteLine("Silinecek üyenin TC numarasını giriniz : ");
                        // Önce girilen TC kontrolünü yapalım 
                        string? girilenTc = Console.ReadLine();
                        if (girilenTc.Length != 11 || !long.TryParse(girilenTc, out long silinecekTc))// try parse true /false değer dondurur eğer dönusturme başarılıysa true ise o sayıyı silinecekTc ye atar
                        {
                            Console.WriteLine("Girilen TC numarası hatalıdır tekrar kontrol ediniz! ");
                            break;
                        }

                        // Şimdi üyeyi bulalım
                        Kullanici? silinecekUye = null;
                        foreach (var u in kütüphaneYöneticisi.kullaniciListesi)
                        {
                            if (u.TC == silinecekTc)
                            {
                                silinecekUye = u;
                                break;
                            }
                        }
                        if (silinecekUye == null)
                        {
                            Console.WriteLine("Girilen TC numarasına ait üye bulunmamaktadır! ");
                            break;
                        }
                        // burdada en önemli kısım silinen uyenın ödünc aldiği kitap var mı bunu kontrol etmemız çok kritiktir!!
                        // kitap listesini gezip "oduncAlan " kısmı bizim silinecek üyeye eşit olan kitap var mı bakıyoruz

                        bool elindeKitapVarMi = false;

                        foreach (var k in kütüphaneYöneticisi.kitapListesi)
                        {
                            // Kitap birindeyse VE o kişi bizim silinecek üyeyse
                            if (k.oduncAlan != null && k.oduncAlan.TC == silinecekUye.TC)
                            {
                                elindeKitapVarMi = true;
                                break; // Bir tane bulsak yeter, döngüden çık.
                            }
                        }

                        if (elindeKitapVarMi)
                        {
                            Console.WriteLine("HATA!! BU ÜYENİN TESLİM ETMEDİĞİ KİTAPLAR VARDIR, SİLİNEMEZ.");
                        }
                        else
                        {
                            // Engel yoksa siliyoruz
                            kütüphaneYöneticisi.kullaniciListesi.Remove(silinecekUye);
                            Console.WriteLine($"{silinecekUye.Ad} {silinecekUye.Soyad} isimli üye başarıyla silinmiştir.");
                            kütüphaneYöneticisi.tümSistemiKaydet();
                        }
                    } 
                        break;

                case "3":

                    if (kütüphaneYöneticisi.kullaniciListesi.Count == 0)// listede eleman yoksa uyarı vermek adına :
                    {
                        Console.WriteLine("Kullanıcı listesi şuan boş eleman ekleyiniz ");
                    }
                    else
                        foreach (var kullanicilar in kütüphaneYöneticisi.kullaniciListesi)
                        {
                            Console.WriteLine("***************************");
                            kullanicilar.BilgileriYazdir();
                        }
                    break;


                case "4":
                    while (true)
                    {
                        try
                        {
                            Kitap k2 = new Kitap();

                            Console.Write("Lütfen Kitap adını giriniz :");
                            k2.KitapAdi = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(k2.KitapAdi))
                                throw new Exception("Kitap isimi boş bırakılamaz!");

                            if (!k2.KitapAdi.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                                //all her karakter tek tek kontrol eder içindeki hatalardan 1 tanesini yakalarsa false döndürür dikkat!
                                throw new Exception("Kitap isimi sadece harf ve boşluk içerebilir!");

                            Console.Write("Yazarını giriniz giriniz : ");
                            k2.Yazar = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(k2.Yazar))
                                throw new Exception("Yazar kısmı boş bırakılamaz!");

                            if (!(k2.Yazar.All(c => char.IsLetter(c) || char.IsWhiteSpace(c))))
                                throw new Exception("Yazar kısmı sadece harf ve boşluk içerebilir!");

                            Console.Write("Lütfen kitap kodunu giriniz : ");
                            string? girilenKod = Console.ReadLine();
                            // bu kodu kontrol edilmeli aynı koddan başka var mı
                            k2.KitapKodu = Convert.ToInt32(girilenKod);
                            bool kitapVarMi = false;
                            foreach (var k in kütüphaneYöneticisi.kitapListesi)
                            {
                                if (k.KitapKodu == k2.KitapKodu)
                                {
                                    kitapVarMi = true;
                                    break;
                                }
                            }

                            if (kitapVarMi)
                            {
                                Console.WriteLine("HATA: Bu Kitap Kodu zaten kullanılıyor! Kitap eklenemedi.");
                                break; 
                            }

                            if (!girilenKod.Any(char.IsDigit))
                                throw new Exception("Kitap kodunda en az bir sayı olması gerekir ");

                            k2.KitapKodu = Convert.ToInt32(girilenKod);
                            kütüphaneYöneticisi.kitapListesi.Add(k2);
                            kütüphaneYöneticisi.tümSistemiKaydet();
                            Console.WriteLine(" Kayıt başarılı!");

                            break;
                        }
                        catch (Exception ex2)
                        {
                            Console.WriteLine("Hata: " + ex2.Message);
                            Console.WriteLine("Lütfen kitap bilgileri tekrar giriniz.\n");


                        }
                    }
                    break;

                case "5":

                    if (kütüphaneYöneticisi.kitapListesi.Count == 0)
                    {
                        Console.WriteLine("Kitap listesi şuan boş eleman ekleyiniz ");
                    }
                    else
                        foreach (var Kitaplar in kütüphaneYöneticisi.kitapListesi)
                        {
                            Console.WriteLine("***************");
                            Kitaplar.KitapBilgileriniYazdir();
                          
                        }
                    break;

                case "6":

                    Console.WriteLine("Lütfen silmek istediğiniz kitap ismini giriniz  ");
                    String? girilenAd = Console.ReadLine()?.Trim().ToLower();// trim ile başta ve sondaki boşlukları görmezden gelelim
                    Kitap silinecekKitap = null;
                    // arama kısmını foreach ile yapıyorum
                    foreach (var k in kütüphaneYöneticisi.kitapListesi)
                    {
                        if (k.KitapAdi.Trim().ToLower() == girilenAd)// kitabı butunluk olarak değil (adı yazarı kodu) sadece adını arıyoruz 
                        {
                            silinecekKitap = k;
                            break;
                        }
                    }

                    if (silinecekKitap != null)
                    {
                        kütüphaneYöneticisi.kitapListesi.Remove(silinecekKitap);
                        kütüphaneYöneticisi.tümSistemiKaydet();
                        Console.WriteLine("Kitap silme işlemi başarılı ");
                    }
                    else
                    {
                        Console.WriteLine("Aradığınız kitap kütüphanede bulunmamaktadır");
                    }
                    break;




                case "7":
                    {
                        //İLK OLARAK EN GÜVENLİ YOL OLAN TC İLE KULLANICI BULMA İŞLEMİ YAPALIM
                        Console.Write("Lütfen kullanıcı TC numarasını giriniz: ");
                        string? girilenTc = Console.ReadLine();
                        long tcNo = Convert.ToInt64(girilenTc);// tc inte cevirirken çok büyük sayı hatası almamak için long kullanılır
                        Kullanici? bulunanUye = null;// foreach ile kullanıcıyı önce bulalım
                        foreach (var u in kütüphaneYöneticisi.kullaniciListesi)
                        {
                            if (u.TC == tcNo)
                            {
                                bulunanUye = u;
                                break;
                            }
                        }
                        if (bulunanUye == null)
                        {
                            Console.WriteLine("Bu TC'ye ait üye bulunamadı.Lütfen önce üye olunuz.");
                            break;
                        }
                        // şimdi kullanıcı bulundu kitap arama işlemini yapcağız

                        Console.WriteLine($"Hoşgeldiniz {bulunanUye.Ad} {bulunanUye.Soyad} ");
                        Console.WriteLine($"Lütfen ödünç almak istediğiniz kitabın adını giriniz: ");
                        string? girilenKitapAdi = Console.ReadLine()?.Trim().ToLower();
                        Kitap? bulunanKitap = null;
                        // kitap ismine göre arayalım 
                        foreach (var k in kütüphaneYöneticisi.kitapListesi)
                        {
                            if (k.KitapAdi.Trim().ToLower() == girilenKitapAdi)
                            {
                                bulunanKitap = k;
                                break;
                            }
                        }
                        if (bulunanKitap == null)
                        {
                            Console.WriteLine("Aradığınız kitap kütüphanede bulunmamaktadır.");
                            Console.WriteLine("Lütfen kitap adını kontrol ediniz.");
                            break;
                        }

                        // şimdi kontrol işlemini yapalaım
                        if (bulunanKitap.oduncAlan != null)
                        {
                            Console.WriteLine($"Üzgünüz, bu kitap şu anda {bulunanKitap.oduncAlan.Ad} {bulunanKitap.oduncAlan.Soyad} tarafından ödünç alınmış.");
                            break;
                        }

                        else
                        {// işte burada kitabın oduncAlan özelliğiyle bulunanUye yi eşitleyip kitabı ödünç verme işlemini tamamlıyoruz
                            bulunanKitap.oduncAlan = bulunanUye;
                            kütüphaneYöneticisi.tümSistemiKaydet();
                            Console.WriteLine("İşlem başarılı! Kitap ödünç verildi.");
                        }
                       

                    } break;
                    
                case "8":
                    {
                        Console.Write("İade edilecek kitabın adını giriniz: ");
                        string? girilenKitapAdi = Console.ReadLine()?.Trim().ToLower();

                        Kitap? iadeKitap = null;

                        // 1. Kitabı Bulalım
                        foreach (var k in kütüphaneYöneticisi.kitapListesi)
                        {
                            if (k.KitapAdi.Trim().ToLower() == girilenKitapAdi)
                            {
                                iadeKitap = k;
                                break;
                            }
                        }

                        if (iadeKitap == null)
                        {
                            Console.WriteLine("Bu isimde bir kitap bulunamadı!");
                        }
                        else
                        {
                            // 2. Kitap zaten bizde mi (Rafta mı)?
                            if (iadeKitap.oduncAlan == null)
                            {
                                Console.WriteLine("Bu kitap zaten kütüphanede (rafta). İade alınacak bir durum yok.");
                            }
                            else
                            {
                                // 3. İade Al (Kişi bilgisini sil)
                                Console.WriteLine($"Kitap, {iadeKitap.oduncAlan.Ad} isimli üyeden teslim alındı.");
                                iadeKitap.oduncAlan = null; // İlişkiyi koparıyoruz (Rafta oluyor)

                                kütüphaneYöneticisi.tümSistemiKaydet(); // Hemen kaydet
                                Console.WriteLine("İade işlemi tamamlandı.");
                            }
                        }
                        break;
                    }

                case "0":
                    {
                        Console.WriteLine("Çıkış yapılıyor...");
                        // Program kapanırken tek komutla her şeyi kaydet:
                        kütüphaneYöneticisi.tümSistemiKaydet();
                        Console.WriteLine("Tüm Veriler Başarıyla Kayıt Edilmiştir.");

                        return 0;//Programı tamamen sonlandırmak için bu return kulanırız
                    }
                default:
                    {
                        Console.WriteLine("Hatalı sayı tuşladınız !!");

                    }
                    break;
            }
            // görüntü kirliliği olmaması adına
            Console.WriteLine("\nDevam etmek için bir tuşa basınız...");
            Console.ReadKey(); // Kullanıcı bir tuşa basana kadar bekler
            Console.Clear();  // Ekranı tertemiz yapar, menü tekrar gelir.



        }


    }

}