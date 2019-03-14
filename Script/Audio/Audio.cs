

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief オーディオ。
*/


/** Fee.Audio
*/
namespace Fee.Audio
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
		public static Audio GetInstance()
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

		/** サウンドプール。
		*/
		private SoundPool soundpool;

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
		private UnityEngine.GameObject root_gameobject;

		/** ＳＥ。オーディオソース。
		*/
		private UnityEngine.GameObject se_audiosource_gameobject;
		private MonoBehaviour_AudioSource_Se se_audiosource_script;

		/** ＢＧＭ。オーディオソース。
		*/
		private UnityEngine.GameObject bgm_audiosource_gameobject;
		private MonoBehaviour_AudioSource_Bgm bgm_audiosource_script;

		/** [シングルトン]constructor
		*/
		private Audio()
		{
			//サウンドプール。
			this.soundpool = new SoundPool();

			//ボリューム。マスター。
			this.volume_master = new Volume(Config.DEFAULT_VOLUME_MASTER);

			//ボリューム。ＳＥ。
			this.volume_se = new Volume(Config.DEFAULT_VOLUME_SE);

			//ボリューム。ＢＧＭ。
			this.volume_bgm = new Volume(Config.DEFAULT_VOLUME_BGM);

			//ルート。
			this.root_gameobject = new UnityEngine.GameObject();
			this.root_gameobject.name = "Audio";
			UnityEngine.Transform t_root_transform = this.root_gameobject.GetComponent<UnityEngine.Transform>();
			UnityEngine.GameObject.DontDestroyOnLoad(this.root_gameobject);

			//オーディオソース。ＳＥ。
			{
				this.se_audiosource_gameobject = new UnityEngine.GameObject();
				this.se_audiosource_gameobject.name = "Se";
				this.se_audiosource_gameobject.transform.SetParent(t_root_transform);
				this.se_audiosource_gameobject.AddComponent<UnityEngine.AudioSource>();
				this.se_audiosource_script = this.se_audiosource_gameobject.AddComponent<MonoBehaviour_AudioSource_Se>();
				this.se_audiosource_script.Initialize(this.volume_master,this.volume_se);
			}

			//オーディオソース。ＢＧＭ。
			{
				this.bgm_audiosource_gameobject = new UnityEngine.GameObject();
				this.bgm_audiosource_gameobject.name = "Bgm";
				this.bgm_audiosource_gameobject.transform.SetParent(t_root_transform);
				this.bgm_audiosource_gameobject.AddComponent<UnityEngine.AudioSource>();
				this.bgm_audiosource_gameobject.AddComponent<UnityEngine.AudioSource>();
				this.bgm_audiosource_script = this.bgm_audiosource_gameobject.AddComponent<MonoBehaviour_AudioSource_Bgm>();
				this.bgm_audiosource_script.Initialize(this.volume_master,this.volume_bgm);
			}
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.se_audiosource_script.Delete();
			this.se_audiosource_script = null;

			this.soundpool.Delete();
			this.soundpool = null;			

			UnityEngine.GameObject.Destroy(this.root_gameobject);
		}

		/** サウンドプール。
		*/
		public SoundPool GetSoundPool()
		{
			return this.soundpool;
		}

		/** サウンドプール数。取得。
		*/
		public int GetSoundPoolCount()
		{
			return this.soundpool.GetCount();
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
		public void LoadBgm(Pack_AudioClip a_pack)
		{
			this.bgm_audiosource_script.SetBank(new Bank(a_pack));
		}

		/** ＳＥ。ロード。
		*/
		public void LoadSe(Pack_AudioClip a_pack,long a_se_id)
		{
			this.se_audiosource_script.SetPack(a_pack,a_se_id);
		}

		/** ＳＥ。ロード。
		*/
		public void LoadSe(Pack_SoundPool a_pack,long a_se_id)
		{
			this.se_audiosource_script.SetPack(a_pack,a_se_id);
		}

		/** ＳＥ。アンロード。
		*/
		public void UnLoadSe(long a_se_id)
		{
			this.se_audiosource_script.UnSetBank(a_se_id);
		}

		/** ＳＥ。チェック。
		*/
		public bool IsExistSe(long a_se_id)
		{
			return this.se_audiosource_script.IsExistBank(a_se_id);
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

		/** ＢＧＭ。停止。
		*/
		public void StopBgm()
		{
			this.bgm_audiosource_script.PlayBgm(-1);
		}

		/** ＢＧＭ数。取得。
		*/
		public int GetBgmMax()
		{
			return this.bgm_audiosource_script.GetBgmMax();
		}

		/** ＢＧＭループカウント。取得。
		*/
		public int GetBgmLoopCount()
		{
			return this.bgm_audiosource_script.GetLoopCount();
		}

		/** ＢＧＭ再生位置。取得。
		*/
		public float GetBgmPlayPosition()
		{
			return this.bgm_audiosource_script.GetPlayPosition();
		}
	}
}

