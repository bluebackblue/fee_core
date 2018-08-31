using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 入力。マウス。
*/


/** NInput
*/
namespace NInput
{
	/** Mouse
	*/
	public class Mouse
	{
		/** [シングルトン]s_instance
		*/
		private static Mouse s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Mouse();
			}
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static Mouse GetInstance()
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

		/** 位置。
		*/
		public Mouse_Pos pos;

		/** ボタン。
		*/
		public Mouse_Button left;
		public Mouse_Button right;

		/** マウスホイール。
		*/
		public float mouse_wheel;
		public float mouse_wheel_old;
		public bool mouse_wheel_action;

		/** [シングルトン]constructor
		*/
		private Mouse()
		{
			//位置。
			this.pos.Reset();

			//ボタン。
			this.left.Reset();
			this.right.Reset();

			//ホイール。
			this.mouse_wheel = 0.0f;
			this.mouse_wheel_old = 0.0f;
			this.mouse_wheel_action = false;
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** マウス表示。設定。
		*/
		public void SetVisible(bool a_flag)
		{
			Cursor.visible = a_flag;
		}

		/** マウスロック。設定。
		*/
		public void SetLock(bool a_flag)
		{
			if(a_flag == true){
				Cursor.lockState = CursorLockMode.Locked;
			}else{
				Cursor.lockState = CursorLockMode.None;
			}
		}

		/** [外部からの呼び出し]更新。
		*/
		public void Main(NRender2D.Render2D a_render2d)
		{
			try{
				{
					//（ＧＵＩスクリーン座標）=>（仮想スクリーン座標）。
					int t_x;
					int t_y;
					a_render2d.GuiScreenToVirtualScreen((int)UnityEngine.Input.mousePosition.x,(int)(Screen.height - UnityEngine.Input.mousePosition.y),out t_x,out t_y);

					//設定。
					this.pos.Set(t_x,t_y);
				}

				//設定。
				this.left.Set(UnityEngine.Input.GetMouseButton(0));
				this.right.Set(UnityEngine.Input.GetMouseButton(1));

				//マウスホイール。
				{
					this.mouse_wheel_old = this.mouse_wheel;

					if(Config.MOUSE_INPUTNAME_WHEEL != null){
						try{
							this.mouse_wheel = UnityEngine.Input.GetAxis(Config.MOUSE_INPUTNAME_WHEEL);
						}catch(System.Exception /*t_exception*/){
							//インプットマネージャで登録が必要。
							Tool.Log("Mouse","ERROR : " + Config.MOUSE_INPUTNAME_WHEEL);
							Config.MOUSE_INPUTNAME_WHEEL = null;
						}
					}

					if((this.mouse_wheel != 0.0f)&&(this.mouse_wheel != this.mouse_wheel_old)){
						this.mouse_wheel_action = true;
					}else{
						this.mouse_wheel_action = false;
					}
				}

				//更新。
				this.left.Main(ref this.pos);
				this.right.Main(ref this.pos);
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}
	}
}

