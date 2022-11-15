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
    public partial class formKeySettings : Form
    {
        public string objName;
        public Data dataObj;
        public formKeySettings(string _objName, Data _dataObj )
        {
            InitializeComponent();
            dataObj = _dataObj;
            objName = _objName;

            if (dataObj.dataObj.Items == null)
                dataObj.dataObj.Items = new System.Collections.ArrayList();
        }

        private void formKeySettings_Load(object sender, EventArgs e)
        {
            this.MinimumSize = new Size(400, 430);
            this.MaximumSize = new Size(400, 1440);

            listView1.Clear();
            listView1.View = View.Details;
            listView1.Columns.Add("NO", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("スキル名称", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("キーコード", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("keyCode", 0, HorizontalAlignment.Left);
            listView1.Columns.Add("キャスト時間", 80, HorizontalAlignment.Left);

            // キー削除は色々問題が派生する為disable
            button3.Enabled = false;

            dispKeyList();
        }
        /// <summary>
        /// キーリスト表示
        /// </summary>
        public void dispKeyList()
        {
            listView1.Items.Clear();
            if (dataObj.dataObj.Items != null)
            {
                _key data;
                KeysConverter kc = new KeysConverter();
                for (int i = 0; i<dataObj.dataObj.Items.Count; i++)
                {
                    data = (_key) dataObj.dataObj.Items[i];
                    listView1.Items.Add(data.keyno.ToString());
                    listView1.Items[i].SubItems.Add(data.keyname);
                    listView1.Items[i].SubItems.Add(
                           kc.ConvertToString((int) data.keycode));
                    listView1.Items[i].SubItems.Add(data.keycode.ToString());
                    listView1.Items[i].SubItems.Add(data.keytime.ToString());
                }
            }
        }
        /// <summary>
        /// キー設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            dataObj.saveData(objName);
            this.Close();
        }
        /// <summary>
        /// 追加ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            keyEdit frm;
            frm = new keyEdit(objName, dataObj);
            frm.StartPosition = FormStartPosition.CenterParent;
            if ( frm.ShowDialog() == DialogResult.OK )
            {
                // 最大番号取得
                int no = dataObj.getMaxKey() + 1;

                if( listView1.SelectedItems.Count >= 1 )
                {
                    // 選択中の後に挿入追加
                    dataObj.dataObj.Items.Insert(listView1.SelectedItems[0].Index + 1, new _key(no, frm.keyName, (byte)frm.keyCode, frm.keyTime));
                }
                else
                {
                    // 最後に追加
                    dataObj.dataObj.Items.Add(new _key(no, frm.keyName, (byte)frm.keyCode, frm.keyTime));
                }

                dispKeyList();
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
            keyEdit frm;
            frm = new keyEdit(objName, dataObj, itemx.SubItems[1].Text, itemx.SubItems[3].Text, itemx.SubItems[4].Text);
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _key data = (_key)dataObj.dataObj.Items[listView1.SelectedItems[0].Index];
                data.keyname = frm.keyName;
                data.keycode = (byte)frm.keyCode;
                data.keytime = frm.keyTime;
                dataObj.dataObj.Items[listView1.SelectedItems[0].Index] = data;

                dispKeyList();
            }

        }
        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            btnDelete();
        }
        private void btnDelete()
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
                dataObj.dataObj.Items.RemoveAt(listView1.SelectedItems[0].Index);
                dispKeyList();
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
                _key item = (_key)dataObj.dataObj.Items[listView1.SelectedItems[0].Index];
                dataObj.dataObj.Items.RemoveAt(listView1.SelectedItems[0].Index);
                dataObj.dataObj.Items.Insert(listView1.SelectedItems[0].Index - 1, item);
                index -= 1;
            }
            dispKeyList();

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

            if (listView1.SelectedItems[0].Index + 1 < dataObj.dataObj.Items.Count)
            {
                _key item = (_key)dataObj.dataObj.Items[listView1.SelectedItems[0].Index];
                dataObj.dataObj.Items.RemoveAt(listView1.SelectedItems[0].Index);
                dataObj.dataObj.Items.Insert(listView1.SelectedItems[0].Index + 1, item);
                index += 1;
            }
            dispKeyList();

            listView1.Items[index].Selected = true;
            listView1.Focus();

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// ダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            int idx = listView1.SelectedItems.Count;
            if (idx < 1)
            {
                MessageBox.Show("選択エラー");
                return;
            }
            ListViewItem itemx = listView1.SelectedItems[0];
            keyEdit frm;
            frm = new keyEdit(objName, dataObj, itemx.SubItems[1].Text, itemx.SubItems[3].Text, itemx.SubItems[4].Text);
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _key data = (_key)dataObj.dataObj.Items[listView1.SelectedItems[0].Index];
                data.keyname = frm.keyName;
                data.keycode = (byte)frm.keyCode;
                data.keytime = frm.keyTime;
                dataObj.dataObj.Items[listView1.SelectedItems[0].Index] = data;

                dispKeyList();

            }

        }
        /// <summary>
        /// 基本キーの追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBasicKey_Click(object sender, EventArgs e)
        {
            if( MessageBox.Show("Shadowbane基本キー設定を登録しますがよろしいですか？","",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            { 
                // 最大番号取得
                int no = dataObj.getMaxKey() + 1;
                _key key = new _key();

                key.keyname = "Next NPC";
                key.keycode = 0xba; // :
                key.keytime = 0.1;
                // 最後に追加
                dataObj.dataObj.Items.Add(new _key(no ++, key.keyname, (byte)key.keycode, key.keytime));

                key.keyname = "Target Self";
                key.keycode = 0x23; // END
                key.keytime = 0.1;
                // 最後に追加
                dataObj.dataObj.Items.Add(new _key(no++, key.keyname, (byte)key.keycode, key.keytime));

                key.keyname = "Target Clear";
                key.keycode = 0x00; // 
                key.keytime = 0.1;
                // 最後に追加
                dataObj.dataObj.Items.Add(new _key(no++, key.keyname, (byte)key.keycode, key.keytime));

                key.keyname = "Target Player";
                key.keycode = 0xbc; // , 
                key.keytime = 0.1;
                // 最後に追加
                dataObj.dataObj.Items.Add(new _key(no++, key.keyname, (byte)key.keycode, key.keytime));

                key.keyname = "Combat";
                key.keycode = 0x43; // C 
                key.keytime = 0.1;
                // 最後に追加
                dataObj.dataObj.Items.Add(new _key(no++, key.keyname, (byte)key.keycode, key.keytime));

                key.keyname = "Run/Walk";
                key.keycode = 0x55; // U 
                key.keytime = 0.1;
                // 最後に追加
                dataObj.dataObj.Items.Add(new _key(no++, key.keyname, (byte)key.keycode, key.keytime));

                key.keyname = "INT Pot";
                key.keycode = 0x00; //  
                key.keytime = 11;
                // 最後に追加
                dataObj.dataObj.Items.Add(new _key(no++, key.keyname, (byte)key.keycode, key.keytime));

                key.keyname = "SPI Pot";
                key.keycode = 0x00; //  
                key.keytime = 11;
                // 最後に追加
                dataObj.dataObj.Items.Add(new _key(no++, key.keyname, (byte)key.keycode, key.keytime));

                key.keyname = "MR Pot";
                key.keycode = 0x00; //  
                key.keytime = 11;
                // 最後に追加
                dataObj.dataObj.Items.Add(new _key(no++, key.keyname, (byte)key.keycode, key.keytime));


                dispKeyList();
            }
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyData == Keys.Delete)
            //{
            //    btnDelete();
            //}
        }
    }
}
