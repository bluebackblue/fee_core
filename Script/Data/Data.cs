

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief データ。
*/


/** Fee.Data
*/
namespace Fee.Data
{
	/** Data
	*/
	public class Data : Config
	{
		/** [シングルトン]s_instance
		*/
		private static Data s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Data();
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
		public static Data GetInstance()
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

		/** list
		*/
		private System.Collections.Generic.Dictionary<string,ListItem> list;

		/** main_security
		*/
		/*
		private Main_Security main_security;
		*/

		/** work_list
		*/
		/*
		private System.Collections.Generic.List<Work> work_list;
		*/

		/** add_list
		*/
		/*
		private System.Collections.Generic.List<Work> add_list;
		*/

		/** [シングルトン]constructor
		*/
		private Data()
		{
			//list
			this.list = new System.Collections.Generic.Dictionary<string,ListItem>();

			/*
			//main_security
			this.main_security = new Main_Security();

			//work_list
			this.work_list = new System.Collections.Generic.List<Work>();

			//add_list
			this.add_list = new System.Collections.Generic.List<Work>();
			*/
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			/*
			this.main_security.Delete();
			*/
		}

		/** メイン。取得。
		*/
		/*
		public Main_Security GetMainSecurity()
		{
			return this.main_security;
		}
		*/

		/** リソースアイテム。登録。
		*/
		public void RegisterResourcesItem(string a_name,Fee.File.Path a_path)
		{
			if(this.list.ContainsKey(a_name) == false){
				this.list.Add(a_name,new ListItem(a_path));
			}else{
				Tool.Assert(false);
			}
		}

		/** リクエスト。暗号化。パブリックキー。
		*/
		/*
		public Item RequestEncryptPublicKey(byte[] a_binary,string a_key)
		{
			Work t_work = new Work();
			t_work.RequestEncryptPublicKey(a_binary,a_key);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}
		*/

		/** 更新。
		*/
		public void Main()
		{
			/*
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
			*/
		}
	}
}

