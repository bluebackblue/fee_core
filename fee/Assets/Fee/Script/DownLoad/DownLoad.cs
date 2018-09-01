using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ダウンロード。
*/


/** NDownLoad
*/
namespace NDownLoad
{
	/** DownLoad
	*/
	public class DownLoad : Config
	{
		/** [シングルトン]s_instance
		*/
		private static DownLoad s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new DownLoad();
			}
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static DownLoad GetInstance()
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

		/** www
		*/
		private GameObject www_gameobject;
		private MonoBehaviour_Www www_script;

		/** work_list
		*/
		private List<Work> work_list;

		/** [シングルトン]constructor
		*/
		private DownLoad()
		{
			//www
			{
				this.www_gameobject = new GameObject();
				this.www_gameobject.name = "DownLoad";
				this.www_script = this.www_gameobject.AddComponent<MonoBehaviour_Www>();

				GameObject.DontDestroyOnLoad(this.www_gameobject);
			}

			//work_list
			this.work_list = new List<Work>();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.www_script.DeleteRequest();
		}

		/** www。取得。
		*/
		public MonoBehaviour_Www GetWww()
		{
			return this.www_script;
		}

		/** リクエスト。
		*/
		public Item Request(string a_url)
		{
			Work t_work = new Work(a_url);
			this.work_list.Add(t_work);
			return t_work.GetItem();
		}

		/** 更新。
		*/
		public void Main()
		{
			try{
				if(this.work_list.Count > 0){
					if(this.work_list[0].Main() == true){
						this.work_list.RemoveAt(0);
					}
				}
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}
	}
}

