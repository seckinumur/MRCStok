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
    public partial class GidenMusteriDuzenle : Form
    {
        public StokMatikEntities db;
        public Form1 f213 = (Form1)System.Windows.Forms.Application.OpenForms["Form1"];
        public GirisEkrani f211 = (GirisEkrani)System.Windows.Forms.Application.OpenForms["GirisEkrani"];
        public string Musteri;
        public string adedinial;
        public GidenMusteriDuzenle()
        {
            InitializeComponent();
            db = new StokMatikEntities();
        }

        private void GidenMusteriDuzenle_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
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
            string selicideger = Convert.ToDateTime(f213.dateTimePicker2.Text).ToShortDateString();
            string seciliay = Convert.ToDateTime(f213.dateTimePicker2.Text).Month.ToString();
            string adet = dataGridView1.Rows[ykoordinat].Cells[1].Value.ToString();
            adedinial = adet;
            if (f213.checkBox2.Checked == true)
            {
                try
                {
                    var bul = db.Raporlama.Where(p => p.Ay == seciliay && p.GidenUrunler == str && p.GidenMusteriler == textBox1.Text && p.UrunAdedi == adet).FirstOrDefault();
                    textBox7.Text = bul.GidenUrunler;
                    UrunGramajiUrunEkle.SelectedItem = bul.UrunGramaji;
                    textBox3.Text = bul.UrunAdedi;
                    adedinial = bul.UrunAdedi;
                    AmbalajUrunEkle.SelectedItem = bul.UrunPaketi;
                    textBox6.Text = bul.Fiyati;
                    textBox16.Text = bul.FaturaDurumu;
                    
                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    var bul = db.Raporlama.Where(p => p.Tarih == selicideger && p.GidenUrunler == str && p.GidenMusteriler == textBox1.Text && p.UrunAdedi == adet).FirstOrDefault();

                    textBox7.Text = bul.GidenUrunler;
                    UrunGramajiUrunEkle.SelectedItem = bul.UrunGramaji;
                    textBox3.Text = bul.UrunAdedi;
                    adedinial = bul.UrunAdedi;
                    AmbalajUrunEkle.SelectedItem = bul.UrunPaketi;
                    textBox6.Text = bul.Fiyati;
                    textBox16.Text = bul.FaturaDurumu;
                    
                }
                catch
                {

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                MessageBox.Show("ÖNCE ÜRÜN SEÇİN");
            }
            else
            {
                string selicideger = Convert.ToDateTime(f213.dateTimePicker2.Text).ToShortDateString();
                string seciliay = Convert.ToDateTime(f213.dateTimePicker2.Text).Month.ToString();
                if (f213.checkBox2.Checked == true)
                {
                    try
                    {
                        double sonuc = 0;
                        var ekle = db.Urunler.Where(p => p.UrunAdi == textBox7.Text).FirstOrDefault();
                        double urunadedi = Convert.ToDouble(ekle.UrunAdedi);
                        var bul = db.Raporlama.Where(p => p.Ay == seciliay && p.GidenUrunler == textBox7.Text && p.GidenMusteriler == textBox1.Text && p.UrunAdedi == adedinial).FirstOrDefault();
                        double seciliadet = Convert.ToDouble(bul.UrunAdedi);
                        double girilenadet = Convert.ToDouble(textBox3.Text);
                        if (girilenadet < seciliadet)
                        {
                            sonuc = seciliadet - girilenadet;
                            double sonuc2 = urunadedi + sonuc;
                            ekle.UrunAdedi = sonuc2.ToString();

                        }
                        else
                        {
                            sonuc = girilenadet - seciliadet;
                            double sonuc2 = urunadedi - sonuc;
                            ekle.UrunAdedi = sonuc2.ToString();
                        }
                        bul.GidenUrunler = textBox7.Text;
                        bul.UrunGramaji = UrunGramajiUrunEkle.SelectedItem.ToString();
                        bul.UrunAdedi = textBox3.Text;
                        bul.UrunPaketi = AmbalajUrunEkle.SelectedItem.ToString();
                        bul.Fiyati = textBox6.Text;
                        bul.FaturaDurumu = textBox16.Text;
                        
                        bul.Ay = DateTime.Now.Month.ToString();
                        bul.Gun = DateTime.Now.Day.ToString();
                        bul.Yil = DateTime.Now.Year.ToString();
                        bul.Tarih = DateTime.Now.ToShortDateString();
                        db.SaveChanges();
                        f213.dataGridView6.Rows.Clear();
                        adedinial = "";
                        f211.yenidenbaslama = true;
                        f213.Close();
                        Form1 frm = new Form1();
                        frm.Show();
                        this.Close();
                        MessageBox.Show("ÜRÜN DÜZELTMESİ TAMAMLANDI!");
                        f213.Refresh();
                    }
                    catch
                    {

                    }

                }
                else
                {
                    try
                    {
                        double sonuc = 0;
                        var ekle = db.Urunler.Where(p => p.UrunAdi == textBox7.Text).FirstOrDefault();
                        double urunadedi = Convert.ToDouble(ekle.UrunAdedi);
                        var bul = db.Raporlama.Where(p => p.Tarih == selicideger && p.GidenUrunler == textBox7.Text && p.GidenMusteriler == textBox1.Text && p.UrunAdedi == adedinial).FirstOrDefault();
                        double seciliadet = Convert.ToDouble(bul.UrunAdedi);
                        double girilenadet = Convert.ToDouble(textBox3.Text);
                        if (girilenadet < seciliadet)
                        {
                            sonuc = seciliadet - girilenadet;
                            double sonuc2 = urunadedi + sonuc;
                            ekle.UrunAdedi = sonuc2.ToString();

                        }
                        else
                        {
                            sonuc = girilenadet - seciliadet;
                            double sonuc2 = urunadedi - sonuc;
                            ekle.UrunAdedi = sonuc2.ToString();
                        }
                        bul.GidenUrunler = textBox7.Text;
                        bul.UrunGramaji = UrunGramajiUrunEkle.SelectedItem.ToString();
                        bul.UrunAdedi = textBox3.Text;
                        bul.UrunPaketi = AmbalajUrunEkle.SelectedItem.ToString();
                        bul.Fiyati = textBox6.Text;
                        bul.FaturaDurumu = textBox16.Text;
                        bul.Ay = DateTime.Now.Month.ToString();
                        bul.Gun = DateTime.Now.Day.ToString();
                        bul.Yil = DateTime.Now.Year.ToString();
                        bul.Tarih = DateTime.Now.ToShortDateString();
                        db.SaveChanges();
                        f213.dataGridView6.Rows.Clear();
                        adedinial = "";
                        f211.yenidenbaslama = true;
                        f213.Close();
                        Form1 frm = new Form1();
                        frm.Show();
                        this.Close();
                        MessageBox.Show("ÜRÜN DÜZELTMESİ TAMAMLANDI!");
                        
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult Uyari = new DialogResult();
            Uyari = MessageBox.Show("MÜŞTERİNİN BU GÖNDERİSİ TAMAMEN SİLİNECEK Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
            if (Uyari == DialogResult.Yes)
            {
                if (textBox7.Text == "")
                {
                    MessageBox.Show("ÖNCE ÜRÜN SEÇİN");
                }
                else
                {
                    string selicideger = Convert.ToDateTime(f213.dateTimePicker2.Text).ToShortDateString();
                    string seciliay = Convert.ToDateTime(f213.dateTimePicker2.Text).Month.ToString();
                    if (f213.checkBox2.Checked == true)
                    {
                        try
                        {
                            var bul = db.Raporlama.Where(p => p.Ay == seciliay && p.GidenUrunler == textBox7.Text && p.GidenMusteriler == textBox1.Text && p.UrunAdedi == adedinial).FirstOrDefault();
                            var ekle = db.Urunler.Where(p => p.UrunAdi == bul.GidenUrunler).FirstOrDefault();
                            double urunadedi = Convert.ToDouble(ekle.UrunAdedi);
                            double seciliadet = Convert.ToDouble(bul.UrunAdedi);
                            double sonuc = urunadedi + seciliadet;
                            ekle.UrunAdedi = sonuc.ToString();
                            db.Raporlama.Remove(bul);
                            db.SaveChanges();
                            MessageBox.Show("MÜŞTERİNİN BU GÖNDERİSİ TAMAMEN SİLİNDİ");
                            f213.dataGridView6.Rows.Clear();
                            f213.Form1_Load(sender, e);
                            adedinial = "";
                            this.Close();
                        }
                        catch
                        {

                        }

                    }
                    else
                    {
                        try
                        {
                            var bul = db.Raporlama.Where(p => p.Tarih == selicideger && p.GidenUrunler == textBox7.Text && p.GidenMusteriler == textBox1.Text && p.UrunAdedi == adedinial).FirstOrDefault();
                            var ekle = db.Urunler.Where(p => p.UrunAdi == bul.GidenUrunler).FirstOrDefault();
                            double urunadedi = Convert.ToDouble(ekle.UrunAdedi);
                            double seciliadet = Convert.ToDouble(bul.UrunAdedi);
                            double sonuc = urunadedi + seciliadet;
                            ekle.UrunAdedi = sonuc.ToString();
                            db.Raporlama.Remove(bul);
                            db.SaveChanges();
                            MessageBox.Show("MÜŞTERİNİN BU GÖNDERİSİ TAMAMEN SİLİNDİ");
                            f213.dataGridView6.Rows.Clear();
                            f213.Form1_Load(sender, e);
                            adedinial = "";
                            this.Close();

                        }
                        catch
                        {

                        }
                    }
                }
            }
            }
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult Uyari = new DialogResult();
            Uyari = MessageBox.Show("MÜŞTERİNİN BU GÖNDERİLERİ TAMAMEN SİLİNECEK Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
            if (Uyari == DialogResult.Yes)
            {
                string selicideger = Convert.ToDateTime(f213.dateTimePicker2.Text).ToShortDateString();
                string seciliay = Convert.ToDateTime(f213.dateTimePicker2.Text).Month.ToString();
                if (f213.checkBox2.Checked == true)
                {

                    try
                    {
                        var bul = db.Raporlama.Where(p => p.GidenMusteriler == textBox1.Text && p.Ay == seciliay).ToList();
                       
                        foreach (var n in bul)
                        {
                            var ekle = db.Urunler.Where(p => p.UrunAdi == n.GidenUrunler).FirstOrDefault();
                            double urunadedi = Convert.ToDouble(ekle.UrunAdedi);
                            double seciliadet = Convert.ToDouble(n.UrunAdedi);
                            double sonuc = urunadedi + seciliadet;
                            ekle.UrunAdedi = sonuc.ToString();
                            db.Raporlama.Remove(n);
                            db.SaveChanges();
                        }
                        MessageBox.Show("MÜŞTERİNİN BU GÖNDERİLERİ TAMAMEN SİLİNDİ");
                        f213.dataGridView6.Rows.Clear();
                        adedinial = "";
                        this.Close();
                        
                    }
                    catch
                    {

                    }
                }
                else
                {
                    try
                    {
                        var bul = db.Raporlama.Where(p => p.GidenMusteriler == textBox1.Text && p.Tarih == selicideger).ToList();
                        foreach (var n in bul)
                        {
                            var ekle = db.Urunler.Where(p => p.UrunAdi == n.GidenUrunler).FirstOrDefault();
                            double urunadedi = Convert.ToDouble(ekle.UrunAdedi);
                            double seciliadet = Convert.ToDouble(n.UrunAdedi);
                            double sonuc = urunadedi + seciliadet;
                            ekle.UrunAdedi = sonuc.ToString();
                            db.Raporlama.Remove(n);
                            db.SaveChanges();
                        }
                        MessageBox.Show("MÜŞTERİNİN BU GÖNDERİLERİ TAMAMEN SİLİNDİ");
                        f213.dataGridView6.Rows.Clear();
                        adedinial = "";
                        this.Close();
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
