

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief シーン。
*/


/** Fee.Scene
*/
namespace Fee.Scene
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

		/** is_scene
		*/
		private bool is_scene;

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

			//is_scene
			this.is_scene = false;

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
		public void Unity_Update(float a_delta)
		{
			if(this.is_scene == true){
				this.current.Unity_Update(a_delta);
			}
		}

		/** 更新。
		*/
		public void Unity_LateUpdate(float a_delta)
		{
			if(this.is_scene == true){
				this.current.Unity_LateUpdate(a_delta);
			}
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
							this.is_scene = true;
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
							this.is_scene = false;
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

