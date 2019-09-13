

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
	public class Crypt : Config
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

		/** main_security
		*/
		private Main_Security main_security;

		/** work_list
		*/
		private System.Collections.Generic.List<WorkItem> work_list;

		/** add_list
		*/
		private System.Collections.Generic.List<WorkItem> add_list;

		/** [シングルトン]constructor
		*/
		private Crypt()
		{
			//main_security
			this.main_security = new Main_Security();

			//work_list
			this.work_list = new System.Collections.Generic.List<WorkItem>();

			//add_list
			this.add_list = new System.Collections.Generic.List<WorkItem>();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.main_security.Delete();
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
			WorkItem t_work_item = new WorkItem();
			t_work_item.RequestEncryptPublicKey(a_binary,a_key);
			this.add_list.Add(t_work_item);
			return t_work_item.GetItem();
		}

		/** リクエスト。複合化。プライベートキー。
		*/
		public Item RequestDecryptPrivateKey(byte[] a_binary,string a_key)
		{
			WorkItem t_work_item = new WorkItem();
			t_work_item.RequestDecryptPrivateKey(a_binary,a_key);
			this.add_list.Add(t_work_item);
			return t_work_item.GetItem();
		}

		/** リクエスト。証明書作成。プライベートキー。
		*/
		public Item RequestCreateSignaturePrivateKey(byte[] a_binary,string a_key)
		{
			WorkItem t_work_item = new WorkItem();
			t_work_item.RequestCreateSignaturePrivateKey(a_binary,a_key);
			this.add_list.Add(t_work_item);
			return t_work_item.GetItem();
		}

		/** リクエスト。証明書検証。パブリックキー。
		*/
		public Item RequestVerifySignaturePublicKey(byte[] a_binary,byte[] a_signature_binary,string a_key)
		{
			WorkItem t_work_item = new WorkItem();
			t_work_item.RequestVerifySignaturePublicKey(a_binary,a_signature_binary,a_key);
			this.add_list.Add(t_work_item);
			return t_work_item.GetItem();
		}

		/** リクエスト。暗号化。パスワード。
		*/
		public Item RequestEncryptPass(byte[] a_binary,string a_pass,string a_salt)
		{
			WorkItem t_work_item = new WorkItem();
			t_work_item.RequestEncryptPass(a_binary,a_pass,a_salt);
			this.add_list.Add(t_work_item);
			return t_work_item.GetItem();
		}

		/** リクエスト。複合化。パス。
		*/
		public Item RequestDecryptPass(byte[] a_binary,string a_pass,string a_salt)
		{
			WorkItem t_work_item = new WorkItem();
			t_work_item.RequestDecryptPass(a_binary,a_pass,a_salt);
			this.add_list.Add(t_work_item);
			return t_work_item.GetItem();
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

