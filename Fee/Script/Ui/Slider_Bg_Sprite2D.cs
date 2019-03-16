

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。スライダー。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Slider_Bg_Sprite2D
	*/
	public class Slider_Bg_Sprite2D : Fee.Render2D.Sprite2D
	{
		/** is_clip
		*/
		private bool is_clip;

		/** corner_size
		*/
		private int corner_size;

		/** clip_rect
		*/
		private Fee.Render2D.Rect2D_R<int> clip_rect;

		/** lock_flag
		*/
		private bool lock_flag;

		/** constructor。
		*/
		public Slider_Bg_Sprite2D(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
			:
			base(a_deleter,a_drawpriority)
		{
			//is_clip
			this.is_clip = false;

			//corner_size
			this.corner_size = Config.DEFAULT_SLIDER_BG_CORNER_SIZE;

			//clip_rect
			this.clip_rect.Set(0,0,0,0);

			//lock_flag
			this.lock_flag = false;

			//マテリアル設定。
			this.SetMaterialType(Fee.Render2D.Config.MaterialType.Slice9);
		}

		/** ロックフラグ。設定。
		*/
		public void SetLock(bool a_flag)
		{
			this.lock_flag = a_flag;
		}

		/** コーナーサイズ。設定。
		*/
		public void SetCornerSize(int a_corner_size)
		{
			this.corner_size = a_corner_size;
		}

		/** クリップ。設定。
		*/
		public void SetClip(bool a_flag)
		{
			this.is_clip = a_flag;
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(ref Fee.Render2D.Rect2D_R<int> a_rect)
		{
			this.clip_rect = a_rect;
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.clip_rect.x = a_x;
			this.clip_rect.y = a_y;
			this.clip_rect.w = a_w;
			this.clip_rect.h = a_h;
		}

		/** クリップ矩形。取得。
		*/
		public int GetClipX()
		{
			return this.clip_rect.x;
		}

		/** クリップ矩形。取得。
		*/
		public int GetClipY()
		{
			return this.clip_rect.y;
		}

		/** クリップ矩形。取得。
		*/
		public int GetClipW()
		{
			return this.clip_rect.w;
		}

		/** クリップ矩形。取得。
		*/
		public int GetClipH()
		{
			return this.clip_rect.h;
		}

		/** マテリアルを更新する。

		戻り値 = true : 変更あり。直後にSetPassの呼び出しが行われます。

		*/
		public override bool UpdateMaterial(ref UnityEngine.Material a_material)
		{
			bool t_setpass = false;

			//テクスチャ設定。
			UnityEngine.Texture2D t_texture = this.GetTexture();
			if(a_material.mainTexture != t_texture){
				a_material.mainTexture = this.GetTexture();
				t_setpass = true;
			}

			//クリップ。
			{
				int t_gui_x1;
				int t_gui_y1;
				int t_gui_x2;
				int t_gui_y2;
				Fee.Render2D.Render2D.GetInstance().VirtualScreenToGuiScreen(this.clip_rect.x,this.clip_rect.y + this.clip_rect.h,out t_gui_x1,out t_gui_y1);
				Fee.Render2D.Render2D.GetInstance().VirtualScreenToGuiScreen(this.clip_rect.x + this.clip_rect.w,this.clip_rect.y,out t_gui_x2,out t_gui_y2);
				float t_clip_x1 = t_gui_x1;
				float t_clip_y1 = t_gui_y1;
				float t_clip_x2 = t_gui_x2;
				float t_clip_y2 = t_gui_y2;

				int t_clip_flag = 0;
				if(this.is_clip == true){
					t_clip_flag = 1;
				}

				if(a_material.GetInt("clip_flag") != t_clip_flag){
					a_material.SetFloat("clip_flag",t_clip_flag);
					t_setpass = true;
				}

				if(t_clip_flag > 0){
					if(a_material.GetFloat("clip_x1") != t_clip_x1){
						a_material.SetFloat("clip_x1",t_clip_x1);
						t_setpass = true;
					}

					if(a_material.GetFloat("clip_y1") != t_clip_y1){
						a_material.SetFloat("clip_y1",t_clip_y1);
						t_setpass = true;
					}

					if(a_material.GetFloat("clip_x2") != t_clip_x2){
						a_material.SetFloat("clip_x2",t_clip_x2);
						t_setpass = true;
					}

					if(a_material.GetFloat("clip_y2") != t_clip_y2){
						a_material.SetFloat("clip_y2",t_clip_y2);
						t_setpass = true;
					}
				}
			}

			if(a_material.GetInt("corner_size") != this.corner_size){
				a_material.SetInt("corner_size",this.corner_size);
				t_setpass = true;						
			}

			{
				float t_texture_x = 0.0f;
				float t_texture_y = 0.0f;

				if(this.lock_flag == true){
					t_texture_x = 0.5f;
				}

				if(a_material.GetFloat("texture_x") != t_texture_x){
					a_material.SetFloat("texture_x",t_texture_x);
					t_setpass = true;
				}
				if(a_material.GetFloat("texture_y") != t_texture_y){
					a_material.SetFloat("texture_y",t_texture_y);
					t_setpass = true;
				}
				if(a_material.GetFloat("texture_w") != 0.5f){
					a_material.SetFloat("texture_w",0.5f);
					t_setpass = true;
				}
				if(a_material.GetFloat("texture_h") != 0.5f){
					a_material.SetFloat("texture_h",0.5f);
					t_setpass = true;
				}
			}

			//矩形サイズ。
			{
				int t_rect_x1;
				int t_rect_y1;
				int t_rect_x2;
				int t_rect_y2;
				Fee.Render2D.Render2D.GetInstance().VirtualScreenToGuiScreen(this.GetX(),this.GetY(),out t_rect_x1,out t_rect_y1);
				Fee.Render2D.Render2D.GetInstance().VirtualScreenToGuiScreen(this.GetX() + this.GetW(),this.GetY() + this.GetH(),out t_rect_x2,out t_rect_y2);
				float t_clip_w = t_rect_x2 - t_rect_x1;
				float t_clip_h = t_rect_y2 - t_rect_y1;

				if(a_material.GetFloat("rect_w") != t_clip_w){
					a_material.SetFloat("rect_w",t_clip_w);
					t_setpass = true;
				}

				if(a_material.GetFloat("rect_h") != t_clip_h){
					a_material.SetFloat("rect_h",t_clip_h);
					t_setpass = true;
				}
			}

			//SetPass要求。
			return t_setpass;
		}
	}
}

