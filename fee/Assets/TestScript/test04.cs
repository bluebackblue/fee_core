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
	NDeleter.Deleter deleter;

	/** step
	*/
	int step;

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
		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//セーブロード。インスタンス作成。
		NSaveLoad.SaveLoad.CreateInstance();

		//deleter
		this.deleter = new NDeleter.Deleter();

		//step
		this.step = 0;

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
		case 0:
			{
				//テキスト。セーブ開始。

				string t_save_text = Random.value.ToString();
				Debug.Log("SAVETEXT : START : " + t_save_text);
				this.saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestSaveLocalTextFile("save.txt",t_save_text);
				this.step++;
			}break;
		case 1:
			{
				//テキスト。セーブ中。

				if(this.saveload_item != null){
					switch(this.saveload_item.GetDataType()){
					case NSaveLoad.DataType.None:
						{
							//セーブ中。
						}break;
					case NSaveLoad.DataType.SaveEnd:
						{
							//セーブ完了。
							Debug.Log("SAVETEXT : END");
							this.saveload_item = null;
						}break;
					default:
						{
							//不明。
							this.saveload_item = null;
							Debug.Log("SAVETEXT : ERROR");
						}break;
					}
				}else{
					this.step++;
				}
			}break;
		case 2:
			{
				//テキスト。ロード開始。

				this.saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestLoadLoaclTextFile("save.txt");
				this.step++;
			}break;
		case 3:
			{
				//テキスト。ロード中。

				if(this.saveload_item != null){
					switch(this.saveload_item.GetDataType()){
					case NSaveLoad.DataType.None:
						{
							//ロード中。
						}break;
					case NSaveLoad.DataType.Text:
						{
							//ロード完了。
							string t_load_text = this.saveload_item.GetResultText();

							this.saveload_item = null;

							Debug.Log("LOADTEXT : " + t_load_text);
						}break;
					default:
						{
							Debug.Log("LOADTEXT : ERROR");
						}break;
					}
				}else{
					this.step++;
				}
			}break;
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
		}
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
		this.deleter.DeleteAll();
	}
}

