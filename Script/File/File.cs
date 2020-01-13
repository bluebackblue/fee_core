

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。
*/


/** Unreachable code detected.
*/
#pragma warning disable 0162


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

		/** main_androidcontent
		*/
		private Main_AndroidContent main_androidcontent;

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
		private System.Collections.Generic.List<WorkItem> work_list;

		/** add_list
		*/
		private System.Collections.Generic.List<WorkItem> add_list;

		/** [シングルトン]constructor
		*/
		private File()
		{
			//main_androidcontent
			this.main_androidcontent = new Main_AndroidContent();

			//main_io
			this.main_io = new Main_Io();

			//main_webrequest
			this.main_webrequest = new Main_WebRequest();

			//main_resources
			this.main_resources = new Main_Resources();

			//work_list
			this.work_list = new System.Collections.Generic.List<WorkItem>();

			//add_list
			this.add_list = new System.Collections.Generic.List<WorkItem>();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** main_androidcontent。取得。
		*/
		public Main_AndroidContent GetMainAndroidContent()
		{
			return this.main_androidcontent;
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

		/** LoadRequestType
		*/
		public enum LoadRequestType
		{
			None,

			LoadLocalBinaryFile,
			LoadLocalTextFile,
			LoadLocalTextureFile,

			LoadStreamingAssetsBinaryFile,
			LoadStreamingAssetsTextFile,
			LoadStreamingAssetsTextureFile,

			LoadResourcesTextFile,
			LoadResourcesTextureFile,
			LoadResourcesPrefabFile,

			LoadUrlBinaryFile,
			LoadUrlTextFile,
			LoadUrlTextureFile,

			LoadFullPathBinaryFile,
			LoadFullPathTextFile,
			LoadFullPathTextureFile,

			#if(UNITY_EDITOR)

			LoadAssetsPathBinaryFile,
			LoadAssetsPathTextFile,

			#endif
		};

		/** SaveRequestType
		*/
		public enum SaveRequestType
		{
			None,

			SaveLocalBinaryFile,
			SaveLocalTextFile,
			SaveLocalTextureFile,
		};

		/** RequestLoadUrl
		*/
		public Item RequestLoadUrl(LoadRequestType a_request_type,Path a_path,UnityEngine.WWWForm a_post_data = null,Fee.File.CustomCertificateHandler a_certificate_handler = null)
		{
			switch(a_request_type){

			//ロードＵＲＬ。

			case LoadRequestType.LoadUrlBinaryFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestLoadUrlBinaryFile(a_path,a_post_data,a_certificate_handler);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;
			case LoadRequestType.LoadUrlTextFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestLoadUrlTextFile(a_path,a_post_data,a_certificate_handler);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;
			case LoadRequestType.LoadUrlTextureFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestLoadUrlTextureFile(a_path,a_post_data,a_certificate_handler);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;

			}

			Tool.Assert(false);
			return null;
		}

		/** RequestLoad
		*/
		public Item RequestLoad(LoadRequestType a_request_type,Path a_path)
		{
			switch(a_request_type){

			//ロードローカル。

			case LoadRequestType.LoadLocalBinaryFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestLoadLocalBinaryFile(a_path);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;
			case LoadRequestType.LoadLocalTextFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestLoadLocalTextFile(a_path);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;
			case LoadRequestType.LoadLocalTextureFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestLoadLocalTextureFile(a_path);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;

			//ロードストリーミングアセット。

			case LoadRequestType.LoadStreamingAssetsBinaryFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestLoadStreamingAssetsBinaryFile(a_path);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;
			case LoadRequestType.LoadStreamingAssetsTextFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestLoadStreamingAssetsTextFile(a_path);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;
			case LoadRequestType.LoadStreamingAssetsTextureFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestLoadStreamingAssetsTextureFile(a_path);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;

			//ロードリソース。

			case LoadRequestType.LoadResourcesTextFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestLoadResourcesTextFile(a_path);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;
			case LoadRequestType.LoadResourcesTextureFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestLoadResourcesTextureFile(a_path);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;
			case LoadRequestType.LoadResourcesPrefabFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestLoadResourcesPrefabFile(a_path);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;

			//ロードＵＲＬ。

			case LoadRequestType.LoadUrlBinaryFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestLoadUrlBinaryFile(a_path,null,null);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;
			case LoadRequestType.LoadUrlTextFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestLoadUrlTextFile(a_path,null,null);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;
			case LoadRequestType.LoadUrlTextureFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestLoadUrlTextureFile(a_path,null,null);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;

			//フルパス。

			case LoadRequestType.LoadFullPathBinaryFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestLoadFullPathBinaryFile(a_path);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;
			case LoadRequestType.LoadFullPathTextFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestLoadFullPathTextFile(a_path);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;
			case LoadRequestType.LoadFullPathTextureFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestLoadFullPathTextureFile(a_path);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;

			#if(UNITY_EDITOR)

			//アセットパス。

			case LoadRequestType.LoadAssetsPathBinaryFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestLoadAssetsPathBinaryFile(a_path);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;
			case LoadRequestType.LoadAssetsPathTextFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestLoadAssetsPathTextFile(a_path);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;

			#endif
			}

			Tool.Assert(false);
			return null;
		}

		/** リクエスト。セーブ。バイナリファイル。
		*/
		public Item RequestSaveBinaryFile(SaveRequestType a_request_type,Path a_relative_path,byte[] a_binary)
		{
			switch(a_request_type){
			case SaveRequestType.SaveLocalBinaryFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestSaveLocalBinaryFile(a_relative_path,a_binary);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;
			}

			Tool.Assert(false);
			return null;
		}

		/** リクエスト。セーブ。テキストファイル。
		*/
		public Item RequestSaveTextFile(SaveRequestType a_request_type,Path a_relative_path,string a_text)
		{
			switch(a_request_type){
			case SaveRequestType.SaveLocalTextFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestSaveLocalTextFile(a_relative_path,a_text);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;
			}

			Tool.Assert(false);
			return null;
		}

		/** リクエスト。セーブ。テクスチャファイル。
		*/
		public Item RequestSaveTextureFile(SaveRequestType a_request_type,Path a_relative_path,UnityEngine.Texture2D a_texture)
		{
			switch(a_request_type){
			case SaveRequestType.SaveLocalTextureFile:
				{
					WorkItem t_work_item = new WorkItem();
					t_work_item.RequestSaveLocalTextureFile(a_relative_path,a_texture);
					this.add_list.Add(t_work_item);
					return t_work_item.GetItem();
				}break;
			}

			Tool.Assert(false);
			return null;
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
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

