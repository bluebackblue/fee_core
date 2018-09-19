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

		/** touch
		*/
		public Touch_Phase[] touch;

		/** [シングルトン]constructor
		*/
		private Touch()
		{
			this.touch = new Touch_Phase[65];
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** 更新。
		*/
		public void Main(NRender2D.Render2D a_render2d)
		{
			try{
				UnityEngine.Experimental.Input.Touchscreen t_touchscreen_current = UnityEngine.Experimental.Input.Touchscreen.current;
				if(t_touchscreen_current != null){

					for(int ii=0;ii<this.touch.Length;ii++){
						if(ii < t_touchscreen_current.allTouchControls.Count){
							//デバイス。
							UnityEngine.Experimental.Input.PointerPhase t_phase =  UnityEngine.Experimental.Input.PointerPhase.None;

							bool t_enable = false;
							int t_touch_id = t_touchscreen_current.allTouchControls[ii].touchId.ReadValue();
							float t_touch_x = 0.0f;
							float t_touch_y = 0.0f;

							if(ii == 0){
								t_phase = t_touchscreen_current.allTouchControls[ii].phase.ReadValue();
								t_touch_x = t_touchscreen_current.allTouchControls[ii].position.x.ReadValue();
								t_touch_y = t_touchscreen_current.allTouchControls[ii].position.y.ReadValue();
								t_enable = true;
							}else{
								if(t_touch_id == 0){
									t_phase = UnityEngine.Experimental.Input.PointerPhase.None;
									t_enable = false;
								}else{
									t_phase = t_touchscreen_current.allTouchControls[ii].phase.ReadValue();
									t_touch_x = t_touchscreen_current.allTouchControls[ii].position.x.ReadValue();
									t_touch_y = t_touchscreen_current.allTouchControls[ii].position.y.ReadValue();
									t_enable = true;
								}
							}

							//設定。
							if(t_enable == true){
								this.touch[ii].Set(true,t_phase,t_touch_x,t_touch_y);
							}else{
								this.touch[ii].Reset();
							}
						}else{
							//設定。
							this.touch[ii].Reset();
						}
					}
				}

				//更新。
				for(int ii=0;ii<this.touch.Length;ii++){
					this.touch[ii].Main();
				}
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}
	}
}

