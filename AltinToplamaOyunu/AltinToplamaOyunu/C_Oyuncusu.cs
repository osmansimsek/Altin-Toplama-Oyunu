using System.Collections.Generic;
using System.Drawing;
using System;

namespace AltinToplamaOyunu
{
    class C_Oyuncusu : Oyuncu
    {
        public C_Oyuncusu(Altin altin, GizliAltin gizliAltin, List<List<Block>> grid, int konumY, int konumX)
        {
            this.oyuncuRengi = Color.DodgerBlue;
            this.altin = altin;
            this.gizliAltin = gizliAltin;
            this.grid = grid;
            this.konum = (konumX, konumY);
            this.hedef = (konumX, konumY);
            this.hamleMaliyet = AnaForm.parametre.c_OyuncuHamleMaliyet;
            this.hedefMaliyet = AnaForm.parametre.c_OyuncuHedefMaliyet;
            this.oyuncuNumarasi = -3;
            this.oyuncuIsmi = "C";
        }

        // C oyuncusuna ait hedef belirleme işlemi yazılmıştır
        public override void hedefBelirle(List<Oyuncu> oyuncular)
        {
            List<(int hedefX, int hedefY, int maliyet)> hedefler;
            hedefler = new List<(int hedefX, int hedefY, int maliyet)>();

            for (int i = 0; i < AnaForm.parametre.boyutY; i++)
            {
                for (int j = 0; j < AnaForm.parametre.boyutX; j++)
                {
                    if (altin.altinMatris[i, j] == 1)
                    {
                        int deger = (Math.Abs(i - konum.y) + Math.Abs(j - konum.x)) * hamleMaliyet;
                        hedefler.Add((j, i, altin.degerMatris[i, j] - deger));
                    }
                }
            }

            double enBuyuk = Double.NegativeInfinity;

            foreach ((int hedefX, int hedefY, int deger) hedef in hedefler)
            {
                if (enBuyuk < hedef.deger)
                {
                    enBuyuk = hedef.deger;
                    this.hedef = (hedef.hedefX, hedef.hedefY);
                }
            }

            hedefler.Clear();
        }

        public void gizliAltinAc()
        {
            List<(int hedefX, int hedefY, double Uzaklik)> gizliAltinlar;
            gizliAltinlar = new List<(int hedefX, int hedefY, double Uzaklik)>();

            for (int i = 0; i < AnaForm.parametre.boyutY; i++)
            {
                for (int j = 0; j < AnaForm.parametre.boyutX; j++)
                { 
                    if (altin.altinMatris[i, j] == 2)
                    {
                        gizliAltinlar.Add((j, i, Math.Sqrt(Math.Pow(Math.Abs(j - konum.x), 2) +
                                                           Math.Pow(Math.Abs(i - konum.y), 2))));
                    }
                }
            }


            for (int i = 0; i < AnaForm.parametre.gizliAltinAcmaSayisi; i++)
            {
                double enKucuk = Double.PositiveInfinity;
                int x = konum.x, y = konum.y, index = -1;
                for (int j = 0; j < gizliAltinlar.Count; j++)
                {
                    if (enKucuk > gizliAltinlar[j].Uzaklik)
                    {
                        enKucuk = gizliAltinlar[j].Uzaklik;
                        x = gizliAltinlar[j].hedefX;
                        y = gizliAltinlar[j].hedefY;
                        index = j;
                    }
                }
                    
                if (x != konum.x || y != konum.y || index != -1)
                {
                    altin.altinMatris[y, x] = 1;
                    gizliAltin.gizliAltinSayisi--;
                    altin.altinSayisi++;
                    gizliAltinlar.RemoveAt(index); 
                }
            }

            gizliAltinlar.Clear();
        }
    }
}
