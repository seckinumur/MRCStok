using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MRCStok
{
    public partial class UrunEklemeEkrani : Form
    {
        public Form1 Anaformugor = (Form1)Application.OpenForms["Form1"];
        public UrunEklemeEkrani()
        {
            InitializeComponent();
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               if(textBox4.Text=="")
                {
                    MessageBox.Show("Ürün adedinin girmediniz!");
                }
               else
                {
                    double kontrol = Convert.ToDouble(textBox3.Text);
                    double girilen = Convert.ToDouble(textBox4.Text);
                    if (girilen <= kontrol && girilen > 0)
                    {
                        var ekle = Anaformugor.db.Urunler.Where(p => p.UrunAdi == textBox1.Text).FirstOrDefault();
                        double mevcutgramaj = Convert.ToDouble(Anaformugor.textBox4.Text);
                        double mevcutadet = Convert.ToDouble(Anaformugor.textBox6.Text);
                        double mevcuttl = Convert.ToDouble(Anaformugor.textBox5.Text);

                        double urungramaji = Convert.ToDouble(ekle.UrunGramaji);
                        double urunadeti = girilen;
                        double urunfiyati = Convert.ToDouble(textBox6.Text);

                        double toplamgramaj = mevcutgramaj + urungramaji*girilen;
                        double toplamadet = mevcutadet + girilen;
                        double tolamtl = mevcuttl + urunfiyati*girilen;
                        double Urunadetii = Convert.ToDouble(ekle.UrunAdedi);
                        double Sonurunadedi = Urunadetii - girilen;

                        ekle.UrunAdedi = Sonurunadedi.ToString();
                        Anaformugor.db.SaveChanges(); 

                        Anaformugor.textBox4.Text = toplamgramaj.ToString();
                        Anaformugor.textBox6.Text = toplamadet.ToString();
                        Anaformugor.textBox5.Text = tolamtl.ToString();

                        Anaformugor.dataGridView2.Rows.Add();
                        Anaformugor.dataGridView2.Rows[Anaformugor.sayac].Cells[0].Value = textBox1.Text;
                        Anaformugor.dataGridView2.Rows[Anaformugor.sayac].Cells[1].Value = textBox2.Text;
                        Anaformugor.dataGridView2.Rows[Anaformugor.sayac].Cells[2].Value = textBox4.Text;
                        Anaformugor.dataGridView2.Rows[Anaformugor.sayac].Cells[3].Value = textBox5.Text;
                        Anaformugor.dataGridView2.Rows[Anaformugor.sayac].Cells[4].Value = textBox6.Text;
                        Anaformugor.sayac++;
                        this.Close();
                        Anaformugor.Form1_Load(sender, e);
                }
                    else
                    {
                        MessageBox.Show("STOKTA BULUNAN ÜRÜN MİKTARINDAN FARKLI ÜRÜN ÇIKIŞI YAPAMAZSINIZ!", "UYARI!");
                    }
                }
                }
                   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UrunEklemeEkrani_Load(object sender, EventArgs e)
        {

        }
    }
}
