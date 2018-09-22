using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief テスト。
*/


/** test02

	セーブロード。

	オブジェクトをＪＳＯＮにコンバート。

	ＪＳＯＮをオブジェクトにコンバート。

*/
public class test02 : main_base
{
	/** SaveData
	*/
	private class SaveData
	{
		public int data_int;
		public float data_float;
		public bool data_bool;
		public Dictionary<string,int> data_dictionary;
		public List<int> data_list;
	}

	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** ボタン。
	*/
	private NUi.Button button_save1;
	private NUi.Button button_save2;
	private NUi.Button button_load1;
	private NUi.Button button_load2;
	private NUi.Button button_random;

	/** ステータス。
	*/
	private NRender2D.Text2D status;

	/** セーブロード処理。
	*/
	private NSaveLoad.Item save_item;
	private NSaveLoad.Item load_item;

	/** セーブデータ。
	*/
	private SaveData savedata = null;

	/** Start
	*/
	private void Start()
	{
		//パフォーマンスカウンター。インスタンス作成。
		NPerformanceCounter.PerformanceCounter.CreateInstance();

		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//ＵＩ。インスタンス作成。
		NUi.Ui.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//イベントプレート。インスタンス作成。
		NEventPlate.EventPlate.CreateInstance();

		//セーブロード。インスタンス作成。
		NSaveLoad.SaveLoad.CreateInstance();

		//ボタン。
		{
			this.button_save1 = new NUi.Button(this.deleter,null,0,Click_Save,1);
			this.button_save1.SetTexture(Resources.Load<Texture2D>("button"));
			this.button_save1.SetRect(100 + 110 * 0,100,100,50);
			this.button_save1.SetText("Save1");

			this.button_save2 = new NUi.Button(this.deleter,null,0,Click_Save,2);
			this.button_save2.SetTexture(Resources.Load<Texture2D>("button"));
			this.button_save2.SetRect(100 + 110 * 1,100,100,50);
			this.button_save2.SetText("Save2");

			this.button_load1 = new NUi.Button(this.deleter,null,0,Click_Load,1);
			this.button_load1.SetTexture(Resources.Load<Texture2D>("button"));
			this.button_load1.SetRect(100 + 110 * 2,100,100,50);
			this.button_load1.SetText("Load1");

			this.button_load2 = new NUi.Button(this.deleter,null,0,Click_Load,2);
			this.button_load2.SetTexture(Resources.Load<Texture2D>("button"));
			this.button_load2.SetRect(100 + 110 * 3,100,100,50);
			this.button_load2.SetText("Load2");

			this.button_random = new NUi.Button(this.deleter,null,0,Click_Random,-1);
			this.button_random.SetTexture(Resources.Load<Texture2D>("button"));
			this.button_random.SetRect(600,100,100,50);
			this.button_random.SetText("Random");
		}

		//ステータス。
		{
			this.status = new NRender2D.Text2D(this.deleter,null,0);
			this.status.SetRect(100,200,0,0);
			this.status.SetText("");
		}

		//セーブロード処理。
		this.save_item = null;
		this.load_item = null;

		//セーブデータ。
		this.savedata = new SaveData();
	}

	/** クリック。
	*/
	public void Click_Save(int a_value)
	{
		//オブジェクトをＪＳＯＮ化。
		NJsonItem.JsonItem t_jsonitem = NJsonItem.ToJson.Convert(this.savedata);

		//ＪＳＯＮを文字列化。
		string t_jsonstring = t_jsonitem.ConvertJsonString();

		//ローカルセーブリクエスト。
		this.save_item = NSaveLoad.SaveLoad.GetInstance().RequestSaveLocalTextFile("save_" + a_value.ToString() + ".json",t_jsonstring);
		if(this.save_item != null){
			this.button_save1.SetLock(true);
			this.button_save2.SetLock(true);
			this.button_load1.SetLock(true);
			this.button_load2.SetLock(true);
			this.button_random.SetLock(true);
		}
	}

	/** クリック。
	*/
	public void Click_Load(int a_value)
	{
		//ローカルロードリクエスト。
		this.load_item = NSaveLoad.SaveLoad.GetInstance().RequestLoadLoaclTextFile("save_" + a_value.ToString() + ".json");
		if(this.load_item != null){
			this.button_save1.SetLock(true);
			this.button_save2.SetLock(true);
			this.button_load1.SetLock(true);
			this.button_load2.SetLock(true);
			this.button_random.SetLock(true);
		}
	}

	/** クリック。
	*/
	public void Click_Random(int a_value)
	{
		this.savedata.data_int = Random.Range(0,9999);
		this.savedata.data_float = Random.value;
		this.savedata.data_bool = (Random.value > 0.5f ? true : false);
		this.savedata.data_dictionary = new Dictionary<string,int>();
		this.savedata.data_dictionary.Add("a",Random.Range(0,9999));
		this.savedata.data_dictionary.Add("b",Random.Range(0,9999));
		this.savedata.data_list = new List<int>();
		this.savedata.data_list.Add(Random.Range(0,9999));
		this.savedata.data_list.Add(Random.Range(0,9999));

		this.SetStatus("Random",this.savedata);

	}

	/** ステータス表示。
	*/
	private void SetStatus(string a_message,SaveData a_data)
	{
		string t_text = "";

		t_text += a_message + "\n";

		if(a_data != null){
			t_text += "data_int = " + a_data.data_int.ToString() + "\n";

			t_text += "data_float = " + a_data.data_float.ToString() + "\n";

			t_text += "data_bool = " + a_data.data_bool.ToString() + "\n";

			if(a_data.data_dictionary != null){
				foreach(KeyValuePair<string,int> t_pair in a_data.data_dictionary){
					t_text += t_pair.ToString() + " = " + t_pair.Key.ToString() + "\n";
				}
			}

			if(a_data.data_list != null){
				for(int ii=0;ii<a_data.data_list.Count;ii++){
					t_text += "[" + ii.ToString() + "] = " + a_data.data_list[ii].ToString() + "\n";
				}
			}
		}

		this.status.SetText(t_text);
	}

	/** Update
	*/
	private void Update()
	{
		//ＵＩ。
		NUi.Ui.GetInstance().Main();

		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//イベントプレート。
		NEventPlate.EventPlate.GetInstance().Main(NInput.Mouse.GetInstance().pos.x,NInput.Mouse.GetInstance().pos.y);

		//セーブロード。
		NSaveLoad.SaveLoad.GetInstance().Main();

		if(this.load_item != null){
			if(this.load_item.IsBusy() == true){
				//ロード中。
			}else{
				if(this.load_item.GetDataType() != NSaveLoad.DataType.Text){
					//ロード失敗。
					this.SetStatus("Load : Faild",this.savedata);
				}else{
					//ロード成功。

					SaveData t_savedata = null;

					string t_jsonstring = this.load_item.GetResultText();
					if(t_jsonstring != null){
						//文字列をＪＳＯＮ化。
						NJsonItem.JsonItem t_jsonitem = new NJsonItem.JsonItem(t_jsonstring);
						if(t_jsonitem != null){
							//ＪＳＯＮをオブジェクト化。
							t_savedata = NJsonItem.FromJson<SaveData>.Convert(t_jsonitem);
						}
					}

					if(t_savedata != null){
						this.savedata = t_savedata;
						this.SetStatus("Load : Success",this.savedata);
					}else{
						this.SetStatus("Load : Convert Error",this.savedata);
					}
				}

				this.button_save1.SetLock(false);
				this.button_save2.SetLock(false);
				this.button_load1.SetLock(false);
				this.button_load2.SetLock(false);
				this.button_random.SetLock(false);

				this.load_item = null;
			}
		}

		if(this.save_item != null){
			if(this.save_item.IsBusy() == true){
				//セーブ中。
			}else{
				if(this.save_item.GetDataType() == NSaveLoad.DataType.SaveEnd){
					//セーブ成功。
					this.SetStatus("Save : Success",this.savedata);
				}else{
					//セーブ失敗。
					this.SetStatus("Save : Faild",this.savedata);
				}

				this.button_save1.SetLock(false);
				this.button_save2.SetLock(false);
				this.button_load1.SetLock(false);
				this.button_load2.SetLock(false);
				this.button_random.SetLock(false);

				this.save_item = null;
			}
		}
	}

	/** 削除前。
	*/
	public override bool PreDestroy(bool a_first)
	{
		return true;
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
	}
}

