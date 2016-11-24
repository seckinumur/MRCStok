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
    public partial class Urunegorelistele : Form
    {
        public StokMatikEntities db;
        public StokmatikHammaddeEntities db1;
        public Form1 ff11 = (Form1)System.Windows.Forms.Application.OpenForms["Form1"];
        public bool gelenurunrapor=false;
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
            if(gelenurunrapor==false)
            {
                string str = gridView1.FocusedValue.ToString();
                string seciliay = Convert.ToDateTime(ff11.dateTimePicker2.Text).Month.ToString();
                if (ff11.checkBox2.Checked == true)
                {
                    ff11.dataGridView6.Rows.Clear();
                    try
                    {
                        var bul = db.Raporlama.Where(p => p.Ay == seciliay && p.GidenUrunler == str).ToList();
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
                            var bul = db.Raporlama.Where(p => p.Tarih == tarih && p.GidenUrunler == str).ToList();
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
                                var bul = db.Raporlama.Where(p => p.Tarih == tarih && p.GidenUrunler == str).ToList();
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
            else    // Filmin başladığı yer hacı
            {
                string str = gridView1.FocusedValue.ToString();
                string seciliay = Convert.ToDateTime(ff11.dateTimePicker2.Text).Month.ToString();
                if (ff11.checkBox2.Checked == true)
                {
                    ff11.dataGridView6.Rows.Clear();
                    try
                    {
                        int i = 0;
                        var bul = db1.Uretim.Where(p => p.Ay== seciliay && p.UrunAdi==str).ToList();
                        foreach (var m in bul)
                        {
                            ff11.dataGridView6.Rows.Add();
                            ff11.dataGridView6.Rows[i].Cells[0].Value = "MRC ÜRETİM";
                            ff11.dataGridView6.Rows[i].Cells[1].Value = m.UrunAdi;
                            ff11.dataGridView6.Rows[i].Cells[2].Value = m.UrunGramaji;
                            ff11.dataGridView6.Rows[i].Cells[3].Value = m.UrunAdedi;
                            ff11.dataGridView6.Rows[i].Cells[4].Value = m.UrunAmbalaji;
                            ff11.dataGridView6.Rows[i].Cells[5].Value = "###";
                            ff11.dataGridView6.Rows[i].Cells[6].Value = "###";
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
                            var bul = db1.Uretim.Where(p => p.UrunUretimTarihi==tarih && p.UrunAdi == str).ToList();
                            foreach (var m in bul)
                            {
                                ff11.dataGridView6.Rows.Add();
                                ff11.dataGridView6.Rows[n].Cells[0].Value = "MRC ÜRETİM";
                                ff11.dataGridView6.Rows[n].Cells[1].Value = m.UrunAdi;
                                ff11.dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                                ff11.dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                                ff11.dataGridView6.Rows[n].Cells[4].Value = m.UrunAmbalaji;
                                ff11.dataGridView6.Rows[n].Cells[5].Value = "###";
                                ff11.dataGridView6.Rows[n].Cells[6].Value = "###";
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
                                var bul = db1.Uretim.Where(p => p.UrunUretimTarihi == tarih && p.UrunAdi == str).ToList();
                                foreach (var m in bul)
                                {
                                    ff11.dataGridView6.Rows.Add();
                                    ff11.dataGridView6.Rows[n].Cells[0].Value = "MRC ÜRETİM";
                                    ff11.dataGridView6.Rows[n].Cells[1].Value = m.UrunAdi;
                                    ff11.dataGridView6.Rows[n].Cells[2].Value = m.UrunGramaji;
                                    ff11.dataGridView6.Rows[n].Cells[3].Value = m.UrunAdedi;
                                    ff11.dataGridView6.Rows[n].Cells[4].Value = m.UrunAmbalaji;
                                    ff11.dataGridView6.Rows[n].Cells[5].Value = "###";
                                    ff11.dataGridView6.Rows[n].Cells[6].Value = "###";
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
