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
    public partial class TedarikciEkle : Form
    {
        public StokMatikEntities db;
        public StokmatikHammaddeEntities db1;
        public int Sayac = 0;
        public Form1 Anaformbudrda = (Form1)Application.OpenForms["Form1"];
        public GirisEkrani f2216 = (GirisEkrani)System.Windows.Forms.Application.OpenForms["GirisEkrani"];
        public bool iade = false;
        public TedarikciEkle()
        {
            InitializeComponent();
            db = new StokMatikEntities();
            db1 = new StokmatikHammaddeEntities();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MusteriVeyaTedarikciSec_Click(object sender, EventArgs e)
        {
           
                MusteriSecmeEkrani ac = new MusteriSecmeEkrani();
                ac.Show();
                ac.counter = "3";
           
        }

        private void Onayla_Click(object sender, EventArgs e)
        {
            if (Onayla.Text== "BU ÜRÜNÜ FATURALI OLARAK STOK'A GİR")
            {
                if(MVeTedarikciAdi.Text=="" || FaturaNo.Text == "" || dataGridView1.Rows[0].Cells[0].Value=="")
                {
                    MessageBox.Show("Tedarikçi Adı, adet ve Fatura No Boş Bırakılamaz!");
                }
                else
                {
                    DialogResult Uyari = new DialogResult();
                    Uyari = MessageBox.Show(MVeTedarikciAdi.Text + " Adlı Tedarikçiden Stok'a Giriş Yapılacaktır. İşleme Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                    if (Uyari == DialogResult.Yes)
                    {
                        try
                        {
                            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++) //hata alirsan "dataGridinIsmi.Rows.Count -1" yap 
                            {
                                string urunadial = dataGridView1.Rows[i].Cells[0].Value.ToString();
                                string urungramajial = dataGridView1.Rows[i].Cells[2].Value.ToString();
                                string urunpaketial = dataGridView1.Rows[i].Cells[3].Value.ToString();
                                Uretim ekle = new Uretim();
                                var ara = db.Urunler.Where(p => p.UrunAdi ==urunadial && p.UrunGramaji == urungramajial && p.UrunPaketi == urunpaketial).FirstOrDefault();
                                int ekleurun = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value.ToString());
                                int mevcuturun = Convert.ToInt32(ara.UrunAdedi);
                                int sonhali = ekleurun + mevcuturun;
                                ara.UrunAdedi = sonhali.ToString();
                                ara.UrunEklemeTarihi = DateTime.Now.ToShortDateString();
                                ekle.Ay = DateTime.Now.Month.ToString();
                                ekle.Gun = DateTime.Now.Day.ToString();
                                ekle.Uretici = MVeTedarikciAdi.Text;
                                ekle.UreticiFaturasi = FaturaNo.Text;
                                ekle.UrunAdedi = dataGridView1.Rows[i].Cells[1].Value.ToString();
                                ekle.UrunAdi = dataGridView1.Rows[i].Cells[0].Value.ToString();
                                ekle.UrunAmbalaji = dataGridView1.Rows[i].Cells[3].Value.ToString();
                                ekle.UrunGramaji = dataGridView1.Rows[i].Cells[2].Value.ToString();
                                ekle.UrunUretimTarihi = DateTime.Now.ToShortDateString();
                                ekle.Yil = DateTime.Now.Year.ToString();
                                db1.Uretim.Add(ekle);
                                db1.SaveChanges();
                                db.SaveChanges();
                            }
                            f2216.yenidenbaslama = true;
                            string kontrolal = Anaformbudrda.AdminKontrol;
                            Anaformbudrda.Close();
                            Form1 frm = new Form1();
                            frm.Show();
                            frm.AdminKontrol = kontrolal;
                            this.Close();
                            MessageBox.Show("İŞLEM BAŞARILI");
                        }
                        catch (Exception EX)
                        {
                            MessageBox.Show(EX.Message);
                        }
                    }
                }
            }
            else
            {
                if (MVeTedarikciAdi.Text == "" || FaturaNo.Text == "" || dataGridView1.Rows[0].Cells[0].Value == "")
                {
                    MessageBox.Show("Müşteri Adı, adet ve Fatura No Boş Bırakılamaz!");
                }
                else
                {
                    DialogResult Uyari = new DialogResult();
                    Uyari = MessageBox.Show(MVeTedarikciAdi.Text + " Adlı Müşteriden İade Ürünler Stok'a Giriş Yapılacaktır. İşleme Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                    if (Uyari == DialogResult.Yes)
                    {
                        try
                        {
                            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++) //hata alirsan "dataGridinIsmi.Rows.Count -1" yap 
                            {
                                string urunadial = dataGridView1.Rows[i].Cells[0].Value.ToString();
                                string urungramajial = dataGridView1.Rows[i].Cells[2].Value.ToString();
                                string urunpaketial = dataGridView1.Rows[i].Cells[3].Value.ToString();
                                Uretim ekle = new Uretim();
                                Raporlama bunudaal = new Raporlama();
                                var ara = db.Urunler.Where(p => p.UrunAdi == urunadial && p.UrunGramaji == urungramajial && p.UrunPaketi == urunpaketial).FirstOrDefault();
                                int ekleurun = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value.ToString());
                                int mevcuturun = Convert.ToInt32(ara.UrunAdedi);
                                int sonhali = ekleurun + mevcuturun;
                                ara.UrunAdedi = sonhali.ToString();
                                ara.UrunEklemeTarihi = DateTime.Now.ToShortDateString();
                                ekle.Ay = DateTime.Now.Month.ToString();
                                ekle.Gun = DateTime.Now.Day.ToString();
                                ekle.Uretici = MVeTedarikciAdi.Text+" İADE";
                                ekle.UreticiFaturasi = FaturaNo.Text+ " İADE GELEN ÜRÜN";
                                ekle.UrunAdedi = dataGridView1.Rows[i].Cells[1].Value.ToString();
                                ekle.UrunAdi = dataGridView1.Rows[i].Cells[0].Value.ToString();
                                ekle.UrunAmbalaji = dataGridView1.Rows[i].Cells[3].Value.ToString();
                                ekle.UrunGramaji = dataGridView1.Rows[i].Cells[2].Value.ToString();
                                ekle.UrunUretimTarihi = DateTime.Now.ToShortDateString();
                                ekle.Yil = DateTime.Now.Year.ToString();
                                bunudaal.Ay = DateTime.Now.Month.ToString();
                                bunudaal.FaturaDurumu = FaturaNo.Text + " İADE GELEN ÜRÜN";
                                bunudaal.Fiyati = "0";
                                bunudaal.GidenMusteriler= MVeTedarikciAdi.Text + " İADE";
                                bunudaal.GidenUrunler = dataGridView1.Rows[i].Cells[0].Value.ToString();
                                bunudaal.Gun= DateTime.Now.Day.ToString();
                                bunudaal.Tarih = DateTime.Now.ToShortDateString();
                                bunudaal.UrunAdedi= dataGridView1.Rows[i].Cells[1].Value.ToString();
                                bunudaal.UrunGramaji= dataGridView1.Rows[i].Cells[2].Value.ToString();
                                bunudaal.UrunPaketi= dataGridView1.Rows[i].Cells[3].Value.ToString();
                                bunudaal.Yil = DateTime.Now.Year.ToString();
                                db.Raporlama.Add(bunudaal);
                                db1.Uretim.Add(ekle);
                                db1.SaveChanges();
                                db.SaveChanges();
                            }
                            f2216.yenidenbaslama = true;
                            string kontrolal = Anaformbudrda.AdminKontrol;
                            Anaformbudrda.Close();
                            Form1 frm = new Form1();
                            frm.Show();
                            frm.AdminKontrol = kontrolal;
                            this.Close();
                            MessageBox.Show("İŞLEM BAŞARILI");
                        }
                        catch (Exception EX)
                        {
                            MessageBox.Show(EX.Message);
                        }
                    }
                }
                }
        }

        private void UrunEklemeTusu_Click(object sender, EventArgs e)
        {                       
            Urunegorelistele ac = new Urunegorelistele();
            ac.Show();
            ac.gelenrapor2 = true;
        }
    }
}
