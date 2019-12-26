using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Yazlab2_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        //int hücreYatay = 124, hücreDikey = 100;
        //private System.Collections.Hashtable hücreDurumu;


    private void Form1_Load(object sender, EventArgs e)
        {

        }

    private void BitmapYap1(Image resim,Image [] Resimler,int index,int Satırid,int Kolonid,int X,int Y)
    {
            Resimler[index] = new Bitmap(X, Y);
            Graphics objGraphics = Graphics.FromImage(Resimler[index]);
            objGraphics.Clear(Color.White);

            objGraphics.DrawImage(resim,
            new Rectangle(0, 0, X, Y),
            new Rectangle(X * (index % Kolonid), Y * (index / Satırid), X, Y),
            GraphicsUnit.Pixel);
            objGraphics.Flush();
    }
        
    private void Karistir(ref int [] yenidizi)
    {
            Random karistir1 = new Random();
            int n = yenidizi.Length;
            while (n > 1)
            {              
            int k = karistir1.Next(n);
            n--;
            int temp = yenidizi[n];
            yenidizi[n] = yenidizi[k];
            yenidizi[k] = temp;
            }
    }
    
    private Bitmap BitmapYap (Image resim)
    {
            Bitmap ResimBölme = new Bitmap(panel1.Width, panel1.Height);
            Graphics objGraphics = Graphics.FromImage(ResimBölme);
            objGraphics.Clear(Color.White);
            objGraphics.DrawImage(resim, new Rectangle(0, 0, panel1.Width, panel1.Height));
            objGraphics.Flush();

            return ResimBölme;
    }

    PictureBox[] ResimHücreleri = null;  
    int Hücre = 16;
    Image[] Resimler = null;    
    int AnlıkDurum = 0;
    int hamle=0;

        private void Karıştır_Click(object sender, EventArgs e)
    {
            AnlıkDurum = Hücre;
            Karıştırma();
    }
   
    private void Karıştırma() 
    {
            if (ResimPaneli != null)
            {
                panel1.Controls.Remove(ResimPaneli);
                ResimPaneli.Dispose();
                ResimPaneli = null;
            }
            if (ResimHücreleri == null)
            {
                Resimler = new Image[Hücre];   
                ResimHücreleri = new PictureBox[Hücre];
            }

            int Satirid = (int)Math.Sqrt(AnlıkDurum);
            int Kolonid = Satirid;
            int X = (panel1.Width / Satirid); 
            int Y = (panel1.Height / Kolonid);
            int[] indis = new int[AnlıkDurum];


            for (int i = 0; i < AnlıkDurum; i++)
            {
                indis[i] = i;
                if (ResimHücreleri[i] == null)
                {
                    ResimHücreleri[i] = new PuzzleOyunu();
                    ResimHücreleri[i].Click += new EventHandler(Tıklama);                  
                }
                ResimHücreleri[i].Height = X;
                ResimHücreleri[i].Width = Y;

                ((PuzzleOyunu)ResimHücreleri[i]).indeks1 = i;

                BitmapYap1(resim, Resimler, i, Satirid, Kolonid, X, Y);

                ResimHücreleri[i].Location = new Point(X * (i % Kolonid), Y * (i / Kolonid));
                if (!panel1.Controls.Contains(ResimHücreleri[i]))
                    panel1.Controls.Add(ResimHücreleri[i]);
            }

          Karistir(ref indis);
          int k = 0;
          while(k < AnlıkDurum)
         {
           ResimHücreleri[k].Image = Resimler[indis[k]];
           ((PuzzleOyunu)ResimHücreleri[k]).Resimindeks1 = indis[k];
           k++;
         }
    }
                OpenFileDialog OpenFileDialog = null;
                Image resim;                           
                PictureBox ResimPaneli = null;         
       
    private void ResimSecButon(object sender, EventArgs e)
             {
            if (OpenFileDialog == null)
                OpenFileDialog = new OpenFileDialog();
            if (OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                resim = BitmapYap(Image.FromFile(OpenFileDialog.FileName));
                if (ResimPaneli == null )
                {
                    ResimPaneli = new PictureBox();
                    ResimPaneli.Height = panel1.Height;
                    ResimPaneli.Width = panel1.Width;
                    panel1.Controls.Add(ResimPaneli);
                }
               ResimPaneli.Image = resim;
            }
        }
        PuzzleOyunu İlkHücre = null ;
        PuzzleOyunu İkinciHücre = null ;
        public void Tıklama (object sender ,EventArgs e)
        {
          if(İlkHücre == null)
            {
                İlkHücre = (PuzzleOyunu)sender;
        }
          else if (İkinciHücre == null )
            {
                İkinciHücre = (PuzzleOyunu)sender;              
                YerDeğiştirme(İlkHücre, İkinciHücre);
                İlkHücre = null;
                İkinciHücre = null;
            }        
       }

    private void YerDeğiştirme(PuzzleOyunu Hücre1, PuzzleOyunu Hücre2)
        {
            
            int tmp = Hücre2.Resimindeks1;
            Hücre2.Image = Resimler[Hücre1.Resimindeks1];
            Hücre2.Resimindeks1 = Hücre1.Resimindeks1;
            Hücre1.Image = Resimler[tmp];
            Hücre1.Resimindeks1 = tmp;
            hamle++;
          /*  for (int hamle = 0; hamle < 100; hamle++)
            { 
            dosyayaYaz(hamle);
            if(hamle <= 15)
                {
                    dosyayaYaz(100);
                }
                else
                {
                    int c = 1000;
                    while (c < AnlıkDurum)
                    {
                        c = c - 5;
                        dosyayaYaz(c);
                    }
                }
            }*/

            if (Bitme())
            {
                if (hamle < 17)
                {
                    dosyayaYaz(100);
                    label2.Text = "100";

                }
                else
                {
                    int c = 100-(hamle - 16)*5;
                    dosyayaYaz(c);
                    label2.Text = c.ToString();
                }
                label1.Text = "Bitti";
                Console.WriteLine("Bitti");
                
            }
           

        }
      
    private bool Bitme()
        {
            int p=0;
            while(p < AnlıkDurum)    
            {
                if(((PuzzleOyunu)ResimHücreleri[p]).Resimindeks1 != ((PuzzleOyunu)ResimHücreleri[p]).indeks1)
                        return false;
                        p++;
            }
            return true;
        }
    private void button1_Click(object sender, EventArgs e)
        {

        }
    private void panel1_Click(object sender, EventArgs e)
        {

        }
    private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    private static void dosyayaYaz(int a)
        {
         string dosya_yolu = @"C:\Users\ardac\Desktop\Skor.txt";
         FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
         StreamWriter sw = new StreamWriter(fs);
         sw.WriteLine(a);
         sw.Flush();
         sw.Close();
         fs.Close(); 
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        /* private void textBox1_TextChanged(object sender, EventArgs e)
         {
             if (Bitme())
             {
                 Console.WriteLine("Bitti");
             }
         }*/
    }
}
