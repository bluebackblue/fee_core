using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ブラー。
*/


/** NBlur
*/
namespace NBlur
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

		/** [シングルトン]インスタンス。取得。
		*/
		public static Blur GetInstance()
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

		/** ルート。
		*/
		private GameObject root_gameobject;

		/** monobehaviour_camera
		*/
		private GameObject camera_gameobject;
		private MonoBehaviour_Camera camera_monobehaviour;

		/** [シングルトン]constructor
		*/
		private Blur()
		{
			//ルート。
			this.root_gameobject = new GameObject();
			this.root_gameobject.name = "Blur";
			Transform t_root_transform = this.root_gameobject.GetComponent<Transform>();
			GameObject.DontDestroyOnLoad(this.root_gameobject);

			//プレハブ読み込み。
			GameObject t_prefab_camera = Resources.Load<GameObject>(Config.PREFAB_NAME_CAMERA);

			{
				//カメラ。
				this.camera_gameobject = GameObject.Instantiate(t_prefab_camera,Vector3.zero,Quaternion.identity);
				this.camera_gameobject.name = "Camera";
				this.camera_gameobject.transform.SetParent(t_root_transform);
				Camera t_camera = this.camera_gameobject.GetComponent<Camera>();
				t_camera.depth = 999.0f;

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
	}
}

