namespace MRCStok
{
    partial class Uruneklemetektus
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label47 = new System.Windows.Forms.Label();
            this.UrunSilButonu = new System.Windows.Forms.Button();
            this.UrunBarkoduUrunDuzenle = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.UrunduzenleButonu = new System.Windows.Forms.Button();
            this.AmbalajUrunDuzenle = new System.Windows.Forms.ComboBox();
            this.UrunGramajıUrunDuzenle = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.UrunFiyatiUrunDuzenle = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.UrunAdediUrunDuzenle = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.UrunAdiUrunDuzenle = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.HizliStokUrunAdedi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label47.ForeColor = System.Drawing.SystemColors.Control;
            this.label47.Location = new System.Drawing.Point(302, 153);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(33, 26);
            this.label47.TabIndex = 32;
            this.label47.Text = "gr";
            // 
            // UrunSilButonu
            // 
            this.UrunSilButonu.BackColor = System.Drawing.Color.SaddleBrown;
            this.UrunSilButonu.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.UrunSilButonu.ForeColor = System.Drawing.SystemColors.Control;
            this.UrunSilButonu.Location = new System.Drawing.Point(168, 201);
            this.UrunSilButonu.Name = "UrunSilButonu";
            this.UrunSilButonu.Size = new System.Drawing.Size(195, 62);
            this.UrunSilButonu.TabIndex = 31;
            this.UrunSilButonu.Text = "ÜRÜNÜ SİL";
            this.UrunSilButonu.UseVisualStyleBackColor = false;
            this.UrunSilButonu.Click += new System.EventHandler(this.UrunSilButonu_Click);
            // 
            // UrunBarkoduUrunDuzenle
            // 
            this.UrunBarkoduUrunDuzenle.BackColor = System.Drawing.Color.OrangeRed;
            this.UrunBarkoduUrunDuzenle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.UrunBarkoduUrunDuzenle.ForeColor = System.Drawing.SystemColors.Window;
            this.UrunBarkoduUrunDuzenle.Location = new System.Drawing.Point(185, 32);
            this.UrunBarkoduUrunDuzenle.Name = "UrunBarkoduUrunDuzenle";
            this.UrunBarkoduUrunDuzenle.ReadOnly = true;
            this.UrunBarkoduUrunDuzenle.Size = new System.Drawing.Size(277, 32);
            this.UrunBarkoduUrunDuzenle.TabIndex = 30;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label16.ForeColor = System.Drawing.SystemColors.Control;
            this.label16.Location = new System.Drawing.Point(14, 38);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(165, 26);
            this.label16.TabIndex = 29;
            this.label16.Text = "Ürün Barkodu:";
            // 
            // UrunduzenleButonu
            // 
            this.UrunduzenleButonu.BackColor = System.Drawing.Color.DarkGreen;
            this.UrunduzenleButonu.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.UrunduzenleButonu.ForeColor = System.Drawing.SystemColors.Control;
            this.UrunduzenleButonu.Location = new System.Drawing.Point(369, 201);
            this.UrunduzenleButonu.Name = "UrunduzenleButonu";
            this.UrunduzenleButonu.Size = new System.Drawing.Size(184, 62);
            this.UrunduzenleButonu.TabIndex = 28;
            this.UrunduzenleButonu.Text = "KAYDET";
            this.UrunduzenleButonu.UseVisualStyleBackColor = false;
            this.UrunduzenleButonu.Click += new System.EventHandler(this.UrunduzenleButonu_Click);
            // 
            // AmbalajUrunDuzenle
            // 
            this.AmbalajUrunDuzenle.BackColor = System.Drawing.Color.OrangeRed;
            this.AmbalajUrunDuzenle.Enabled = false;
            this.AmbalajUrunDuzenle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.AmbalajUrunDuzenle.ForeColor = System.Drawing.SystemColors.Window;
            this.AmbalajUrunDuzenle.FormattingEnabled = true;
            this.AmbalajUrunDuzenle.Items.AddRange(new object[] {
            "TENEKE KUTU",
            "ÖZEL AMBALAJ",
            "OPP POŞET",
            "CAM KAVANOZ",
            "CAM ŞİŞE",
            "POLIETILEN ŞİŞE ",
            "3 KG\'LIK KUTU"});
            this.AmbalajUrunDuzenle.Location = new System.Drawing.Point(341, 146);
            this.AmbalajUrunDuzenle.Name = "AmbalajUrunDuzenle";
            this.AmbalajUrunDuzenle.Size = new System.Drawing.Size(212, 33);
            this.AmbalajUrunDuzenle.TabIndex = 27;
            this.AmbalajUrunDuzenle.Text = "TENEKE KUTU";
            // 
            // UrunGramajıUrunDuzenle
            // 
            this.UrunGramajıUrunDuzenle.BackColor = System.Drawing.Color.OrangeRed;
            this.UrunGramajıUrunDuzenle.Enabled = false;
            this.UrunGramajıUrunDuzenle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.UrunGramajıUrunDuzenle.ForeColor = System.Drawing.SystemColors.Window;
            this.UrunGramajıUrunDuzenle.FormattingEnabled = true;
            this.UrunGramajıUrunDuzenle.Items.AddRange(new object[] {
            "0,100",
            "0,125",
            "0,250",
            "0,350",
            "0,500",
            "0,600",
            "0,700",
            "0,750",
            "1000",
            "1100",
            "2500",
            "5000",
            "10000",
            "25000",
            "30000",
            "40000",
            "50000"});
            this.UrunGramajıUrunDuzenle.Location = new System.Drawing.Point(185, 146);
            this.UrunGramajıUrunDuzenle.Name = "UrunGramajıUrunDuzenle";
            this.UrunGramajıUrunDuzenle.Size = new System.Drawing.Size(109, 33);
            this.UrunGramajıUrunDuzenle.TabIndex = 25;
            this.UrunGramajıUrunDuzenle.Text = "0,100";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label19.ForeColor = System.Drawing.SystemColors.Control;
            this.label19.Location = new System.Drawing.Point(14, 153);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(103, 26);
            this.label19.TabIndex = 24;
            this.label19.Text = "Gramajı:";
            // 
            // UrunFiyatiUrunDuzenle
            // 
            this.UrunFiyatiUrunDuzenle.BackColor = System.Drawing.Color.OrangeRed;
            this.UrunFiyatiUrunDuzenle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.UrunFiyatiUrunDuzenle.ForeColor = System.Drawing.SystemColors.Window;
            this.UrunFiyatiUrunDuzenle.Location = new System.Drawing.Point(443, 108);
            this.UrunFiyatiUrunDuzenle.Name = "UrunFiyatiUrunDuzenle";
            this.UrunFiyatiUrunDuzenle.ReadOnly = true;
            this.UrunFiyatiUrunDuzenle.Size = new System.Drawing.Size(110, 32);
            this.UrunFiyatiUrunDuzenle.TabIndex = 23;
            this.UrunFiyatiUrunDuzenle.Text = "0";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label20.ForeColor = System.Drawing.SystemColors.Control;
            this.label20.Location = new System.Drawing.Point(302, 114);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(135, 26);
            this.label20.TabIndex = 22;
            this.label20.Text = "Ürün Fiyatı:";
            // 
            // UrunAdediUrunDuzenle
            // 
            this.UrunAdediUrunDuzenle.BackColor = System.Drawing.Color.OrangeRed;
            this.UrunAdediUrunDuzenle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.UrunAdediUrunDuzenle.ForeColor = System.Drawing.SystemColors.Window;
            this.UrunAdediUrunDuzenle.Location = new System.Drawing.Point(185, 108);
            this.UrunAdediUrunDuzenle.Name = "UrunAdediUrunDuzenle";
            this.UrunAdediUrunDuzenle.ReadOnly = true;
            this.UrunAdediUrunDuzenle.Size = new System.Drawing.Size(109, 32);
            this.UrunAdediUrunDuzenle.TabIndex = 21;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label21.ForeColor = System.Drawing.SystemColors.Control;
            this.label21.Location = new System.Drawing.Point(14, 114);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(138, 26);
            this.label21.TabIndex = 20;
            this.label21.Text = "Ürün Adedi:";
            // 
            // UrunAdiUrunDuzenle
            // 
            this.UrunAdiUrunDuzenle.BackColor = System.Drawing.Color.OrangeRed;
            this.UrunAdiUrunDuzenle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.UrunAdiUrunDuzenle.ForeColor = System.Drawing.SystemColors.Window;
            this.UrunAdiUrunDuzenle.Location = new System.Drawing.Point(185, 70);
            this.UrunAdiUrunDuzenle.Name = "UrunAdiUrunDuzenle";
            this.UrunAdiUrunDuzenle.ReadOnly = true;
            this.UrunAdiUrunDuzenle.Size = new System.Drawing.Size(368, 32);
            this.UrunAdiUrunDuzenle.TabIndex = 19;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label22.ForeColor = System.Drawing.SystemColors.Control;
            this.label22.Location = new System.Drawing.Point(14, 76);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(112, 26);
            this.label22.TabIndex = 18;
            this.label22.Text = "Ürün Adı:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(559, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 26);
            this.label1.TabIndex = 33;
            this.label1.Text = "₺";
            // 
            // HizliStokUrunAdedi
            // 
            this.HizliStokUrunAdedi.BackColor = System.Drawing.Color.DarkGreen;
            this.HizliStokUrunAdedi.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.HizliStokUrunAdedi.ForeColor = System.Drawing.SystemColors.Window;
            this.HizliStokUrunAdedi.Location = new System.Drawing.Point(323, 269);
            this.HizliStokUrunAdedi.Name = "HizliStokUrunAdedi";
            this.HizliStokUrunAdedi.Size = new System.Drawing.Size(230, 44);
            this.HizliStokUrunAdedi.TabIndex = 34;
            this.HizliStokUrunAdedi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HizliStokUrunAdedi_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(12, 272);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(305, 37);
            this.label2.TabIndex = 35;
            this.label2.Text = "HIZLI STOK EKLE:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MidnightBlue;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(19, 201);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 62);
            this.button1.TabIndex = 36;
            this.button1.Text = "ÜRÜNÜ DÜZENLE";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button2.Location = new System.Drawing.Point(536, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(51, 43);
            this.button2.TabIndex = 37;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Uruneklemetektus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkRed;
            this.ClientSize = new System.Drawing.Size(590, 325);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.HizliStokUrunAdedi);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label47);
            this.Controls.Add(this.UrunSilButonu);
            this.Controls.Add(this.UrunBarkoduUrunDuzenle);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.UrunduzenleButonu);
            this.Controls.Add(this.AmbalajUrunDuzenle);
            this.Controls.Add(this.UrunGramajıUrunDuzenle);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.UrunFiyatiUrunDuzenle);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.UrunAdediUrunDuzenle);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.UrunAdiUrunDuzenle);
            this.Controls.Add(this.label22);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Uruneklemetektus";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Uruneklemetektus";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Uruneklemetektus_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Button UrunSilButonu;
        private System.Windows.Forms.TextBox UrunBarkoduUrunDuzenle;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button UrunduzenleButonu;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox HizliStokUrunAdedi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.ComboBox AmbalajUrunDuzenle;
        public System.Windows.Forms.ComboBox UrunGramajıUrunDuzenle;
        public System.Windows.Forms.TextBox UrunFiyatiUrunDuzenle;
        public System.Windows.Forms.TextBox UrunAdediUrunDuzenle;
        public System.Windows.Forms.TextBox UrunAdiUrunDuzenle;
        private System.Windows.Forms.Button button2;
    }
}