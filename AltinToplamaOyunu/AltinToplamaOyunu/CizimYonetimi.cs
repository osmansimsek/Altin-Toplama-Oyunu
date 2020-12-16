using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AltinToplamaOyunu
{
    class CizimYonetimi
    {
        // Oyundaki her bloğun çizdirilmesi için kullanılır
        private void blockCizdir(PaintEventArgs g, Color color, Block block)
        {
            block.rectangle = new Rectangle(block.x, block.y, block.width, block.heigth);
            g.Graphics.DrawRectangle(new Pen(color, 2), block.rectangle);
        }

        // Oyundaki her bloğun içinin boyanması için kullanılır
        private void blockBoya(PaintEventArgs g, Color color, Block block, string deger = "degerYok")
        {
            if (deger == "degerYok")
            {
                g.Graphics.FillRectangle(new SolidBrush(color), block.rectangle);
            }
            else
            {
                g.Graphics.FillRectangle(new SolidBrush(color), block.rectangle);
                g.Graphics.DrawString(deger,
                                      new Font("Arial", 10),
                                      new SolidBrush(Color.Black),
                                      block.rectangle,
                                      new StringFormat()
                                      {
                                          Alignment = StringAlignment.Center,
                                          LineAlignment = StringAlignment.Center
                                      });
            }
        }

        // oyundaki her biri bir block olan tabanın çizdirilmesi yapılır
        public void tabanCiz(PaintEventArgs g, List<List<Block>> grids)
        {
            foreach (List<Block> grid in grids)
            {
                foreach (Block block in grid)
                {
                    blockCizdir(g, Color.White, block);
                }
            }
        }

        // oyunda teorik olarak yerleştirilen altınlar daha sonrasında
        // görünür kılınması için burda çizilmektedir.
        public void altinCiz(PaintEventArgs args, List<List<Block>> grid, Altin altin)
        {
            for (int i = 0; i < AnaForm.parametre.boyutY; i++)
            {
                for (int j = 0; j < AnaForm.parametre.boyutX; j++)
                {
                    if (altin.altinMatris[i, j] == 1)
                    {
                        blockBoya(args, Color.Yellow, grid[i][j], altin.degerMatris[i, j].ToString());
                    }
                }
            }
        }

        // ooyundaki her bir gizli altının çizdirilme işlemi yapılmaktadır
        public void gizliAltinCiz(PaintEventArgs args, List<List<Block>> grid, Altin altin)
        {
            for (int i = 0; i < AnaForm.parametre.boyutY; i++)
            {
                for (int j = 0; j < AnaForm.parametre.boyutX; j++)
                {
                    if (altin.altinMatris[i, j] == 2)
                    {
                        blockBoya(args, Color.Orange, grid[i][j], altin.degerMatris[i, j].ToString());
                    }
                }
            }
        }

        // her bir oyuncunun çizidirilme işlemini gerçekleştirmektedir.
        public void oyuncuCiz(Oyuncu oyuncu, PaintEventArgs args, List<List<Block>> grid, string name)
        {
            blockBoya(args, oyuncu.oyuncuRengi, grid[oyuncu.konum.y][oyuncu.konum.x], name);
        }

        // her bir oyuncu hedef gösterdikten sonra oyunda hedefin hangisi olduğu
        // gösterilmesi için çizilmektedir.
        public void hedefGostergeCiz(Oyuncu oyuncu, PaintEventArgs args, List<List<Block>> grid)
        {
            blockCizdir(args, oyuncu.oyuncuRengi, grid[oyuncu.hedef.y][oyuncu.hedef.x]);
        }
    }
}
