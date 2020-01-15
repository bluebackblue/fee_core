

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
	/** Line2D
	*/
	public class Line2D : Fee.Deleter.OnDelete_CallBackInterface
	{
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

		/** constructor

			プール用に作成。

		*/
		public Line2D()
		{
		}

		public static Line2D Create(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			//Line2D t_this = Fee.Ui.Ui.GetInstance().GetPoolList_Line2D().PoolNew();
			Line2D t_this = new Line2D();
			{
				//size
				t_this.size = 3;

				//length
				t_this.length = 0;

				//sprite
				t_this.sprite = Fee.Render2D.Sprite2D.Create(null,a_drawpriority);
				t_this.sprite.SetTextureRect(in Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
				t_this.sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
				t_this.sprite.SetRotate(true);
				t_this.sprite.SetWH(t_this.length,t_this.size);

				if(a_deleter != null){
					a_deleter.Regist(t_this);
				}
			}
			return t_this;
		}

		/** [Fee.Deleter.OnDelete_CallBackInterface]削除。
		*/
		public void OnDelete()
		{
			//OnDelete
			this.sprite.OnDelete();

			//Fee.Ui.Ui.GetInstance().GetPoolList_Line2D().PoolDelete(this);
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

		/** テクスチャー。設定。
		*/
		public void SetTexture(UnityEngine.Texture a_texture)
		{
			this.sprite.SetTexture(a_texture);
		}

		/** マテリアルタイプ。設定。
		*/
		public void SetMaterialType(Render2D.Config.MaterialType a_materialtype)
		{
			this.sprite.SetMaterialType(a_materialtype);
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
			this.sprite.SetRect(this.rect.x1,this.rect.y1 - this.size / 2,this.length,this.size);
		}
	}
}

