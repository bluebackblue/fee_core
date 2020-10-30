

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
	public class Scene : Fee.Function.UnityUpdate_CallBackInterface<int> , Fee.Function.UnityLateUpdate_CallBackInterface<int>
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

		/** gameobject
		*/
		private UnityEngine.GameObject gameobject;

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

		/** playerloop_flag
		*/
		private bool playerloop_flag;

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

			//PlayerLoopType
			this.playerloop_flag = true;
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Config.PLAYERLOOP_ADDTYPE,Config.PLAYERLOOP_TARGETTYPE,typeof(PlayerLoopType.Fee_Scene_Main),this.Main);

			//gameobject
			this.gameobject = new UnityEngine.GameObject("scene");
			UnityEngine.GameObject.DontDestroyOnLoad(this.gameobject);
			this.gameobject.AddComponent<Fee.Function.UnityUpdate_MonoBehaviour>().SetCallBack(this,0);
			this.gameobject.AddComponent<Fee.Function.UnityLateUpdate_MonoBehaviour>().SetCallBack(this,0);
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			//playerloop_flag
			this.playerloop_flag = false;

			//PlayerLoopType
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof(PlayerLoopType.Fee_Scene_Main));

			//gameobject
			UnityEngine.GameObject.Destroy(this.gameobject);
			this.gameobject = null;
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

		/** [Fee.Graphic.UnityUpdate_CallBackInterface]UnityUpdate
		*/
		public void UnityUpdate(int a_id)
		{
			if(this.is_scene == true){
				this.current.Unity_Update();
			}
		}

		/** [Fee.Graphic.UnityLateUpdate_CallBackInterface]UnityLateUpdate
		*/
		public void UnityLateUpdate(int a_id)
		{
			if(this.is_scene == true){
				this.current.Unity_LateUpdate();
			}
		}

		/** Main
		*/
		private void Main()
		{
			try{
				if(this.playerloop_flag == true){
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
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

