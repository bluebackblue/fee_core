

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief フェード。
*/


/** Fee.Fade
*/
namespace Fee.Fade
{
	/** Fade
	*/
	public class Fade : Config
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

		/** ＧＵＩ。
		*/
		private Fee.Render2D.Size2D<int> gui_size;

		/** deleter
		*/
		private Fee.Deleter.Deleter deleter;

		/** flag
		*/
		private MonoBehaviour_Flag flag;
		private UnityEngine.GameObject flag_gameobject;

		/** sprite
		*/
		private Fade_Sprite2D sprite;

		/** [シングルトン]constructor
		*/
		private Fade()
		{
			//ＧＵＩ。
			this.gui_size.Set(0,0);

			//deleter
			this.deleter = new Fee.Deleter.Deleter();

			//flag
			{
				this.flag_gameobject = new UnityEngine.GameObject();
				this.flag_gameobject.name = "Fade";
				this.flag = this.flag_gameobject.AddComponent<MonoBehaviour_Flag>();
				this.flag .Initialize();

				UnityEngine.GameObject.DontDestroyOnLoad(this.flag_gameobject);
			}
			
			//sprite
			this.sprite = new Fade_Sprite2D(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP);
			this.sprite.SetTextureRect(0.0f,0.0f,Fee.Render2D.Render2D.TEXTURE_W,Fee.Render2D.Render2D.TEXTURE_H);
			this.sprite.SetRect(0,0,0,0);
			this.sprite.SetColor(0.0f,0.0f,0.0f,0.0f);
			this.sprite.SetMaterialType(Fee.Render2D.Config.MaterialType.Alpha);
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.deleter.DeleteAll();

			UnityEngine.GameObject.Destroy(this.flag_gameobject);
		}

		/** 描画前処理。
		*/
		public void Main_PreDraw()
		{
			try{
				//画面サイズ変更チェック。
				if((this.gui_size.w != Fee.Render2D.Render2D.GetInstance().GetGuiW())||(this.gui_size.h != Fee.Render2D.Render2D.GetInstance().GetGuiH())){
					this.gui_size.Set(Fee.Render2D.Render2D.GetInstance().GetGuiW(), Fee.Render2D.Render2D.GetInstance().GetGuiH());

					int t_x1;
					int t_y1;
					int t_x2;
					int t_y2;

					Fee.Render2D.Render2D.GetInstance().GuiScreenToVirtualScreen(-2,-2,out t_x1,out t_y1);
					Fee.Render2D.Render2D.GetInstance().GuiScreenToVirtualScreen(this.gui_size.w + 2,this.gui_size.h + 2,out t_x2,out t_y2);

					this.sprite.SetRect(t_x1,t_y1,(t_x2 - t_x1),(t_y2 - t_y1));
				}
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}

		/** 更新。
		*/
		public void Main()
		{
			try{
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

					this.sprite.SetColor(ref this.flag.anime_color);

					if(t_fix == true){
						this.flag.anime_now = false;
					}
				}
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
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
		public void SetColor(ref UnityEngine.Color a_color)
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

