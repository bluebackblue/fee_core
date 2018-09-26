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
		public static DownLoad GetInstance()
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

		/** webrequest
		*/
		private GameObject webrequest_gameobject;
		private MonoBehaviour_WebRequest webrequest_script;

		/** soundpool
		*/
		private GameObject soundpool_gameobject;
		private MonoBehaviour_SoundPool soundpool_script;

		/** work_list
		*/
		private List<Work> work_list;

		/** add_list
		*/
		private List<Work> add_list;

		/** アセットバンドルリスト。
		*/
		private AssetBundleList assetbundle_list;

		/** [シングルトン]constructor
		*/
		private DownLoad()
		{
			//TODO:ルート。

			//webrequest
			{
				this.webrequest_gameobject = new GameObject();
				this.webrequest_gameobject.name = "DownLoad_WebRequest";
				this.webrequest_script = this.webrequest_gameobject.AddComponent<MonoBehaviour_WebRequest>();

				GameObject.DontDestroyOnLoad(this.webrequest_gameobject);
			}

			//soundpool
			{
				this.soundpool_gameobject = new GameObject();
				this.soundpool_gameobject.name = "DownLoad_SoundPool";
				this.soundpool_script = this.soundpool_gameobject.AddComponent<MonoBehaviour_SoundPool>();

				GameObject.DontDestroyOnLoad(this.soundpool_gameobject);
			}

			//work_list
			this.work_list = new List<Work>();

			//add_list
			this.add_list = new List<Work>();

			//assetbundle_list
			this.assetbundle_list = new AssetBundleList();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.assetbundle_list.UnloadAllAssetBundle();

			this.webrequest_script.DeleteRequest();
			this.soundpool_script.DeleteRequest();
		}

		/** ウェブリクエスト。取得。
		*/
		public MonoBehaviour_WebRequest GetWebRequest()
		{
			return this.webrequest_script;
		}

		/** サウンドプール。取得。
		*/
		public MonoBehaviour_SoundPool GetSoundPool()
		{
			return this.soundpool_script;
		}

		/** アセットバンドルリスト。取得。
		*/
		public AssetBundleList GetAssetBundleList()
		{
			return this.assetbundle_list;
		}

		/** リクエスト。
		*/
		public Item Request(string a_url,DataType a_datatype)
		{
			Work t_work = new Work(a_url,a_datatype,0,Config.INVALID_ASSSETBUNDLE_ID);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。アセットバンドル。

		a_url                 : アドレス。
		a_assetbundle_id      : 重複チェック用のＩＤ。
		a_data_version        : 再ダウンロードチェック用のバージョン値。

		*/
		public Item RequestAssetBundle(string a_url,long a_assetbundle_id,uint a_data_version)
		{
			Work t_work = new Work(a_url,DataType.AssetBundle,a_data_version,a_assetbundle_id);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。サウンドプール。

		a_url                 : アドレス。
		a_data_version        : 再ダウンロードチェック用のバージョン値。

		*/
		public Item RequestSoundPool(string a_url,uint a_data_version)
		{
			Work t_work = new Work(a_url,DataType.SoundPool,a_data_version,Config.INVALID_ASSSETBUNDLE_ID);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** 処理中。チェック。
		*/
		public bool IsBusy()
		{
			if((this.work_list.Count > 0)||(this.add_list.Count > 0)){
				return true;
			}
			return false;
		}

		/** 更新。
		*/
		public void Main()
		{
			try{
				//追加。
				if(this.add_list.Count > 0){
					for(int ii=0;ii<this.add_list.Count;ii++){
						this.work_list.Add(this.add_list[ii]);
					}
					this.add_list.Clear();
				}

				int t_index = 0;
				while(t_index < this.work_list.Count){
					if(this.work_list[t_index].Main() == true){
						this.work_list.RemoveAt(t_index);
					}else{
						t_index++;
					}
				}
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}
	}
}

