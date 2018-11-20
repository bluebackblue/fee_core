using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief シーン。
*/


/** NScene
*/
namespace NScene
{
	/** Scene
	*/
	public class Scene
	{
		/** [シングルトン]s_instance
		*/
		private static Scene s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Scene();
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
		public static Scene GetInstance()
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

		/** current
		*/
		private Scene_Base current;

		/** request
		*/
		private Scene_Base request;

		/** mode
		*/
		enum Mode
		{
			/** リクエスト待ち。
			*/
			WaitRequest,

			/** メイン。
			*/
			Main,

			/** シーン終了。
			*/
			SceneEnd,
		};
		private Mode mode;

		/** [シングルトン]constructor
		*/
		private Scene()
		{
			//current
			this.current = null;

			//request
			this.request = null;

			//mode
			this.mode = Mode.WaitRequest;
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** 次のシーン。設定。
		*/
		public void SetNextScene(Scene_Base a_scene)
		{
			this.request = a_scene;
		}

		/** 次のシーン。チェック。
		*/
		public bool IsNextScene()
		{
			if(this.request != null){
				return true;
			}
			return false;
		}

		/** 処理。チェック。
		*/
		public bool IsBusy()
		{
			if((this.request == null)&&(this.current == null)&&(this.mode == Mode.WaitRequest)){
				return false;
			}
			return true;
		}

		/** 更新。
		*/
		public void Main()
		{
			switch(this.mode){
			case Mode.WaitRequest:
				{
					//リクエスト待ち。

					if(this.current == null){
						if(this.request != null){
							this.current = this.request;
							this.request = null;

							Tool.Log("Scene","this.current = this.request");
						}
					}

					if(this.current != null){
						if(this.current.SceneStart() == true){
							this.mode = Mode.Main;
						}
					}
				}break;
			case Mode.Main:
				{
					//メイン。

					if(this.current != null){
						if(this.current.Main() == true){
							this.mode = Mode.SceneEnd;
						}
					}
				}break;
			case Mode.SceneEnd:
				{
					//シーン終了

					if(this.current != null){
						if(this.current.SceneEnd() == true){
							this.current.Delete();
							this.current = null;
							this.mode = Mode.WaitRequest;

							Tool.Log("Scene","this.current = null;");
						}
					}
				}break;
			}
		}
	}
}

