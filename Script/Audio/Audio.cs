

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief オーディオ。
*/


/** Fee.Audio
*/
namespace Fee.Audio
{
	/** Audio
	*/
	public class Audio
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
		private Se_AudioSource_MonoBehaviour se_audiosource_monobehaviour;

		/** ＢＧＭ。オーディオソース。
		*/
		private UnityEngine.GameObject bgm_audiosource_gameobject;
		private Bgm_AudioSource_MonoBehaviour bgm_audiosource_monobehaviour;

		/** フォーカス。
		*/
		private bool is_focus;

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
				this.se_audiosource_monobehaviour = this.se_audiosource_gameobject.AddComponent<Se_AudioSource_MonoBehaviour>();
				this.se_audiosource_monobehaviour.Initialize(this.volume_master,this.volume_se);
			}

			//オーディオソース。ＢＧＭ。
			{
				this.bgm_audiosource_gameobject = new UnityEngine.GameObject();
				this.bgm_audiosource_gameobject.name = "Bgm";
				this.bgm_audiosource_gameobject.transform.SetParent(t_root_transform);
				this.bgm_audiosource_gameobject.AddComponent<UnityEngine.AudioSource>();
				this.bgm_audiosource_gameobject.AddComponent<UnityEngine.AudioSource>();
				this.bgm_audiosource_monobehaviour = this.bgm_audiosource_gameobject.AddComponent<Bgm_AudioSource_MonoBehaviour>();
				this.bgm_audiosource_monobehaviour.Initialize(this.volume_master,this.volume_bgm);
			}

			//フォーカス。
			this.is_focus = false;
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.se_audiosource_monobehaviour.Delete();
			this.se_audiosource_monobehaviour = null;

			UnityEngine.GameObject.Destroy(this.root_gameobject);
		}

		/** 更新。
		*/
		public void Main(bool a_is_focus)
		{
			if(this.is_focus != a_is_focus){
				this.is_focus = a_is_focus;

				float t_volume = this.GetBgmVolume();
				this.SetBgmVolume(t_volume);
			}
		}

		/** マスターボリューム。設定。
		*/
		public void SetMasterVolume(float a_volume)
		{
			this.volume_master.SetVolume(a_volume);
			this.se_audiosource_monobehaviour.UpdateVolume();
			this.bgm_audiosource_monobehaviour.UpdateVolume();
		}

		/** ＳＥボリューム。設定。
		*/
		public void SetSeVolume(float a_volume)
		{
			this.volume_se.SetVolume(a_volume);
			this.se_audiosource_monobehaviour.UpdateVolume();
		}

		/** ＢＧＭボリューム。設定。
		*/
		public void SetBgmVolume(float a_volume)
		{
			this.volume_bgm.SetVolume(a_volume);
			this.bgm_audiosource_monobehaviour.UpdateVolume();
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
		public bool LoadBgm(Bank a_bank)
		{
			return this.bgm_audiosource_monobehaviour.SetBank(a_bank);
		}

		/** ＢＧＭ。アンロード。
		*/
		public bool UnLoadBgm()
		{
			return this.bgm_audiosource_monobehaviour.UnSetBank();
		}

		/** ＳＥ。ロード。
		*/
		public void LoadSe(Bank a_bank,long a_se_id)
		{
			this.se_audiosource_monobehaviour.SetBank(a_bank,a_se_id);
		}

		/** ＳＥ。アンロード。
		*/
		public void UnLoadSe(long a_se_id)
		{
			this.se_audiosource_monobehaviour.UnSetBank(a_se_id);
		}

		/** ＳＥ。チェック。
		*/
		public bool IsExistSe(long a_se_id)
		{
			return this.se_audiosource_monobehaviour.IsExistBank(a_se_id);
		}

		/** 再生。
		*/
		public void PlaySe(long a_se_id,int a_index)
		{
			this.se_audiosource_monobehaviour.PlayOneShot(a_se_id,a_index);
		}

		/** 再生。
		*/
		public void PlaySe<T>(long a_se_id,T a_index)
			where T : struct
		{
			System.Object t_object = a_index;
			int t_index = (int)t_object;
			this.se_audiosource_monobehaviour.PlayOneShot(a_se_id,t_index);
		}

		/** ＢＧＭ。再生。
		*/
		public void PlayBgm(int a_index)
		{
			this.bgm_audiosource_monobehaviour.PlayBgm(a_index);
		}

		/** ＢＧＭ。再生。
		*/
		public void PlayBgm<T>(T a_index)
			where T : struct
		{
			System.Object t_object = a_index;
			int t_index = (int)t_object;
			this.bgm_audiosource_monobehaviour.PlayBgm(t_index);
		}

		/** ＢＧＭ。停止。
		*/
		public void StopBgm()
		{
			this.bgm_audiosource_monobehaviour.PlayBgm(-1);
		}

		/** ＢＧＭ数。取得。
		*/
		public int GetBgmMax()
		{
			return this.bgm_audiosource_monobehaviour.GetBgmMax();
		}

		/** ＢＧＭループカウント。取得。
		*/
		public int GetBgmLoopCount()
		{
			return this.bgm_audiosource_monobehaviour.GetLoopCount();
		}

		/** ＢＧＭ再生位置。取得。
		*/
		public float GetBgmPlayPosition()
		{
			return this.bgm_audiosource_monobehaviour.GetPlayPosition();
		}
	}
}

