

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
	/** Key
	*/
	public class Key
	{
		/** list
		*/
		public System.Collections.Generic.Dictionary<Status_Key_Type,Status_Key_Item> list;

		/** constructor
		*/
		public Key()
		{
			//list
			this.list = new System.Collections.Generic.Dictionary<Status_Key_Type,Status_Key_Item>();
		}

		/** 削除。
		*/
		public void Delete()
		{
		}

		/** 登録。
		*/
		public void Regist(Status_Key_Type a_key_type)
		{
			this.list.Add(a_key_type,new Status_Key_Item(a_key_type));
		}

		/** 解除。
		*/
		public void UnRegist(Status_Key_Type a_key_type)
		{
			this.list.Remove(a_key_type);
		}

		/** 取得。
		*/
		public Status_Key_Item GetKey(Status_Key_Type a_key_type)
		{
			Status_Key_Item t_key_item;

			if(this.list.TryGetValue(a_key_type,out t_key_item) == true){
				return t_key_item;
			}

			return null;
		}

		/** 更新。キー。
		*/
		public void Main_Key()
		{
			//インプットシステム。キーボード。キー。
			#if(USE_DEF_FEE_INPUTSYSTEM)
			if(Config.USE_INPUTSYSTEM_KEYBOARD_KEY == true){
				if(Key_Button_InputSystem_KeyBoard.Main() == true){
					return;
				}
			}
			#endif

			//インプットマネージャ。ゲットキー。キー。
			if(Config.USE_INPUTMANAGER_GETKEY_KEY == true){
				if(Key_Button_InputManager_InputKey.Main() == true){
					return;
				}
			}
		}

		/** 更新。
		*/
		public void Main()
		{
			//更新。キー。
			this.Main_Key();

			//更新。
			foreach(System.Collections.Generic.KeyValuePair<Status_Key_Type,Status_Key_Item> t_pair in this.list){
				t_pair.Value.digital.Main();
			}
		}
	}
}

