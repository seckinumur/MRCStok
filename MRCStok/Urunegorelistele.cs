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
        public Form1 ff11 = (Form1)System.Windows.Forms.Application.OpenForms["Form1"];
        public Urunegorelistele()
        {
            InitializeComponent();
            db = new StokMatikEntities();
        }

        private void Urunegorelistele_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'stokMatikDataSet.Urunler' table. You can move, or remove it, as needed.
            this.urunlerTableAdapter.Fill(this.stokMatikDataSet.Urunler);

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            string str = gridView1.FocusedValue.ToString();
            string selicideger = Convert.ToDateTime(ff11.dateTimePicker2.Text).ToShortDateString();
            string seciliay = Convert.ToDateTime(ff11.dateTimePicker2.Text).Month.ToString();
            if (ff11.checkBox2.Checked == true)
            {
                ff11.dataGridView6.Rows.Clear();
                try
                {
                    var bul = db.Raporlama.Where(p => p.Ay == seciliay && p.GidenUrunler==str).ToList();
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
                    ff11.Form1_Load(sender, e);
                    this.Close();
                }
                catch
                {

                }
            }
            else
            {
                ff11.dataGridView6.Rows.Clear();
                try
                {
                    var bul = db.Raporlama.Where(p => p.Tarih == selicideger && p.GidenUrunler == str).ToList();
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
                    ff11.Form1_Load(sender, e);
                    this.Close();
                }
                catch
                {

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
