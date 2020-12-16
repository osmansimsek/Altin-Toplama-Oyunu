using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;

namespace AltinToplamaOyunu
{
    class OyunAnaLabel : Label
    {
        // oyunAnaLabel da oyunda teorik olarak önceden tanımlanmış 
        // hazırlanmış verilerin görselleştirilmesi çizdirilmesinden sorumlu classtır
        // her bir oyuncu hareket ettiğinde ya da altın azaldığında haritayı tekrar günceller
        public Taban taban;
        public Altin altin;
        public GizliAltin gizliAltin;
        public List<Oyuncu> oyuncular;
        public DispatcherTimer anaTimer;
        private CizimYonetimi cizimYonetimi;
        private List<Dosya> dosyalar;
        private int hamleSirasi = 0;
        private int tabanYuksekligi;
        private int tabanGenisligi;
        private int[] turlar;
        private int turSayac = 0;

        public OyunAnaLabel(int tabanYuksekligi, int tabanGenisligi)
        {
            this.turlar = new int[] { 1, 1, 1, 1 };
            this.tabanGenisligi = tabanGenisligi;
            this.tabanYuksekligi = tabanYuksekligi;

            // oyunun taban kısmı teorik olarak oluşturulur
            this.taban = new Taban(tabanYuksekligi, tabanGenisligi); 

            // oyunun altin kısmı teorik olarak oluşturulur
            this.altin = new Altin();

            // oyunun gizliAltin kısmı teorik olarak oluşturulur
            this.gizliAltin = new GizliAltin(altin);
            altin.altinSayisi -= gizliAltin.gizliAltinSayisi;

            // oyunda yer alan 4 farklı oyuncu bir listenin içinde polymorghism kullanılarak oluşturulur
            this.oyuncular = oyuncular = new List<Oyuncu>()
            {
                { new A_Oyuncusu(altin, gizliAltin, taban.grid, 0, 0) },
                { new B_Oyuncusu(altin, gizliAltin, taban.grid, 0, AnaForm.parametre.boyutX - 1) },
                { new C_Oyuncusu(altin, gizliAltin, taban.grid, AnaForm.parametre.boyutY - 1, 0) },
                { new D_Oyuncusu(altin, gizliAltin, taban.grid, AnaForm.parametre.boyutY - 1, AnaForm.parametre.boyutX - 1) },
            };

            // oyunda ki her oyuncu için kayıtlar tutulması için dosyalar oluşturulur
            this.dosyalar = dosyalar = new List<Dosya>()
            {
                { new Dosya("A_Oyuncu_Kayitlari") },
                { new Dosya("B_Oyuncu_Kayitlari") },
                { new Dosya("C_Oyuncu_Kayitlari") },
                { new Dosya("D_Oyuncu_Kayitlari") }
            };

            // oluşturulan dosyalara başlıklar eklenir
            foreach (var dosya in dosyalar)
            {
                dosya.DosyaYazdır("Oyun Başladı...");
            }

            this.cizimYonetimi = new CizimYonetimi();
            this.Paint += new PaintEventHandler(this.oyunAnaLabelCizdir);
            this.Dock = DockStyle.Fill;

            anaTimer = new DispatcherTimer();
            anaTimer.Interval = new TimeSpan(0, 0, 0, 0, 400); // 400 Milliseconds
            anaTimer.Tick += new EventHandler(this.anaTimer_Tick);

            anaTimer.Start();
        }

        // oyunun oynanıcağı alanı oluşturur ve oyunla ilgili herhangi bir değişiklik
        // olduğunda tekrar çizdirerek yeni halini çizdirir

        public void oyunAnaLabelCizdir(object sender, PaintEventArgs args)
        {
            cizimYonetimi.tabanCiz(args, taban.grid);
            cizimYonetimi.altinCiz(args, taban.grid, altin);
            cizimYonetimi.gizliAltinCiz(args, taban.grid, altin);
            foreach (var oyuncu in oyuncular)
            {
                cizimYonetimi.oyuncuCiz(oyuncu, args, taban.grid, oyuncu.oyuncuIsmi);
                cizimYonetimi.hedefGostergeCiz(oyuncu, args, taban.grid);
            }
        }

        // timer kullanrak oyunda 400 milisecond da çalışmasını sağladık ve
        // oyundaki gerçekleşmesi gerekne her durumu ve adımı tek tek yazdık

        public void anaTimer_Tick(object sender, EventArgs eventArgs)
        {
            // oyunun devam etmesi için gereken koşullar
            if (altin.altinSayisi != 0 && (oyuncular[0].baslangicAltinMiktari != 0 ||
                oyuncular[1].baslangicAltinMiktari != 0 || oyuncular[2].baslangicAltinMiktari != 0 ||
                oyuncular[3].baslangicAltinMiktari != 0))
            {

                // her oyuncu için tur başı burasıdır ve tur başında yapılması gereken adımlar yapılır.
                if (turSayac == 0)
                {
                    // her tur başında dosyaya tur bilgisi ile ilgili metin yazdırılır
                    dosyalar[hamleSirasi].DosyaYazdır("\n " + turlar[hamleSirasi] + ". Tur\n");
                    turlar[hamleSirasi]++;
                    turSayac++;
                }

                hedefBelirlemeAsamasi();
                this.Refresh();
                hareketEtmeAsamasi();
                this.Refresh();
            }

            // oyunun bitmesi için altınların bitmesi gerekir
            else if (altin.altinSayisi == 0)
            {
                foreach (var dosya in dosyalar)
                {
                    dosya.DosyaYazdır("\nOyun Bitti...");
                    dosya.streamWriter.Close();
                    dosya.fs.Close();
                }

                this.Hide();
                anaTimer.Stop();
            }

            // oyunun bitmesi için oyuncuların altınlarının bitmesi gerekir
            else if (oyuncular[0].baslangicAltinMiktari == 0 && oyuncular[1].baslangicAltinMiktari == 0 &&
                     oyuncular[2].baslangicAltinMiktari == 0 && oyuncular[3].baslangicAltinMiktari == 0)
            {
                foreach (var dosya in dosyalar)
                {
                    dosya.DosyaYazdır("\nOyun Bitti...");
                    dosya.streamWriter.Close();
                    dosya.fs.Close();
                }

                this.Hide();
                anaTimer.Stop();
            }
        }

        private void hedefBelirlemeAsamasi()
        {
            // oyuncunun hedefi yoksa ama altını varsa
            if (altin.altinMatris[oyuncular[hamleSirasi].hedef.y, oyuncular[hamleSirasi].hedef.x] != 1 &&
                oyuncular[hamleSirasi].baslangicAltinMiktari >= oyuncular[hamleSirasi].hedefMaliyet)
            {
                // hamle sırasi c oyuncusunda ise gizli altın açması sağlanır
                if (oyuncular[hamleSirasi].oyuncuNumarasi == -3)
                {
                    C_Oyuncusu c_oyuncusu = oyuncular[hamleSirasi] as C_Oyuncusu;
                    c_oyuncusu.gizliAltinAc();
                    this.Refresh();
                }

                // hedef belirleme aşamasıdır
                oyuncular[hamleSirasi].hedefBelirle(oyuncular);

                // oyunculardan hedef maliyetinin düşürüldüğü yer
                oyuncular[hamleSirasi].baslangicAltinMiktari -= oyuncular[hamleSirasi].hedefMaliyet;
                oyuncular[hamleSirasi].harcananAltinMiktari += oyuncular[hamleSirasi].hedefMaliyet;

                // oyuncu hedefe vardığında kayıt alır
                dosyalar[hamleSirasi].DosyaYazdır("Hedef belirlendi. Hedef " + "X: " + oyuncular[hamleSirasi].hedef.x +
                                                  " Y: " + oyuncular[hamleSirasi].hedef.y);
                dosyalar[hamleSirasi].DosyaYazdır("Hedefe Olan birim uzaklık : " +
                                                 (Math.Abs(oyuncular[hamleSirasi].konum.x - oyuncular[hamleSirasi].hedef.x) +
                                                 Math.Abs(oyuncular[hamleSirasi].konum.y - oyuncular[hamleSirasi].hedef.y)));

                dosyalar[hamleSirasi].DosyaYazdır("Hedefteki altın miktari :" +
                                                 altin.degerMatris[oyuncular[hamleSirasi].hedef.y, oyuncular[hamleSirasi].hedef.x]);
                dosyalar[hamleSirasi].DosyaYazdır("Hedef belirleme maliyeti : " + oyuncular[hamleSirasi].hedefMaliyet);
                dosyalar[hamleSirasi].DosyaYazdır("Hamle belirleme maliyeti : " + oyuncular[hamleSirasi].hamleMaliyet);
                dosyalar[hamleSirasi].DosyaYazdır("Oyuncu kalan altın miktari : " + oyuncular[hamleSirasi].baslangicAltinMiktari);

                this.Refresh();
                Thread.Sleep(400);
            }

            // oyuncunun hedefi ve altını yoksa
            else if (altin.altinMatris[oyuncular[hamleSirasi].hedef.y, oyuncular[hamleSirasi].hedef.x] != 1
                     && oyuncular[hamleSirasi].baslangicAltinMiktari < oyuncular[hamleSirasi].hedefMaliyet)
            {

                dosyalar[hamleSirasi].DosyaYazdır("Oyuncunun altını bittiğinden dolayı hedef belirleyemez");

                // hamle sırası diğer oyuncuya verilir ve hedef olarak kendini gösterir
                oyuncular[hamleSirasi].hedef = (oyuncular[hamleSirasi].konum.x, oyuncular[hamleSirasi].konum.y);

                if (hamleSirasi < 3)
                {
                    hamleSirasi++;
                }

                else
                {
                    hamleSirasi = 0;
                }
            }
        }

        private void hareketEtmeAsamasi()
        {
            // oyuncunun bir hedefi varsa eğer öyle hareket edebilir
            if (altin.altinMatris[oyuncular[hamleSirasi].hedef.y, oyuncular[hamleSirasi].hedef.x] == 1)
            {
                if (oyuncular[hamleSirasi].baslangicAltinMiktari >= oyuncular[hamleSirasi].hamleMaliyet)
                {
                    // oyuncunun adım sayisi varsa
                    if (oyuncular[hamleSirasi].adimSayisi > 0)
                    {
                        // hareket etme işlemi gerçekleşmektedir. 
                        oyuncular[hamleSirasi].hareketET(dosyalar[hamleSirasi]);

                        if (oyuncular[hamleSirasi].adimSayisi != 0)
                        {
                            oyuncular[hamleSirasi].adimSayisi--;
                        }

                        this.Refresh();
                    }

                    // oyuncunun adım sayisi bittiyse ve tur bitiminin olduğu yer
                    else if (oyuncular[hamleSirasi].adimSayisi == 0)
                    {
                        // tur bitiminde oyuncudan hamle maliyeti kesilir
                        oyuncular[hamleSirasi].baslangicAltinMiktari -= oyuncular[hamleSirasi].hamleMaliyet;
                        oyuncular[hamleSirasi].harcananAltinMiktari += oyuncular[hamleSirasi].hamleMaliyet;

                        // adim sayisini tekrardan sıfırlayıp bir diğer oyuncuya geçer hamle sırası
                        oyuncular[hamleSirasi].adimSayisi = AnaForm.parametre.adimSayisi;

                        if (hamleSirasi < 3)
                        {
                            hamleSirasi++;
                        }

                        else
                        {
                            hamleSirasi = 0;
                        }

                        turSayac = 0;
                    }
                }

                // oyunucunun parası bittiyse 
                else if (oyuncular[hamleSirasi].baslangicAltinMiktari < oyuncular[hamleSirasi].hamleMaliyet)
                {
                    Console.WriteLine("osman");
                    dosyalar[hamleSirasi].DosyaYazdır("Oyuncunun altını bittiğinden dolayı hareket edemez");

                    // altın miktarını 0 layıp hedef olarak oyuncunun kendi konumunu gösterir
                    oyuncular[hamleSirasi].baslangicAltinMiktari = 0;
                    oyuncular[hamleSirasi].hedef = (oyuncular[hamleSirasi].konum.x, oyuncular[hamleSirasi].konum.y);

                    this.Refresh();

                    if (hamleSirasi < 3)
                    {
                        hamleSirasi++;
                    }

                    else
                    {
                        hamleSirasi = 0;
                    }
                }
            }
        }
    }
}