

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

		/** playerloop_flag
		*/
		private bool playerloop_flag;

		/** ボリューム。マスター。
		*/
		private Volume volume_master;

		/** bgm
		*/
		private Bgm bgm;

		/** se
		*/
		private Se se;

		/** フォーカス。
		*/
		private bool is_focus;

		/** [シングルトン]constructor
		*/
		private Audio()
		{
			//ボリューム。マスター。
			this.volume_master = new Volume(null,Config.DEFAULT_VOLUME_MASTER);

			//bgm
			this.bgm = new Bgm(this.volume_master);

			//se
			this.se = new Se(this.volume_master);

			//フォーカス。
			this.is_focus = true;

			//playerloop_flag
			this.playerloop_flag = true;

			//PlayerLoopSystem
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Config.PLAYERLOOP_ADDTYPE,Config.PLAYERLOOP_TARGETTYPE,typeof(PlayerLoopSystemType.Fee_Audio_Main),this.Main);
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			//bgm
			this.bgm.Delete();
			this.bgm = null;

			//se
			this.se.Delete();
			this.se = null;

			//playerloop_flag
			this.playerloop_flag = false;

			//PlayerLoopSystem
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof(PlayerLoopSystemType.Fee_Audio_Main));
		}

		/** 更新。
		*/
		private void Main()
		{
			try{
				if(this.playerloop_flag == true){
					this.bgm.Main();
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}

		/** フォーカスフラグ。設定。
		*/
		public void SetFocusFlag(bool a_is_focus)
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
			this.se.ApplyVolume();
			this.bgm.ApplyVolume();
		}

		/** ＳＥボリューム。設定。
		*/
		public void SetSeVolume(float a_volume)
		{
			this.se.SetVolume(a_volume);
			this.se.ApplyVolume();
		}

		/** ＢＧＭボリューム。設定。
		*/
		public void SetBgmVolume(float a_volume)
		{
			this.bgm.SetVolume(a_volume);
			this.bgm.ApplyVolume();
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
			return this.se.GetVolume();
		}

		/** ＢＧＭボリューム。取得。
		*/
		public float GetBgmVolume()
		{
			return this.bgm.GetVolume();
		}

		/** ＢＧＭ。ロード。
		*/
		public bool LoadBgm(Bank a_bank)
		{
			return this.bgm.SetBank(a_bank);
		}

		/** ＢＧＭ。アンロード。
		*/
		public bool UnLoadBgm()
		{
			return this.bgm.UnSetBank();
		}

		/** ＳＥ。ロード。
		*/
		public void LoadSe(Bank a_bank,long a_se_id)
		{
			this.se.SetBank(a_bank,a_se_id);
		}

		/** ＳＥ。アンロード。
		*/
		public void UnLoadSe(long a_se_id)
		{
			this.se.UnSetBank(a_se_id);
		}

		/** ＳＥ。チェック。
		*/
		public bool IsExistSe(long a_se_id)
		{
			return this.se.IsExistBank(a_se_id);
		}

		/** 再生。
		*/
		public void PlaySe(long a_se_id,int a_index)
		{
			this.se.PlayOneShot(a_se_id,a_index);
		}

		/** 再生。
		*/
		public void PlaySe<T>(long a_se_id,T a_index)
			where T : struct
		{
			System.Object t_object = a_index;
			int t_index = (int)t_object;
			this.se.PlayOneShot(a_se_id,t_index);
		}

		/** ＢＧＭ。再生。
		*/
		public void PlayBgm(int a_index)
		{
			this.bgm.PlayBgm(a_index);
		}

		/** ＢＧＭ。再生。
		*/
		public void PlayBgm<T>(T a_index)
			where T : struct
		{
			System.Object t_object = a_index;
			int t_index = (int)t_object;
			this.bgm.PlayBgm(t_index);
		}

		/** ＢＧＭ。停止。
		*/
		public void StopBgm()
		{
			this.bgm.PlayBgm(-1);
		}

		/** ＢＧＭ数。取得。
		*/
		public int GetBgmMax()
		{
			return this.bgm.GetBgmMax();
		}

		/** ＢＧＭループカウント。取得。
		*/
		public int GetBgmLoopCount()
		{
			return this.bgm.GetLoopCount();
		}

		/** ＢＧＭ再生位置。取得。
		*/
		public float GetBgmPlayPosition()
		{
			return this.bgm.GetPlayPosition();
		}

		/** ＢＧＭ再生インデックス。取得。
		*/
		public int GetBgmPlayIndex()
		{
			return this.bgm.GetPlayIndex();
		}
	}
}

