

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

		/** main_load
		*/
		private Main_Load main_load;

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

			//main_load
			this.main_load = new Main_Load();

			//work_list
			this.work_list = new System.Collections.Generic.List<Work>();

			//add_list
			this.add_list = new System.Collections.Generic.List<Work>();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.main_load.Delete();
		}

		/** メイン。取得。
		*/
		public Main_Load GetMainLoad()
		{
			return this.main_load;
		}

		/** データリスト。登録。
		*/
		public void RegisterDataList(System.Collections.Generic.Dictionary<string,Fee.Data.JsonListItem> a_list)
		{
			foreach(System.Collections.Generic.KeyValuePair<string,Fee.Data.JsonListItem> t_pair in a_list){
				this.RegisterDataItem(t_pair.Key,t_pair.Value.path_type,new Fee.File.Path(t_pair.Value.path),t_pair.Value.assetbundle_name);
			}
		}

		/** データアイテム。登録。
		*/
		public void RegisterDataItem(string a_id,PathType a_path_type,Fee.File.Path a_path,string a_assetbundle_name)
		{
			if(this.list.ContainsKey(a_id) == false){
				this.list.Add(a_id,new ListItem(a_id,a_path_type,a_path,a_assetbundle_name));
			}else{
				Tool.Assert(false);
			}
		}

		/** リクエスト。ロード。
		*/
		public Item RequestLoad(string a_id)
		{
			ListItem t_item;
			if(this.list.TryGetValue(a_id,out t_item) == true){
				Work t_work = new Work();
				t_work.RequestLoad(t_item);
				this.add_list.Add(t_work);
				return t_work.GetItem();
			}else{
				Item t_result = new Item();
				t_result.SetResultErrorString("ID Not Found : " + a_id);
				return t_result;
			}
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

