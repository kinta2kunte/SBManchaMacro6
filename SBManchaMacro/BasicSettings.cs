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
    public partial class BasicSettings : Form
    {
        public bool bTopMost;
        public bool bLog;
        public double dBasicWait;

        public BasicSettings()
        {
            InitializeComponent();
        }

        private void BasicSettings_Load(object sender, EventArgs e)
        {
            txtBaseicWait.Text =  Properties.Settings.Default.dBasicWait.ToString();
            chkTopMust.Checked =  Properties.Settings.Default.chkTopMust;
            chkLog.Checked = Properties.Settings.Default.bLog;;
            bTopMost = chkTopMust.Checked;
            bLog = chkLog.Checked;
            dBasicWait = Properties.Settings.Default.dBasicWait;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            { 
                bTopMost = chkTopMust.Checked;
                dBasicWait = double.Parse(txtBaseicWait.Text);
                Properties.Settings.Default.dBasicWait = double.Parse(txtBaseicWait.Text);
                Properties.Settings.Default.chkTopMust = chkTopMust.Checked;
                Properties.Settings.Default.bLog = chkLog.Checked;
                Properties.Settings.Default.bTaskTray = chkTaskTray.Checked;
                Properties.Settings.Default.Save();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         }
    }
}
