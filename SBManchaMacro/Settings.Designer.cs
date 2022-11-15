namespace SBManchaMacro
{
    partial class Settings
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
            this.txtMacroName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEtcBuff = new System.Windows.Forms.Button();
            this.Buff03 = new System.Windows.Forms.Button();
            this.Buff02 = new System.Windows.Forms.Button();
            this.Buff01 = new System.Windows.Forms.Button();
            this.btnKeySettings = new System.Windows.Forms.Button();
            this.Atack01 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMacroName
            // 
            this.txtMacroName.Location = new System.Drawing.Point(49, 8);
            this.txtMacroName.Name = "txtMacroName";
            this.txtMacroName.ReadOnly = true;
            this.txtMacroName.Size = new System.Drawing.Size(215, 19);
            this.txtMacroName.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "名称：";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(37, 346);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(95, 41);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(138, 346);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 41);
            this.button2.TabIndex = 10;
            this.button2.Text = "Cencel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "▽";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnEtcBuff);
            this.groupBox1.Controls.Add(this.Buff03);
            this.groupBox1.Controls.Add(this.Buff02);
            this.groupBox1.Controls.Add(this.Buff01);
            this.groupBox1.Location = new System.Drawing.Point(40, 142);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 198);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "定時サイクル";
            // 
            // btnEtcBuff
            // 
            this.btnEtcBuff.Image = global::SBManchaMacro.Properties.Resources.wing_of_seraphim;
            this.btnEtcBuff.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEtcBuff.Location = new System.Drawing.Point(9, 105);
            this.btnEtcBuff.Name = "btnEtcBuff";
            this.btnEtcBuff.Size = new System.Drawing.Size(174, 39);
            this.btnEtcBuff.TabIndex = 2;
            this.btnEtcBuff.Text = "その他 Buff/DoT";
            this.btnEtcBuff.UseVisualStyleBackColor = true;
            this.btnEtcBuff.Click += new System.EventHandler(this.btnEtcBuff_Click);
            // 
            // Buff03
            // 
            this.Buff03.Image = global::SBManchaMacro.Properties.Resources.amazons_endurance;
            this.Buff03.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Buff03.Location = new System.Drawing.Point(9, 60);
            this.Buff03.Name = "Buff03";
            this.Buff03.Size = new System.Drawing.Size(174, 39);
            this.Buff03.TabIndex = 1;
            this.Buff03.Text = "自己Buff設定2";
            this.Buff03.UseVisualStyleBackColor = true;
            this.Buff03.Click += new System.EventHandler(this.Buff03_Click);
            // 
            // Buff02
            // 
            this.Buff02.Image = global::SBManchaMacro.Properties.Resources.pot2;
            this.Buff02.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Buff02.Location = new System.Drawing.Point(9, 150);
            this.Buff02.Name = "Buff02";
            this.Buff02.Size = new System.Drawing.Size(174, 39);
            this.Buff02.TabIndex = 3;
            this.Buff02.Text = "Pot Buff設定";
            this.Buff02.UseVisualStyleBackColor = true;
            this.Buff02.Click += new System.EventHandler(this.Buff02_Click);
            // 
            // Buff01
            // 
            this.Buff01.Image = global::SBManchaMacro.Properties.Resources.blessing_of_the_grove_;
            this.Buff01.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Buff01.Location = new System.Drawing.Point(9, 15);
            this.Buff01.Name = "Buff01";
            this.Buff01.Size = new System.Drawing.Size(174, 39);
            this.Buff01.TabIndex = 0;
            this.Buff01.Text = "自己Buff設定";
            this.Buff01.UseVisualStyleBackColor = true;
            this.Buff01.Click += new System.EventHandler(this.Buff01_Click);
            // 
            // btnKeySettings
            // 
            this.btnKeySettings.Image = global::SBManchaMacro.Properties.Resources.basicSetting;
            this.btnKeySettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKeySettings.Location = new System.Drawing.Point(49, 33);
            this.btnKeySettings.Name = "btnKeySettings";
            this.btnKeySettings.Size = new System.Drawing.Size(174, 39);
            this.btnKeySettings.TabIndex = 0;
            this.btnKeySettings.Text = "キー設定";
            this.btnKeySettings.UseVisualStyleBackColor = true;
            this.btnKeySettings.Click += new System.EventHandler(this.btnKeySettings_Click);
            // 
            // Atack01
            // 
            this.Atack01.Image = global::SBManchaMacro.Properties.Resources.psychic_shout;
            this.Atack01.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Atack01.Location = new System.Drawing.Point(49, 89);
            this.Atack01.Name = "Atack01";
            this.Atack01.Size = new System.Drawing.Size(174, 39);
            this.Atack01.TabIndex = 1;
            this.Atack01.Text = "攻撃サイクル設定";
            this.Atack01.UseVisualStyleBackColor = true;
            this.Atack01.Click += new System.EventHandler(this.Atack01_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 398);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMacroName);
            this.Controls.Add(this.btnKeySettings);
            this.Controls.Add(this.Atack01);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Text = "マクロ設定";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Atack01;
        private System.Windows.Forms.Button Buff02;
        private System.Windows.Forms.Button Buff01;
        private System.Windows.Forms.Button btnKeySettings;
        private System.Windows.Forms.TextBox txtMacroName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Buff03;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnEtcBuff;
    }
}