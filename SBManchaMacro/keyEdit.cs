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
  
    public partial class keyEdit : Form
    {
        public string objName;
        public Data dataObj;

        public string keyName;
        public int keyCode;
        public double keyTime;

        public keyEdit(string _objName, Data _dataObj)
        {
            InitializeComponent();
            dataObj = _dataObj;
            objName = _objName;
        }
        public keyEdit(string _objName, Data _dataObj, string s1, string s2, string s3)
        {
            InitializeComponent();
            dataObj = _dataObj;
            objName = _objName;

            txtName.Text = s1;
            keyCode = byte.Parse(s2);
            txtKeyCode.Text = s2;
            txtTime.Text = s3;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            KeysConverter kc = new KeysConverter();
            txtKeyCode.Text = kc.ConvertToString(keyCode);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            keyCode = (int)e.KeyCode;
            KeysConverter kc = new KeysConverter();
            txtKeyCode.Text = kc.ConvertToString(keyCode);
        }
        /// <summary>
        /// OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            { 
                keyName = txtName.Text;
                //keyCode = keyCode;
                keyTime = Double.Parse(txtTime.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                DialogResult = DialogResult.Cancel;
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// キーコードクリア（None）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            KeysConverter kc = new KeysConverter();
            string str = kc.ConvertToString(0);
            keyCode = 0;
            txtKeyCode.Text = str;
        }
    }
}
