using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。ツール。
*/


/** NUi
*/
namespace NUi
{
	/** Ui
	*/
	public class Ui : Config
	{
		/** [シングルトン]s_instance
		*/
		private static Ui s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Ui();
			}
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static Ui GetInstance()
		{
			return s_instance;			
		}

		/** [シングルトン]インスタンス。削除。
		*/
		public static void DeleteInstance()
		{
			if(s_instance != null){
				s_instance.Delete();
				s_instance = null;
			}
		}

		//ターゲットリスト。
		private List<OnTargetCallBack_Base> target_list;

		/** [シングルトン]constructor
		*/
		private Ui()
		{
			//target_list
			this.target_list = new List<OnTargetCallBack_Base>();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** 更新。
		*/
		public void Main()
		{
			try{
				for(int ii=0;ii<this.target_list.Count;ii++){
					this.target_list[ii].OnTarget();
				}
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}

		/** ターゲット。設定。
		*/
		public void SetTarget(OnTargetCallBack_Base a_item)
		{
			this.target_list.Add(a_item);
		}

		/** ターゲット。解除。
		*/
		public void UnSetTarget(OnTargetCallBack_Base a_item)
		{
			this.target_list.Remove(a_item);
		}
	}
}

