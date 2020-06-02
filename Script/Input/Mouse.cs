

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。マウス。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** Mouse
	*/
	public class Mouse
	{
		/** カーソル。
		*/
		public Status_Mouse_Cursor cursor;

		/** ボタン。
		*/
		public Status_Mouse_Button left;
		public Status_Mouse_Button right;
		public Status_Mouse_Button middle;

		/** マウスホイール。
		*/
		public Status_Mouse_Wheel mouse_wheel;

		/** constructor
		*/
		public Mouse()
		{
			//カーソル。
			this.cursor.Reset();

			//ボタン。
			this.left.Reset();
			this.right.Reset();
			this.middle.Reset();

			//ホイール。
			this.mouse_wheel.Reset();
		}

		/** 削除。
		*/
		public void Delete()
		{
		}

		/** マウス表示。設定。
		*/
		public void SetVisible(bool a_flag)
		{
			UnityEngine.Cursor.visible = a_flag;
		}

		/** マウスロック。設定。
		*/
		public void SetLock(bool a_flag)
		{
			if(a_flag == true){
				UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.Locked;
			}else{
				UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.None;
			}
		}

		/** 更新。位置。
		*/
		private void Main_Pos()
		{
			//インプットシステム。マウス。マウス位置。
			#if(USE_DEF_FEE_INPUTSYSTEM)
			if(Config.USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION == true){
				if(Mouse_Position_InputSystem_Mouse.Main() == true){
					return;
				}
			}
			#endif

			//インプットシステム。ポインター。マウス位置。
			#if(USE_DEF_FEE_INPUTSYSTEM)
			if(Config.USE_INPUTSYSTEM_POINTER_MOUSEPOSITION == true){
				if(Mouse_Position_InputSystem_Pointer.Main() == true){
					return;
				}
			}
			#endif

			//インプットマネージャ。インプットマウス。マウス位置。
			if(Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEPOSITION == true){
				if(Mouse_Position_InputManager_InputMouse.Main() == true){
					return;
				}
			}
		}

		/** 更新。ボタン。
		*/
		private void Main_Button()
		{
			//インプットシステム。マウス。マウスボタン。
			#if(USE_DEF_FEE_INPUTSYSTEM)
			if(Config.USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON == true){
				if(Mouse_MouseButton_InputSystem_Mouse.Main() == true){
					return;
				}
			}
			#endif

			//インプットシステム。ポインター。マウスボタン。
			#if(USE_DEF_FEE_INPUTSYSTEM)
			if(Config.USE_INPUTSYSTEM_POINTER_MOUSEBUTTON == true){
				if(Mouse_MouseButton_InputSystem_Pointer.Main() == true){
					return;
				}
			}
			#endif

			//インプットマネージャ。インプットマウス。マウスボタン。
			if(Config.USE_INPUTMANAGER_INPUTMOUSE_MOUSEBUTTON == true){
				if(Mouse_MouseButton_InputManager_InputMouse.Main() == true){
					return;
				}
			}
		}

		/** 更新。ホイール。
		*/
		private void Main_Wheel()
		{
			//インプットシステム。マウス。マウスホイール。
			#if(USE_DEF_FEE_INPUTSYSTEM)
			if(Config.USE_INPUTSYSTEM_MOUSE_MOUSEWHEEL == true){
				if(Mouse_MouseWheel_InputSystem_Mouse.Main() == true){
					return;
				}
			}
			#endif

			//インプットマネージャ。インプットネーム。マウスホイール。
			if(Config.USE_INPUTMANAGER_INPUTNAME_MOUSEWHEEL == true){
				if(Mouse_MouseWheel_InputManager_InputMouse.Main() == true){
					return;
				}
			}
		}

		/** 更新。
		*/
		public void Main()
		{
			//位置。
			this.Main_Pos();

			//ボタン。
			this.Main_Button();

			//マウスホイール。
			this.Main_Wheel();

			//更新。
			this.left.Main(in this.cursor);
			this.right.Main(in this.cursor);
			this.middle.Main(in this.cursor);
			this.mouse_wheel.Main();
		}
	}
}

