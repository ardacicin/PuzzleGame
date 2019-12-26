using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yazlab2_1
{
    class PuzzleOyunu : PictureBox
    {
        int İndeks = 0;

        public int indeks1 {
            get { return İndeks; }
            set { İndeks = value;}
        }

        int Resimİndeks = 0;
        public int Resimindeks1 {
            get { return Resimİndeks; }
            set { Resimİndeks = value; }
        }
        public bool Eşleştirme()
        {
            return (İndeks == Resimİndeks);
        }
        
    }
}
