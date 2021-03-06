﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace MRCStok
{
    public partial class Form1 : Form
    {
        public StokMatikEntities db;
        public StokmatikHammaddeEntities db1;
        public StokmatikSepetEntities db2;
        public string RaporlamaKontrol = "OFFLINE";
        public GirisEkrani f21 = (GirisEkrani)System.Windows.Forms.Application.OpenForms["GirisEkrani"];
        public Uruneklemetektus uruneklemyegit = (Uruneklemetektus)System.Windows.Forms.Application.OpenForms["Uruneklemetektus"];
        public string AdminKontrol;
        public string tarih = DateTime.Now.ToShortDateString();
        public string MevcutGun = DateTime.Now.Day.ToString();
        public string MevcutAy = DateTime.Now.Month.ToString();
        public string MevcutYil = DateTime.Now.Year.ToString();
        public int a = 15000;
        public int sayac = 0;
        public Form1()
        {
            InitializeComponent();
            db = new StokMatikEntities();
            db1 = new StokmatikHammaddeEntities();
            db2 = new StokmatikSepetEntities();

        }   // Veritabanı Nesnelerini Tanımladık ve Yenilerini Oluşturduk.

        private void label13_Click(object sender, EventArgs e)
        {
        }  // Gereksiz Bir Tıklama.

        public void Form1_Load(object sender, EventArgs e)
        {
            RAPORLAMADURUMUGOSTER.Text = "RAPORLAMA AKTİF DEĞİL!";
            this.sepetTableAdapter.Fill(this.stokmatikSepetDataSet.Sepet);
            this.musterilerTableAdapter.Fill(this.stokMatikDataSet2.Musteriler);
            this.kullanicilarTableAdapter.Fill(this.stokMatikDataSet1.Kullanicilar);
            this.urunlerTableAdapter.Fill(this.stokMatikDataSet.Urunler);
            f21.yenidenbaslama = false;
            double sepetkalem=0;
            for (int i=0; i< gridView4.RowCount; i++)
            {
                sepetkalem++;
                textBox6.Text = sepetkalem.ToString();
            }
        }  // Form1 Yükleme Alanı.

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox txBox = (System.Windows.Forms.TextBox)sender;
            int pos = txBox.SelectionStart;
            int slen = txBox.SelectionLength;
            txBox.Text = txBox.Text.ToUpper();
            txBox.SelectionStart = pos;
            txBox.SelectionLength = slen;
            txBox.Focus();
           
        }  // Tüm TextBox Büyük Harfle Yazma Kodu.

        private void button34_Click(object sender, EventArgs e)
        {
            if (YoneticiKullaniciAdi.Text == "")
            {
                MessageBox.Show("Yönetici Adını Girin!");
            }
            else
            {
                if (YöneticiSifresi.Text == "")
                {
                    MessageBox.Show("Yönetici Şifresini Girin!");
                }
                else
                {
                    try
                    {
                        var arama = db.Kullanicilar.Where(p => p.KullaniciAdi == YoneticiKullaniciAdi.Text).FirstOrDefault();
                        if (arama.KullaniciYetkisi == "admin")
                        {
                            if (arama.KullaniciSifresi == YöneticiSifresi.Text)
                            {
                                Kullaniciİslemleri.Visible = true;
                                KullaniciListesi.Visible = true;
                                MessageBox.Show("Admin yetkisi Kazanıldı");
                                YoneticiKullaniciAdi.Text = "";
                                YöneticiSifresi.Text = "";

                            }
                            else
                            {
                                MessageBox.Show("Şifre Yanlış!");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Bu kullanıcı admin yetkisinde değil!");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Kullanıcı adı yada şifresi hatalı!");
                    }
                }
            }
        } // Ayarlar Kilit Açma İşlemi.

        private void KullaniciListesi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)  //Ayarlar Kullanıcalar Listesinden Kullanıcı Seçip İşlem Yapma.
        {
            int xkoordinat = KullaniciListesi.CurrentCellAddress.X; //Seçili satırın X koordinatı
            int ykoordinat = KullaniciListesi.CurrentCellAddress.Y;  //Seçili satırın Y koordinatı
            string str = "";
            str = KullaniciListesi.Rows[ykoordinat].Cells[xkoordinat].Value.ToString();
            if (e.RowIndex == -1)
            {
                return;
            }
            try
            {
                var Kullanicibulma = db.Kullanicilar.Where(w => w.KullaniciAdi == str).FirstOrDefault();
                if (Kullanicibulma.KullanicilarId == 1)
                {
                    MessageBox.Show("ROOT Yetkisi Verilmiş Kullanıcıyla İlgili İşlem Yapılamaz! Lütfen Başka Bir Kullanıcı Seçin!");
                }
                else
                {
                    textBox27.Text = Kullanicibulma.KullaniciAdi;
                    textBox26.Text = Kullanicibulma.KullaniciSifresi;
                }
            }
            catch
            {
                MessageBox.Show("Seçmek İçin Kullanıcı İsmine Çift Tıklayın");
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            DialogResult Uyari = new DialogResult();
            Uyari = MessageBox.Show(textBox27.Text + " Adlı Kullanıcı Sisteme Yüklenecek Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
            if (Uyari == DialogResult.Yes)
            {
                if (checkBox4.Checked == true)
                {
                    Uyari = MessageBox.Show(textBox27.Text + " Adlı Kullanıcıya Yönetici İzini Verilecek Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                    if (Uyari == DialogResult.Yes)
                    {
                        try
                        {
                            var bulbakayim = db.Kullanicilar.Where(p => p.KullaniciAdi == textBox27.Text).FirstOrDefault();
                            bulbakayim.KullaniciAdi = textBox27.Text;
                            bulbakayim.KullaniciSifresi = textBox26.Text;
                            bulbakayim.KullaniciYetkisi = "admin";
                            db.SaveChanges();
                            MessageBox.Show(textBox27.Text + " Adlı Kullanıcı Sistemde Mevcuttu. Bilgileri Güncelleştirilip Yönetici Yetkisiyle Başarıyla Kaydedildi ");
                            textBox27.Text = "";
                            textBox26.Text = "";
                            Form1_Load(sender, e);    // datagridt wiev yineleme işleme önemli kod.
                        }
                        catch
                        {
                            Kullanicilar ekle = new Kullanicilar();
                            ekle.KullaniciAdi = textBox27.Text;
                            ekle.KullaniciSifresi = textBox26.Text;
                            ekle.KullaniciYetkisi = "admin";
                            db.Kullanicilar.Add(ekle);
                            db.SaveChanges();
                            MessageBox.Show(textBox27.Text + " Adlı Kullanıcı Yönetici Yetkisiyle Başarıyla Kaydedildi ");
                            textBox27.Text = "";
                            textBox26.Text = "";
                            Form1_Load(sender, e);
                        }
                    }
                }
                else if (checkBox1.Checked == true)
                {
                    Uyari = MessageBox.Show(textBox27.Text + " Adlı Kullanıcıya Stok İzini Verilecek Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                    if (Uyari == DialogResult.Yes)
                    {
                        try
                        {
                            var bulbakayim = db.Kullanicilar.Where(p => p.KullaniciAdi == textBox27.Text).FirstOrDefault();
                            bulbakayim.KullaniciAdi = textBox27.Text;
                            bulbakayim.KullaniciSifresi = textBox26.Text;
                            bulbakayim.KullaniciYetkisi = "stok";
                            db.SaveChanges();
                            MessageBox.Show(textBox27.Text + " Adlı Kullanıcı Sistemde Mevcuttu. Bilgileri Güncelleştirilip Stok Yetkisiyle Başarıyla Kaydedildi ");
                            textBox27.Text = "";
                            textBox26.Text = "";
                            Form1_Load(sender, e);    // datagridt wiev yineleme işleme önemli kod.
                        }
                        catch
                        {
                            Kullanicilar ekle = new Kullanicilar();
                            ekle.KullaniciAdi = textBox27.Text;
                            ekle.KullaniciSifresi = textBox26.Text;
                            ekle.KullaniciYetkisi = "stok";
                            db.Kullanicilar.Add(ekle);
                            db.SaveChanges();
                            MessageBox.Show(textBox27.Text + " Adlı Kullanıcı Stok Yetkisiyle Başarıyla Kaydedildi ");
                            textBox27.Text = "";
                            textBox26.Text = "";
                            Form1_Load(sender, e);
                        }
                    }
                }
                else
                {
                    try
                    {
                        var bulbakayim = db.Kullanicilar.Where(p => p.KullaniciAdi == textBox27.Text).FirstOrDefault();
                        bulbakayim.KullaniciAdi = textBox27.Text;
                        bulbakayim.KullaniciSifresi = textBox26.Text;
                        bulbakayim.KullaniciYetkisi = "yerel";
                        db.SaveChanges();
                        MessageBox.Show(textBox27.Text + " Adlı Kullanıcı Sistemde Mevcuttu. Bilgileri Güncelleştirilip Başarıyla Kaydedildi ");
                        textBox27.Text = "";
                        textBox26.Text = "";
                        Form1_Load(sender, e);
                    }
                    catch
                    {
                        Kullanicilar ekle = new Kullanicilar();
                        ekle.KullaniciAdi = textBox27.Text;
                        ekle.KullaniciSifresi = textBox26.Text;
                        ekle.KullaniciYetkisi = "yerel";
                        db.Kullanicilar.Add(ekle);
                        db.SaveChanges();
                        MessageBox.Show(textBox27.Text + " Adlı Kullanıcı Başarıyla Kaydedildi ");
                        textBox27.Text = "";
                        textBox26.Text = "";
                        Form1_Load(sender, e);
                    }
                }
            }
        }  // Ayarlar Kullanıcı Ekleme İşlemi.

        private void button10_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();

        }  // Sistemden Çıkış. Form1 Kapama işlemi.

        private void button36_Click(object sender, EventArgs e)
        {
            try
            {
                var bulbakayim = db.Kullanicilar.Where(p => p.KullaniciAdi == textBox27.Text).FirstOrDefault();
                if (bulbakayim.KullaniciAdi == textBox27.Text)
                {
                    DialogResult Uyari = new DialogResult();
                    Uyari = MessageBox.Show(textBox27.Text + " Adlı Kullanıcı Silinecek Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                    if (Uyari == DialogResult.Yes)
                    {
                        db.Kullanicilar.Remove(bulbakayim);
                        db.SaveChanges();
                        MessageBox.Show(textBox27.Text + " Adlı Kullanıcı Sistemden Kaldırıldı");
                        textBox27.Text = "";
                        textBox26.Text = "";
                        Form1_Load(sender, e);    // datagridt wiev yineleme işleme önemli kod.
                    }
                }
            }
            catch
            {
                MessageBox.Show("Kullanıcı Sistemde Bulunamadı!");
                textBox27.Text = "";
                textBox26.Text = "";
                Form1_Load(sender, e);    // datagridt wiev yineleme işleme önemli kod.
            }
        }  // Ayaralar Kullanıcı Silme.

        private void button37_Click(object sender, EventArgs e)
        {
            Kullaniciİslemleri.Visible = false;
            KullaniciListesi.Visible = false;
            MessageBox.Show("Admin yetkisi Sonlandırıldı!");
            YoneticiKullaniciAdi.Text = "";
            YöneticiSifresi.Text = "";
        }  // Ayarlar Kullanıcı İşlemlerini Kapama. (Oturum Kapatma)

        private void MusteriKaydetMusteriEkle_Click(object sender, EventArgs e)
        {
            if (MusteriAdiMusteriEkle.Text == "")
            {
                MessageBox.Show("Müşteri Adı Girin!");
            }
            else
            {
                if (AdresMusteriEkle.Text == "")
                {
                    MessageBox.Show("Müşteri Adresini Girin!");
                }
                else
                {
                    try
                    {
                        var ara = db.Musteriler.Where(p => p.MusteriAdi == MusteriAdiMusteriEkle.Text).FirstOrDefault();
                        if (ara.MusteriAdi == MusteriAdiMusteriEkle.Text)
                        {
                            DialogResult Uyari = new DialogResult();
                            Uyari = MessageBox.Show(MusteriAdiMusteriEkle.Text + " Adlı Musteri Sisteme Daha Önceden Yüklenmiş! Musteri Bilgileri Güncellenecek Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                            if (Uyari == DialogResult.Yes)
                            {
                                ara.MusteriAdi = MusteriAdiMusteriEkle.Text;
                                ara.MusteriAdresi = AdresMusteriEkle.Text;
                                db.SaveChanges();
                                MessageBox.Show("Müşteri Bilgileri Başarıyla Güncelleştirildi!");
                                MusteriAdiMusteriEkle.Text = "";
                                AdresMusteriEkle.Text = "";
                                Form1_Load(sender, e);
                            }
                        }

                    }
                    catch
                    {
                        try
                        {
                            Musteriler ekle = new Musteriler();
                            ekle.MusteriAdi = MusteriAdiMusteriEkle.Text;
                            ekle.MusteriAdresi = AdresMusteriEkle.Text;
                            db.Musteriler.Add(ekle);
                            db.SaveChanges();
                            MessageBox.Show("Müşteri Başarıyla Eklendi!");
                            MusteriAdiMusteriEkle.Text = "";
                            AdresMusteriEkle.Text = "";
                            Form1_Load(sender, e);
                        }
                        catch
                        {
                            MessageBox.Show("Veritabanına Bağlanamadı!");
                        }


                    }
                }
            }

        }  // Müşteri Kaydet.

        private void button21_Click(object sender, EventArgs e)
        {
            MusteriAdiMusteriEkle.Text = "";
            AdresMusteriEkle.Text = "";
        }

        private void UrunEkleButon_Click(object sender, EventArgs e)
        {
            if (UrunadiUrunEkle.Text == "")
            {
                MessageBox.Show("Ürün Adını Girin!");
            }
            else
            {
                if (UrunAdediUrunEkle.Text == "")
                {
                    MessageBox.Show("Ürün Adedini Girin!");
                }
                else
                {
                    if (UrunFiyatiUrunEkle.Text == "")
                    {
                        MessageBox.Show("Ürün Fiyatını Girin!");
                    }
                    else
                    {
                        try
                        {
                            string urungramaji = UrunGramajiUrunEkle.SelectedItem.ToString();
                            string urunpaketi = AmbalajUrunEkle.SelectedItem.ToString();
                            var bul = db.Urunler.Where(p => p.UrunAdi == UrunadiUrunEkle.Text && p.UrunGramaji == urungramaji && p.UrunPaketi == urunpaketi).FirstOrDefault();
                            if (bul.UrunAdi == UrunadiUrunEkle.Text && bul.UrunGramaji == urungramaji && bul.UrunPaketi == urunpaketi)
                            {
                                MessageBox.Show("Bu ürün Daha Önceden Kaydedilmiş!, Eğer Ürünün Bilgilerini Güncellemek istiyorsanız ürün ismine çift tıklayarak ürün güncelle Panosundan işlemlerinizi yapabilirsiniz!", "UYARI!");
                            }

                        }
                        catch
                        {
                            try
                            {
                                string secimial;
                                Uretim ekle1 = new Uretim();
                                Urunler ekle = new Urunler();
                                ekle.UrunAdi = UrunadiUrunEkle.Text;
                                ekle.UrunAdedi = UrunAdediUrunEkle.Text;
                                ekle.UrunBarkodu = UrunbarkoduUrunEkle.Text;
                                ekle.UrunFiyati = UrunFiyatiUrunEkle.Text;
                                if (UrunGramajiUrunEkle.SelectedItem != UrunGramajiUrunEkle.Text)
                                {
                                    ekle.UrunGramaji = UrunGramajiUrunEkle.Text;
                                    secimial = UrunGramajiUrunEkle.Text;
                                }
                                else
                                {
                                    ekle.UrunGramaji = UrunGramajiUrunEkle.SelectedItem.ToString();
                                    secimial = UrunGramajiUrunEkle.SelectedItem.ToString();
                                }
                                ekle.UrunPaketi = AmbalajUrunEkle.SelectedItem.ToString();
                                ekle.UrunEklemeTarihi = DateTime.Now.ToShortDateString();
                                db.Urunler.Add(ekle);
                                ekle1.Ay = DateTime.Now.Month.ToString();
                                ekle1.Gun = DateTime.Now.Day.ToString();
                                ekle1.UrunAdedi = UrunAdediUrunEkle.Text;
                                ekle1.UrunAdi = UrunadiUrunEkle.Text;
                                ekle1.UrunAmbalaji = AmbalajUrunEkle.SelectedItem.ToString();
                                ekle1.UrunGramaji = secimial;
                                ekle1.UrunUretimTarihi = DateTime.Now.ToShortDateString();
                                ekle1.Yil = DateTime.Now.Year.ToString();
                                db1.Uretim.Add(ekle1);
                                db1.SaveChanges();
                                db.SaveChanges();
                                MessageBox.Show("Ürün Başarıyla Eklendi!");
                                UrunadiUrunEkle.Text = "";
                                UrunAdediUrunEkle.Text = "";
                                UrunbarkoduUrunEkle.Text = "";
                                UrunFiyatiUrunEkle.Text = "0";
                                Form1_Load(sender, e);
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

        private void button12_Click(object sender, EventArgs e)
        {
            ExcelUyari ac = new ExcelUyari();
            ac.Show();
            ac.Visible = false;
            try
            {
                ac.Visible = true;
                Excel.Application excel = new Excel.Application();
                excel.Visible = true;
                object Missing = Type.Missing;
                Workbook workbook = excel.Workbooks.Add(Missing);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                int StartCol = 1;
                int StartRow = 1;
                for (int j = 0; j < dataGridView5.Columns.Count; j++)
                {
                    Range myRange = (Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = dataGridView5.Columns[j].HeaderText;
                }
                StartRow++;
                for (int i = 0; i < dataGridView5.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView5.Columns.Count; j++)
                    {

                        Range myRange = (Range)sheet1.Cells[StartRow + i, StartCol + j];
                        myRange.Value2 = dataGridView5[j, i].Value == null ? "" : dataGridView5[j, i].Value;
                        myRange.Select();
                    }
                }
                ac.Close();
            }
            catch
            {
                MessageBox.Show("Bilgisayarınızda Microsoft Office 2016 Yüklü değil. İşlem İptal Edildi");
                ac.Close();
            }

        }

        private void IptalButonUrunEkle_Click(object sender, EventArgs e)
        {
            UrunadiUrunEkle.Text = "";
            UrunAdediUrunEkle.Text = "";
            UrunbarkoduUrunEkle.Text = "";
            UrunFiyatiUrunEkle.Text = "0";
            Form1_Load(sender, e);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            ExcelUyari ac = new ExcelUyari();
            ac.Show();
            ac.Visible = false;
            try
            {
                ac.Visible = true;
                Excel.Application excel = new Excel.Application();
                excel.Visible = true;
                object Missing = Type.Missing;
                Workbook workbook = excel.Workbooks.Add(Missing);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                int StartCol = 1;
                int StartRow = 1;
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    Range myRange = (Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = dataGridView1.Columns[j].HeaderText;
                }
                StartRow++;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {

                        Range myRange = (Range)sheet1.Cells[StartRow + i, StartCol + j];
                        myRange.Value2 = dataGridView1[j, i].Value == null ? "" : dataGridView1[j, i].Value;
                        myRange.Select();
                    }
                }
                ac.Close();
            }
            catch
            {
                MessageBox.Show("Bilgisayarınızda Microsoft Office 2016 Yüklü değil. İşlem İptal Edildi");
                ac.Close();
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 6;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult Uyari = new DialogResult();
            Uyari = MessageBox.Show("Siparişler İptal Edilecek Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
            if (Uyari == DialogResult.Yes)
            {
                try
                {
                    var iptal = db2.Sepet.ToList();
                    foreach (var m in iptal)
                    {
                        var bul = db.Urunler.Where(p => p.UrunAdi == m.UrunAdi && p.UrunGramaji == m.UrunGramaji && p.UrunPaketi == m.UrunPaketi).FirstOrDefault();
                        double adet = Convert.ToDouble(m.UrunAdedi);
                        double stokadet = Convert.ToDouble(bul.UrunAdedi);
                        double topla = stokadet + adet;
                        bul.UrunAdedi = topla.ToString();
                        db2.Sepet.Remove(m);
                        db2.SaveChanges();
                        db.SaveChanges();
                    }
                    MessageBox.Show("Sipariş Sepetindeki Ürün Silindi!");
                    textBox2.Text = "";
                    textBox16.Text = "";
                    sayac = 0;
                    //textBox4.Text = "0";
                    textBox5.Text = "0";
                    textBox6.Text = "0";
                    Form1_Load(sender, e);
                }
                catch
                {
                    MessageBox.Show("Sipariş Sepetinde Ürün Zaten Yok!");
                }
            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            MusteriSecmeEkrani musac = new MusteriSecmeEkrani();
            musac.Show();
            musac.counter = "1";
        }

        private void button2_Click(object sender, EventArgs e)  // sipariş sepetinden gönder
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("ÖNCE MÜŞTERİ SEÇİNİZ!");
            }
            else
            {
                if (comboBox3.SelectedItem.ToString() == "FATURALI" || comboBox3.SelectedItem.ToString() == "İADE")
                {
                    if (textBox16.Text == "")
                    {
                        MessageBox.Show("Fatura Numarası girmemişsiniz!");
                        return;
                    }
                    else
                    {
                        try
                        {
                            var faturabul = db.Raporlama.Where(p => p.FaturaDurumu == textBox16.Text).FirstOrDefault();

                            if (faturabul.FaturaDurumu == textBox16.Text)
                            {
                                MessageBox.Show("Bu Fatura Numarası ile Daha Önceden Kayıt Olmuş. Lütfen Fatura Numarasını Değiştirin.");
                                return;
                            }
                        }
                        catch
                        {
                        }
                    }

                }
                DialogResult Uyari = new DialogResult();
                Uyari = MessageBox.Show("Siparişiniz Tamamlanacak Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                if (Uyari == DialogResult.Yes)
                {
                    try
                    {
                        Sepet sil = new Sepet();
                        var al = db2.Sepet.ToList();
                        foreach (var m in al)
                        {
                            Raporlama ekle = new Raporlama();
                            ekle.GidenMusteriler = textBox2.Text;
                            ekle.Gun = DateTime.Now.Day.ToString();
                            ekle.Tarih = DateTime.Now.ToShortDateString();
                            ekle.Ay = DateTime.Now.Month.ToString();
                            ekle.Yil = DateTime.Now.Year.ToString();
                            ekle.GidenUrunler = m.UrunAdi;
                            ekle.UrunGramaji = m.UrunGramaji;
                            ekle.UrunAdedi = m.UrunAdedi;
                            ekle.UrunPaketi = m.UrunPaketi;
                            ekle.Fiyati = m.UrunFiyati;
                            if (comboBox3.SelectedItem.ToString() == "FATURALI")
                            {
                                if (m.UrunFaturası == "BEDELSİZ")
                                {
                                    ekle.FaturaDurumu = m.UrunFaturası;
                                }
                                else
                                {
                                    ekle.FaturaDurumu = textBox16.Text;
                                }
                            }
                            else if (comboBox3.SelectedItem.ToString() == "İADE")
                            {
                                if (m.UrunFaturası == "BEDELSİZ")
                                {
                                    ekle.FaturaDurumu = m.UrunFaturası;
                                }
                                else
                                {
                                    ekle.FaturaDurumu = textBox16.Text+" İADE EDİLEN ÜRÜN";
                                }
                            }
                            else
                            {
                                if (m.UrunFaturası == "BEDELSİZ")
                                {
                                    ekle.FaturaDurumu = m.UrunFaturası;
                                }
                                else
                                {
                                    ekle.FaturaDurumu = comboBox3.SelectedItem.ToString();
                                }
                            }
                            db.Raporlama.Add(ekle);
                            db2.Sepet.Remove(m);
                            db2.SaveChanges();
                            db.SaveChanges();
                        }
                        textBox2.Text = "";
                        textBox16.Text = "";
                        sayac = 0;
                        //textBox4.Text = "0";
                        textBox5.Text = "0";
                        textBox6.Text = "0";
                        Form1_Load(sender, e);
                        gridControl4.RefreshDataSource();
                        MessageBox.Show("Ürünler Sepete Eklendi!");
                    }
                    catch
                    {
                        MessageBox.Show("Önce Sepete Ürün Ekleyin!");
                    }
                }
            }
        }

        private void button24_Click(object sender, EventArgs e)  // hepsini göster
        {
            RaporlamaKontrol = "HEPSİ";
            RAPORLAMADURUMUGOSTER.Text = "Seçilen Tarih Aralığına Göre Rapolama Gösteriliyor!";
            string seciliay = Convert.ToDateTime(dateTimePicker2.Text).Month.ToString();
            if (checkBox2.Checked == true)
            {
                dataGridView6.Rows.Clear();

                try
                {
                    var bul = db.Raporlama.Where(p => p.Ay == seciliay).ToList();
                    int i = 0;
                    foreach (var m in bul)
                    {
                        dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = m.GidenMusteriler;
                        dataGridView6.Rows[i].Cells[1].Value = m.GidenUrunler;
                        dataGridView6.Rows[i].Cells[2].Value = m.UrunGramaji;
                        dataGridView6.Rows[i].Cells[3].Value = m.UrunAdedi;
                        dataGridView6.Rows[i].Cells[4].Value = m.UrunPaketi;
                        dataGridView6.Rows[i].Cells[5].Value = m.Fiyati;
                        dataGridView6.Rows[i].Cells[6].Value = m.FaturaDurumu;
                        dataGridView6.Rows[i].Cells[7].Value = m.Tarih;
                        i++;
                    }
                }
                catch
                {

                }
            }
            else
            {
                int n = 0;
                dataGridView6.Rows.Clear();
                DateTime ilktarih = this.dateTimePicker2.Value.Date;
                DateTime sontarih = this.dateTimePicker1.Value.Date;
                if (ilktarih.ToShortDateString() == sontarih.ToShortDateString())
                {
                    try
                    {
                        string tarih = ilktarih.ToShortDateString();
                        var bul = db.Raporlama.Where(p => p.Tarih == tarih).ToList();
                        foreach (var m in bul)
                        {
                            dataGridView6.Rows.Add();
                            dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                            dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                            dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                            dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                            dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                            dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                            dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                            dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                            n++;
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    for (int i = 0; i <= (sontarih - ilktarih).Days; i++)
                    {
                        string tarih = ilktarih.AddDays(i).ToShortDateString();
                        try
                        {
                            var bul = db.Raporlama.Where(p => p.Tarih == tarih).ToList();
                            foreach (var m in bul)
                            {
                                dataGridView6.Rows.Add();
                                dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                                dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                                dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                                dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                                dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                                dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                                dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                                dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                                n++;
                            }
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            dataGridView6.Rows.Clear();
            textBox7.Text = "";
            RaporlamaKontrol = "OFFLINE";
            RAPORLAMADURUMUGOSTER.Text = "RAPORLAMA AKTİF DEĞİL!";
        }

        private void button31_Click(object sender, EventArgs e)
        {
            ExcelUyari ac = new ExcelUyari();
            ac.Show();
            ac.Visible = false;
            try
            {
                ac.Visible = true;
                Excel.Application excel = new Excel.Application();
                excel.Visible = true;
                object Missing = Type.Missing;
                Workbook workbook = excel.Workbooks.Add(Missing);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                int StartCol = 1;
                int StartRow = 1;
                for (int j = 0; j < dataGridView6.Columns.Count; j++)
                {
                    Range myRange = (Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = dataGridView6.Columns[j].HeaderText;
                }
                StartRow++;
                for (int i = 0; i < dataGridView6.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView6.Columns.Count; j++)
                    {

                        Range myRange = (Range)sheet1.Cells[StartRow + i, StartCol + j];
                        myRange.Value2 = dataGridView6[j, i].Value == null ? "" : dataGridView6[j, i].Value;
                        myRange.Select();
                    }
                }
                ac.Close();
            }
            catch
            {
                MessageBox.Show("Bilgisayarınızda Microsoft Office 2016 Yüklü değil. İşlem İptal Edildi");
                ac.Close();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (f21.yenidenbaslama != true)
            {
                f21.Close();
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e) // çift tıklama sahasaı.
        {
            string str = gridView1.FocusedValue.ToString();
            string Urungramajinial = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "UrunGramaji").ToString();
            string urunpaketinial = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "UrunPaketi").ToString();
            try
            {
                var ekle = db.Urunler.Where(p => p.UrunAdi == str && p.UrunGramaji == Urungramajinial && p.UrunPaketi == urunpaketinial).FirstOrDefault();
                if (ekle.UrunAdi == str)
                {
                    UrunEklemeEkrani ac = new UrunEklemeEkrani();
                    ac.Show();
                    ac.textBox1.Text = ekle.UrunAdi;
                    ac.textBox2.Text = ekle.UrunGramaji;
                    ac.textBox3.Text = ekle.UrunAdedi;
                    ac.textBox5.Text = ekle.UrunPaketi;
                    ac.textBox6.Text = ekle.UrunFiyati;
                }
            }
            catch
            {
                MessageBox.Show("Ürün Bulunamadı!", "UYARI!");
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)  // enter ile basınca
        {
            if (e.KeyCode == Keys.Enter)
            {
                string str = gridView1.FocusedValue.ToString();
                string Urungramajinial = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "UrunGramaji").ToString();
                string urunpaketinial = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "UrunPaketi").ToString();
                try
                {
                    var ekle = db.Urunler.Where(p => p.UrunAdi == str && p.UrunGramaji == Urungramajinial && p.UrunPaketi == urunpaketinial).FirstOrDefault();
                    if (ekle.UrunAdi == str)
                    {
                        UrunEklemeEkrani ac = new UrunEklemeEkrani();
                        ac.Show();
                        ac.textBox1.Text = ekle.UrunAdi;
                        ac.textBox2.Text = ekle.UrunGramaji;
                        ac.textBox3.Text = ekle.UrunAdedi;
                        ac.textBox5.Text = ekle.UrunPaketi;
                        ac.textBox6.Text = ekle.UrunFiyati;
                    }
                }
                catch
                {
                    MessageBox.Show("Ürün Bulunamadı!", "UYARI!");
                }
            }
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
        }

        private void gridView2_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void button25_Click(object sender, EventArgs e)
        {
            MusteriSecmeEkrani AC = new MusteriSecmeEkrani();
            AC.Show();
            AC.counter = "2";
        }

        private void button29_Click(object sender, EventArgs e) //Müşteriye göre raporlama
        {
            RaporlamaKontrol = "MÜŞTERİ";
            RAPORLAMADURUMUGOSTER.Text = "Müşteriye Göre Rapolama Gösteriliyor!";
            string seciliay = Convert.ToDateTime(dateTimePicker2.Text).Month.ToString();
            if (checkBox2.Checked == true)
            {
                dataGridView6.Rows.Clear();
                try
                {
                    var musteri = db.Raporlama.Where(p => p.GidenMusteriler == textBox7.Text && p.Ay == seciliay).ToList();
                    int i = 0;
                    foreach (var m in musteri)
                    {
                        dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = m.GidenMusteriler;
                        dataGridView6.Rows[i].Cells[1].Value = m.GidenUrunler;
                        dataGridView6.Rows[i].Cells[2].Value = m.UrunGramaji;
                        dataGridView6.Rows[i].Cells[3].Value = m.UrunAdedi;
                        dataGridView6.Rows[i].Cells[4].Value = m.UrunPaketi;
                        dataGridView6.Rows[i].Cells[5].Value = m.Fiyati;
                        dataGridView6.Rows[i].Cells[6].Value = m.FaturaDurumu;
                        dataGridView6.Rows[i].Cells[7].Value = m.Tarih;
                        i++;
                    }
                }
                catch
                {
                    MessageBox.Show("Bulunamadı!");
                }
            }
            else
            {
                int n = 0;
                dataGridView6.Rows.Clear();
                DateTime ilktarih = this.dateTimePicker2.Value.Date;
                DateTime sontarih = this.dateTimePicker1.Value.Date;
                if (ilktarih.ToShortDateString() == sontarih.ToShortDateString())
                {
                    try
                    {
                        string tarih = ilktarih.ToShortDateString();
                        var bul = db.Raporlama.Where(p => p.GidenMusteriler == textBox7.Text && p.Tarih == tarih).ToList();
                        foreach (var m in bul)
                        {
                            dataGridView6.Rows.Add();
                            dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                            dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                            dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                            dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                            dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                            dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                            dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                            dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                            n++;
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    for (int i = 0; i <= (sontarih - ilktarih).Days; i++)
                    {
                        string tarih = ilktarih.AddDays(i).ToShortDateString();
                        try
                        {
                            var bul = db.Raporlama.Where(p => p.GidenMusteriler == textBox7.Text && p.Tarih == tarih).ToList();
                            foreach (var m in bul)
                            {
                                dataGridView6.Rows.Add();
                                dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                                dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                                dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                                dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                                dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                                dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                                dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                                dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                                n++;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            dataGridView6.Rows.Clear();
            textBox7.Text = "";
            RaporlamaKontrol = "OFFLINE";
            RAPORLAMADURUMUGOSTER.Text = "RAPORLAMA AKTİF DEĞİL!";
        }

        private void button30_Click(object sender, EventArgs e)  // FATURASIZ RAPOR
        {
            RaporlamaKontrol = "FATURASIZ";
            RAPORLAMADURUMUGOSTER.Text = "Faturasız Rapolama Gösteriliyor!";
            string seciliay = Convert.ToDateTime(dateTimePicker2.Text).Month.ToString();
            if (checkBox2.Checked == true)
            {
                dataGridView6.Rows.Clear();
                try
                {
                    var musteri = db.Raporlama.Where(p => p.FaturaDurumu == "FATURASIZ" && p.Ay == seciliay).ToList();
                    int i = 0;
                    foreach (var m in musteri)
                    {
                        dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = m.GidenMusteriler;
                        dataGridView6.Rows[i].Cells[1].Value = m.GidenUrunler;
                        dataGridView6.Rows[i].Cells[2].Value = m.UrunGramaji;
                        dataGridView6.Rows[i].Cells[3].Value = m.UrunAdedi;
                        dataGridView6.Rows[i].Cells[4].Value = m.UrunPaketi;
                        dataGridView6.Rows[i].Cells[5].Value = m.Fiyati;
                        dataGridView6.Rows[i].Cells[6].Value = m.FaturaDurumu;
                        dataGridView6.Rows[i].Cells[7].Value = m.Tarih;
                        i++;
                    }
                    Form1_Load(sender, e);
                }
                catch
                {
                    MessageBox.Show("Bulunamadı!");
                }
            }
            else
            {
                int n = 0;
                dataGridView6.Rows.Clear();
                DateTime ilktarih = this.dateTimePicker2.Value.Date;
                DateTime sontarih = this.dateTimePicker1.Value.Date;
                if (ilktarih.ToShortDateString() == sontarih.ToShortDateString())
                {
                    try
                    {
                        string tarih = ilktarih.ToShortDateString();
                        var bul = db.Raporlama.Where(p => p.FaturaDurumu == "FATURASIZ" && p.Tarih == tarih).ToList();
                        foreach (var m in bul)
                        {
                            dataGridView6.Rows.Add();
                            dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                            dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                            dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                            dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                            dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                            dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                            dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                            dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                            n++;
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    for (int i = 0; i <= (sontarih - ilktarih).Days; i++)
                    {
                        string tarih = ilktarih.AddDays(i).ToShortDateString();
                        try
                        {
                            var bul = db.Raporlama.Where(p => p.FaturaDurumu == "FATURASIZ" && p.Tarih == tarih).ToList();
                            foreach (var m in bul)
                            {
                                dataGridView6.Rows.Add();
                                dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                                dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                                dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                                dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                                dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                                dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                                dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                                dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                                n++;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        private void button33_Click(object sender, EventArgs e) // Bedelsiz raporlama
        {
            RaporlamaKontrol = "BEDELSİZ";
            RAPORLAMADURUMUGOSTER.Text = "Bedelsiz Rapolama Gösteriliyor!";
            string seciliay = Convert.ToDateTime(dateTimePicker2.Text).Month.ToString();
            if (checkBox2.Checked == true)
            {
                dataGridView6.Rows.Clear();
                try
                {
                    var musteri = db.Raporlama.Where(p => p.FaturaDurumu == "BEDELSİZ" && p.Ay == seciliay).ToList();
                    int i = 0;
                    foreach (var m in musteri)
                    {
                        dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = m.GidenMusteriler;
                        dataGridView6.Rows[i].Cells[1].Value = m.GidenUrunler;
                        dataGridView6.Rows[i].Cells[2].Value = m.UrunGramaji;
                        dataGridView6.Rows[i].Cells[3].Value = m.UrunAdedi;
                        dataGridView6.Rows[i].Cells[4].Value = m.UrunPaketi;
                        dataGridView6.Rows[i].Cells[5].Value = m.Fiyati;
                        dataGridView6.Rows[i].Cells[6].Value = m.FaturaDurumu;
                        dataGridView6.Rows[i].Cells[7].Value = m.Tarih;
                        i++;
                    }
                    Form1_Load(sender, e);
                }
                catch
                {
                    MessageBox.Show("Bulunamadı!");
                }
            }
            else
            {
                int n = 0;
                dataGridView6.Rows.Clear();
                DateTime ilktarih = this.dateTimePicker2.Value.Date;
                DateTime sontarih = this.dateTimePicker1.Value.Date;
                if (ilktarih.ToShortDateString() == sontarih.ToShortDateString())
                {
                    try
                    {
                        string tarih = ilktarih.ToShortDateString();
                        var bul = db.Raporlama.Where(p => p.FaturaDurumu == "BEDELSİZ" && p.Tarih == tarih).ToList();
                        foreach (var m in bul)
                        {
                            dataGridView6.Rows.Add();
                            dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                            dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                            dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                            dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                            dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                            dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                            dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                            dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                            n++;
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    for (int i = 0; i <= (sontarih - ilktarih).Days; i++)
                    {
                        string tarih = ilktarih.AddDays(i).ToShortDateString();
                        try
                        {
                            var bul = db.Raporlama.Where(p => p.FaturaDurumu == "BEDELSİZ" && p.Tarih == tarih).ToList();
                            foreach (var m in bul)
                            {
                                dataGridView6.Rows.Add();
                                dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                                dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                                dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                                dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                                dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                                dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                                dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                                dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                                n++;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            dataGridView6.Rows.Clear();
            textBox7.Text = "";
            RaporlamaKontrol = "OFFLINE";
            RAPORLAMADURUMUGOSTER.Text = "RAPORLAMA AKTİF DEĞİL!";
        }

        private void button6_Click_1(object sender, EventArgs e) //fatura no ile arama raporlama
        {
            RaporlamaKontrol = textBox1.Text;
            RAPORLAMADURUMUGOSTER.Text = "Fatura No İle Rapolama Gösteriliyor!";
            string seciliay = Convert.ToDateTime(dateTimePicker2.Text).Month.ToString();
            if (checkBox2.Checked == true)
            {
                dataGridView6.Rows.Clear();
                try
                {
                    var musteri = db.Raporlama.Where(p => p.FaturaDurumu == textBox1.Text && p.Ay == seciliay).ToList();
                    int i = 0;
                    foreach (var m in musteri)
                    {
                        dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = m.GidenMusteriler;
                        dataGridView6.Rows[i].Cells[1].Value = m.GidenUrunler;
                        dataGridView6.Rows[i].Cells[2].Value = m.UrunGramaji;
                        dataGridView6.Rows[i].Cells[3].Value = m.UrunAdedi;
                        dataGridView6.Rows[i].Cells[4].Value = m.UrunPaketi;
                        dataGridView6.Rows[i].Cells[5].Value = m.Fiyati;
                        dataGridView6.Rows[i].Cells[6].Value = m.FaturaDurumu;
                        dataGridView6.Rows[i].Cells[7].Value = m.Tarih;
                        i++;
                    }
                }
                catch
                {
                    MessageBox.Show("Bulunamadı!");
                }
            }
            else
            {
                int n = 0;
                dataGridView6.Rows.Clear();
                DateTime ilktarih = this.dateTimePicker2.Value.Date;
                DateTime sontarih = this.dateTimePicker1.Value.Date;
                if (ilktarih.ToShortDateString() == sontarih.ToShortDateString())
                {
                    try
                    {
                        string tarih = ilktarih.ToShortDateString();
                        var bul = db.Raporlama.Where(p => p.FaturaDurumu == textBox1.Text && p.Tarih == tarih).ToList();
                        foreach (var m in bul)
                        {
                            dataGridView6.Rows.Add();
                            dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                            dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                            dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                            dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                            dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                            dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                            dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                            dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                            n++;
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    for (int i = 0; i <= (sontarih - ilktarih).Days; i++)
                    {
                        string tarih = ilktarih.AddDays(i).ToShortDateString();
                        try
                        {
                            var bul = db.Raporlama.Where(p => p.FaturaDurumu == textBox1.Text && p.Tarih == tarih).ToList();
                            foreach (var m in bul)
                            {
                                dataGridView6.Rows.Add();
                                dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                                dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                                dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                                dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                                dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                                dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                                dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                                dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                                n++;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) // İade Gelen Ürün raporlama
        {
            RaporlamaKontrol = "İADE GELEN ÜRÜN";
            RAPORLAMADURUMUGOSTER.Text = "İade Gelen Ürün Rapolama Gösteriliyor!";
            string seciliay = Convert.ToDateTime(dateTimePicker2.Text).Month.ToString();
            if (checkBox2.Checked == true)
            {
                dataGridView6.Rows.Clear();
                try
                {
                    var musteri = db.Raporlama.Where(p => p.FaturaDurumu.Contains("İADE GELEN ÜRÜN") && p.Ay == seciliay).ToList();
                    int i = 0;
                    foreach (var m in musteri)
                    {
                        dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = m.GidenMusteriler;
                        dataGridView6.Rows[i].Cells[1].Value = m.GidenUrunler;
                        dataGridView6.Rows[i].Cells[2].Value = m.UrunGramaji;
                        dataGridView6.Rows[i].Cells[3].Value = m.UrunAdedi;
                        dataGridView6.Rows[i].Cells[4].Value = m.UrunPaketi;
                        dataGridView6.Rows[i].Cells[5].Value = m.Fiyati;
                        dataGridView6.Rows[i].Cells[6].Value = m.FaturaDurumu;
                        dataGridView6.Rows[i].Cells[7].Value = m.Tarih;
                        i++;
                    }
                    Form1_Load(sender, e);
                }
                catch
                {
                    MessageBox.Show("Bulunamadı!");
                }
            }
            else
            {
                int n = 0;
                dataGridView6.Rows.Clear();
                DateTime ilktarih = this.dateTimePicker2.Value.Date;
                DateTime sontarih = this.dateTimePicker1.Value.Date;
                if (ilktarih.ToShortDateString() == sontarih.ToShortDateString())
                {
                    try
                    {
                        string tarih = ilktarih.ToShortDateString();
                        var bul = db.Raporlama.Where(p => p.FaturaDurumu.Contains("İADE GELEN ÜRÜN") && p.Tarih == tarih).ToList();
                        foreach (var m in bul)
                        {
                            dataGridView6.Rows.Add();
                            dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                            dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                            dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                            dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                            dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                            dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                            dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                            dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                            n++;
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    for (int i = 0; i <= (sontarih - ilktarih).Days; i++)
                    {
                        string tarih = ilktarih.AddDays(i).ToShortDateString();
                        try
                        {
                            var bul = db.Raporlama.Where(p => p.FaturaDurumu.Contains("İADE GELEN ÜRÜN") && p.Tarih == tarih).ToList();
                            foreach (var m in bul)
                            {
                                dataGridView6.Rows.Add();
                                dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                                dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                                dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                                dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                                dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                                dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                                dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                                dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                                n++;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView6_CellDoubleClick(object sender, DataGridViewCellEventArgs e)   // Raporlar bölgesi
        {
            
            if(RaporlamaKontrol== "OFFLINE") { MessageBox.Show("BU LİSTE DEĞİŞTİRİLEMEZ!"); }
            else
            {
                string GOREV = RaporlamaKontrol;
                int xkoordinat = dataGridView6.CurrentCellAddress.X; //Seçili satırın X koordinatı
                int ykoordinat = dataGridView6.CurrentCellAddress.Y;  //Seçili satırın Y koordinatı
                string str = "";
                str = dataGridView6.Rows[ykoordinat].Cells[xkoordinat].Value.ToString();
                if (e.RowIndex == -1)
                {
                    return;
                }

                try
                {
                    GidenMusteriDuzenle ac = new GidenMusteriDuzenle();
                    ac.Show();
                    ac.textBox1.Text = str;
                    ac.dataGridView1.Columns.Add("1", "ÜRÜN ADI");
                    ac.dataGridView1.Columns.Add("2", "ÜRÜN ADEDİ");
                    ac.dataGridView1.Columns.Add("3", "ÜRÜN FİYATI");
                    ac.dataGridView1.Columns.Add("4", "ÜRÜN GRAMAJI");
                    ac.dataGridView1.Columns.Add("5", "ÜRÜN PAKETİ");
                    ac.dataGridView1.Columns.Add("6", "ÜRÜN FATURASI");
                    ac.dataGridView1.Columns.Add("7", "ÜRÜN TARİHİ");

                    string selicideger = Convert.ToDateTime(dateTimePicker2.Text).ToShortDateString();
                    string seciliay = Convert.ToDateTime(dateTimePicker2.Text).Month.ToString();
                    double adet, sonuc = 0, fiyat, fiyatsonuc = 0, gramaj, toplamgramaj = 0;
                    if (checkBox2.Checked == true)
                    {
                        if (GOREV == "HEPSİ" || GOREV == "MÜŞTERİ" || GOREV == "FATURASIZ ÖDEME" || GOREV == "ÜRÜN" || GOREV == "ZAYİ" || GOREV == "NUMUNE" || GOREV == "İADE GELEN ÜRÜN" || GOREV == "İADE GİDEN ÜRÜN")
                        {
                            var bul = db.Raporlama.Where(p => p.GidenMusteriler == str && p.Ay == seciliay).ToList();
                            int sayac = 0;
                            foreach (var n in bul)
                            {
                                ac.dataGridView1.Rows.Add();
                                ac.dataGridView1.Rows[sayac].Cells[0].Value = n.GidenUrunler;
                                ac.dataGridView1.Rows[sayac].Cells[1].Value = n.UrunAdedi;
                                ac.dataGridView1.Rows[sayac].Cells[2].Value = n.Fiyati;
                                ac.dataGridView1.Rows[sayac].Cells[3].Value = n.UrunGramaji;
                                ac.dataGridView1.Rows[sayac].Cells[4].Value = n.UrunPaketi;
                                ac.dataGridView1.Rows[sayac].Cells[5].Value = n.FaturaDurumu;
                                ac.dataGridView1.Rows[sayac].Cells[6].Value = n.Tarih;
                                adet = Convert.ToDouble(n.UrunAdedi);
                                sonuc = adet + sonuc;
                                ac.textBox2.Text = sonuc.ToString();
                                fiyat = Convert.ToDouble(n.Fiyati);
                                fiyatsonuc = fiyat + fiyatsonuc;
                                ac.textBox5.Text = fiyatsonuc.ToString();
                                gramaj = Convert.ToDouble(n.UrunGramaji);
                                toplamgramaj = gramaj + toplamgramaj;
                                ac.textBox4.Text = toplamgramaj.ToString();
                                sayac++;
                            }
                        }
                        else {
                            var bul = db.Raporlama.Where(p => p.GidenMusteriler == str && p.Ay == seciliay && p.FaturaDurumu == GOREV).ToList();
                            int sayac = 0;
                            foreach (var n in bul)
                            {
                                ac.dataGridView1.Rows.Add();
                                ac.dataGridView1.Rows[sayac].Cells[0].Value = n.GidenUrunler;
                                ac.dataGridView1.Rows[sayac].Cells[1].Value = n.UrunAdedi;
                                ac.dataGridView1.Rows[sayac].Cells[2].Value = n.Fiyati;
                                ac.dataGridView1.Rows[sayac].Cells[3].Value = n.UrunGramaji;
                                ac.dataGridView1.Rows[sayac].Cells[4].Value = n.UrunPaketi;
                                ac.dataGridView1.Rows[sayac].Cells[5].Value = n.FaturaDurumu;
                                ac.dataGridView1.Rows[sayac].Cells[6].Value = n.Tarih;
                                adet = Convert.ToDouble(n.UrunAdedi);
                                sonuc = adet + sonuc;
                                ac.textBox2.Text = sonuc.ToString();
                                fiyat = Convert.ToDouble(n.Fiyati);
                                fiyatsonuc = fiyat + fiyatsonuc;
                                ac.textBox5.Text = fiyatsonuc.ToString();
                                gramaj = Convert.ToDouble(n.UrunGramaji);
                                toplamgramaj = gramaj + toplamgramaj;
                                ac.textBox4.Text = toplamgramaj.ToString();
                                sayac++;
                            }
                        }
                    }
                    else
                    {
                        DateTime ilktarih = this.dateTimePicker2.Value.Date;
                        DateTime sontarih = this.dateTimePicker1.Value.Date;
                        if (ilktarih.ToShortDateString() == sontarih.ToShortDateString())
                        {
                            if (GOREV == "HEPSİ" || GOREV == "MÜŞTERİ" || GOREV == "FATURASIZ ÖDEME" || GOREV == "ÜRÜN" || GOREV == "ZAYİ" || GOREV == "NUMUNE" || GOREV == "İADE GELEN ÜRÜN" || GOREV == "İADE GİDEN ÜRÜN")
                            {
                                var bul = db.Raporlama.Where(p => p.GidenMusteriler == str && p.Tarih == selicideger).ToList();
                                int sayac = 0;
                                foreach (var n in bul)
                                {
                                    ac.dataGridView1.Rows.Add();
                                    ac.dataGridView1.Rows[sayac].Cells[0].Value = n.GidenUrunler;
                                    ac.dataGridView1.Rows[sayac].Cells[1].Value = n.UrunAdedi;
                                    ac.dataGridView1.Rows[sayac].Cells[2].Value = n.Fiyati;
                                    ac.dataGridView1.Rows[sayac].Cells[3].Value = n.UrunGramaji;
                                    ac.dataGridView1.Rows[sayac].Cells[4].Value = n.UrunPaketi;
                                    ac.dataGridView1.Rows[sayac].Cells[5].Value = n.FaturaDurumu;
                                    ac.dataGridView1.Rows[sayac].Cells[6].Value = n.Tarih;
                                    adet = Convert.ToDouble(n.UrunAdedi);
                                    sonuc = adet + sonuc;
                                    ac.textBox2.Text = sonuc.ToString();
                                    fiyat = Convert.ToDouble(n.Fiyati);
                                    fiyatsonuc = fiyat + fiyatsonuc;
                                    ac.textBox5.Text = fiyatsonuc.ToString();
                                    gramaj = Convert.ToDouble(n.UrunGramaji);
                                    toplamgramaj = gramaj + toplamgramaj;
                                    ac.textBox4.Text = toplamgramaj.ToString();
                                    sayac++;
                                }
                            }
                            else
                            {
                                var bul = db.Raporlama.Where(p => p.GidenMusteriler == str && p.Tarih == selicideger && p.FaturaDurumu == GOREV).ToList();
                                int sayac = 0;
                                foreach (var n in bul)
                                {
                                    ac.dataGridView1.Rows.Add();
                                    ac.dataGridView1.Rows[sayac].Cells[0].Value = n.GidenUrunler;
                                    ac.dataGridView1.Rows[sayac].Cells[1].Value = n.UrunAdedi;
                                    ac.dataGridView1.Rows[sayac].Cells[2].Value = n.Fiyati;
                                    ac.dataGridView1.Rows[sayac].Cells[3].Value = n.UrunGramaji;
                                    ac.dataGridView1.Rows[sayac].Cells[4].Value = n.UrunPaketi;
                                    ac.dataGridView1.Rows[sayac].Cells[5].Value = n.FaturaDurumu;
                                    ac.dataGridView1.Rows[sayac].Cells[6].Value = n.Tarih;
                                    adet = Convert.ToDouble(n.UrunAdedi);
                                    sonuc = adet + sonuc;
                                    ac.textBox2.Text = sonuc.ToString();
                                    fiyat = Convert.ToDouble(n.Fiyati);
                                    fiyatsonuc = fiyat + fiyatsonuc;
                                    ac.textBox5.Text = fiyatsonuc.ToString();
                                    gramaj = Convert.ToDouble(n.UrunGramaji);
                                    toplamgramaj = gramaj + toplamgramaj;
                                    ac.textBox4.Text = toplamgramaj.ToString();
                                    sayac++;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i <= (sontarih - ilktarih).Days; i++)
                            {
                                string tarih = ilktarih.AddDays(i).ToShortDateString();
                                if (GOREV == "HEPSİ" || GOREV == "MÜŞTERİ" || GOREV == "FATURASIZ ÖDEME" || GOREV == "ÜRÜN" || GOREV == "ZAYİ" || GOREV == "NUMUNE" || GOREV == "İADE GELEN ÜRÜN" || GOREV == "İADE GİDEN ÜRÜN")
                                {
                                    var bul = db.Raporlama.Where(p => p.GidenMusteriler == str && p.Tarih == tarih).ToList();
                                    int sayac = 0;
                                    foreach (var n in bul)
                                    {
                                        ac.dataGridView1.Rows.Add();
                                        ac.dataGridView1.Rows[sayac].Cells[0].Value = n.GidenUrunler;
                                        ac.dataGridView1.Rows[sayac].Cells[1].Value = n.UrunAdedi;
                                        ac.dataGridView1.Rows[sayac].Cells[2].Value = n.Fiyati;
                                        ac.dataGridView1.Rows[sayac].Cells[3].Value = n.UrunGramaji;
                                        ac.dataGridView1.Rows[sayac].Cells[4].Value = n.UrunPaketi;
                                        ac.dataGridView1.Rows[sayac].Cells[5].Value = n.FaturaDurumu;
                                        ac.dataGridView1.Rows[sayac].Cells[6].Value = n.Tarih;
                                        adet = Convert.ToDouble(n.UrunAdedi);
                                        sonuc = adet + sonuc;
                                        ac.textBox2.Text = sonuc.ToString();
                                        fiyat = Convert.ToDouble(n.Fiyati);
                                        fiyatsonuc = fiyat + fiyatsonuc;
                                        ac.textBox5.Text = fiyatsonuc.ToString();
                                        gramaj = Convert.ToDouble(n.UrunGramaji);
                                        toplamgramaj = gramaj + toplamgramaj;
                                        ac.textBox4.Text = toplamgramaj.ToString();
                                        sayac++;
                                    }
                                }
                                else
                                {
                                    var bul = db.Raporlama.Where(p => p.GidenMusteriler == str && p.Tarih == tarih && p.FaturaDurumu == GOREV).ToList();
                                    int sayac = 0;
                                    foreach (var n in bul)
                                    {
                                        ac.dataGridView1.Rows.Add();
                                        ac.dataGridView1.Rows[sayac].Cells[0].Value = n.GidenUrunler;
                                        ac.dataGridView1.Rows[sayac].Cells[1].Value = n.UrunAdedi;
                                        ac.dataGridView1.Rows[sayac].Cells[2].Value = n.Fiyati;
                                        ac.dataGridView1.Rows[sayac].Cells[3].Value = n.UrunGramaji;
                                        ac.dataGridView1.Rows[sayac].Cells[4].Value = n.UrunPaketi;
                                        ac.dataGridView1.Rows[sayac].Cells[5].Value = n.FaturaDurumu;
                                        ac.dataGridView1.Rows[sayac].Cells[6].Value = n.Tarih;
                                        adet = Convert.ToDouble(n.UrunAdedi);
                                        sonuc = adet + sonuc;
                                        ac.textBox2.Text = sonuc.ToString();
                                        fiyat = Convert.ToDouble(n.Fiyati);
                                        fiyatsonuc = fiyat + fiyatsonuc;
                                        ac.textBox5.Text = fiyatsonuc.ToString();
                                        gramaj = Convert.ToDouble(n.UrunGramaji);
                                        toplamgramaj = gramaj + toplamgramaj;
                                        ac.textBox4.Text = toplamgramaj.ToString();
                                        sayac++;
                                    }
                                }
                            }
                        }
                    }
                    }
                
                catch
                {
                    MessageBox.Show("Seçmek İçin Ürün İsmine Çift Tıklayın");
                }
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Urunegorelistele ac = new Urunegorelistele();
            ac.Show();
            ac.gelenurunrapor = "1";
            RaporlamaKontrol = "ÜRÜN";
            RAPORLAMADURUMUGOSTER.Text = "ÜRÜNE GÖRE Rapolama Gösteriliyor!";
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {
        }

        private void HizliStokUrunAdedi_TextChanged(object sender, EventArgs e)
        {
        }

        private void gridView2_DoubleClick_1(object sender, EventArgs e)
        {
            string str = gridView2.FocusedValue.ToString();
            string Urungramajinial = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "UrunGramaji").ToString();
            string urunpaketinial = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "UrunPaketi").ToString();
            try
            {
                var ekle = db.Urunler.Where(p => p.UrunAdi == str && p.UrunGramaji == Urungramajinial && p.UrunPaketi == urunpaketinial).FirstOrDefault();
                if (ekle.UrunAdi == str)
                {
                    Uruneklemetektus ac = new Uruneklemetektus();
                    ac.Show();
                    ac.label3.Text = ekle.UrunAdi;
                    ac.label4.Text = ekle.UrunGramaji;
                    ac.label5.Text = ekle.UrunPaketi;
                    ac.UrunAdiUrunDuzenle.Text = ekle.UrunAdi;
                    ac.UrunGramajıUrunDuzenle.SelectedItem = ekle.UrunGramaji;
                    ac.UrunAdediUrunDuzenle.Text = ekle.UrunAdedi;
                    ac.AmbalajUrunDuzenle.SelectedItem = ekle.UrunPaketi;
                    ac.UrunFiyatiUrunDuzenle.Text = ekle.UrunFiyati;
                }
            }
            catch
            {
                MessageBox.Show("Ürün Bulunamadı!", "UYARI!");
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            RaporlamaKontrol = "OFFLINE";
            RAPORLAMADURUMUGOSTER.Text = "Üretilen Tüm Ürünlere Göre Rapolama Gösteriliyor!";
            string seciliay = Convert.ToDateTime(dateTimePicker2.Text).Month.ToString();
            if (checkBox2.Checked == true)
            {
                dataGridView6.Rows.Clear();

                try
                {
                    var bul = db1.Uretim.Where(p => p.Ay == seciliay).ToList();
                    int i = 0;
                    foreach (var m in bul)
                    {
                        dataGridView6.Rows.Add();
                        if (m.Uretici == null)
                        {
                            dataGridView6.Rows[i].Cells[0].Value = "MRC ÜRETİM";
                            dataGridView6.Rows[i].Cells[6].Value = "###";
                        }
                        else
                        {
                            dataGridView6.Rows[i].Cells[0].Value = m.Uretici;
                            dataGridView6.Rows[i].Cells[6].Value = m.UreticiFaturasi;
                        }

                        dataGridView6.Rows[i].Cells[1].Value = m.UrunAdi;
                        dataGridView6.Rows[i].Cells[2].Value = m.UrunGramaji;
                        dataGridView6.Rows[i].Cells[3].Value = m.UrunAdedi;
                        dataGridView6.Rows[i].Cells[4].Value = m.UrunAmbalaji;
                        dataGridView6.Rows[i].Cells[5].Value = "###";
                        dataGridView6.Rows[i].Cells[7].Value = m.UrunUretimTarihi;
                        i++;
                    }
                }
                catch
                {
                }
            }
            else
            {
                int n = 0;
                dataGridView6.Rows.Clear();
                DateTime ilktarih = this.dateTimePicker2.Value.Date;
                DateTime sontarih = this.dateTimePicker1.Value.Date;
                if (ilktarih.ToShortDateString() == sontarih.ToShortDateString())
                {
                    try
                    {
                        string tarih = ilktarih.ToShortDateString();
                        var bul = db1.Uretim.Where(p => p.UrunUretimTarihi == tarih).ToList();
                        foreach (var m in bul)
                        {
                            dataGridView6.Rows.Add();
                            if (m.Uretici == null)
                            {
                                dataGridView6.Rows[n].Cells[0].Value = "MRC ÜRETİM";
                                dataGridView6.Rows[n].Cells[6].Value = "###";
                            }
                            else
                            {
                                dataGridView6.Rows[n].Cells[0].Value = m.Uretici;
                                dataGridView6.Rows[n].Cells[6].Value = m.UreticiFaturasi;
                            }

                            dataGridView6.Rows[n].Cells[1].Value = m.UrunAdi;
                            dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                            dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                            dataGridView6.Rows[n].Cells[4].Value = m.UrunAmbalaji;
                            dataGridView6.Rows[n].Cells[5].Value = "###";
                            dataGridView6.Rows[n].Cells[7].Value = m.UrunUretimTarihi;
                            n++;
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    for (int i = 0; i <= (sontarih - ilktarih).Days; i++)
                    {
                        string tarih = ilktarih.AddDays(i).ToShortDateString();
                        try
                        {
                            var bul = db1.Uretim.Where(p => p.UrunUretimTarihi == tarih).ToList();
                            foreach (var m in bul)
                            {
                                dataGridView6.Rows.Add();
                                if (m.Uretici == null)
                                {
                                    dataGridView6.Rows[n].Cells[0].Value = "MRC ÜRETİM";
                                    dataGridView6.Rows[n].Cells[6].Value = "###";
                                }
                                else
                                {
                                    dataGridView6.Rows[n].Cells[0].Value = m.Uretici;
                                    dataGridView6.Rows[n].Cells[6].Value = m.UreticiFaturasi;
                                }

                                dataGridView6.Rows[n].Cells[1].Value = m.UrunAdi;
                                dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                                dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                                dataGridView6.Rows[n].Cells[4].Value = m.UrunAmbalaji;
                                dataGridView6.Rows[n].Cells[5].Value = "###";
                                dataGridView6.Rows[n].Cells[7].Value = m.UrunUretimTarihi;
                                n++;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            Urunegorelistele ac = new Urunegorelistele();
            ac.Show();
            ac.gelenurunrapor = "2";
            RaporlamaKontrol = "OFFLINE";
            RAPORLAMADURUMUGOSTER.Text = "Üretilen Ürüne Göre Rapolama Gösteriliyor!";
        }

        private void textBox23_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar);
        }

        private void UrunGramajiUrunEkle_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar);
        }

        private void UrunFiyatiUrunEkle_TextChanged(object sender, EventArgs e)
        {
        }

        private void button13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu sürümde Yazdırma Özelliği Etkin Değildir. \n Lütfen Excel'e Çıkarıp Yazdırın.");
        }

        private void gridView3_DoubleClick(object sender, EventArgs e)
        {
            string str = gridView3.FocusedValue.ToString();
            try
            {
                var musteribul = db.Musteriler.Where(p => p.MusteriAdi == str).FirstOrDefault();
                if (musteribul.MusteriAdi == str)
                {
                    MusteriKontrolEkrani ac = new MusteriKontrolEkrani();
                    ac.Show();
                    ac.BurdanAl.Text = musteribul.MusteriAdi;
                    ac.MusteriAdiMusteriDuzenle.Text = musteribul.MusteriAdi;
                    ac.AdresMusteriDuzenle.Text = musteribul.MusteriAdresi;
                }
            }
            catch
            {
                MessageBox.Show("Seçmek İçin Müşteri İsmine Çift Tıklayın");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Hakkinda ac = new Hakkinda();
            ac.Show();
        }

        private void gridView4_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string str = gridView4.FocusedValue.ToString();
                string Urungramajinial = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "UrunGramaji").ToString();
                string urunpaketinial = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "UrunPaketi").ToString();
                var bul = db2.Sepet.Where(p => p.UrunAdi == str && p.UrunGramaji == Urungramajinial && p.UrunPaketi == urunpaketinial).FirstOrDefault();
                SepettenUrunCikar ac = new SepettenUrunCikar();
                ac.Show();
                ac.Urunadi.Text = bul.UrunAdi;
                ac.UrunAmbalaji.Text = bul.UrunPaketi;
                ac.UrunFiyati.Text = bul.UrunFiyati;
                ac.UrunGramaji.Text = bul.UrunGramaji;
                ac.Adedi.Text = bul.UrunAdedi;
            }
            catch
            {
                MessageBox.Show("Çıkarılıcak Ürün İsmine Tıklayın!");
            }

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem.ToString() != "FATURALI" && comboBox3.SelectedItem.ToString() != "İADE")
            {
                textBox16.Enabled = false;
            }
            else
            {
                textBox16.Enabled = true;
            }
        }

        private void button9_Click(object sender, EventArgs e) // Numune Raporlama
        {
            RaporlamaKontrol = "NUMUNE";
            RAPORLAMADURUMUGOSTER.Text = "Numune Rapolama Gösteriliyor!";
            string seciliay = Convert.ToDateTime(dateTimePicker2.Text).Month.ToString();
            if (checkBox2.Checked == true)
            {
                dataGridView6.Rows.Clear();
                try
                {
                    var musteri = db.Raporlama.Where(p => p.FaturaDurumu == "NUMUNE" && p.Ay == seciliay).ToList();
                    int i = 0;
                    foreach (var m in musteri)
                    {
                        dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = m.GidenMusteriler;
                        dataGridView6.Rows[i].Cells[1].Value = m.GidenUrunler;
                        dataGridView6.Rows[i].Cells[2].Value = m.UrunGramaji;
                        dataGridView6.Rows[i].Cells[3].Value = m.UrunAdedi;
                        dataGridView6.Rows[i].Cells[4].Value = m.UrunPaketi;
                        dataGridView6.Rows[i].Cells[5].Value = m.Fiyati;
                        dataGridView6.Rows[i].Cells[6].Value = m.FaturaDurumu;
                        dataGridView6.Rows[i].Cells[7].Value = m.Tarih;
                        i++;
                    }
                    Form1_Load(sender, e);
                }
                catch
                {
                    MessageBox.Show("Bulunamadı!");
                }
            }
            else
            {
                int n = 0;
                dataGridView6.Rows.Clear();
                DateTime ilktarih = this.dateTimePicker2.Value.Date;
                DateTime sontarih = this.dateTimePicker1.Value.Date;
                if (ilktarih.ToShortDateString() == sontarih.ToShortDateString())
                {
                    try
                    {
                        string tarih = ilktarih.ToShortDateString();
                        var bul = db.Raporlama.Where(p => p.FaturaDurumu == "NUMUNE" && p.Tarih == tarih).ToList();
                        foreach (var m in bul)
                        {
                            dataGridView6.Rows.Add();
                            dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                            dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                            dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                            dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                            dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                            dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                            dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                            dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                            n++;
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    for (int i = 0; i <= (sontarih - ilktarih).Days; i++)
                    {
                        string tarih = ilktarih.AddDays(i).ToShortDateString();
                        try
                        {
                            var bul = db.Raporlama.Where(p => p.FaturaDurumu == "NUMUNE" && p.Tarih == tarih).ToList();
                            foreach (var m in bul)
                            {
                                dataGridView6.Rows.Add();
                                dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                                dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                                dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                                dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                                dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                                dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                                dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                                dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                                n++;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        private void button10_Click_1(object sender, EventArgs e) // zayi olan ürün raporlama
        {
            RaporlamaKontrol = "ZAYİ";
            RAPORLAMADURUMUGOSTER.Text = "Zayi Rapolama Gösteriliyor!";
            string seciliay = Convert.ToDateTime(dateTimePicker2.Text).Month.ToString();
            if (checkBox2.Checked == true)
            {
                dataGridView6.Rows.Clear();
                try
                {
                    var musteri = db.Raporlama.Where(p => p.FaturaDurumu == "ZAYİ" && p.Ay == seciliay).ToList();
                    int i = 0;
                    foreach (var m in musteri)
                    {
                        dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = m.GidenMusteriler;
                        dataGridView6.Rows[i].Cells[1].Value = m.GidenUrunler;
                        dataGridView6.Rows[i].Cells[2].Value = m.UrunGramaji;
                        dataGridView6.Rows[i].Cells[3].Value = m.UrunAdedi;
                        dataGridView6.Rows[i].Cells[4].Value = m.UrunPaketi;
                        dataGridView6.Rows[i].Cells[5].Value = m.Fiyati;
                        dataGridView6.Rows[i].Cells[6].Value = m.FaturaDurumu;
                        dataGridView6.Rows[i].Cells[7].Value = m.Tarih;
                        i++;
                    }
                    Form1_Load(sender, e);
                }
                catch
                {
                    MessageBox.Show("Bulunamadı!");
                }
            }
            else
            {
                int n = 0;
                dataGridView6.Rows.Clear();
                DateTime ilktarih = this.dateTimePicker2.Value.Date;
                DateTime sontarih = this.dateTimePicker1.Value.Date;
                if (ilktarih.ToShortDateString() == sontarih.ToShortDateString())
                {
                    try
                    {
                        string tarih = ilktarih.ToShortDateString();
                        var bul = db.Raporlama.Where(p => p.FaturaDurumu == "ZAYİ" && p.Tarih == tarih).ToList();
                        foreach (var m in bul)
                        {
                            dataGridView6.Rows.Add();
                            dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                            dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                            dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                            dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                            dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                            dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                            dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                            dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                            n++;
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    for (int i = 0; i <= (sontarih - ilktarih).Days; i++)
                    {
                        string tarih = ilktarih.AddDays(i).ToShortDateString();
                        try
                        {
                            var bul = db.Raporlama.Where(p => p.FaturaDurumu == "ZAYİ" && p.Tarih == tarih).ToList();
                            foreach (var m in bul)
                            {
                                dataGridView6.Rows.Add();
                                dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                                dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                                dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                                dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                                dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                                dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                                dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                                dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                                n++;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)  // faturasız ödeme raporla
        {
            RaporlamaKontrol = "FATURASIZ ÖDEME";
            RAPORLAMADURUMUGOSTER.Text = "Faturasız Ödeme Rapolama Gösteriliyor!";
            string seciliay = Convert.ToDateTime(dateTimePicker2.Text).Month.ToString();
            if (checkBox2.Checked == true)
            {
                dataGridView6.Rows.Clear();
                try
                {
                    var musteri = db.Raporlama.Where(p => p.FaturaDurumu == "FATURASIZ ÖDEME" && p.Ay == seciliay).ToList();
                    int i = 0;
                    foreach (var m in musteri)
                    {
                        dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = m.GidenMusteriler;
                        dataGridView6.Rows[i].Cells[1].Value = m.GidenUrunler;
                        dataGridView6.Rows[i].Cells[2].Value = m.UrunGramaji;
                        dataGridView6.Rows[i].Cells[3].Value = m.UrunAdedi;
                        dataGridView6.Rows[i].Cells[4].Value = m.UrunPaketi;
                        dataGridView6.Rows[i].Cells[5].Value = m.Fiyati;
                        dataGridView6.Rows[i].Cells[6].Value = m.FaturaDurumu;
                        dataGridView6.Rows[i].Cells[7].Value = m.Tarih;
                        i++;
                    }
                    Form1_Load(sender, e);
                }
                catch
                {
                    MessageBox.Show("Bulunamadı!");
                }
            }
            else
            {
                int n = 0;
                dataGridView6.Rows.Clear();
                DateTime ilktarih = this.dateTimePicker2.Value.Date;
                DateTime sontarih = this.dateTimePicker1.Value.Date;
                if (ilktarih.ToShortDateString() == sontarih.ToShortDateString())
                {
                    try
                    {
                        string tarih = ilktarih.ToShortDateString();
                        var bul = db.Raporlama.Where(p => p.FaturaDurumu == "FATURASIZ ÖDEME" && p.Tarih == tarih).ToList();
                        foreach (var m in bul)
                        {
                            dataGridView6.Rows.Add();
                            dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                            dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                            dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                            dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                            dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                            dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                            dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                            dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                            n++;
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    for (int i = 0; i <= (sontarih - ilktarih).Days; i++)
                    {
                        string tarih = ilktarih.AddDays(i).ToShortDateString();
                        try
                        {
                            var bul = db.Raporlama.Where(p => p.FaturaDurumu == "FATURASIZ ÖDEME" && p.Tarih == tarih).ToList();
                            foreach (var m in bul)
                            {
                                dataGridView6.Rows.Add();
                                dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                                dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                                dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                                dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                                dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                                dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                                dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                                dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                                n++;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            DialogResult Uyari = new DialogResult();
            Uyari = MessageBox.Show("VERİTABANI YEDEKLENECEK DEVAM EDİLSİNMİ? ", "DİKKAT!", MessageBoxButtons.YesNo);
            if (Uyari == DialogResult.Yes)
            {
                folderBrowserDialog1.ShowNewFolderButton = true;
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string dosyaadi = folderBrowserDialog1.SelectedPath;
                    File.Copy(@"C:\MRCStok\Data\StokMatik.db", dosyaadi + "\\StokMatik.db");
                    File.Copy(@"C:\MRCStok\Data\StokmatikHammadde.db", dosyaadi + "\\StokmatikHammadde.db");
                    File.Copy(@"C:\MRCStok\Data\StokmatikSepet.db", dosyaadi + "\\StokmatikSepet.db");
                    MessageBox.Show("VERİTABANI YEDEKLENDİ!");
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Safemod ac = new Safemod();
            ac.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            TedarikciEkle ac = new TedarikciEkle();
            ac.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            TedarikciEkle ac = new TedarikciEkle();
            ac.Show();
            ac.Onayla.Text = "BU ÜRÜNÜ İADE OLARAK STOK'A GİR";
            ac.MusteriTedarikciDegistir.Text = "MÜŞTERİ ADI:";
            ac.MusteriVeyaTedarikciSec.Text = "MÜŞTERİ SEÇ";
            ac.iade = true;
        }

        private void gridView2_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string str = gridView2.FocusedValue.ToString();
                string Urungramajinial = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "UrunGramaji").ToString();
                string urunpaketinial = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "UrunPaketi").ToString();
                try
                {
                    var ekle = db.Urunler.Where(p => p.UrunAdi == str && p.UrunGramaji == Urungramajinial && p.UrunPaketi == urunpaketinial).FirstOrDefault();
                    if (ekle.UrunAdi == str)
                    {
                        Uruneklemetektus ac = new Uruneklemetektus();
                        ac.Show();
                        ac.label3.Text = ekle.UrunAdi;
                        ac.label4.Text = ekle.UrunGramaji;
                        ac.label5.Text = ekle.UrunPaketi;
                        ac.UrunAdiUrunDuzenle.Text = ekle.UrunAdi;
                        ac.UrunGramajıUrunDuzenle.SelectedItem = ekle.UrunGramaji;
                        ac.UrunAdediUrunDuzenle.Text = ekle.UrunAdedi;
                        ac.AmbalajUrunDuzenle.SelectedItem = ekle.UrunPaketi;
                        ac.UrunFiyatiUrunDuzenle.Text = ekle.UrunFiyati;
                    }
                }
                catch
                {
                    MessageBox.Show("Ürün Bulunamadı!", "UYARI!");
                }
            }
        }

        private void gridView3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string str = gridView3.FocusedValue.ToString();
                try
                {
                    var musteribul = db.Musteriler.Where(p => p.MusteriAdi == str).FirstOrDefault();
                    if (musteribul.MusteriAdi == str)
                    {
                        MusteriKontrolEkrani ac = new MusteriKontrolEkrani();
                        ac.Show();
                        ac.BurdanAl.Text = musteribul.MusteriAdi;
                        ac.MusteriAdiMusteriDuzenle.Text = musteribul.MusteriAdi;
                        ac.AdresMusteriDuzenle.Text = musteribul.MusteriAdresi;
                    }
                }
                catch
                {
                    MessageBox.Show("Seçmek İçin Müşteri İsmine Çift Tıklayın");
                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            RaporlamaKontrol = "İADE GİDEN ÜRÜN";
            RAPORLAMADURUMUGOSTER.Text = "İade Giden Ürün Rapolama Gösteriliyor!";
            string seciliay = Convert.ToDateTime(dateTimePicker2.Text).Month.ToString();
            if (checkBox2.Checked == true)
            {
                dataGridView6.Rows.Clear();
                try
                {
                    var musteri = db.Raporlama.Where(p => p.FaturaDurumu.Contains("İADE EDİLEN ÜRÜN") && p.Ay == seciliay).ToList();
                    int i = 0;
                    foreach (var m in musteri)
                    {
                        dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = m.GidenMusteriler;
                        dataGridView6.Rows[i].Cells[1].Value = m.GidenUrunler;
                        dataGridView6.Rows[i].Cells[2].Value = m.UrunGramaji;
                        dataGridView6.Rows[i].Cells[3].Value = m.UrunAdedi;
                        dataGridView6.Rows[i].Cells[4].Value = m.UrunPaketi;
                        dataGridView6.Rows[i].Cells[5].Value = m.Fiyati;
                        dataGridView6.Rows[i].Cells[6].Value = m.FaturaDurumu;
                        dataGridView6.Rows[i].Cells[7].Value = m.Tarih;
                        i++;
                    }
                    Form1_Load(sender, e);
                }
                catch
                {
                    MessageBox.Show("Bulunamadı!");
                }
            }
            else
            {
                int n = 0;
                dataGridView6.Rows.Clear();
                DateTime ilktarih = this.dateTimePicker2.Value.Date;
                DateTime sontarih = this.dateTimePicker1.Value.Date;
                if (ilktarih.ToShortDateString() == sontarih.ToShortDateString())
                {
                    try
                    {
                        string tarih = ilktarih.ToShortDateString();
                        var bul = db.Raporlama.Where(p => p.FaturaDurumu.Contains("İADE EDİLEN ÜRÜN") && p.Tarih == tarih).ToList();
                        foreach (var m in bul)
                        {
                            dataGridView6.Rows.Add();
                            dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                            dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                            dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                            dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                            dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                            dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                            dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                            dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                            n++;
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    for (int i = 0; i <= (sontarih - ilktarih).Days; i++)
                    {
                        string tarih = ilktarih.AddDays(i).ToShortDateString();
                        try
                        {
                            var bul = db.Raporlama.Where(p => p.FaturaDurumu.Contains("İADE EDİLEN ÜRÜN") && p.Tarih == tarih).ToList();
                            foreach (var m in bul)
                            {
                                dataGridView6.Rows.Add();
                                dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                                dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                                dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                                dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                                dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                                dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                                dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                                dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                                n++;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }
    }
}