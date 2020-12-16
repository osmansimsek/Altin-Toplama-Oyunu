using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Threading;

namespace AltinToplamaOyunu
{
    class OyunBitisLabel : Label
    {
        // oyun bittikten sonra bir tablo şeklinde toplam adim sayisi, harcanan altin sayisi,
        // kasadaki altin sayisi, toplanan altın sayısı gösterilmektedir.
        private OyunAnaLabel oyunAnaLabel;
        private int tabanGenisligi;
        private int tabanYuksekligi;

        public OyunBitisLabel(int tabanGenisligi, int tabanYuksekligi, OyunAnaLabel oyunAnaLabel)
        {
            this.oyunAnaLabel = oyunAnaLabel;
            this.tabanGenisligi = tabanGenisligi;
            this.tabanYuksekligi = tabanYuksekligi;

            OyunBitisLabelComponentOlustur();
        }

        private void OyunBitisLabelComponentOlustur()
        {
            BaslikOlustur();

            OyuncuIsimleriOlustur("A Oyuncusu", 250, 150, Color.Tomato);
            OyuncuIsimleriOlustur("B Oyuncusu", 400, 150, Color.Green);
            OyuncuIsimleriOlustur("C Oyuncusu", 550, 150, Color.DodgerBlue);
            OyuncuIsimleriOlustur("D Oyuncusu", 700, 150, Color.BlueViolet);

            KategoriIsimOlustur("Toplam Adim Sayisi :", 0, 200);
            KategoriIsimOlustur("Harcanan Altin Miktari :", 0, 250);
            KategoriIsimOlustur("Kasadaki Altin Miktari :", 0, 300);
            KategoriIsimOlustur("Toplanan Altin Miktari :", 0, 350);

            ToplamAdimSayisiOlustur(oyunAnaLabel.oyuncular[0].toplamAdimMiktari, 250, 200, Color.Tomato);
            ToplamAdimSayisiOlustur(oyunAnaLabel.oyuncular[1].toplamAdimMiktari, 400, 200, Color.Green);
            ToplamAdimSayisiOlustur(oyunAnaLabel.oyuncular[2].toplamAdimMiktari, 550, 200, Color.DodgerBlue);
            ToplamAdimSayisiOlustur(oyunAnaLabel.oyuncular[3].toplamAdimMiktari, 700, 200, Color.BlueViolet);

            HarcananAltinMiktariOlustur(oyunAnaLabel.oyuncular[0].harcananAltinMiktari, 250, 250, Color.Tomato);
            HarcananAltinMiktariOlustur(oyunAnaLabel.oyuncular[1].harcananAltinMiktari, 400, 250, Color.Green);
            HarcananAltinMiktariOlustur(oyunAnaLabel.oyuncular[2].harcananAltinMiktari, 550, 250, Color.DodgerBlue);
            HarcananAltinMiktariOlustur(oyunAnaLabel.oyuncular[3].harcananAltinMiktari, 700, 250, Color.BlueViolet);

            KasadakiAltinMiktariOlustur(oyunAnaLabel.oyuncular[0].baslangicAltinMiktari, 250, 300, Color.Tomato);
            KasadakiAltinMiktariOlustur(oyunAnaLabel.oyuncular[1].baslangicAltinMiktari, 400, 300, Color.Green);
            KasadakiAltinMiktariOlustur(oyunAnaLabel.oyuncular[2].baslangicAltinMiktari, 550, 300, Color.DodgerBlue);
            KasadakiAltinMiktariOlustur(oyunAnaLabel.oyuncular[3].baslangicAltinMiktari, 700, 300, Color.BlueViolet);

            ToplananAltinMiktariOlustur(oyunAnaLabel.oyuncular[0].toplananAltinMiktari, 250, 350, Color.Tomato);
            ToplananAltinMiktariOlustur(oyunAnaLabel.oyuncular[1].toplananAltinMiktari, 400, 350, Color.Green);
            ToplananAltinMiktariOlustur(oyunAnaLabel.oyuncular[2].toplananAltinMiktari, 550, 350, Color.DodgerBlue);
            ToplananAltinMiktariOlustur(oyunAnaLabel.oyuncular[3].toplananAltinMiktari, 700, 350, Color.BlueViolet);

            TekrarOynaButtonuOlustur();
            CıkısButtonuOlustur();
        }

        private void BaslikOlustur()
        {
            Label label = new Label();
            label.Size = new Size(tabanGenisligi, tabanYuksekligi / 6);
            label.Location = new Point(0, 0);
            label.Font = new Font("Arial", 25, FontStyle.Bold);
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Text = "Oyun Bitti...";
            this.Controls.Add(label);
        }

        private void OyuncuIsimleriOlustur(string metin, int x, int y, Color color)
        {
            Label label = new Label();
            label.Size = new Size(150, 50);
            label.Location = new Point(x, y);
            label.Text = metin;
            label.Font = new Font("Arial", 15, FontStyle.Bold);
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.ForeColor = color;
            this.Controls.Add(label);
        }

        private void KategoriIsimOlustur(string metin, int x, int y)
        {
            Label label = new Label();
            label.Size = new Size(250, 50);
            label.Location = new Point(x, y);
            label.Text = metin;
            label.Font = new Font("Arial", 14, FontStyle.Bold);
            label.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(label);
        }

        private void ToplamAdimSayisiOlustur(int metin, int x, int y, Color color)
        {
            Label label = new Label();
            label.Size = new Size(150, 50);
            label.Location = new Point(x, y);
            label.Text = metin.ToString();
            label.Font = new Font("Arial", 15, FontStyle.Bold);
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.ForeColor = color;
            this.Controls.Add(label);
        }

        private void HarcananAltinMiktariOlustur(int metin, int x, int y, Color color)
        {
            Label label = new Label();
            label.Size = new Size(150, 50);
            label.Location = new Point(x, y);
            label.Text = metin.ToString();
            label.Font = new Font("Arial", 15, FontStyle.Bold);
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.ForeColor = color;
            this.Controls.Add(label);
        }

        private void KasadakiAltinMiktariOlustur(int metin, int x, int y, Color color)
        {
            Label label = new Label();
            label.Size = new Size(150, 50);
            label.Location = new Point(x, y);
            label.Text = metin.ToString();
            label.Font = new Font("Arial", 15, FontStyle.Bold);
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.ForeColor = color;
            this.Controls.Add(label);
        }

        private void ToplananAltinMiktariOlustur(int metin, int x, int y, Color color)
        {
            Label label = new Label();
            label.Size = new Size(150, 50);
            label.Location = new Point(x, y);
            label.Text = metin.ToString();
            label.Font = new Font("Arial", 15, FontStyle.Bold);
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.ForeColor = color;
            this.Controls.Add(label);
        }

        private void TekrarOynaButtonuOlustur()
        {
            Button button = new Button();
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Arial", 16F, FontStyle.Regular);
            button.Location = new Point(50, 450);
            button.Size = new Size(200, 50);
            button.Text = "Yeniden Başlat";
            button.UseVisualStyleBackColor = true;
            button.Click += new System.EventHandler(this.yenidenBaslat );
            this.Controls.Add(button);
        }

        private void CıkısButtonuOlustur()
        {
            Button button = new Button();
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Arial", 16F, FontStyle.Regular);
            button.Location = new Point(300, 450);
            button.Size = new Size(200, 50);
            button.Text = "Çıkış Yap";
            button.UseVisualStyleBackColor = true;
            button.Click += new System.EventHandler(this.cıkısYap);
            this.Controls.Add(button);
        }

        private void cıkısYap(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void yenidenBaslat(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
