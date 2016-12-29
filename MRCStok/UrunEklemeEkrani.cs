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
        public StokmatikSepetEntities db2;
        public UrunEklemeEkrani()
        {
            InitializeComponent();
            db2 = new StokmatikSepetEntities();
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox4.Text == "")
                {
                    MessageBox.Show("Ürün adedinin girmediniz!");
                }
                else
                {
                    double kontrol = Convert.ToDouble(textBox3.Text);
                    double girilen = Convert.ToDouble(textBox4.Text);
                    if (girilen <= kontrol && girilen > 0)
                    {

                        try
                        {
                            var ekle = Anaformugor.db.Urunler.Where(p => p.UrunAdi == textBox1.Text && p.UrunGramaji == textBox2.Text && p.UrunPaketi == textBox5.Text).FirstOrDefault();
                            double mevcuttl = Convert.ToDouble(Anaformugor.textBox5.Text);
                            double urungramaji = Convert.ToDouble(ekle.UrunGramaji);
                            double urunadeti = girilen;
                            double urunfiyati = Convert.ToDouble(textBox6.Text);
                            double tolamtl = mevcuttl + (urunfiyati * girilen);
                            double Urunadetii = Convert.ToDouble(ekle.UrunAdedi);
                            double Sonurunadedi = Urunadetii - girilen;
                            ekle.UrunAdedi = Sonurunadedi.ToString();
                            Sepet sepeteat = new Sepet();
                            sepeteat.Ay = DateTime.Now.Month.ToString();
                            sepeteat.Gun = DateTime.Now.Day.ToString();
                            sepeteat.Tarih = DateTime.Now.ToShortDateString();
                            sepeteat.UrunAdedi = textBox4.Text;
                            sepeteat.UrunAdi = textBox1.Text;
                            if (textBox6.Text != ekle.UrunFiyati)
                            {
                                DialogResult Uyari = new DialogResult();
                                Uyari = MessageBox.Show("Bu Ürünün Fiyatı Kalıcı Olarak Değiştirilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                                if (Uyari == DialogResult.Yes)
                                {
                                    ekle.UrunFiyati = textBox6.Text;
                                }
                            }
                            sepeteat.UrunFiyati = textBox6.Text;
                            sepeteat.UrunGramaji = textBox2.Text;
                            sepeteat.UrunPaketi = textBox5.Text;
                            if(checkBox1.Checked==true)
                            {
                                sepeteat.UrunFaturası = "BEDELSİZ";
                            }
                            else
                            {
                                sepeteat.UrunFaturası = "BELİRSİZ";
                            }
                            sepeteat.Yil = DateTime.Now.Year.ToString();
                            sepeteat.UrunBarkotu = "";
                            db2.Sepet.Add(sepeteat);
                            db2.SaveChanges();
                            Anaformugor.db.SaveChanges();
                            Anaformugor.textBox5.Text = tolamtl.ToString();
                            Anaformugor.gridControl4.RefreshDataSource();
                            this.Close();
                            Anaformugor.Form1_Load(sender, e);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar);
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar);
        }
    }
}
