namespace SBManchaMacro
{
    partial class BasicSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtBaseicWait = new System.Windows.Forms.TextBox();
            this.chkTopMust = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkLog = new System.Windows.Forms.CheckBox();
            this.chkTaskTray = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "コマンド間Wait設定:";
            // 
            // txtBaseicWait
            // 
            this.txtBaseicWait.Location = new System.Drawing.Point(118, 20);
            this.txtBaseicWait.Name = "txtBaseicWait";
            this.txtBaseicWait.Size = new System.Drawing.Size(100, 19);
            this.txtBaseicWait.TabIndex = 1;
            // 
            // chkTopMust
            // 
            this.chkTopMust.AutoSize = true;
            this.chkTopMust.Location = new System.Drawing.Point(12, 82);
            this.chkTopMust.Name = "chkTopMust";
            this.chkTopMust.Size = new System.Drawing.Size(114, 16);
            this.chkTopMust.TabIndex = 2;
            this.chkTopMust.Text = "常に最前面に表示";
            this.chkTopMust.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(168, 228);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(82, 36);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(269, 228);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82, 36);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(312, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "※マナ管理用（マナ不足になる場合、Wait時間を増やして下さい）";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(224, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "秒";
            // 
            // chkLog
            // 
            this.chkLog.AutoSize = true;
            this.chkLog.Location = new System.Drawing.Point(12, 124);
            this.chkLog.Name = "chkLog";
            this.chkLog.Size = new System.Drawing.Size(66, 16);
            this.chkLog.TabIndex = 7;
            this.chkLog.Text = "ログ出力";
            this.chkLog.UseVisualStyleBackColor = true;
            // 
            // chkTaskTray
            // 
            this.chkTaskTray.AutoSize = true;
            this.chkTaskTray.Location = new System.Drawing.Point(12, 102);
            this.chkTaskTray.Name = "chkTaskTray";
            this.chkTaskTray.Size = new System.Drawing.Size(153, 16);
            this.chkTaskTray.TabIndex = 8;
            this.chkTaskTray.Text = "起動時にタスクトレイに格納";
            this.chkTaskTray.UseVisualStyleBackColor = true;
            // 
            // BasicSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 276);
            this.Controls.Add(this.chkTaskTray);
            this.Controls.Add(this.chkLog);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkTopMust);
            this.Controls.Add(this.txtBaseicWait);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BasicSettings";
            this.Text = "基本設定";
            this.Load += new System.EventHandler(this.BasicSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBaseicWait;
        private System.Windows.Forms.CheckBox chkTopMust;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkLog;
        private System.Windows.Forms.CheckBox chkTaskTray;
    }
}