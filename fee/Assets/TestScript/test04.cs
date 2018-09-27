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

	テキスト。セーブ。
	テキスト。ロード。
	ＰＮＧ。セーブ。
	ＰＮＧ、ロード。

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

	/** Start
	*/
	private void Start()
	{
		//タスク。インスタンス作成。
		NTaskW.TaskW.CreateInstance();

		//パフォーマンスカウンター。インスタンス作成。
		NPerformanceCounter.PerformanceCounter.CreateInstance();

		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//セーブロード。インスタンス作成。
		NSaveLoad.Config.LOG_ENABLE = true;
		NSaveLoad.SaveLoad.CreateInstance();

		//deleter
		this.deleter = new NDeleter.Deleter();

		//step
		this.step = Step.Start;

		//saveload_item
		this.saveload_item = null;

		//sprite
		int t_layerindex = 0;
		long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;
		this.sprite = new NRender2D.Sprite2D(this.deleter,null,t_drawpriority);
		this.sprite.SetRect(100,100,64,64);
		this.sprite.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
		this.sprite.SetTexture(Texture2D.whiteTexture);
		this.sprite.SetMaterialType(NRender2D.Config.MaterialType.Alpha);
	}

	/** Update
	*/
	private void Update()
	{
		//セーブロード。
		NSaveLoad.SaveLoad.GetInstance().Main();

		switch(this.step){
		case Step.Start:
			{
				this.step = Step.SaveBinaryStart;
			}break;
		case Step.SaveBinaryStart:
			{
				//ファイル名。
				string t_filename = "test_binary.bin";

				//データ。
				byte[] t_binary = new byte[1 * 1024 * 1024];
				for(int ii=0;ii<t_binary.Length;ii++){
					t_binary[ii] = (byte)(ii % 256);
				}

				Debug.Log("SaveBinaryStart : " + t_filename + " : size = " + t_binary.Length.ToString());
				this.saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestSaveLocalBinaryFile(t_filename,t_binary);
				this.step = Step.SaveBinaryNow;
			}break;
		case Step.SaveBinaryNow:
			{
				if(this.saveload_item.IsBusy() == true){
					//セーブ中。
					Debug.Log("SaveBinaryNow");
				}else{
					if(this.saveload_item.GetResultDataType() == NSaveLoad.DataType.SaveEnd){
						//成功。
						Debug.Log("SaveBinaryNow : Success");
						this.step = Step.LoadBinaryStart;
					}else{
						//失敗。
						Debug.Log("SaveBinaryNow : Faild");
						this.step = Step.LoadBinaryStart;
					}
				}
			}break;
		case Step.LoadBinaryStart:
			{
				//ファイル名。
				string t_filename = "test_binary.bin";

				Debug.Log("SaveBinaryStart : " + t_filename);
				this.saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestLoadLoaclBinaryFile(t_filename);
				this.step = Step.LoadBinaryNow;
			}break;
		case Step.LoadBinaryNow:
			{
				if(this.saveload_item.IsBusy() == true){
					//セーブ中。
					Debug.Log("LoadBinaryNow");
				}else{
					if(this.saveload_item.GetResultDataType() == NSaveLoad.DataType.Binary){
						//成功。
						Debug.Log("LoadBinaryNow : Success : size = " + this.saveload_item.GetResultBinary().Length.ToString());
						this.step = Step.SaveTextStart;

						//チェック。
						byte[] t_binary = this.saveload_item.GetResultBinary();
						for(int ii=0;ii<t_binary.Length;ii++){
							if(t_binary[ii] != (byte)(ii % 256)){
								Debug.Log("LoadBinaryNow : error");
							}
						}
					}else{
						//失敗。
						Debug.Log("LoadBinaryNow : Faild");
						this.step = Step.SaveTextStart;
					}
				}
			}break;
		case Step.SaveTextStart:
			{
				//ファイル名。
				string t_filename = "test_text.txt";

				//データ。
				string t_text = Random.value.ToString();

				Debug.Log("SaveTextStart : " + t_filename + " : text = " + t_text);
				this.saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestSaveLocalTextFile(t_filename,t_text);
				this.step = Step.SaveTextNow;
			}break;
		case Step.SaveTextNow:
			{
				if(this.saveload_item.IsBusy() == true){
					//セーブ中。
					Debug.Log("SaveTextNow");
				}else{
					if(this.saveload_item.GetResultDataType() == NSaveLoad.DataType.SaveEnd){
						//成功。
						Debug.Log("SaveTextNow : Success");
						this.step = Step.LoadTextStart;
					}else{
						//失敗。
						Debug.Log("SaveTextNow : Faild");
						this.step = Step.LoadTextStart;
					}
				}
			}break;
		case Step.LoadTextStart:
			{
				//ファイル名。
				string t_filename = "test_text.txt";

				Debug.Log("LoadTextStart : " + t_filename);
				this.saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestLoadLoaclTextFile(t_filename);
				this.step = Step.LoadTextNow;
			}break;
		case Step.LoadTextNow:
			{
				if(this.saveload_item.IsBusy() == true){
					//セーブ中。
					Debug.Log("LoadTextNow");
				}else{
					if(this.saveload_item.GetResultDataType() == NSaveLoad.DataType.Text){
						//成功。
						Debug.Log("LoadTextNow : Success : text = " + this.saveload_item.GetResultText());
						this.step = Step.End;
					}else{
						//失敗。
						Debug.Log("LoadTextNow : Faild");
						this.step = Step.End;
					}
				}
			}break;

		case Step.SavePngStart:
		case Step.SavePngNow:
		case Step.LoadPngStart:
		case Step.LoadPngNow:
			{
			}break;

		case Step.End:
			{
			}break;

		#if false
		case 4:
			{
				//ＰＮＧ。セーブ開始。

				Texture2D t_save_texture = new Texture2D(64,64);
				t_save_texture.filterMode = FilterMode.Point;
				t_save_texture.wrapMode = TextureWrapMode.Clamp;
				for(int xx=0;xx<t_save_texture.width;xx++){
					for(int yy=0;yy<t_save_texture.height;yy++){
						t_save_texture.SetPixel(xx,yy,new Color((float)xx / t_save_texture.width,(float)yy / t_save_texture.height,0.0f,(float)xx / t_save_texture.width));
					}
				}
				t_save_texture.Apply();

				this.sprite.SetTexture(t_save_texture);

				Debug.Log("SAVEPNG : START");

				this.saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestSaveLocalPngFile("save.png",t_save_texture);
				this.step++;
			}break;
		case 5:
			{
				//ＰＮＧ。セーブ中。

				if(this.saveload_item != null){
					switch(this.saveload_item.GetDataType()){
					case NSaveLoad.DataType.None:
						{
							//セーブ中。
						}break;
					case NSaveLoad.DataType.SaveEnd:
						{
							//セーブ完了。
							Debug.Log("SAVEPNG : END");
							this.saveload_item = null;
						}break;
					default:
						{
							//不明。
							this.saveload_item = null;
							Debug.Log("SAVEPNG : ERROR");
						}break;
					}
				}else{
					this.step++;
				}
			}break;
		case 6:
			{
				//ＰＮＧ。ロード開始。

				this.saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestLoadLocalPngFile("save.png");
				this.step++;

			}break;
		case 7:
			{
				//ＰＮＧ。ロード中。

				if(this.saveload_item != null){
					switch(this.saveload_item.GetDataType()){
					case NSaveLoad.DataType.None:
						{
							//ロード中。
						}break;
					case NSaveLoad.DataType.Texture:
						{
							//ロード完了。
							Texture2D t_load_texture = this.saveload_item.GetResultTexture();
							t_load_texture.filterMode = FilterMode.Point;
							t_load_texture.wrapMode = TextureWrapMode.Clamp;

							this.sprite.SetTexture(t_load_texture);

							this.saveload_item = null;

							Debug.Log("LOADPNG : END");
						}break;
					default:
						{
							Debug.Log("LOADPNG : ERROR");
						}break;
					}
				}else{
					this.step++;
				}
			}break;
		#endif
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

