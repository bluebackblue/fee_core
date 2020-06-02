

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。タッチ。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** Touch
	*/
	public class Touch
	{
		/** INVALID_TOUCH_RAW_ID
		*/
		public const int INVALID_TOUCH_RAW_ID = -1;

		/** タッチデバイスアイテム。
		*/
		public struct Touch_Device_Item
		{
			/** link
			*/
			public bool link;

			/** xy
			*/
			public int x;
			public int y;

			/** phasetype
			*/
			public Touch_Phase.PhaseType phasetype;

			/** touch_raw_id
			*/
			public int touch_raw_id;
		}

		/** タッチコールバック。
		*/
		public delegate void CallBack_OnTouch(Touch_Phase a_touch_phase);

		/** コールバック。
		*/
		public CallBack_OnTouch callback;

		/** リスト。
		*/
		public System.Collections.Generic.List<Touch_Phase> list;

		/** デバイスアイテムリスト。
		*/
		public Touch_Device_Item[] device_item_list;
		public int device_item_list_count;

		/** コールバック。設定。
		*/
		public void SetCallBack(CallBack_OnTouch a_callback)
		{
			this.callback = a_callback;
		}

		/** constructor
		*/
		public Touch()
		{
			//callback
			this.callback = null;

			//list
			this.list = new System.Collections.Generic.List<Touch_Phase>();

			//device_item_list
			this.device_item_list = new Touch_Device_Item[1];
			this.device_item_list_count = 0;
		}

		/** 削除。
		*/
		public void Delete()
		{
		}

		/** 検索。
		*/
		public int SearchListItemFromNoUpdate(int a_x,int a_y,int a_limit)
		{
			int t_ret_index = -1;
			int t_ret_length = 0;

			for(int ii=0;ii<this.list.Count;ii++){
				if(this.list[ii].update == false){
					int t_length_x = a_x - this.list[ii].value_x;
					int t_length_y = a_y - this.list[ii].value_y;
					int t_length = t_length_x * t_length_x + t_length_y * t_length_y;

					if((a_limit < 0)||(t_length < a_limit)){
						if(t_ret_index < 0){
							t_ret_index = ii;
							t_ret_length = t_length;
						}else if(t_ret_length > t_length){
							t_ret_index = ii;
							t_ret_length = t_length;
						}
					}
				}
			}

			return t_ret_index;
		}

		/** 更新。タッチ。
		*/
		public void Main_Touch()
		{
			//インプットシステム。マウス。タッチ。
			#if(USE_DEF_FEE_INPUTSYSTEM)
			if(Config.USE_INPUTSYSTEM_MOUSE_TOUCH == true){
				if(Touch_Phase_InputSystem_Mouse.Main() == true){
					return;
				}
			}
			#endif

			//インプットマネージャ。インプットマウス。タッチ。
			if(Config.USE_INPUTMANAGER_INPUTMOUSE_TOUCH == true){
				if(Touch_Phase_InputManager_InputMouse.Main() == true){
					return;
				}
			}

			//インプットシステム。タッチスクリーン。タッチ。
			#if(USE_DEF_FEE_INPUTSYSTEM)
			if(Config.USE_INPUTSYSTEM_TOUCHSCREEN_TOUCH == true){
				if(Touch_Phase_InputSystem_TouchScreen.Main() == true){
					return;
				}
			}
			#endif

			//インプットマネージャ。インプットタッチ。タッチ。
			if(Config.USE_INPUTMANAGER_INPUTTOUCH_TOUCH == true){
				if(Touch_Phase_InputManager_InputTouch.Main() == true){
					return;
				}
			}
		}

		/** 更新。
		*/
		public void Main()
		{
			for(int ii=0;ii<this.list.Count;ii++){
				this.list[ii].update = false;
			}

			//タッチ。
			this.Main_Touch();

			//近距離追跡。
			for(int ii=0;ii<this.device_item_list_count;ii++){
				if(this.device_item_list[ii].link == false){
					int t_index = this.SearchListItemFromNoUpdate(this.device_item_list[ii].x,this.device_item_list[ii].y,100);
					if(t_index >= 0){
						//追跡。
						this.device_item_list[ii].link = true;

						//設定。
						this.list[t_index].Set(this.device_item_list[ii].x,this.device_item_list[ii].y,this.device_item_list[ii].phasetype);
						this.list[t_index].SetRawID(this.device_item_list[ii].touch_raw_id);

						this.list[t_index].update = true;
						this.list[t_index].fadeoutframe = 0;
					}
				}
			}

			for(int ii=0;ii<this.device_item_list_count;ii++){
				if(this.device_item_list[ii].link == false){
					int t_index = this.SearchListItemFromNoUpdate(this.device_item_list[ii].x,this.device_item_list[ii].y,-1);
					if(t_index >= 0){
						//遠距離追跡。
						this.device_item_list[ii].link = true;

						//設定。
						this.list[t_index].Set(this.device_item_list[ii].x,this.device_item_list[ii].y,this.device_item_list[ii].phasetype);
						this.list[t_index].SetRawID(this.device_item_list[ii].touch_raw_id);

						this.list[t_index].update = true;
						this.list[t_index].fadeoutframe = 0;
					}else{
						Touch_Phase t_touch_phase = new Touch_Phase();
						this.list.Add(t_touch_phase);
						t_index = this.list.Count - 1;
						{
							//新規。
							this.device_item_list[ii].link = true;

							//設定。
							this.list[t_index].Set(this.device_item_list[ii].x,this.device_item_list[ii].y,this.device_item_list[ii].phasetype);
							this.list[t_index].SetRawID(this.device_item_list[ii].touch_raw_id);

							this.list[t_index].update = true;
							this.list[t_index].fadeoutframe = 0;
						}
						if(this.callback != null){
							this.callback(t_touch_phase);
						}
					}
				}
			}

			{
				int ii=0;
				while(ii<this.list.Count){
					if(this.list[ii].update == false){
						this.list[ii].fadeoutframe++;
						if(this.list[ii].fadeoutframe >= 10){
							//タイムアウト削除。
							this.list.RemoveAt(ii);
						}else{
							this.list[ii].update = true;
							this.list[ii].phasetype = Touch_Phase.PhaseType.FadeOut;
						}
					}else{
						ii++;
					}
				}
			}

			//更新。
			for(int ii=0;ii<this.list.Count;ii++){
				this.list[ii].Main();
			}
		}

		/** タッチリスト作成。
		*/
		public static System.Collections.Generic.Dictionary<TYPE,Fee.Input.Touch_Phase> CreateTouchList<TYPE>()
			where TYPE : Touch_Phase_Key_Base
		{
			return new System.Collections.Generic.Dictionary<TYPE,Touch_Phase>();
		}

		/** タッチリスト更新。

			タッチアップされたアイテムはa_listから削除する。

		*/
		public static void UpdateTouchList<TYPE>(System.Collections.Generic.Dictionary<TYPE,Fee.Input.Touch_Phase> a_list)
			where TYPE : Touch_Phase_Key_Base
		{
			System.Collections.Generic.Stack<TYPE> t_delete_keylist = null;

			foreach(System.Collections.Generic.KeyValuePair<TYPE,Fee.Input.Touch_Phase> t_pair in a_list){
				if(t_pair.Value.update == false){
					if(t_delete_keylist == null){
						t_delete_keylist = new System.Collections.Generic.Stack<TYPE>();
					}
					t_delete_keylist.Push(t_pair.Key);
				}else{
					//更新。
					t_pair.Key.OnUpdate();
				}
			}

			//リストから削除。
			if(t_delete_keylist != null){
				while(t_delete_keylist.Count > 0){
					TYPE t_key = t_delete_keylist.Pop();
					a_list.Remove(t_key);
					t_key.OnRemove();
				}
			}
		}
	}
}

