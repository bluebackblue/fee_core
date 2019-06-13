

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 入力。タッチ。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** UnityEngine_InputSystem
	*/
	#if(USE_DEF_FEE_INPUTSYSTEM)
		#if((UNITY_2018_3)||(UNITY_2018_4))
			using UnityEngine_InputSystem = UnityEngine.Experimental.Input;
		#else
			using UnityEngine_InputSystem = UnityEngine.InputSystem;
		#endif
	#endif

	/** Touch
	*/
	public class Touch
	{
		/** [シングルトン]s_instance
		*/
		private static Touch s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Touch();
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
		public static Touch GetInstance()
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

		/** screen
		*/
		public int screen_w;
		public int screen_h;

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

		/** インプットシステムのバグ。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public class TouchBugCheckItem
		{
			public int errorcount;
			public bool existflag;
			public TouchBugCheckItem()
			{
				this.errorcount = 0;
				this.existflag = true;
			}
		}
		public System.Collections.Generic.Dictionary<int,TouchBugCheckItem> touch_bug_check_list;
		#endif

		/** コールバック。設定。
		*/
		public void SetCallBack(CallBack_OnTouch a_callback)
		{
			this.callback = a_callback;
		}

		/** [シングルトン]constructor
		*/
		private Touch()
		{
			//screen_w
			this.screen_w = UnityEngine.Screen.width;
			this.screen_h = UnityEngine.Screen.height;

			//callback
			this.callback = null;

			//list
			this.list = new System.Collections.Generic.List<Touch_Phase>();

			//device_item_list
			this.device_item_list = new Touch_Device_Item[1];
			this.device_item_list_count = 0;

			//インプットシステムのバグ。
			#if(USE_DEF_FEE_INPUTSYSTEM)
			this.touch_bug_check_list = new System.Collections.Generic.Dictionary<int,TouchBugCheckItem>();
			#endif
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
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

		/** 更新。インプットシステム。タッチスクリーン。タッチ。
		*/
		public bool Main_InputSystem_Touchscreen_Touch(Fee.Render2D.Render2D a_render2d)
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine_InputSystem.Touchscreen t_touchscreen_current = UnityEngine_InputSystem.InputSystem.GetDevice<UnityEngine_InputSystem.Touchscreen>();
				if(t_touchscreen_current != null){
					this.device_item_list_count = 0;

					//インプットシステムのバグ。
					{
						System.Collections.Generic.Stack<int> t_bug_delete_list = null;

						foreach(System.Collections.Generic.KeyValuePair<int,TouchBugCheckItem> t_pair in this.touch_bug_check_list){
							if(t_pair.Value.existflag == true){
								t_pair.Value.existflag = false;
							}else{
								if(t_bug_delete_list == null){
									t_bug_delete_list = new System.Collections.Generic.Stack<int>();
								}
								t_bug_delete_list.Push(t_pair.Key);
							}
						}

						if(t_bug_delete_list != null){
							while(true){
								int t_key = t_bug_delete_list.Pop();
								this.touch_bug_check_list.Remove(t_key);
								if(t_bug_delete_list.Count <= 0){
									break;
								}
							}
						}

						int t_bug_last_id = INVALID_TOUCH_RAW_ID;
						for(int ii=0;ii<t_touchscreen_current.activeTouches.Count;ii++){
							UnityEngine_InputSystem.Controls.TouchControl t_touch = t_touchscreen_current.activeTouches[ii];
							if(this.touch_bug_check_list.TryGetValue(t_touch.touchId.ReadValue(),out TouchBugCheckItem t_item) == true){
								t_item.existflag = true;
							}else{
								this.touch_bug_check_list.Add(t_touch.touchId.ReadValue(),new TouchBugCheckItem());
							}

							if((t_bug_last_id == INVALID_TOUCH_RAW_ID)||(t_bug_last_id < t_touch.touchId.ReadValue())){
								t_bug_last_id = t_touch.touchId.ReadValue();
							}
						}
						for(int ii=0;ii<t_touchscreen_current.activeTouches.Count;ii++){
							UnityEngine_InputSystem.Controls.TouchControl t_touch = t_touchscreen_current.activeTouches[ii];
							if(this.touch_bug_check_list.TryGetValue(t_touch.touchId.ReadValue(),out TouchBugCheckItem t_item) == true){

								if(t_touch.touchId.ReadValue() < t_bug_last_id - 5){
									t_item.errorcount = 99999;
								}

								if(t_touch.phase.ReadValue() == UnityEngine_InputSystem.PointerPhase.Stationary){
									t_item.errorcount++;
									if(t_item.errorcount >= 60){
										t_item.errorcount = 99999;
									}
								}else{
									t_item.errorcount = 0;
								}
							}
						}
					}

					//インプットシステムのバグ。
					int t_bug_max = 0;
					foreach(System.Collections.Generic.KeyValuePair<int,TouchBugCheckItem> t_pair in this.touch_bug_check_list){
						if(t_pair.Value.errorcount < 99999){
							t_bug_max++;
						}
					}

					//リスト作成。
					#if(false)
					{
						if(this.device_item_list.Length < t_touchscreen_current.activeTouches.Count){
							this.device_item_list = new Touch_Device_Item[t_touchscreen_current.activeTouches.Count];
						}
					}
					#else
					{
						//インプットシステムのバグ。
						if(this.device_item_list.Length < t_bug_max){
							this.device_item_list = new Touch_Device_Item[t_bug_max];
						}
					}
					#endif

					for(int ii=0;ii<t_touchscreen_current.activeTouches.Count;ii++){
						//デバイス。
						UnityEngine_InputSystem.Controls.TouchControl t_touch = t_touchscreen_current.activeTouches[ii];

						UnityEngine_InputSystem.PointerPhase t_touch_phase = t_touch.phase.ReadValue();
						int t_touch_id = t_touch.touchId.ReadValue();
						int t_touch_x = (int)t_touch.position.x.ReadValue();

						#if(UNITY_ANDROID)
						int t_touch_y = (int)(this.screen_h - t_touch.position.y.ReadValue());
						#else
						int t_touch_y = (int)t_touch.position.y.ReadValue();
						#endif

						//インプットシステムのバグ。
						{
							if(this.touch_bug_check_list.TryGetValue(t_touch_id,out TouchBugCheckItem t_bug_item) == true){
								if(t_bug_item.errorcount < 99999){
								}else{
									t_bug_item = null;
									continue;
								}
							}else{
								continue;
							}
						}

						if(this.device_item_list_count < this.device_item_list.Length){

							//（ＧＵＩスクリーン座標）=>（仮想スクリーン座標）。
							a_render2d.GuiScreenToVirtualScreen(t_touch_x,t_touch_y,out this.device_item_list[this.device_item_list_count].x,out this.device_item_list[this.device_item_list_count].y);

							//フェーズ。
							switch(t_touch_phase){
							case UnityEngine_InputSystem.PointerPhase.Began:
								{
									this.device_item_list[this.device_item_list_count].phasetype = Touch_Phase.PhaseType.Began;
								}break;
							case UnityEngine_InputSystem.PointerPhase.Moved:
								{
									this.device_item_list[this.device_item_list_count].phasetype = Touch_Phase.PhaseType.Moved;
								}break;
							case UnityEngine_InputSystem.PointerPhase.Stationary:
								{
									this.device_item_list[this.device_item_list_count].phasetype = Touch_Phase.PhaseType.Stationary;
								}break;
							default:
								{
									this.device_item_list[this.device_item_list_count].phasetype = Touch_Phase.PhaseType.None;
								}break;
							}

							//フラグ。
							this.device_item_list[this.device_item_list_count].link = false;

							//追加情報。
							this.device_item_list[this.device_item_list_count].touch_raw_id = t_touch_id;

							this.device_item_list_count++;
						}
					}

					return true;
				}
			}
			#endif

			return false;
		}

		/** 更新。インプットマネージャ。インプットタッチ。タッチ。
		*/
		public bool Main_InputManager_InputTouch_Touch(Fee.Render2D.Render2D a_render2d)
		{
			this.device_item_list_count = 0;

			//リスト作成。
			if(this.device_item_list.Length < UnityEngine.Input.touchCount){
				this.device_item_list = new Touch_Device_Item[UnityEngine.Input.touchCount];
			}

			for(int ii=0;ii<UnityEngine.Input.touchCount;ii++){
				UnityEngine.Touch t_touch = UnityEngine.Input.GetTouch(ii);

				switch(t_touch.phase){
				case UnityEngine.TouchPhase.Began:
				case UnityEngine.TouchPhase.Moved:
				case UnityEngine.TouchPhase.Stationary:
					{
						float t_touch_x = t_touch.position.x;
						float t_touch_y = t_touch.position.y;

						/*
						{
							t_touch_x = t_touch_x * UnityEngine.Screen.width / this.screen_w;
							t_touch_y = t_touch_y * UnityEngine.Screen.height / this.screen_h;
						}
						*/

						//（ＧＵＩスクリーン座標）=>（仮想スクリーン座標）。
						a_render2d.GuiScreenToVirtualScreen((int)t_touch_x,(int)(this.screen_h - t_touch_y),out this.device_item_list[this.device_item_list_count].x,out this.device_item_list[this.device_item_list_count].y);

						//フェーズ。
						if(t_touch.phase == UnityEngine.TouchPhase.Began){
							this.device_item_list[this.device_item_list_count].phasetype = Touch_Phase.PhaseType.Began;
						}else if(t_touch.phase == UnityEngine.TouchPhase.Moved){
							this.device_item_list[this.device_item_list_count].phasetype = Touch_Phase.PhaseType.Moved;
						}else if(t_touch.phase == UnityEngine.TouchPhase.Stationary){
							this.device_item_list[this.device_item_list_count].phasetype = Touch_Phase.PhaseType.Stationary;
						}else{
							this.device_item_list[this.device_item_list_count].phasetype = Touch_Phase.PhaseType.None;
						}

						//フラグ。
						this.device_item_list[this.device_item_list_count].link = false;

						//追加情報。
						this.device_item_list[this.device_item_list_count].touch_raw_id = Touch.INVALID_TOUCH_RAW_ID;

						this.device_item_list_count++;
					}break;
				}
			}

			return true;
		}

		/** 更新。インプットマネージャ。インプットマウス。タッチ。
		*/
		#if(true)
		public bool Main_InputManager_InputMouse_Touch(Fee.Render2D.Render2D a_render2d)
		{
			this.device_item_list_count = 0;

			//リスト作成。
			if(this.device_item_list.Length < 1){
				this.device_item_list = new Touch_Device_Item[1];
			}

			if(UnityEngine.Input.GetMouseButton(0) == true){
				int t_touch_x = (int)UnityEngine.Input.mousePosition.x;
				int t_touch_y = (int)UnityEngine.Input.mousePosition.y;

				/*
				{
					t_touch_x = t_touch_x * UnityEngine.Screen.width / this.screen_w;
					t_touch_y = t_touch_y * UnityEngine.Screen.height / this.screen_h;
				}
				*/

				//（ＧＵＩスクリーン座標）=>（仮想スクリーン座標）。
				a_render2d.GuiScreenToVirtualScreen((int)t_touch_x,(int)(this.screen_h - t_touch_y),out this.device_item_list[this.device_item_list_count].x,out this.device_item_list[this.device_item_list_count].y);

				//フェーズ。
				this.device_item_list[this.device_item_list_count].phasetype = Touch_Phase.PhaseType.Moved;

				//フラグ。
				this.device_item_list[this.device_item_list_count].link = false;

				//追加情報。
				this.device_item_list[this.device_item_list_count].touch_raw_id = Touch.INVALID_TOUCH_RAW_ID;

				this.device_item_list_count++;
			}

			return true;
		}
		#endif

		/** 更新。タッチ。
		*/
		public void Main_Touch(Fee.Render2D.Render2D a_render2d)
		{
			//インプットシステム。タッチスクリーン。タッチ。
			if(Config.USE_INPUTSYSTEM_TOUCHSCREEN_TOUCH == true){
				if(this.Main_InputSystem_Touchscreen_Touch(a_render2d) == true){
					return;
				}
			}

			//インプットマネージャ。インプットタッチ。タッチ。
			if(Config.USE_INPUTMANAGER_INPUTTOUCH_TOUCH == true){
				if(this.Main_InputManager_InputTouch_Touch(a_render2d) == true){
					return;
				}
			}

			//インプットマネージャ。インプットマウス。タッチ。
			if(Config.USE_INPUTMANAGER_INPUTMOUSE_TOUCH == true){
				if(this.Main_InputManager_InputMouse_Touch(a_render2d) == true){
					return;
				}
			}
		}

		/** 更新。
		*/
		public void Main(Fee.Render2D.Render2D a_render2d)
		{
			//スクリーンサイズ更新。
			{
				this.screen_w = UnityEngine.Screen.width;
				this.screen_h = UnityEngine.Screen.height;
			}

			try{
				for(int ii=0;ii<this.list.Count;ii++){
					this.list[ii].update = false;
				}

				//タッチ。
				this.Main_Touch(a_render2d);

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
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
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

