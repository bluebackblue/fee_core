

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。スライス９スプライト。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Slice9Sprite_Sprite2D
	*/
	public class Slice9Sprite_Sprite2D : Fee.Render2D.Sprite2D
	{
		/** is_clip
		*/
		private bool is_clip;

		/** corner_size
		*/
		private int corner_size;

		/** clip_rect
		*/
		private Fee.Geometry.Rect2D_R<int> clip_rect;

		/** texture_rect2
		*/
		private Fee.Geometry.Rect2D_R<float> texture_rect2;

		/** constructor
		*/
		public Slice9Sprite_Sprite2D(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
			:
			base(a_deleter,a_drawpriority)
		{
			//is_clip
			this.is_clip = false;

			//corner_size
			this.corner_size = Config.DEFAULT_SLIDER_BG_CORNER_SIZE;

			//clip_rect
			this.clip_rect.Set(0,0,0,0);

			//マテリアル設定。
			this.SetMaterialType(Fee.Render2D.Config.MaterialType.Slice9);
			this.SetTextureRect(in Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
		}

		/** SetTextureRect2
		*/
		public void SetTextureRect2(in Fee.Geometry.Rect2D_R<float> a_texture_rect)
		{
			this.texture_rect2 = a_texture_rect;
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureRect2(float a_texture_x,float a_texture_y,float a_texture_w,float a_texture_h)
		{
			this.texture_rect2.x = a_texture_x;
			this.texture_rect2.y = a_texture_y;
			this.texture_rect2.w = a_texture_w;
			this.texture_rect2.h = a_texture_h;
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

		/** クリップ。取得。
		*/
		public bool IsClip()
		{
			return this.is_clip;
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(in Fee.Geometry.Rect2D_R<int> a_rect)
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

		/** マテリアルアイテムを更新する。

			return == true : 変更あり。直後にSetPassの呼び出しが行われます。

		*/
		public override bool UpdateMaterialItem(Fee.Render2D.Material_Item a_material_item)
		{
			bool t_setpass = false;

			//メインテクスチャー設定。
			if(a_material_item.SetProperty_MainTexture(this.GetTexture()) == true){
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

				//clip_flag
				if(a_material_item.SetProperty_ClipFlag(t_clip_flag) == true){
					t_setpass = true;
				}

				if(t_clip_flag > 0){
					//clip_rect
					if(a_material_item.SetProperty_ClipRectA(t_clip_x1,t_clip_y1,t_clip_x2,t_clip_y2) == true){
						t_setpass = true;
					}
				}
			}

			//corner_size
			if(a_material_item.SetProperty_CornerSize(this.corner_size) == false){
				t_setpass = true;
			}

			{
				float t_texture_x = this.texture_rect2.x / Render2D.Config.TEXTURE_W;
				float t_texture_y = this.texture_rect2.y / Render2D.Config.TEXTURE_H;
				float t_texture_w = this.texture_rect2.w / Render2D.Config.TEXTURE_W;
				float t_texture_h = this.texture_rect2.h / Render2D.Config.TEXTURE_H;

				if(a_material_item.SetProperty_TextureRerctR(t_texture_x,t_texture_y,t_texture_w,t_texture_h) == true){
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
				float t_rect_w = t_rect_x2 - t_rect_x1;
				float t_rect_h = t_rect_y2 - t_rect_y1;

				if(a_material_item.SetProperty_RerctWH(t_rect_w,t_rect_h) == true){
					t_setpass = true;
				}
			}

			//SetPass要求。
			return t_setpass;
		}
	}
}

