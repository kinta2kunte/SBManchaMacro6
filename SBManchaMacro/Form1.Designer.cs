namespace SBManchaMacro
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cmbMacroName = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnBaseSttings = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.マクロ停止ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnInport = new System.Windows.Forms.Button();
            this.SBMacroStart = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbMacroName
            // 
            this.cmbMacroName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMacroName.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbMacroName.FormattingEnabled = true;
            this.cmbMacroName.Location = new System.Drawing.Point(12, 10);
            this.cmbMacroName.Name = "cmbMacroName";
            this.cmbMacroName.Size = new System.Drawing.Size(180, 23);
            this.cmbMacroName.TabIndex = 0;
            this.cmbMacroName.SelectedIndexChanged += new System.EventHandler(this.cmbMacroName_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(201, 35);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "新規追加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(201, 9);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "編集";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(282, 35);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "削除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnBaseSttings
            // 
            this.btnBaseSttings.Location = new System.Drawing.Point(282, 61);
            this.btnBaseSttings.Name = "btnBaseSttings";
            this.btnBaseSttings.Size = new System.Drawing.Size(75, 23);
            this.btnBaseSttings.TabIndex = 7;
            this.btnBaseSttings.Text = "オプション";
            this.btnBaseSttings.UseVisualStyleBackColor = true;
            this.btnBaseSttings.Click += new System.EventHandler(this.btnBaseSttings_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(282, 9);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 5;
            this.btnCopy.Text = "複製";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "SBManchaMacro";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.表示ToolStripMenuItem,
            this.マクロ停止ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.終了ToolStripMenuItem,
            this.toolStripMenuItem2});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(126, 82);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // 表示ToolStripMenuItem
            // 
            this.表示ToolStripMenuItem.Name = "表示ToolStripMenuItem";
            this.表示ToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.表示ToolStripMenuItem.Text = "表示";
            this.表示ToolStripMenuItem.ToolTipText = "ダイアログ表示に戻す";
            this.表示ToolStripMenuItem.Click += new System.EventHandler(this.表示ToolStripMenuItem_Click);
            // 
            // マクロ停止ToolStripMenuItem
            // 
            this.マクロ停止ToolStripMenuItem.Name = "マクロ停止ToolStripMenuItem";
            this.マクロ停止ToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.マクロ停止ToolStripMenuItem.Text = "マクロ停止";
            this.マクロ停止ToolStripMenuItem.ToolTipText = "実行中マクロ停止";
            this.マクロ停止ToolStripMenuItem.Click += new System.EventHandler(this.マクロ停止ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(122, 6);
            // 
            // 終了ToolStripMenuItem
            // 
            this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
            this.終了ToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.終了ToolStripMenuItem.Text = "終了";
            this.終了ToolStripMenuItem.ToolTipText = "プログラム終了";
            this.終了ToolStripMenuItem.Click += new System.EventHandler(this.終了ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(122, 6);
            // 
            // btnInport
            // 
            this.btnInport.Location = new System.Drawing.Point(201, 61);
            this.btnInport.Name = "btnInport";
            this.btnInport.Size = new System.Drawing.Size(75, 23);
            this.btnInport.TabIndex = 4;
            this.btnInport.Text = "インポート";
            this.btnInport.UseVisualStyleBackColor = true;
            this.btnInport.Click += new System.EventHandler(this.btnInport_Click);
            // 
            // SBMacroStart
            // 
            this.SBMacroStart.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SBMacroStart.Image = global::SBManchaMacro.Properties.Resources.start;
            this.SBMacroStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SBMacroStart.Location = new System.Drawing.Point(12, 39);
            this.SBMacroStart.Name = "SBMacroStart";
            this.SBMacroStart.Size = new System.Drawing.Size(180, 45);
            this.SBMacroStart.TabIndex = 1;
            this.SBMacroStart.Text = "マクロ開始";
            this.SBMacroStart.UseVisualStyleBackColor = true;
            this.SBMacroStart.Click += new System.EventHandler(this.SBMacroStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(362, 89);
            this.Controls.Add(this.btnInport);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnBaseSttings);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cmbMacroName);
            this.Controls.Add(this.SBMacroStart);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::SBManchaMacro.Properties.Settings.Default, "MyLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = global::SBManchaMacro.Properties.Settings.Default.MyLocation;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "SBManchaMacro v0.972";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ClientSizeChanged += new System.EventHandler(this.Form1_ClientSizeChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SBMacroStart;
        private System.Windows.Forms.ComboBox cmbMacroName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnBaseSttings;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 表示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 終了ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem マクロ停止ToolStripMenuItem;
        private System.Windows.Forms.Button btnInport;
    }
}

