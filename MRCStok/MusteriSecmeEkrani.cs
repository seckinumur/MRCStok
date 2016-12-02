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
    public partial class MusteriSecmeEkrani : Form
    {
        public Form1 bulma = (Form1)System.Windows.Forms.Application.OpenForms["Form1"];
        public bool counter = false;
        
        public MusteriSecmeEkrani()
        {
            InitializeComponent();
        }

        private void MusteriSecmeEkrani_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'stokMatikDataSet2.Musteriler' table. You can move, or remove it, as needed.
            this.musterilerTableAdapter.Fill(this.stokMatikDataSet2.Musteriler);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if(counter==false)
            {
                string str = gridView1.FocusedValue.ToString();
                try
                {
                    var bul = bulma.db.Musteriler.Where(p => p.MusteriAdi == str).FirstOrDefault();
                    if (bul.MusteriAdi == str)
                    {
                        bulma.textBox2.Text = str;
                        this.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("MÜŞTERİYİ SEÇMEK İÇİN; MÜŞTERİ İSMİNE TIKLAMALISINIZ!");
                }
            }
            else
            {
                string str = gridView1.FocusedValue.ToString();
                try
                {
                    var bul = bulma.db.Musteriler.Where(p => p.MusteriAdi == str).FirstOrDefault();
                    if (bul.MusteriAdi == str)
                    {
                        bulma.textBox7.Text = str;
                        this.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("MÜŞTERİYİ SEÇMEK İÇİN; MÜŞTERİ İSMİNE TIKLAMALISINIZ!");
                }
            }
                   
        }
    }
}
