

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** File
	*/
	public class File : Config
	{
		/** [シングルトン]s_instance
		*/
		private static File s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new File();
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
		public static File GetInstance()
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

		/** ルート。
		*/
		private UnityEngine.GameObject root_gameobject;
		private UnityEngine.Transform root_transform;

		/** io
		*/
		private UnityEngine.GameObject io_gameobject;
		private MonoBehaviour_Io io_script;

		/** webrequest
		*/
		private UnityEngine.GameObject webrequest_gameobject;
		private MonoBehaviour_WebRequest webrequest_script;

		/** soundpool
		*/
		private UnityEngine.GameObject soundpool_gameobject;
		private MonoBehaviour_SoundPool soundpool_script;

		/** work_list
		*/
		private System.Collections.Generic.List<Work> work_list;

		/** add_list
		*/
		private System.Collections.Generic.List<Work> add_list;

		/** アセットバンドルリスト。
		*/
		private AssetBundleList assetbundle_list;

		/** [シングルトン]constructor
		*/
		private File()
		{
			//ルート。
			this.root_gameobject = new UnityEngine.GameObject();
			this.root_gameobject.name = "DownLoad";
			UnityEngine.GameObject.DontDestroyOnLoad(this.root_gameobject);
			this.root_transform = this.root_gameobject.GetComponent<UnityEngine.Transform>();

			//io
			{
				this.io_gameobject = new UnityEngine.GameObject();
				this.io_gameobject.name = "DownLoad_Io";
				this.io_script = this.io_gameobject.AddComponent<MonoBehaviour_Io>();
				this.io_gameobject.GetComponent<UnityEngine.Transform>().SetParent(this.root_transform);
			}

			//webrequest
			{
				this.webrequest_gameobject = new UnityEngine.GameObject();
				this.webrequest_gameobject.name = "DownLoad_WebRequest";
				this.webrequest_script = this.webrequest_gameobject.AddComponent<MonoBehaviour_WebRequest>();
				this.webrequest_gameobject.GetComponent<UnityEngine.Transform>().SetParent(this.root_transform);
			}

			//soundpool
			{
				this.soundpool_gameobject = new UnityEngine.GameObject();
				this.soundpool_gameobject.name = "DownLoad_SoundPool";
				this.soundpool_script = this.soundpool_gameobject.AddComponent<MonoBehaviour_SoundPool>();
				this.soundpool_gameobject.GetComponent<UnityEngine.Transform>().SetParent(this.root_transform);
			}

			//work_list
			this.work_list = new System.Collections.Generic.List<Work>();

			//add_list
			this.add_list = new System.Collections.Generic.List<Work>();

			//assetbundle_list
			this.assetbundle_list = new AssetBundleList();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			//管理しているアセットバンドルをすべてアンロード。
			this.assetbundle_list.UnloadAllAssetBundle();

			//削除リクエスト。
			this.io_gameobject.GetComponent<UnityEngine.Transform>().SetParent(null);
			UnityEngine.GameObject.DontDestroyOnLoad(this.io_gameobject);
			this.io_script.DeleteRequest();

			//削除リクエスト。
			this.webrequest_gameobject.GetComponent<UnityEngine.Transform>().SetParent(null);
			UnityEngine.GameObject.DontDestroyOnLoad(this.webrequest_gameobject);
			this.webrequest_script.DeleteRequest();

			//削除リクエスト。
			this.soundpool_gameobject.GetComponent<UnityEngine.Transform>().SetParent(null);
			UnityEngine.GameObject.DontDestroyOnLoad(this.soundpool_gameobject);
			this.soundpool_script.DeleteRequest();

			//ルート削除。
			UnityEngine.GameObject.Destroy(this.root_gameobject);
		}

		/** MonoIo。取得。
		*/
		public MonoBehaviour_Io GetMonoIo()
		{
			return this.io_script;
		}

		/** MonoWebRequest。取得。
		*/
		public MonoBehaviour_WebRequest GetMonoWebRequest()
		{
			return this.webrequest_script;
		}

		/** MonoSoundPool。取得。
		*/
		public MonoBehaviour_SoundPool GetMonoSoundPool()
		{
			return this.soundpool_script;
		}

		/** アセットバンドルリスト。取得。
		*/
		public AssetBundleList GetAssetBundleList()
		{
			return this.assetbundle_list;
		}

		/** リクエスト。ロードローカル。バイナリファイル。
		*/
		public Item RequestLoadLocalBinaryFile(string a_filename)
		{
			Work t_work = new Work();
			t_work.RequestLoadLocalBinaryFile(a_filename);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ロードローカル。テキストファイル。
		*/
		public Item RequestLoadLocalTextFile(string a_filename)
		{
			Work t_work = new Work();
			t_work.RequestLoadLocalTextFile(a_filename);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ロードローカル。テクスチャファイル。
		*/
		public Item RequestLoadLocalTextureFile(string a_filename)
		{
			Work t_work = new Work();
			t_work.RequestLoadLocalTextureFile(a_filename);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。セーブローカル。バイナリファイル。
		*/
		public Item RequestSaveLocalBinaryFile(string a_filename,byte[] a_binary)
		{
			Work t_work = new Work();
			t_work.RequestSaveLocalBinaryFile(a_filename,a_binary);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。セーブローカル。テキストファイル。
		*/
		public Item RequestSaveLocalTextFile(string a_filename,string a_text)
		{
			Work t_work = new Work();
			t_work.RequestSaveLocalTextFile(a_filename,a_text);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。セーブローカル。テクスチャファイル。
		*/
		public Item RequestSaveLocalTextureFile(string a_filename,UnityEngine.Texture2D a_texture)
		{
			Work t_work = new Work();
			t_work.RequestSaveLocalTextureFile(a_filename,a_texture);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ダウンロード。バイナリファイル。
		*/
		public Item RequestDownLoadBinaryFile(string a_url,UnityEngine.WWWForm a_post_data,ProgressMode a_progress_mode)
		{
			Work t_work = new Work();
			t_work.RequestDownLoadBinaryFile(a_url,a_post_data,a_progress_mode);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ダウンロード。テキストファイル。
		*/
		public Item RequestDownLoadTextFile(string a_url,UnityEngine.WWWForm a_post_data,ProgressMode a_progress_mode)
		{
			Work t_work = new Work();
			t_work.RequestDownLoadTextFile(a_url,a_post_data,a_progress_mode);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ダウンロード。テクスチャファイル。
		*/
		public Item RequestDownLoadTextureFile(string a_url)
		{
			Work t_work = new Work();
			t_work.RequestDownLoadTextureFile(a_url);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。アセットバンドル。

		a_url                 : ＵＲＬ。
		a_assetbundle_id      : 重複チェック用のＩＤ。
		a_data_version        : 再ダウンロードチェック用のバージョン値。

		*/
		public Item RequestDownLoadAssetBundle(string a_url,long a_assetbundle_id,uint a_data_version)
		{
			Work t_work = new Work();
			t_work.RequestDownLoadAssetBundle(a_url,a_assetbundle_id,a_data_version);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ロードストリーミングアセット。バイナリファイル。
		*/
		public Item RequestLoadStreamingAssetsBinaryFile(string a_filename)
		{
			Work t_work = new Work();
			t_work.RequestLoadStreamingAssetsBinaryFile(a_filename);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。サウンドプール。

		a_url                 : ＵＲＬ。
		a_data_version        : 再ダウンロードチェック用のバージョン値。

		*/
		public Item RequestDownLoadSoundPool(string a_url,uint a_data_version)
		{
			Work t_work = new Work();
			t_work.RequestDownLoadSoundPool(a_url,a_data_version);
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

		/** アセットバンドル数。取得。
		*/
		public int GetAssetBundleCount()
		{
			return this.assetbundle_list.GetCount();
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

