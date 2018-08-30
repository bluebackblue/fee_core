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
*/
public class test04 : main_base
{
	/** step
	*/
	int step;

	/** セーブロードアイテム。
	*/
	private NSaveLoad.Item saveload_item;

	/** Start
	*/
	private void Start()
	{
		//セーブロード。インスタンス作成。
		NSaveLoad.SaveLoad.CreateInstance();

		//step
		this.step = 0;

		//saveload_item
		this.saveload_item = null;
	}

	/** Update
	*/
	private void Update()
	{
		//セーブロード。
		NSaveLoad.SaveLoad.GetInstance();

		switch(this.step){
		case 0:
			{
				//セーブ開始。

				string t_save_text = Random.value.ToString();

				Debug.Log(t_save_text);

				this.saveload_item = NSaveLoad.SaveLoad.GetInstance().SaveLocalTextFileRequest("save.txt",t_save_text);

				this.step++;
			}break;
		case 1:
			{
				if(this.saveload_item != null){
					switch(this.saveload_item.GetDataType()){
					case NSaveLoad.DataType.None:
						{
							//セーブ中。
						}break;
					case NSaveLoad.DataType.SaveEnd:
						{
							//セーブ完了。
							this.saveload_item = null;
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
	}
}

