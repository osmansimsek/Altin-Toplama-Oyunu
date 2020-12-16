using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltinToplamaOyunu
{
    class Dosya
    {
        string dosyaUzantisi;
        public FileStream fs;
        public StreamWriter streamWriter;

        // Dosyaya yazdırma işlerinin yapıldığı kısımdır
        // dosya sürekli açık tutulur ve kayıt alınması sağlanır
        // oyun bittiği zaman dosya kapatılır
        public Dosya(string dosyaAdi)
        {
            dosyaUzantisi = "..\\..\\..\\OyuncuKayitlari\\" + dosyaAdi + ".txt";
            fs = new FileStream(dosyaUzantisi, FileMode.Create, FileAccess.Write);
            streamWriter = new StreamWriter(fs);
        }

        public void DosyaYazdır(string metin)
        {
            if (File.Exists(dosyaUzantisi))
            {
                streamWriter.WriteLine(metin);
                streamWriter.Flush();
            }
        }
    }
}
