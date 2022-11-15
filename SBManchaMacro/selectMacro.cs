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
    public partial class selectMacro : Form
    {
        public string objName;
        public Data dataObj;
        public string[] macroName;
        public string selectMacroName;
        public string selectFolder;

        public selectMacro(string _objName, Data _dataObj)
        {
            InitializeComponent();
            selectMacroName = "";
        }
        private void selectMacro_Load(object sender, EventArgs e)
        {
            listMacro.Clear();
            listMacro.View = View.Details;
            listMacro.Columns.Add("マクロ名", 200, HorizontalAlignment.Left);
            //dispMainList();
            for (int i = 0; i < macroName.Count(); i++)
            {
                listMacro.Items.Add(macroName[i]);
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int idx = listMacro.SelectedItems.Count;
            if (idx < 1)
            {
                return;
            }

            selectMacroName = listMacro.SelectedItems[0].Text;

        }

    }
}
