namespace VanClinic.Utilities.ConfluenceProfilePhotos
{
    partial class Manual
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tslblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlObjects = new System.Windows.Forms.Panel();
            this.pnlPict = new System.Windows.Forms.Panel();
            this.pbPerson = new System.Windows.Forms.PictureBox();
            this.lblNoFace = new System.Windows.Forms.Label();
            this.lvNoFace = new System.Windows.Forms.ListView();
            this.colFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.trkSize = new System.Windows.Forms.TrackBar();
            this.btnUpload = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            this.pnlObjects.SuspendLayout();
            this.pnlPict.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPerson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSize)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 319);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(544, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 5;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tslblStatus
            // 
            this.tslblStatus.Name = "tslblStatus";
            this.tslblStatus.Size = new System.Drawing.Size(10, 17);
            this.tslblStatus.Text = " ";
            // 
            // pnlObjects
            // 
            this.pnlObjects.Controls.Add(this.pnlPict);
            this.pnlObjects.Controls.Add(this.lblNoFace);
            this.pnlObjects.Controls.Add(this.lvNoFace);
            this.pnlObjects.Controls.Add(this.trkSize);
            this.pnlObjects.Controls.Add(this.btnUpload);
            this.pnlObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlObjects.Location = new System.Drawing.Point(0, 0);
            this.pnlObjects.Name = "pnlObjects";
            this.pnlObjects.Size = new System.Drawing.Size(544, 319);
            this.pnlObjects.TabIndex = 6;
            // 
            // pnlPict
            // 
            this.pnlPict.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPict.Controls.Add(this.pbPerson);
            this.pnlPict.Location = new System.Drawing.Point(89, 128);
            this.pnlPict.Name = "pnlPict";
            this.pnlPict.Size = new System.Drawing.Size(256, 256);
            this.pnlPict.TabIndex = 13;
            // 
            // pbPerson
            // 
            this.pbPerson.Location = new System.Drawing.Point(0, 0);
            this.pbPerson.Name = "pbPerson";
            this.pbPerson.Size = new System.Drawing.Size(256, 256);
            this.pbPerson.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPerson.TabIndex = 15;
            this.pbPerson.TabStop = false;
            this.pbPerson.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbEmployee_MouseDown);
            this.pbPerson.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbEmployee_MouseMove);
            // 
            // lblNoFace
            // 
            this.lblNoFace.AutoSize = true;
            this.lblNoFace.Location = new System.Drawing.Point(9, 9);
            this.lblNoFace.Name = "lblNoFace";
            this.lblNoFace.Size = new System.Drawing.Size(164, 13);
            this.lblNoFace.TabIndex = 11;
            this.lblNoFace.Text = "Images that failed face detection:";
            // 
            // lvNoFace
            // 
            this.lvNoFace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvNoFace.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFileName});
            this.lvNoFace.FullRowSelect = true;
            this.lvNoFace.GridLines = true;
            this.lvNoFace.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvNoFace.HideSelection = false;
            this.lvNoFace.Location = new System.Drawing.Point(12, 25);
            this.lvNoFace.MultiSelect = false;
            this.lvNoFace.Name = "lvNoFace";
            this.lvNoFace.Size = new System.Drawing.Size(520, 97);
            this.lvNoFace.TabIndex = 10;
            this.lvNoFace.UseCompatibleStateImageBehavior = false;
            this.lvNoFace.View = System.Windows.Forms.View.Details;
            this.lvNoFace.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvNoFace_ItemSelectionChanged);
            // 
            // colFileName
            // 
            this.colFileName.Text = "File Name";
            this.colFileName.Width = 601;
            // 
            // trkSize
            // 
            this.trkSize.Location = new System.Drawing.Point(12, 231);
            this.trkSize.Maximum = 100;
            this.trkSize.Name = "trkSize";
            this.trkSize.Size = new System.Drawing.Size(250, 45);
            this.trkSize.TabIndex = 9;
            this.trkSize.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkSize.Value = 100;
            this.trkSize.Scroll += new System.EventHandler(this.trkSize_Scroll);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(12, 282);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 8;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // Manual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 341);
            this.Controls.Add(this.pnlObjects);
            this.Controls.Add(this.statusStrip);
            this.MaximumSize = new System.Drawing.Size(560, 380);
            this.MinimumSize = new System.Drawing.Size(560, 380);
            this.Name = "Manual";
            this.Text = "Manual";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Manual_FormClosed);
            this.Load += new System.EventHandler(this.Manual_Load);
            this.Resize += new System.EventHandler(this.Manual_Resize);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.pnlObjects.ResumeLayout(false);
            this.pnlObjects.PerformLayout();
            this.pnlPict.ResumeLayout(false);
            this.pnlPict.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPerson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Panel pnlObjects;
        private System.Windows.Forms.TrackBar trkSize;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Label lblNoFace;
        private System.Windows.Forms.ListView lvNoFace;
        private System.Windows.Forms.ColumnHeader colFileName;
        private System.Windows.Forms.ToolStripStatusLabel tslblStatus;
        private System.Windows.Forms.Panel pnlPict;
        private System.Windows.Forms.PictureBox pbPerson;
    }
}