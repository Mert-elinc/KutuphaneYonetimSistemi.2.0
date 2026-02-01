using System;

namespace KayitSistemi
{
    public class Kullanici
    {
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public long TC { get; set; }
    
public void BilgileriYazdir()
{
    Console.WriteLine($"ÜYE : {this.Ad}  {this.Soyad}  Hoşgeldiniz Kütüphanemize.");
}
}
}


    
