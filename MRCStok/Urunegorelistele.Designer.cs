namespace MRCStok
{
    partial class Urunegorelistele
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
            this.components = new System.ComponentModel.Container();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.urunlerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.stokMatikDataSet = new MRCStok.StokMatikDataSet();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUrunAdi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUrunFiyati = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUrunGramaji = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUrunPaketi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.urunlerTableAdapter = new MRCStok.StokMatikDataSetTableAdapters.UrunlerTableAdapter();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.urunlerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stokMatikDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.urunlerBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(731, 443);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // urunlerBindingSource
            // 
            this.urunlerBindingSource.DataMember = "Urunler";
            this.urunlerBindingSource.DataSource = this.stokMatikDataSet;
            // 
            // stokMatikDataSet
            // 
            this.stokMatikDataSet.DataSetName = "StokMatikDataSet";
            this.stokMatikDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUrunAdi,
            this.colUrunFiyati,
            this.colUrunGramaji,
            this.colUrunPaketi});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupPanelText = "ÜRÜN SEÇİN";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // colUrunAdi
            // 
            this.colUrunAdi.FieldName = "UrunAdi";
            this.colUrunAdi.Name = "colUrunAdi";
            this.colUrunAdi.OptionsColumn.AllowEdit = false;
            this.colUrunAdi.OptionsColumn.ReadOnly = true;
            this.colUrunAdi.Visible = true;
            this.colUrunAdi.VisibleIndex = 0;
            // 
            // colUrunFiyati
            // 
            this.colUrunFiyati.FieldName = "UrunFiyati";
            this.colUrunFiyati.Name = "colUrunFiyati";
            this.colUrunFiyati.OptionsColumn.AllowEdit = false;
            this.colUrunFiyati.OptionsColumn.ReadOnly = true;
            this.colUrunFiyati.Visible = true;
            this.colUrunFiyati.VisibleIndex = 1;
            // 
            // colUrunGramaji
            // 
            this.colUrunGramaji.FieldName = "UrunGramaji";
            this.colUrunGramaji.Name = "colUrunGramaji";
            this.colUrunGramaji.OptionsColumn.AllowEdit = false;
            this.colUrunGramaji.OptionsColumn.ReadOnly = true;
            this.colUrunGramaji.Visible = true;
            this.colUrunGramaji.VisibleIndex = 2;
            // 
            // colUrunPaketi
            // 
            this.colUrunPaketi.FieldName = "UrunPaketi";
            this.colUrunPaketi.Name = "colUrunPaketi";
            this.colUrunPaketi.OptionsColumn.AllowEdit = false;
            this.colUrunPaketi.OptionsColumn.ReadOnly = true;
            this.colUrunPaketi.Visible = true;
            this.colUrunPaketi.VisibleIndex = 3;
            // 
            // urunlerTableAdapter
            // 
            this.urunlerTableAdapter.ClearBeforeFill = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Maroon;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(682, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 38);
            this.button1.TabIndex = 1;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Urunegorelistele
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 443);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gridControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Urunegorelistele";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Urunegorelistele";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Urunegorelistele_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.urunlerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stokMatikDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private StokMatikDataSet stokMatikDataSet;
        private System.Windows.Forms.BindingSource urunlerBindingSource;
        private StokMatikDataSetTableAdapters.UrunlerTableAdapter urunlerTableAdapter;
        private DevExpress.XtraGrid.Columns.GridColumn colUrunAdi;
        private DevExpress.XtraGrid.Columns.GridColumn colUrunFiyati;
        private DevExpress.XtraGrid.Columns.GridColumn colUrunGramaji;
        private DevExpress.XtraGrid.Columns.GridColumn colUrunPaketi;
        private System.Windows.Forms.Button button1;
    }
}