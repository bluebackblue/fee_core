

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
	public class Data
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

		/** work
		*/
		private Fee.List.NodePool<WorkItem> work_pool;
		private System.Collections.Generic.LinkedList<WorkItem> work_add;
		private System.Collections.Generic.LinkedList<WorkItem> work_list;

		/** playerloop_flag
		*/
		private bool playerloop_flag;

		/** list
		*/
		private System.Collections.Generic.Dictionary<string,ListItem> list;

		/** main_load
		*/
		private Main_Load main_load;

		/** [シングルトン]constructor
		*/
		private Data()
		{
			//work
			this.work_pool = new List.NodePool<WorkItem>(16);
			this.work_add = new System.Collections.Generic.LinkedList<WorkItem>();
			this.work_list = new System.Collections.Generic.LinkedList<WorkItem>();

			//list
			this.list = new System.Collections.Generic.Dictionary<string,ListItem>();

			//main_load
			this.main_load = new Main_Load();

			//PlayerLoopType
			this.playerloop_flag = true;
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Config.PLAYERLOOP_ADDTYPE,Config.PLAYERLOOP_TARGETTYPE,typeof(PlayerLoopType.Fee_Data_Main),this.Main);
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.main_load.Delete();

			//PlayerLoopType
			this.playerloop_flag = false;
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof(PlayerLoopType.Fee_Data_Main));
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
		public Main_Load GetMainLoad()
		{
			return this.main_load;
		}

		/** データ。登録。
		*/
		public void RegistDataJson(string a_json_string)
		{
			System.Collections.Generic.Dictionary<string,Fee.Data.JsonListItem> t_list = Fee.JsonItem.Convert.JsonStringToObject<System.Collections.Generic.Dictionary<string,Fee.Data.JsonListItem>>(a_json_string);
			if(t_list != null){
				foreach(System.Collections.Generic.KeyValuePair<string,Fee.Data.JsonListItem> t_pair in t_list){
					this.RegistDataItem(t_pair.Key,t_pair.Value.path_type,new Fee.File.Path(t_pair.Value.path),t_pair.Value.assetbundle_name);
				}
			}
		}

		/** データ。登録。
		*/
		public void RegistDataItem(string a_id,PathType a_path_type,Fee.File.Path a_path,string a_assetbundle_name)
		{
			if(this.list.ContainsKey(a_id) == false){
				this.list.Add(a_id,new ListItem(a_id,a_path_type,a_path,a_assetbundle_name));
			}else{
				Tool.Assert(false);
			}
		}

		/** データ。クリア。
		*/
		public void ClearData()
		{
			this.list.Clear();
		}

		/** リクエスト。ロード。
		*/
		public Item RequestLoad(string a_id)
		{
			ListItem t_item;
			if(this.list.TryGetValue(a_id,out t_item) == true){
				System.Collections.Generic.LinkedListNode<WorkItem> t_work_node = this.work_pool.Alloc();
				t_work_node.Value.Reset();

				t_work_node.Value.RequestLoad(t_item);
				this.work_add.AddLast(t_work_node);

				return t_work_node.Value.GetItem();
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
			if((this.work_list.Count > 0)||(this.work_add.Count > 0)){
				return true;
			}
			return false;
		}
	}
}

