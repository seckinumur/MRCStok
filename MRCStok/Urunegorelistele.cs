using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace MRCStok
{
    public partial class Urunegorelistele : Form
    {
        public StokMatikEntities db;
        public StokmatikHammaddeEntities db1;
        public Form1 ff11 = (Form1)System.Windows.Forms.Application.OpenForms["Form1"];
        public TedarikciEkle urunegore = (TedarikciEkle) Application.OpenForms["TedarikciEkle"];
        public string gelenurunrapor;
        public bool gelenrapor2 = false;
        public Urunegorelistele()
        {
            InitializeComponent();
            db = new StokMatikEntities();
            db1 = new StokmatikHammaddeEntities();
        }

        private void Urunegorelistele_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'stokMatikDataSet.Urunler' table. You can move, or remove it, as needed.
            this.urunlerTableAdapter.Fill(this.stokMatikDataSet.Urunler);

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if(gelenurunrapor=="1")
            {
                string str = gridView1.FocusedValue.ToString();
                string Urungramajinial = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "UrunGramaji").ToString();
                string urunpaketinial = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "UrunPaketi").ToString();
                string seciliay = Convert.ToDateTime(ff11.dateTimePicker2.Text).Month.ToString();
                if (ff11.checkBox2.Checked == true)
                {
                    ff11.dataGridView6.Rows.Clear();
                    try
                    {
                        var bul = db.Raporlama.Where(p => p.Ay == seciliay && p.GidenUrunler == str && p.UrunGramaji==Urungramajinial && p.UrunPaketi==urunpaketinial).ToList();
                        int i = 0;
                        foreach (var m in bul)
                        {
                            ff11.dataGridView6.Rows.Add();
                            ff11.dataGridView6.Rows[i].Cells[0].Value = m.GidenMusteriler;
                            ff11.dataGridView6.Rows[i].Cells[1].Value = m.GidenUrunler;
                            ff11.dataGridView6.Rows[i].Cells[2].Value = m.UrunGramaji;
                            ff11.dataGridView6.Rows[i].Cells[3].Value = m.UrunAdedi;
                            ff11.dataGridView6.Rows[i].Cells[4].Value = m.UrunPaketi;
                            ff11.dataGridView6.Rows[i].Cells[5].Value = m.Fiyati;
                            ff11.dataGridView6.Rows[i].Cells[6].Value = m.FaturaDurumu;
                            ff11.dataGridView6.Rows[i].Cells[7].Value = m.Tarih;
                            i++;
                        }
                        this.Close();
                    }
                    catch
                    {

                    }
                }
                else
                {
                    int n = 0;
                    ff11.dataGridView6.Rows.Clear();
                    DateTime ilktarih = ff11.dateTimePicker2.Value.Date;
                    DateTime sontarih = ff11.dateTimePicker1.Value.Date;
                    if (ilktarih.ToShortDateString() == sontarih.ToShortDateString())
                    {
                        try
                        {
                            string tarih = ilktarih.ToShortDateString();
                            var bul = db.Raporlama.Where(p => p.Tarih == tarih && p.GidenUrunler == str && p.UrunGramaji == Urungramajinial && p.UrunPaketi == urunpaketinial).ToList();
                            foreach (var m in bul)
                            {
                                ff11.dataGridView6.Rows.Add();
                                ff11.dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                                ff11.dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                                ff11.dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                                ff11.dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                                ff11.dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                                ff11.dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                                ff11.dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                                ff11.dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                                n++;
                            }
                            this.Close();
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
                                var bul = db.Raporlama.Where(p => p.Tarih == tarih && p.GidenUrunler == str && p.UrunGramaji == Urungramajinial && p.UrunPaketi == urunpaketinial).ToList();
                                foreach (var m in bul)
                                {
                                    ff11.dataGridView6.Rows.Add();
                                    ff11.dataGridView6.Rows[n].Cells[0].Value = m.GidenMusteriler;
                                    ff11.dataGridView6.Rows[n].Cells[1].Value = m.GidenUrunler;
                                    ff11.dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                                    ff11.dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                                    ff11.dataGridView6.Rows[n].Cells[4].Value = m.UrunPaketi;
                                    ff11.dataGridView6.Rows[n].Cells[5].Value = m.Fiyati;
                                    ff11.dataGridView6.Rows[n].Cells[6].Value = m.FaturaDurumu;
                                    ff11.dataGridView6.Rows[n].Cells[7].Value = m.Tarih;
                                    n++;
                                }
                                this.Close();
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            
            }
            else if (gelenurunrapor == "2")  // Filmin başladığı yer hacı
            {
                string str = gridView1.FocusedValue.ToString();
                string Urungramajinial = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "UrunGramaji").ToString();
                string urunpaketinial = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "UrunPaketi").ToString();
                string seciliay = Convert.ToDateTime(ff11.dateTimePicker2.Text).Month.ToString();
                if (ff11.checkBox2.Checked == true)
                {
                    ff11.dataGridView6.Rows.Clear();
                    try
                    {
                        int i = 0;
                        var bul = db1.Uretim.Where(p => p.Ay== seciliay && p.UrunAdi==str && p.UrunGramaji == Urungramajinial && p.UrunAmbalaji == urunpaketinial).ToList();
                        foreach (var m in bul)
                        {
                            ff11.dataGridView6.Rows.Add();
                            if (m.Uretici == null)
                            {
                                ff11.dataGridView6.Rows[i].Cells[0].Value = "MRC ÜRETİM";
                                ff11.dataGridView6.Rows[i].Cells[6].Value = "###";
                            }
                            else
                            {
                                ff11.dataGridView6.Rows[i].Cells[0].Value = m.Uretici;
                                ff11.dataGridView6.Rows[i].Cells[6].Value = m.UreticiFaturasi;
                            }

                            ff11.dataGridView6.Rows[i].Cells[1].Value = m.UrunAdi;
                            ff11.dataGridView6.Rows[i].Cells[2].Value = m.UrunGramaji;
                            ff11.dataGridView6.Rows[i].Cells[3].Value = m.UrunAdedi;
                            ff11.dataGridView6.Rows[i].Cells[4].Value = m.UrunAmbalaji;
                            ff11.dataGridView6.Rows[i].Cells[5].Value = "###";
                            ff11.dataGridView6.Rows[i].Cells[7].Value = m.UrunUretimTarihi;
                            i++;
                        }
                        this.Close();
                    }
                    catch
                    {

                    }
                }
                else
                {
                    int n = 0;
                    ff11.dataGridView6.Rows.Clear();
                    DateTime ilktarih = ff11.dateTimePicker2.Value.Date;
                    DateTime sontarih = ff11.dateTimePicker1.Value.Date;
                    if (ilktarih.ToShortDateString() == sontarih.ToShortDateString())
                    {
                        try
                        {
                            string tarih = ilktarih.ToShortDateString();
                            var bul = db1.Uretim.Where(p => p.UrunUretimTarihi==tarih && p.UrunAdi == str && p.UrunGramaji == Urungramajinial && p.UrunAmbalaji == urunpaketinial).ToList();
                            foreach (var m in bul)
                            {
                                ff11.dataGridView6.Rows.Add();
                                if (m.Uretici == null)
                                {
                                    ff11.dataGridView6.Rows[n].Cells[0].Value = "MRC ÜRETİM";
                                    ff11.dataGridView6.Rows[n].Cells[6].Value = "###";
                                }
                                else
                                {
                                    ff11.dataGridView6.Rows[n].Cells[0].Value = m.Uretici;
                                    ff11.dataGridView6.Rows[n].Cells[6].Value = m.UreticiFaturasi;
                                }

                                ff11.dataGridView6.Rows[n].Cells[1].Value = m.UrunAdi;
                                ff11.dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                                ff11.dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                                ff11.dataGridView6.Rows[n].Cells[4].Value = m.UrunAmbalaji;
                                ff11.dataGridView6.Rows[n].Cells[5].Value = "###";
                                ff11.dataGridView6.Rows[n].Cells[7].Value = m.UrunUretimTarihi;
                                n++;
                            }
                            this.Close();
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
                                var bul = db1.Uretim.Where(p => p.UrunUretimTarihi == tarih && p.UrunAdi == str && p.UrunGramaji == Urungramajinial && p.UrunAmbalaji == urunpaketinial).ToList();
                                foreach (var m in bul)
                                {
                                    ff11.dataGridView6.Rows.Add();
                                    if (m.Uretici == null)
                                    {
                                        ff11.dataGridView6.Rows[n].Cells[0].Value = "MRC ÜRETİM";
                                        ff11.dataGridView6.Rows[n].Cells[6].Value = "###";
                                    }
                                    else
                                    {
                                        ff11.dataGridView6.Rows[n].Cells[0].Value = m.Uretici;
                                        ff11.dataGridView6.Rows[n].Cells[6].Value = m.UreticiFaturasi;
                                    }

                                    ff11.dataGridView6.Rows[n].Cells[1].Value = m.UrunAdi;
                                    ff11.dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                                    ff11.dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                                    ff11.dataGridView6.Rows[n].Cells[4].Value = m.UrunAmbalaji;
                                    ff11.dataGridView6.Rows[n].Cells[5].Value = "###";
                                    ff11.dataGridView6.Rows[n].Cells[7].Value = m.UrunUretimTarihi;
                                    n++;
                                }
                                this.Close();
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }
            if (gelenrapor2==true)
            {
                urunegore.Hide();
                this.Hide();
                string str = gridView1.FocusedValue.ToString();
                string Urungramajinial = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "UrunGramaji").ToString();
                string urunpaketinial = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "UrunPaketi").ToString();
                try
                {
                    var bul = db.Urunler.Where(p => p.UrunAdi == str && p.UrunGramaji == Urungramajinial && p.UrunPaketi == urunpaketinial).FirstOrDefault();
                    string Urunadedi = Interaction.InputBox(str+" ADLI ÜRÜNÜN ADEDİNİ GİRİN:", "ÜRÜN ADEDİ", "", 680, 380);
                    if (Urunadedi == "0" || Urunadedi=="")
                    {
                        MessageBox.Show("Sıfır Girilemez");
                    }
                    else
                    {
                        urunegore.dataGridView1.Rows.Add();
                        urunegore.dataGridView1.Rows[urunegore.Sayac].Cells[0].Value = bul.UrunAdi;
                        urunegore.dataGridView1.Rows[urunegore.Sayac].Cells[1].Value = Urunadedi;
                        urunegore.dataGridView1.Rows[urunegore.Sayac].Cells[2].Value = bul.UrunGramaji;
                        urunegore.dataGridView1.Rows[urunegore.Sayac].Cells[3].Value = bul.UrunPaketi;
                        urunegore.dataGridView1.Rows[urunegore.Sayac].Cells[4].Value = bul.UrunFiyati;
                        urunegore.Sayac++;
                    }
                    urunegore.Visible = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    urunegore.Visible = true;
                    this.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
