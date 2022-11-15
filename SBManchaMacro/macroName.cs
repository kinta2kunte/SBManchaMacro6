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
    public partial class macroName : Form
    {
        public string sMacroName;
        public macroName(string _macroName)
        {
            InitializeComponent();
            sMacroName = _macroName;
        }

        private void macroName_Load(object sender, EventArgs e)
        {
            txtMacroName.Text = sMacroName;
        }
        /// <summary>
        /// OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            sMacroName = txtMacroName.Text;
        }
    }
}
