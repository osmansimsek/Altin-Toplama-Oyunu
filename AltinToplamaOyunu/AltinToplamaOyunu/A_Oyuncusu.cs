using System;
using System.Collections.Generic;
using System.Drawing;

namespace AltinToplamaOyunu
{
    class A_Oyuncusu : Oyuncu
    {
        public A_Oyuncusu(Altin altin, GizliAltin gizliAltin, List<List<Block>> grid, int konumY, int konumX)
        {
            this.oyuncuRengi = Color.Tomato;
            this.altin = altin;
            this.gizliAltin = gizliAltin;
            this.grid = grid;
            this.konum = (konumX, konumY);
            this.hedef = (konumX, konumY);
            this.hamleMaliyet = AnaForm.parametre.a_OyuncuHamleMaliyet;
            this.hedefMaliyet = AnaForm.parametre.a_OyuncuHedefMaliyet;
            this.oyuncuNumarasi = -1;
            this.oyuncuIsmi = "A";
        }
        
        // a oyuncusuna ait hedef belirleme işlemi yazılmıştır
        public override void hedefBelirle(List<Oyuncu> oyuncular)
        {
            List<(int hedefX, int hedefY, double Uzaklik)> hedefler;
            hedefler = new List<(int hedefX, int hedefY, double Uzaklik)>();

            for (int i = 0; i < AnaForm.parametre.boyutY; i++)
            {
                for (int j = 0; j < AnaForm.parametre.boyutX; j++)
                {
                    if (altin.altinMatris[i, j] == 1)
                    {
                        hedefler.Add((j, i, Math.Sqrt(Math.Pow(Math.Abs(j - konum.x), 2) + 
                                                      Math.Pow(Math.Abs(i - konum.y), 2))));
                    }
                }
            }

            double enKucuk = Double.PositiveInfinity;

            foreach ((int hedefX, int hedefY, double Uzaklik) hedef in hedefler)
            {
                if (enKucuk > hedef.Uzaklik)
                {
                    enKucuk = hedef.Uzaklik;
                    this.hedef = (hedef.hedefX, hedef.hedefY);
                }
            }

            hedefler.Clear();
        }
    }
}
