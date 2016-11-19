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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (counter == false)
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
