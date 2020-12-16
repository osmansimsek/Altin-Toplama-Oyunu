using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AltinToplamaOyunu
{
    class GizliAltin : Altin
    {
        public int gizliAltinSayisi;
        private Altin altin;
        public GizliAltin(Altin altin)
        {
            this.altin = altin;
            gizliAltinSayisi = altin.altinSayisi * AnaForm.parametre.gizliAltinOrani / 100;

            gizliAltinOlustur(gizliAltinSayisi);
        }

        // Altın matrisi oluşturulduktan sonra o matrisde altınların bazılarını kapatarak
        // gizli altın oluşturulmaktadır.
        private void gizliAltinOlustur(int gizliAltinSayisi)
        {
            int gizliAltinSayac = 0;
            
            while (true)
            {
                int x = 0 + random.Next(AnaForm.parametre.boyutY);
                int y = 0 + random.Next(AnaForm.parametre.boyutX);

                if ((altin.altinMatris[x, y] == 1) && (gizliAltinSayac != gizliAltinSayisi))
                {
                    altin.altinMatris[x, y] = 2;
                    gizliAltinSayac++;
                }
                else if (gizliAltinSayac == gizliAltinSayisi)
                {
                    break;
                }
            }
        }
    }
}
