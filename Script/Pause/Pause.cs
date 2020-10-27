

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ポーズ。
*/


/** Fee.Pause
*/
namespace Fee.Pause
{
	/** Pause
	*/
	public class Pause
	{
		/** [シングルトン]s_instance
		*/
		private static Pause s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Pause();
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
		public static Pause GetInstance()
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

		/** timescale
		*/
		private float timescale;

		/** is_stepplay
		*/
		private bool is_stepplay;

		/** [シングルトン]constructor
		*/
		private Pause()
		{
			//PlayerLoopType
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Config.PLAYERLOOP_ADDTYPE,Config.PLAYERLOOP_TARGETTYPE,typeof(PlayerLoopType.Fee_Pause_Main),this.Main);

			//timescale
			this.timescale = 1.0f;

			//is_stepplay
			this.is_stepplay = false;
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			//PlayerLoopType
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof(PlayerLoopType.Fee_Pause_Main));
		}

		/** Main
		*/
		private void Main()
		{
			//現在のフレームでのタイムスケールを取得。
			this.timescale = UnityEngine.Time.timeScale;

			if(this.is_stepplay == true){
				this.is_stepplay = false;
				
				//このフレームはステッププレイ。
				UnityEngine.Time.timeScale = 0.0f;
			}
		}

		/** 現在のフレームのタイムスケール。取得。
		*/
		public float GetTimeScale()
		{
			return this.timescale;
		}

		/** 次のフレームのタイムスケール。取得。
		*/
		public float GetNextFrameTimeScale()
		{
			return UnityEngine.Time.timeScale;
		}

		/** 次のフレームでのタイムスケールを設定。
		*/
		public void SetNextFrameTimeScale(float a_timescale)
		{
			UnityEngine.Time.timeScale = a_timescale;
		}

		/** １フーレーム再生後、タイムスケールを０にする。
		*/
		public void StepOneFrame(float a_timescale)
		{
			Tool.Assert(a_timescale > 0.0f);

			//次のフレームで進めるタイムスケールを設定。
			UnityEngine.Time.timeScale = a_timescale;

			//次のフレームはステッププレイ。
			this.is_stepplay = true;
		}
	}
}

