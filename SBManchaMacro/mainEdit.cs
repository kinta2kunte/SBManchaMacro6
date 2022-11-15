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
    public partial class mainEdit : Form
    {
        public string objName;
        public Data dataObj;
        public int keyno;
        public string keyname;

        public mainEdit(string _objName, Data _dataObj)
        {
            InitializeComponent();
            dataObj = _dataObj;
            objName = _objName;
            keyno = -1;
            label2.Text = "";
        }
        public mainEdit(string _objName, Data _dataObj, int _keyno)
        {
            InitializeComponent();
            dataObj = _dataObj;
            objName = _objName;
            keyno = _keyno;
            label2.Text = searchKeyName(_keyno);
        }

        private void mainEdit_Load(object sender, EventArgs e)
        {
            cmbKey.Items.Clear();
            if (dataObj.dataObj.Items != null)
            {
                _key data;
                for (int i = 0; i < dataObj.dataObj.Items.Count; i++)
                {
                    data = (_key)dataObj.dataObj.Items[i];
                    cmbKey.Items.Add(data.keyname);
                    //keyno = data.keyno;
                    //keyname = data.keyname;
                }
                cmbKey.SelectedIndex = 0;
            }

        }
        private string searchKeyName(int keyno)
        {
            string str = "";
            _key keydata;

            for (int i = 0; i < dataObj.dataObj.Items.Count; i++)
            {
                keydata = (_key)dataObj.dataObj.Items[i];
                if (keyno == keydata.keyno)
                    return keydata.keyname;
            }
            return str;
        }

        private void cmbKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataObj.dataObj.Items != null)
            {
                _key data;
                for (int i = 0; i < dataObj.dataObj.Items.Count; i++)
                {
                    string str = cmbKey.SelectedItem.ToString();
                    data = (_key)dataObj.dataObj.Items[i];
                    if (str == data.keyname)
                    {
                        keyno = data.keyno;
                        keyname = data.keyname;
                    }
                }
            }

        }
    }
}
