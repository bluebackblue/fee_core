

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief パフォーマンスカウンター。コンフィグ。
*/


/** Fee.PerformanceCounter

	https://docs.unity3d.com/ja/current/Manual/ExecutionOrder.html

*/
namespace Fee.PerformanceCounter
{
	/** PerformanceCounter
	*/
	public class PerformanceCounter : Config
	{
		/** [シングルトン]s_instance
		*/
		private static PerformanceCounter s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new PerformanceCounter();
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
		public static PerformanceCounter GetInstance()
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

		/** ルート。
		*/
		private UnityEngine.GameObject root_gameobject;

		/** camera_gameobject
		*/
		private UnityEngine.GameObject camera_gameobject;
		private MonoBehaviour_Camera camera_script;

		/** フレームデータ。
		*/
		private FrameData framedata;

		/** [シングルトン]constructor
		*/
		private PerformanceCounter()
		{
			//ルート。
			this.root_gameobject = new UnityEngine.GameObject();
			this.root_gameobject.name = "PerformanceCounter";
			UnityEngine.Transform t_root_transform = this.root_gameobject.GetComponent<UnityEngine.Transform>();
			UnityEngine.GameObject.DontDestroyOnLoad(this.root_gameobject);

			//フレームデータ。
			this.framedata = new FrameData();

			//カメラ。
			this.camera_gameobject = Fee.Instantiate.Instantiate.CreateOrthographicCameraObject("Camera",t_root_transform,999.0f);
			this.camera_script = this.camera_gameobject.AddComponent<MonoBehaviour_Camera>();
			this.camera_script.Initialize();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.camera_script.Delete();
			UnityEngine.GameObject.Destroy(this.root_gameobject);
		}

		/** フレームデータ。取得。
		*/
		public FrameData GetFrameData()
		{
			return this.framedata;
		}
	}
}

