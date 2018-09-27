using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ブルーム。
*/


/** NBloom
*/
namespace NBloom
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
		private GameObject root_gameobject;

		/** monobehaviour_camera
		*/
		private GameObject camera_gameobject;
		private MonoBehaviour_Camera camera_monobehaviour;

		/** [シングルトン]constructor
		*/
		private Bloom()
		{
			//ルート。
			this.root_gameobject = new GameObject();
			this.root_gameobject.name = "Bloom";
			Transform t_root_transform = this.root_gameobject.GetComponent<Transform>();
			GameObject.DontDestroyOnLoad(this.root_gameobject);

			{
				//カメラ。
				this.camera_gameobject = NInstantiate.Instantiate.CreateOrthographicCameraObject("Camera",t_root_transform,Config.DEFAULT_CAMERA_DEPTH);

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
			GameObject.Destroy(this.root_gameobject);
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

		/** 輝度抽出閾値。設定。
		*/
		public void SetThreshold(float a_threshold)
		{
			/* TODO:
			this.camera_monobehaviour.threshold = a_threshold;

			if(this.camera_monobehaviour.threshold < 0.0f){
				this.camera_monobehaviour.threshold = 0.0f;
			}else if(this.camera_monobehaviour.threshold > 1.0f){
				this.camera_monobehaviour.threshold = 1.0f;
			}
			*/
		}

		/** 加算強度。設定。
		*/
		public void SetIntensity(float a_intensity)
		{
			//TODO:
			/*
			this.camera_monobehaviour.intensity = a_intensity;

			if(this.camera_monobehaviour.intensity < 0.0f){
				this.camera_monobehaviour.intensity = 0.0f;
			}
			*/
		}
	}
}

