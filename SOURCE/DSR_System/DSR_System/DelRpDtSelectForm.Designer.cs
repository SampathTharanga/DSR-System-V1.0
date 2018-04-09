namespace DSR_System
{
    partial class DelRpDtSelectForm
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
            this.dtpFrom = new Bunifu.Framework.UI.BunifuDatepicker();
            this.dtpTo = new Bunifu.Framework.UI.BunifuDatepicker();
            this.btnSelect = new Bunifu.Framework.UI.BunifuFlatButton();
            this.SuspendLayout();
            // 
            // dtpFrom
            // 
            this.dtpFrom.BackColor = System.Drawing.Color.SeaGreen;
            this.dtpFrom.BorderRadius = 0;
            this.dtpFrom.ForeColor = System.Drawing.Color.White;
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpFrom.FormatCustom = null;
            this.dtpFrom.Location = new System.Drawing.Point(17, 16);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(303, 36);
            this.dtpFrom.TabIndex = 0;
            this.dtpFrom.Value = new System.DateTime(2018, 4, 7, 19, 4, 22, 134);
            // 
            // dtpTo
            // 
            this.dtpTo.BackColor = System.Drawing.Color.SeaGreen;
            this.dtpTo.BorderRadius = 0;
            this.dtpTo.ForeColor = System.Drawing.Color.White;
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpTo.FormatCustom = null;
            this.dtpTo.Location = new System.Drawing.Point(17, 75);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(303, 36);
            this.dtpTo.TabIndex = 1;
            this.dtpTo.Value = new System.DateTime(2018, 4, 7, 19, 4, 22, 134);
            // 
            // btnSelect
            // 
            this.btnSelect.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelect.BorderRadius = 0;
            this.btnSelect.ButtonText = "OK";
            this.btnSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelect.DisabledColor = System.Drawing.Color.Gray;
            this.btnSelect.Iconcolor = System.Drawing.Color.Transparent;
            this.btnSelect.Iconimage = null;
            this.btnSelect.Iconimage_right = null;
            this.btnSelect.Iconimage_right_Selected = null;
            this.btnSelect.Iconimage_Selected = null;
            this.btnSelect.IconMarginLeft = 0;
            this.btnSelect.IconMarginRight = 0;
            this.btnSelect.IconRightVisible = true;
            this.btnSelect.IconRightZoom = 0D;
            this.btnSelect.IconVisible = true;
            this.btnSelect.IconZoom = 90D;
            this.btnSelect.IsTab = false;
            this.btnSelect.Location = new System.Drawing.Point(353, 36);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnSelect.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btnSelect.OnHoverTextColor = System.Drawing.Color.White;
            this.btnSelect.selected = false;
            this.btnSelect.Size = new System.Drawing.Size(78, 47);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = "OK";
            this.btnSelect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSelect.Textcolor = System.Drawing.Color.White;
            this.btnSelect.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // DelRpDtSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(44)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(460, 125);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DelRpDtSelectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DeliveryReportForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuDatepicker dtpFrom;
        private Bunifu.Framework.UI.BunifuDatepicker dtpTo;
        private Bunifu.Framework.UI.BunifuFlatButton btnSelect;
    }
}