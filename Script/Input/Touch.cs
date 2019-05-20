

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
	#if(UNITY_2018_3)
	using UnityEngine_InputSystem = UnityEngine.Experimental.Input;
	#else
	using UnityEngine_InputSystem = UnityEngine.InputSystem;
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

		/** タッチデバイスアイテム。
		*/
		public struct Touch_Device_Item
		{
			public bool link;
			public int x;
			public int y;
			public Touch_Phase.PhaseType phasetype;

			/** raw_id
			*/
			public int raw_id;

			/** pressure
			*/
			/*
			public float pressure;
			*/

			/** radius
			*/
			/*
			public float radius;
			*/

			/** angle_altitude
			*/
			/*
			public float angle_altitude;
			*/

			/** angle_azimuth
			*/
			/*
			public float angle_azimuth;
			*/
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

		/** 更新。インプットシステムタッチスクリーン。タッチ。
		*/
		public bool Main_InputSystemTouchscreen_Touch(Fee.Render2D.Render2D a_render2d)
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine_InputSystem.Touchscreen t_touchscreen_current = UnityEngine_InputSystem.InputSystem.GetDevice<UnityEngine_InputSystem.Touchscreen>();
				if(t_touchscreen_current != null){

					this.device_item_list_count = 0;

					//リスト作成。
					if(this.device_item_list.Length < t_touchscreen_current.activeTouches.Count){
						this.device_item_list = new Touch_Device_Item[t_touchscreen_current.activeTouches.Count];
					}

					for(int ii=0;ii<t_touchscreen_current.activeTouches.Count;ii++){
						//デバイス。
						UnityEngine_InputSystem.Controls.TouchControl t_touch = t_touchscreen_current.activeTouches[ii];

						UnityEngine_InputSystem.PointerPhase t_touch_phase = t_touch.phase.ReadValue();
						int t_touch_id = t_touch.touchId.ReadValue();
						int t_touch_x = (int)t_touch.position.x.ReadValue();
						int t_touch_y = (int)t_touch.position.y.ReadValue();

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
						this.device_item_list[this.device_item_list_count].raw_id = t_touch_id;
						/*
						this.device_item_list[this.device_item_list_count].pressure = t_touch.pressure.ReadValue();
						this.device_item_list[this.device_item_list_count].radius = 0.0f;
						this.device_item_list[this.device_item_list_count].angle_altitude = t_touch.radius.ReadValue().x;
						this.device_item_list[this.device_item_list_count].angle_azimuth = t_touch.radius.ReadValue().y;
						*/

						this.device_item_list_count++;
					}

					return true;
				}
			}
			#endif

			return false;
		}

		/** 更新。インプットマネージャタッチ。タッチ。
		*/
		public bool Main_InputManagerTouch_Touch(Fee.Render2D.Render2D a_render2d)
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

						//resolution
						{
							t_touch_x = t_touch_x * UnityEngine.Screen.width / this.screen_w;
							t_touch_y = t_touch_y * UnityEngine.Screen.height / this.screen_h;
						}

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
						this.device_item_list[this.device_item_list_count].raw_id = -1;
						/*
						this.device_item_list[this.device_item_list_count].pressure = t_touch.pressure;
						this.device_item_list[this.device_item_list_count].radius = t_touch.radius;
						this.device_item_list[this.device_item_list_count].angle_altitude = t_touch.altitudeAngle;
						this.device_item_list[this.device_item_list_count].angle_azimuth = t_touch.azimuthAngle;
						*/

						this.device_item_list_count++;
					}break;
				}
			}

			return true;
		}

		/** 更新。インプットマネージャマウス。タッチ。
		*/
		#if(true)
		public bool Main_InputManagerMouse_Touch(Fee.Render2D.Render2D a_render2d)
		{
			this.device_item_list_count = 0;

			//リスト作成。
			if(this.device_item_list.Length < 1){
				this.device_item_list = new Touch_Device_Item[1];
			}

			if(UnityEngine.Input.GetMouseButton(0) == true){
				int t_touch_x = (int)UnityEngine.Input.mousePosition.x;
				int t_touch_y = (int)UnityEngine.Input.mousePosition.y;

				//resolution
				{
					t_touch_x = t_touch_x * UnityEngine.Screen.width / this.screen_w;
					t_touch_y = t_touch_y * UnityEngine.Screen.height / this.screen_h;
				}

				//（ＧＵＩスクリーン座標）=>（仮想スクリーン座標）。
				a_render2d.GuiScreenToVirtualScreen((int)t_touch_x,(int)(this.screen_h - t_touch_y),out this.device_item_list[this.device_item_list_count].x,out this.device_item_list[this.device_item_list_count].y);

				//フェーズ。
				this.device_item_list[this.device_item_list_count].phasetype = Touch_Phase.PhaseType.Moved;

				//フラグ。
				this.device_item_list[this.device_item_list_count].link = false;

				//追加情報。
				this.device_item_list[this.device_item_list_count].raw_id = -2;
				/*
				this.device_item_list[this.device_item_list_count].pressure = 0.0f;
				this.device_item_list[this.device_item_list_count].radius = 0.0f;
				this.device_item_list[this.device_item_list_count].angle_altitude = 0.0f;
				this.device_item_list[this.device_item_list_count].angle_azimuth = 0.0f;
				*/

				this.device_item_list_count++;
			}

			return true;
		}
		#endif

		/** 更新。タッチ。
		*/
		public void Main_Touch(Fee.Render2D.Render2D a_render2d)
		{
			//インプットシステムタッチ。
			if(Config.USE_INPUTSYSTEM_TOUCHSCREEN == true){
				if(this.Main_InputSystemTouchscreen_Touch(a_render2d) == true){
					return;
				}
			}

			//インプットマネージャタッチ。
			if(Config.USE_INPUTMANAGER_TOUCH == true){
				if(this.Main_InputManagerTouch_Touch(a_render2d) == true){
					return;
				}
			}

			//インプットマネージャマウス。
			if(Config.USE_INPUTMANAGER_MOUSE == true){
				if(this.Main_InputManagerMouse_Touch(a_render2d) == true){
					return;
				}
			}
		}

		/** 更新。
		*/
		public void Main(Fee.Render2D.Render2D a_render2d)
		{
			//screen_w
			#if(UNITY_EDITOR)||(UNITY_WEBGL)
			{
				this.screen_w = UnityEngine.Screen.width;
				this.screen_h = UnityEngine.Screen.height;
			}
			#endif

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
							this.list[t_index].SetRawID(this.device_item_list[ii].raw_id);
							/*
							this.list[t_index].SetAngle(this.device_item_list[ii].angle_altitude,this.device_item_list[ii].angle_azimuth);
							this.list[t_index].SetPressure(this.device_item_list[ii].pressure);
							this.list[t_index].SetRadius(this.device_item_list[ii].radius);
							*/
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
							this.list[t_index].SetRawID(this.device_item_list[ii].raw_id);
							/*
							this.list[t_index].SetAngle(this.device_item_list[ii].angle_altitude,this.device_item_list[ii].angle_azimuth);
							this.list[t_index].SetPressure(this.device_item_list[ii].pressure);
							ths.list[t_index].SetRadius(this.device_item_list[ii].radius);
							*/
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
								this.list[t_index].SetRawID(this.device_item_list[ii].raw_id);
								/*
								this.list[t_index].SetAngle(this.device_item_list[ii].angle_altitude,this.device_item_list[ii].angle_azimuth);
								this.list[t_index].SetPressure(this.device_item_list[ii].pressure);
								this.list[t_index].SetRadius(this.device_item_list[ii].radius);
								*/
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
				Tool.LogError(t_exception);
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

