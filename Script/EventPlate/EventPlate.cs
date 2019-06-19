

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
	public class EventPlate : Config
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

		/** 位置。
		*/
		private Fee.Render2D.Pos2D<int> pos;

		/** worklist
		*/
		private Work[] worklist;

		/** [シングルトン]constructor
		*/
		private EventPlate()
		{
			//位置。
			this.pos.x = 0;
			this.pos.y = 0;

			//worklist
			this.worklist = new Work[(int)EventType.Max];
			for(int ii=0;ii<this.worklist.Length;ii++){
				this.worklist[ii] = new Work();
			}
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
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

		/** 位置。取得。
		*/
		public int GetX()
		{
			return this.pos.x;
		}

		/** 位置。取得。
		*/
		public int GetY()
		{
			return this.pos.y;
		}

		/** [外部からの呼び出し]更新。
		*/
		public void Main(int a_x,int a_y)
		{
			try{
				//位置。
				this.pos.Set(a_x,a_y);

				//更新。
				for(int ii=0;ii<this.worklist.Length;ii++){
					this.worklist[ii].Main(ref this.pos);
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

