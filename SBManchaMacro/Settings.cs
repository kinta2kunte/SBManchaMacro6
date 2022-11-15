using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SBManchaMacro
{
    public partial class Settings : Form
    {
        public string objName;
        public Data dataObj;
        public bool bNewFlg = false;
        public Settings(string _objName, Data _dataObj)
        {
            InitializeComponent();
            dataObj = _dataObj;
            objName = _objName;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            this.txtMacroName.Text = objName;
           if( bNewFlg == true )
                this.txtMacroName.ReadOnly = false;

        }

        // キー設定
        private void btnKeySettings_Click(object sender, EventArgs e)
        {
            formKeySettings frm;

            frm = new formKeySettings(objName, dataObj);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        // OK ボタン
        private void button1_Click(object sender, EventArgs e)
        {
            objName = txtMacroName.Text;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 初期buff設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Buff01_Click(object sender, EventArgs e)
        {
            edtiBuff frm = new edtiBuff(objName, dataObj);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }
        /// <summary>
        /// pot buff設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Buff02_Click(object sender, EventArgs e)
        {
            edtPotBuff frm = new edtPotBuff(objName, dataObj);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        /// <summary>
        /// 通常攻撃設定1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Atack01_Click(object sender, EventArgs e)
        {
            edtMainloop frm = new edtMainloop(objName, dataObj);
            frm.StartPosition = FormStartPosition.CenterParent;
            if( frm.ShowDialog() == DialogResult.Cancel)
            {
            }
        }
        /// <summary>
        /// 初期buff2設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Buff03_Click(object sender, EventArgs e)
        {
            edtiBuff2 frm = new edtiBuff2(objName, dataObj);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }
        /// <summary>
        /// その他buff
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEtcBuff_Click(object sender, EventArgs e)
        {
            edtiBuff3 frm = new edtiBuff3(objName, dataObj);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }
    }
}
