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
    public partial class edtMainloop : Form
    {
        public string objName;
        public Data dataObj;

        public edtMainloop(string _objName, Data _dataObj)
        {
            InitializeComponent();
            dataObj = _dataObj;
            objName = _objName;
        }

        private void edtMainloop_Load(object sender, EventArgs e)
        {
            try
            { 
                this.MinimumSize = new Size(265, 428);
                this.MaximumSize = new Size(265, 1440);

                listView1.Clear();
                listView1.View = View.Details;
                listView1.Columns.Add("スキル名称", 200, HorizontalAlignment.Left);

                if (dataObj.dataMainObj == null || dataObj.dataMainObj.Items == null)
                {
                    dataObj.dataMainObj = new mainLoopData();
                    dataObj.dataMainObj.Items = new System.Collections.ArrayList();
                }
                dispMainList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //btnEdit.Enabled = false;
            //btnDelete.Enabled = false;
            //btnUp.Enabled = false;
            //btnDown.Enabled = false;
        }

        /// <summary>
        /// キーリスト表示
        /// </summary>
        public void dispMainList()
        {
            listView1.Items.Clear();
            if (dataObj.dataMainObj.Items != null)
            {
                _mainLoop data;
                for (int i = 0; i < dataObj.dataMainObj.Items.Count; i++)
                {
                    data = (_mainLoop)dataObj.dataMainObj.Items[i];
                    listView1.Items.Add(searchKeyName(data.mainkey));
                }
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
        /// <summary>
        /// 追加ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            mainEdit frm = new mainEdit(objName, dataObj);
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (dataObj.dataMainObj == null)
                {
                    dataObj.dataMainObj = new mainLoopData();
                    dataObj.dataMainObj.Items = new System.Collections.ArrayList();
                }

                if (listView1.SelectedItems.Count >= 1)
                {
                    // 選択中の後に挿入追加
                    dataObj.dataMainObj.Items.Insert(listView1.SelectedItems[0].Index + 1, new _mainLoop(0, frm.keyno));
                }
                else
                {
                    // 最後に追加
                    dataObj.dataMainObj.Items.Add(new _mainLoop(0, frm.keyno));
                }
                dispMainList();
            }
        }
        /// <summary>
        /// 編集ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            int idx = listView1.SelectedItems.Count;
            if (idx < 1)
            {
                MessageBox.Show("選択エラー");
                return;
            }
            ListViewItem itemx = listView1.SelectedItems[0];

            _mainLoop data = (_mainLoop)dataObj.dataMainObj.Items[listView1.SelectedItems[0].Index];

            mainEdit frm = new mainEdit(objName, dataObj, data.mainkey);
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                data.mainno = 0;
                data.mainkey = frm.keyno;
                dataObj.dataMainObj.Items[listView1.SelectedItems[0].Index] = data;

                dispMainList();
            }


        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            dataObj.saveData(objName);
            this.Close();

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //btnEdit.Enabled = true;
            //btnDelete.Enabled = true;
            //btnUp.Enabled = true;
            //btnDown.Enabled = true;

        }

        /// <summary>
        /// ↑移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            int idx = listView1.SelectedItems.Count;
            if (idx < 1)
            {
                MessageBox.Show("選択エラー");
                return;
            }
            ListViewItem itemx = listView1.SelectedItems[0];
            int index = listView1.SelectedItems[0].Index;

            if (listView1.SelectedItems[0].Index > 0 )
            { 
                _mainLoop item = (_mainLoop)dataObj.dataMainObj.Items[listView1.SelectedItems[0].Index];
                dataObj.dataMainObj.Items.RemoveAt(listView1.SelectedItems[0].Index);
                dataObj.dataMainObj.Items.Insert(listView1.SelectedItems[0].Index - 1, item);
                index -= 1;
            }
            dispMainList();
            listView1.Items[index].Selected = true;
            listView1.Focus();
        }
        /// <summary>
        /// ↓移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            int idx = listView1.SelectedItems.Count;
            if (idx < 1)
            {
                MessageBox.Show("選択エラー");
                return;
            }
            ListViewItem itemx = listView1.SelectedItems[0];
            int index = listView1.SelectedItems[0].Index;

            if (listView1.SelectedItems[0].Index + 1 < dataObj.dataMainObj.Items.Count )
            {
                _mainLoop item = (_mainLoop)dataObj.dataMainObj.Items[listView1.SelectedItems[0].Index];
                dataObj.dataMainObj.Items.RemoveAt(listView1.SelectedItems[0].Index);
                dataObj.dataMainObj.Items.Insert(listView1.SelectedItems[0].Index + 1, item);
                index += 1;
            }
            dispMainList();
            listView1.Items[index].Selected = true;
            listView1.Focus();
        }
        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            keyDelete();
        }
        private void keyDelete()
        { 
            int idx = listView1.SelectedItems.Count;
            if (idx < 1)
            {
                MessageBox.Show("選択エラー");
                return;
            }
            ListViewItem itemx = listView1.SelectedItems[0];

            //_mainLoop data = (_mainLoop)dataObj.dataMainObj.Items[listView1.SelectedItems[0].Index];
            //_key keydata = (_key)dataObj.dataObj.Items[listView1.SelectedItems[0].Index];
            if (MessageBox.Show(itemx.Text + "を削除します。", "確認",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                dataObj.dataMainObj.Items.RemoveAt(listView1.SelectedItems[0].Index);
                dispMainList();
            }

        }
        /// <summary>
        /// フォーカスアウト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_Leave(object sender, EventArgs e)
        {
            //btnEdit.Enabled = false;
            //btnDelete.Enabled = false;
            //btnUp.Enabled = false;
            //btnDown.Enabled = false;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            int idx = listView1.SelectedItems.Count;
            if (idx < 1)
            {
                MessageBox.Show("選択エラー");
                return;
            }
            ListViewItem itemx = listView1.SelectedItems[0];

            _mainLoop data = (_mainLoop)dataObj.dataMainObj.Items[listView1.SelectedItems[0].Index];

            mainEdit frm = new mainEdit(objName, dataObj, data.mainkey);
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                data.mainno = 0;
                data.mainkey = frm.keyno;
                dataObj.dataMainObj.Items[listView1.SelectedItems[0].Index] = data;

                dispMainList();
            }

        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                keyDelete();
            }

        }
    }
}
