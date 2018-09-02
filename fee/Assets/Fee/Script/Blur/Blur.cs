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
		private MonoBehaviour_Camera monobehaviour_camera;

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
				GameObject t_gameobject_camera = GameObject.Instantiate(t_prefab_camera,Vector3.zero,Quaternion.identity);
				t_gameobject_camera.name = "Camera";
				t_gameobject_camera.transform.SetParent(t_root_transform);
				Camera t_camera = t_gameobject_camera.GetComponent<Camera>();
				t_camera.depth = 999.0f;

				//OnRenderImage
				this.monobehaviour_camera = t_gameobject_camera.AddComponent<MonoBehaviour_Camera>();
				this.monobehaviour_camera.Initialize();
			}
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.monobehaviour_camera.Delete();
			GameObject.Destroy(this.root_gameobject);
		}

		/** カメラデプス。設定。
		*/
		public void SetCameraDepth(float a_depth)
		{
			this.monobehaviour_camera.mycamera.depth = a_depth;
		}
	}
}

