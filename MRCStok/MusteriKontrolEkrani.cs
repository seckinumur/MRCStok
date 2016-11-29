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
    public partial class MusteriKontrolEkrani : Form
    {
        public StokMatikEntities db;
        public Form1 form111 = (Form1)Application.OpenForms["Form1"];
        public GirisEkrani formbaslangic = (GirisEkrani)System.Windows.Forms.Application.OpenForms["GirisEkrani"];
        public MusteriKontrolEkrani()
        {
            InitializeComponent();
            db = new StokMatikEntities();
        }

        private void MusteriKontrolEkrani_Load(object sender, EventArgs e)
        {
            
        }

        private void button22_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MusteriDuzenleMusteriDuzenle_Click(object sender, EventArgs e)
        {
            if(MusteriAdiMusteriDuzenle.Text=="")
            {
                MessageBox.Show("Müşteri İsmi Boş Bırakılamaz!");
            }
            else if (AdresMusteriDuzenle.Text=="")
            {
                MessageBox.Show("Müşteri Adresini Giriniz!");
            }
            else
            {
                try
                {
                    DialogResult Uyari = new DialogResult();
                    Uyari = MessageBox.Show(BurdanAl.Text + " Adlı Müşteri"+MusteriAdiMusteriDuzenle.Text+ " Olarak Değiştirilecek Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                    if (Uyari == DialogResult.Yes)
                    {
                        var bul = db.Musteriler.Where(p => p.MusteriAdi == BurdanAl.Text).FirstOrDefault();
                        bul.MusteriAdi = MusteriAdiMusteriDuzenle.Text;
                        bul.MusteriAdresi = AdresMusteriDuzenle.Text;
                        db.SaveChanges();
                        formbaslangic.yenidenbaslama = true;
                        string kontrolal = form111.AdminKontrol;
                        form111.Close();
                        Form1 frm = new Form1();
                        frm.Show();
                        frm.AdminKontrol = kontrolal;
                        this.Close();
                        MessageBox.Show("Müşteri Başarıyla Güncellendi!");
                    }
                }catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Uyari = new DialogResult();
                Uyari = MessageBox.Show(BurdanAl.Text + " Adlı Müşteri Silinecek Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                if (Uyari == DialogResult.Yes)
                {
                    var bul = db.Musteriler.Where(p => p.MusteriAdi == BurdanAl.Text).FirstOrDefault();
                    db.Musteriler.Remove(bul);
                    db.SaveChanges();
                    formbaslangic.yenidenbaslama = true;
                    string kontrolal = form111.AdminKontrol;
                    form111.Close();
                    Form1 frm = new Form1();
                    frm.Show();
                    frm.AdminKontrol = kontrolal;
                    this.Close();
                    MessageBox.Show("Müşteri Başarıyla Silindi!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
