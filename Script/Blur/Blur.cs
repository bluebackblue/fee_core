

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ブラー。
*/


/** Fee.Blur
*/
namespace Fee.Blur
{
	/** Blur
	*/
	public class Blur : Config
	{
		/** [シングルトン]s_instance
		*/
		private static Blur s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Blur();
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
		public static Blur GetInstance()
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

		/** monobehaviour_camera
		*/
		private UnityEngine.GameObject camera_gameobject;
		private MonoBehaviour_Camera camera_monobehaviour;

		/** [シングルトン]constructor
		*/
		private Blur()
		{
			//ルート。
			this.root_gameobject = new UnityEngine.GameObject();
			this.root_gameobject.name = "Blur";
			UnityEngine.Transform t_root_transform = this.root_gameobject.GetComponent<UnityEngine.Transform>();
			UnityEngine.GameObject.DontDestroyOnLoad(this.root_gameobject);

			{
				//カメラ。
				this.camera_gameobject = Fee.Instantiate.Instantiate.CreateOrthographicCameraObject("Camera",t_root_transform,Config.DEFAULT_CAMERA_DEPTH);

				//OnRenderImage
				this.camera_monobehaviour = this.camera_gameobject.AddComponent<MonoBehaviour_Camera>();
				this.camera_monobehaviour.Initialize();

				//無効。
				this.camera_gameobject.SetActive(false);
			}
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.camera_monobehaviour.Delete();
			UnityEngine.GameObject.Destroy(this.root_gameobject);
		}

		/** カメラデプス。設定。
		*/
		public void SetCameraDepth(float a_depth)
		{
			this.camera_monobehaviour.SetCameraDepth(a_depth);
		}

		/** 有効。設定。
		*/
		public void SetEnable(bool a_bool)
		{
			this.camera_gameobject.SetActive(a_bool);
		}
	}
}

