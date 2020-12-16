using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AltinToplamaOyunu
{
    class Altin
    {
        public int altinSayisi;
        public int[,] altinMatris;
        public int[,] degerMatris;
        protected Random random;

        public Altin()
        {
            this.altinMatris = new int[AnaForm.parametre.boyutY, AnaForm.parametre.boyutX];
            this.degerMatris = new int[AnaForm.parametre.boyutY, AnaForm.parametre.boyutX];
            this.altinSayisi = AnaForm.parametre.boyutX * AnaForm.parametre.boyutY * AnaForm.parametre.altinOrani / 100;
            this.random = new Random();

            MatrisDoldur();
            altinOlustur(altinSayisi);
            AltinDegerOlustur(altinSayisi);
        }

        // Oyunda parametre olarak girilen boyutlardan oluşan bir matris hazırlanıp
        // bu matrisin içinde altınlar rastgele yerleştirilip 1 rakamı konmaktadır.
        // köşelere asla altın gelemez. Altınlarında degerleri random olarak verilmektedir.
        public void altinOlustur(int altinSayisi)
        {
            int altinSayac = 0;

            while (true)
            {
                int x = 0 + random.Next(AnaForm.parametre.boyutY);
                int y = 0 + random.Next(AnaForm.parametre.boyutX);

                if (altinMatris[x, y] == 1 || altinMatris[x, y] == -1 || altinMatris[x, y] == -2 ||
                    altinMatris[x, y] == -3 || altinMatris[x, y] == -4)
                {
                    continue;
                }

                else if (altinSayac == altinSayisi)
                {
                    break;
                }

                else
                {
                    altinMatris[x, y] = 1;
                    altinSayac++;
                }
            }
        }

        // Altınların degerlerinin random olarak verildiği kısımdır
        private void AltinDegerOlustur(int altinSayisi)
        {
            int[] altinDegerleri = { 5, 10, 15, 20 };

            for (int i = 0; i < AnaForm.parametre.boyutY; i++)
            {
                for (int j = 0; j < AnaForm.parametre.boyutX; j++)
                {
                    if (altinMatris[i, j] == 1)
                    {
                        degerMatris[i, j] = altinDegerleri[0 + random.Next(4)];
                    }

                    else
                    {
                        degerMatris[i, j] = 0;
                    }
                }
            }
        }

        // köşelere altın gelmemesi için matrisi o şekilde doldurma işlevi görmektedir.
        protected void MatrisDoldur()
        {
            for (int i = 0; i < AnaForm.parametre.boyutY; i++)
            {
                for (int j = 0; j < AnaForm.parametre.boyutX; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        altinMatris[i, j] = -1;
                    }

                    else if (i == 0 && j == AnaForm.parametre.boyutX - 1)
                    {
                        altinMatris[i, j] = -2;
                    }

                    else if (i == AnaForm.parametre.boyutY - 1 && j == 0)
                    {
                        altinMatris[i, j] = -3;
                    }

                    else if (i == AnaForm.parametre.boyutX - 1 && j == AnaForm.parametre.boyutY - 1)
                    {
                        altinMatris[i, j] = -4;
                    }

                    else
                    {
                        altinMatris[i, j] = 0;
                    }
                }
            }
        }
    }
}
