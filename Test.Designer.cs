namespace VanClinic.Utilities.ConfluenceProfilePhotos
{
    partial class Test
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
            this.lblFile = new System.Windows.Forms.Label();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnFile = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ibPicture = new Emgu.CV.UI.ImageBox();
            this.lblRed = new System.Windows.Forms.Label();
            this.lblBlue = new System.Windows.Forms.Label();
            this.tblLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlMid = new System.Windows.Forms.Panel();
            this.ibCropOriginal = new Emgu.CV.UI.ImageBox();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.ibCropPreCrop = new Emgu.CV.UI.ImageBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSavePreCropped = new System.Windows.Forms.Button();
            this.pnlTopMid = new System.Windows.Forms.Panel();
            this.pnlTopRight = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.ibPicture)).BeginInit();
            this.tblLayout.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlMid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibCropOriginal)).BeginInit();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibCropPreCrop)).BeginInit();
            this.pnlTopMid.SuspendLayout();
            this.pnlTopRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(12, 9);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(124, 13);
            this.lblFile.TabIndex = 0;
            this.lblFile.Text = "Choose an image to test:";
            // 
            // txtFile
            // 
            this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFile.Location = new System.Drawing.Point(12, 25);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(573, 20);
            this.txtFile.TabIndex = 1;
            // 
            // btnFile
            // 
            this.btnFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFile.Location = new System.Drawing.Point(591, 23);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(75, 23);
            this.btnFile.TabIndex = 2;
            this.btnFile.Text = "Browse";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // ibPicture
            // 
            this.ibPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ibPicture.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.ibPicture.Location = new System.Drawing.Point(3, 20);
            this.ibPicture.Name = "ibPicture";
            this.ibPicture.Size = new System.Drawing.Size(202, 229);
            this.ibPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ibPicture.TabIndex = 2;
            this.ibPicture.TabStop = false;
            // 
            // lblRed
            // 
            this.lblRed.AutoSize = true;
            this.lblRed.ForeColor = System.Drawing.Color.Red;
            this.lblRed.Location = new System.Drawing.Point(9, 58);
            this.lblRed.Name = "lblRed";
            this.lblRed.Size = new System.Drawing.Size(136, 13);
            this.lblRed.TabIndex = 3;
            this.lblRed.Text = "Red square: Detected face";
            // 
            // lblBlue
            // 
            this.lblBlue.AutoSize = true;
            this.lblBlue.ForeColor = System.Drawing.Color.Blue;
            this.lblBlue.Location = new System.Drawing.Point(151, 58);
            this.lblBlue.Name = "lblBlue";
            this.lblBlue.Size = new System.Drawing.Size(141, 13);
            this.lblBlue.TabIndex = 4;
            this.lblBlue.Text = "Blue square: Crop as original";
            // 
            // tblLayout
            // 
            this.tblLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblLayout.ColumnCount = 3;
            this.tblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tblLayout.Controls.Add(this.pnlLeft, 0, 1);
            this.tblLayout.Controls.Add(this.pnlMid, 1, 1);
            this.tblLayout.Controls.Add(this.pnlRight, 2, 1);
            this.tblLayout.Controls.Add(this.pnlTopMid, 1, 0);
            this.tblLayout.Controls.Add(this.pnlTopRight, 2, 0);
            this.tblLayout.Location = new System.Drawing.Point(12, 83);
            this.tblLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tblLayout.Name = "tblLayout";
            this.tblLayout.RowCount = 2;
            this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblLayout.Size = new System.Drawing.Size(651, 300);
            this.tblLayout.TabIndex = 5;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLeft.Controls.Add(this.label1);
            this.pnlLeft.Controls.Add(this.ibPicture);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(3, 43);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(210, 254);
            this.pnlLeft.TabIndex = 0;
            // 
            // pnlMid
            // 
            this.pnlMid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMid.Controls.Add(this.label2);
            this.pnlMid.Controls.Add(this.ibCropOriginal);
            this.pnlMid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMid.Location = new System.Drawing.Point(219, 43);
            this.pnlMid.Name = "pnlMid";
            this.pnlMid.Size = new System.Drawing.Size(211, 254);
            this.pnlMid.TabIndex = 1;
            // 
            // ibCropOriginal
            // 
            this.ibCropOriginal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ibCropOriginal.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.ibCropOriginal.Location = new System.Drawing.Point(3, 20);
            this.ibCropOriginal.Name = "ibCropOriginal";
            this.ibCropOriginal.Size = new System.Drawing.Size(203, 229);
            this.ibCropOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ibCropOriginal.TabIndex = 2;
            this.ibCropOriginal.TabStop = false;
            // 
            // pnlRight
            // 
            this.pnlRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRight.Controls.Add(this.label3);
            this.pnlRight.Controls.Add(this.ibCropPreCrop);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(436, 43);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(212, 254);
            this.pnlRight.TabIndex = 2;
            // 
            // ibCropPreCrop
            // 
            this.ibCropPreCrop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ibCropPreCrop.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.ibCropPreCrop.Location = new System.Drawing.Point(3, 20);
            this.ibCropPreCrop.Name = "ibCropPreCrop";
            this.ibCropPreCrop.Size = new System.Drawing.Size(204, 229);
            this.ibCropPreCrop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ibCropPreCrop.TabIndex = 2;
            this.ibCropPreCrop.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(50, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Original with crop boxes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Crop as original";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Crop as pre-cropped";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Green;
            this.label4.Location = new System.Drawing.Point(298, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(173, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Green square: Crop as pre-cropped";
            // 
            // btnSavePreCropped
            // 
            this.btnSavePreCropped.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSavePreCropped.Location = new System.Drawing.Point(51, 6);
            this.btnSavePreCropped.Name = "btnSavePreCropped";
            this.btnSavePreCropped.Size = new System.Drawing.Size(111, 23);
            this.btnSavePreCropped.TabIndex = 8;
            this.btnSavePreCropped.Text = "Save";
            this.btnSavePreCropped.UseVisualStyleBackColor = true;
            this.btnSavePreCropped.Click += new System.EventHandler(this.btnSavePreCropped_Click);
            // 
            // pnlTopMid
            // 
            this.pnlTopMid.Controls.Add(this.btnSave);
            this.pnlTopMid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTopMid.Location = new System.Drawing.Point(219, 3);
            this.pnlTopMid.Name = "pnlTopMid";
            this.pnlTopMid.Size = new System.Drawing.Size(211, 34);
            this.pnlTopMid.TabIndex = 3;
            // 
            // pnlTopRight
            // 
            this.pnlTopRight.Controls.Add(this.btnSavePreCropped);
            this.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTopRight.Location = new System.Drawing.Point(436, 3);
            this.pnlTopRight.Name = "pnlTopRight";
            this.pnlTopRight.Size = new System.Drawing.Size(212, 34);
            this.pnlTopRight.TabIndex = 4;
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 392);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tblLayout);
            this.Controls.Add(this.lblBlue);
            this.Controls.Add(this.lblRed);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.lblFile);
            this.Name = "Test";
            this.Text = "Test";
            ((System.ComponentModel.ISupportInitialize)(this.ibPicture)).EndInit();
            this.tblLayout.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.pnlMid.ResumeLayout(false);
            this.pnlMid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibCropOriginal)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibCropPreCrop)).EndInit();
            this.pnlTopMid.ResumeLayout(false);
            this.pnlTopRight.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private Emgu.CV.UI.ImageBox ibPicture;
        private System.Windows.Forms.Label lblRed;
        private System.Windows.Forms.Label lblBlue;
        private System.Windows.Forms.TableLayoutPanel tblLayout;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlMid;
        private Emgu.CV.UI.ImageBox ibCropOriginal;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Panel pnlRight;
        private Emgu.CV.UI.ImageBox ibCropPreCrop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlTopMid;
        private System.Windows.Forms.Panel pnlTopRight;
        private System.Windows.Forms.Button btnSavePreCropped;
    }
}