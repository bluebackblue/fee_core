

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief サウンドプール。ツール。
*/


/** Fee.SoundPool
*/
namespace Fee.SoundPool
{
	/** SoundPool
	*/
	public class SoundPool
	{
		/** [シングルトン]s_instance
		*/
		private static SoundPool s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new SoundPool();
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
		public static SoundPool GetInstance()
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

		/** main_file
		*/
		private Main_File main_file;

		/** player
		*/
		private Player player;

		/** [シングルトン]constructor
		*/
		private SoundPool()
		{
			//work
			this.work_pool = new List.NodePool<WorkItem>(16);
			this.work_add = new System.Collections.Generic.LinkedList<WorkItem>();
			this.work_list = new System.Collections.Generic.LinkedList<WorkItem>();

			//main_file
			this.main_file = new Main_File();

			//player
			this.player = new Player();

			//playerloop_flag
			this.playerloop_flag = true;

			//PlayerLoopSystem
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Config.PLAYERLOOP_ADDTYPE,Config.PLAYERLOOP_TARGETTYPE,typeof(PlayerLoopSystemType.Fee_SoundPool_Main),this.Main);
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			//player
			this.player.Delete();
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

		/** GetPlayer
		*/
		public Player GetPlayer()
		{
			return this.player;
		}

		/** main_file。取得。
		*/
		public Main_File GetMainFile()
		{
			return this.main_file;
		}

		/** リクエスト。ロードローカル。パック。

			管理ファイルを直接ロード。

		*/
		public Item RequestLoadLocalPack(Fee.File.Path a_relative_path)
		{
			System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
			t_work_node.Value.Reset();

			t_work_node.Value.RequestLoadLocalPack(a_relative_path);
			this.work_add.AddLast(t_work_node);

			return t_work_node.Value.GetItem();
		}

		/** リクエスト。ロードストリーミングアセット。パック。

			ロード後ローカルセーブ。

		*/
		public Item RequestLoadStreamingAssetsPack(Fee.File.Path a_relative_path,uint a_data_version)
		{
			System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
			t_work_node.Value.Reset();


			t_work_node.Value.RequestLoadStreamingAssetsPack(a_relative_path,a_data_version);
			this.work_add.AddLast(t_work_node);

			return t_work_node.Value.GetItem();
		}

		/** リクエスト。ロードＵＲＬ。パック。

			ロード後ローカルセーブ。

		*/
		public Item RequestLoadUrlPack(File.Path a_path,System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> a_post_data,Fee.File.CustomCertificateHandler a_certificate_handler,uint a_data_version)
		{
			System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
			t_work_node.Value.Reset();


			t_work_node.Value.RequestLoadUrlBinaryFile(a_path,a_post_data,a_certificate_handler,a_data_version);
			this.work_add.AddLast(t_work_node);

			return t_work_node.Value.GetItem();
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

