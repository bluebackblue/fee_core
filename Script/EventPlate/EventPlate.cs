

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief イベントプレート。
*/


/** Fee.EventPlate
*/
namespace Fee.EventPlate
{
	/** EventPlate
	*/
	public class EventPlate
	{
		/** [シングルトン]s_instance
		*/
		private static EventPlate s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new EventPlate();
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
		public static EventPlate GetInstance()
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

		/** playerloop_flag
		*/
		private bool playerloop_flag;

		/** worklist
		*/
		private WorkItem[] worklist;

		/** [シングルトン]constructor
		*/
		private EventPlate()
		{
			//worklist
			this.worklist = new WorkItem[(int)EventType.Max];
			for(int ii=0;ii<this.worklist.Length;ii++){
				this.worklist[ii] = new WorkItem();
			}

			//PlayerLoopType
			this.playerloop_flag = true;
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Config.PLAYERLOOP_ADDTYPE,Config.PLAYERLOOP_TARGETTYPE,typeof(PlayerLoopType.Fee_EventPlate_Main),this.Main);
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			//PlayerLoopType
			this.playerloop_flag = false;
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof(PlayerLoopType.Fee_EventPlate_Main));
		}

		/** Main
		*/
		private void Main()
		{
			try{
				if(this.playerloop_flag == true){
					Geometry.Pos2D<int> t_pos;
					if(Fee.Input.Input.IsCreateInstance() == true){
						t_pos = Fee.Input.Input.GetInstance().mouse.cursor.pos;
					}else{
						t_pos = new Geometry.Pos2D<int>(0,0);
					}
					for(int ii=0;ii<this.worklist.Length;ii++){
						this.worklist[ii].Main(in t_pos);
					}
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}

		/** 追加。
		*/
		public void Add(Item a_eventitem,EventType a_eventtype)
		{
			this.worklist[(int)a_eventtype].Add(a_eventitem);
		}

		/** 削除。
		*/
		public void Remove(Item a_eventitem,EventType a_eventtype)
		{
			this.worklist[(int)a_eventtype].Remove(a_eventitem);
		}

		/** ソート。リクエスト。
		*/
		public void SortRequest(EventType a_eventtype)
		{
			this.worklist[(int)a_eventtype].SortRequest();
		}
	}
}

