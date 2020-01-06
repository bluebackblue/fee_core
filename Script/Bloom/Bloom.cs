

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ブルーム。
*/


/** Fee.Bloom
*/
namespace Fee.Bloom
{
	/** Bloom
	*/
	public class Bloom : Config
	{
		/** [シングルトン]s_instance
		*/
		private static Bloom s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Bloom();
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
		public static Bloom GetInstance()
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

		/** camera
		*/
		private UnityEngine.GameObject camera_gameobject;
		private Camera_MonoBehaviour camera_monobehaviour;

		/** flag
		*/
		private bool flag;

		/** [シングルトン]constructor
		*/
		private Bloom()
		{
			//ルート。
			this.root_gameobject = new UnityEngine.GameObject();
			this.root_gameobject.name = "Bloom";
			UnityEngine.Transform t_root_transform = this.root_gameobject.GetComponent<UnityEngine.Transform>();
			UnityEngine.GameObject.DontDestroyOnLoad(this.root_gameobject);

			{
				//カメラ。
				this.camera_gameobject = Fee.Instantiate.Instantiate.CreateOrthographicCameraObject("Camera",t_root_transform,Config.DEFAULT_CAMERA_DEPTH);

				//OnRenderImage
				this.camera_monobehaviour = this.camera_gameobject.AddComponent<Camera_MonoBehaviour>();
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

		/** 輝度抽出閾値。設定。
		*/
		public void SetThreshold(float a_threshold)
		{
			this.camera_monobehaviour.SetThreshold(a_threshold);
		}

		/** 輝度抽出閾値。設定。
		*/
		public float GetThreshold()
		{
			return this.camera_monobehaviour.GetThreshold();
		}

		/** 加算強度。設定。
		*/
		public void SetIntensity(float a_intensity)
		{
			this.camera_monobehaviour.SetIntensity(a_intensity);
		}

		/** 加算強度。設定。
		*/
		public float GetIntensity()
		{
			return this.camera_monobehaviour.GetIntensity();
		}
	}
}

