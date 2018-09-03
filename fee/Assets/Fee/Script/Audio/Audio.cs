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
		private GameObject audiosource_se_gameobject;
		private MonoBehaviour_AudioSource_Se audiosource_se_script;

		/** ボリューム。マスター。
		*/
		private Volume volume_master;

		/** ボリューム。ＳＥ。
		*/
		private Volume volume_se;

		/** [シングルトン]constructor
		*/
		private Audio()
		{
			//ボリューム。マスター。
			this.volume_master = new Volume(Config.DEFAULT_VOLUME_MASTER);

			//ボリューム。ＳＥ。
			this.volume_se = new Volume(Config.DEFAULT_VOLUME_SE);

			//ルート。
			this.root_gameobject = new GameObject();
			this.root_gameobject.name = "Audio";
			Transform t_root_transform = this.root_gameobject.GetComponent<Transform>();
			GameObject.DontDestroyOnLoad(this.root_gameobject);

			//オーディオソース。ＳＥ。
			{
				this.audiosource_se_gameobject = new GameObject();
				this.audiosource_se_gameobject.name = "Se";
				this.audiosource_se_gameobject.transform.SetParent(t_root_transform);
				this.audiosource_se_gameobject.AddComponent<AudioSource>();
				this.audiosource_se_script = this.audiosource_se_gameobject.AddComponent<MonoBehaviour_AudioSource_Se>();
				this.audiosource_se_script.Initialize(this.volume_master,this.volume_se);
			}
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.audiosource_se_script.Delete();
			GameObject.Destroy(this.root_gameobject);
		}

		/** 再生。
		*/
		public void PlaySe(AudioClip a_audioclip)
		{
			this.audiosource_se_script.PlayOneShot(a_audioclip);
		}

		/** マスターボリューム。設定。
		*/
		public void SetMasterVolume(float a_volume)
		{
			this.volume_master.SetVolume(a_volume);
			this.audiosource_se_script.UpdateVolume();
		}

		/** ＳＥボリューム。設定。
		*/
		public void SetSeVolume(float a_volume)
		{
			this.volume_se.SetVolume(a_volume);
			this.audiosource_se_script.UpdateVolume();
		}

		/** マスターボリューム。取得。
		*/
		public float GetMasterVolume()
		{
			return this.volume_master.GetVolume();
		}

		/** ＳＥボリューム。取得。
		*/
		public float GetSeVolume()
		{
			return this.volume_se.GetVolume();
		}
	}
}

