using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief オーディオ。
*/


/** NAudio
*/
namespace NAudio
{
	/** Audio
	*/
	public class Audio : Config
	{
		/** [シングルトン]s_instance
		*/
		private static Audio s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Audio();
			}
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static Audio GetInstance()
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

		/** オーディオソース。
		*/
		private GameObject audiosource_gameobject;
		private MonoBehaviour_AudioSource audiosource_script;

		/** [シングルトン]constructor
		*/
		private Audio()
		{
			//ルート。
			this.root_gameobject = new GameObject();
			this.root_gameobject.name = "Audio";
			Transform t_root_transform = this.root_gameobject.GetComponent<Transform>();
			GameObject.DontDestroyOnLoad(this.root_gameobject);

			{
				this.audiosource_gameobject = new GameObject();
				this.audiosource_gameobject.name = "AudioSource";
				this.audiosource_gameobject.transform.SetParent(t_root_transform);

				this.audiosource_gameobject.AddComponent<AudioSource>();
				this.audiosource_script = this.audiosource_gameobject.AddComponent<MonoBehaviour_AudioSource>();
				this.audiosource_script.Initialize();
			}
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.audiosource_script.Delete();
			GameObject.Destroy(this.root_gameobject);
		}

		/** 再生。
		*/
		public void PlayOneShot(AudioClip a_audioclip)
		{
			this.audiosource_script.PlayOneShot(a_audioclip);
		}
	}
}

