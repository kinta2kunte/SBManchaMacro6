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
    public partial class edtiBuff : Form
    {
        public string objName;
        public Data dataObj;

        public edtiBuff(string _objName, Data _dataObj)
        {
            InitializeComponent();
            dataObj = _dataObj;
            objName = _objName;

        }
        private void edtInitBuff_Load(object sender, EventArgs e)
        {
            try
            {
                this.MinimumSize = new Size(265, 428);
                this.MaximumSize = new Size(265, 1440);

                listView1.Clear();
                listView1.View = View.Details;
                listView1.Columns.Add("スキル名称", 200, HorizontalAlignment.Left);

                _initBuff data;
                if (dataObj.dataIBuffObj == null || dataObj.dataIBuffObj.Items == null || dataObj.dataIBuffObj.Items.Count == 0)
                {
                    dataObj.dataIBuffObj = new iBuffCodeData();
                    dataObj.dataIBuffObj.Items = new System.Collections.ArrayList();
                    dataObj.dataIBuffObj.Items.Add(new _initBuff(0, 1800));
                    data = (_initBuff)dataObj.dataIBuffObj.Items[0];
                    txtWaitTime.Text = data.ibuffkey.ToString();
                    chkStart.Checked = true;

                }
                else
                {
                    data = (_initBuff)dataObj.dataIBuffObj.Items[0];
                    txtWaitTime.Text = data.ibuffkey.ToString();
                    // マクロ開始時に実行
                    if (data.ibuffno == 2)
                        chkStart.Checked = true;
                }
                dispiBuffList();
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
        public void dispiBuffList()
        {
            listView1.Items.Clear();
            if (dataObj.dataIBuffObj.Items != null)
            {
                _initBuff data;
                for (int i = 1; i < dataObj.dataIBuffObj.Items.Count; i++)
                {
                    data = (_initBuff)dataObj.dataIBuffObj.Items[i];
                    listView1.Items.Add( searchKeyName(data.ibuffkey));
                }
            }
        }
        /// <summary>
        /// 追加ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            iBuffEdit frm = new iBuffEdit(objName, dataObj);
            frm.StartPosition = FormStartPosition.CenterParent;

            if ( frm.ShowDialog() == DialogResult.OK )
            {
                if (dataObj.dataIBuffObj == null)
                { 
                    dataObj.dataIBuffObj = new iBuffCodeData();
                    dataObj.dataIBuffObj.Items = new System.Collections.ArrayList();
                }

                if (listView1.SelectedItems.Count >= 1)
                {
                    // 選択中の後に挿入追加
                    dataObj.dataIBuffObj.Items.Insert(listView1.SelectedItems[0].Index + 1 + 1, new _initBuff(0, frm.keyno));
                }
                else
                {
                    // 最後に追加
                    dataObj.dataIBuffObj.Items.Add(new _initBuff(0, frm.keyno));
                }
                dispiBuffList();
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

            _initBuff data = (_initBuff)dataObj.dataIBuffObj.Items[listView1.SelectedItems[0].Index + 1];

            iBuffEdit frm = new iBuffEdit(objName, dataObj, data.ibuffkey);
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                data.ibuffno = 0;
                data.ibuffkey = frm.keyno;
                dataObj.dataIBuffObj.Items[listView1.SelectedItems[0].Index + 1] = data;

                dispiBuffList();
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
                _initBuff data = (_initBuff)dataObj.dataIBuffObj.Items[0];
                // マクロ開始時に実行
                if( chkStart.Checked == true )
                    data.ibuffno = 2;
                else
                    data.ibuffno = 1;
                // サイクル時間
                data.ibuffkey = int.Parse(txtWaitTime.Text);
                dataObj.dataIBuffObj.Items[0] = data;
                dataObj.saveData(objName);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

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
                if( keyno == keydata.keyno )
                    return keydata.keyname;
            }
            return str;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnUp.Enabled = true;
            btnDown.Enabled = true;

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
                dataObj.dataIBuffObj.Items.RemoveAt(listView1.SelectedItems[0].Index + 1);

                dispiBuffList();
            }

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

            if (listView1.SelectedItems[0].Index > 0)
            {
                _initBuff item = (_initBuff)dataObj.dataIBuffObj.Items[listView1.SelectedItems[0].Index + 1];
                dataObj.dataIBuffObj.Items.RemoveAt(listView1.SelectedItems[0].Index + 1);
                dataObj.dataIBuffObj.Items.Insert(listView1.SelectedItems[0].Index - 1 + 1, item);
                index -= 1;
            }
            dispiBuffList();
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

            if (listView1.SelectedItems[0].Index + 1 + 1 < dataObj.dataIBuffObj.Items.Count)
            {
                _initBuff item = (_initBuff)dataObj.dataIBuffObj.Items[listView1.SelectedItems[0].Index + 1];
                dataObj.dataIBuffObj.Items.RemoveAt(listView1.SelectedItems[0].Index + 1);
                dataObj.dataIBuffObj.Items.Insert(listView1.SelectedItems[0].Index + 1 + 1, item);
                index += 1;
            }
            dispiBuffList();
            listView1.Items[index].Selected = true;
            listView1.Focus();

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

            _initBuff data = (_initBuff)dataObj.dataIBuffObj.Items[listView1.SelectedItems[0].Index + 1];

            iBuffEdit frm = new iBuffEdit(objName, dataObj, data.ibuffkey);
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                data.ibuffno = 0;
                data.ibuffkey = frm.keyno;
                dataObj.dataIBuffObj.Items[listView1.SelectedItems[0].Index + 1] = data;

                dispiBuffList();
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
