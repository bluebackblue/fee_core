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


/** test12
*/
public class test12 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** bgm_index
	*/
	private int bgm_index;

	/** Start
	*/
	private void Start()
	{
		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//オーディオ。インスタンス作成。
		NAudio.Audio.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//プレハブの読み込み。
		GameObject t_prefab_clippack = Resources.Load<GameObject>("bgm");
		NAudio.ClipPack t_clippack = t_prefab_clippack.GetComponent<NAudio.ClipPack>();

		//ＢＧＭロード。
		NAudio.Audio.GetInstance().LoadBgm(t_clippack);

		//ＢＧＭインデックス。
		this.bgm_index = 0;
	}

	/** Update
	*/
	private void Update()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		if(NInput.Mouse.GetInstance().left.down == true){
			if(this.bgm_index == 0){
				this.bgm_index = 1;
			}else{
				this.bgm_index = 0;
			}
			NAudio.Audio.GetInstance().PlayBgm(this.bgm_index);
		}
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
		this.deleter.DeleteAll();
	}
}

