using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MRCStok
{
    public partial class Safemod : Form
    {
        public string StokMatikyolu;
        public string StokmatikHammadde;
        public string StokmatikSepet;
        public Safemod()
        {
            InitializeComponent();
        }

        private void Safemod_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Stokmatik Veritabanı Dosyası |*.db";
            file.FilterIndex = 2;
            file.RestoreDirectory = true;
            file.CheckFileExists = false;
            file.Title = "StokMatik.db Adlı Dosyayı Seçin";

            if (file.ShowDialog() == DialogResult.OK)
            {
                string DosyaYolu = file.FileName;
                string DosyaAdi = file.SafeFileName;
                if (DosyaAdi== "StokMatik.db")
                {
                    MessageBox.Show("StokMatik.db Veritabanını Belirlenen Dosyası : " + DosyaYolu + " Olarak Seçildi!.");
                    StokMatikyolu = DosyaYolu;
                    label5.Visible = true;
                }
                else
                {
                    MessageBox.Show("DOSYA İSMİ UYUŞMADI BİR DAHA DENEYİN!");
                }
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Stokmatik Veritabanı Dosyası |*.db";
            file.FilterIndex = 2;
            file.RestoreDirectory = true;
            file.CheckFileExists = false;
            file.Title = "StokmatikHammadde.db Adlı Dosyayı Seçin";

            if (file.ShowDialog() == DialogResult.OK)
            {
                string DosyaYolu = file.FileName;
                string DosyaAdi = file.SafeFileName;
                if (DosyaAdi == "StokmatikHammadde.db")
                {
                    MessageBox.Show("StokmatikHammadde.db Veritabanını Belirlenen Dosyası : " + DosyaYolu + " Olarak Seçildi!.");
                    StokmatikHammadde = DosyaYolu;
                    label6.Visible = true;
                }
                else
                {
                    MessageBox.Show("DOSYA İSMİ UYUŞMADI BİR DAHA DENEYİN!");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Stokmatik Veritabanı Dosyası |*.db";
            file.FilterIndex = 2;
            file.RestoreDirectory = true;
            file.CheckFileExists = false;
            file.Title = "StokmatikSepet.db Adlı Dosyayı Seçin";

            if (file.ShowDialog() == DialogResult.OK)
            {
                string DosyaYolu = file.FileName;
                string DosyaAdi = file.SafeFileName;
                if (DosyaAdi == "StokmatikSepet.db")
                {
                    MessageBox.Show("StokmatikSepet.db Veritabanını Belirlenen Dosyası : " + DosyaYolu + " Olarak Seçildi!.");
                    StokmatikSepet = DosyaYolu;
                    label7.Visible = true;
                }
                else
                {
                    MessageBox.Show("DOSYA İSMİ UYUŞMADI BİR DAHA DENEYİN!");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (StokMatikyolu== "" && StokmatikHammadde== "" && StokmatikSepet=="")
            {
                MessageBox.Show("Tüm Veritabanlarını doğru seçiniz");
            }
            else
            {
                Directory.CreateDirectory(@"C:\MRCStok\Data");
                File.Copy(StokMatikyolu, @"C:\MRCStok\Data\StokMatik.db");
                File.Copy(StokmatikHammadde, @"C:\MRCStok\Data\StokmatikHammadde.db");
                File.Copy(StokmatikSepet, @"C:\MRCStok\Data\StokmatikSepet.db");
                MessageBox.Show("VERİTABANI KOPYALANDI PROGRAM KULLANIMA HAZIR!");
                this.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult Uyari = new DialogResult();
            Uyari = MessageBox.Show("VERİTABANI KURTARILACAK! TÜM VERİLERİNİZ KAYBOLACAKTIR. DEVAM EDİLSİNMİ? ", "DİKKAT!", MessageBoxButtons.YesNo);
            if (Uyari == DialogResult.Yes)
            {
                Directory.CreateDirectory(@"C:\MRCStok\Data");
                File.Copy(@"C:\MRCStok\backup\StokMatik.db", @"C:\MRCStok\Data\StokMatik.db");
                File.Copy(@"C:\MRCStok\backup\StokmatikHammadde.db", @"C:\MRCStok\Data\StokmatikHammadde.db");
                File.Copy(@"C:\MRCStok\backup\StokmatikSepet.db", @"C:\MRCStok\Data\StokmatikSepet.db");
                MessageBox.Show("VERİTABANI KURTARILDI PROGRAM KULLANIMA HAZIR!");
                this.Close();
            }
            }
    }
}
