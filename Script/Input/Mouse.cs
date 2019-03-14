

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
			#if(USE_DEF_INPUTSYSTEM)
			{
				UnityEngine.Experimental.Input.Pointer t_pointer_current = UnityEngine.Experimental.Input.InputSystem.GetDevice<UnityEngine.Experimental.Input.Pointer>();
				if(t_pointer_current != null){
					//デバイス。
					int t_pointer_x = (int)t_pointer_current.position.x.ReadValue();

					#if((UNITY_STANDALONE_WIN)||(UNITY_EDITOR_WIN))
					int t_pointer_y = (int)(Screen.height - t_pointer_current.position.y.ReadValue());
					#else
					int t_pointer_y = (int)(t_pointer_current.position.y.ReadValue());
					#endif

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
					this.pos.Set(t_x,t_y);

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
			#if(USE_DEF_INPUTSYSTEM)
			{
				UnityEngine.Experimental.Input.Mouse t_mouse_current = UnityEngine.Experimental.Input.InputSystem.GetDevice<UnityEngine.Experimental.Input.Mouse>();
				if(t_mouse_current != null){
					//デバイス。
					int t_mouse_x = (int)t_mouse_current.position.x.ReadValue();

					#if((UNITY_STANDALONE_WIN)||(UNITY_EDITOR_WIN))
					int t_mouse_y = (int)(Screen.height - t_mouse_current.position.y.ReadValue());
					#else
					int t_mouse_y = (int)(t_mouse_current.position.y.ReadValue());
					#endif

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
					this.pos.Set(t_x,t_y);

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
			this.pos.Set(t_x,t_y);

			return true;
		}
		#endif

		/** 更新。インプットシステムポインター。ボタン。
		*/
		private bool Main_InputSystemPointer_Button()
		{
			#if(USE_DEF_INPUTSYSTEM)
			{
				UnityEngine.Experimental.Input.Pointer t_pointer_current = UnityEngine.Experimental.Input.InputSystem.GetDevice<UnityEngine.Experimental.Input.Pointer>();
				if(t_pointer_current != null){
					bool t_l_on = this.left.on;

					//デバイス。
					switch(t_pointer_current.phase.ReadValue()){
					case UnityEngine.Experimental.Input.PointerPhase.Began:
						{
							//開始。
							t_l_on = true;
						}break;
					case UnityEngine.Experimental.Input.PointerPhase.Ended:
					case UnityEngine.Experimental.Input.PointerPhase.Cancelled:
						{
							//終了。
							t_l_on = false;
						}break;
					case UnityEngine.Experimental.Input.PointerPhase.Moved:
					case UnityEngine.Experimental.Input.PointerPhase.None:
					case UnityEngine.Experimental.Input.PointerPhase.Stationary:
						{
							//保留。
						}break;
					}

					//設定。
					this.left.Set(t_l_on);
					this.right.Set(false);
					this.middle.Set(false);

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
			#if(USE_DEF_INPUTSYSTEM)
			{
				UnityEngine.Experimental.Input.Mouse t_mouse_current = UnityEngine.Experimental.Input.InputSystem.GetDevice<UnityEngine.Experimental.Input.Mouse>();
				if(t_mouse_current != null){
					//デバイス。
					bool t_l_on = t_mouse_current.leftButton.isPressed;
					bool t_r_on = t_mouse_current.rightButton.isPressed;
					bool t_m_on = t_mouse_current.middleButton.isPressed;

					//設定。
					this.left.Set(t_l_on);
					this.right.Set(t_r_on);
					this.middle.Set(t_m_on);

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
			this.left.Set(t_l_on);
			this.right.Set(t_r_on);
			this.middle.Set(t_m_on);

			return true;
		}
		#endif

		/** 更新。インプットシステムマウス。ホイール。
		*/
		private bool Main_InputSystemMouse_Wheel()
		{
			#if(USE_DEF_INPUTSYSTEM)
			{
				UnityEngine.Experimental.Input.Mouse t_mouse_current = UnityEngine.Experimental.Input.InputSystem.GetDevice<UnityEngine.Experimental.Input.Mouse>();
				if(t_mouse_current != null){
					//デバイス。
					int t_x = (int)t_mouse_current.scroll.ReadValue().x;
					int t_y = (int)t_mouse_current.scroll.ReadValue().y;

					//設定。
					this.mouse_wheel.Set(t_x,t_y);

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
			float t_wheel = UnityEngine.Input.GetAxis("Mouse ScrollWheel");

			//設定。
			if(t_wheel > 0.0f){
				this.mouse_wheel.Set(0,20);
			}else if(t_wheel < 0.0f){
				this.mouse_wheel.Set(0,-20);
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
		public void Main(Fee.Render2D.Render2D a_render2d)
		{
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

