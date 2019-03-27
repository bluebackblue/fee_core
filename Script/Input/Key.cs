

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
		public Digital_Button l_left;
		public Digital_Button l_right;
		public Digital_Button l_up;
		public Digital_Button l_down;

		public Digital_Button r_left;
		public Digital_Button r_right;
		public Digital_Button r_up;
		public Digital_Button r_down;

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
			this.l_left.Reset();
			this.l_right.Reset();
			this.l_up.Reset();
			this.l_down.Reset();
			this.r_left.Reset();
			this.r_right.Reset();
			this.r_up.Reset();
			this.r_down.Reset();
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
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine.Experimental.Input.Keyboard t_key_current = UnityEngine.Experimental.Input.InputSystem.GetDevice<UnityEngine.Experimental.Input.Keyboard>();
				if(t_key_current != null){
					//デバイス。
					bool t_l_left_on = t_key_current[Config.INPUTSYSTEM_LEFT].isPressed;
					bool t_l_right_on = t_key_current[Config.INPUTSYSTEM_RIGHT].isPressed;
					bool t_l_up_on = t_key_current[Config.INPUTSYSTEM_UP].isPressed;
					bool t_l_down_on = t_key_current[Config.INPUTSYSTEM_DOWN].isPressed;
					bool t_r_left_on = t_key_current[Config.INPUTSYSTEM_LEFT].isPressed;
					bool t_r_right_on = t_key_current[Config.INPUTSYSTEM_RIGHT].isPressed;
					bool t_r_up_on = t_key_current[Config.INPUTSYSTEM_UP].isPressed;
					bool t_r_down_on = t_key_current[Config.INPUTSYSTEM_DOWN].isPressed;
					bool t_enter_on = t_key_current[Config.INPUTSYSTEM_ENTER].isPressed;
					bool t_escape_on = t_key_current[Config.INPUTSYSTEM_ESCAPE].isPressed;
					bool t_sub1_on = t_key_current[Config.INPUTSYSTEM_SUB1].isPressed;
					bool t_sub2_on = t_key_current[Config.INPUTSYSTEM_SUB2].isPressed;
					bool t_left_menu_on = t_key_current[Config.INPUTSYSTEM_LEFT_MENU].isPressed;
					bool t_right_menu_on = t_key_current[Config.INPUTSYSTEM_RIGHT_MENU].isPressed;

					//設定。
					this.l_left.Set(t_l_left_on);
					this.l_right.Set(t_l_right_on);
					this.l_up.Set(t_l_up_on);
					this.l_down.Set(t_l_down_on);
					this.r_left.Set(t_r_left_on);
					this.r_right.Set(t_r_right_on);
					this.r_up.Set(t_r_up_on);
					this.r_down.Set(t_r_down_on);
					this.enter.Set(t_enter_on);
					this.escape.Set(t_escape_on);
					this.sub1.Set(t_sub1_on);
					this.sub2.Set(t_sub2_on);
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
			bool t_l_left_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.A);
			bool t_l_right_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.D);
			bool t_l_up_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.W);
			bool t_l_down_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.S);
			bool t_r_left_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftArrow);
			bool t_r_right_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.RightArrow);
			bool t_r_up_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.UpArrow);
			bool t_r_down_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.DownArrow);
			bool t_enter_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.Return);
			bool t_escape_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.Escape);
			bool t_sub1_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftShift);
			bool t_sub2_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftControl);
			bool t_left_menu_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.Space);
			bool t_right_menu_on = UnityEngine.Input.GetKey(UnityEngine.KeyCode.Backspace);

			//設定。
			this.l_left.Set(t_l_left_on);
			this.l_right.Set(t_l_right_on);
			this.l_up.Set(t_l_up_on);
			this.l_down.Set(t_l_down_on);
			this.r_left.Set(t_r_left_on);
			this.r_right.Set(t_r_right_on);
			this.r_up.Set(t_r_up_on);
			this.r_down.Set(t_r_down_on);
			this.enter.Set(t_enter_on);
			this.escape.Set(t_escape_on);
			this.sub1.Set(t_sub1_on);
			this.sub2.Set(t_sub2_on);
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
				this.l_left.Main();
				this.l_right.Main();
				this.l_up.Main();
				this.l_down.Main();
				this.r_left.Main();
				this.r_right.Main();
				this.r_up.Main();
				this.r_down.Main();
				this.enter.Main();
				this.escape.Main();
				this.sub1.Main();
				this.sub2.Main();
				this.left_menu.Main();
				this.right_menu.Main();
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}
	}
}

