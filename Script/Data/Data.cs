

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
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

		/** main_normal
		*/
		private Main_Normal main_normal;

		/** work_list
		*/
		private System.Collections.Generic.List<Work> work_list;

		/** add_list
		*/
		private System.Collections.Generic.List<Work> add_list;

		/** [シングルトン]constructor
		*/
		private Data()
		{
			//list
			this.list = new System.Collections.Generic.Dictionary<string,ListItem>();

			//main_normal
			this.main_normal = new Main_Normal();

			//work_list
			this.work_list = new System.Collections.Generic.List<Work>();

			//add_list
			this.add_list = new System.Collections.Generic.List<Work>();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.main_normal.Delete();
		}

		/** メイン。取得。
		*/
		public Main_Normal GetMainNormal()
		{
			return this.main_normal;
		}

		/** リソースアイテム。登録。
		*/
		public void RegisterResourcesItem(string a_name,PathType a_path_type,Fee.File.Path a_path)
		{
			if(this.list.ContainsKey(a_name) == false){
				this.list.Add(a_name,new ListItem(a_path_type,a_path,null));
			}else{
				Tool.Assert(false);
			}
		}

		/** リクエスト。ノーマル。
		*/
		public Item RequestNormal(string a_name)
		{
			if(this.list.TryGetValue(a_name,out ListItem t_item) == true){
				Work t_work = new Work();
				t_work.RequestNormal(t_item);
				this.add_list.Add(t_work);
				return t_work.GetItem();
			}else{
				Item t_result = new Item();
				t_result.SetResultErrorString("name not found : " + a_name);
				return t_result;
			}
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

