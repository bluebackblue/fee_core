using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 入力。キー。
*/


/** NInput
*/
namespace NInput
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

		/** [シングルトン]インスタンス。取得。
		*/
		public static Key GetInstance()
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

		/** ボタン。
		*/
		public Key_Button left;
		public Key_Button right;
		public Key_Button up;
		public Key_Button down;
		public Key_Button enter;
		public Key_Button escape;
		public Key_Button sub1;
		public Key_Button sub2;

		/** [シングルトン]constructor
		*/
		private Key()
		{
			/** ボタン。
			*/
			this.left.Reset();
			this.right.Reset();
			this.up.Reset();
			this.down.Reset();
			this.enter.Reset();
			this.escape.Reset();
			this.sub1.Reset();
			this.sub2.Reset();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** 更新。
		*/
		public void Main()
		{
			try{
				//設定。
				UnityEngine.Experimental.Input.Keyboard t_key_current = UnityEngine.Experimental.Input.Keyboard.current;

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

					//設定。
					this.enter.Set(t_enter_on);
					this.escape.Set(t_escape_on);
					this.sub1.Set(t_sub1_on);
					this.sub2.Set(t_sub2_on);
					this.left.Set(t_left_on);
					this.right.Set(t_right_on);
					this.up.Set(t_up_on);
					this.down.Set(t_down_on);
				}

				//更新。
				this.left.Main();
				this.right.Main();
				this.up.Main();
				this.down.Main();
				this.enter.Main();
				this.escape.Main();
				this.sub1.Main();
				this.sub2.Main();
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

