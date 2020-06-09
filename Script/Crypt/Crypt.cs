

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 暗号。
*/


/** Fee.Crypt
*/
namespace Fee.Crypt
{
	/** Crypt
	*/
	public class Crypt
	{
		/** [シングルトン]s_instance
		*/
		private static Crypt s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Crypt();
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
		public static Crypt GetInstance()
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

		/** main_security
		*/
		private Main_Security main_security;

		/** [シングルトン]constructor
		*/
		private Crypt()
		{
			//work
			this.work_pool = new List.NodePool<WorkItem>(16);
			this.work_add = new System.Collections.Generic.LinkedList<WorkItem>();
			this.work_list = new System.Collections.Generic.LinkedList<WorkItem>();

			//main_security
			this.main_security = new Main_Security();

			//playerloop_flag
			this.playerloop_flag = true;

			//PlayerLoopSystem
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Config.PLAYERLOOP_ADDTYPE,Config.PLAYERLOOP_TARGETTYPE,typeof(PlayerLoopSystemType.Fee_Crypt_Main),this.Main);
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.main_security.Delete();

			//playerloop_flag
			this.playerloop_flag = false;

			//PlayerLoopSystem
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof(PlayerLoopSystemType.Fee_Crypt_Main));
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

		/** メイン。取得。
		*/
		public Main_Security GetMainSecurity()
		{
			return this.main_security;
		}

		/** リクエスト。暗号化。パブリックキー。
		*/
		public Item RequestEncryptPublicKey(byte[] a_binary,string a_key)
		{
			System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
			t_work_node.Value.Reset();


			t_work_node.Value.RequestEncryptPublicKey(a_binary,a_key);
			this.work_add.AddLast(t_work_node);

			return t_work_node.Value.GetItem();
		}

		/** リクエスト。複合化。プライベートキー。
		*/
		public Item RequestDecryptPrivateKey(byte[] a_binary,string a_key)
		{
			System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
			t_work_node.Value.Reset();

			t_work_node.Value.RequestDecryptPrivateKey(a_binary,a_key);
			this.work_add.AddLast(t_work_node);

			return t_work_node.Value.GetItem();
		}

		/** リクエスト。証明書作成。プライベートキー。
		*/
		public Item RequestCreateSignaturePrivateKey(byte[] a_binary,string a_key)
		{
			System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
			t_work_node.Value.Reset();

			t_work_node.Value.RequestCreateSignaturePrivateKey(a_binary,a_key);
			this.work_add.AddLast(t_work_node);

			return t_work_node.Value.GetItem();
		}

		/** リクエスト。証明書検証。パブリックキー。
		*/
		public Item RequestVerifySignaturePublicKey(byte[] a_binary,byte[] a_signature_binary,string a_key)
		{
			System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
			t_work_node.Value.Reset();

			t_work_node.Value.RequestVerifySignaturePublicKey(a_binary,a_signature_binary,a_key);
			this.work_add.AddLast(t_work_node);

			return t_work_node.Value.GetItem();
		}

		/** リクエスト。暗号化。パスワード。
		*/
		public Item RequestEncryptPass(byte[] a_binary,int a_index,int a_length,string a_pass,string a_salt)
		{
			System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
			t_work_node.Value.Reset();

			t_work_node.Value.RequestEncryptPass(a_binary,a_index,a_length,a_pass,a_salt);
			this.work_add.AddLast(t_work_node);

			return t_work_node.Value.GetItem();
		}

		/** リクエスト。複合化。パス。
		*/
		public Item RequestDecryptPass(byte[] a_binary,int a_index,int a_length,string a_pass,string a_salt)
		{
			System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
			t_work_node.Value.Reset();

			t_work_node.Value.RequestDecryptPass(a_binary,a_index,a_length,a_pass,a_salt);
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

