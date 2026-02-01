using KayitSistemi;
using System;
public class Kitap
{
    public string? KitapAdi { get; set; }
    public string? Yazar { get; set; }
    public int KitapKodu { get; set; }

    public Kullanici? oduncAlan { get; set; }// burda ödünç alınan kullanıcıyı tutmak için Kullanici tipinde bir değişken tanımladım

    public void KitapBilgileriniYazdir()
    {
        Console.WriteLine($"Kitap Adı   : {KitapAdi}");
        Console.WriteLine($"Yazar       : {Yazar}");
        Console.WriteLine($"Kitap kodu  : {KitapKodu}");
        if (oduncAlan != null)
        {
            Console.WriteLine($"Durum:  {oduncAlan.Ad} {oduncAlan.Soyad} tarafından ödünç alınmış.");
        }
        else
        {
            Console.WriteLine($"Durum:   Kitap rafta  mevcut.");
        }
    }
}
 