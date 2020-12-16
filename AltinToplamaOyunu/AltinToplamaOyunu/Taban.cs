using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AltinToplamaOyunu
{
    class Taban
    {
        private int tabanOlcegi;
        private int startPositionX;
        public List<List<Block>> grid;

        public Taban(int tabanOlcegi, int tabanGenisligi)
        {
            this.grid = new List<List<Block>>();
            this.startPositionX = (tabanGenisligi - tabanOlcegi) / 2;
            this.tabanOlcegi = tabanOlcegi;

            TabanOlustur();
        }

        // Oyunda Teorik olarak her biri block classından oluşan taban oluşturulmaktadır.
        private void TabanOlustur()
        {
            int x = startPositionX,
                y = (tabanOlcegi % AnaForm.parametre.boyutY) / 2,
                width = tabanOlcegi / AnaForm.parametre.boyutX,
                height = tabanOlcegi / AnaForm.parametre.boyutY;

            for (int i = 0; i < AnaForm.parametre.boyutY; i++)
            {
                List<Block> blocks = new List<Block>();

                for (int j = 0; j < AnaForm.parametre.boyutX; j++)
                {
                    Block block = new Block()
                    {
                        x = x,
                        y = y,
                        width = width,
                        heigth = height,
                    };
                    blocks.Add(block);
                    x += width;
                }

                grid.Add(blocks);
                x = startPositionX;
                y += height;
            }
        }
    }
}
