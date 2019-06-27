

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 深度。
*/


/** Fee.Depth
*/
namespace Fee.Depth
{
	/** Depth
	*/
	public class Depth : Config
	{
		/** [シングルトン]s_instance
		*/
		private static Depth s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Depth();
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
		public static Depth GetInstance()
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

		/** flag
		*/
		private bool flag;

		/** [シングルトン]constructor
		*/
		private Depth()
		{
			//ルート。
			this.root_gameobject = new UnityEngine.GameObject();
			this.root_gameobject.name = "Depth";
			UnityEngine.Transform t_root_transform = this.root_gameobject.GetComponent<UnityEngine.Transform>();
			UnityEngine.GameObject.DontDestroyOnLoad(this.root_gameobject);

			{
				//カメラ。
				this.camera_gameobject = Fee.Instantiate.Instantiate.CreateOrthographicCameraObject("Camera",t_root_transform,Config.DEFAULT_CAMERA_DEPTH);

				//OnRenderImage
				this.camera_monobehaviour = this.camera_gameobject.AddComponent<MonoBehaviour_Camera>();
				this.camera_monobehaviour.Initialize();

				//無効。
				this.flag = false;
				this.camera_gameobject.SetActive(this.flag);
			}
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.camera_monobehaviour.Delete();
			UnityEngine.GameObject.Destroy(this.root_gameobject);
		}

		/** カメラ深度。設定。
		*/
		public void SetCameraDepth(float a_depth)
		{
			this.camera_monobehaviour.SetCameraDepth(a_depth);
		}

		/** カメラ深度。取得。
		*/
		public float GetCameraDepth()
		{
			return this.camera_monobehaviour.GetCameraDepth();
		}

		/** 有効。設定。
		*/
		public void SetEnable(bool a_bool)
		{
			this.flag = a_bool;
			this.camera_gameobject.SetActive(this.flag);
		}

		/** 有効。取得。
		*/
		public bool IsEnable()
		{
			return this.flag;
		}

		/** 深度テクスチャ。設定。
		*/
		public void SetDepthTexture(UnityEngine.RenderTexture a_rendertexture_depth)
		{
			this.camera_monobehaviour.SetDepthTexture(a_rendertexture_depth);
		}

		/** ブレンド比率。設定。
		*/
		public void SetBlendRate(float a_blend)
		{
			this.camera_monobehaviour.SetBlendRate(a_blend);
		}

		/** ブレンド比率。取得。
		*/
		public float GetBlendRate()
		{
			return this.camera_monobehaviour.GetBlendRate();
		}
	}
}

