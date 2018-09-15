using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief パフォーマンスカウンター。コンフィグ。
*/


/** NPerformanceCounter

	https://docs.unity3d.com/ja/current/Manual/ExecutionOrder.html

*/
namespace NPerformanceCounter
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

		/** [シングルトン]インスタンス。取得。
		*/
		public static PerformanceCounter GetInstance()
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

		/** camera_gameobject
		*/
		private GameObject camera_gameobject;

		/** フレームデータ。
		*/
		private FrameData framedata;

		/** [シングルトン]constructor
		*/
		private PerformanceCounter()
		{
			//ルート。
			this.root_gameobject = new GameObject();
			this.root_gameobject.name = "PerformanceCounter";
			Transform t_root_transform = this.root_gameobject.GetComponent<Transform>();
			GameObject.DontDestroyOnLoad(this.root_gameobject);

			//フレームデータ。
			this.framedata = new FrameData();

			//カメラ。
			this.camera_gameobject = NInstantiate.Instantiate.CreateOrthographicCameraObject("Camera",t_root_transform,999.0f);
			MonoBehaviour_Camera t_script = this.camera_gameobject.AddComponent<MonoBehaviour_Camera>();
			t_script.Initialize();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			GameObject.Destroy(this.root_gameobject);
		}

		/** フレームデータ。取得。
		*/
		public FrameData GetFrameData()
		{
			return this.framedata;
		}
	}
}

