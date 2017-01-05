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
            //string yetki = f213.AdminKontrol;
            //f211.yenidenbaslama = true;
            //f213.Close();
            //Form1 frm = new Form1();
            //frm.Show();
            //frm.AdminKontrol = yetki;
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
            string gramajii = dataGridView1.Rows[ykoordinat].Cells[3].Value.ToString();
            string ambalajiii = dataGridView1.Rows[ykoordinat].Cells[4].Value.ToString();
            adiurunun.Text = str;
            gramajiurunun.Text = gramajii;
            ambalajiurunun.Text = ambalajiii;
            adedinial = adet;
            if (f213.checkBox2.Checked == true)
            {
                try
                {
                    var bul = db.Raporlama.Where(p => p.Ay == seciliay && p.GidenUrunler == str && p.GidenMusteriler == textBox1.Text && p.UrunAdedi == adet && p.UrunGramaji == gramajii && p.UrunPaketi == ambalajiii).FirstOrDefault();
                    textBox7.Text = bul.GidenUrunler;
                    UrunGramajiUrunEkle.SelectedItem = bul.UrunGramaji;
                    textBox3.Text = bul.UrunAdedi;
                    adedinial = bul.UrunAdedi;
                    AmbalajUrunEkle.SelectedItem = bul.UrunPaketi;
                    textBox6.Text = bul.Fiyati;
                    if (bul.FaturaDurumu == "FATURASIZ") { textBox16.Enabled = false; comboBox1.SelectedItem = bul.FaturaDurumu; }
                    else if (bul.FaturaDurumu == "BEDELSİZ") { textBox16.Enabled = false; comboBox1.SelectedItem = bul.FaturaDurumu; }
                    else if (bul.FaturaDurumu == "NUMUNE") { textBox16.Enabled = false; comboBox1.SelectedItem = bul.FaturaDurumu; }
                    else if (bul.FaturaDurumu == "ZAYİ") { textBox16.Enabled = false; comboBox1.SelectedItem = bul.FaturaDurumu; }
                    else if (bul.FaturaDurumu == "İADE") { textBox16.Enabled = false; comboBox1.SelectedItem = bul.FaturaDurumu; }
                    else if (bul.FaturaDurumu == "FATURASIZ ÖDEME") { textBox16.Enabled = false; comboBox1.SelectedItem = bul.FaturaDurumu; }
                    else { textBox16.Enabled = true; textBox16.Text = bul.FaturaDurumu; comboBox1.SelectedItem = "FATURALI"; }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    var bul = db.Raporlama.Where(p => p.Tarih == selicideger && p.GidenUrunler == str && p.GidenMusteriler == textBox1.Text && p.UrunAdedi == adet && p.UrunGramaji == gramajii && p.UrunPaketi == ambalajiii).FirstOrDefault();

                    textBox7.Text = bul.GidenUrunler;
                    UrunGramajiUrunEkle.SelectedItem = bul.UrunGramaji;
                    textBox3.Text = bul.UrunAdedi;
                    adedinial = bul.UrunAdedi;
                    AmbalajUrunEkle.SelectedItem = bul.UrunPaketi;
                    textBox6.Text = bul.Fiyati;
                    if (bul.FaturaDurumu == "FATURASIZ") { textBox16.Enabled = false; comboBox1.SelectedItem = bul.FaturaDurumu; }
                    else if (bul.FaturaDurumu == "BEDELSİZ") { textBox16.Enabled = false; comboBox1.SelectedItem = bul.FaturaDurumu; }
                    else if (bul.FaturaDurumu == "NUMUNE") { textBox16.Enabled = false; comboBox1.SelectedItem = bul.FaturaDurumu; }
                    else if (bul.FaturaDurumu == "ZAYİ") { textBox16.Enabled = false; comboBox1.SelectedItem = bul.FaturaDurumu; }
                    else if (bul.FaturaDurumu == "İADE") { textBox16.Enabled = false; comboBox1.SelectedItem = bul.FaturaDurumu; }
                    else if (bul.FaturaDurumu == "FATURASIZ ÖDEME") { textBox16.Enabled = false; comboBox1.SelectedItem = bul.FaturaDurumu; }
                    else { textBox16.Enabled = true; textBox16.Text = bul.FaturaDurumu; comboBox1.SelectedItem = "FATURALI"; }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
                        var bul = db.Raporlama.Where(p => p.Ay == seciliay && p.GidenUrunler == textBox7.Text && p.GidenMusteriler == textBox1.Text && p.UrunAdedi == adedinial && p.UrunGramaji == gramajiurunun.Text && p.UrunPaketi == ambalajiurunun.Text).FirstOrDefault();
                        var ekle = db.Urunler.Where(p => p.UrunAdi == textBox7.Text && p.UrunGramaji == bul.UrunGramaji && p.UrunPaketi == bul.UrunPaketi).FirstOrDefault();
                        double urunadedi = Convert.ToDouble(ekle.UrunAdedi);
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
                        if (comboBox1.SelectedItem == "FATURASIZ") { textBox16.Enabled = false; bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                        else if (comboBox1.SelectedItem == "BEDELSİZ") { textBox16.Enabled = false; bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                        else if (comboBox1.SelectedItem == "NUMUNE") { textBox16.Enabled = false; bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                        else if (comboBox1.SelectedItem == "ZAYİ") { textBox16.Enabled = false; bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                        else if (comboBox1.SelectedItem == "İADE") { textBox16.Enabled = false; bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                        else if (comboBox1.SelectedItem == "FATURASIZ ÖDEME") { textBox16.Enabled = false; bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                        else { textBox16.Enabled = true; bul.FaturaDurumu = textBox16.Text; comboBox1.SelectedItem = "FATURALI"; }

                        bul.Ay = DateTime.Now.Month.ToString();
                        bul.Gun = DateTime.Now.Day.ToString();
                        bul.Yil = DateTime.Now.Year.ToString();
                        bul.Tarih = DateTime.Now.ToShortDateString();
                        db.SaveChanges();
                        f213.dataGridView6.Rows.Clear();
                        adedinial = "";
                        string yetki = f213.AdminKontrol;
                        f211.yenidenbaslama = true;
                        f213.Close();
                        Form1 frm = new Form1();
                        frm.Show();
                        frm.AdminKontrol = yetki;
                        this.Close();
                        MessageBox.Show("ÜRÜN DÜZELTMESİ TAMAMLANDI!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                else
                {
                    try
                    {
                        double sonuc = 0;
                        var bul = db.Raporlama.Where(p => p.Tarih == selicideger && p.GidenUrunler == textBox7.Text && p.GidenMusteriler == textBox1.Text && p.UrunAdedi == adedinial && p.UrunGramaji == gramajiurunun.Text && p.UrunPaketi == ambalajiurunun.Text).FirstOrDefault();
                        var ekle = db.Urunler.Where(p => p.UrunAdi == textBox7.Text && p.UrunGramaji == bul.UrunGramaji && p.UrunPaketi == bul.UrunPaketi).FirstOrDefault();
                        double urunadedi = Convert.ToDouble(ekle.UrunAdedi);
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
                        if (comboBox1.SelectedItem == "FATURASIZ") { textBox16.Enabled = false; bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                        else if (comboBox1.SelectedItem == "BEDELSİZ") { textBox16.Enabled = false; bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                        else if (comboBox1.SelectedItem == "NUMUNE") { textBox16.Enabled = false; bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                        else if (comboBox1.SelectedItem == "ZAYİ") { textBox16.Enabled = false; bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                        else if (comboBox1.SelectedItem == "İADE") { textBox16.Enabled = false; bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                        else if (comboBox1.SelectedItem == "FATURASIZ ÖDEME") { textBox16.Enabled = false; bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                        else { textBox16.Enabled = true; bul.FaturaDurumu = textBox16.Text; comboBox1.SelectedItem = "FATURALI"; }
                        bul.Ay = DateTime.Now.Month.ToString();
                        bul.Gun = DateTime.Now.Day.ToString();
                        bul.Yil = DateTime.Now.Year.ToString();
                        bul.Tarih = DateTime.Now.ToShortDateString();
                        db.SaveChanges();
                        f213.dataGridView6.Rows.Clear();
                        adedinial = "";
                        string yetki = f213.AdminKontrol;
                        f211.yenidenbaslama = true;
                        f213.Close();
                        Form1 frm = new Form1();
                        frm.Show();
                        frm.AdminKontrol = yetki;
                        this.Close();
                        MessageBox.Show("ÜRÜN DÜZELTMESİ TAMAMLANDI!");
                    }
                    catch (Exception EX)
                    {
                        MessageBox.Show(EX.Message);
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
                            var bul = db.Raporlama.Where(p => p.Ay == seciliay && p.GidenUrunler == textBox7.Text && p.GidenMusteriler == textBox1.Text && p.UrunAdedi == adedinial && p.UrunGramaji == gramajiurunun.Text && p.UrunPaketi == ambalajiurunun.Text).FirstOrDefault();
                            var ekle = db.Urunler.Where(p => p.UrunAdi == bul.GidenUrunler && p.UrunGramaji == bul.UrunGramaji && p.UrunPaketi == bul.UrunPaketi).FirstOrDefault();
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
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                    else
                    {
                        try
                        {
                            var bul = db.Raporlama.Where(p => p.Tarih == selicideger && p.GidenUrunler == textBox7.Text && p.GidenMusteriler == textBox1.Text && p.UrunAdedi == adedinial && p.UrunGramaji == gramajiurunun.Text && p.UrunPaketi == ambalajiurunun.Text).FirstOrDefault();
                            var ekle = db.Urunler.Where(p => p.UrunAdi == bul.GidenUrunler && p.UrunGramaji == bul.UrunGramaji && p.UrunPaketi == bul.UrunPaketi).FirstOrDefault();
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
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
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
                            var ekle = db.Urunler.Where(p => p.UrunAdi == n.GidenUrunler && p.UrunGramaji == n.UrunGramaji && p.UrunPaketi == n.UrunPaketi).FirstOrDefault();
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
                        string yetki = f213.AdminKontrol;
                        f211.yenidenbaslama = true;
                        f213.Close();
                        Form1 frm = new Form1();
                        frm.Show();
                        frm.AdminKontrol = yetki;
                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        var bul = db.Raporlama.Where(p => p.GidenMusteriler == textBox1.Text && p.Tarih == selicideger).ToList();
                        foreach (var n in bul)
                        {
                            var ekle = db.Urunler.Where(p => p.UrunAdi == n.GidenUrunler && p.UrunGramaji == n.UrunGramaji && p.UrunPaketi == n.UrunPaketi).FirstOrDefault();
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
                        string yetki = f213.AdminKontrol;
                        f211.yenidenbaslama = true;
                        f213.Close();
                        Form1 frm = new Form1();
                        frm.Show();
                        frm.AdminKontrol = yetki;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar);
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar);
        }

        private void UrunGramajiUrunEkle_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != "FATURALI") { textBox16.Enabled = false; }
            else { textBox16.Enabled = true; }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "") { MessageBox.Show("Önce fatura durumunu seçin"); }
            else if (comboBox1.SelectedItem == "FATURALI" && textBox16.Text == "") { MessageBox.Show("Fatura Numarasını Girmeniz Gerekiyor"); }
            else
            {
                DialogResult Uyari = new DialogResult();
                Uyari = MessageBox.Show("Bu Listedeki Tüm Gönderilerin Fatura Durumu Değişecek. Devam Etmek İstiyormusunuz?", "DİKKAT!", MessageBoxButtons.YesNo);
                if (Uyari == DialogResult.Yes)
                {
                    string selicideger = Convert.ToDateTime(f213.dateTimePicker2.Text).ToShortDateString();
                    string seciliay = Convert.ToDateTime(f213.dateTimePicker2.Text).Month.ToString();
                    if (f213.checkBox2.Checked == true)
                    {
                        try
                        {
                            for (int i = 0; i < dataGridView1.RowCount -1; i++)
                            {
                                string urunadi = dataGridView1.Rows[i].Cells[0].Value.ToString();
                                string adet = dataGridView1.Rows[i].Cells[1].Value.ToString();
                                string gramajii = dataGridView1.Rows[i].Cells[3].Value.ToString();
                                string ambalajiii = dataGridView1.Rows[i].Cells[4].Value.ToString();
                                var bul = db.Raporlama.Where(p => p.Ay == seciliay && p.GidenUrunler == urunadi && p.GidenMusteriler == textBox1.Text && p.UrunAdedi == adet && p.UrunGramaji == gramajii && p.UrunPaketi == ambalajiii).FirstOrDefault();
                                if (comboBox1.SelectedItem == "FATURASIZ") { bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                                else if (comboBox1.SelectedItem == "BEDELSİZ") {  bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                                else if (comboBox1.SelectedItem == "NUMUNE") { bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                                else if (comboBox1.SelectedItem == "ZAYİ") {  bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                                else if (comboBox1.SelectedItem == "İADE") { bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                                else if (comboBox1.SelectedItem == "FATURASIZ ÖDEME") { bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                                else {  bul.FaturaDurumu = textBox16.Text; }
                                db.SaveChanges();
                            }
                            f213.dataGridView6.Rows.Clear();
                            adedinial = "";
                            string yetki = f213.AdminKontrol;
                            f211.yenidenbaslama = true;
                            f213.Close();
                            Form1 frm = new Form1();
                            frm.Show();
                            frm.AdminKontrol = yetki;
                            this.Close();
                            MessageBox.Show("Mevcut Listedeki Tüm Gönderilerin Fatura Durumu Başarıyla Değiştirildi!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        try
                        {
                            for (int i = 0; i < dataGridView1.RowCount -1; i++)
                            {
                                string urunadi = dataGridView1.Rows[i].Cells[0].Value.ToString();
                                string adet = dataGridView1.Rows[i].Cells[1].Value.ToString();
                                string gramajii = dataGridView1.Rows[i].Cells[3].Value.ToString();
                                string ambalajiii = dataGridView1.Rows[i].Cells[4].Value.ToString();
                                var bul = db.Raporlama.Where(p => p.Tarih == selicideger && p.GidenUrunler == urunadi && p.GidenMusteriler == textBox1.Text && p.UrunAdedi == adet && p.UrunGramaji == gramajii && p.UrunPaketi == ambalajiii).FirstOrDefault();
                                if (comboBox1.SelectedItem == "FATURASIZ") { bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                                else if (comboBox1.SelectedItem == "BEDELSİZ") { bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                                else if (comboBox1.SelectedItem == "NUMUNE") { bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                                else if (comboBox1.SelectedItem == "ZAYİ") { bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                                else if (comboBox1.SelectedItem == "İADE") { bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                                else if (comboBox1.SelectedItem == "FATURASIZ ÖDEME") { bul.FaturaDurumu = comboBox1.SelectedItem.ToString(); }
                                else { bul.FaturaDurumu = textBox16.Text; }
                                db.SaveChanges();
                                
                            }
                            f213.dataGridView6.Rows.Clear();
                            adedinial = "";
                            string yetki = f213.AdminKontrol;
                            f211.yenidenbaslama = true;
                            f213.Close();
                            Form1 frm = new Form1();
                            frm.Show();
                            frm.AdminKontrol = yetki;
                            this.Close();
                            MessageBox.Show("Mevcut Listedeki Tüm Gönderilerin Fatura Durumu Başarıyla Değiştirildi!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            //FATURALI //FATURASIZ //BEDELSİZ //NUMUNE //ZAYİ  //İADE //FATURASIZ ÖDEME
        }

        private void button6_Click(object sender, EventArgs e)
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
    }
}
