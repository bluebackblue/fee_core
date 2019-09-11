

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
	/** Slice9Sprite
	*/
	public class Slice9Sprite : Fee.Deleter.OnDelete_CallBackInterface
	{
		/** deleter
		*/
		private Fee.Deleter.Deleter deleter;

		/** sprite
		*/
		private Fee.Ui.ClipSprite sprite_1;
		private Fee.Ui.ClipSprite sprite_2;
		private Fee.Ui.ClipSprite sprite_3;
		private Fee.Ui.ClipSprite sprite_4;
		private Fee.Ui.ClipSprite sprite_5;
		private Fee.Ui.ClipSprite sprite_6;
		private Fee.Ui.ClipSprite sprite_7;
		private Fee.Ui.ClipSprite sprite_8;
		private Fee.Ui.ClipSprite sprite_9;

		/** rect
		*/
		private Fee.Geometry.Rect2D_R<int> rect;
		private Fee.Geometry.Rect2D_R<float> texture_rect;
		private Fee.Geometry.Size2D<float> texture_size;

		/** corner_size
		*/
		private int corner_size;

		/** rect
		*/
		private void UpdateRect()
		{
			int t_w_a = this.corner_size;
			int t_w_b = this.rect.w - this.corner_size * 2;
			int t_w_c = this.corner_size;
			if(t_w_b < 0){
				//指定サイズが小さい。
				t_w_a = this.rect.w / 2;
				t_w_b = 0;
				t_w_c = this.rect.w / 2;
			}

			int t_h_a = this.corner_size;
			int t_h_b = this.rect.h - this.corner_size * 2;
			int t_h_c = this.corner_size;
			if(t_h_b < 0){
				//指定サイズが小さい。
				t_h_a = this.rect.h / 2;
				t_h_b = 0;
				t_h_c = this.rect.h / 2;
			}

			int t_x_0 = this.rect.x;
			int t_x_1 = this.rect.x + t_w_a;
			int t_x_2 = this.rect.x + t_w_a + t_w_b;

			int t_y_0 = this.rect.y;
			int t_y_1 = this.rect.y + t_h_a;
			int t_y_2 = this.rect.y + t_h_a + t_h_b;

			this.sprite_1.SetRect(t_x_0,t_y_0,t_w_a,t_h_a);
			this.sprite_2.SetRect(t_x_1,t_y_0,t_w_b,t_h_a);
			this.sprite_3.SetRect(t_x_2,t_y_0,t_w_c,t_h_a);
			this.sprite_4.SetRect(t_x_0,t_y_1,t_w_a,t_h_b);
			this.sprite_5.SetRect(t_x_1,t_y_1,t_w_b,t_h_b);
			this.sprite_6.SetRect(t_x_2,t_y_1,t_w_c,t_h_b);
			this.sprite_7.SetRect(t_x_0,t_y_2,t_w_a,t_h_c);
			this.sprite_8.SetRect(t_x_1,t_y_2,t_w_b,t_h_c);
			this.sprite_9.SetRect(t_x_2,t_y_2,t_w_c,t_h_c);
		}

		/** rect
		*/
		private void UpdateTextureRect()
		{
			float t_corner_texture_w = this.corner_size * Fee.Render2D.Config.TEXTURE_W / this.texture_size.w;
			float t_corner_texture_h = this.corner_size * Fee.Render2D.Config.TEXTURE_H / this.texture_size.h;

			float t_w_a = t_corner_texture_w;
			float t_w_b = this.texture_rect.w - t_corner_texture_w * 2;
			if(t_w_b < 0){
				t_w_b = 0;
			}

			float t_h_a = t_corner_texture_h;
			float t_h_b = this.texture_rect.w - t_corner_texture_h * 2;
			if(t_h_b < 0){
				t_h_b = 0;
			}

			float t_x_0 = this.texture_rect.x;
			float t_x_1 = this.texture_rect.x + (t_corner_texture_w);
			float t_x_2 = this.texture_rect.x + this.texture_rect.w - (t_corner_texture_w);
			if(t_x_2 < 0){
				t_x_2 = 0;
			}

			float t_y_0 = this.texture_rect.y;
			float t_y_1 = this.texture_rect.y + (t_corner_texture_h);
			float t_y_2 = this.texture_rect.y + this.texture_rect.h - (t_corner_texture_h);
			if(t_y_2 < 0){
				t_y_2 = 0;
			}

			this.sprite_1.SetTextureRect(t_x_0,t_y_0,t_w_a,t_h_a);
			this.sprite_2.SetTextureRect(t_x_1,t_y_0,t_w_b,t_h_a);
			this.sprite_3.SetTextureRect(t_x_2,t_y_0,t_w_a,t_h_a);
			this.sprite_4.SetTextureRect(t_x_0,t_y_1,t_w_a,t_h_b);
			this.sprite_5.SetTextureRect(t_x_1,t_y_1,t_w_b,t_h_b);
			this.sprite_6.SetTextureRect(t_x_2,t_y_1,t_w_a,t_h_b);
			this.sprite_7.SetTextureRect(t_x_0,t_y_2,t_w_a,t_h_a);
			this.sprite_8.SetTextureRect(t_x_1,t_y_2,t_w_b,t_h_a);
			this.sprite_9.SetTextureRect(t_x_2,t_y_2,t_w_a,t_h_a);
		}

		/** constructor
		*/
		public Slice9Sprite(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			//deleter
			this.deleter = new Fee.Deleter.Deleter();

			//sprite
			this.sprite_1 = new Fee.Ui.ClipSprite(this.deleter,a_drawpriority);
			this.sprite_2 = new Fee.Ui.ClipSprite(this.deleter,a_drawpriority);
			this.sprite_3 = new Fee.Ui.ClipSprite(this.deleter,a_drawpriority);
			this.sprite_4 = new Fee.Ui.ClipSprite(this.deleter,a_drawpriority);
			this.sprite_5 = new Fee.Ui.ClipSprite(this.deleter,a_drawpriority);
			this.sprite_6 = new Fee.Ui.ClipSprite(this.deleter,a_drawpriority);
			this.sprite_7 = new Fee.Ui.ClipSprite(this.deleter,a_drawpriority);
			this.sprite_8 = new Fee.Ui.ClipSprite(this.deleter,a_drawpriority);
			this.sprite_9 = new Fee.Ui.ClipSprite(this.deleter,a_drawpriority);

			//corner_size
			this.corner_size = 0;

			this.texture_size.Set(1,1);

			//削除管理。
			if(a_deleter != null){
				a_deleter.Register(this);
			}
		}

		/** [Fee.Deleter.OnDelete_CallBackInterface]削除。
		*/
		public void OnDelete()
		{
			this.deleter.DeleteAll();
		}

		/** クリップ。設定。
		*/
		public void SetClip(bool a_clip)
		{
			this.sprite_1.SetClip(a_clip);
			this.sprite_2.SetClip(a_clip);
			this.sprite_3.SetClip(a_clip);
			this.sprite_4.SetClip(a_clip);
			this.sprite_5.SetClip(a_clip);
			this.sprite_6.SetClip(a_clip);
			this.sprite_7.SetClip(a_clip);
			this.sprite_8.SetClip(a_clip);
			this.sprite_9.SetClip(a_clip);
		}

		/** クリップ。取得。
		*/
		public bool IsClip()
		{
			return this.sprite_1.IsClip();
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(in Fee.Geometry.Rect2D_R<int> a_rect)
		{
			this.sprite_1.SetClipRect(in a_rect);
			this.sprite_2.SetClipRect(in a_rect);
			this.sprite_3.SetClipRect(in a_rect);
			this.sprite_4.SetClipRect(in a_rect);
			this.sprite_5.SetClipRect(in a_rect);
			this.sprite_6.SetClipRect(in a_rect);
			this.sprite_7.SetClipRect(in a_rect);
			this.sprite_8.SetClipRect(in a_rect);
			this.sprite_9.SetClipRect(in a_rect);
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.sprite_1.SetClipRect(a_x,a_y,a_w,a_h);
			this.sprite_2.SetClipRect(a_x,a_y,a_w,a_h);
			this.sprite_3.SetClipRect(a_x,a_y,a_w,a_h);
			this.sprite_4.SetClipRect(a_x,a_y,a_w,a_h);
			this.sprite_5.SetClipRect(a_x,a_y,a_w,a_h);
			this.sprite_6.SetClipRect(a_x,a_y,a_w,a_h);
			this.sprite_7.SetClipRect(a_x,a_y,a_w,a_h);
			this.sprite_8.SetClipRect(a_x,a_y,a_w,a_h);
			this.sprite_9.SetClipRect(a_x,a_y,a_w,a_h);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureRect(in Fee.Geometry.Rect2D_R<float> a_texture_rect)
		{
			this.texture_rect = a_texture_rect;
			this.UpdateTextureRect();
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureRect(float a_texture_x,float a_texture_y,float a_texture_w,float a_texture_h)
		{
			this.texture_rect.Set(a_texture_x,a_texture_y,a_texture_w,a_texture_h);
			this.UpdateTextureRect();
		}
		
		/** 矩形。設定。
		*/
		public void SetRect(in Fee.Geometry.Rect2D_R<int> a_rect)
		{
			this.rect = a_rect;
			this.UpdateRect();
		}

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.rect.Set(a_x,a_y,a_w,a_h);
			this.UpdateRect();
		}

		/** 矩形。設定。
		*/
		public void SetX(int a_x)
		{
			this.rect.x = a_x;
			this.UpdateRect();
		}

		/** 矩形。設定。
		*/
		public void SetY(int a_y)
		{
			this.rect.y = a_y;
			this.UpdateRect();
		}

		/** 矩形。設定。
		*/
		public void SetXY(int a_x,int a_y)
		{
			this.rect.x = a_x;
			this.rect.y = a_y;
			this.UpdateRect();
		}

		/** 矩形。設定。
		*/
		public void SetW(int a_w)
		{
			this.rect.w = a_w;
			this.UpdateRect();
		}

		/** 矩形。設定。
		*/
		public void SetH(int a_h)
		{
			this.rect.h = a_h;
			this.UpdateRect();
		}

		/** 矩形。設定。
		*/
		public void SetWH(int a_w,int a_h)
		{
			this.rect.w = a_w;
			this.rect.h = a_h;
			this.UpdateRect();
		}

		/** 矩形。取得。
		*/
		public int GetX()
		{
			return this.rect.x;
		}

		/** 矩形。取得。
		*/
		public int GetY()
		{
			return this.rect.y;
		}

		/** 矩形。取得。
		*/
		public int GetW()
		{
			return this.rect.w;
		}

		/** 矩形。取得。
		*/
		public int GetH()
		{
			return this.rect.h;
		}

		/** 回転。設定。
		*/
		public void SetRotate(bool a_flag)
		{
			this.sprite_1.SetRotate(a_flag);
			this.sprite_2.SetRotate(a_flag);
			this.sprite_3.SetRotate(a_flag);
			this.sprite_4.SetRotate(a_flag);
			this.sprite_5.SetRotate(a_flag);
			this.sprite_6.SetRotate(a_flag);
			this.sprite_7.SetRotate(a_flag);
			this.sprite_8.SetRotate(a_flag);
			this.sprite_9.SetRotate(a_flag);
		}

		/** 回転。取得。
		*/
		public bool IsRotate()
		{
			return this.sprite_1.IsRotate();
		}

		/** 中心。設定。
		*/
		public void SetCenter(int a_x,int a_y)
		{
			this.sprite_1.SetCenter(a_x,a_y);
			this.sprite_2.SetCenter(a_x,a_y);
			this.sprite_3.SetCenter(a_x,a_y);
			this.sprite_4.SetCenter(a_x,a_y);
			this.sprite_5.SetCenter(a_x,a_y);
			this.sprite_6.SetCenter(a_x,a_y);
			this.sprite_7.SetCenter(a_x,a_y);
			this.sprite_8.SetCenter(a_x,a_y);
			this.sprite_9.SetCenter(a_x,a_y);
		}

		/** 中心。取得。
		*/
		public int GetCenterX()
		{
			return this.sprite_1.GetCenterX();
		}

		/** 中心。取得。
		*/
		public int GetCenterY()
		{
			return this.sprite_1.GetCenterY();
		}

		/** クォータニオン。設定。
		*/
		public void SetQuaternion(float a_euler_x,float a_euler_y,float a_euler_z)
		{
			this.sprite_1.SetQuaternion(a_euler_x,a_euler_y,a_euler_z);
			UnityEngine.Quaternion t_quaternion = this.sprite_1.GetQuaternion();
			this.sprite_2.SetQuaternion(in t_quaternion);
			this.sprite_3.SetQuaternion(in t_quaternion);
			this.sprite_4.SetQuaternion(in t_quaternion);
			this.sprite_5.SetQuaternion(in t_quaternion);
			this.sprite_6.SetQuaternion(in t_quaternion);
			this.sprite_7.SetQuaternion(in t_quaternion);
			this.sprite_8.SetQuaternion(in t_quaternion);
			this.sprite_9.SetQuaternion(in t_quaternion);
		}

		/** クォータニオン。設定。
		*/
		public void SetQuaternion(in UnityEngine.Quaternion a_quaternion)
		{
			this.sprite_1.SetQuaternion(in a_quaternion);
			this.sprite_2.SetQuaternion(in a_quaternion);
			this.sprite_3.SetQuaternion(in a_quaternion);
			this.sprite_4.SetQuaternion(in a_quaternion);
			this.sprite_5.SetQuaternion(in a_quaternion);
			this.sprite_6.SetQuaternion(in a_quaternion);
			this.sprite_7.SetQuaternion(in a_quaternion);
			this.sprite_8.SetQuaternion(in a_quaternion);
			this.sprite_9.SetQuaternion(in a_quaternion);
		}

		/** クォータニオン。取得。
		*/
		public UnityEngine.Quaternion GetQuaternion()
		{
			return this.sprite_1.GetQuaternion();
		}

		/** 表示。設定。
		*/
		public void SetVisible(bool a_flag)
		{
			this.sprite_1.SetVisible(a_flag);
			this.sprite_2.SetVisible(a_flag);
			this.sprite_3.SetVisible(a_flag);
			this.sprite_4.SetVisible(a_flag);
			this.sprite_5.SetVisible(a_flag);
			this.sprite_6.SetVisible(a_flag);
			this.sprite_7.SetVisible(a_flag);
			this.sprite_8.SetVisible(a_flag);
			this.sprite_9.SetVisible(a_flag);
		}

		/** 表示。取得。
		*/
		public bool IsVisible()
		{
			return this.sprite_1.IsVisible();
		}

		/** 描画プライオリティ。取得。
		*/
		public long GetDrawPriority()
		{
			return this.sprite_1.GetDrawPriority();
		}

		/** 描画プライオリティ。設定。
		*/
		public void SetDrawPriority(long a_drawpriority)
		{
			this.sprite_1.SetDrawPriority(a_drawpriority);
			this.sprite_2.SetDrawPriority(a_drawpriority);
			this.sprite_3.SetDrawPriority(a_drawpriority);
			this.sprite_4.SetDrawPriority(a_drawpriority);
			this.sprite_5.SetDrawPriority(a_drawpriority);
			this.sprite_6.SetDrawPriority(a_drawpriority);
			this.sprite_7.SetDrawPriority(a_drawpriority);
			this.sprite_8.SetDrawPriority(a_drawpriority);
			this.sprite_9.SetDrawPriority(a_drawpriority);
		}

		/** テクスチャ。設定。
		*/
		public void SetTexture(UnityEngine.Texture2D a_texture)
		{
			if(a_texture != null){
				this.texture_size.Set(a_texture.width,a_texture.height);
			}else{
				this.texture_size.Set(1,1);
			}

			this.sprite_1.SetTexture(a_texture);
			this.sprite_2.SetTexture(a_texture);
			this.sprite_3.SetTexture(a_texture);
			this.sprite_4.SetTexture(a_texture);
			this.sprite_5.SetTexture(a_texture);
			this.sprite_6.SetTexture(a_texture);
			this.sprite_7.SetTexture(a_texture);
			this.sprite_8.SetTexture(a_texture);
			this.sprite_9.SetTexture(a_texture);

			this.UpdateTextureRect();
		}

		/** テクスチャ。取得。
		*/
		public UnityEngine.Texture2D GetTexture()
		{
			return this.sprite_1.GetTexture();
		}

		/** 色。設定。
		*/
		public void SetColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.sprite_1.SetColor(a_r,a_g,a_b,a_a);
			this.sprite_2.SetColor(a_r,a_g,a_b,a_a);
			this.sprite_3.SetColor(a_r,a_g,a_b,a_a);
			this.sprite_4.SetColor(a_r,a_g,a_b,a_a);
			this.sprite_5.SetColor(a_r,a_g,a_b,a_a);
			this.sprite_6.SetColor(a_r,a_g,a_b,a_a);
			this.sprite_7.SetColor(a_r,a_g,a_b,a_a);
			this.sprite_8.SetColor(a_r,a_g,a_b,a_a);
			this.sprite_9.SetColor(a_r,a_g,a_b,a_a);
		}

		/** 色。設定。
		*/
		public void SetColor(in UnityEngine.Color a_color)
		{
			this.sprite_1.SetColor(in a_color);
			this.sprite_2.SetColor(in a_color);
			this.sprite_3.SetColor(in a_color);
			this.sprite_4.SetColor(in a_color);
			this.sprite_5.SetColor(in a_color);
			this.sprite_6.SetColor(in a_color);
			this.sprite_7.SetColor(in a_color);
			this.sprite_8.SetColor(in a_color);
			this.sprite_9.SetColor(in a_color);
		}

		/** 色。取得。
		*/
		public UnityEngine.Color GetColor()
		{
			return this.sprite_1.GetColor();
		}

		/** コーナーサイズ。設定。
		*/
		public void SetCornerSize(int a_corner_size)
		{
			this.corner_size = a_corner_size;
			this.UpdateTextureRect();
			this.UpdateRect();
		}
	}
}

