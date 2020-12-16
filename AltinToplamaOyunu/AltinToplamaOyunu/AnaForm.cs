using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AltinToplamaOyunu
{
    public partial class AnaForm : Form
    {
        // Oyunumuzdaki genel sınıfların tanımlandığı yer
        OyunAnaLabel oyunAnaLabel;
        OyunBitisLabel oyunBitisLabel;

        // Oyunda yer alan parametrelerin tanımlandığı yer
        public static (int boyutX, int boyutY, int altinOrani, int gizliAltinOrani,
        int baslangicAltinMiktari, int adimSayisi, int a_OyuncuHamleMaliyet,
        int a_OyuncuHedefMaliyet, int b_OyuncuHamleMaliyet, int b_OyuncuHedefMaliyet,
        int c_OyuncuHamleMaliyet, int c_OyuncuHedefMaliyet, int gizliAltinAcmaSayisi,
        int d_OyuncuHamleMaliyet, int d_OyuncuHedefMaliyet) parametre;

        public AnaForm()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(50, 50, 50);

            // Parametrelere varsayılan degerler atanır
            parametre = (20, 20, 20, 10, 200, 3, 5, 5, 5, 10, 5, 15, 2, 5, 20);
        }

        private void btnBasla_Click(object sender, EventArgs e)
        {
            // Başla tuşuna basıldığı zaman formun özellikleri değiştirilir
            this.Controls.Remove(this.BaslangicPanel);
            this.WindowState = FormWindowState.Maximized;
            BaslangicAdimlari();
        }

        private void BaslangicAdimlari()
        {
            VarsayilanDegerControl();

            // oyunAnaLabel yani oyunun görsel olarak gösterildiği kısım forma eklenir
            oyunAnaLabel = new OyunAnaLabel(this.ClientSize.Height, this.ClientSize.Width);
            oyunAnaLabel.VisibleChanged += OyunAnaLabel_VisibleChanged;
            this.Controls.Add(oyunAnaLabel);
        }

        private void OyunAnaLabel_VisibleChanged(object sender, EventArgs e)
        {
            // oyun bittiği zaman oyunAnaLabel'ın visible kapatılır ve event tetiklenir
            // sonrasında sonuç ekranı ortaya çıkartılır
            this.Controls.Clear();
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1020, 596);
            oyunBitisLabel = new OyunBitisLabel(this.ClientSize.Width, this.ClientSize.Height, oyunAnaLabel);
            oyunBitisLabel.Dock = DockStyle.Fill;
            this.Controls.Add(oyunBitisLabel);
        }

        private void VarsayilanDegerControl()
        {
            // Her bir parametre için varsayılan degerler yerine yeni degerler
            // girilip girilmediği kontrolü yapılmaktadır
            ParametreKontrol(this.txtBoyutX.Text, ref parametre.boyutX);
            ParametreKontrol(this.txtBoyutY.Text, ref parametre.boyutY);
            ParametreKontrol(this.txtAltinOrani.Text, ref parametre.altinOrani);
            ParametreKontrol(this.txtGizliAltinOrani.Text, ref parametre.gizliAltinOrani);
            ParametreKontrol(this.txtBaslangicAltinMiktari.Text, ref parametre.baslangicAltinMiktari);
            ParametreKontrol(this.txtAdimSayisiMiktari.Text, ref parametre.adimSayisi);
            ParametreKontrol(this.txtAHamle.Text, ref parametre.a_OyuncuHamleMaliyet);
            ParametreKontrol(this.txtAHedef.Text, ref parametre.a_OyuncuHedefMaliyet);
            ParametreKontrol(this.txtBHamle.Text, ref parametre.b_OyuncuHamleMaliyet);
            ParametreKontrol(this.txtBHedef.Text, ref parametre.b_OyuncuHedefMaliyet);
            ParametreKontrol(this.txtCHamle.Text, ref parametre.c_OyuncuHamleMaliyet);
            ParametreKontrol(this.txtCHedef.Text, ref parametre.c_OyuncuHedefMaliyet);
            ParametreKontrol(this.txtgizliAltınAcmaSayisi.Text, ref parametre.gizliAltinAcmaSayisi);
            ParametreKontrol(this.txtDHamle.Text, ref parametre.d_OyuncuHamleMaliyet);
            ParametreKontrol(this.txtDHedef.Text, ref parametre.d_OyuncuHedefMaliyet);

        }

        public static void ParametreKontrol(string gelenDeger, ref int deger)
        {
            // Parametrelerin kontrolünü yapan algoritma
            if (!(gelenDeger == ""))
            {
                int number;
                if (int.TryParse(gelenDeger, out number))
                {
                    deger = number;
                }
            }
        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            // Oyundan çıkış yapılmaktadır
            Application.Exit();
        }
    }
}
