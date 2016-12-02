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
    public partial class GirisEkrani : Form
    {
        public StokMatikEntities db;
        public bool yenidenbaslama;
        public string Baglanti;
        public GirisEkrani()
        {
            InitializeComponent();
            db = new StokMatikEntities();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Kullanıcı Adını Girin!", "UYARI!");
            }
            else
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("Şifreyi Girin!", "UYARI!");
                }
                else
                {
                    try
                    {
                        var KullaniciBul = db.Kullanicilar.Where(p => p.KullaniciAdi == textBox1.Text).FirstOrDefault();
                        if (KullaniciBul.KullaniciAdi == textBox1.Text)
                        {
                            if (KullaniciBul.KullaniciSifresi == textBox2.Text)
                            {

                                Form1 frm = new Form1();
                                frm.Show();
                                frm.Kullaniciata.Text = KullaniciBul.KullaniciAdi;
                                frm.AdminKontrol = KullaniciBul.KullaniciYetkisi;
                                frm.UyariBilgisi.Text = "Tüm Sistemler Normal Çalışıyor";
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Şifre Yanlış!", "HATA!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı Bulunamadı!", "HATA!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) // tüm texboxlar seçiliyken büyük harf kullanma.
        {
            TextBox txBox = (TextBox)sender;
            int pos = txBox.SelectionStart;
            int slen = txBox.SelectionLength;
            txBox.Text = txBox.Text.ToUpper();
            txBox.SelectionStart = pos;
            txBox.SelectionLength = slen;
            txBox.Focus();
        }

        private void GirisEkrani_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void GirisEkrani_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

       
    }
}
