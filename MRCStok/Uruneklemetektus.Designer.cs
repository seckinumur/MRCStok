﻿namespace MRCStok
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
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
            this.UrunSilButonu.Location = new System.Drawing.Point(590, 70);
            this.UrunSilButonu.Name = "UrunSilButonu";
            this.UrunSilButonu.Size = new System.Drawing.Size(134, 62);
            this.UrunSilButonu.TabIndex = 2;
            this.UrunSilButonu.Text = "ÜRÜNÜ SİL";
            this.UrunSilButonu.UseVisualStyleBackColor = false;
            this.UrunSilButonu.Visible = false;
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
            this.UrunBarkoduUrunDuzenle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UrunBarkoduUrunDuzenle_KeyPress);
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
            this.UrunduzenleButonu.Location = new System.Drawing.Point(590, 134);
            this.UrunduzenleButonu.Name = "UrunduzenleButonu";
            this.UrunduzenleButonu.Size = new System.Drawing.Size(134, 63);
            this.UrunduzenleButonu.TabIndex = 3;
            this.UrunduzenleButonu.Text = "KAYDET";
            this.UrunduzenleButonu.UseVisualStyleBackColor = false;
            this.UrunduzenleButonu.Visible = false;
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
            "OPP POŞET GRİ",
            "OPP POŞET SİYAH",
            "OPP POŞET GOLD",
            "OPP POŞET SARI",
            "OPP POŞET KIRMIZI",
            "KRAFT POŞET",
            "ŞEFFAF POŞET",
            "CAM KAVANOZ",
            "CAM ŞİŞE",
            "POLIETILEN ŞİŞE"});
            this.AmbalajUrunDuzenle.Location = new System.Drawing.Point(341, 146);
            this.AmbalajUrunDuzenle.Name = "AmbalajUrunDuzenle";
            this.AmbalajUrunDuzenle.Size = new System.Drawing.Size(212, 33);
            this.AmbalajUrunDuzenle.TabIndex = 8;
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
            "1500",
            "2000",
            "2500",
            "3000",
            "5000",
            "7500",
            "10000",
            "12000",
            "15000",
            "20000",
            "25000",
            "30000",
            "40000",
            "50000"});
            this.UrunGramajıUrunDuzenle.Location = new System.Drawing.Point(185, 146);
            this.UrunGramajıUrunDuzenle.Name = "UrunGramajıUrunDuzenle";
            this.UrunGramajıUrunDuzenle.Size = new System.Drawing.Size(109, 33);
            this.UrunGramajıUrunDuzenle.TabIndex = 7;
            this.UrunGramajıUrunDuzenle.Text = "0,100";
            this.UrunGramajıUrunDuzenle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UrunGramajıUrunDuzenle_KeyPress);
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
            this.UrunFiyatiUrunDuzenle.TabIndex = 6;
            this.UrunFiyatiUrunDuzenle.Text = "0";
            this.UrunFiyatiUrunDuzenle.TextChanged += new System.EventHandler(this.UrunFiyatiUrunDuzenle_TextChanged);
            this.UrunFiyatiUrunDuzenle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UrunFiyatiUrunDuzenle_KeyPress);
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
            this.UrunAdediUrunDuzenle.TabIndex = 5;
            this.UrunAdediUrunDuzenle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UrunAdediUrunDuzenle_KeyPress);
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
            this.UrunAdiUrunDuzenle.TabIndex = 4;
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
            this.HizliStokUrunAdedi.Location = new System.Drawing.Point(494, 203);
            this.HizliStokUrunAdedi.Name = "HizliStokUrunAdedi";
            this.HizliStokUrunAdedi.Size = new System.Drawing.Size(230, 44);
            this.HizliStokUrunAdedi.TabIndex = 0;
            this.HizliStokUrunAdedi.TextChanged += new System.EventHandler(this.HizliStokUrunAdedi_TextChanged);
            this.HizliStokUrunAdedi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HizliStokUrunAdedi_KeyDown);
            this.HizliStokUrunAdedi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UrunBarkoduUrunDuzenle_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(183, 206);
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
            this.button1.Location = new System.Drawing.Point(472, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(191, 63);
            this.button1.TabIndex = 1;
            this.button1.Text = "ÜRÜNÜ DÜZENLE";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button2.Location = new System.Drawing.Point(679, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(51, 43);
            this.button2.TabIndex = 9;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 20);
            this.label3.TabIndex = 38;
            this.label3.Text = "label3";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(78, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 20);
            this.label4.TabIndex = 39;
            this.label4.Text = "label4";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(141, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 20);
            this.label5.TabIndex = 40;
            this.label5.Text = "label5";
            this.label5.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.LavenderBlush;
            this.label12.Location = new System.Drawing.Point(14, 197);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(113, 20);
            this.label12.TabIndex = 52;
            this.label12.Text = "ADET YAZIP";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.LavenderBlush;
            this.label11.Location = new System.Drawing.Point(14, 224);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(125, 20);
            this.label11.TabIndex = 53;
            this.label11.Text = "ENTER\'E BAS";
            // 
            // Uruneklemetektus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkRed;
            this.ClientSize = new System.Drawing.Size(734, 266);
            this.ControlBox = false;
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
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
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
    }
}