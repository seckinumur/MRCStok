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
                    DialogResult Uyari = new DialogResult();
                    Uyari = MessageBox.Show(UrunAdiUrunDuzenle.Text + " Adlı ürünün Bilgileri Güncellenecek Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                    if (Uyari == DialogResult.Yes)
                    {
                        bul.UrunAdi = UrunAdiUrunDuzenle.Text;
                        bul.UrunAdedi = UrunAdediUrunDuzenle.Text;
                        bul.UrunBarkodu = UrunBarkoduUrunDuzenle.Text;
                        bul.UrunFiyati = UrunFiyatiUrunDuzenle.Text;
                        if (UrunGramajıUrunDuzenle.Text != UrunGramajıUrunDuzenle.SelectedItem)
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

        private void button4_Click(object sender, EventArgs e)
        {
            MusteriSecmeEkrani ac = new MusteriSecmeEkrani();
            ac.Show();
            ac.counter = "3";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text=="" || textBox16.Text=="")
            {
                MessageBox.Show("Müşteri Adı, adet ve Fatura No Boş Bırakılamaz!");
            }
            else
            {
                DialogResult Uyari = new DialogResult();
                Uyari = MessageBox.Show(UrunAdiUrunDuzenle.Text + " Adlı ürünün " + textBox1.Text + " Adlı Müşteriden İade Olarak Alınacak. İşleme Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                if (Uyari == DialogResult.Yes)
                {
                    try
                    {
                        var bul = db.Raporlama.Where(p=> p.GidenMusteriler == textBox1.Text && p.GidenUrunler== label3.Text && p.UrunGramaji == label4.Text && p.UrunPaketi == label5.Text).FirstOrDefault();
                        double satilanurun = Convert.ToDouble(bul.UrunAdedi);
                        double girilenurunadedtt = Convert.ToDouble(textBox2.Text);
                        double sonucc = satilanurun - girilenurunadedtt;
                        if (satilanurun< girilenurunadedtt)
                        {
                            MessageBox.Show("Bu Ürünün " + bul.FaturaDurumu + " Nolu Faturasında Yer Alan " + bul.UrunAdedi + " Adet Üründen Daha Fazla Ürünü İade Almaya Çalıştınız! Sattığınızdan Daha Fazla iade alamazsınız!");
                        }
                        else
                        {
                            Raporlama ekle = new Raporlama();
                            var ara = db.Urunler.Where(p => p.UrunAdi == label3.Text && p.UrunGramaji == label4.Text && p.UrunPaketi == label5.Text).FirstOrDefault();
                            int ekleurun = Convert.ToInt32(textBox2.Text);
                            int mevcuturun = Convert.ToInt32(ara.UrunAdedi);
                            int sonhali = ekleurun + mevcuturun;
                            ara.UrunAdedi = sonhali.ToString();
                            ara.UrunEklemeTarihi = DateTime.Now.ToShortDateString();
                            ekle.Ay = DateTime.Now.Month.ToString();
                            ekle.Gun = DateTime.Now.Day.ToString();
                            ekle.Tarih = DateTime.Now.ToShortDateString();
                            ekle.UrunAdedi = textBox2.Text;
                            ekle.GidenUrunler = label3.Text;
                            ekle.FaturaDurumu = "İADE GELEN ÜRÜN "+textBox16.Text;
                            ekle.Fiyati = bul.Fiyati;
                            ekle.UrunGramaji = bul.UrunGramaji;
                            ekle.UrunPaketi = bul.UrunPaketi;
                            ekle.Yil = DateTime.Now.Year.ToString();
                            ekle.GidenMusteriler = bul.GidenMusteriler;
                            if (sonucc==0)
                            {
                                db.Raporlama.Remove(bul);
                            }
                            else
                            {
                                bul.UrunAdedi = sonucc.ToString();
                            }
                            db.Raporlama.Add(ekle);
                            db.SaveChanges();
                            f216.yenidenbaslama = true;
                            string kontrolal = Anaformburda.AdminKontrol;
                            Anaformburda.Close();
                            Form1 frm = new Form1();
                            frm.Show();
                            frm.AdminKontrol = kontrolal;
                            this.Close();
                            MessageBox.Show(bul.GidenUrunler + " Adlı Ürünün " +textBox2.Text+" Adet "+bul.GidenMusteriler+ " Adlı Müşteri Tarafından İade Edilmiştir." );
                        }
                        
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

        }
    }
}
