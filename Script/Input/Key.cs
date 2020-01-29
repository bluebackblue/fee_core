

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

		/** list
		*/
		public System.Collections.Generic.Dictionary<Key_Type,Key_Item> list;

		/** [シングルトン]constructor
		*/
		private Key()
		{
			//is_focus
			this.is_focus = false;

			//list
			this.list = new System.Collections.Generic.Dictionary<Key_Type,Key_Item>();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** 登録。
		*/
		public void Regist(Key_Type a_key_type)
		{
			this.list.Add(a_key_type,new Key_Item(a_key_type));
		}

		/** 解除。
		*/
		public void UnRegist(Key_Type a_key_type)
		{
			this.list.Remove(a_key_type);
		}

		/** 取得。
		*/
		public Key_Item GetKey(Key_Type a_key_type)
		{
			Key_Item t_key_item;

			if(this.list.TryGetValue(a_key_type,out t_key_item) == true){
				return t_key_item;
			}

			return null;
		}

		/** 更新。インプットシステム。キーボード。キー。
		*/
		public bool Main_InputSystem_KeyBoard_Key()
		{	
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine_InputSystem.Keyboard t_key_current = UnityEngine_InputSystem.InputSystem.GetDevice<UnityEngine_InputSystem.Keyboard>();
				if(t_key_current != null){
					foreach(System.Collections.Generic.KeyValuePair<Key_Type,Key_Item> t_pair in this.list){
						t_pair.Value.digital.Set(t_key_current[t_pair.Value.inputsystem_key].isPressed);
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
			foreach(System.Collections.Generic.KeyValuePair<Key_Type,Key_Item> t_pair in this.list){
				t_pair.Value.digital.Set(UnityEngine.Input.GetKey(t_pair.Value.inputmanager_key));
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
				foreach(System.Collections.Generic.KeyValuePair<Key_Type,Key_Item> t_pair in this.list){
					t_pair.Value.digital.Main();
				}

			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

