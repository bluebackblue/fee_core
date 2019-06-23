

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

			TODO:sprite[9]

		*/
		private Fee.Ui.Slice9Sprite_Sprite2D sprite;

		/** constructor
		*/
		public Slice9Sprite(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			//deleter
			this.deleter = new Fee.Deleter.Deleter();

			//sprite
			this.sprite = new Slice9Sprite_Sprite2D(this.deleter,a_drawpriority);

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
			this.sprite.SetClip(a_clip);
		}

		/** クリップ。取得。
		*/
		public bool IsClip()
		{
			return this.sprite.IsClip();
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(ref Fee.Render2D.Rect2D_R<int> a_rect)
		{
			this.sprite.SetClipRect(ref a_rect);
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.sprite.SetClipRect(a_x,a_y,a_w,a_h);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureRect(ref Fee.Render2D.Rect2D_R<float> a_texture_rect)
		{
			this.sprite.SetTextureRect2(ref a_texture_rect);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureRect(float a_texture_x,float a_texture_y,float a_texture_w,float a_texture_h)
		{
			this.sprite.SetTextureRect2(a_texture_x,a_texture_y,a_texture_w,a_texture_h);
		}
		
		/** 矩形。設定。
		*/
		public void SetRect(ref Fee.Render2D.Rect2D_R<int> a_rect)
		{
			this.sprite.SetRect(ref a_rect);
		}

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.sprite.SetRect(a_x,a_y,a_w,a_h);
		}

		/** 矩形。設定。
		*/
		public void SetX(int a_x)
		{
			this.sprite.SetX(a_x);
		}

		/** 矩形。設定。
		*/
		public void SetY(int a_y)
		{
			this.sprite.SetY(a_y);
		}

		/** 矩形。設定。
		*/
		public void SetXY(int a_x,int a_y)
		{
			this.sprite.SetX(a_x);
			this.sprite.SetY(a_y);
		}

		/** 矩形。設定。
		*/
		public void SetW(int a_w)
		{
			this.sprite.SetW(a_w);
		}

		/** 矩形。設定。
		*/
		public void SetH(int a_h)
		{
			this.sprite.SetH(a_h);
		}

		/** 矩形。設定。
		*/
		public void SetWH(int a_w,int a_h)
		{
			this.sprite.SetWH(a_w,a_h);
		}

		/** 矩形。取得。
		*/
		public int GetX()
		{
			return this.sprite.GetX();
		}

		/** 矩形。取得。
		*/
		public int GetY()
		{
			return this.sprite.GetY();
		}

		/** 矩形。取得。
		*/
		public int GetW()
		{
			return this.sprite.GetW();
		}

		/** 矩形。取得。
		*/
		public int GetH()
		{
			return this.sprite.GetH();
		}

		/** 回転。設定。
		*/
		public void SetRotate(bool a_flag)
		{
			this.sprite.SetRotate(a_flag);
		}

		/** 回転。取得。
		*/
		public bool IsRotate()
		{
			return this.sprite.IsRotate();
		}

		/** 中心。設定。
		*/
		public void SetCenter(int a_x,int a_y)
		{
			this.sprite.SetCenter(a_x,a_y);
		}

		/** 中心。取得。
		*/
		public int GetCenterX()
		{
			return this.sprite.GetCenterX();
		}

		/** 中心。取得。
		*/
		public int GetCenterY()
		{
			return this.sprite.GetCenterY();
		}

		/** クォータニオン。設定。
		*/
		public void SetQuaternion(float a_euler_x,float a_euler_y,float a_euler_z)
		{
			this.sprite.SetQuaternion(a_euler_x,a_euler_y,a_euler_z);
		}

		/** クォータニオン。設定。
		*/
		public void SetQuaternion(ref UnityEngine.Quaternion a_quaternion)
		{
			this.sprite.SetQuaternion(ref a_quaternion);
		}

		/** クォータニオン。取得。
		*/
		public UnityEngine.Quaternion GetQuaternion()
		{
			return this.sprite.GetQuaternion();
		}

		/** 表示。設定。
		*/
		public void SetVisible(bool a_flag)
		{
			this.sprite.SetVisible(a_flag);
		}

		/** 表示。取得。
		*/
		public bool IsVisible()
		{
			return this.sprite.IsVisible();
		}

		/** 描画プライオリティ。取得。
		*/
		public long GetDrawPriority()
		{
			return this.sprite.GetDrawPriority();
		}

		/** 描画プライオリティ。設定。
		*/
		public void SetDrawPriority(long a_drawpriority)
		{
			this.sprite.SetDrawPriority(a_drawpriority);
		}

		/** テクスチャ。設定。
		*/
		public void SetTexture(UnityEngine.Texture2D a_texture)
		{
			this.sprite.SetTexture(a_texture);
		}

		/** テクスチャ。取得。
		*/
		public UnityEngine.Texture2D GetTexture()
		{
			return this.sprite.GetTexture();
		}

		/** 色。設定。
		*/
		public void SetColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.sprite.SetColor(a_r,a_g,a_b,a_a);
		}

		/** 色。設定。
		*/
		public void SetColor(ref UnityEngine.Color a_color)
		{
			this.sprite.SetColor(ref a_color);
		}

		/** 色。取得。
		*/
		public UnityEngine.Color GetColor()
		{
			return this.sprite.GetColor();
		}

		/** コーナーサイズ。設定。
		*/
		public void SetCornerSize(int a_corner_size)
		{
			this.sprite.SetCornerSize(a_corner_size);
		}
	}
}

