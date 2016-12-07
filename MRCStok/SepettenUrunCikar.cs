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
    public partial class SepettenUrunCikar : Form
    {
        public Form1 form1gor1 = (Form1)Application.OpenForms["Form1"];
        public GirisEkrani f2143 = (GirisEkrani)Application.OpenForms["GirisEkrani"];
        public StokmatikSepetEntities db2;
        public StokMatikEntities db;
        public SepettenUrunCikar()
        {
            InitializeComponent();
            db2 = new StokmatikSepetEntities();
            db = new StokMatikEntities();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var bul = db2.Sepet.Where(p => p.UrunAdi == Urunadi.Text && p.UrunGramaji == UrunGramaji.Text && p.UrunPaketi == UrunAmbalaji.Text).FirstOrDefault();
                var bul1 = db.Urunler.Where(p => p.UrunAdi == Urunadi.Text && p.UrunGramaji == UrunGramaji.Text && p.UrunPaketi == UrunAmbalaji.Text).FirstOrDefault();
                double adet = Convert.ToDouble(bul.UrunAdedi);
                double oncekiadet = Convert.ToDouble(form1gor1.textBox6.Text);
                double oncekitoplam = Convert.ToDouble(form1gor1.textBox4.Text);
                double oncekilira = Convert.ToDouble(form1gor1.textBox5.Text);
                double cikicakadet = Convert.ToDouble(bul.UrunAdedi);
                double cikicaklira = Convert.ToDouble(bul.UrunFiyati);
                double cikicaktoplam = Convert.ToDouble(bul.UrunGramaji);
                double toplamgramyaz = oncekitoplam - (cikicaktoplam * cikicakadet);
                double toplamadetyaz = oncekiadet - cikicakadet;
                double toplamfiyatyaz = oncekilira - (cikicaklira * cikicakadet);
                double stokadet = Convert.ToDouble(bul1.UrunAdedi);
                double topla = stokadet + adet;
                bul1.UrunAdedi = topla.ToString();
                db2.Sepet.Remove(bul);
                db.SaveChanges();
                db2.SaveChanges();
                MessageBox.Show("Ürün Başarıyla Sepetten Silindi");
                f2143.yenidenbaslama = true;
                string kontrolal = form1gor1.AdminKontrol;
                form1gor1.Close();
                Form1 frm = new Form1();
                frm.Show();
                frm.AdminKontrol = kontrolal;
                frm.textBox4.Text = toplamgramyaz.ToString();
                frm.textBox5.Text = toplamfiyatyaz.ToString();
                frm.textBox6.Text = toplamadetyaz.ToString();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
