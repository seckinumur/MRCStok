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
    public partial class VeritabaniSecimi : Form
    {
        public VeritabaniSecimi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.Text = ofd.FileName;
            }
            this.Close();
        }

        private void VeritabaniSecimi_Load(object sender, EventArgs e)
        {

        }
    }
}
