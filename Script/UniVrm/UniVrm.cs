

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＮＩＶＲＭ。
*/


/** Fee.UniVrm
*/
namespace Fee.UniVrm
{
	/** UniVrm
	*/
	public class UniVrm
	{
		/** [シングルトン]s_instance
		*/
		private static UniVrm s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new UniVrm();
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
		public static UniVrm GetInstance()
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

		/** main_vrm
		*/
		private Main_Vrm main_vrm;

		/** work_list
		*/
		private System.Collections.Generic.List<WorkItem> work_list;

		/** add_list
		*/
		private System.Collections.Generic.List<WorkItem> add_list;

		/** [シングルトン]constructor
		*/
		private UniVrm()
		{
			//main_vrm
			this.main_vrm = new Main_Vrm();

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

		/** main_vrm。取得。
		*/
		public Main_Vrm GetMainVrm()
		{
			return this.main_vrm;
		}

		/** リクエスト。ロードＶＲＭ。
		*/
		public Item RequestLoadVrm(byte[] a_binary)
		{
			WorkItem t_work = new WorkItem();
			t_work.RequestLoadVrm(a_binary);
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
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

