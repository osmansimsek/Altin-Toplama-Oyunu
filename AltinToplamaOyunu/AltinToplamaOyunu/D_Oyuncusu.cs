using System.Collections.Generic;
using System.Drawing;
using System;

namespace AltinToplamaOyunu
{
    class D_Oyuncusu : Oyuncu
    {
        public D_Oyuncusu(Altin altin, GizliAltin gizliAltin, List<List<Block>> grid, int konumY, int konumX)
        {
            this.oyuncuRengi = Color.BlueViolet;
            this.altin = altin;
            this.gizliAltin = gizliAltin;
            this.grid = grid;
            this.konum = (konumX, konumY);
            this.hedef = (konumX, konumY);
            this.hamleMaliyet = AnaForm.parametre.d_OyuncuHamleMaliyet;
            this.hedefMaliyet = AnaForm.parametre.d_OyuncuHedefMaliyet;
            this.oyuncuNumarasi = -4;
            this.oyuncuIsmi = "D";
        }

        // D oyuncusuna ait hedef belirleme işlemi yazılmıştır
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
                        int kontrol = 0;
                        foreach (var oyuncu in oyuncular)
                        {
                            if (oyuncu.hedef.x == j && oyuncu.hedef.y == i && oyuncu.oyuncuNumarasi != -4)
                            {
                                int konum1 = Math.Abs(i - this.konum.y) + Math.Abs(j - this.konum.x);
                                int konum2 = Math.Abs(i - oyuncu.konum.y) + Math.Abs(j - oyuncu.konum.x);
                                if (konum1 > konum2)
                                {
                                    kontrol = 1;
                                }
                            }
                        }

                        if (kontrol == 0)
                        {
                            int deger = (Math.Abs(i - konum.y) + Math.Abs(j - konum.x)) * hamleMaliyet;
                            hedefler.Add((j, i, altin.degerMatris[i, j] - deger));
                        }
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
        }
    }
}