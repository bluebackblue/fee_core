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

		/** ボリューム。マスター。
		*/
		private Volume volume_master;

		/** ボリューム。ＳＥ。
		*/
		private Volume volume_se;

		/** ボリューム。ＢＧＭ。
		*/
		private Volume volume_bgm;

		/** ルート。
		*/
		private GameObject root_gameobject;

		/** ＳＥ。オーディオソース。
		*/
		private GameObject se_audiosource_gameobject;
		private MonoBehaviour_AudioSource_Se se_audiosource_script;

		/** ＢＧＭ。オーディオソース。
		*/
		private GameObject bgm_audiosource_gameobject;
		private MonoBehaviour_AudioSource_Bgm bgm_audiosource_script;
		
		/** [シングルトン]constructor
		*/
		private Audio()
		{
			//ボリューム。マスター。
			this.volume_master = new Volume(Config.DEFAULT_VOLUME_MASTER);

			//ボリューム。ＳＥ。
			this.volume_se = new Volume(Config.DEFAULT_VOLUME_SE);

			//ボリューム。ＢＧＭ。
			this.volume_bgm = new Volume(Config.DEFAULT_VOLUME_BGM);

			//ルート。
			this.root_gameobject = new GameObject();
			this.root_gameobject.name = "Audio";
			Transform t_root_transform = this.root_gameobject.GetComponent<Transform>();
			GameObject.DontDestroyOnLoad(this.root_gameobject);

			//オーディオソース。ＳＥ。
			{
				this.se_audiosource_gameobject = new GameObject();
				this.se_audiosource_gameobject.name = "Se";
				this.se_audiosource_gameobject.transform.SetParent(t_root_transform);
				this.se_audiosource_gameobject.AddComponent<AudioSource>();
				this.se_audiosource_script = this.se_audiosource_gameobject.AddComponent<MonoBehaviour_AudioSource_Se>();
				this.se_audiosource_script.Initialize(this.volume_master,this.volume_se);
			}

			//オーディオソース。ＢＧＭ。
			{
				this.bgm_audiosource_gameobject = new GameObject();
				this.bgm_audiosource_gameobject.name = "Bgm";
				this.bgm_audiosource_gameobject.transform.SetParent(t_root_transform);
				this.bgm_audiosource_gameobject.AddComponent<AudioSource>();
				this.bgm_audiosource_gameobject.AddComponent<AudioSource>();
				this.bgm_audiosource_script = this.bgm_audiosource_gameobject.AddComponent<MonoBehaviour_AudioSource_Bgm>();
				this.bgm_audiosource_script.Initialize(this.volume_master,this.volume_bgm);
			}
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.se_audiosource_script.Delete();
			GameObject.Destroy(this.root_gameobject);
		}

		/** マスターボリューム。設定。
		*/
		public void SetMasterVolume(float a_volume)
		{
			this.volume_master.SetVolume(a_volume);
			this.se_audiosource_script.UpdateVolume();
			this.bgm_audiosource_script.UpdateVolume();
		}

		/** ＳＥボリューム。設定。
		*/
		public void SetSeVolume(float a_volume)
		{
			this.volume_se.SetVolume(a_volume);
			this.se_audiosource_script.UpdateVolume();
		}

		/** ＢＧＭボリューム。設定。
		*/
		public void SetBgmVolume(float a_volume)
		{
			this.volume_bgm.SetVolume(a_volume);
			this.bgm_audiosource_script.UpdateVolume();
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

		/** ＢＧＭボリューム。取得。
		*/
		public float GetBgmVolume()
		{
			return this.volume_bgm.GetVolume();
		}

		/** ＢＧＭ。ロード。
		*/
		public void LoadBgm(ClipPack a_clippack)
		{
			this.bgm_audiosource_script.SetClipPack(a_clippack);
		}

		/** ＳＥ。ロード。
		*/
		public void LoadSe(ClipPack a_clippack,long a_se_id)
		{
			this.se_audiosource_script.SetClipPack(a_clippack,a_se_id);
		}

		/** ＳＥ。アンロード。
		*/
		public void UnLoadSe(long a_se_id)
		{
			this.se_audiosource_script.UnSetClipPack(a_se_id);
		}

		/** ＳＥ。チェック。
		*/
		public bool IsExistSe(long a_se_id)
		{
			return this.se_audiosource_script.IsExistClipPack(a_se_id);
		}

		/** 再生。
		*/
		public void PlaySe(long a_se_id,int a_index)
		{
			this.se_audiosource_script.PlayOneShot(a_se_id,a_index);
		}

		/** ＢＧＭ。再生。
		*/
		public void PlayBgm(int a_index)
		{
			this.bgm_audiosource_script.PlayBgm(a_index);
		}
	}
}

