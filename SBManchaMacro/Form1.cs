using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

using System.Runtime.InteropServices; //DLLImportを使うために

namespace SBManchaMacro
{
    public partial class Form1 : Form
    {
        //ウィンドウを探す用のメソッド
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern Int32 FindWindow(String lpClassName, String lpWindowName);

        //送信するためのメソッド(数値)
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern Int32 SendMessage(Int32 hWnd, Int32 Msg, Int32 wParam, ref COPYDATASTRUCT lParam);

        //送信するためのメソッド(文字も可能)
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern Int32 SendMessage(Int32 hWnd, Int32 Msg, Int32 wParam, Int32 lParam);

        //送信するためのメソッド(数値)
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern Int32 PostMessage(Int32 hWnd, Int32 Msg, Int32 wParam, ref COPYDATASTRUCT lParam);

        //送信するためのメソッド(文字も可能)
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern Int32 PostMessage(Int32 hWnd, Int32 Msg, Int32 wParam, Int32 lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int IsWindow(IntPtr hWnd);

        //文字列を送信する時に使う構造体
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData; //送信する32ビット値
            public int cbData; //lpDataのバイト数
            public string lpData; //送信するデータへのポインタ(0も可能)
        }

        public List<string> macroName;
        public int nameIndex;
        public Data data;
        public string objName;

        public bool threadFlg = false;
        public bool macroBtnFlg = false;

        // 基本設定
        public double dBasicWait = 0;
        public bool bTopMust = false;
        public bool bLog = false;
        public string sWindowName = "shadowbane";

        // 画面終了フラグ
        public bool closeFlg = false;

        public Form1()
        {
            InitializeComponent();

            macroName = new List<string>();
            // オブジェクト名＝マクロ名
            //objName = "Warlock マクロサンプル";
            //macroName.Add(objName);
            //saveMacroName();


            // データオブジェクト＆ロード
            data = new Data(objName);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // マクロ名読み込み
            loadMacroName();
            if( macroName.Count > 0 )
            { 
                cmbMacroName.SelectedIndex = 0;
                objName = cmbMacroName.SelectedItem.ToString();
            
            }
            else
            {
                objName = "";
            }
            //cmbMacroName.Items.Clear();
            //cmbMacroName.Items.Add(objName);
            dBasicWait = Properties.Settings.Default.dBasicWait;
            bTopMust = Properties.Settings.Default.chkTopMust;
            sWindowName= Properties.Settings.Default.sWindowNmae;

            int idx = Properties.Settings.Default.nSekectMacro;
            if (idx < 0 || idx > cmbMacroName.Items.Count)
                idx = 0;
            if(macroName.Count > 0) 
                cmbMacroName.SelectedIndex = idx;

            if (bTopMust == true)
                this.TopMost = true;

            //----------------------------------------
            // 右クリックメニュー
            //----------------------------------------
            this.contextMenuStrip1.Items[1].Visible = false;
            for (int i = 0; i < macroName.Count; i++)
            {
                ToolStripMenuItem tsiLeft = new ToolStripMenuItem();
                tsiLeft.Text = macroName[i];
                tsiLeft.ToolTipText = macroName[i] + " 実行";
                this.contextMenuStrip1.Items.Add(tsiLeft);
            }
            // タスクトレイ格納
            if (Properties.Settings.Default.bTaskTray == true )
            {
                this.WindowState = FormWindowState.Minimized;
            }
            else
            { 
                //タスクトレイのアイコン表示を無効
                notifyIcon1.Visible = false;
                this.Visible = true;
                this.Focus();
            }
        }
        /// <summary>
        /// 終了処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 終了フラグ
            closeFlg = true;
            // スレッド終了処理
            threadFlg = false;
            //タスクトレイのアイコン表示を無効
            notifyIcon1.Visible = false;

            Properties.Settings.Default.nSekectMacro = cmbMacroName.SelectedIndex;
            Properties.Settings.Default.Save();

        }
        /// <summary>
        /// マクロ開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SBMacroStart_Click(object sender, EventArgs e)
        {
            MacroStartButton();
        }
        void MacroStartButton()
        {
            try
            {
                if (threadFlg == false)
                {
                    //    this.SBMacroStart.Text = "マクロ停止";
                    //    this.SBMacroStart.Image = Properties.Resources.stop1;
                    //    cmbMacroName.Enabled = false;
                    //    btnEdit.Enabled = false;
                    //    btnAdd.Enabled = false;
                    //    btnDelete.Enabled = false;
                    //    btnBaseSttings.Enabled = false;
                    //    btnCopy.Enabled = false;

                    threadFlg = true;
                    dispMacroBtn();
                    Thread t = new Thread(new ThreadStart(StartMacro));
                    t.Start();

                }
                else
                {
                    threadFlg = false;
                    dispMacroBtn();
                    //    this.SBMacroStart.Text = "マクロ開始";
                    //    this.SBMacroStart.Image = Properties.Resources.start;
                    //    cmbMacroName.Enabled = true;
                    //    btnEdit.Enabled = true;
                    //    btnAdd.Enabled = true;
                    //    btnDelete.Enabled = true;
                    //    btnBaseSttings.Enabled = true;
                    //    btnCopy.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                putErrLog(ex.Message);
                putErrLog(ex.StackTrace);
            }
        }
        delegate void dispMacroBtnDelegate();
        private void dispListDelegateWorker()
        {
            if (closeFlg == true) return;
            Invoke(new dispMacroBtnDelegate(dispMacroBtn));
        }
        private void dispMacroBtn()
        {
            if (threadFlg == true)
            {
                this.SBMacroStart.Text = "マクロ停止";
                this.SBMacroStart.Image = Properties.Resources.stop1;
                cmbMacroName.Enabled = false;
                btnEdit.Enabled = false;
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                btnBaseSttings.Enabled = false;
                btnCopy.Enabled = false;
                btnInport.Enabled = false;

                // メニュー disable
                for (int j = 0; j < macroName.Count; j++)
                {
                    if (this.contextMenuStrip1.Items.Count > j + 5)
                        this.contextMenuStrip1.Items[j + 5].Visible = false;
                }
            }
            else
            {
                this.SBMacroStart.Text = "マクロ開始";
                this.SBMacroStart.Image = Properties.Resources.start;
                cmbMacroName.Enabled = true;
                btnEdit.Enabled = true;
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                btnBaseSttings.Enabled = true;
                btnCopy.Enabled = true;
                btnInport.Enabled = true;

                this.contextMenuStrip1.Items[1].Visible = false;
                // メニュー enable
                //for (int j = 0; j < macroName.Count + 1; j++)
                for (int j = 0; j < macroName.Count; j++)
                {
                    if (this.contextMenuStrip1.Items.Count > j + 5)
                    {
                        this.contextMenuStrip1.Items[j + 5].Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// マクロ実行スレッド
        /// </summary>
        private void StartMacro()
        {
            try
            { 
                int hWnd = FindWindow(null, sWindowName);
                //int hWnd = FindWindow(null, "shadowbane");
                if (hWnd == 0)
                {
                    //ハンドルが取得できなかった
                    MessageBox.Show("Shadowbane のWindowのハンドルが取得できません", "エラー",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // スレッド終了処理
                    threadFlg = false;
                    dispListDelegateWorker();
                    //this.SBMacroStart.Text = "マクロ開始";
                    //this.SBMacroStart.Image = Properties.Resources.start;
                    //btnEdit.Enabled = true;
                    //btnAdd.Enabled = true;
                    //btnDelete.Enabled = true;
                    //btnBaseSttings.Enabled = true;
                    return;
                }

                putLog("■Macro Start");


                //文字列を送る
                //int result = PostMessage(hWnd, 0x0100, 0x48, 0);        // Hキー
                //int result = PostMessage(hWnd, 0x0100, 0x70, 0);        // F1キー

                _key keyData;
                int keyCode;
                double castTime;
                int iniWait = 99999999;
                int ini2Wait = 99999999;
                int ini3Wait = 99999999;
                int potWait = 99999999;
                if (data.dataIBuffObj.Items != null && data.dataIBuffObj.Items.Count > 0)
                {
                    _initBuff d = (_initBuff)data.dataIBuffObj.Items[0];
                    // 割り込み周期時間
                    iniWait = d.ibuffkey;

                    if (d.ibuffno == 2)
                    {
                        // 初期buff
                        if (ibuffMacro(hWnd) == false)
                        {
                            // スレッド終了処理
                            threadFlg = false;
                            dispListDelegateWorker();
                            return;
                        }
                    }
                }
                if (data.dataIBuff2Obj.Items != null && data.dataIBuff2Obj.Items.Count > 0)
                {
                    _initBuff d = (_initBuff)data.dataIBuff2Obj.Items[0];
                    // 割り込み周期時間
                    ini2Wait = d.ibuffkey;

                    if (d.ibuffno == 2)
                    {
                        // 初期buff
                        if (ibuff2Macro(hWnd) == false)
                        {
                            // スレッド終了処理
                            threadFlg = false;
                            dispListDelegateWorker();
                            return;
                        }
                    }
                }
                if (data.dataIBuff3Obj.Items != null && data.dataIBuff3Obj.Items.Count > 0)
                {
                    _initBuff d = (_initBuff)data.dataIBuff3Obj.Items[0];
                    // 割り込み周期時間
                    ini3Wait = d.ibuffkey;

                    if (d.ibuffno == 2)
                    {
                        // 初期buff
                        if (ibuff3Macro(hWnd) == false)
                        {
                            // スレッド終了処理
                            threadFlg = false;
                            dispListDelegateWorker();
                            return;
                        }
                    }
                }

                if (data.dataPotObj.Items != null && data.dataPotObj.Items.Count > 0)
                {
                    _potBuff pd = (_potBuff)data.dataPotObj.Items[0];
                    potWait = pd.potbuffkey;
                    if (pd.potbuffno == 2)
                    {
                        // pot buff
                        if (potMacro(hWnd) == false)
                        {
                            // スレッド終了処理
                            threadFlg = false;
                            dispListDelegateWorker();
                            return;
                        }
                    }
                }

                _mainLoop md;

                DateTime startIBffDt = DateTime.Now;
                DateTime startIBff2Dt = DateTime.Now;
                DateTime startIBff3Dt = DateTime.Now;
                DateTime startPotDt = DateTime.Now;
                while (threadFlg)
                {
                    putLog("MainMacro");
                    if (IsWindow((System.IntPtr)hWnd) == 0)
                    {
                        //MessageBox.Show("Shandowbaneハンドル無効");
                        threadFlg = false;
                        // スレッド終了処理
                        dispListDelegateWorker();
                        return;
                    }
                    if (data.dataMainObj.Items == null || data.dataMainObj.Items.Count == 0)
                    {
                        if (threadFlg == false)
                        {
                            // スレッド終了処理
                            dispListDelegateWorker();
                            return;
                        }
                        // 初期buff時間判断
                        DateTime endDt = DateTime.Now;
                        TimeSpan ts = endDt - startIBffDt; // 時間の差分を取得
                        if (ts.TotalSeconds > iniWait)
                        {
                            // 初期buff
                            if (ibuffMacro(hWnd) == false)
                            {
                                // スレッド終了処理
                                threadFlg = false;
                                dispListDelegateWorker();
                                return;
                            }
                            startIBffDt = DateTime.Now;
                            //i = 0;
                            continue;
                        }
                        ts = endDt - startIBff2Dt; // 時間の差分を取得
                        if (ts.TotalSeconds > ini2Wait)
                        {
                            // 初期buff
                            if (ibuff2Macro(hWnd) == false)
                            {
                                // スレッド終了処理
                                threadFlg = false;
                                dispListDelegateWorker();
                                return;
                            }
                            startIBff2Dt = DateTime.Now;
                            //i = 0;
                            continue;
                        }
                        ts = endDt - startIBff3Dt; // 時間の差分を取得
                        if (ts.TotalSeconds > ini3Wait)
                        {
                            // その他buff
                            if (ibuff3Macro(hWnd) == false)
                            {
                                // スレッド終了処理
                                threadFlg = false;
                                dispListDelegateWorker();
                                return;
                            }
                            startIBff3Dt = DateTime.Now;
                            //i = 0;
                            continue;
                        }
                        // pot buff時間判断
                        ts = endDt - startPotDt; // 時間の差分を取得
                        if (ts.TotalSeconds > potWait)
                        {
                            // pot buff
                            if (potMacro(hWnd) == false)
                            {
                                // スレッド終了処理
                                threadFlg = false;
                                dispListDelegateWorker();
                                return;
                            }
                            startPotDt = DateTime.Now;
                            //i = 0;
                            continue;
                        }
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        for (int i = 0; i < data.dataMainObj.Items.Count; i++)
                        {
                            if (IsWindow((System.IntPtr)hWnd) == 0)
                            {
                                //MessageBox.Show("Shandowbaneハンドル無効");
                                threadFlg = false;
                            }

                            if (threadFlg == false)
                            {
                                // スレッド終了処理
                                dispListDelegateWorker();
                                return;
                            }
                            // 初期buff時間判断
                            DateTime endDt = DateTime.Now;
                            TimeSpan ts = endDt - startIBffDt; // 時間の差分を取得
                            if (ts.TotalSeconds > iniWait)
                            {
                                // 初期buff
                                if (ibuffMacro(hWnd) == false)
                                {
                                    // スレッド終了処理
                                    threadFlg = false;
                                    dispListDelegateWorker();
                                    return;
                                }
                                startIBffDt = DateTime.Now;
                                i = 0;
                                continue;
                            }
                            ts = endDt - startIBff2Dt; // 時間の差分を取得
                            if (ts.TotalSeconds > ini2Wait)
                            {
                                // 初期buff
                                if (ibuff2Macro(hWnd) == false)
                                {
                                    // スレッド終了処理
                                    threadFlg = false;
                                    dispListDelegateWorker();
                                    return;
                                }
                                startIBff2Dt = DateTime.Now;
                                i = 0;
                                continue;
                            }
                            ts = endDt - startIBff3Dt; // 時間の差分を取得
                            if (ts.TotalSeconds > ini3Wait)
                            {
                                // その他buff
                                if (ibuff3Macro(hWnd) == false)
                                {
                                    // スレッド終了処理
                                    threadFlg = false;
                                    dispListDelegateWorker();
                                    return;
                                }
                                startIBff3Dt = DateTime.Now;
                                i = 0;
                                continue;
                            }
                            // pot buff時間判断
                            ts = endDt - startPotDt; // 時間の差分を取得
                            if (ts.TotalSeconds > potWait)
                            {
                                // pot buff
                                if (potMacro(hWnd) == false)
                                {
                                    // スレッド終了処理
                                    threadFlg = false;
                                    dispListDelegateWorker();
                                    return;
                                }
                                startPotDt = DateTime.Now;
                                i = 0;
                                continue;
                            }

                            md = (_mainLoop)data.dataMainObj.Items[i];
                            keyData = searchKey(md.mainkey);
                            keyCode = keyData.keycode;
                            castTime = keyData.keytime;

                            if (keyCode != 0)
                                PostMessage(hWnd, 0x0100, keyCode, 0);        // キー
                            Thread.Sleep((int)(castTime * 1000));
                            Thread.Sleep((int)(dBasicWait * 1000));                // 基本wait
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                putErrLog(ex.Message);
                putErrLog(ex.StackTrace);
            }
        }
        /// <summary>
        /// 初期Buffマクロ
        /// </summary>
        /// <param name="hWnd"></param>
        private bool ibuffMacro(int hWnd)
        {
            putLog("ini Macro");

            _initBuff d = (_initBuff)data.dataIBuffObj.Items[0];
            _key keyData;
            // 割り込み周期時間
            int iniWait = d.ibuffkey;
            int keyCode;
            double castTime;
            for (int i = 1; i < data.dataIBuffObj.Items.Count; i++)
            {
                if (threadFlg == false)
                    return false;
                d = (_initBuff)data.dataIBuffObj.Items[i];
                keyData = searchKey(d.ibuffkey);
                keyCode = keyData.keycode;
                castTime = keyData.keytime;

                if (keyCode != 0)
                    PostMessage(hWnd, 0x0100, keyCode, 0);        // キー
                Thread.Sleep((int)(castTime * 1000));
                Thread.Sleep((int)(dBasicWait * 1000));                // 基本wait
            }
            return true;
        }
        /// <summary>
        /// 初期Buff2マクロ
        /// </summary>
        /// <param name="hWnd"></param>
        private bool ibuff2Macro(int hWnd)
        {
            putLog("ini 2 Macro");

            _initBuff d = (_initBuff)data.dataIBuff2Obj.Items[0];
            _key keyData;
            // 割り込み周期時間
            int iniWait = d.ibuffkey;
            int keyCode;
            double castTime;
            for (int i = 1; i < data.dataIBuff2Obj.Items.Count; i++)
            {
                if (threadFlg == false)
                    return false;
                d = (_initBuff)data.dataIBuff2Obj.Items[i];
                keyData = searchKey(d.ibuffkey);
                keyCode = keyData.keycode;
                castTime = keyData.keytime;

                if (keyCode != 0)
                    PostMessage(hWnd, 0x0100, keyCode, 0);        // キー
                Thread.Sleep((int)(castTime * 1000));
                Thread.Sleep((int)(dBasicWait * 1000));                // 基本wait
            }
            return true;
        }
        /// <summary>
        /// その他Buffマクロ
        /// </summary>
        /// <param name="hWnd"></param>
        private bool ibuff3Macro(int hWnd)
        {
            putLog("etc Macro");

            _initBuff d = (_initBuff)data.dataIBuff3Obj.Items[0];
            _key keyData;
            // 割り込み周期時間
            int iniWait = d.ibuffkey;
            int keyCode;
            double castTime;
            for (int i = 1; i < data.dataIBuff3Obj.Items.Count; i++)
            {
                if (threadFlg == false)
                    return false;
                d = (_initBuff)data.dataIBuff3Obj.Items[i];
                keyData = searchKey(d.ibuffkey);
                keyCode = keyData.keycode;
                castTime = keyData.keytime;

                if (keyCode != 0)
                    PostMessage(hWnd, 0x0100, keyCode, 0);        // キー
                Thread.Sleep((int)(castTime * 1000));
                Thread.Sleep((int)(dBasicWait * 1000));                // 基本wait
            }
            return true;
        }
        /// <summary>
        /// Pot Buffマクロ
        /// </summary>
        /// <param name="hWnd"></param>
        private bool potMacro(int hWnd)
        {
            putLog("PotMacro");

            _potBuff d = (_potBuff)data.dataPotObj.Items[0];
            _key keyData;
            // 割り込み周期時間
            int iniWait = d.potbuffkey;
            int keyCode;
            double castTime;
            for (int i = 1; i < data.dataPotObj.Items.Count; i++)
            {
                if (threadFlg == false)
                    return false;
                d = (_potBuff)data.dataPotObj.Items[i];
                keyData = searchKey(d.potbuffkey);
                keyCode = keyData.keycode;
                castTime = keyData.keytime;

                if (keyCode != 0)
                    PostMessage(hWnd, 0x0100, keyCode, 0);        // キー
                Thread.Sleep((int)(castTime * 1000));
                Thread.Sleep((int)(dBasicWait * 1000));                // 基本wait
            }
            return true;
        }

        private _key searchKey(int keyno)
        {
            _key keydata;

            for (int i = 0; i < data.dataObj.Items.Count; i++)
            {
                keydata = (_key)data.dataObj.Items[i];
                if (keyno == keydata.keyno)
                    return keydata;
            }
            return null;
        }
        /// <summary>
        /// 編集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            Settings frm;
            this.TopMost = false;
            frm = new Settings(objName, data);
            frm.StartPosition = FormStartPosition.CenterParent;
            if ( frm.ShowDialog() == DialogResult.OK )
            {
                macroName[cmbMacroName.SelectedIndex] = frm.objName;
                // マクロ名保存
                saveMacroName();
                int index = cmbMacroName.SelectedIndex;
                loadMacroName();
                cmbMacroName.SelectedIndex = index;
                objName = cmbMacroName.SelectedItem.ToString();
            }
            this.TopMost = bTopMust; ;
        }

        /// <summary>
        /// マクロ名ロード
        /// </summary>
        public void loadMacroName()
        {
            try
            { 
                //XmlSerializerオブジェクトを作成
                System.Xml.Serialization.XmlSerializer serializer =
                        new System.Xml.Serialization.XmlSerializer(typeof(List<string>));
                //読み込むファイルを開く
                string fname = @".\macroName.xml";
                using (System.IO.StreamReader sr = new System.IO.StreamReader(
                    fname, new System.Text.UTF8Encoding(false)))
                {
                    //XMLファイルから読み込み、逆シリアル化する
                    macroName = (List<string>)serializer.Deserialize(sr);
                }
                cmbMacroName.Items.Clear();
                for( int i=0; i<macroName.Count(); i++)
                {
                    cmbMacroName.Items.Add(macroName[i]);
                }
            }
            catch(Exception ex)
            {
                File.Create(@".\macroName.xml").Close();
                //MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// マクロ名ロード※特定フォルダ
        /// </summary>
        public string [] loadFolderMacroName(string folderName)
        {
            List<string> mName;
            try
            {
                //XmlSerializerオブジェクトを作成
                System.Xml.Serialization.XmlSerializer serializer =
                        new System.Xml.Serialization.XmlSerializer(typeof(List<string>));
                //読み込むファイルを開く
                string fname = folderName + @"\macroName.xml";
                using (System.IO.StreamReader sr = new System.IO.StreamReader(
                    fname, new System.Text.UTF8Encoding(false)))
                {
                    //XMLファイルから読み込み、逆シリアル化する
                    mName = (List<string>)serializer.Deserialize(sr);
                    string[] ret = mName.ToArray();
                    return ret;
                }
            }
            catch (Exception ex)
            {
                //File.Create(@".\macroName.xml").Close();
                //MessageBox.Show(ex.Message);
            }
            return null;
        }
        /// <summary>
        /// マクロ名セーブ
        /// </summary>
        public void saveMacroName()
        {
            try
            {
                // オブジェクト名＝マクロ名
                //XMLファイルに保存する
                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(
                        typeof(List<string>));
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(
                    @".\macroName.xml", false, new System.Text.UTF8Encoding(false)))
                {
                    serializer.Serialize(sw, macroName);
                }
            }
            catch (Exception ex)
            {
                //File.Create(@".\macroName.xml").Close();
                MessageBox.Show(ex.Message);
                putErrLog(ex.Message);
                putErrLog(ex.StackTrace);
            }
        }
        /// <summary>
        /// マクロ選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbMacroName_SelectedIndexChanged(object sender, EventArgs e)
        {

            objName = cmbMacroName.SelectedItem.ToString();

            // データオブジェクト＆ロード
            data = new Data(objName);

        }
        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.TopMost = false;

            macroName frm = new macroName("New");
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                objName = frm.sMacroName;
                // マクロ追加
                macroName.Add(objName);
                // マクロ名保存
                saveMacroName();

                data = new Data(objName);
                data.saveData(objName);

                int index = cmbMacroName.SelectedIndex;
                loadMacroName();
                cmbMacroName.SelectedIndex = cmbMacroName.Items.Count - 1;
                objName = cmbMacroName.SelectedItem.ToString();
            }

            //Settings frm;
            //this.TopMost = false;

            //data = new Data("New");
            //frm = new Settings("New", data);
            //frm.bNewFlg = true;

            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    // マクロ追加
            //    macroName.Add(frm.objName);
            //    // マクロ名保存
            //    saveMacroName();
            //    // マクロ保存
            //    data.saveData(objName);
            //    int index = cmbMacroName.SelectedIndex;
            //    loadMacroName();
            //    cmbMacroName.SelectedIndex = cmbMacroName.Items.Count - 1;
            //    objName = cmbMacroName.SelectedItem.ToString();
            //    data.loadData(objName);
            //}

            this.TopMost = bTopMust; ;
        }
        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(cmbMacroName.SelectedItem.ToString() + "を削除します。", "確認",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                macroName.RemoveAt(cmbMacroName.SelectedIndex);
                string fname = cmbMacroName.SelectedItem.ToString() + "*.xml";
                foreach (string pathFrom in System.IO.Directory.EnumerateFiles(@".\", fname, System.IO.SearchOption.AllDirectories))
                {
                    //１ファイルの削除実行。
                    System.IO.File.Delete(pathFrom);
                }
                // マクロ名保存
                saveMacroName();
                // マクロ名読み込み
                loadMacroName();
                cmbMacroName.SelectedIndex = 0;
                objName = cmbMacroName.SelectedItem.ToString();
            }
        }
        /// <summary>
        /// 基本設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBaseSttings_Click(object sender, EventArgs e)
        {
            BasicSettings frm;
            this.TopMost = false;

            frm = new BasicSettings();
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bTopMust = frm.bTopMost;
                dBasicWait = frm.dBasicWait;
                bLog = frm.bLog;
            }
            this.TopMost = bTopMust;

        }

        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="mess"></param>
        private void putLog(string mess)
        {
            DateTime dt = DateTime.Now;

            if (bLog == false) return;

            try
            {
                // テキストファイル出力（追記出力）
                using (StreamWriter sw = new StreamWriter(@".\trace.log", true))
                {
                    sw.WriteLine(dt.ToString() + ":" + mess);
                }
            }
            // 例外処理
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                putErrLog(ex.Message);
                putErrLog(ex.StackTrace);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        public void putErrLog(string ms)
        {
            DateTime dt = DateTime.Now;

            try
            {
                // テキストファイル出力（追記出力）
                using (StreamWriter sw = new StreamWriter(@".\Error.log", true))
                {
                    sw.WriteLine(dt.ToString() + ":" + ms);
                }
            }
            // 例外処理
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        // 複製
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            macroName frm = new macroName(objName + " Copy");
            frm.StartPosition = FormStartPosition.CenterParent;
            if ( frm.ShowDialog() == DialogResult.OK)
            { 
                objName = frm.sMacroName;
                // マクロ追加
                macroName.Add(objName);
                // マクロ名保存
                saveMacroName();

                data.saveData(objName);

                int index = cmbMacroName.SelectedIndex;
                loadMacroName();
                cmbMacroName.SelectedIndex = cmbMacroName.Items.Count - 1;
                objName = cmbMacroName.SelectedItem.ToString();
            }
            this.TopMost = bTopMust;
        }
        /// <summary>
        /// マクロインポート
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInport_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            // フォルダ選択
            // FolderBrowserDialogクラスのインスタンスを作成
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            //上部に表示する説明テキストを指定する
            fbd.Description = "インポートマクロのフォルダを指定してください。";
            //ルートフォルダを指定する
            //デフォルトでDesktop
            //fbd.RootFolder = Environment.SpecialFolder.Desktop;
            //最初に選択するフォルダを指定する
            //RootFolder以下にあるフォルダである必要がある
            //fbd.SelectedPath = @"C:\Windows";
            //ユーザーが新しいフォルダを作成できるようにする
            //デフォルトでTrue
            fbd.ShowNewFolderButton = true;

            //ダイアログを表示する
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                //選択されたフォルダ
                string [] macroStr = loadFolderMacroName(fbd.SelectedPath);
                if (macroStr == null || macroStr.Count() <= 0)
                {
                    MessageBox.Show("SBManchaMacro用のマクロが見つかりませんでした。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                selectMacro macroForm = new selectMacro(objName, data);
                macroForm.macroName = macroStr;
                if( macroForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    { 
                        string name = macroForm.selectMacroName;
                        string folder = fbd.SelectedPath;
                        string fName;

                        // マクロ名重複確認
                        bool flg = false;
                        for( int i=0; i< macroName.Count; i++ )
                        {
                            if( name.CompareTo(macroName[i]) == 0 )
                            {
                                flg = true;
                                break;
                            }
                        }
                        if( flg )
                        {
                            MessageBox.Show("同一のマクロ名が存在します。", "エラー,", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if( MessageBox.Show(name + " マクロをインポートします。宜しいですか？", "インポート", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return;
                        }
                        // マクロファイルコピー
                        fName = name + "key.xml";
                        File.Copy(folder + @"\" + fName, @".\" + fName);
                        fName = name + "iBuff.xml";
                        File.Copy(folder + @"\" + fName, @".\" + fName);
                        fName = name + "iBuff2.xml";
                        File.Copy(folder + @"\" + fName, @".\" + fName);
                        fName = name + "Main.xml";
                        File.Copy(folder + @"\" + fName, @".\" + fName);
                        fName = name + "etcBuff.xml";
                        File.Copy(folder + @"\" + fName, @".\" + fName);
                        fName = name + "potBuff.xml";
                        File.Copy(folder + @"\" + fName, @".\" + fName);
                        // マクロ名追加
                        objName = name;
                        // マクロ追加
                        macroName.Add(objName);
                        // マクロ名保存
                        saveMacroName();

                        data = new Data(objName);
                        data.loadData(objName);
                        //data.saveData(objName);

                        int index = cmbMacroName.SelectedIndex;
                        loadMacroName();
                        cmbMacroName.SelectedIndex = cmbMacroName.Items.Count - 1;
                        objName = cmbMacroName.SelectedItem.ToString();

                        // 右クリックメニューにマクロ名追加
                        ToolStripMenuItem tsiLeft = new ToolStripMenuItem();
                        tsiLeft.Text = name;
                        tsiLeft.ToolTipText = name + " 実行";
                        this.contextMenuStrip1.Items.Add(tsiLeft);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        putErrLog(ex.Message);
                        putErrLog(ex.StackTrace);
                    }
                }

            }
            //macroName frm = new macroName(objName + "Copy");
            //frm.StartPosition = FormStartPosition.CenterParent;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    objName = frm.sMacroName;
            //    // マクロ追加
            //    macroName.Add(objName);
            //    // マクロ名保存
            //    saveMacroName();

            //    data.saveData(objName);

            //    int index = cmbMacroName.SelectedIndex;
            //    loadMacroName();
            //    cmbMacroName.SelectedIndex = cmbMacroName.Items.Count - 1;
            //    objName = cmbMacroName.SelectedItem.ToString();
            //}
            this.TopMost = bTopMust;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            // 最小化
            if (this.WindowState == FormWindowState.Minimized)
            {
                //this.WindowState = FormWindowState.Normal;
                //this.ShowInTaskbar = false;
                //this.Visible = false;
                //フォームを非表示
                this.Visible = false;                //タスクトレイにアイコンを表示
                notifyIcon1.Visible = true;

                if (threadFlg == true)
                {
                    this.contextMenuStrip1.Items[1].Visible = true;
                    // メニュー disable
                    for (int j = 0; j < macroName.Count; j++)
                    {
                        if (this.contextMenuStrip1.Items.Count > j + 5)
                           this.contextMenuStrip1.Items[j + 5].Visible = false;
                    }
                }
                // メニュー enable
                else
                {
                    this.contextMenuStrip1.Items[1].Visible = false;
                    for (int j = 0; j < macroName.Count; j++)
                    {
                        if (this.contextMenuStrip1.Items.Count > j + 5)
                            this.contextMenuStrip1.Items[j + 5].Visible = true;
                    }
                }
            }
        }
        /// <summary>
        /// 格納アイコンダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            //フォームを表示
            this.Visible = true;

            //最小化状態を解除
            this.WindowState = FormWindowState.Normal;

            //タスクトレイのアイコン表示を無効
            notifyIcon1.Visible = false;
            //this.Visible = true;
            //this.ShowInTaskbar = true;
            //notifyIcon1.Icon = null;
        }
        /// <summary>
        /// 右メニュー終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;    //アイコンをトレイから取り除く
            this.Close();
        }
        /// <summary>
        /// 右メニュー表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 表示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //フォームを表示
            this.Visible = true;

            //最小化状態を解除
            this.WindowState = FormWindowState.Normal;

            //タスクトレイのアイコン表示を無効
            notifyIcon1.Visible = false;
            //this.Visible = true;
            //this.ShowInTaskbar = true;
            //notifyIcon1.Icon = null;
        }
        /// <summary>
        /// 右メニューアイテムクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //ToolStripItem mi = (ToolStripItem)sender;
            //ToolStripMenuItem mi = (ToolStripMenuItem)sender;

            //e.ClickedItem.Name;
            for( int i=0; i<macroName.Count; i++)
            {
                if(threadFlg == false)
                { 
                    if (e.ClickedItem.Text == macroName[i])
                    {
                        //// メニュー disable
                        for (int j = 0; j < macroName.Count; j++)
                        {
                            if (this.contextMenuStrip1.Items.Count > j + 5)
                                this.contextMenuStrip1.Items[j + 5].Visible = false;
                        }
                        objName = macroName[i];
                        // データオブジェクト＆ロード
                        data = new Data(objName);

                        //----------------------------------------
                        // 右クリックメニュー
                        //----------------------------------------
                        this.contextMenuStrip1.Items[1].Visible = true;

                        cmbMacroName.SelectedIndex = i;

                        MacroStartButton();
                        //MessageBox.Show(e.ClickedItem.Text);
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// 右メニューマクロ停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void マクロ停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.contextMenuStrip1.Items[1].Visible = false;
            //// メニュー enable
            //for (int j = 0; j < macroName.Count; j++)
            //    this.contextMenuStrip1.Items[j + 5].Enabled = true;
            MacroStartButton();
        }

    }
}
