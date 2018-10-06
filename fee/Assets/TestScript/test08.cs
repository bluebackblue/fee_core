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
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** Start
	*/
	private void Start()
	{
		//タスク。インスタンス作成。
		NTaskW.TaskW.CreateInstance();

		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//パフォーマンスカウンター。インスタンス作成。
		NPerformanceCounter.Config.LOG_ENABLE = true;
		NPerformanceCounter.PerformanceCounter.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Config.LOG_ENABLE = true;
		NInput.Mouse.CreateInstance();

		//イベントプレート。
		NEventPlate.Config.LOG_ENABLE = true;
		NEventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		NUi.Config.LOG_ENABLE = true;
		NUi.Ui.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(NRender2D.Render2D.MAX_LAYER - 1) * NRender2D.Render2D.DRAWPRIORITY_STEP);

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

