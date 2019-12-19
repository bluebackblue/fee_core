

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＮＩＶＲＭ。
*/


/** Fee.UniVrm
*/
namespace Fee.UniVrm
{
	/** SimpleAnimation
	*/
	public class Controller_SimpleAnimation
	{
		/** raw
		*/
		#if(USE_DEF_FEE_SIMPLEANIMATION)
		private SimpleAnimation raw;
		#endif

		/** constructor
		*/
		#if(USE_DEF_FEE_SIMPLEANIMATION)
		public Controller_SimpleAnimation(SimpleAnimation a_simpleanimationtion)
		{
			this.raw = a_simpleanimationtion;
		}
		#endif

		/** 削除。
		*/
		public void Delete()
		{
		}

		/** モーション。追加。
		*/
		public void AddMotion(string a_state_name,UnityEngine.AnimationClip a_animetion_clip)
		{
			#if(USE_DEF_FEE_SIMPLEANIMATION)
			this.raw.AddClip(a_animetion_clip,a_state_name);
			#else
			Tool.Assert(false);
			#endif
		}
		
		/** モーション。停止。
		*/
		public void StopMotion()
		{
			#if(USE_DEF_FEE_SIMPLEANIMATION)
			this.raw.Stop();
			#else
			Tool.Assert(false);
			#endif
		}

		/** モーション。停止。
		*/
		public void StopMotion(string a_state_name)
		{
			#if(USE_DEF_FEE_SIMPLEANIMATION)
			this.raw.Stop(a_state_name);
			#else
			Tool.Assert(false);
			#endif
		}

		/** モーション。再生。
		*/
		public void PlayMotion()
		{
			#if(USE_DEF_FEE_SIMPLEANIMATION)
			this.raw.Play();
			#else
			Tool.Assert(false);
			#endif
		}

		/** モーション。再生。
		*/
		public void PlayMotion(string a_state_name,float a_cross_time,bool a_cross)
		{
			#if(USE_DEF_FEE_SIMPLEANIMATION)
			if(a_cross == true){
				this.raw.CrossFade(a_state_name,a_cross_time);
			}else{
				this.raw.Stop();
				this.raw.Play(a_state_name);
			}
			#else
			{
				Tool.Assert(false);
			}
			#endif
		}

		/** 正規化時間。取得。
		*/
		public float GetNormalizedTime(string a_state_name)
		{
			#if(USE_DEF_FEE_SIMPLEANIMATION)
			{
				SimpleAnimation.State t_state = this.raw.GetState(a_state_name);
				return t_state.normalizedTime;
			}
			#else
			{
				Tool.Assert(false);
				return 0.0f;
			}
			#endif
		}

		/** 時間。取得。
		*/
		public float GetTime(string a_state_name)
		{
			#if(USE_DEF_FEE_SIMPLEANIMATION)
			{
				SimpleAnimation.State t_state = this.raw.GetState(a_state_name);
				return t_state.time;
			}
			#else
			{
				Tool.Assert(false);
				return 0.0f;
			}
			#endif
		}

		/** ブレンド率。取得。
		*/
		public float GetBlendWeight(string a_state_name)
		{
			#if(USE_DEF_FEE_SIMPLEANIMATION)
			{
				SimpleAnimation.State t_state = this.raw.GetState(a_state_name);
				return t_state.weight;
			}
			#else
			{
				Tool.Assert(false);
				return 0.0f;
			}
			#endif
		}

		/** 再生中かどうか。取得。
		*/
		public bool IsPlay(string a_state_name)
		{
			#if(USE_DEF_FEE_SIMPLEANIMATION)
			{
				SimpleAnimation.State t_state = this.raw.GetState(a_state_name);
				return t_state.enabled;
			}
			#else
			{
				Tool.Assert(false);
				return false;
			}
			#endif
		}
	}
}

