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
    public partial class Uruneklemetektus : Form
    {
        public Form1 Anaformburda = (Form1)Application.OpenForms["Form1"];
        public StokMatikEntities db;
        public StokmatikHammaddeEntities db1;
        public Uruneklemetektus()
        {
            InitializeComponent();
            db = new StokMatikEntities();
            db1 = new StokmatikHammaddeEntities();
        }

        private void Uruneklemetektus_Load(object sender, EventArgs e)
        {

        }

        private void HizliStokUrunAdedi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(HizliStokUrunAdedi.Text=="")
                {
                    MessageBox.Show("Lütfen eklenecek stok adedini giriniz!");
                }
                else
                {
                    try
                    {
                        Uretim ekle = new Uretim();
                        var ara = db.Urunler.Where(p => p.UrunAdi == UrunAdiUrunDuzenle.Text).FirstOrDefault();
                        int ekleurun = Convert.ToInt32(HizliStokUrunAdedi.Text);
                        int mevcuturun = Convert.ToInt32(ara.UrunAdedi);
                        int sonhali = ekleurun + mevcuturun;
                        ara.UrunAdedi = sonhali.ToString();
                        ara.UrunEklemeTarihi = DateTime.Now.ToShortDateString();
                        ekle.Ay = DateTime.Now.Month.ToString();
                        ekle.Gun = DateTime.Now.Day.ToString();
                        ekle.UrunAdedi = HizliStokUrunAdedi.Text;
                        ekle.UrunAdi = UrunAdiUrunDuzenle.Text;
                        ekle.UrunAmbalaji = ara.UrunPaketi;
                        ekle.UrunGramaji = ara.UrunGramaji;
                        ekle.UrunUretimTarihi = DateTime.Now.ToShortDateString();
                        ekle.Yil = DateTime.Now.Year.ToString();
                        db1.Uretim.Add(ekle);
                        db1.SaveChanges();
                        db.SaveChanges();
                        MessageBox.Show(ara.UrunAdi + " Adlı Ürünün " + mevcuturun.ToString() + " Adet'ine " + ekleurun.ToString() + " Adet Daha Eklendi. Ürünün Yeni Adeti: " + sonhali.ToString() + " Olmuştur.");
                        Anaformburda.Form1_Load(sender, e);
                        this.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Veritabanına Bağlanamadı!");
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Anaformburda.AdminKontrol == "admin" || Anaformburda.AdminKontrol == "stok")
            {
            UrunAdiUrunDuzenle.ReadOnly = false;
            UrunAdediUrunDuzenle.ReadOnly = false;
            UrunFiyatiUrunDuzenle.ReadOnly = false;
            UrunGramajıUrunDuzenle.Enabled = true;
            AmbalajUrunDuzenle.Enabled = true;
            MessageBox.Show("Ürün Düzenleme Aktifleştirildi! Şimdi Ürünü Düzenleyebilirsiniz!");
            }
            else
            {
                MessageBox.Show("Sadece ADMIN Yetkisi Verilen Kullanıcılar Hızlı Ürün Ekleyebilir.", "Yetkisiz Kullanıcı!");
            }
            
        }

        private void UrunSilButonu_Click(object sender, EventArgs e)
        {
            if (Anaformburda.AdminKontrol == "admin")
            {
                try
                {
                    var bul = db.Urunler.Where(p => p.UrunAdi == UrunAdiUrunDuzenle.Text).FirstOrDefault();
                    if (bul.UrunAdi == UrunAdiUrunDuzenle.Text)
                    {
                        DialogResult Uyari = new DialogResult();
                        Uyari = MessageBox.Show(UrunAdiUrunDuzenle.Text + " Adlı ürünün Silinecek Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                        if (Uyari == DialogResult.Yes)
                        {

                            db.Urunler.Remove(bul);
                            db.SaveChanges();
                            MessageBox.Show("Ürün Başarıyla Silindi!");
                            Anaformburda.Form1_Load(sender, e);
                            this.Close();
                        }
                    }
                }
                catch
                {

                    MessageBox.Show("Veritabanına Bağlanamadı!");

                }
            }
            else
            {
                MessageBox.Show("Sadece ADMIN Yetkisi Verilmiş Kullanıcılar Ürün Silebilir.", "Yetkisiz Kullanıcı!");
            }
        }

        private void UrunduzenleButonu_Click(object sender, EventArgs e)
        {
            if (Anaformburda.AdminKontrol == "admin" || Anaformburda.AdminKontrol == "stok")
            {
                try
                {
                    var bul = db.Urunler.Where(p => p.UrunAdi == UrunAdiUrunDuzenle.Text).FirstOrDefault();
                        DialogResult Uyari = new DialogResult();
                        Uyari = MessageBox.Show(UrunAdiUrunDuzenle.Text + " Adlı ürünün Bilgileri Güncellenecek Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                        if (Uyari == DialogResult.Yes)
                        {
                            bul.UrunAdi = UrunAdiUrunDuzenle.Text;
                            bul.UrunAdedi = UrunAdediUrunDuzenle.Text;
                            bul.UrunBarkodu = UrunBarkoduUrunDuzenle.Text;
                            bul.UrunFiyati = UrunFiyatiUrunDuzenle.Text;
                            if (UrunGramajıUrunDuzenle.Text!= UrunGramajıUrunDuzenle.SelectedItem)
                        {
                            bul.UrunGramaji = UrunGramajıUrunDuzenle.Text;
                        }
                        else
                        {
                             bul.UrunGramaji = UrunGramajıUrunDuzenle.SelectedItem.ToString();
                        }
                            
                            bul.UrunPaketi = AmbalajUrunDuzenle.SelectedItem.ToString();
                            bul.UrunEklemeTarihi = DateTime.Now.ToShortDateString();
                            db.SaveChanges();
                            MessageBox.Show("Ürün Başarıyla Güncellendi");
                            Anaformburda.Form1_Load(sender, e);
                            this.Close();
                        }
                }
                catch
                {

                    MessageBox.Show("Veritabanına Bağlanamadı!");
                }
            }
            else
            {
                MessageBox.Show("Yalnızca ADMIN Yetkisi Verilmiş Kullanıcılar Ürün Düzenleyebilirler.", "Yetkisiz Kullanıcı!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HizliStokUrunAdedi_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
