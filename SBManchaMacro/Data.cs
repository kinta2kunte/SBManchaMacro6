using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Collections;

namespace SBManchaMacro
{

#if true
    /// <summary>
    /// キーコードデータクラス
    /// </summary>
    public class _key
    {
        public int keyno { get; set; }
        public string keyname { get; set; }
        public byte keycode { get; set; }
        public double keytime { get; set; }

        public _key()
        {
            keyno = 0;
            keyname = "";
            keycode = 0x0;
            keytime = 0;
        }
        public _key(int no, string name, byte key, double time)
        {
                keyno = no;
                keyname = name;
                keycode = key;
                keytime = time;
        }
    }

    //シリアル化するクラス
    public class KeyCodeData
    {
        //ArrayListに追加される型を指定する
        [System.Xml.Serialization.XmlArrayItem(typeof(_key)),
        System.Xml.Serialization.XmlArrayItem(typeof(string))]
        public System.Collections.ArrayList Items;
    }


    /// <summary>
    /// 初期Buffデータクラス
    /// </summary>
    public class _initBuff
    {
        public int ibuffno { get; set; }
        public int ibuffkey { get; set; }

        public _initBuff()
        {
            ibuffno = 0;
            ibuffkey = 0;
        }
        public _initBuff(int no, int key)
        {
            ibuffno = no;
            ibuffkey = key;
        }
    }
    //シリアル化するクラス
    public class iBuffCodeData
    {
        //ArrayListに追加される型を指定する
        [System.Xml.Serialization.XmlArrayItem(typeof(_initBuff)),
        System.Xml.Serialization.XmlArrayItem(typeof(string))]
        public System.Collections.ArrayList Items;
    }

    /// <summary>
    /// pot buffデータクラス
    /// </summary>
    public class _potBuff
    {
        public int potbuffno { get; set; }
        public int potbuffkey { get; set; }

        public _potBuff()
        {
            potbuffno = 0;
            potbuffkey = 0;
        }
        public _potBuff(int no, int key)
        {
            potbuffno = no;
            potbuffkey = key;
        }
    }
    //シリアル化するクラス
    public class potBuffCodeData
    {
        //ArrayListに追加される型を指定する
        [System.Xml.Serialization.XmlArrayItem(typeof(_potBuff)),
        System.Xml.Serialization.XmlArrayItem(typeof(string))]
        public System.Collections.ArrayList Items;
    }

    /// <summary>
    /// 通常攻撃（メインループ）データクラス
    /// </summary>
    public class _mainLoop
    {
        public int mainno { get; set; }
        public int mainkey { get; set; }

        public _mainLoop()
        {
            mainno = 0;
            mainkey = 0;
        }
        public _mainLoop(int no, int key)
        {
            mainno = no;
            mainkey = key;
        }
    }
    //シリアル化するクラス
    public class mainLoopData
    {
        //ArrayListに追加される型を指定する
        [System.Xml.Serialization.XmlArrayItem(typeof(_mainLoop)),
        System.Xml.Serialization.XmlArrayItem(typeof(string))]
        public System.Collections.ArrayList Items;
    }


    public class Data
    {
        public string dataName;
        public KeyCodeData dataObj;
        public iBuffCodeData dataIBuffObj;
        public mainLoopData dataMainObj;
        public potBuffCodeData dataPotObj;
        public iBuffCodeData dataIBuff2Obj;
        public iBuffCodeData dataIBuff3Obj;

        public Data(string dataName)
        {
            try
            {
                //XMLシリアル化するオブジェクト
                dataObj = new KeyCodeData();
                dataIBuffObj = new iBuffCodeData();
                dataIBuffObj.Items = new System.Collections.ArrayList();
                dataMainObj = new mainLoopData();
                dataMainObj.Items = new System.Collections.ArrayList();
                dataPotObj = new potBuffCodeData();
                dataPotObj.Items = new System.Collections.ArrayList();
                dataIBuff2Obj = new iBuffCodeData();
                dataIBuff2Obj.Items = new System.Collections.ArrayList();
                dataIBuff3Obj = new iBuffCodeData();
                dataIBuff3Obj.Items = new System.Collections.ArrayList();

                //saveData(dataName);
                loadData(dataName);

                //dataObj.Items = new System.Collections.ArrayList();
                //dataObj.Items.Add(new _key(0, "sample", 0x41, 2.0));
                //dataObj.Items.Add(new _key(1, "test", 0x42, 3.0));

                //saveData(dataName);
            }
            catch (Exception) { }
        }

        public void addKey( int num, string name, byte c, double t)
        {
            dataObj.Items.Add(new _key(num, name, c, t));
        }


        /// <summary>
        /// キーコードNO最大値
        /// </summary>
        /// <returns></returns>
        public int getMaxKey()
        {
            int max = 0;
            //ArryList dataObj.Items;
            if (dataObj == null || dataObj.Items == null)
                return 0;
            for( int i=0; i< dataObj.Items.Count; i++)
            { 
                _key data = (_key)dataObj.Items[i];
                if (data.keyno > max)
                    max = data.keyno;
            }
            return max;
        }

        //シリアル化読込
        public void loadData(string name)
        {
            // キーコードデータ
            //XmlSerializerオブジェクトを作成
            System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(KeyCodeData));
            //読み込むファイルを開く
            string fname = @".\" + name + "Key.xml";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(
                fname, new System.Text.UTF8Encoding(false)))
            {
                //XMLファイルから読み込み、逆シリアル化する
                dataObj = (KeyCodeData)serializer.Deserialize(sr);
            }

            // 初期buffデータ
            serializer = new System.Xml.Serialization.XmlSerializer(typeof(iBuffCodeData));
            //読み込むファイルを開く
            fname = @".\" + name + "iBuff.xml";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(
                fname, new System.Text.UTF8Encoding(false)))
            {
                //XMLファイルから読み込み、逆シリアル化する
                dataIBuffObj = (iBuffCodeData)serializer.Deserialize(sr);
            }

            // pot buffデータ
            serializer = new System.Xml.Serialization.XmlSerializer(typeof(potBuffCodeData));
            //読み込むファイルを開く
            fname = @".\" + name + "PotBuff.xml";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(
                fname, new System.Text.UTF8Encoding(false)))
            {
                //XMLファイルから読み込み、逆シリアル化する
                dataPotObj = (potBuffCodeData)serializer.Deserialize(sr);
            }

            // main loopデータ
            serializer = new System.Xml.Serialization.XmlSerializer(typeof(mainLoopData));
            //読み込むファイルを開く
            fname = @".\" + name + "Main.xml";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(
                fname, new System.Text.UTF8Encoding(false)))
            {
                //XMLファイルから読み込み、逆シリアル化する
                dataMainObj = (mainLoopData)serializer.Deserialize(sr);
            }

            // 初期buff2データ
            serializer = new System.Xml.Serialization.XmlSerializer(typeof(iBuffCodeData));
            //読み込むファイルを開く
            fname = @".\" + name + "iBuff2.xml";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(
                fname, new System.Text.UTF8Encoding(false)))
            {
                //XMLファイルから読み込み、逆シリアル化する
                dataIBuff2Obj = (iBuffCodeData)serializer.Deserialize(sr);
            }

            // その他buffデータ
            serializer = new System.Xml.Serialization.XmlSerializer(typeof(iBuffCodeData));
            //読み込むファイルを開く
            fname = @".\" + name + "etcBuff.xml";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(
                fname, new System.Text.UTF8Encoding(false)))
            {
                //XMLファイルから読み込み、逆シリアル化する
                dataIBuff3Obj = (iBuffCodeData)serializer.Deserialize(sr);
            }

        }
        //シリアル化保存
        public void saveData(string name)
        {
            //ArrayListに追加されているオブジェクトを指定してXMLファイルに保存する
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(KeyCodeData));
            string fname = @".\" + name + "Key.xml";
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(
                fname, false, new System.Text.UTF8Encoding(false)))
            //  @".\test.xml", false, new System.Text.UTF8Encoding(false));
            {
                serializer.Serialize(sw, dataObj);
            }

            // 初期buff
            System.Xml.Serialization.XmlSerializer sri = new System.Xml.Serialization.XmlSerializer(typeof(iBuffCodeData));
            fname = @".\" + name + "iBuff.xml";
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(
                fname, false, new System.Text.UTF8Encoding(false)))
            //  @".\test.xml", false, new System.Text.UTF8Encoding(false));
            {
                sri.Serialize(sw, dataIBuffObj);
            }

            // pot buff
            sri = new System.Xml.Serialization.XmlSerializer(typeof(potBuffCodeData));
            fname = @".\" + name + "PotBuff.xml";
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(
                fname, false, new System.Text.UTF8Encoding(false)))
            //  @".\test.xml", false, new System.Text.UTF8Encoding(false));
            {
                sri.Serialize(sw, dataPotObj);
            }

            // main loop buff
            sri = new System.Xml.Serialization.XmlSerializer(typeof(mainLoopData));
            fname = @".\" + name + "Main.xml";
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(
                fname, false, new System.Text.UTF8Encoding(false)))
            //  @".\test.xml", false, new System.Text.UTF8Encoding(false));
            {
                sri.Serialize(sw, dataMainObj);
            }
            // 初期buff2
            sri = new System.Xml.Serialization.XmlSerializer(typeof(iBuffCodeData));
            fname = @".\" + name + "iBuff2.xml";
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(
                fname, false, new System.Text.UTF8Encoding(false)))
            //  @".\test.xml", false, new System.Text.UTF8Encoding(false));
            {
                sri.Serialize(sw, dataIBuff2Obj);
            }
            // その他buff
            sri = new System.Xml.Serialization.XmlSerializer(typeof(iBuffCodeData));
            fname = @".\" + name + "etcBuff.xml";
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(
                fname, false, new System.Text.UTF8Encoding(false)))
            //  @".\test.xml", false, new System.Text.UTF8Encoding(false));
            {
                sri.Serialize(sw, dataIBuff3Obj);
            }
        }
    }


#else
    public class _key
    {
        public int keyno { get; set; }
        public string keyname { get; set; }
        public ushort keycode { get; set; }
        public double keytime { get; set; }
        public _key(int no, string name, ushort key, double time)
        {
            keyno = no;
            keyname = name;
            keycode = key;
            keytime = time;
        }
    }
    //シリアル化するクラス
    public class SampleClass
    {
        //ArrayListに追加される型を指定する
        [System.Xml.Serialization.XmlArrayItem(typeof(_key)),
        System.Xml.Serialization.XmlArrayItem(typeof(string))]
        public System.Collections.ArrayList Items;
    }

    public class Data
    {
        //List<_key> keyList = new List<_key>();
        SampleClass obj = new SampleClass();
        ArrayList keyList = new ArrayList();
        public Data( string dataName)
        {
            obj.Items = new ArrayList();

            obj.Items.Add(new _key(0, "sample", 'H', 2.0));
            obj.Items.Add(new _key(1, "test", 'T', 3.0));

            //keyList.Add(new _key(0, "sample", 'H', 2.0));
            //keyList.Add(new _key(1, "test", 'T', 3.0));
            saveData("testData");
        }
        private bool saveData(string name)
        {
            // ファイルへ保存
            //ArrayListに追加されているオブジェクトの型の配列を作成
            XmlSerializer xs = new XmlSerializer(typeof(SampleClass));
            //var xs = new XmlSerializer(typeof(ArrayList));
            using (var sw = new StreamWriter(name, false, Encoding.UTF8))
            {
                xs.Serialize(sw, obj);
                //xs.Serialize(sw, keyList);
            }
            return true;
        }
    }
#endif
}

