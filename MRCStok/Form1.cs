using System;
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

namespace MRCStok
{
    public partial class Form1 : Form
    {
        public StokMatikEntities db;
        public StokmatikHammaddeEntities db1;
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
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        public void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'stokMatikDataSet3.UrunSepeti' table. You can move, or remove it, as needed.

            // TODO: This line of code loads data into the 'stokMatikDataSet2.Musteriler' table. You can move, or remove it, as needed.
            this.musterilerTableAdapter.Fill(this.stokMatikDataSet2.Musteriler);
            // TODO: This line of code loads data into the 'stokMatikDataSet1.Kullanicilar' table. You can move, or remove it, as needed.
            this.kullanicilarTableAdapter.Fill(this.stokMatikDataSet1.Kullanicilar);
            // TODO: This line of code loads data into the 'stokMatikDataSet.Urunler' table. You can move, or remove it, as needed.
            this.urunlerTableAdapter.Fill(this.stokMatikDataSet.Urunler);
            f21.yenidenbaslama = false;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox txBox = (System.Windows.Forms.TextBox)sender;
            int pos = txBox.SelectionStart;
            int slen = txBox.SelectionLength;
            txBox.Text = txBox.Text.ToUpper();
            txBox.SelectionStart = pos;
            txBox.SelectionLength = slen;
            txBox.Focus();
        }

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
        }

        private void KullaniciListesi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
        }

        private void button10_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();

        }

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
        }

        private void button37_Click(object sender, EventArgs e)
        {
            Kullaniciİslemleri.Visible = false;
            KullaniciListesi.Visible = false;
            MessageBox.Show("Admin yetkisi Sonlandırıldı!");
            YoneticiKullaniciAdi.Text = "";
            YöneticiSifresi.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 4;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 5;
        }

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

        }

        private void MusteriDuzenleMusteriDuzenle_Click(object sender, EventArgs e)
        {
            if (MusteriAdiMusteriDuzenle.Text == "")
            {
                MessageBox.Show("Muşteri Adını Girin!");
            }
            else
            {
                if (AdresMusteriDuzenle.Text == "")
                {
                    MessageBox.Show("Muşteri Adresini Girin!");
                }
                else
                {
                    try
                    {
                        var bul = db.Musteriler.Where(p => p.MusteriAdi == SecilenMusteriAdi.Text).FirstOrDefault();
                        if (bul.MusteriAdi == SecilenMusteriAdi.Text)
                        {
                            DialogResult Uyari = new DialogResult();
                            Uyari = MessageBox.Show(SecilenMusteriAdi.Text + " Adlı Musteri Bilgileri Güncellenecek Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                            if (Uyari == DialogResult.Yes)
                            {

                                bul.MusteriAdi = MusteriAdiMusteriDuzenle.Text;
                                bul.MusteriAdresi = AdresMusteriDuzenle.Text;
                                MessageBox.Show("Müşteri Başarıyla Güncelleştirildi!");
                                MusteriAdiMusteriDuzenle.Text = "";
                                AdresMusteriDuzenle.Text = "";
                                SecilenMusteriAdi.Text = "";
                                db.SaveChanges();
                                Form1_Load(sender, e);
                            }
                        }

                    }
                    catch
                    {
                        MessageBox.Show("Veritabanına Bağlanamadı!");
                    }
                }
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            MusteriAdiMusteriEkle.Text = "";
            AdresMusteriEkle.Text = "";
        }

        private void button22_Click(object sender, EventArgs e)
        {
            MusteriAdiMusteriDuzenle.Text = "";
            AdresMusteriDuzenle.Text = "";
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
                            var bul = db.Urunler.Where(p => p.UrunAdi == UrunadiUrunEkle.Text && p.UrunPaketi == AmbalajUrunEkle.SelectedItem.ToString()).FirstOrDefault();
                            if (bul.UrunAdi == UrunadiUrunEkle.Text)
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
                                if(UrunGramajiUrunEkle.SelectedItem!=UrunGramajiUrunEkle.Text)
                                {
                                    ekle.UrunGramaji = UrunGramajiUrunEkle.Text;
                                    secimial= UrunGramajiUrunEkle.Text;
                                }
                                else
                                {
                                   ekle.UrunGramaji = UrunGramajiUrunEkle.SelectedItem.ToString();
                                    secimial= UrunGramajiUrunEkle.SelectedItem.ToString();
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
                            catch
                            {
                                MessageBox.Show("Veritabanına Bağlanamadı!");
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

        private void dataGridView5_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int xkoordinat = dataGridView5.CurrentCellAddress.X; //Seçili satırın X koordinatı
            int ykoordinat = dataGridView5.CurrentCellAddress.Y;  //Seçili satırın Y koordinatı
            string str = "";
            str = dataGridView5.Rows[ykoordinat].Cells[xkoordinat].Value.ToString();
            if (e.RowIndex == -1)
            {
                return;
            }
            try
            {
                var musteribul = db.Musteriler.Where(p => p.MusteriAdi == str).FirstOrDefault();
                SecilenMusteriAdi.Text = musteribul.MusteriAdi;
                MusteriAdiMusteriDuzenle.Text = musteribul.MusteriAdi;
                AdresMusteriDuzenle.Text = musteribul.MusteriAdresi;
            }
            catch
            {
                MessageBox.Show("Seçmek İçin Müşteri İsmine Çift Tıklayın");
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


        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                var bul = db.Musteriler.Where(p => p.MusteriAdi == SecilenMusteriAdi.Text).FirstOrDefault();
                if (bul.MusteriAdi == SecilenMusteriAdi.Text)
                {
                    DialogResult Uyari = new DialogResult();
                    Uyari = MessageBox.Show(SecilenMusteriAdi.Text + " Adlı Musteri Silinecek Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                    if (Uyari == DialogResult.Yes)
                    {

                        db.Musteriler.Remove(bul);
                        db.SaveChanges();
                        MessageBox.Show("Müşteri Başarıyla Silindi!");
                        MusteriAdiMusteriDuzenle.Text = "";
                        AdresMusteriDuzenle.Text = "";
                        SecilenMusteriAdi.Text = "";

                        Form1_Load(sender, e);
                    }
                }

            }
            catch
            {
                MessageBox.Show("Veritabanına Bağlanamadı!");
            }
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
                    for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                    {
                        string urunn = dataGridView2.Rows[i].Cells[0].Value.ToString();
                        var bul = db.Urunler.Where(p => p.UrunAdi == urunn).FirstOrDefault();
                        double adet = Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value.ToString());
                        double stokadet = Convert.ToDouble(bul.UrunAdedi);
                        double topla = stokadet + adet;
                        bul.UrunAdedi = topla.ToString();
                        db.SaveChanges();
                    }
                    MessageBox.Show("Sipariş Sepetindeki Ürün Silindi!");
                    dataGridView2.Rows.Clear();
                    textBox2.Text = "";
                    textBox16.Text = "";
                    sayac = 0;
                    textBox4.Text = "0";
                    textBox5.Text = "0";
                    textBox6.Text = "0";
                    Form1_Load(sender, e);
                }
                catch
                {

                }
            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            MusteriSecmeEkrani musac = new MusteriSecmeEkrani();
            musac.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("ÖNCE MÜŞTERİ SEÇİNİZ!");
            }
            else
            {
                if (dataGridView2.Rows[0].Cells[0].Value == null)
                {
                    MessageBox.Show("Önce Sepete Ürün Ekleyin!");
                }
                else
                {
                    if (comboBox3.SelectedItem.ToString() == "FATURALI")
                    {
                        if (textBox16.Text == "")
                        {
                            MessageBox.Show("Fatura Numarası girmemişsiniz!");
                        }
                        else
                        {
                            DialogResult Uyari = new DialogResult();
                            Uyari = MessageBox.Show("Siparişiniz Tamamlanacak Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                            if (Uyari == DialogResult.Yes)
                            {
                                try
                                {

                                    for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                                    {
                                        Raporlama ekle = new Raporlama();
                                        string urunadii = dataGridView2.Rows[i].Cells[0].Value.ToString();
                                        var bul = db.Urunler.Where(p => p.UrunAdi == urunadii).FirstOrDefault();
                                        ekle.GidenMusteriler = textBox2.Text;
                                        ekle.Gun = DateTime.Now.Day.ToString();
                                        ekle.Tarih = DateTime.Now.ToShortDateString();
                                        ekle.Ay = DateTime.Now.Month.ToString();
                                        ekle.Yil = DateTime.Now.Year.ToString();
                                        ekle.GidenUrunler = bul.UrunAdi;
                                        ekle.UrunGramaji = bul.UrunGramaji;
                                        ekle.UrunAdedi = dataGridView2.Rows[i].Cells[1].Value.ToString();
                                        ekle.UrunPaketi = bul.UrunPaketi;
                                        ekle.Fiyati = dataGridView2.Rows[i].Cells[2].Value.ToString();
                                        ekle.FaturaDurumu = textBox16.Text;
                                        db.Raporlama.Add(ekle);
                                        db.SaveChanges();
                                    }
                                    MessageBox.Show("Ürünler Sepete Eklendi!");
                                    dataGridView2.Rows.Clear();
                                    textBox2.Text = "";
                                    textBox16.Text = "";
                                    sayac = 0;
                                    textBox4.Text = "0";
                                    textBox5.Text = "0";
                                    textBox6.Text = "0";
                                    Form1_Load(sender, e);
                                }
                                catch
                                {
                                    MessageBox.Show("VERİTABANI HATASI");
                                }
                            }
                        }
                    }
                    else if (comboBox3.SelectedItem.ToString() == "FATURASIZ")
                    {
                        if (textBox16.Text == "")
                        {
                            DialogResult Uyari = new DialogResult();
                            Uyari = MessageBox.Show("Siparişiniz Tamamlanacak Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                            if (Uyari == DialogResult.Yes)
                            {
                                try
                                {

                                    for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                                    {
                                        Raporlama ekle = new Raporlama();
                                        string urunadii = dataGridView2.Rows[i].Cells[0].Value.ToString();
                                        var bul = db.Urunler.Where(p => p.UrunAdi == urunadii).FirstOrDefault();
                                        ekle.GidenMusteriler = textBox2.Text;
                                        ekle.Gun = DateTime.Now.Day.ToString();
                                        ekle.Tarih = DateTime.Now.ToShortDateString();
                                        ekle.Ay = DateTime.Now.Month.ToString();
                                        ekle.Yil = DateTime.Now.Year.ToString();
                                        ekle.GidenUrunler = bul.UrunAdi;
                                        ekle.UrunGramaji = bul.UrunGramaji;
                                        ekle.UrunAdedi = dataGridView2.Rows[i].Cells[1].Value.ToString();
                                        ekle.UrunPaketi = bul.UrunPaketi;
                                        ekle.Fiyati = dataGridView2.Rows[i].Cells[2].Value.ToString();
                                        ekle.FaturaDurumu = "FATURASIZ";
                                        db.Raporlama.Add(ekle);
                                        db.SaveChanges();
                                    }
                                    MessageBox.Show("Ürünler Sepete Eklendi!");
                                    dataGridView2.Rows.Clear();
                                    textBox2.Text = "";
                                    textBox16.Text = "";
                                    sayac = 0;
                                    textBox4.Text = "0";
                                    textBox5.Text = "0";
                                    textBox6.Text = "0";
                                    Form1_Load(sender, e);
                                }
                                catch
                                {
                                    MessageBox.Show("VERİTABANI HATASI");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Fatura Numarası Girilmiş!");
                        }
                    }
                    else if (comboBox3.SelectedItem.ToString() == "BEDELSİZ")
                    {
                        if (textBox16.Text == "")
                        {
                            DialogResult Uyari = new DialogResult();
                            Uyari = MessageBox.Show("Siparişiniz Tamamlanacak Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                            if (Uyari == DialogResult.Yes)
                            {
                                try
                                {

                                    for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                                    {
                                        Raporlama ekle = new Raporlama();
                                        string urunadii = dataGridView2.Rows[i].Cells[0].Value.ToString();
                                        var bul = db.Urunler.Where(p => p.UrunAdi == urunadii).FirstOrDefault();
                                        ekle.GidenMusteriler = textBox2.Text;
                                        ekle.Gun = DateTime.Now.Day.ToString();
                                        ekle.Tarih = DateTime.Now.ToShortDateString();
                                        ekle.Ay = DateTime.Now.Month.ToString();
                                        ekle.Yil = DateTime.Now.Year.ToString();
                                        ekle.GidenUrunler = bul.UrunAdi;
                                        ekle.UrunGramaji = bul.UrunGramaji;
                                        ekle.UrunAdedi = dataGridView2.Rows[i].Cells[1].Value.ToString();
                                        ekle.UrunPaketi = bul.UrunPaketi;
                                        ekle.Fiyati = dataGridView2.Rows[i].Cells[2].Value.ToString();
                                        ekle.FaturaDurumu = "BEDELSİZ";
                                        db.Raporlama.Add(ekle);
                                        db.SaveChanges();
                                    }
                                    MessageBox.Show("Ürünler Sepete Eklendi!");
                                    dataGridView2.Rows.Clear();
                                    textBox2.Text = "";
                                    textBox16.Text = "";
                                    sayac = 0;
                                    textBox4.Text = "0";
                                    textBox5.Text = "0";
                                    textBox6.Text = "0";
                                    Form1_Load(sender, e);
                                }
                                catch
                                {
                                    MessageBox.Show("VERİTABANI HATASI");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Fatura Numarası Girilmiş!");
                        }
                    }
                }
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            string selicideger = Convert.ToDateTime(dateTimePicker2.Text).ToShortDateString();
            string selicideger2 = Convert.ToDateTime(dateTimePicker1.Text).ToShortDateString();
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
                    //Form1_Load(sender, e);
                }
                catch
                {

                }
            }
            else
            {
                dataGridView6.Rows.Clear();

                try
                {
                    var bul = db.Raporlama.Where(p => p.Tarih == selicideger && p.Tarih==selicideger2).ToList();
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
                    //Form1_Load(sender, e);
                }
                catch
                {

                }
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            dataGridView6.Rows.Clear();
            textBox7.Text = "";
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

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            string str = gridView1.FocusedValue.ToString();
            try
            {
                var ekle = db.Urunler.Where(p => p.UrunAdi == str).FirstOrDefault();
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

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string str = gridView1.FocusedValue.ToString();
                try
                {
                    var ekle = db.Urunler.Where(p => p.UrunAdi == str).FirstOrDefault();
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

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int xkoordinat = dataGridView2.CurrentCellAddress.X; //Seçili satırın X koordinatı
            int ykoordinat = dataGridView2.CurrentCellAddress.Y;  //Seçili satırın Y koordinatı
            string str = "";
            str = dataGridView2.Rows[ykoordinat].Cells[xkoordinat].Value.ToString();
            if (e.RowIndex == -1)
            {
                return;
            }
            DialogResult Uyari = new DialogResult();
            Uyari = MessageBox.Show("Sepetteki Ürün Silinecek Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
            if (Uyari == DialogResult.Yes)
            {
                try
                {
                    var bul = db.Urunler.Where(p => p.UrunAdi == str).FirstOrDefault();

                    double adet = Convert.ToDouble(dataGridView2.Rows[ykoordinat].Cells[1].Value.ToString());
                    double stokadet = Convert.ToDouble(bul.UrunAdedi);
                    double topla = stokadet + adet;
                    bul.UrunAdedi = topla.ToString();
                    dataGridView2.Rows.RemoveAt(ykoordinat);
                    sayac--;
                    db.SaveChanges();
                    MessageBox.Show("Sipariş Sepetindeki Ürün Silindi!");
                    Form1_Load(sender, e);
                }
                catch
                {

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
            AC.counter = true;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            string selicideger = Convert.ToDateTime(dateTimePicker2.Text).ToShortDateString();
            string selicideger2 = Convert.ToDateTime(dateTimePicker1.Text).ToShortDateString();
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
                    //Form1_Load(sender, e);
                }
                catch
                {
                    MessageBox.Show("Bulunamadı!");
                }
            }
            else
            {
                dataGridView6.Rows.Clear();
                try
                {
                    var musteri = db.Raporlama.Where(p => p.GidenMusteriler == textBox7.Text && p.Tarih == selicideger && p.Tarih==selicideger2).ToList();
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
                    //Form1_Load(sender, e);
                }
                catch
                {
                    MessageBox.Show("Bulunamadı!");
                }
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            dataGridView6.Rows.Clear();
            textBox7.Text = "";
        }

        private void button30_Click(object sender, EventArgs e)
        {
            string selicideger = Convert.ToDateTime(dateTimePicker2.Text).ToShortDateString();
            string selicideger2 = Convert.ToDateTime(dateTimePicker1.Text).ToShortDateString();
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
                dataGridView6.Rows.Clear();
                try
                {
                    var musteri = db.Raporlama.Where(p => p.FaturaDurumu == "FATURASIZ" && p.Tarih == selicideger && p.Tarih==selicideger2).ToList();
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
        }

        private void button33_Click(object sender, EventArgs e)
        {
            string selicideger = Convert.ToDateTime(dateTimePicker2.Text).ToShortDateString();
            string selicideger2 = Convert.ToDateTime(dateTimePicker1.Text).ToShortDateString();
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
                dataGridView6.Rows.Clear();
                try
                {
                    var musteri = db.Raporlama.Where(p => p.FaturaDurumu == "BEDELSİZ" && p.Tarih == selicideger && p.Tarih==selicideger2).ToList();
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
        }

        private void button32_Click(object sender, EventArgs e)
        {
            dataGridView6.Rows.Clear();
            textBox7.Text = "";
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            string selicideger = Convert.ToDateTime(dateTimePicker2.Text).ToShortDateString();
            string selicideger2 = Convert.ToDateTime(dateTimePicker1.Text).ToShortDateString();
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
                    //Form1_Load(sender, e);
                }
                catch
                {
                    MessageBox.Show("Bulunamadı!");
                }
            }
            else
            {
                dataGridView6.Rows.Clear();
                try
                {
                    var musteri = db.Raporlama.Where(p => p.FaturaDurumu == textBox1.Text && p.Tarih == selicideger && p.Tarih==selicideger2).ToList();
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selicideger = Convert.ToDateTime(dateTimePicker2.Text).ToShortDateString();
            string selicideger2 = Convert.ToDateTime(dateTimePicker1.Text).ToShortDateString();
            string seciliay = Convert.ToDateTime(dateTimePicker2.Text).Month.ToString();
            if (checkBox2.Checked == true)
            {
                dataGridView6.Rows.Clear();
                try
                {
                    var musteri = db.Raporlama.Where(p => p.FaturaDurumu == "İADE" && p.Ay == seciliay).ToList();
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
                dataGridView6.Rows.Clear();
                try
                {
                    var musteri = db.Raporlama.Where(p => p.FaturaDurumu == "İADE" && p.Tarih == selicideger && p.Tarih==selicideger2).ToList();
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
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView6_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
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
                else
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
            }
            catch
            {
                MessageBox.Show("Seçmek İçin Ürün İsmine Çift Tıklayın");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Urunegorelistele ac = new Urunegorelistele();
            ac.Show();
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
            try
            {
                var ekle = db.Urunler.Where(p => p.UrunAdi == str).FirstOrDefault();
                if (ekle.UrunAdi == str)
                {
                    Uruneklemetektus ac = new Uruneklemetektus();
                    ac.Show();
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
            string selicideger = Convert.ToDateTime(dateTimePicker2.Text).ToShortDateString();
            string selicideger2 = Convert.ToDateTime(dateTimePicker1.Text).ToShortDateString();
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
                        dataGridView6.Rows[i].Cells[0].Value ="MRC ÜRETİM";
                        dataGridView6.Rows[i].Cells[1].Value = m.UrunAdi;
                        dataGridView6.Rows[i].Cells[2].Value = m.UrunGramaji;
                        dataGridView6.Rows[i].Cells[3].Value = m.UrunAdedi;
                        dataGridView6.Rows[i].Cells[4].Value = m.UrunAmbalaji;
                        dataGridView6.Rows[i].Cells[5].Value = "###";
                        dataGridView6.Rows[i].Cells[6].Value = "###";
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
                dataGridView6.Rows.Clear();

                try
                {
                    var bul = db1.Uretim.Where(p => p.UrunUretimTarihi == selicideger && p.UrunUretimTarihi == selicideger2).ToList();
                    int i = 0;
                    foreach (var m in bul)
                    {
                        dataGridView6.Rows.Add();
                        dataGridView6.Rows[i].Cells[0].Value = "MRC ÜRETİM";
                        dataGridView6.Rows[i].Cells[1].Value = m.UrunAdi;
                        dataGridView6.Rows[i].Cells[2].Value = m.UrunGramaji;
                        dataGridView6.Rows[i].Cells[3].Value = m.UrunAdedi;
                        dataGridView6.Rows[i].Cells[4].Value = m.UrunAmbalaji;
                        dataGridView6.Rows[i].Cells[5].Value = "###";
                        dataGridView6.Rows[i].Cells[6].Value = "###";
                        dataGridView6.Rows[i].Cells[7].Value = m.UrunUretimTarihi;
                        i++;
                    }
                }
                catch
                {

                }
            }
        }
    }
}


