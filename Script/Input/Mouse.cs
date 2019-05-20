

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 入力。マウス。
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
		public static Mouse GetInstance()
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

		/** screen
		*/
		public int screen_w;
		public int screen_h;

		/** is_focus
		*/
		public bool is_focus;

		/** 位置。
		*/
		public Mouse_Pos pos;

		/** ボタン。
		*/
		public Mouse_Button left;
		public Mouse_Button right;
		public Mouse_Button middle;

		/** マウスホイール。
		*/
		public Mouse_Pos mouse_wheel;

		/** [シングルトン]constructor
		*/
		private Mouse()
		{
			//screen_w
			this.screen_w = UnityEngine.Screen.width;
			this.screen_h = UnityEngine.Screen.height;

			//is_focus
			this.is_focus = false;

			//位置。
			this.pos.Reset();

			//ボタン。
			this.left.Reset();
			this.right.Reset();
			this.middle.Reset();

			//ホイール。
			this.mouse_wheel.Reset();
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

		/** 更新。インプットシステムポインター。位置。
		*/
		private bool Main_InputSystemPointer_Position(Fee.Render2D.Render2D a_render2d)
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine_InputSystem.Pointer t_pointer_current = UnityEngine_InputSystem.InputSystem.GetDevice<UnityEngine_InputSystem.Pointer>();
				if(t_pointer_current != null){
					//デバイス。
					int t_pointer_x = (int)t_pointer_current.position.x.ReadValue();

					#if((UNITY_STANDALONE_WIN)||(UNITY_EDITOR_WIN))
					int t_pointer_y = (int)(this.screen_h - t_pointer_current.position.y.ReadValue());
					#else
					int t_pointer_y = (int)(t_pointer_current.position.y.ReadValue());
					#endif

					//resolution
					{
						t_pointer_x = (int)((float)t_pointer_x * UnityEngine.Screen.width / this.screen_w);
						t_pointer_y = (int)((float)t_pointer_y * UnityEngine.Screen.height / this.screen_h);
					}

					#if(UNITY_EDITOR)
					{
						t_pointer_x += Config.MOUSE_EDITOR_OFFSET_X;
						t_pointer_y += Config.MOUSE_EDITOR_OFFSET_Y;
					}
					#endif

					//（ＧＵＩスクリーン座標）=>（仮想スクリーン座標）。
					int t_x;
					int t_y;
					a_render2d.GuiScreenToVirtualScreen(t_pointer_x,t_pointer_y,out t_x,out t_y);

					//設定。
					if(this.is_focus == true){
						this.pos.Set(t_x,t_y);
					}else{
						this.pos.Set(this.pos.x_old,this.pos.y_old);
					}

					return true;
				}
			}
			#endif

			return false;
		}

		/** 更新。インプットシステムマウス。位置。
		*/
		private bool Main_InputSystemMouse_Position(Fee.Render2D.Render2D a_render2d)
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine_InputSystem.Mouse t_mouse_current = UnityEngine_InputSystem.InputSystem.GetDevice<UnityEngine_InputSystem.Mouse>();
				if(t_mouse_current != null){
					//デバイス。
					int t_mouse_x = (int)t_mouse_current.position.x.ReadValue();

					#if((UNITY_STANDALONE_WIN)||(UNITY_EDITOR_WIN))
					int t_mouse_y = (int)(this.screen_h - t_mouse_current.position.y.ReadValue());
					#else
					int t_mouse_y = (int)(t_mouse_current.position.y.ReadValue());
					#endif

					//resolution
					{
						t_mouse_x = (int)((float)t_mouse_x * UnityEngine.Screen.width / this.screen_w);
						t_mouse_y = (int)((float)t_mouse_y * UnityEngine.Screen.height / this.screen_h);
					}

					#if(UNITY_EDITOR)
					{
						t_mouse_x += Config.MOUSE_EDITOR_OFFSET_X;
						t_mouse_y += Config.MOUSE_EDITOR_OFFSET_Y;
					}
					#endif

					//（ＧＵＩスクリーン座標）=>（仮想スクリーン座標）。
					int t_x;
					int t_y;
					a_render2d.GuiScreenToVirtualScreen(t_mouse_x,t_mouse_y,out t_x,out t_y);

					//設定。
					if(this.is_focus == true){
						this.pos.Set(t_x,t_y);
					}else{
						this.pos.Set(this.pos.x_old,this.pos.y_old);
					}

					return true;
				}
			}
			#endif

			return false;
		}

		/** 更新。インプットマネージャマウス。位置。
		*/
		#if(true)
		private bool Main_InputManagerMouse_Position(Fee.Render2D.Render2D a_render2d)
		{
			//デバイス。
			int t_mouse_x = (int)UnityEngine.Input.mousePosition.x;
			int t_mouse_y = UnityEngine.Screen.height - (int)UnityEngine.Input.mousePosition.y;

			//resolution
			{
				t_mouse_x = (int)((float)t_mouse_x * UnityEngine.Screen.width / this.screen_w);
				t_mouse_y = (int)((float)t_mouse_y * UnityEngine.Screen.height / this.screen_h);
			}

			#if(UNITY_EDITOR)
			{
				t_mouse_x += Config.MOUSE_EDITOR_OFFSET_X;
				t_mouse_y += Config.MOUSE_EDITOR_OFFSET_Y;
			}
			#endif

			//（ＧＵＩスクリーン座標）=>（仮想スクリーン座標）。
			int t_x;
			int t_y;
			a_render2d.GuiScreenToVirtualScreen(t_mouse_x,t_mouse_y,out t_x,out t_y);

			//設定。
			if(this.is_focus == true){
				this.pos.Set(t_x,t_y);
			}else{
				this.pos.Set(this.pos.x_old,this.pos.y_old);
			}

			return true;
		}
		#endif

		/** 更新。インプットシステムポインター。ボタン。
		*/
		private bool Main_InputSystemPointer_Button()
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine_InputSystem.Pointer t_pointer_current = UnityEngine_InputSystem.InputSystem.GetDevice<UnityEngine_InputSystem.Pointer>();
				if(t_pointer_current != null){
					bool t_l_on = this.left.on;

					//デバイス。
					switch(t_pointer_current.phase.ReadValue()){
					case UnityEngine_InputSystem.PointerPhase.Began:
						{
							//開始。
							t_l_on = true;
						}break;
					case UnityEngine_InputSystem.PointerPhase.Ended:
					#if(UNITY_2018_3)
					case UnityEngine_InputSystem.PointerPhase.Cancelled:
					#else
					case UnityEngine_InputSystem.PointerPhase.Canceled:
					#endif
						{
							//終了。
							t_l_on = false;
						}break;
					case UnityEngine_InputSystem.PointerPhase.Moved:
					case UnityEngine_InputSystem.PointerPhase.None:
					case UnityEngine_InputSystem.PointerPhase.Stationary:
						{
							//保留。
						}break;
					}

					//設定。
					this.left.Set(t_l_on & this.is_focus);
					this.right.Set(false & this.is_focus);
					this.middle.Set(false & this.is_focus);

					//設定。
					return true;
				}
			}
			#endif

			return false;
		}

		/** 更新。インプットシステムマウス。ボタン。
		*/
		private bool Main_InputSystemMouse_Button()
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine_InputSystem.Mouse t_mouse_current = UnityEngine_InputSystem.InputSystem.GetDevice<UnityEngine_InputSystem.Mouse>();
				if(t_mouse_current != null){
					//デバイス。
					bool t_l_on = t_mouse_current.leftButton.isPressed;
					bool t_r_on = t_mouse_current.rightButton.isPressed;
					bool t_m_on = t_mouse_current.middleButton.isPressed;

					//設定。
					this.left.Set(t_l_on & this.is_focus);
					this.right.Set(t_r_on & this.is_focus);
					this.middle.Set(t_m_on & this.is_focus);

					return true;
				}
			}
			#endif

			return false;
		}

		/** 更新。インプットマネージャマウス。ボタン。
		*/
		#if(true)
		private bool Main_InputManagerMouse_Button()
		{
			//デバイス。
			bool t_l_on = UnityEngine.Input.GetMouseButton(0);
			bool t_r_on = UnityEngine.Input.GetMouseButton(1);
			bool t_m_on = UnityEngine.Input.GetMouseButton(2);

			//設定。
			this.left.Set(t_l_on & this.is_focus);
			this.right.Set(t_r_on & this.is_focus);
			this.middle.Set(t_m_on & this.is_focus);

			return true;
		}
		#endif

		/** 更新。インプットシステムマウス。ホイール。
		*/
		private bool Main_InputSystemMouse_Wheel()
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine_InputSystem.Mouse t_mouse_current = UnityEngine_InputSystem.InputSystem.GetDevice<UnityEngine_InputSystem.Mouse>();
				if(t_mouse_current != null){
					//デバイス。
					int t_x = (int)t_mouse_current.scroll.ReadValue().x;
					int t_y = (int)t_mouse_current.scroll.ReadValue().y;

					//設定。
					if(this.is_focus == true){
						this.mouse_wheel.Set(t_x,t_y);
					}else{
						this.mouse_wheel.Set(0,0);
					}

					return true;
				}
			}
			#endif

			return false;
		}		

		/** 更新。インプットマネージャマウス。ホイール。
		*/
		#if(true)
		private bool Main_InputManagerMouse_Wheel()
		{
			//デバイス。
			float t_wheel = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_MOUSEWHEEL);

			//設定。
			if(this.is_focus == true){
				if(t_wheel > 0.0f){
					this.mouse_wheel.Set(0,20);
				}else if(t_wheel < 0.0f){
					this.mouse_wheel.Set(0,-20);
				}else{
					this.mouse_wheel.Set(0,0);
				}
			}else{
				this.mouse_wheel.Set(0,0);
			}

			return false;
		}
		#endif

		/** 更新。位置。
		*/
		private void Main_Pos(Fee.Render2D.Render2D a_render2d)
		{
			//インプットシステム。マウス。
			if(Config.USE_INPUTSYSTEM_MOUSE == true){
				if(this.Main_InputSystemMouse_Position(a_render2d) == true){
					return;
				}
			}

			//インプットシステム。ポインター。
			if(Config.USE_INPUTSYSTEM_POINTER == true){
				if(this.Main_InputSystemPointer_Position(a_render2d) == true){
					return;
				}
			}

			//インプットマネージャ。マウス。
			if(Config.USE_INPUTMANAGER_MOUSE == true){
				if(this.Main_InputManagerMouse_Position(a_render2d) == true){
					return;
				}
			}
		}

		/** 更新。ボタン。
		*/
		private void Main_Button()
		{
			//インプットシステムマウス。
			if(Config.USE_INPUTSYSTEM_MOUSE == true){
				if(this.Main_InputSystemMouse_Button() == true){
					return;
				}
			}

			//インプットシステムポインター。
			if(Config.USE_INPUTSYSTEM_POINTER == true){
				if(this.Main_InputSystemPointer_Button() == true){
					return;
				}
			}

			//インプットマネージャマウス。
			if(Config.USE_INPUTMANAGER_MOUSE == true){
				if(this.Main_InputManagerMouse_Button() == true){
					return;
				}
			}
		}

		/** 更新。ホイール。
		*/
		private void Main_Wheel()
		{
			//インプットシステムマウス。
			if(Config.USE_INPUTSYSTEM_MOUSE == true){
				if(this.Main_InputSystemMouse_Wheel() == true){
					return;
				}
			}

			//インプットマネージャマウス。
			if(Config.USE_INPUTMANAGER_MOUSE == true){
				if(this.Main_InputManagerMouse_Wheel() == true){
					return;
				}
			}
		}

		/** 更新。
		*/
		public void Main(bool a_is_focus,Fee.Render2D.Render2D a_render2d)
		{
			//is_focus
			this.is_focus = a_is_focus;

			//screen_w
			#if(UNITY_EDITOR)||(UNITY_WEBGL)
			{
				this.screen_w = UnityEngine.Screen.width;
				this.screen_h = UnityEngine.Screen.height;
			}
			#endif

			try{
				//位置。
				this.Main_Pos(a_render2d);

				//ボタン。
				this.Main_Button();

				//マウスホイール。
				this.Main_Wheel();

				//更新。
				this.left.Main(ref this.pos);
				this.right.Main(ref this.pos);
				this.middle.Main(ref this.pos);
				this.mouse_wheel.Main();
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}

		/** 範囲チェック。
		*/
		public bool InRectCheck(int a_x,int a_y,int a_w,int a_h)
		{
			if((a_x <= this.pos.x) && (a_y <= this.pos.y)){
				if(((a_x + a_w) >= this.pos.x) && ((a_y + a_h) >= this.pos.y)){
					return true;
				}
			}
			return false;
		}

		/** 範囲チェック。
		*/
		public bool InRectCheck(ref Fee.Render2D.Rect2D_R<int> a_rect)
		{
			if((a_rect.x <= this.pos.x) && (a_rect.y <= this.pos.y)){
				if(((a_rect.x + a_rect.w) >= this.pos.x) && ((a_rect.y + a_rect.h) >= this.pos.y)){
					return true;
				}
			}
			return false;
		}

		/** 移動チェック。左ボタンドラッグアップ時。
		*/
		public Dir4Type LeftDragUpMoveCheck()
		{
			if((this.left.up == true)&&(this.left.drag_dir_magnitude >= Config.DRAGUP_LENGTH_MIN)&&(this.left.drag_totallength <= (this.left.drag_dir_magnitude * Config.DRAGUP_LENGTH_SCALE))){
				{
					float t_dot = UnityEngine.Vector2.Dot(this.left.drag_dir_normalized,UnityEngine.Vector2.down);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Up;
					}
				}

				{
					float t_dot = UnityEngine.Vector2.Dot(this.left.drag_dir_normalized,UnityEngine.Vector2.up);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Down;
					}
				}

				{
					float t_dot = UnityEngine.Vector2.Dot(this.left.drag_dir_normalized,UnityEngine.Vector2.left);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Left;
					}
				}

				{
					float t_dot = UnityEngine.Vector2.Dot(this.left.drag_dir_normalized,UnityEngine.Vector2.right);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Right;
					}
				}
			}

			return Dir4Type.None;
		}

		/** 移動チェック。左ボタンドラッグオン時。
		*/
		public Dir4Type LeftDragOnMoveCheck()
		{
			if((this.left.on == true)&&(this.left.drag_dir_magnitude >= Config.DRAGON_LENGTH_MIN)){
				{
					float t_dot = UnityEngine.Vector2.Dot(this.left.drag_dir_normalized,UnityEngine.Vector2.down);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Up;
					}
				}

				{
					float t_dot = UnityEngine.Vector2.Dot(this.left.drag_dir_normalized,UnityEngine.Vector2.up);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Down;
					}
				}

				{
					float t_dot = UnityEngine.Vector2.Dot(this.left.drag_dir_normalized,UnityEngine.Vector2.left);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Left;
					}
				}

				{
					float t_dot = UnityEngine.Vector2.Dot(this.left.drag_dir_normalized,UnityEngine.Vector2.right);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Right;
					}
				}
			}

			return Dir4Type.None;
		}

		/** 移動チェック。左ボタンドラッグアップ時。
		*/
		public Dir4Type RightDragUpMoveCheck()
		{
			if((this.right.up == true)&&(this.right.drag_dir_magnitude >= Config.DRAGUP_LENGTH_MIN)&&(this.right.drag_totallength <= (this.right.drag_dir_magnitude * Config.DRAGUP_LENGTH_SCALE))){
				{
					float t_dot = UnityEngine.Vector2.Dot(this.right.drag_dir_normalized,UnityEngine.Vector2.down);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Up;
					}
				}

				{
					float t_dot = UnityEngine.Vector2.Dot(this.right.drag_dir_normalized,UnityEngine.Vector2.up);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Down;
					}
				}

				{
					float t_dot = UnityEngine.Vector2.Dot(this.right.drag_dir_normalized,UnityEngine.Vector2.left);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Left;
					}
				}

				{
					float t_dot = UnityEngine.Vector2.Dot(this.right.drag_dir_normalized,UnityEngine.Vector2.right);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Right;
					}
				}
			}

			return Dir4Type.None;
		}

		/** 移動チェック。左ボタンドラッグオン時。
		*/
		public Dir4Type RightDragOnMoveCheck()
		{
			if((this.right.on == true)&&(this.right.drag_dir_magnitude >= Config.DRAGON_LENGTH_MIN)){
				{
					float t_dot = UnityEngine.Vector2.Dot(this.right.drag_dir_normalized,UnityEngine.Vector2.down);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Up;
					}
				}

				{
					float t_dot = UnityEngine.Vector2.Dot(this.right.drag_dir_normalized,UnityEngine.Vector2.up);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Down;
					}
				}

				{
					float t_dot = UnityEngine.Vector2.Dot(this.right.drag_dir_normalized,UnityEngine.Vector2.left);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Left;
					}
				}

				{
					float t_dot = UnityEngine.Vector2.Dot(this.right.drag_dir_normalized,UnityEngine.Vector2.right);
					if(t_dot >= Config.DRAG_DIR4_DOT){
						return Dir4Type.Right;
					}
				}
			}

			return Dir4Type.None;
		}
	}
}

