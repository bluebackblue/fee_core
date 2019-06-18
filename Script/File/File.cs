

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

		/** main_io
		*/
		private Main_Io main_io;

		/** main_webrequest
		*/
		private Main_WebRequest main_webrequest;

		/** main_resources
		*/
		private Main_Resources main_resources;

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
			//main_io
			this.main_io = new Main_Io();

			//main_webrequest
			this.main_webrequest = new Main_WebRequest();

			//main_resources
			this.main_resources = new Main_Resources();

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
		}

		/** main_io。取得。
		*/
		public Main_Io GetMainIo()
		{
			return this.main_io;
		}

		/** main_webrequest。取得。
		*/
		public Main_WebRequest GetMainWebRequest()
		{
			return this.main_webrequest;
		}

		/** main_resources。取得。
		*/
		public Main_Resources GetMainResources()
		{
			return this.main_resources;
		}

		/** アセットバンドルリスト。取得。
		*/
		public AssetBundleList GetAssetBundleList()
		{
			return this.assetbundle_list;
		}

		/** リクエスト。ロードローカル。バイナリファイル。
		*/
		public Item RequestLoadLocalBinaryFile(Path a_relative_path)
		{
			Work t_work = new Work();
			t_work.RequestLoadLocalBinaryFile(a_relative_path);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ロードローカル。テキストファイル。
		*/
		public Item RequestLoadLocalTextFile(Path a_relative_path)
		{
			Work t_work = new Work();
			t_work.RequestLoadLocalTextFile(a_relative_path);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ロードローカル。テクスチャファイル。
		*/
		public Item RequestLoadLocalTextureFile(Path a_relative_path)
		{
			Work t_work = new Work();
			t_work.RequestLoadLocalTextureFile(a_relative_path);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。セーブローカル。バイナリファイル。
		*/
		public Item RequestSaveLocalBinaryFile(Path a_relative_path,byte[] a_binary)
		{
			Work t_work = new Work();
			t_work.RequestSaveLocalBinaryFile(a_relative_path,a_binary);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。セーブローカル。テキストファイル。
		*/
		public Item RequestSaveLocalTextFile(Path a_relative_path,string a_text)
		{
			Work t_work = new Work();
			t_work.RequestSaveLocalTextFile(a_relative_path,a_text);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。セーブローカル。テクスチャファイル。
		*/
		public Item RequestSaveLocalTextureFile(Path a_relative_path,UnityEngine.Texture2D a_texture)
		{
			Work t_work = new Work();
			t_work.RequestSaveLocalTextureFile(a_relative_path,a_texture);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ダウンロード。バイナリファイル。
		*/
		public Item RequestDownLoadBinaryFile(Path a_url_path,UnityEngine.WWWForm a_post_data)
		{
			Work t_work = new Work();
			t_work.RequestDownLoadBinaryFile(a_url_path,a_post_data);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ダウンロード。テキストファイル。
		*/
		public Item RequestDownLoadTextFile(Path a_url_path,UnityEngine.WWWForm a_post_data)
		{
			Work t_work = new Work();
			t_work.RequestDownLoadTextFile(a_url_path,a_post_data);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ダウンロード。テクスチャファイル。
		*/
		public Item RequestDownLoadTextureFile(Path a_url_path,UnityEngine.WWWForm a_post_data)
		{
			Work t_work = new Work();
			t_work.RequestDownLoadTextureFile(a_url_path,a_post_data);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。アセットバンドル。

		a_path                : パス。
		a_assetbundle_id      : 重複チェック用のＩＤ。
		a_data_version        : 再ダウンロードチェック用のバージョン値。

		*/
		public Item RequestDownLoadAssetBundle(Path a_url_path,long a_assetbundle_id,uint a_data_version)
		{
			Work t_work = new Work();
			t_work.RequestDownLoadAssetBundle(a_url_path,a_assetbundle_id,a_data_version);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ロードストリーミングアセット。バイナリファイル。
		*/
		public Item RequestLoadStreamingAssetsBinaryFile(Path a_relative_path)
		{
			Work t_work = new Work();
			t_work.RequestLoadStreamingAssetsBinaryFile(a_relative_path);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ロードストリーミングアセット。テキストファイル。
		*/
		public Item RequestLoadStreamingAssetsTextFile(Path a_relative_path)
		{
			Work t_work = new Work();
			t_work.RequestLoadStreamingAssetsTextFile(a_relative_path);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ロードストリーミングアセット。テクスチャーファイル。
		*/
		public Item RequestLoadStreamingAssetsTextureFile(Path a_relative_path)
		{
			Work t_work = new Work();
			t_work.RequestLoadStreamingAssetsTextureFile(a_relative_path);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ロードリソース。アセットファイル。
		*/
		public Item RequestLoadResourcesAssetFile(Path a_relative_path)
		{
			Work t_work = new Work();
			t_work.RequestLoadResourcesAssetFile(a_relative_path);
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
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

