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
    public partial class edtPotBuff : Form
    {
        public string objName;
        public Data dataObj;

        public edtPotBuff(string _objName, Data _dataObj)
        {
            InitializeComponent();
            dataObj = _dataObj;
            objName = _objName;
        }

        private void edtPotBuff_Load(object sender, EventArgs e)
        {
            try
            {
                this.MinimumSize = new Size(265, 428);
                this.MaximumSize = new Size(265, 1440);

                listView1.Clear();
                listView1.View = View.Details;
                listView1.Columns.Add("スキル名称", 200, HorizontalAlignment.Left);

                _potBuff data;
                if (dataObj.dataPotObj == null || dataObj.dataPotObj.Items == null || dataObj.dataPotObj.Items.Count == 0)
                {
                    dataObj.dataPotObj = new potBuffCodeData();
                    dataObj.dataPotObj.Items = new System.Collections.ArrayList();
                    dataObj.dataPotObj.Items.Add(new _potBuff(0, 3600));
                    data = (_potBuff)dataObj.dataPotObj.Items[0];
                    txtWaitTime.Text = data.potbuffkey.ToString();
                    chkStart.Checked = false;
                }
                else
                {
                    data = (_potBuff)dataObj.dataPotObj.Items[0];
                    txtWaitTime.Text = data.potbuffkey.ToString();
                    // マクロ開始時に実行
                    if (data.potbuffno == 2)
                        chkStart.Checked = true;
                }
                dispPotList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //btnUp.Enabled = false;
            //btnDown.Enabled = false;

        }
        /// <summary>
        /// キーリスト表示
        /// </summary>
        public void dispPotList()
        {
            listView1.Items.Clear();
            if (dataObj.dataPotObj.Items != null)
            {
                _potBuff data;
                for (int i = 1; i < dataObj.dataPotObj.Items.Count; i++)
                {
                    data = (_potBuff)dataObj.dataPotObj.Items[i];
                    listView1.Items.Add(searchKeyName(data.potbuffkey));
                }
            }
        }
        /// <summary>
        /// キーコード名称取得
        /// </summary>
        /// <param name="keyno"></param>
        /// <returns></returns>
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
        /// 追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            potEdit frm = new potEdit(objName, dataObj);
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (dataObj.dataPotObj == null)
                {
                    dataObj.dataPotObj = new potBuffCodeData();
                    dataObj.dataPotObj.Items = new System.Collections.ArrayList();
                }
                if (listView1.SelectedItems.Count >= 1)
                {
                    // 選択中の後に挿入追加
                    dataObj.dataPotObj.Items.Insert(listView1.SelectedItems[0].Index + 1 + 1, new _potBuff(0, frm.keyno));
                }
                else
                {
                    // 最後に追加
                    dataObj.dataPotObj.Items.Add(new _potBuff(0, frm.keyno));
                }
                dispPotList();
            }

        }
        /// <summary>
        /// 編集
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

            _potBuff data = (_potBuff)dataObj.dataPotObj.Items[listView1.SelectedItems[0].Index + 1];

            potEdit frm = new potEdit(objName, dataObj, data.potbuffkey);
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                data.potbuffno = 0;
                data.potbuffkey = frm.keyno;
                dataObj.dataPotObj.Items[listView1.SelectedItems[0].Index + 1] = data;

                dispPotList();
            }


        }
        /// <summary>
        /// OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                _potBuff data = (_potBuff)dataObj.dataPotObj.Items[0];
                // マクロ開始時に実行
                if (chkStart.Checked == true)
                    data.potbuffno = 2;
                else
                    data.potbuffno = 1;
                // サイクル時間
                data.potbuffkey = int.Parse(txtWaitTime.Text);
                dataObj.dataPotObj.Items[0] = data;
                dataObj.saveData(objName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();

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
                dataObj.dataPotObj.Items.RemoveAt(listView1.SelectedItems[0].Index + 1);
                dispPotList();
            }
        }

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
                _potBuff item = (_potBuff)dataObj.dataPotObj.Items[listView1.SelectedItems[0].Index + 1];
                dataObj.dataPotObj.Items.RemoveAt(listView1.SelectedItems[0].Index + 1);
                dataObj.dataPotObj.Items.Insert(listView1.SelectedItems[0].Index - 1 + 1, item);
                index -= 1;
            }
            dispPotList();

            listView1.Items[index].Selected = true;
            listView1.Focus();

        }

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

            if (listView1.SelectedItems[0].Index + 1 + 1 < dataObj.dataPotObj.Items.Count)
            {
                _potBuff item = (_potBuff)dataObj.dataPotObj.Items[listView1.SelectedItems[0].Index + 1];
                dataObj.dataPotObj.Items.RemoveAt(listView1.SelectedItems[0].Index + 1);
                dataObj.dataPotObj.Items.Insert(listView1.SelectedItems[0].Index + 1 + 1, item);
                index += 1;
            }
            dispPotList();
            listView1.Items[index].Selected = true;
            listView1.Focus();

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //btnUp.Enabled = true;
            //btnDown.Enabled = true;

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

            _potBuff data = (_potBuff)dataObj.dataPotObj.Items[listView1.SelectedItems[0].Index + 1];

            potEdit frm = new potEdit(objName, dataObj, data.potbuffkey);
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                data.potbuffno = 0;
                data.potbuffkey = frm.keyno;
                dataObj.dataPotObj.Items[listView1.SelectedItems[0].Index + 1] = data;

                dispPotList();
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
