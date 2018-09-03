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


/** test11
*/
public class test11 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** audiosource
	*/
	private AudioSource audiosource;

	/** audioclip
	*/
	private AudioClip audioclip;

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
		GameObject t_prefab_clippack = Resources.Load<GameObject>("se");
		NAudio.ClipPack t_clippack = t_prefab_clippack.GetComponent<NAudio.ClipPack>();

		//audiosource
		this.audiosource = null;

		//audioclip
		this.audioclip = t_clippack.clip_list[0];
	}

	/** Update
	*/
	private void Update()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		if(NInput.Mouse.GetInstance().left.down == true){

			/*
			if(this.audiosource == null){
				this.audiosource = this.GetComponent<AudioSource>();
			}
			this.audiosource.PlayOneShot(this.audioclip);
			*/

			NAudio.Audio.GetInstance().PlayOneShot(this.audioclip);
		}
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
		this.deleter.DeleteAll();
	}
}

