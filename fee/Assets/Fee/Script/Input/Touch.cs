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

