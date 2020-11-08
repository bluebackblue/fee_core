

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief フェード。
*/


/** Fee.Fade
*/
namespace Fee.Fade
{
	/** Fade
	*/
	public class Fade
	{
		/** [シングルトン]s_instance
		*/
		private static Fade s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Fade();
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
		public static Fade GetInstance()
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

		/** flag
		*/
		private Flag flag;

		/** sprite
		*/
		private Sprite2D_Fade sprite;

		/** playerloop_flag
		*/
		private bool playerloop_flag;

		/** [シングルトン]constructor
		*/
		private Fade()
		{
			//flag
			this.flag.Initialize();
		
			//sprite
			this.sprite = new Sprite2D_Fade(null,(Fee.Render2D.Config.MAX_LAYER - 1) * Fee.Render2D.Config.DRAWPRIORITY_STEP);
			this.sprite.SetTextureRect(0.0f,0.0f,Fee.Render2D.Config.TEXTURE_W,Fee.Render2D.Config.TEXTURE_H);
			this.sprite.SetColor(UnityEngine.Color.clear);
			this.sprite.SetMaterialType(Fee.Render2D.MaterialType.Alpha);

			//SetRectFromScreenSize
			this.SetRectFromScreenSize();

			//スクリーンサイズ変更通知。登録。
			//ソートリストタスク終了後、バーテックス計算タスク開始前。
			Fee.Render2D.Render2D.GetInstance().RegistOnChangeScreenSize(this.OnChangeScreenSize);

			//PlayerLoopType
			this.playerloop_flag = true;
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().Add(Config.PLAYERLOOP_ADDTYPE,Config.PLAYERLOOP_TARGETTYPE,typeof(PlayerLoopType.Fee_Fade_Main),this.Main);
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			//スクリーンサイズ変更通知。解除。
			Fee.Render2D.Render2D.GetInstance().UnRegistOnChangeScreenSize(this.OnChangeScreenSize);

			//OnDelete
			this.sprite.OnDelete();

			//PlayerLoopType
			this.playerloop_flag = false;
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof(PlayerLoopType.Fee_Fade_Main));
		}

		/** SetRectFromScreenSize
		*/
		private void SetRectFromScreenSize()
		{
			int t_x1;
			int t_y1;
			int t_x2;
			int t_y2;
			Fee.Render2D.Render2D.GetInstance().GuiScreenToVirtualScreen(-2,-2,out t_x1,out t_y1);
			Fee.Render2D.Render2D.GetInstance().GuiScreenToVirtualScreen(Fee.Render2D.Render2D.GetInstance().GetGuiW() + 2,Fee.Render2D.Render2D.GetInstance().GetGuiH() + 2,out t_x2,out t_y2);
			this.sprite.SetRect(t_x1,t_y1,(t_x2 - t_x1),(t_y2 - t_y1));
		}

		/** スクリーンサイズ変更通知
		*/
		private void OnChangeScreenSize()
		{
			this.SetRectFromScreenSize();
		}

		/** Main
		*/
		private void Main()
		{
			try{
				if(this.playerloop_flag == true){
					if(this.flag.anime_now == true){
						bool t_fix = true;

						if(this.flag.anime_color.r < this.flag.anime_color_to.r){
							t_fix = false;
							this.flag.anime_color.r += this.flag.anime_speed;
							if(this.flag.anime_color.r >= this.flag.anime_color_to.r){
								this.flag.anime_color.r = this.flag.anime_color_to.r;
							}
						}else if(this.flag.anime_color.r > this.flag.anime_color_to.r){
							t_fix = false;
							this.flag.anime_color.r -= this.flag.anime_speed;
							if(this.flag.anime_color.r <= this.flag.anime_color_to.r){
								this.flag.anime_color.r = this.flag.anime_color_to.r;
							}
						}

						if(this.flag.anime_color.g < this.flag.anime_color_to.g){
							t_fix = false;
							this.flag.anime_color.g += this.flag.anime_speed;
							if(this.flag.anime_color.g >= this.flag.anime_color_to.g){
								this.flag.anime_color.g = this.flag.anime_color_to.g;
							}
						}else if(this.flag.anime_color.g > this.flag.anime_color_to.g){
							t_fix = false;
							this.flag.anime_color.g -= this.flag.anime_speed;
							if(this.flag.anime_color.g <= this.flag.anime_color_to.g){
								this.flag.anime_color.g = this.flag.anime_color_to.g;
							}
						}

						if(this.flag.anime_color.b < this.flag.anime_color_to.b){
							t_fix = false;
							this.flag.anime_color.b += this.flag.anime_speed;
							if(this.flag.anime_color.b >= this.flag.anime_color_to.b){
								this.flag.anime_color.b = this.flag.anime_color_to.b;
							}
						}else if(this.flag.anime_color.b > this.flag.anime_color_to.b){
							t_fix = false;
							this.flag.anime_color.b -= this.flag.anime_speed;
							if(this.flag.anime_color.b <= this.flag.anime_color_to.b){
								this.flag.anime_color.b = this.flag.anime_color_to.b;
							}
						}

						if(this.flag.anime_color.a < this.flag.anime_color_to.a){
							t_fix = false;
							this.flag.anime_color.a += this.flag.anime_speed;
							if(this.flag.anime_color.a >= this.flag.anime_color_to.a){
								this.flag.anime_color.a = this.flag.anime_color_to.a;
							}
						}else if(this.flag.anime_color.a > this.flag.anime_color_to.a){
							t_fix = false;
							this.flag.anime_color.a -= this.flag.anime_speed;
							if(this.flag.anime_color.a <= this.flag.anime_color_to.a){
								this.flag.anime_color.a = this.flag.anime_color_to.a;
							}
						}

						if(this.flag.anime_color.a <= 0.0f){
							this.sprite.SetVisible(false);
							this.flag.anime_color = this.flag.anime_color_to;
							t_fix = true;
						}else{
							this.sprite.SetVisible(true);
						}

						this.sprite.SetColor(in this.flag.anime_color);

						if(t_fix == true){
							this.flag.anime_now = false;
						}
					}
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}

		/** 描画プライオリティ。設定。
		*/
		public void SetDrawPriority(long a_drawpriority)
		{
			this.sprite.SetDrawPriority(a_drawpriority);
		}

		/** 色。設定。
		*/
		public void SetColor(in UnityEngine.Color a_color)
		{
			this.flag.anime_color = a_color;
		}

		/** 色。設定。
		*/
		public void SetColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.flag.anime_color.r = a_r;
			this.flag.anime_color.g = a_g;
			this.flag.anime_color.b = a_b;
			this.flag.anime_color.a = a_a;
		}

		/** 色。取得。
		*/
		public float GetColorR()
		{
			return this.flag.anime_color.r;
		}

		/** 色。取得。
		*/
		public float GetColorG()
		{
			return this.flag.anime_color.r;
		}

		/** 色。取得。
		*/
		public float GetColorB()
		{
			return this.flag.anime_color.r;
		}

		/** 色。取得。
		*/
		public float GetColorA()
		{
			return this.flag.anime_color.r;
		}

		/** 変更色。設定。
		*/
		public void SetToColor(float a_r,float a_g,float a_b,float a_a)
		{
			if((this.flag.anime_color_to.r != a_r)||(this.flag.anime_color_to.g != a_g)||(this.flag.anime_color_to.b != a_b)||(this.flag.anime_color_to.a != a_a)){
				this.flag.anime_color_to.r = a_r;
				this.flag.anime_color_to.g = a_g;
				this.flag.anime_color_to.b = a_b;
				this.flag.anime_color_to.a = a_a;
			}
		}

		/** 変更色。取得。
		*/
		public float GetToColorR()
		{
			return this.flag.anime_color_to.r;
		}

		/** 変更色。取得。
		*/
		public float GetToColorG()
		{
			return this.flag.anime_color_to.r;
		}

		/** 変更色。取得。
		*/
		public float GetToColorB()
		{
			return this.flag.anime_color_to.r;
		}

		/** 変更色。取得。
		*/
		public float GetToColorA()
		{
			return this.flag.anime_color_to.r;
		}

		/** フェードイン。
		*/
		public void FadeIn()
		{
			this.flag.anime_color_to.a = 0.0f;

			if(this.flag.anime_color_to.a != this.flag.anime_color.a){
				this.flag.anime_now = true;
			}
		}

		/** フェードアウト。
		*/
		public void FadeOut()
		{
			this.flag.anime_color_to.a = 1.0f;

			if(this.flag.anime_color_to.a != this.flag.anime_color.a){
				this.flag.anime_now = true;
			}
		}

		/** アニメ。チェック。
		*/
		public bool IsAnime()
		{
			return this.flag.anime_now;
		}

		/** アニメ。開始。
		*/
		public void SetAnime()
		{
			this.flag.anime_now = true;
		}

		/** フェードイン。チェック。
		*/
		public bool IsFadeIn()
		{
			if(this.flag.anime_color.a == 0.0f){
				return true;
			}
			return false;
		}

		/** フェードアウト。チェック。
		*/
		public bool IsFadeOut()
		{
			if(this.flag.anime_color.a == 1.0f){
				return true;
			}
			return false;
		}

		/** 速度。設定。
		*/
		public void SetSpeed(float a_speed)
		{
			this.flag.anime_speed = a_speed;
		}

		/** 速度。取得。
		*/
		public float GetSpeed()
		{
			return this.flag.anime_speed;
		}
	}
}

