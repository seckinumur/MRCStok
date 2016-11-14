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
    public partial class VeritabaniSecme : Form
    {
        public GirisEkrani f1 = (GirisEkrani)Application.OpenForms["GirisEkrani"];
        public VeritabaniSecme()
        {
            InitializeComponent();
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f1.db.Database.Connection.ConnectionString = textBox1.Text;
            
            this.Close();
        }
    }
}
