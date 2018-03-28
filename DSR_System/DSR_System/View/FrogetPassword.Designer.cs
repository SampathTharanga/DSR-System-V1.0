namespace DSR_System
{
    partial class FrogetPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrogetPassword));
            this.btnExit = new Bunifu.Framework.UI.BunifuImageButton();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel3 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.txtAnswer = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.bunifuCustomLabel5 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.btnRecover = new Bunifu.Framework.UI.BunifuFlatButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbxSecQue = new Bunifu.Framework.UI.BunifuDropdown();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Red;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageActive = null;
            this.btnExit.Location = new System.Drawing.Point(371, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(68, 22);
            this.btnExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnExit.TabIndex = 22;
            this.btnExit.TabStop = false;
            this.btnExit.Zoom = 10;
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.AutoSize = true;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Papyrus", 23.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(89, 3);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(260, 51);
            this.bunifuCustomLabel1.TabIndex = 37;
            this.bunifuCustomLabel1.Text = "Froget Password";
            this.bunifuCustomLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // bunifuCustomLabel3
            // 
            this.bunifuCustomLabel3.AutoSize = true;
            this.bunifuCustomLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel3.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel3.Location = new System.Drawing.Point(25, 81);
            this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
            this.bunifuCustomLabel3.Size = new System.Drawing.Size(58, 17);
            this.bunifuCustomLabel3.TabIndex = 50;
            this.bunifuCustomLabel3.Text = "Answer:";
            // 
            // txtAnswer
            // 
            this.txtAnswer.BorderColorFocused = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(177)))), ((int)(((byte)(136)))));
            this.txtAnswer.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.txtAnswer.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(177)))), ((int)(((byte)(136)))));
            this.txtAnswer.BorderThickness = 1;
            this.txtAnswer.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAnswer.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.txtAnswer.ForeColor = System.Drawing.Color.White;
            this.txtAnswer.isPassword = false;
            this.txtAnswer.Location = new System.Drawing.Point(25, 107);
            this.txtAnswer.Margin = new System.Windows.Forms.Padding(4);
            this.txtAnswer.Name = "txtAnswer";
            this.txtAnswer.Size = new System.Drawing.Size(340, 29);
            this.txtAnswer.TabIndex = 49;
            this.txtAnswer.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // bunifuCustomLabel5
            // 
            this.bunifuCustomLabel5.AutoSize = true;
            this.bunifuCustomLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel5.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel5.Location = new System.Drawing.Point(25, 16);
            this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
            this.bunifuCustomLabel5.Size = new System.Drawing.Size(124, 17);
            this.bunifuCustomLabel5.TabIndex = 48;
            this.bunifuCustomLabel5.Text = "Security Question:";
            // 
            // btnRecover
            // 
            this.btnRecover.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnRecover.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnRecover.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRecover.BorderRadius = 0;
            this.btnRecover.ButtonText = "Recover";
            this.btnRecover.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRecover.DisabledColor = System.Drawing.Color.Gray;
            this.btnRecover.Iconcolor = System.Drawing.Color.Transparent;
            this.btnRecover.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnRecover.Iconimage")));
            this.btnRecover.Iconimage_right = null;
            this.btnRecover.Iconimage_right_Selected = null;
            this.btnRecover.Iconimage_Selected = null;
            this.btnRecover.IconMarginLeft = 0;
            this.btnRecover.IconMarginRight = 0;
            this.btnRecover.IconRightVisible = true;
            this.btnRecover.IconRightZoom = 0D;
            this.btnRecover.IconVisible = false;
            this.btnRecover.IconZoom = 90D;
            this.btnRecover.IsTab = false;
            this.btnRecover.Location = new System.Drawing.Point(52, 151);
            this.btnRecover.Margin = new System.Windows.Forms.Padding(4);
            this.btnRecover.Name = "btnRecover";
            this.btnRecover.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnRecover.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btnRecover.OnHoverTextColor = System.Drawing.Color.White;
            this.btnRecover.selected = false;
            this.btnRecover.Size = new System.Drawing.Size(284, 44);
            this.btnRecover.TabIndex = 46;
            this.btnRecover.Text = "Recover";
            this.btnRecover.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRecover.Textcolor = System.Drawing.Color.White;
            this.btnRecover.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cbxSecQue);
            this.panel1.Controls.Add(this.bunifuCustomLabel3);
            this.panel1.Controls.Add(this.txtAnswer);
            this.panel1.Controls.Add(this.bunifuCustomLabel5);
            this.panel1.Controls.Add(this.btnRecover);
            this.panel1.Location = new System.Drawing.Point(24, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(391, 214);
            this.panel1.TabIndex = 51;
            // 
            // cbxSecQue
            // 
            this.cbxSecQue.BackColor = System.Drawing.Color.Transparent;
            this.cbxSecQue.BorderRadius = 3;
            this.cbxSecQue.DisabledColor = System.Drawing.Color.Gray;
            this.cbxSecQue.ForeColor = System.Drawing.Color.White;
            this.cbxSecQue.Items = new string[] {
        "What is the first name of the person you first kissed?",
        "What is the name of your favourite teacher ?",
        "What is the colour of your first pet ?",
        "What is the favourte color of your favourite person?",
        "To what country you mostly likely to travel ?",
        "What is the first name of your best friend in school?",
        "What is the middle name of your oldest child?",
        "In what city or town was your first job?"};
            this.cbxSecQue.Location = new System.Drawing.Point(25, 43);
            this.cbxSecQue.Name = "cbxSecQue";
            this.cbxSecQue.NomalColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.cbxSecQue.onHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.cbxSecQue.selectedIndex = -1;
            this.cbxSecQue.Size = new System.Drawing.Size(340, 29);
            this.cbxSecQue.TabIndex = 51;
            // 
            // FrogetPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(44)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(439, 288);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bunifuCustomLabel1);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrogetPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrogetPassword";
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuImageButton btnExit;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel3;
        private Bunifu.Framework.UI.BunifuMetroTextbox txtAnswer;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel5;
        private Bunifu.Framework.UI.BunifuFlatButton btnRecover;
        private System.Windows.Forms.Panel panel1;
        private Bunifu.Framework.UI.BunifuDropdown cbxSecQue;
    }
}