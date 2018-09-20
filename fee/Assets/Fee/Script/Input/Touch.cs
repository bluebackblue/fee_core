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

		/** [シングルトン]インスタンス。取得。
		*/
		public static Touch GetInstance()
		{
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

		/** 最近点。結果。
		*/
		public struct ReturnValue_SearchListItem
		{
			public int index;
			public int length;

			public ReturnValue_SearchListItem(int a_index,int a_length)
			{
				this.index = a_index;
				this.length = a_length;
			}
		}

		/** 最近点。
		*/
		public ReturnValue_SearchListItem SearchListItem(int a_x,int a_y)
		{
			int t_ret_index = -1;
			int t_ret_length = 1000;

			for(int ii=0;ii<this.list.Count;ii++){
				//距離計測。

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

			return new ReturnValue_SearchListItem(t_ret_index,t_ret_length);
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

				for(int ii=0;ii<UnityEngine.Input.touchCount;ii++){
					UnityEngine.Touch t_touch = UnityEngine.Input.GetTouch(ii);

					switch(t_touch.phase){
					case TouchPhase.Began:
					case TouchPhase.Moved:
						{
							int t_touch_x = (int)t_touch.position.x;
							int t_touch_y = (int)t_touch.position.y;

							//（ＧＵＩスクリーン座標）=>（仮想スクリーン座標）。
							int t_x;
							int t_y;
							a_render2d.GuiScreenToVirtualScreen(t_touch_x,t_touch_y,out t_x,out t_y);

							ReturnValue_SearchListItem t_ret = this.SearchListItem(t_x,t_y);
							if((t_ret.index >= 0)&&(t_ret.length < 20)){
								//追跡。
								this.list[t_ret.index].Set(t_touch_x,t_touch_y);
								this.list[t_ret.index].update = true;
							}else{
								//新規。
								Touch_Phase t_touch_phase = new Touch_Phase();
								t_touch_phase.Set(t_touch_x,t_touch_y);
								this.list.Add(t_touch_phase);
								int t_index = this.list.Count - 1;
								this.list[t_index].update = true;

								if(this.callback != null){
									this.callback(t_touch_phase);
								}
							}
						}break;
					}
				}

				{
					int ii=0;
					while(ii<this.list.Count){
						if(this.list[ii].update == false){
							this.list.RemoveAt(ii);
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
	}
}

