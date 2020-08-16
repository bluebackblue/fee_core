

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
	public class File
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

		/** work
		*/
		private Fee.List.NodePool<WorkItem> work_pool;
		private System.Collections.Generic.LinkedList<WorkItem> work_add;
		private System.Collections.Generic.LinkedList<WorkItem> work_list;

		/** playerloop_flag
		*/
		private bool playerloop_flag;

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

		/** certificate_list
		*/
		private CertificateList certificate_list;

		/** [シングルトン]constructor
		*/
		private File()
		{
			//work
			this.work_pool = new List.NodePool<WorkItem>(16);
			this.work_add = new System.Collections.Generic.LinkedList<WorkItem>();
			this.work_list = new System.Collections.Generic.LinkedList<WorkItem>();

			//main_androidcontent
			this.main_androidcontent = new Main_AndroidContent();

			//main_io
			this.main_io = new Main_Io();

			//main_webrequest
			this.main_webrequest = new Main_WebRequest();

			//main_resources
			this.main_resources = new Main_Resources();

			//certificate_list
			this.certificate_list = new CertificateList();

			//PlayerLoopType
			this.playerloop_flag = true;
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Config.PLAYERLOOP_ADDTYPE,Config.PLAYERLOOP_TARGETTYPE,typeof(PlayerLoopType.Fee_File_Main),this.Main);
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			//PlayerLoopType
			this.playerloop_flag = false;
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof(PlayerLoopType.Fee_File_Main));
		}

		/** 更新。
		*/
		private void Main()
		{
			try{
				if(this.playerloop_flag == true){
					//追加。
					{
						System.Collections.Generic.LinkedListNode<WorkItem> t_node = this.work_add.Last;
						while(t_node != null){
							this.work_add.Remove(t_node);
							this.work_list.AddLast(t_node);
							t_node = this.work_add.Last;
						}
					}

					//更新。
					{
						System.Collections.Generic.LinkedListNode<WorkItem> t_node = this.work_list.First;
						while(t_node != null){
							System.Collections.Generic.LinkedListNode<WorkItem> t_node_next = t_node.Next;
							if(t_node.Value.Main() == true){
								this.work_list.Remove(t_node);
								this.work_pool.Free(t_node);
							}
							t_node = t_node_next;
						}
					}
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
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

		/** RegistCertificate
		*/
		public void RegistCertificate(string a_tag,string a_url_pattern,string a_certificate_string)
		{
			this.certificate_list.Regist(a_tag,a_url_pattern,a_certificate_string);
		}

		/** UnRegistCertificate
		*/
		public void UnRegistCertificate(string a_tag)
		{
			this.certificate_list.UnRegist(a_tag);
		}

		/** GetCertificateString
		*/
		public string GetCertificateString(string a_url)
		{
			return this.certificate_list.GetCertificateString(a_url);
		}

		/** RequestLoadUrl
		*/
		public Item RequestLoadUrl(LoadRequestType a_request_type,Path a_path,System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> a_post_data = null,Fee.File.CustomCertificateHandler a_certificate_handler = null)
		{
			switch(a_request_type){

			//ロードＵＲＬ。

			case LoadRequestType.LoadUrlBinaryFile:
				{
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestLoadUrlBinaryFile(a_path,a_post_data,a_certificate_handler);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
				}break;
			case LoadRequestType.LoadUrlTextFile:
				{
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestLoadUrlTextFile(a_path,a_post_data,a_certificate_handler);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
				}break;
			case LoadRequestType.LoadUrlTextureFile:
				{
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestLoadUrlTextureFile(a_path,a_post_data,a_certificate_handler);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
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
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestLoadLocalBinaryFile(a_path);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
				}break;
			case LoadRequestType.LoadLocalTextFile:
				{
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestLoadLocalTextFile(a_path);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
				}break;
			case LoadRequestType.LoadLocalTextureFile:
				{
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestLoadLocalTextureFile(a_path);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
				}break;

			//ロードストリーミングアセット。

			case LoadRequestType.LoadStreamingAssetsBinaryFile:
				{
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestLoadStreamingAssetsBinaryFile(a_path);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
				}break;
			case LoadRequestType.LoadStreamingAssetsTextFile:
				{
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestLoadStreamingAssetsTextFile(a_path);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
				}break;
			case LoadRequestType.LoadStreamingAssetsTextureFile:
				{
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestLoadStreamingAssetsTextureFile(a_path);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
				}break;

			//ロードリソース。

			case LoadRequestType.LoadResourcesTextFile:
				{
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestLoadResourcesTextFile(a_path);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
				}break;
			case LoadRequestType.LoadResourcesTextureFile:
				{
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestLoadResourcesTextureFile(a_path);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
				}break;
			case LoadRequestType.LoadResourcesPrefabFile:
				{
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestLoadResourcesPrefabFile(a_path);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
				}break;

			//ロードＵＲＬ。

			case LoadRequestType.LoadUrlBinaryFile:
				{
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestLoadUrlBinaryFile(a_path,null,null);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
				}break;
			case LoadRequestType.LoadUrlTextFile:
				{
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestLoadUrlTextFile(a_path,null,null);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
				}break;
			case LoadRequestType.LoadUrlTextureFile:
				{
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestLoadUrlTextureFile(a_path,null,null);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
				}break;

			//フルパス。

			case LoadRequestType.LoadFullPathBinaryFile:
				{
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestLoadFullPathBinaryFile(a_path);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
				}break;
			case LoadRequestType.LoadFullPathTextFile:
				{
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestLoadFullPathTextFile(a_path);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
				}break;
			case LoadRequestType.LoadFullPathTextureFile:
				{
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestLoadFullPathTextureFile(a_path);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
				}break;

			#if(UNITY_EDITOR)

			//アセットパス。

			case LoadRequestType.LoadAssetsPathBinaryFile:
				{
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestLoadAssetsPathBinaryFile(a_path);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
				}break;
			case LoadRequestType.LoadAssetsPathTextFile:
				{
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestLoadAssetsPathTextFile(a_path);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
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
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestSaveLocalBinaryFile(a_relative_path,a_binary);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
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
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestSaveLocalTextFile(a_relative_path,a_text);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
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
					System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
					t_work_node.Value.Reset();

					t_work_node.Value.RequestSaveLocalTextureFile(a_relative_path,a_texture);
					this.work_add.AddLast(t_work_node);

					return t_work_node.Value.GetItem();
				}break;
			}

			Tool.Assert(false);
			return null;
		}

		/** 処理中。チェック。
		*/
		public bool IsBusy()
		{
			if((this.work_list.Count > 0)||(this.work_add.Count > 0)){
				return true;
			}
			return false;
		}
	}
}

