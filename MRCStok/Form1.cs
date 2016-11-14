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
        public string AdminKontrol;
        public string tarih = DateTime.Now.ToShortDateString();
        public string MevcutGun = DateTime.Now.Day.ToString();
        public string MevcutAy = DateTime.Now.Month.ToString();
        public string MevcutYil = DateTime.Now.Year.ToString();
        public int a = 30000; 
        public int sayac = 0;
        public Form1()
        {
            InitializeComponent();
            db = new StokMatikEntities();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        public void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'stokMatikDataSet3.UrunSepeti' table. You can move, or remove it, as needed.
            this.urunSepetiTableAdapter.Fill(this.stokMatikDataSet3.UrunSepeti);
            // TODO: This line of code loads data into the 'stokMatikDataSet2.Musteriler' table. You can move, or remove it, as needed.
            this.musterilerTableAdapter.Fill(this.stokMatikDataSet2.Musteriler);
            // TODO: This line of code loads data into the 'stokMatikDataSet1.Kullanicilar' table. You can move, or remove it, as needed.
            this.kullanicilarTableAdapter.Fill(this.stokMatikDataSet1.Kullanicilar);
            // TODO: This line of code loads data into the 'stokMatikDataSet.Urunler' table. You can move, or remove it, as needed.
            this.urunlerTableAdapter.Fill(this.stokMatikDataSet.Urunler);
            timer1.Interval = a;
            timer1.Enabled = true;
            try
            {
                dataGridView4.Rows.Clear();
                var bul = db.UrunSepeti.Where(p => p.Tarih == tarih).ToList();
                int i = 0;
                string kontrol = "";
                foreach (var u in bul)
                {
                    if (u.MusteriAdi == kontrol)
                    {
                    }
                    else
                    {
                        dataGridView4.Rows.Add();
                        dataGridView4.Rows[i].Cells[0].Value = u.MusteriAdi;
                        i++;
                        kontrol = u.MusteriAdi;
                    }
                    UyariBilgisi.Text = "Bu Gün Sepette Bekeleyen Ürünleriniz Var!";
                }
            }
            catch
            {
                UyariBilgisi.Text = "VERİTABANINA BAĞLANILAMAI!";
            }
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
                else if (checkBox1.Checked== true)
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
                            var bul = db.Urunler.Where(p => p.UrunAdi == UrunadiUrunEkle.Text && p.UrunPaketi== AmbalajUrunEkle.SelectedItem.ToString()).FirstOrDefault();
                            if (bul.UrunAdi == UrunadiUrunEkle.Text)
                            {
                                MessageBox.Show("Bu ürün Daha Önceden Kaydedilmiş!, Eğer Ürünün Bilgilerini Güncellemek istiyorsanız ürün ismine çift tıklayarak ürün güncelle Panosundan işlemlerinizi yapabilirsiniz!", "UYARI!");
                            }

                        }
                        catch
                        {
                            try
                            {
                                Urunler ekle = new Urunler();
                                ekle.UrunAdi = UrunadiUrunEkle.Text;
                                ekle.UrunAdedi = UrunAdediUrunEkle.Text;
                                ekle.UrunBarkodu = UrunbarkoduUrunEkle.Text;
                                ekle.UrunFiyati = UrunFiyatiUrunEkle.Text;
                                ekle.UrunGramaji = UrunGramajiUrunEkle.SelectedItem.ToString();
                                ekle.UrunPaketi = AmbalajUrunEkle.SelectedItem.ToString();
                                ekle.UrunEklemeTarihi = DateTime.Now.ToShortDateString();
                                db.Urunler.Add(ekle);
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

        private void UrunduzenleButonu_Click(object sender, EventArgs e)
        {
            if (AdminKontrol == "admin" && AdminKontrol=="stok")
            {
                try
                {
                    var bul = db.Urunler.Where(p => p.UrunAdi == UrunDuzenleSecilenAd.Text).FirstOrDefault();

                    if (bul.UrunAdi == UrunDuzenleSecilenAd.Text)
                    {

                        DialogResult Uyari = new DialogResult();
                        Uyari = MessageBox.Show(UrunDuzenleSecilenAd.Text + " Adlı ürünün Bilgileri Güncellenecek Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                        if (Uyari == DialogResult.Yes)
                        {

                            bul.UrunAdi = UrunAdiUrunDuzenle.Text;
                            bul.UrunAdedi = UrunAdediUrunDuzenle.Text;
                            bul.UrunBarkodu = UrunBarkoduUrunDuzenle.Text;
                            bul.UrunFiyati = UrunFiyatiUrunDuzenle.Text;
                            bul.UrunGramaji = UrunGramajıUrunDuzenle.SelectedItem.ToString();
                            bul.UrunPaketi = AmbalajUrunDuzenle.SelectedItem.ToString();
                            bul.UrunEklemeTarihi = DateTime.Now.ToShortDateString();
                            db.SaveChanges();
                            MessageBox.Show("Ürün Başarıyla Güncellendi");
                            UrunDuzenleSecilenAd.Text = "";
                            UrunAdiUrunDuzenle.Text = "";
                            UrunAdediUrunDuzenle.Text = "";
                            UrunBarkoduUrunDuzenle.Text = "";
                            UrunFiyatiUrunDuzenle.Text = "0";
                            HizliStokBarkod.Text = "";
                            HizliStokUrunAdedi.Text = "";
                            HizliStokUrunAdi.Text = "";
                            Form1_Load(sender, e);
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
                MessageBox.Show("Yalnızca ADMIN Yetkisi Verilmiş Kullanıcılar Ürün Düzenleyebilirler.", "Yetkisiz Kullanıcı!");
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

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int xkoordinat = dataGridView3.CurrentCellAddress.X; //Seçili satırın X koordinatı
            int ykoordinat = dataGridView3.CurrentCellAddress.Y;  //Seçili satırın Y koordinatı
            string str = "";
            str = dataGridView3.Rows[ykoordinat].Cells[xkoordinat].Value.ToString();
            if (e.RowIndex == -1)
            {
                return;
            }
            try
            {
                var urunbul = db.Urunler.Where(p => p.UrunAdi == str).FirstOrDefault();
                UrunDuzenleSecilenAd.Text = urunbul.UrunAdi;
                UrunAdiUrunDuzenle.Text = urunbul.UrunAdi;
                UrunAdediUrunDuzenle.Text = urunbul.UrunAdedi;
                UrunBarkoduUrunDuzenle.Text = urunbul.UrunBarkodu;
                UrunFiyatiUrunDuzenle.Text = urunbul.UrunFiyati;
                UrunGramajıUrunDuzenle.SelectedItem = urunbul.UrunGramaji;
                AmbalajUrunDuzenle.SelectedItem = urunbul.UrunPaketi;
                HizliStokUrunAdi.Text = urunbul.UrunAdi;
                HizliStokBarkod.Text = urunbul.UrunBarkodu;
            }
            catch
            {
                MessageBox.Show("Seçmek İçin Ürün İsmine Çift Tıklayın");
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

        private void IptalUrunDuzenle_Click(object sender, EventArgs e)
        {
            UrunAdiUrunDuzenle.Text = "";
            UrunAdediUrunDuzenle.Text = "";
            UrunBarkoduUrunDuzenle.Text = "";
            UrunFiyatiUrunDuzenle.Text = "0";
            HizliStokBarkod.Text = "";
            HizliStokUrunAdedi.Text = "";
            HizliStokUrunAdi.Text = "";
            Form1_Load(sender, e);
        }

        private void UrunSilButonu_Click(object sender, EventArgs e)
        {
            if (AdminKontrol == "admin")
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
                            UrunDuzenleSecilenAd.Text = "";
                            UrunAdiUrunDuzenle.Text = "";
                            UrunAdediUrunDuzenle.Text = "";
                            UrunBarkoduUrunDuzenle.Text = "";
                            UrunFiyatiUrunDuzenle.Text = "0";
                            HizliStokBarkod.Text = "";
                            HizliStokUrunAdedi.Text = "";
                            HizliStokUrunAdi.Text = "";
                            Form1_Load(sender, e);
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

        private void button14_Click(object sender, EventArgs e)
        {
            if (AdminKontrol == "admin" && AdminKontrol=="stok")
            {
                if (HizliStokUrunAdedi.Text == "")
                {
                    MessageBox.Show("Ürün Adedini Giriniz!");
                }
                else
                {
                    try
                    {
                        var ara = db.Urunler.Where(p => p.UrunAdi == HizliStokUrunAdi.Text).FirstOrDefault();
                        int ekleurun = Convert.ToInt32(HizliStokUrunAdedi.Text);
                        int mevcuturun = Convert.ToInt32(ara.UrunAdedi);
                        int sonhali = ekleurun + mevcuturun;
                        ara.UrunAdedi = sonhali.ToString();
                        ara.UrunEklemeTarihi = DateTime.Now.ToShortDateString();
                        db.SaveChanges();
                        MessageBox.Show(ara.UrunAdi + " Adlı Ürünün " + mevcuturun.ToString() + " Adet'ine " + ekleurun.ToString() + " Adet Daha Eklendi. Ürünün Yeni Adeti: " + sonhali.ToString() + " Olmuştur.");
                        HizliStokBarkod.Text = "";
                        HizliStokUrunAdedi.Text = "";
                        HizliStokUrunAdi.Text = "";
                        UrunDuzenleSecilenAd.Text = "";
                        UrunAdiUrunDuzenle.Text = "";
                        UrunAdediUrunDuzenle.Text = "";
                        UrunBarkoduUrunDuzenle.Text = "";
                        UrunFiyatiUrunDuzenle.Text = "0";
                        Form1_Load(sender, e);

                    }
                    catch
                    {
                        MessageBox.Show("Veritabanına Bağlanamadı!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Sadece ADMIN Yetkisi Verilen Kullanıcılar Hızlı Ürün Ekleyebilir.", "Yetkisiz Kullanıcı!");
            }

        }

        private void button15_Click(object sender, EventArgs e)
        {
            HizliStokBarkod.Text = "";
            HizliStokUrunAdedi.Text = "";
            HizliStokUrunAdi.Text = "";
            UrunDuzenleSecilenAd.Text = "";
            UrunAdiUrunDuzenle.Text = "";
            UrunAdediUrunDuzenle.Text = "";
            UrunBarkoduUrunDuzenle.Text = "";
            UrunFiyatiUrunDuzenle.Text = "0";
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
                for (int j = 0; j < dataGridView3.Columns.Count; j++)
                {
                    Range myRange = (Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = dataGridView3.Columns[j].HeaderText;
                }
                StartRow++;
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView3.Columns.Count; j++)
                    {

                        Range myRange = (Range)sheet1.Cells[StartRow + i, StartCol + j];
                        myRange.Value2 = dataGridView3[j, i].Value == null ? "" : dataGridView3[j, i].Value;
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

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    var ekle = db.Urunler.Where(p => p.UrunAdi == textBox1.Text).FirstOrDefault();
                    if (ekle.UrunAdi == textBox1.Text)
                    {

                        UrunEklemeEkrani ac = new UrunEklemeEkrani();
                        ac.Show();
                        ac.textBox1.Text = ekle.UrunAdi;
                        ac.textBox2.Text = ekle.UrunGramaji;
                        ac.textBox3.Text = ekle.UrunAdedi;
                        textBox1.Text = "";
                    }

                }
                catch
                {
                    MessageBox.Show("Ürün Bulunamadı!", "UYARI!");
                    textBox1.Text = "";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult Uyari = new DialogResult();
            Uyari = MessageBox.Show("Siparişler İptal Edilecek Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
            if (Uyari == DialogResult.Yes)
            {
                try
                {
                    var bul = db.UrunSepeti.Where(p => p.MusteriAdi == textBox3.Text).ToList();
                    foreach (var n in bul)
                    {
                        double adet = Convert.ToDouble(n.UrunAdedi);
                        string urunadi = n.UrunAdi;
                        var adibul = db.Urunler.Where(p => p.UrunAdi == urunadi).FirstOrDefault();
                        double stokadet = Convert.ToDouble(adibul.UrunAdedi);
                        double topla = stokadet + adet;
                        adibul.UrunAdedi = topla.ToString();
                        db.UrunSepeti.Remove(n);
                        db.SaveChanges();
                        Form1_Load(sender, e);
                        MessageBox.Show("Sipariş Silindi");
                        textBox3.Text = "";
                    }

                }
                catch
                {

                }
            }
            dataGridView2.Rows.Clear();
            textBox2.Text = "";
            textBox16.Text = "";
            sayac = 0;
            textBox4.Text = "0";
            textBox5.Text = "0";
            textBox6.Text = "0";
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int xkoordinat = dataGridView1.CurrentCellAddress.X; //Seçili satırın X koordinatı
            int ykoordinat = dataGridView1.CurrentCellAddress.Y;  //Seçili satırın Y koordinatı
            string str = "";
            str = dataGridView1.Rows[ykoordinat].Cells[xkoordinat].Value.ToString();
            if (e.RowIndex == -1)
            {
                return;
            }
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
                    textBox1.Text = "";
                }
            }
            catch
            {
                MessageBox.Show("Ürün Bulunamadı!", "UYARI!");
                textBox1.Text = "";
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
                    if (comboBox3.SelectedItem.ToString()=="FATURALI") 
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
                                    UrunSepeti ekle = new UrunSepeti();
                                    for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                                    {

                                        ekle.MusteriAdi = textBox2.Text;
                                        ekle.Gun = DateTime.Now.Day.ToString();
                                        ekle.Tarih = DateTime.Now.ToShortDateString();
                                        ekle.Ay = DateTime.Now.Month.ToString();
                                        ekle.Yil = DateTime.Now.Year.ToString();
                                        ekle.UrunAdi = dataGridView2.Rows[i].Cells[0].Value.ToString();
                                        ekle.UrunGramaji = dataGridView2.Rows[i].Cells[1].Value.ToString();
                                        ekle.UrunAdedi = dataGridView2.Rows[i].Cells[2].Value.ToString();
                                        ekle.UrunAmbalaji = dataGridView2.Rows[i].Cells[3].Value.ToString();
                                        ekle.UrunFiyati = dataGridView2.Rows[i].Cells[4].Value.ToString();
                                        ekle.UrunFaturasi = textBox16.Text;
                                        db.UrunSepeti.Add(ekle);
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
                                    UrunSepeti ekle = new UrunSepeti();
                                    for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                                    {
                                        ekle.MusteriAdi = textBox2.Text;
                                        ekle.Gun = DateTime.Now.Day.ToString();
                                        ekle.Tarih = DateTime.Now.ToShortDateString();
                                        ekle.Ay = DateTime.Now.Month.ToString();
                                        ekle.Yil = DateTime.Now.Year.ToString();
                                        ekle.UrunAdi = dataGridView2.Rows[i].Cells[0].Value.ToString();
                                        ekle.UrunGramaji = dataGridView2.Rows[i].Cells[1].Value.ToString();
                                        ekle.UrunAdedi = dataGridView2.Rows[i].Cells[2].Value.ToString();
                                        ekle.UrunAmbalaji = dataGridView2.Rows[i].Cells[3].Value.ToString();
                                        ekle.UrunFiyati = dataGridView2.Rows[i].Cells[4].Value.ToString();
                                        ekle.UrunFaturasi = "FATURASIZ";
                                        db.UrunSepeti.Add(ekle);
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
                                    UrunSepeti ekle = new UrunSepeti();
                                    for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                                    {
                                        ekle.MusteriAdi = textBox2.Text;
                                        ekle.Gun = DateTime.Now.Day.ToString();
                                        ekle.Tarih = DateTime.Now.ToShortDateString();
                                        ekle.Ay = DateTime.Now.Month.ToString();
                                        ekle.Yil = DateTime.Now.Year.ToString();
                                        ekle.UrunAdi = dataGridView2.Rows[i].Cells[0].Value.ToString();
                                        ekle.UrunGramaji = dataGridView2.Rows[i].Cells[1].Value.ToString();
                                        ekle.UrunAdedi = dataGridView2.Rows[i].Cells[2].Value.ToString();
                                        ekle.UrunAmbalaji = dataGridView2.Rows[i].Cells[3].Value.ToString();
                                        ekle.UrunFiyati = dataGridView2.Rows[i].Cells[4].Value.ToString();
                                        ekle.UrunFaturasi = "BEDELSİZ";
                                        db.UrunSepeti.Add(ekle);
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

        private void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView8.Rows.Clear();
            int xkoordinat = dataGridView4.CurrentCellAddress.X; //Seçili satırın X koordinatı
            int ykoordinat = dataGridView4.CurrentCellAddress.Y;  //Seçili satırın Y koordinatı
            string str = "";
            str = dataGridView4.Rows[ykoordinat].Cells[xkoordinat].Value.ToString();
            if (e.RowIndex == -1)
            {
                return;
            }
            try
            {
                var bul = db.UrunSepeti.Where(p => p.MusteriAdi == str && p.Tarih ==tarih ).ToList();
                textBox3.Visible = true;
                double TUG1, TUG2 = 0, TUA1, TUA2 = 0, TUF1, TUF2 = 0;
                int i = 0;
                foreach (var m in bul)
                {
                    dataGridView8.Rows.Add();
                    dataGridView8.Rows[i].Cells[0].Value = m.MusteriAdi;
                    dataGridView8.Rows[i].Cells[1].Value = m.UrunAdi;
                    dataGridView8.Rows[i].Cells[2].Value = m.UrunGramaji;
                    dataGridView8.Rows[i].Cells[3].Value = m.UrunAdedi;
                    dataGridView8.Rows[i].Cells[4].Value = m.UrunAmbalaji;
                    dataGridView8.Rows[i].Cells[5].Value = m.UrunFiyati;
                    dataGridView8.Rows[i].Cells[6].Value = m.Tarih;
                    TUG1 = Convert.ToDouble(m.UrunGramaji);
                    TUG2 = TUG2 + TUG1;
                    TUA1 = Convert.ToDouble(m.UrunAdedi);
                    TUA2 = TUA2 + TUA1;
                    TUF1 = Convert.ToDouble(m.UrunFiyati);
                    TUF2 = TUF2 + TUF1;
                    i++;
                    textBox3.Text = m.MusteriAdi;
                }
                textBox10.Text = TUG2.ToString();
                textBox9.Text = TUF2.ToString();
                textBox8.Text = TUA2.ToString();
            }
            catch
            {

            }
        }

        private void button40_Click(object sender, EventArgs e)
        {
            string selicideger = Convert.ToDateTime(dateTimePicker1.Text).ToShortDateString();
            double TUG1, TUG2 = 0, TUA1, TUA2 = 0, TUF1, TUF2 = 0;
            try
            {
                dataGridView8.Rows.Clear();
                var bul = db.UrunSepeti.Where(p => p.Tarih == selicideger).ToList();
                int i = 0;
                foreach (var m in bul)
                {
                    dataGridView8.Rows.Add();
                    dataGridView8.Rows[i].Cells[0].Value = m.MusteriAdi;
                    dataGridView8.Rows[i].Cells[1].Value = m.UrunAdi;
                    dataGridView8.Rows[i].Cells[2].Value = m.UrunGramaji;
                    dataGridView8.Rows[i].Cells[3].Value = m.UrunAdedi;
                    dataGridView8.Rows[i].Cells[4].Value = m.UrunAmbalaji;
                    dataGridView8.Rows[i].Cells[5].Value = m.UrunFiyati;
                    dataGridView8.Rows[i].Cells[6].Value = m.Tarih;
                    TUG1 = Convert.ToDouble(m.UrunGramaji);
                    TUG2 = TUG2 + TUG1;
                    TUA1 = Convert.ToDouble(m.UrunAdedi);
                    TUA2 = TUA2 + TUA1;
                    TUF1 = Convert.ToDouble(m.UrunFiyati);
                    TUF2 = TUF2 + TUF1;
                    i++;
                }
                textBox10.Text = TUG2.ToString();
                textBox9.Text = TUF2.ToString();
                textBox8.Text = TUA2.ToString();
                textBox3.Visible = false;
            }            
        catch
            {
                MessageBox.Show("Seçilen Tarih'e Ait Sepette Ürün Yok!");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        { 
            try
            {
                dataGridView4.Rows.Clear();
                var bul = db.UrunSepeti.Where(p => p.Tarih == tarih).ToList();
                int i = 0;
                string kontrol = "";
                foreach (var u in bul)
                {
                    if (u.MusteriAdi == kontrol)
                    {
                    }
                    else
                    {
                    dataGridView4.Rows.Add();
                    dataGridView4.Rows[i].Cells[0].Value = u.MusteriAdi;
                    i++;
                        kontrol = u.MusteriAdi;
                    }
                    UyariBilgisi.Text = "Bu Gün Sepette Bekeleyen Ürünleriniz Var!";
                }
            }
            catch
            {
                UyariBilgisi.Text = "VERİTABANINA BAĞLANILAMAI!";
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            a= Convert.ToInt32(comboBox4.SelectedItem);
            Form1_Load(sender, e);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView8.Rows.Clear();
                double TUG1, TUG2=0, TUA1, TUA2=0, TUF1, TUF2=0; 
                var bul = db.UrunSepeti.ToList();
                int i = 0;
                foreach (var m in bul)
                {
                    dataGridView8.Rows.Add();
                    dataGridView8.Rows[i].Cells[0].Value = m.MusteriAdi;
                    dataGridView8.Rows[i].Cells[1].Value = m.UrunAdi;
                    dataGridView8.Rows[i].Cells[2].Value = m.UrunGramaji;
                    dataGridView8.Rows[i].Cells[3].Value = m.UrunAdedi;
                    dataGridView8.Rows[i].Cells[4].Value = m.UrunAmbalaji;
                    dataGridView8.Rows[i].Cells[5].Value = m.UrunFiyati;
                    dataGridView8.Rows[i].Cells[6].Value = m.Tarih;
                    i++;
                    TUG1 = Convert.ToDouble(m.UrunGramaji);
                    TUG2 = TUG2 + TUG1;
                    TUA1 = Convert.ToDouble(m.UrunAdedi);
                    TUA2 = TUA2 + TUA1;
                    TUF1 = Convert.ToDouble(m.UrunFiyati);
                    TUF2 = TUF2 + TUF1;
                }
                textBox10.Text = TUG2.ToString();
                textBox9.Text = TUF2.ToString();
                textBox8.Text = TUA2.ToString();
                textBox3.Visible = false;
            }
            catch
            {
                MessageBox.Show("Ürün Sepeti Tamamen Boş!");
            }
        }

        private void button17_Click(object sender, EventArgs e)
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
                for (int j = 0; j < dataGridView8.Columns.Count; j++)
                {
                    Range myRange = (Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = dataGridView8.Columns[j].HeaderText;
                }
                StartRow++;
                for (int i = 0; i < dataGridView8.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView8.Columns.Count; j++)
                    {

                        Range myRange = (Range)sheet1.Cells[StartRow + i, StartCol + j];
                        myRange.Value2 = dataGridView8[j, i].Value == null ? "" : dataGridView8[j, i].Value;
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

        private void button19_Click(object sender, EventArgs e)
        {
            if (AdminKontrol != "yerel" && AdminKontrol != "admin")
            {
                MessageBox.Show("Yalnızca Yerel ve admin Kullanıcı siparişi iptal edebilir!");
            }
            else
            {
                if (textBox3.Text == "")
                {
                    MessageBox.Show("Önce Sipariş Seçilmeli!");
                }
                else
                {
                    DialogResult Uyari = new DialogResult();
                    Uyari = MessageBox.Show(textBox3.Text + " Adlı Müşterinin Tüm Siparişleri Tamemen Silinecek Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                    if (Uyari == DialogResult.Yes)
                    {
                        try
                        {
                            var bul = db.UrunSepeti.Where(p => p.MusteriAdi == textBox3.Text).ToList();
                            foreach (var n in bul)
                            {
                                double adet = Convert.ToDouble(n.UrunAdedi);
                                string urunadi = n.UrunAdi;
                                var adibul = db.Urunler.Where(p => p.UrunAdi == urunadi).FirstOrDefault();
                                double stokadet = Convert.ToDouble(adibul.UrunAdedi);
                                double topla = stokadet + adet;
                                adibul.UrunAdedi = topla.ToString();
                                db.UrunSepeti.Remove(n);
                                db.SaveChanges();
                                Form1_Load(sender, e);
                                MessageBox.Show("Sipariş Silindi");
                                textBox3.Text = "";
                            }

                        }
                        catch
                        {

                        }
                    }
                }
            }  
        }

        private void button24_Click(object sender, EventArgs e)
        {
            string selicideger = Convert.ToDateTime(dateTimePicker2.Text).ToShortDateString();
            if (checkBox2.Checked==true)
            {
                dataGridView6.Rows.Clear();
                try
                {
                   var bul=  db.Raporlama.Where(p => p.Ay == MevcutAy).ToList();
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
                    Form1_Load(sender, e);
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
                    var bul = db.Raporlama.Where(p => p.Tarih == selicideger).ToList();
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
                    Form1_Load(sender, e);
                }
                catch
                {

                }
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if(AdminKontrol=="admin" || AdminKontrol=="stok")
            {
                DialogResult Uyari = new DialogResult();
                Uyari = MessageBox.Show("sipariş tamamalanacak Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                if (Uyari == DialogResult.Yes)
                {
                    try
                    {
                        var bul = db.UrunSepeti.Where(p => p.MusteriAdi == textBox3.Text).ToList();
                        foreach (var n in bul)
                        {
                            Raporlama ekle = new Raporlama();
                            ekle.Ay = MevcutAy;
                            ekle.FaturaDurumu = n.UrunFaturasi;
                            ekle.Fiyati = n.UrunFiyati;
                            ekle.GidenMusteriler = n.MusteriAdi;
                            ekle.GidenUrunler = n.UrunAdi;
                            ekle.Gun = MevcutGun;
                            ekle.Tarih = tarih;
                            ekle.UrunAdedi = n.UrunAdedi;
                            ekle.UrunGramaji = n.UrunGramaji;
                            ekle.UrunPaketi = n.UrunAmbalaji;
                            ekle.Yil = MevcutYil;
                            db.Raporlama.Add(ekle);
                            db.UrunSepeti.Remove(n);
                            db.SaveChanges();
                            Form1_Load(sender, e);
                            MessageBox.Show("Sipariş Tamamlanadı");
                            textBox3.Text = "";
                        }
                        dataGridView8.Rows.Clear();
                    }
                    catch
                    {

                    }
                }

                }
            else
            {
                MessageBox.Show("Sadece admin ve stok yetkisi olan kullanıcı siparişi tamamlayabilir!");
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            dataGridView8.Rows.Clear();
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
    }
}


