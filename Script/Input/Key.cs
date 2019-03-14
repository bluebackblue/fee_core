

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 入力。キー。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** Key
	*/
	public class Key
	{
		/** [シングルトン]s_instance
		*/
		private static Key s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Key();
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
		public static Key GetInstance()
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

		/** ボタン。
		*/
		public Digital_Button left;
		public Digital_Button right;
		public Digital_Button up;
		public Digital_Button down;
		public Digital_Button enter;
		public Digital_Button escape;
		public Digital_Button sub1;
		public Digital_Button sub2;
		public Digital_Button left_menu;
		public Digital_Button right_menu;

		/** [シングルトン]constructor
		*/
		private Key()
		{
			//ボタン。
			this.left.Reset();
			this.right.Reset();
			this.up.Reset();
			this.down.Reset();
			this.enter.Reset();
			this.escape.Reset();
			this.sub1.Reset();
			this.sub2.Reset();
			this.left_menu.Reset();
			this.right_menu.Reset();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** 更新。インプットシステムキー。
		*/
		public bool Main_InputSystemKey_Key()
		{	
			#if(USE_DEF_INPUTSYSTEM)
			{
				UnityEngine.Experimental.Input.Keyboard t_key_current = UnityEngine.Experimental.Input.InputSystem.GetDevice<UnityEngine.Experimental.Input.Keyboard>();
				if(t_key_current != null){
					//デバイス。
					bool t_enter_on = t_key_current[Config.KEY_ENTER].isPressed;
					bool t_escape_on = t_key_current[Config.KEY_ESCAPE].isPressed;
					bool t_sub1_on = t_key_current[Config.KEY_SUB1].isPressed;
					bool t_sub2_on = t_key_current[Config.KEY_SUB2].isPressed;
					bool t_left_on = t_key_current[Config.KEY_LEFT].isPressed;
					bool t_right_on = t_key_current[Config.KEY_RIGHT].isPressed;
					bool t_up_on = t_key_current[Config.KEY_UP].isPressed;
					bool t_down_on = t_key_current[Config.KEY_DOWN].isPressed;
					bool t_left_menu_on = t_key_current[Config.KEY_LEFT_MENU].isPressed;
					bool t_right_menu_on = t_key_current[Config.KEY_RIGHT_MENU].isPressed;

					//設定。
					this.enter.Set(t_enter_on);
					this.escape.Set(t_escape_on);
					this.sub1.Set(t_sub1_on);
					this.sub2.Set(t_sub2_on);
					this.left.Set(t_left_on);
					this.right.Set(t_right_on);
					this.up.Set(t_up_on);
					this.down.Set(t_down_on);
					this.left_menu.Set(t_left_menu_on);
					this.right_menu.Set(t_right_menu_on);

					return true;
				}
			}
			#endif

			return false;
		}

		/** 更新。インプットマネージャキーボード。
		*/
		public bool Main_InputManagerKey_Key()
		{
			//デバイス。
			bool t_enter_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.Return);
			bool t_escape_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.Escape);
			bool t_sub1_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftShift);
			bool t_sub2_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftControl);
			bool t_left_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.A);
			bool t_right_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.D);
			bool t_up_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.W);
			bool t_down_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.S);
			bool t_left_menu_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.Space);
			bool t_right_menu_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.Backspace);

			//設定。
			this.enter.Set(t_enter_on);
			this.escape.Set(t_escape_on);
			this.sub1.Set(t_sub1_on);
			this.sub2.Set(t_sub2_on);
			this.left.Set(t_left_on);
			this.right.Set(t_right_on);
			this.up.Set(t_up_on);
			this.down.Set(t_down_on);
			this.left_menu.Set(t_left_menu_on);
			this.right_menu.Set(t_right_menu_on);

			return true;	
		}

		/** 更新。キー。
		*/
		public void Main_Key()
		{
			//インプットシステム。キー。
			if(Config.USE_INPUTSYSTEM_KEY == true){
				if(this.Main_InputSystemKey_Key() == true){
					return;
				}
			}

			//インプットマネージャ。キー。
			if(Config.USE_INPUTMANAGER_KEY == true){
				if(this.Main_InputManagerKey_Key() == true){
					return;
				}
			}
		}

		/** 更新。
		*/
		public void Main()
		{
			try{
				//更新。キー。
				this.Main_Key();

				//更新。
				this.enter.Main();
				this.escape.Main();
				this.sub1.Main();
				this.sub2.Main();
				this.left.Main();
				this.right.Main();
				this.up.Main();
				this.down.Main();
				this.left_menu.Main();
				this.right_menu.Main();
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}

		/** 移動チェック。ダウン時。
		*/
		public Dir4Type DownMoveCheck()
		{
			if(this.up.down == true){
				return Dir4Type.Up;
			}else if(this.down.down == true){
				return Dir4Type.Down;
			}else if(this.left.down == true){
				return Dir4Type.Left;
			}else if(this.right.down == true){
				return Dir4Type.Right;
			}
			return Dir4Type.None;
		}

		/** 移動チェック。オン時。
		*/
		public Dir4Type OnMoveCheck()
		{
			if(this.up.on == true){
				return Dir4Type.Up;
			}else if(this.down.on == true){
				return Dir4Type.Down;
			}else if(this.left.on == true){
				return Dir4Type.Left;
			}else if(this.right.on == true){
				return Dir4Type.Right;
			}
			return Dir4Type.None;
		}

	}
}

