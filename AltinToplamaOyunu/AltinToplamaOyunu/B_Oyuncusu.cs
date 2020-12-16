using System;
using System.Collections.Generic;
using System.Drawing;

namespace AltinToplamaOyunu
{
    class B_Oyuncusu : Oyuncu
    {

        public B_Oyuncusu(Altin altin, GizliAltin gizliAltin, List<List<Block>> grid, int konumY, int konumX)
        {
            this.oyuncuRengi = Color.Green;
            this.hamleMaliyet = AnaForm.parametre.b_OyuncuHamleMaliyet;
            this.hedefMaliyet = AnaForm.parametre.b_OyuncuHedefMaliyet;
            this.altin = altin;
            this.gizliAltin = gizliAltin;
            this.grid = grid;
            this.konum = (konumX, konumY);
            this.hedef = (konumX, konumY);
            this.oyuncuNumarasi = -2;
            this.oyuncuIsmi = "B";
        }

        // B oyuncusuna ait hedef belirleme işlemi yazılmıştır
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
    }
}
