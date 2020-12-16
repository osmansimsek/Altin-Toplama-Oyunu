using System;
using System.Collections.Generic;
using System.Drawing;

namespace AltinToplamaOyunu
{
    abstract class Oyuncu
    {
        // Her bir oyuncuya ait veriler değişkenler burda tutlmaktadır
        // her bir oyuncu oyuncu classından türetilmektedir.

        public Color oyuncuRengi;
        public List<List<Block>> grid;
        public Altin altin;
        public GizliAltin gizliAltin;
        public string oyuncuIsmi;
        public (int x, int y) konum;
        public (int x, int y) hedef;
        public int oyuncuNumarasi;
        public int adimSayisi;
        public int baslangicAltinMiktari;
        public int toplamAdimMiktari;
        public int harcananAltinMiktari;
        public int toplananAltinMiktari;
        public int hamleMaliyet;
        public int hedefMaliyet;
        private int iterator;

        public Oyuncu()
        {
            this.baslangicAltinMiktari = AnaForm.parametre.baslangicAltinMiktari;
            this.adimSayisi = AnaForm.parametre.adimSayisi;
            this.toplamAdimMiktari = 0;
            this.harcananAltinMiktari = 0;
            this.toplananAltinMiktari = 0;
            this.iterator = 0;
        }

        // oyuncuların hedef belirleme işlmeleri farklılık gösterdiğinden dolayı
        // abstract olarak tanımladık ve her bir oyuncu kendi classının iiçinde istediği
        // gibi methodu doldurabilmektedir.

        public abstract void hedefBelirle(List<Oyuncu> oyuncular);

        // oyuncuların hareket işlemleri karmaşık bir yapıya sahiptir.
        // ilk olarak hareket işleminde sol sağ yukarı ve aşşağıya gitme 
        // kontrolleri yapılır oyuncuların sınır dışına çıkmaması için 
        // önlem alınmıştır. oyuncular hedeflediği altına giderken
        // geçtikleri altını almaması ve gizli altından geçerken açması sağlanmıştır
        // oyuncular hedeflediği altına gidince alması da sağlanmıştır.

        public void hareketET(Dosya dosya)
        {
            if (hedef.x - konum.x > 0)
            {
                sagaGit(dosya);
            }
            else if (hedef.x - konum.x < 0)
            {
                solaGit(dosya);
            }
            else if (hedef.y - konum.y > 0)
            {
                asagiGit(dosya);
            }
            else if (hedef.y - konum.y < 0) yukariGit(dosya);
            toplamAdimMiktari++;
        }

        // üstünden geçilen her bir bloğun kontrolü yapılır

        private int BlockKontrol(int konumX, int konumY, int nokta, Dosya dosya)
        {
            if (konumX == hedef.x && konumY == hedef.y)
            {
                altin.altinSayisi--;
                toplananAltinMiktari += altin.degerMatris[hedef.y, hedef.x];
                baslangicAltinMiktari += altin.degerMatris[hedef.y, hedef.x];

                // hedefe ulaştığı zaman kayıt alıyor
                dosya.DosyaYazdır("Hedefe ulaşıldı : " + "x: " + konumX + " y: " + konumY);
                dosya.DosyaYazdır("Altın Toplandı. Toplanan miktar : " + altin.degerMatris[konumY, konumX]);
                dosya.DosyaYazdır("Oyuncu kalan altın miktari : " + baslangicAltinMiktari);

                adimSayisi = 0;
                return 0;
            }
            else if (nokta == 2)
            {
                gizliAltin.gizliAltinSayisi--;
                altin.altinSayisi++;

                // gizli altını açtığında kayıt alıyor
                dosya.DosyaYazdır("Gizli altın açıldı : " + "x: " + konumX + " y: " + konumY);
                dosya.DosyaYazdır("Açılan gizli altının miktari : " + altin.degerMatris[konumY, konumX]);

                return 1;
            }
            else
            {
                return nokta;
            }
        }

        private void solaGit(Dosya dosya)
        {
            if (konum.x > 0)
            {
                altin.altinMatris[konum.y, konum.x] = iterator;
                konum.x--;
                iterator = BlockKontrol(konum.x, konum.y, altin.altinMatris[konum.y, konum.x], dosya);
                altin.altinMatris[konum.y, konum.x] = oyuncuNumarasi;

                // oyuncu sola gittiği zaman kayıt alıyor
                dosya.DosyaYazdır("Sola hareket etti :" + "x: " + konum.x + " y: " + konum.y);
                dosya.DosyaYazdır("Oyuncu kalan altın miktari : " + baslangicAltinMiktari);
            }
        }

        private void sagaGit(Dosya dosya)
        {
            if (konum.x < 20)
            {
                altin.altinMatris[konum.y, konum.x] = iterator;
                konum.x++;
                iterator = BlockKontrol(konum.x, konum.y, altin.altinMatris[konum.y, konum.x], dosya);
                altin.altinMatris[konum.y, konum.x] = oyuncuNumarasi;

                // oyuncu sağa gittiği zaman kayıt alıyor
                dosya.DosyaYazdır("Sağa hareket etti : " + "x: " + konum.x + " y: " + konum.y);
                dosya.DosyaYazdır("Oyuncu kalan altın miktari : " + baslangicAltinMiktari);
            }
        }

        private void yukariGit(Dosya dosya)
        {
            if (konum.y > 0)
            {
                altin.altinMatris[konum.y, konum.x] = iterator;
                konum.y--;
                iterator = BlockKontrol(konum.x, konum.y, altin.altinMatris[konum.y, konum.x], dosya);
                altin.altinMatris[konum.y, konum.x] = oyuncuNumarasi;

                // oyuncu yukarı gittiği zaman kayıt alıyor
                dosya.DosyaYazdır("Yukari hareket etti : " + "x: " + konum.x + " y: " + konum.y);
                dosya.DosyaYazdır("Oyuncu kalan altın miktari : " + baslangicAltinMiktari);
            }
        }

        private void asagiGit(Dosya dosya)
        {
            if (konum.y < 20)
            {
                altin.altinMatris[konum.y, konum.x] = iterator;
                konum.y++;
                iterator = BlockKontrol(konum.x, konum.y, altin.altinMatris[konum.y, konum.x], dosya);
                altin.altinMatris[konum.y, konum.x] = oyuncuNumarasi;

                // oyuncu aşağı gittiği zaman kayıt alıyor
                dosya.DosyaYazdır("Aşağı hareket etti : " + "x: " + konum.x + " y: " + konum.y);
                dosya.DosyaYazdır("Oyuncu kalan altın miktari : " + baslangicAltinMiktari);
            }
        }
    }
}
