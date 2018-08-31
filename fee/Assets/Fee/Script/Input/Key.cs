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
				this.left.Set(UnityEngine.Input.GetKey(Config.KEY_LEFT));
				this.right.Set(UnityEngine.Input.GetKey(Config.KEY_RIGHT));
				this.up.Set(UnityEngine.Input.GetKey(Config.KEY_UP));
				this.down.Set(UnityEngine.Input.GetKey(Config.KEY_DOWN));
				this.enter.Set(UnityEngine.Input.GetKey(KeyCode.Return));
				this.escape.Set(UnityEngine.Input.GetKey(KeyCode.Escape));

				//更新。
				this.left.Main();
				this.right.Main();
				this.up.Main();
				this.down.Main();
				this.enter.Main();
				this.escape.Main();
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}
	}
}

