

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。キー。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** UnityEngine_InputSystem
	*/
	#if(USE_DEF_FEE_INPUTSYSTEM)
		#if((UNITY_2018_3)||(UNITY_2018_4))
			using UnityEngine_InputSystem = UnityEngine.Experimental.Input;
		#else
			using UnityEngine_InputSystem = UnityEngine.InputSystem;
		#endif
	#endif

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

		/** is_focus
		*/
		public bool is_focus;

		//デジタル。
		public Digital_Button left;
		public Digital_Button right;
		public Digital_Button up;
		public Digital_Button down;

		//左アナログ。
		public Digital_Button l_left;
		public Digital_Button l_right;
		public Digital_Button l_up;
		public Digital_Button l_down;

		//右アナログ。
		public Digital_Button r_left;
		public Digital_Button r_right;
		public Digital_Button r_up;
		public Digital_Button r_down;

		//エンター。エスケープ。サブ１。サブ２。
		public Digital_Button enter;
		public Digital_Button escape;
		public Digital_Button sub1;
		public Digital_Button sub2;

		//左メニュー。右メニュー。
		public Digital_Button left_menu;
		public Digital_Button right_menu;

		//左トリガー１。右トリガー１。左トリガー２。右トリガー２。
		public Digital_Button l_trigger_1;
		public Digital_Button r_trigger_1;
		public Digital_Button l_trigger_2;
		public Digital_Button r_trigger_2;

		/** [シングルトン]constructor
		*/
		private Key()
		{
			//is_focus
			this.is_focus = false;

			//デジタル。
			this.left.Reset();
			this.right.Reset();
			this.up.Reset();
			this.down.Reset();

			//左アナログ。
			this.l_left.Reset();
			this.l_right.Reset();
			this.l_up.Reset();
			this.l_down.Reset();

			//右アナログ。
			this.r_left.Reset();
			this.r_right.Reset();
			this.r_up.Reset();
			this.r_down.Reset();

			//エンター。エスケープ。サブ１。サブ２。
			this.enter.Reset();
			this.escape.Reset();
			this.sub1.Reset();
			this.sub2.Reset();

			//メニュー。
			this.left_menu.Reset();
			this.right_menu.Reset();

			//トリガー。
			this.l_trigger_1.Reset();
			this.r_trigger_1.Reset();
			this.l_trigger_2.Reset();
			this.r_trigger_2.Reset();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** 更新。インプットシステム。キーボード。キー。
		*/
		public bool Main_InputSystem_KeyBoard_Key()
		{	
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine_InputSystem.Keyboard t_key_current = UnityEngine_InputSystem.InputSystem.GetDevice<UnityEngine_InputSystem.Keyboard>();
				if(t_key_current != null){

					//デジタル。
					bool t_left_on = t_key_current[Config.INPUTSYSTEM_B_LEFT].isPressed;
					bool t_right_on = t_key_current[Config.INPUTSYSTEM_B_RIGHT].isPressed;
					bool t_up_on = t_key_current[Config.INPUTSYSTEM_B_UP].isPressed;
					bool t_down_on = t_key_current[Config.INPUTSYSTEM_B_DOWN].isPressed;

					//左アナログ。
					bool t_l_left_on = t_key_current[Config.INPUTSYSTEM_B_L_LEFT].isPressed;
					bool t_l_right_on = t_key_current[Config.INPUTSYSTEM_B_L_RIGHT].isPressed;
					bool t_l_up_on = t_key_current[Config.INPUTSYSTEM_B_L_UP].isPressed;
					bool t_l_down_on = t_key_current[Config.INPUTSYSTEM_B_L_DOWN].isPressed;

					//右アナログ。
					bool t_r_left_on = t_key_current[Config.INPUTSYSTEM_B_R_LEFT].isPressed;
					bool t_r_right_on = t_key_current[Config.INPUTSYSTEM_B_R_RIGHT].isPressed;
					bool t_r_up_on = t_key_current[Config.INPUTSYSTEM_B_R_UP].isPressed;
					bool t_r_down_on = t_key_current[Config.INPUTSYSTEM_B_R_DOWN].isPressed;

					//エンター。エスケープ。サブ１。サブ２。
					bool t_enter_on = t_key_current[Config.INPUTSYSTEM_B_ENTER].isPressed;
					bool t_escape_on = t_key_current[Config.INPUTSYSTEM_B_ESCAPE].isPressed;
					bool t_sub1_on = t_key_current[Config.INPUTSYSTEM_B_SUB1].isPressed;
					bool t_sub2_on = t_key_current[Config.INPUTSYSTEM_B_SUB2].isPressed;

					//メニュー。
					bool t_left_menu_on = t_key_current[Config.INPUTSYSTEM_B_LEFT_MENU].isPressed;
					bool t_right_menu_on = t_key_current[Config.INPUTSYSTEM_B_RIGHT_MENU].isPressed;

					//トリガー。
					bool t_l_trigger_1_on = t_key_current[Config.INPUTSYSTEM_B_L_TRIGGER_1].isPressed;
					bool t_r_trigger_1_on = t_key_current[Config.INPUTSYSTEM_B_R_TRIGGER_1].isPressed;
					bool t_l_trigger_2_on = t_key_current[Config.INPUTSYSTEM_B_L_TRIGGER_2].isPressed;
					bool t_r_trigger_2_on = t_key_current[Config.INPUTSYSTEM_B_R_TRIGGER_2].isPressed;

					{
						//デジタル。
						this.left.Set(this.is_focus & t_left_on);
						this.right.Set(this.is_focus & t_right_on);
						this.up.Set(this.is_focus & t_up_on);
						this.down.Set(this.is_focus & t_down_on);

						//左アナログ。
						this.l_left.Set(this.is_focus & t_l_left_on);
						this.l_right.Set(this.is_focus & t_l_right_on);
						this.l_up.Set(this.is_focus & t_l_up_on);
						this.l_down.Set(this.is_focus & t_l_down_on);

						//右アナログ。
						this.r_left.Set(this.is_focus & t_r_left_on);
						this.r_right.Set(this.is_focus & t_r_right_on);
						this.r_up.Set(this.is_focus & t_r_up_on);
						this.r_down.Set(this.is_focus & t_r_down_on);

						//エンター。エスケープ。サブ１。サブ２。
						this.enter.Set(this.is_focus & t_enter_on);
						this.escape.Set(this.is_focus & t_escape_on);
						this.sub1.Set(this.is_focus & t_sub1_on);
						this.sub2.Set(this.is_focus & t_sub2_on);

						//メニュー。
						this.left_menu.Set(this.is_focus & t_left_menu_on);
						this.right_menu.Set(this.is_focus & t_right_menu_on);

						//トリガー。
						this.l_trigger_1.Set(this.is_focus & t_l_trigger_1_on);
						this.r_trigger_1.Set(this.is_focus & t_r_trigger_1_on);
						this.l_trigger_2.Set(this.is_focus & t_l_trigger_2_on);
						this.r_trigger_2.Set(this.is_focus & t_r_trigger_2_on);
					}

					return true;
				}
			}
			#endif

			return false;
		}

		/** 更新。インプットマネージャ。ゲットキー。キー。
		*/
		public bool Main_InputManager_GetKey_Key()
		{
			//デジタル。
			bool t_left_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_LEFT);
			bool t_right_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_RIGHT);
			bool t_up_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_UP);
			bool t_down_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_DOWN);

			//左アナログ。
			bool t_l_left_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_L_LEFT);
			bool t_l_right_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_L_RIGHT);
			bool t_l_up_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_L_UP);
			bool t_l_down_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_L_DOWN);

			//右アナログ。
			bool t_r_left_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_R_LEFT);
			bool t_r_right_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_R_RIGHT);
			bool t_r_up_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_R_UP);
			bool t_r_down_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_R_DOWN);

			//エンター。エスケープ。サブ１。サブ２。
			bool t_enter_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_ENTER);
			bool t_escape_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_ESCAPE);
			bool t_sub1_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_SUB1);
			bool t_sub2_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_SUB2);

			//メニュー。
			bool t_left_menu_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_LEFT_MENU);
			bool t_right_menu_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_RIGHT_MENU);

			//トリガー。
			bool t_l_trigger_1_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_L_TRIGGER_1);
			bool t_r_trigger_1_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_R_TRIGGER_1);
			bool t_l_trigger_2_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_L_TRIGGER_2);
			bool t_r_trigger_2_on = UnityEngine.Input.GetKey(Config.INPUTMANAGER_B_R_TRIGGER_2);

			{
				//デジタル。
				this.left.Set(this.is_focus & t_left_on);
				this.right.Set(this.is_focus & t_right_on);
				this.up.Set(this.is_focus & t_up_on);
				this.down.Set(this.is_focus & t_down_on);

				//左アナログ。
				this.l_left.Set(this.is_focus & t_l_left_on);
				this.l_right.Set(this.is_focus & t_l_right_on);
				this.l_up.Set(this.is_focus & t_l_up_on);
				this.l_down.Set(this.is_focus & t_l_down_on);

				//右アナログ。
				this.r_left.Set(this.is_focus & t_r_left_on);
				this.r_right.Set(this.is_focus & t_r_right_on);
				this.r_up.Set(this.is_focus & t_r_up_on);
				this.r_down.Set(this.is_focus & t_r_down_on);

				//エンター。エスケープ。サブ１。サブ２。
				this.enter.Set(this.is_focus & t_enter_on);
				this.escape.Set(this.is_focus & t_escape_on);
				this.sub1.Set(this.is_focus & t_sub1_on);
				this.sub2.Set(this.is_focus & t_sub2_on);

				//メニュー。
				this.left_menu.Set(this.is_focus & t_left_menu_on);
				this.right_menu.Set(this.is_focus & t_right_menu_on);

				//トリガー。
				this.l_trigger_1.Set(this.is_focus & t_l_trigger_1_on);
				this.r_trigger_1.Set(this.is_focus & t_r_trigger_1_on);
				this.l_trigger_2.Set(this.is_focus & t_l_trigger_2_on);
				this.r_trigger_2.Set(this.is_focus & t_r_trigger_2_on);
			}

			return true;	
		}

		/** 更新。キー。
		*/
		public void Main_Key()
		{
			//インプットシステム。キーボード。キー。
			if(Config.USE_INPUTSYSTEM_KEYBOARD_KEY == true){
				if(this.Main_InputSystem_KeyBoard_Key() == true){
					return;
				}
			}

			//インプットマネージャ。ゲットキー。キー。
			if(Config.USE_INPUTMANAGER_GETKEY_KEY == true){
				if(this.Main_InputManager_GetKey_Key() == true){
					return;
				}
			}
		}

		/** 更新。
		*/
		public void Main(bool a_is_focus)
		{
			//フォーカス。
			this.is_focus = a_is_focus;

			try{
				//更新。キー。
				this.Main_Key();

				//更新。

				//デジタル。
				this.left.Main();
				this.right.Main();
				this.up.Main();
				this.down.Main();

				//左アナログ。
				this.l_left.Main();
				this.l_right.Main();
				this.l_up.Main();
				this.l_down.Main();

				//右アナログ。
				this.r_left.Main();
				this.r_right.Main();
				this.r_up.Main();
				this.r_down.Main();

				//エンター。エスケープ。サブ１。サブ２。
				this.enter.Main();
				this.escape.Main();
				this.sub1.Main();
				this.sub2.Main();

				//メニュー。
				this.left_menu.Main();
				this.right_menu.Main();

				//トリガー。
				this.l_trigger_1.Main();
				this.r_trigger_1.Main();
				this.l_trigger_2.Main();
				this.r_trigger_2.Main();

			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

