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


/** test03

	ダウンロードしたＪＰＧを表示。


*/
public class test03 : main_base
{
	/** 削除管理。
	*/
	NDeleter.Deleter deleter;

	/** スプライト。
	*/
	NRender2D.Sprite2D sprite;

	/** ダウンロードアイテム。
	*/
	NDownLoad.Item download_item;
	
	/** Start
	*/
	private void Start()
	{
		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//ダウンロード。インスタンス作成。
		NDownLoad.DownLoad.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//スプライト。
		int t_layerindex = 0;
		long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;
		this.sprite = new NRender2D.Sprite2D(this.deleter,null,t_drawpriority);
		this.sprite.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
		this.sprite.SetRect(ref NRender2D.Render2D.VIRTUAL_RECT_MAX);
		this.sprite.SetTexture(null);	

		//ダウンロードアイテム。
		this.download_item = NDownLoad.DownLoad.GetInstance().Request("http://bbbproject.sakura.ne.jp/wordpress/wp-content/uploads/2016/06/IMGP8657.jpg");	
	}

	/** Update
	*/
	private void Update()
	{
		NDownLoad.DownLoad.GetInstance().Main();

		if(this.download_item != null){
			switch(this.download_item.GetDataType()){
			case NDownLoad.DataType.None:
				{
					//ダウンロード中。
				}break;
			case NDownLoad.DataType.Texture:
				{
					//ダウンロード完了。
					this.sprite.SetTexture(this.download_item.GetResultTexture());
					this.download_item = null;
				}break;
			case NDownLoad.DataType.Error:
				{
					//ダウンロード失敗。
					this.download_item = null;
				}break;
			default:
				{
					//不明なタイプ。
					this.download_item = null;
				}break;
			}
		}
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
		this.deleter.DeleteAll();
	}
}

