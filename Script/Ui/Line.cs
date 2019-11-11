

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。ライン。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Line
	*/
	public class Line : Fee.Deleter.OnDelete_CallBackInterface
	{
		/** deleter
		*/
		private Fee.Deleter.Deleter deleter;

		/** size
		*/
		private int size;

		/** length
		*/
		private int length;

		/** rect
		*/
		private Fee.Geometry.Rect2D_A<int> rect;

		/** sprite
		*/
		private Fee.Render2D.Sprite2D sprite;


		/*
		private Fee.Render2D.Sprite2D start;
		private Fee.Render2D.Sprite2D end;
		*/

		/** constructor
		*/
		public Line(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			//deleter
			this.deleter = new Fee.Deleter.Deleter();

			//size
			this.size = 3;

			//length
			this.length = 0;

			//sprite
			this.sprite = Fee.Render2D.Sprite2D.Create(a_deleter,a_drawpriority + 1);
			this.sprite.SetTextureRect(in Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
			this.sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.sprite.SetRotate(true);
			this.sprite.SetWH(this.length,this.size);
		}

		/** [Fee.Deleter.OnDelete_CallBackInterface]削除。
		*/
		public void OnDelete()
		{
			this.deleter.DeleteAll();
		}

		/** サイズ。設定。
		*/
		public void SetSize(int a_size)
		{
			//size
			this.size = a_size;

			//sprite
			this.sprite.SetRect(this.rect.x1,this.rect.y1,this.length,this.size);
		}

		/** 色。設定。
		*/
		public void SetColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.sprite.SetColor(a_r,a_g,a_b,a_a);
		}

		/** 矩形。設定。
		*/
		public void SetRect(in Fee.Geometry.Rect2D_A<int> a_rect)
		{
			this.rect = a_rect;

			//線の視点。
			this.sprite.SetCenter(this.rect.x1,this.rect.y1);

			//方向。
			UnityEngine.Vector3 t_direction = new UnityEngine.Vector3(this.rect.x2 - this.rect.x1,this.rect.y2 - this.rect.y1,0.0f);

			//回転。
			this.sprite.SetQuaternion(UnityEngine.Quaternion.FromToRotation(new UnityEngine.Vector3(1.0f,0.0f,0.0f),t_direction.normalized));

			//長さ。
			this.length = (int)t_direction.magnitude;

			//sprite
			this.sprite.SetRect(this.rect.x1,this.rect.y1,this.length,this.size);
		}
	}
}

