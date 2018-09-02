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


/** test08

	ディレクトリ探査

*/
public class test08 : main_base
{
	/** Start
	*/
	private void Start()
	{
		NDirectory.Item t_item_root = NDirectory.Directory.GetDirectoryItem(Application.dataPath);

		//ルートのフルパス。
		Debug.Log(t_item_root.GetRoot().GetFullPath());

		Debug.Log("----------");

		//ルート。
		{
			List<NDirectory.Item> t_directory_list = t_item_root.GetDirectoryList();
			for(int ii=0;ii<t_directory_list.Count;ii++){
				Debug.Log(t_directory_list[ii].GetName());
			}
			Debug.Log("----------");
		}

		//ルート => Fee
		NDirectory.Item t_item_root_fee = t_item_root.FindDirectory("Fee");
		{
			if(t_item_root_fee != null){
				List<NDirectory.Item> t_directory_list = t_item_root_fee.GetDirectoryList();
				for(int ii=0;ii<t_directory_list.Count;ii++){
					Debug.Log(t_directory_list[ii].GetName());
				}
				Debug.Log("----------");
			}
		}

		//ルート => Fee => フォント。
		NDirectory.Item t_item_root_fee_font = t_item_root_fee.FindDirectory("Font");
		{
			if(t_item_root_fee_font != null){
				List<NDirectory.Item> t_file_list = t_item_root_fee_font.GetFileList();
				for(int ii=0;ii<t_file_list.Count;ii++){
					Debug.Log(t_file_list[ii].GetName());
				}
				Debug.Log("----------");
			}
		}
	}

	/** Update
	*/
	private void Update()
	{
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
	}
}

