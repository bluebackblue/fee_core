using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 入力。タッチ。
*/


/** NInput
*/
namespace NInput
{
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

		/** タッチコールバック。
		*/
		public delegate void CallBack_OnTouch(Touch_Phase a_touch_phase);

		/** コールバック。
		*/
		public CallBack_OnTouch callback;

		/** リスト。
		*/
		public List<Touch_Phase> list;

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
			//callback
			this.callback = null;

			//list
			this.list = new List<Touch_Phase>();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** 更新。タッチ。タッチスクリーン
		*/
		/*
		public bool Main_Touch_Touchscreen(NRender2D.Render2D a_render2d)
		{
			UnityEngine.Experimental.Input.Touchscreen t_touchscreen_current = UnityEngine.Experimental.Input.Touchscreen.current;
			if(t_touchscreen_current != null){
				for(int ii=0;ii<this.touch.Length;ii++){
					if(ii < t_touchscreen_current.allTouchControls.Count){
						//デバイス。
						UnityEngine.Experimental.Input.PointerPhase t_touch_phase = t_touchscreen_current.allTouchControls[ii].phase.ReadValue();
						int t_touch_id = t_touchscreen_current.allTouchControls[ii].touchId.ReadValue();
						int t_touch_x = (int)t_touchscreen_current.allTouchControls[ii].position.x.ReadValue();
						int t_touch_y = (int)t_touchscreen_current.allTouchControls[ii].position.y.ReadValue();

						//（ＧＵＩスクリーン座標）=>（仮想スクリーン座標）。
						int t_x;
						int t_y;
						a_render2d.GuiScreenToVirtualScreen(t_touch_x,t_touch_y,out t_x,out t_y);

						//設定。
						this.touch[ii].Set(ii,t_touch_phase,t_touch_id,t_x,t_y);
					}else{
						//設定。
						this.touch[ii].Set(ii,UnityEngine.Experimental.Input.PointerPhase.None,-1,0,0);
					}
				}
				return true;
			}
			return false;
		}
		*/

		/** 検索。
		*/
		public int SearchListItemFromNoUpdate(int a_x,int a_y)
		{
			int t_ret_index = -1;
			int t_ret_length = 0;

			for(int ii=0;ii<this.list.Count;ii++){
				if(this.list[ii].update == false){
					int t_length_x = a_x - this.list[ii].value_x;
					int t_length_y = a_y - this.list[ii].value_y;
					int t_length = t_length_x * t_length_x + t_length_y * t_length_y;
					if(t_ret_index < 0){
						t_ret_index = ii;
						t_ret_length = t_length;
					}else if(t_ret_length > t_length){
						t_ret_index = ii;
						t_ret_length = t_length;
					}
				}
			}

			return t_ret_index;
		}

		/** 更新。タッチ。
		*/
		public void Main_Touch(NRender2D.Render2D a_render2d)
		{
			#if(true)
			{
				//TODO:旧InputMnager使用。

				for(int ii=0;ii<this.list.Count;ii++){
					this.list[ii].update = false;
				}

				//新規、追跡。
				for(int ii=0;ii<UnityEngine.Input.touchCount;ii++){
					UnityEngine.Touch t_touch = UnityEngine.Input.GetTouch(ii);

					switch(t_touch.phase){
					case TouchPhase.Began:
					case TouchPhase.Moved:
					case TouchPhase.Stationary:
						{
							int t_touch_x = (int)t_touch.position.x;
							int t_touch_y = (int)(Screen.height - t_touch.position.y);
							float t_pressure = t_touch.pressure;
							float t_radius = t_touch.radius;
							float t_angle_altitude = t_touch.altitudeAngle;
							float t_angle_azimuth = t_touch.azimuthAngle;
							string t_phase_string = "n";

							if(t_touch.phase == TouchPhase.Began){
								t_phase_string = "b";
							}else if(t_touch.phase == TouchPhase.Moved){
								t_phase_string = "m";
							}else if(t_touch.phase == TouchPhase.Stationary){
								t_phase_string = "s";
							}

							//（ＧＵＩスクリーン座標）=>（仮想スクリーン座標）。
							int t_x;
							int t_y;
							a_render2d.GuiScreenToVirtualScreen(t_touch_x,t_touch_y,out t_x,out t_y);

							//検索。
							int t_index = this.SearchListItemFromNoUpdate(t_x,t_y);
							if(t_index >= 0){
								//追跡。
								this.list[t_index].Set(t_x,t_y,t_phase_string);
								{
									this.list[t_index].update = true;
									this.list[t_index].fadeoutframe = 0;
								}
							}else{
								//新規。
								Touch_Phase t_touch_phase = new Touch_Phase();
								t_touch_phase.Set(t_x,t_y,t_phase_string);
								this.list.Add(t_touch_phase);
								t_index = this.list.Count - 1;
								{
									this.list[t_index].update = true;
									this.list[t_index].fadeoutframe = 0;
								}
								if(this.callback != null){
									this.callback(t_touch_phase);
								}
							}
						}break;
					}
				}

				//強制削除。
				#if(false)
				for(int ii=0;ii<UnityEngine.Input.touchCount;ii++){
					UnityEngine.Touch t_touch = UnityEngine.Input.GetTouch(ii);

					switch(t_touch.phase){
					case TouchPhase.Ended:
					case TouchPhase.Canceled:
						{
							int t_touch_x = (int)t_touch.position.x;
							int t_touch_y = (int)(Screen.height - t_touch.position.y);
							float t_pressure = t_touch.pressure;
							float t_radius = t_touch.radius;
							float t_angle_altitude = t_touch.altitudeAngle;
							float t_angle_azimuth = t_touch.azimuthAngle;

							//（ＧＵＩスクリーン座標）=>（仮想スクリーン座標）。
							int t_x;
							int t_y;
							a_render2d.GuiScreenToVirtualScreen(t_touch_x,t_touch_y,out t_x,out t_y);

							//検索。
							int t_index = this.SearchListItemFromNoUpdate(t_x,t_y);
							if(t_index >= 0){
								//強制削除。
								this.list.RemoveAt(t_index);
							}
						}break;
					}
				}
				#endif

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
			}
			#else
			{
				/*
				if(this.Main_Touch_Touchscreen(a_render2d) == true){
					//タッチスクリーンが有効。
				}
				*/
			}
			#endif
		}

		/** 更新。
		*/
		public void Main(NRender2D.Render2D a_render2d)
		{
			try{
				//タッチ。
				this.Main_Touch(a_render2d);

				//更新。
				for(int ii=0;ii<this.list.Count;ii++){
					this.list[ii].Main();
				}
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}

		/** リスト更新。
		*/
		public void UpdateList<TYPE>(Dictionary<TYPE,NInput.Touch_Phase> a_list)
			where TYPE : Touch_Phase_Key_Base
		{
			List<TYPE> t_delete_keylist = null;

			foreach(KeyValuePair<TYPE,NInput.Touch_Phase> t_pair in a_list){
				if(t_pair.Value.update == false){
					if(t_delete_keylist == null){
						t_delete_keylist = new List<TYPE>();
					}
					t_delete_keylist.Add(t_pair.Key);
				}else{
					//更新。
					t_pair.Key.OnUpdate();
				}
			}

			if(t_delete_keylist != null){
				for(int ii=0;ii<t_delete_keylist.Count;ii++){
					//リストから削除。
					TYPE t_key = t_delete_keylist[ii];
					a_list.Remove(t_key);
					t_key.OnRemove();
				}
			}
		}
	}
}

