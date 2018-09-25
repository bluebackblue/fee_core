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

		/** [シングルトン]インスタンス。チェック。
		*/
		public static bool IsCreateInstance()
		{
			if(s_instance != null){
				return true;
			}
			return false;
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static Ui GetInstance()
		{
			#if(UNITY_EDITOR)
			if(s_instance == null){
				Tool.Assert(false);
			}
			#endif

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

		//追加リスト。
		private List<OnTargetCallBack_Base> add_list;

		//削除リスト。
		private List<OnTargetCallBack_Base> remove_list;

		/** [シングルトン]constructor
		*/
		private Ui()
		{
			//target_list
			this.target_list = new List<OnTargetCallBack_Base>();

			//add_list
			this.add_list = new List<OnTargetCallBack_Base>();

			//remove_list
			this.remove_list = new List<OnTargetCallBack_Base>();
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
				//追加。
				for(int ii=0;ii<this.add_list.Count;ii++){
					this.target_list.Add(this.add_list[ii]);
				}
				this.add_list.Clear();

				//削除。
				for(int ii=0;ii<this.remove_list.Count;ii++){
					this.target_list.Remove(this.remove_list[ii]);
				}
				this.remove_list.Clear();

				//呼び出し。
				for(int ii=0;ii<this.target_list.Count;ii++){
					this.target_list[ii].OnTarget();
				}
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}

		/** 追加リクエスト。設定。
		*/
		public void SetTargetRequest(OnTargetCallBack_Base a_item)
		{
			this.add_list.Add(a_item);
			this.remove_list.Remove(a_item);
		}

		/** 削除リクエスト。解除。
		*/
		public void UnSetTargetRequest(OnTargetCallBack_Base a_item)
		{
			this.remove_list.Add(a_item);
			this.add_list.Remove(a_item);
		}
	}
}

