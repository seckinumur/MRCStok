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
        public GirisEkrani f216 = (GirisEkrani)System.Windows.Forms.Application.OpenForms["GirisEkrani"];
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
                if (HizliStokUrunAdedi.Text == "")
                {
                    MessageBox.Show("Lütfen eklenecek stok adedini giriniz!");
                }
                else
                {
                    try
                    {
                        Uretim ekle = new Uretim();
                        var ara = db.Urunler.Where(p => p.UrunAdi == label3.Text && p.UrunGramaji == label4.Text && p.UrunPaketi == label5.Text).FirstOrDefault();
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
                        f216.yenidenbaslama = true;
                        string kontrolal = Anaformburda.AdminKontrol;
                        Anaformburda.Close();
                        Form1 frm = new Form1();
                        frm.Show();
                        frm.AdminKontrol = kontrolal;
                        this.Close();
                        MessageBox.Show(ara.UrunAdi + " Adlı Ürünün " + mevcuturun.ToString() + " Adet'ine " + ekleurun.ToString() + " Adet Daha Eklendi. Ürünün Yeni Adeti: " + sonhali.ToString() + " Olmuştur.");
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
                UrunSilButonu.Visible = true;
                UrunduzenleButonu.Visible = true;
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
                    var bul = db.Urunler.Where(p => p.UrunAdi == UrunAdiUrunDuzenle.Text && p.UrunGramaji == label4.Text && p.UrunPaketi == label5.Text).FirstOrDefault();
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
                    var bul = db.Urunler.Where(p => p.UrunAdi == label3.Text && p.UrunGramaji == label4.Text && p.UrunPaketi == label5.Text).FirstOrDefault();
                    Uretim ekle = new Uretim();
                    DialogResult Uyari = new DialogResult();
                    Uyari = MessageBox.Show(UrunAdiUrunDuzenle.Text + " Adlı ürünün Bilgileri Güncellenecek Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                    if (Uyari == DialogResult.Yes)
                    {
                        double eskiurunadedi = Convert.ToDouble(bul.UrunAdedi);
                        double yeniurunadedi = Convert.ToDouble(UrunAdediUrunDuzenle.Text);
                        double sonuc = eskiurunadedi < yeniurunadedi ? yeniurunadedi - eskiurunadedi : yeniurunadedi - eskiurunadedi;
                        string ay = DateTime.Now.Month.ToString();
                        ekle.UrunAdi = UrunAdiUrunDuzenle.Text;
                        bul.UrunAdi = UrunAdiUrunDuzenle.Text;
                        ekle.UrunAdedi = sonuc.ToString();
                        ekle.Uretici = "ÜRÜN DÜZELTMESİ";
                        ekle.Ay = ay;
                        ekle.Gun = DateTime.Now.Day.ToString();
                        bul.UrunAdedi = UrunAdediUrunDuzenle.Text;
                        bul.UrunBarkodu = UrunBarkoduUrunDuzenle.Text;
                        bul.UrunFiyati = UrunFiyatiUrunDuzenle.Text;
                        if (UrunGramajıUrunDuzenle.Text != UrunGramajıUrunDuzenle.SelectedItem)
                        {
                            bul.UrunGramaji = UrunGramajıUrunDuzenle.Text;
                            ekle.UrunGramaji = UrunGramajıUrunDuzenle.Text;
                        }
                        else
                        {
                            bul.UrunGramaji = UrunGramajıUrunDuzenle.SelectedItem.ToString();
                            ekle.UrunGramaji = UrunGramajıUrunDuzenle.SelectedItem.ToString();
                        }
                        bul.UrunPaketi = AmbalajUrunDuzenle.SelectedItem.ToString();
                        ekle.UrunAmbalaji = AmbalajUrunDuzenle.SelectedItem.ToString();
                        bul.UrunEklemeTarihi = DateTime.Now.ToShortDateString();
                        ekle.Yil = DateTime.Now.Year.ToString();
                        ekle.UrunUretimTarihi = DateTime.Now.ToShortDateString();
                        db.SaveChanges();
                        db1.Uretim.Add(ekle);
                        db1.SaveChanges();
                        string yetki = Anaformburda.AdminKontrol;
                        f216.yenidenbaslama = true;
                        Anaformburda.Close();
                        Form1 frm = new Form1();
                        frm.Show();
                        frm.AdminKontrol = yetki;
                        this.Close();
                        MessageBox.Show("Ürün Başarıyla Güncellendi");
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

        private void UrunBarkoduUrunDuzenle_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar);
        }

        private void UrunAdediUrunDuzenle_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar);
        }

        private void UrunFiyatiUrunDuzenle_TextChanged(object sender, EventArgs e)
        {

        }

        private void UrunFiyatiUrunDuzenle_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar);
        }

        private void UrunGramajıUrunDuzenle_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar);
        }
    }
}
