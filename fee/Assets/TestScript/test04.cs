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


/** test04

	バイナリ。セーブ。ロード。
	テキスト。セーブ。ロード。
	ＰＮＧ。セーブ。ロード。

*/
public class test04 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** Step
	*/
	private enum Step
	{
		None,

		Start,

		SaveBinaryStart,
		SaveBinaryNow,
		LoadBinaryStart,
		LoadBinaryNow,

		SaveTextStart,
		SaveTextNow,
		LoadTextStart,
		LoadTextNow,

		SavePngStart,
		SavePngNow,
		LoadPngStart,
		LoadPngNow,

		End,
	}

	/** step
	*/
	private Step step;

	/** セーブロードアイテム。
	*/
	private NSaveLoad.Item saveload_item;

	/** sprite
	*/
	private NRender2D.Sprite2D sprite;

	/** status
	*/
	private NRender2D.Text2D status;

	/** button
	*/
	private NUi.Button button;

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

		//セーブロード。インスタンス作成。
		NSaveLoad.Config.LOG_ENABLE = true;
		NSaveLoad.SaveLoad.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//イベントプレート。インスタンス作成。
		NEventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		NUi.Ui.CreateInstance();
	
		//deleter
		this.deleter = new NDeleter.Deleter();

		//step
		this.step = Step.None;

		//saveload_item
		this.saveload_item = null;

		//drawpriority
		int t_layerindex = 0;
		long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;

		//sprite
		this.sprite = new NRender2D.Sprite2D(this.deleter,null,t_drawpriority);
		this.sprite.SetRect(100,300,64,64);
		this.sprite.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
		this.sprite.SetTexture(Texture2D.whiteTexture);
		this.sprite.SetMaterialType(NRender2D.Config.MaterialType.Alpha);

		//status
		this.status = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
		this.status.SetRect(100,100,0,0);

		//button
		this.button = new NUi.Button(this.deleter,null,t_drawpriority,this.CallBack_Click,0);
		this.button.SetRect(100,150,100,100);
		this.button.SetTexture(Resources.Load<Texture2D>("button"));
		this.button.SetText("Start");
	}

	/** [Button_Base]コールバック。クリック。
	*/
	private void CallBack_Click(int a_id)
	{
		if(this.step == Step.None){
			this.step = Step.Start;
		}
	}

	/** Update
	*/
	private void Update()
	{
		//セーブロード。
		NSaveLoad.SaveLoad.GetInstance().Main();

		//マウス。インスタンス作成。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//イベントプレート。インスタンス作成。
		NEventPlate.EventPlate.GetInstance().Main(NInput.Mouse.GetInstance().pos.x,NInput.Mouse.GetInstance().pos.y);

		//ＵＩ。インスタンス作成。
		NUi.Ui.GetInstance().Main();

		switch(this.step){
		case Step.Start:
			{
				{
					string t_log_text = this.step.ToString();
					this.status.SetText(t_log_text);
					Debug.Log(t_log_text);
				}

				this.step = Step.SaveBinaryStart;
			}break;
		case Step.SaveBinaryStart:
			{
				//ファイル名。
				string t_filename = "test_binary.bin";

				//データ。
				byte[] t_binary = new byte[1 * 1024 * 1024 * 100];
				for(int ii=0;ii<t_binary.Length;ii++){
					t_binary[ii] = (byte)(ii % 256);
				}

				this.saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestSaveLocalBinaryFile(t_filename,t_binary);

				{
					string t_log_text = this.step.ToString() + " : " + t_filename + " : size = " + t_binary.Length.ToString();
					this.status.SetText(t_log_text);
					Debug.Log(t_log_text);
				}

				this.step = Step.SaveBinaryNow;
			}break;
		case Step.SaveBinaryNow:
			{
				if(this.saveload_item.IsBusy() == true){
					//セーブ中。
				
					{
						string t_log_text = this.step.ToString();
						this.status.SetText(t_log_text);
						Debug.Log(t_log_text); 
					}
				}else{
					if(this.saveload_item.GetResultDataType() == NSaveLoad.DataType.SaveEnd){
						//成功。

						{
							string t_log_text = this.step.ToString() + " : Success";
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.LoadBinaryStart;
					}else{
						//失敗。

						{
							string t_log_text = this.step.ToString() + " : Faild";
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.End;
					}
				}
			}break;
		case Step.LoadBinaryStart:
			{
				//ファイル名。
				string t_filename = "test_binary.bin";

				this.saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestLoadLoaclBinaryFile(t_filename);

				{
					string t_log_text = this.step.ToString() + " : " + t_filename;
					this.status.SetText(t_log_text);
					Debug.Log(t_log_text); 
				}

				this.step = Step.LoadBinaryNow;
			}break;
		case Step.LoadBinaryNow:
			{
				if(this.saveload_item.IsBusy() == true){
					//ロード中。
					{
						string t_log_text = this.step.ToString();
						this.status.SetText(t_log_text);
						Debug.Log(t_log_text); 
					}
				}else{
					if(this.saveload_item.GetResultDataType() == NSaveLoad.DataType.Binary){
						//成功。

						//チェック。
						bool t_error = false;
						byte[] t_binary = this.saveload_item.GetResultBinary();

						for(int ii=0;ii<t_binary.Length;ii++){
							if(t_binary[ii] != (byte)(ii % 256)){
								t_error = true;
							}
						}

						{
							string t_log_text = this.step.ToString() + " : Success : size = " +  this.saveload_item.GetResultBinary().Length.ToString() + " error = " + t_error.ToString();
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.SaveTextStart;
					}else{
						//失敗。

						{
							string t_log_text = this.step.ToString() + " : Faild";
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.End;
					}
				}
			}break;
		case Step.SaveTextStart:
			{
				//ファイル名。
				string t_filename = "test_text.txt";

				//データ。
				string t_text = Random.value.ToString();

				this.saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestSaveLocalTextFile(t_filename,t_text);

				{
					string t_log_text = this.step.ToString() + " : " + t_filename + " : text = " + t_text;
					this.status.SetText(t_log_text);
					Debug.Log(t_log_text); 
				}

				this.step = Step.SaveTextNow;
			}break;
		case Step.SaveTextNow:
			{
				if(this.saveload_item.IsBusy() == true){
					//セーブ中。
					{
						string t_log_text = this.step.ToString();
						this.status.SetText(t_log_text);
						Debug.Log(t_log_text); 
					}
				}else{
					if(this.saveload_item.GetResultDataType() == NSaveLoad.DataType.SaveEnd){
						//成功。

						{
							string t_log_text = this.step.ToString() + " : Success";
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.LoadTextStart;
					}else{
						//失敗。

						{
							string t_log_text = this.step.ToString() + " : Faild";
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.End;
					}
				}
			}break;
		case Step.LoadTextStart:
			{
				//ファイル名。
				string t_filename = "test_text.txt";

				this.saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestLoadLoaclTextFile(t_filename);

				{
					string t_log_text = this.step.ToString() + " : " + t_filename;
					this.status.SetText(t_log_text);
					Debug.Log(t_log_text); 
				}

				this.step = Step.LoadTextNow;
			}break;
		case Step.LoadTextNow:
			{
				if(this.saveload_item.IsBusy() == true){
					//ロード中。
					{
						string t_log_text = this.step.ToString();
						this.status.SetText(t_log_text);
						Debug.Log(t_log_text); 
					}
				}else{
					if(this.saveload_item.GetResultDataType() == NSaveLoad.DataType.Text){
						//成功。

						{
							string t_log_text = this.step.ToString() + " : Success : = " + this.saveload_item.GetResultText();
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.SavePngStart;
					}else{
						//失敗。
						
						{
							string t_log_text = this.step.ToString() + " : Faild";
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.End;
					}
				}
			}break;
		case Step.SavePngStart:
			{
				//ファイル名。
				string t_filename = "test_png.png";

				Texture2D t_texture = new Texture2D(64,64);
				{
					t_texture.filterMode = FilterMode.Point;
					t_texture.wrapMode = TextureWrapMode.Clamp;
					for(int xx=0;xx<t_texture.width;xx++){
						for(int yy=0;yy<t_texture.height;yy++){
							t_texture.SetPixel(xx,yy,new Color((float)xx / t_texture.width,(float)yy / t_texture.height,0.0f,(float)xx / t_texture.width));
						}
					}
					t_texture.Apply();
				}

				this.saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestSaveLocalPngFile(t_filename,t_texture);

				{
					string t_log_text = this.step.ToString() + " : " + t_filename;
					this.status.SetText(t_log_text);
					Debug.Log(t_log_text); 
				}

				this.step = Step.SavePngNow;
			}break;
		case Step.SavePngNow:
			{
				if(this.saveload_item.IsBusy() == true){
					//セーブ中。
					{
						string t_log_text = this.step.ToString();
						this.status.SetText(t_log_text);
						Debug.Log(t_log_text); 
					}
				}else{
					if(this.saveload_item.GetResultDataType() == NSaveLoad.DataType.SaveEnd){
						//成功。

						{
							string t_log_text = this.step.ToString() + " : Success";
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.LoadPngStart;
					}else{
						//失敗。

						{
							string t_log_text = this.step.ToString() + " : Faild";
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.End;
					}
				}
			}break;
		case Step.LoadPngStart:
			{
				//ファイル名。
				string t_filename = "test_png.png";

				this.saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestLoadLocalPngFile(t_filename);

				{
					string t_log_text = this.step.ToString() + " : " + t_filename;
					this.status.SetText(t_log_text);
					Debug.Log(t_log_text); 
				}

				this.step = Step.LoadPngNow;
			}break;
		case Step.LoadPngNow:
			{
				if(this.saveload_item.IsBusy() == true){
					//ロード中。
					{
						string t_log_text = this.step.ToString();
						this.status.SetText(t_log_text);
						Debug.Log(t_log_text); 
					}
				}else{
					if(this.saveload_item.GetResultDataType() == NSaveLoad.DataType.Texture){
						//成功。

						Texture2D t_load_texture = this.saveload_item.GetResultTexture();
						{
							t_load_texture.filterMode = FilterMode.Point;
							t_load_texture.wrapMode = TextureWrapMode.Clamp;
						}
						this.sprite.SetTexture(t_load_texture);

						{
							string t_log_text = this.step.ToString() + " : success";
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.End;
					}else{
						//失敗。

						{
							string t_log_text = this.step.ToString() + " : Faild";
							this.status.SetText(t_log_text);
							Debug.Log(t_log_text); 
						}

						this.step = Step.End;
					}
				}
			}break;
		case Step.End:
			{
				this.step = Step.None;
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
}

