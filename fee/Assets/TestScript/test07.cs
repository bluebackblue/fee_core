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


/** test07
*/
public class test07 : main_base
{
	/** Step
	*/
	private enum Step
	{
		None,

		EncryptPublicKey_Start,
		EncryptPublicKey_Do,

		DecryptPrivateKey_Start,
		DecryptPrivateKey_Do,
	};

	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** step
	*/
	private Step step;

	/** button
	*/
	private NUi.Button button;

	/** text
	*/
	private NRender2D.Text2D text;

	/** crypt_item
	*/
	private NCrypt.Item crypt_item;

	/** private_key
	*/
	private string private_key;

	/** public_key
	*/
	private string public_key;

	/** plane_binary
	*/
	private byte[] plane_binary;

	/** encrypt_binary
	*/
	private byte[] encrypt_binary;

	/** Start
	*/
	private void Start()
	{
		//タスク。インスタンス作成。
		NTaskW.TaskW.CreateInstance();

		//パフォーマンスカウンター。インスタンス作成。
		NPerformanceCounter.Config.LOG_ENABLE = true;
		NPerformanceCounter.PerformanceCounter.CreateInstance();

		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//イベントプレート。インスタンス作成。
		NEventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		//NUi.Config.LOG_ENABLE = true;
		NUi.Ui.CreateInstance();

		//暗号。インスタンス作成。
		NCrypt.Config.LOG_ENABLE = true;
		NCrypt.Crypt.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(NRender2D.Render2D.MAX_LAYER - 1) * NRender2D.Render2D.DRAWPRIORITY_STEP);

		//step
		this.step = Step.None;

		//button
		this.button = new NUi.Button(this.deleter,null,0,this.CallBack_Click,-1);
		this.button.SetTexture(Resources.Load<Texture2D>("button"));
		this.button.SetRect(100,100,50,50);
		this.button.SetText("開始");

		//text
		this.text = new NRender2D.Text2D(this.deleter,null,0);
		this.text.SetRect(100,50,0,0);
		this.text.SetText("---");

		//crypt_item
		this.crypt_item = null;

		//key
		this.private_key = null;
		this.public_key = null;

		//binary
		this.plane_binary = null;
		this.encrypt_binary = null;
	}

	/** [Button_Base]コールバック。クリック。
	*/
	private void CallBack_Click(int a_id)
	{
		if(this.step == Step.None){

			//public
			NJsonItem.JsonItem t_item_public = new NJsonItem.JsonItem(Resources.Load<TextAsset>("public_key").text);
			this.public_key = null;
			if(t_item_public != null){
				if(t_item_public.IsAssociativeArray() == true){
					if(t_item_public.IsExistItem("public",NJsonItem.ValueType.StringData) == true){
						this.public_key = t_item_public.GetItem("public").GetStringData();
					}
				}
			}

			//private
			NJsonItem.JsonItem t_item_private = new NJsonItem.JsonItem(Resources.Load<TextAsset>("private_key").text);
			this.private_key = null;
			if(t_item_private != null){
				if(t_item_private.IsAssociativeArray() == true){
					if(t_item_private.IsExistItem("private",NJsonItem.ValueType.StringData) == true){
						this.private_key = t_item_private.GetItem("private").GetStringData();
					}
				}
			}

			this.step = Step.EncryptPublicKey_Start;
		}
	}

	/** FixedUpdate
	*/
	private void FixedUpdate()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//イベントプレート。
		NEventPlate.EventPlate.GetInstance().Main(NInput.Mouse.GetInstance().pos.x,NInput.Mouse.GetInstance().pos.y);

		//ＵＩ。
		NUi.Ui.GetInstance().Main();

		//暗号。
		NCrypt.Crypt.GetInstance().Main();

		//ステップ。
		switch(this.step){
		case Step.EncryptPublicKey_Start:
			{
				this.plane_binary = new byte[15];
				for(int ii=0;ii<this.plane_binary.Length;ii++){
					this.plane_binary[ii] = (byte)(ii % 256);
				}

				//暗号化開始。
				this.crypt_item = NCrypt.Crypt.GetInstance().RequestEncryptPublicKey(this.plane_binary,this.public_key);

				this.step = Step.EncryptPublicKey_Do;
			}break;
		case Step.EncryptPublicKey_Do:
			{
				if(this.crypt_item.IsBusy() == true){
					//暗号化中。
					this.text.SetText(this.step.ToString());
				}else{
					if(this.crypt_item.GetResultType() == NCrypt.Item.ResultType.Binary){
						//成功。
						byte[] t_binary = this.crypt_item.GetResultBinary();
						this.text.SetText(this.step.ToString() + " : Success");

						this.encrypt_binary = t_binary;
						this.crypt_item = null;
						this.step = Step.DecryptPrivateKey_Start;
					}else{
						//失敗。
						this.encrypt_binary = null;
						this.text.SetText(this.step.ToString() + " : Failed");

						this.crypt_item = null;
						this.step = Step.None;
					}
				}
			}break;
		case Step.DecryptPrivateKey_Start:
			{
				//複合化開始。
				this.crypt_item = NCrypt.Crypt.GetInstance().RequestDecryptPrivateKey(this.encrypt_binary,this.private_key);

				this.step = Step.DecryptPrivateKey_Do;
			}break;
		case Step.DecryptPrivateKey_Do:
			{
				if(this.crypt_item.IsBusy() == true){
					//暗号化中。
					this.text.SetText(this.step.ToString());
				}else{
					if(this.crypt_item.GetResultType() == NCrypt.Item.ResultType.Binary){
						//成功。
						byte[] t_binary = this.crypt_item.GetResultBinary();
						this.text.SetText(this.step.ToString() + " : Success");

						this.crypt_item = null;
						this.step = Step.None;
					}else{
						//失敗。
						this.encrypt_binary = null;
						this.text.SetText(this.step.ToString() + " : Failed");

						this.crypt_item = null;
						this.step = Step.None;
					}
				}
			}break;
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
		this.deleter.DeleteAll();
	}

	/** ＪＳＯＮ保存。
	*/
	#if(UNITY_EDITOR)
	private static void SaveJson(NJsonItem.JsonItem a_jsonitem,string a_full_path)
	{
		string t_json_string = a_jsonitem.ConvertJsonString();

		System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(a_full_path);
		System.IO.StreamWriter t_filestream_write = null;

		try{
			//open
			t_filestream_write = t_fileinfo.CreateText();

			//write
			if(t_filestream_write != null){
				t_filestream_write.Write(t_json_string);
				t_filestream_write.Flush();
			}
		}catch(System.Exception){
			Debug.Assert(false);
		}

		//close
		if(t_filestream_write != null){
			t_filestream_write.Close();
			t_filestream_write = null;
		}
	}
	#endif

	/** 公開鍵秘密鍵作成。
	*/
	#if(UNITY_EDITOR)
	[UnityEditor.MenuItem("Test/Test07/MakePublicKeyPrivateKey")]
	private static void MakePublicKeyPrivateKey()
	{
		string t_public_key;
		string t_private_key;
		if(NCrypt.Crypt.CreateNewKey(out t_public_key,out t_private_key) == true){

			//public
			{
				NJsonItem.JsonItem t_jsonitem = new NJsonItem.JsonItem(new NJsonItem.Value_AssociativeArray());
				NJsonItem.JsonItem t_jsonitem_public = new NJsonItem.JsonItem(new NJsonItem.Value_StringData(t_public_key));
				t_jsonitem.SetItem("public",t_jsonitem_public,false);

				SaveJson(t_jsonitem,Application.dataPath + "/Resources/public_key.json");
			}

			//private
			{
				NJsonItem.JsonItem t_jsonitem = new NJsonItem.JsonItem(new NJsonItem.Value_AssociativeArray());
				NJsonItem.JsonItem t_jsonitem_private = new NJsonItem.JsonItem(new NJsonItem.Value_StringData(t_private_key));
				t_jsonitem.SetItem("private",t_jsonitem_private,false);

				SaveJson(t_jsonitem,Application.dataPath + "/Resources/private_key.json");
			}
			
			UnityEditor.AssetDatabase.Refresh();
		}
	}
	#endif
}

