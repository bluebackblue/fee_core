

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。クリップスプライト。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Sprite_Clip
	*/
	public class Sprite_Clip : Fee.Deleter.OnDelete_CallBackInterface , Fee.Render2D.OnSprite2DMaterialUpdate_CallBackInterface , Fee.Pool.PoolItem_Base
	{
		/** sprite
		*/
		private Fee.Render2D.Sprite2D sprite;

		/** is_clip
		*/
		private bool is_clip;

		/** clip_rect
		*/
		private Fee.Geometry.Rect2D_R<int> clip_rect;

		/** constructor

			プール用に作成。

		*/
		public Sprite_Clip()
		{
		}

		/** 作成。
		*/
		public static Sprite_Clip Create(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			Sprite_Clip t_this = Fee.Ui.Ui.GetInstance().GetPoolList_Sprite_Clip().PoolNew();
			{
				t_this.sprite = Fee.Render2D.Sprite2D.Create(null,a_drawpriority);

				//is_clip
				t_this.is_clip = false;

				//clip_rect
				t_this.clip_rect.Set(0,0,0,0);

				//マテリアル設定。
				t_this.sprite.SetMaterialType(Fee.Render2D.Config.MaterialType.AlphaClip);
				t_this.sprite.SetOnSprite2DMaterialUpdate(t_this);

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

			//プールへ返還。
			Fee.Ui.Ui.GetInstance().GetPoolList_Sprite_Clip().PoolToDelete(this);
		}

		/** [Fee.Pool.PoolItem_Base]プールアイテムをメモリから削除。
		*/
		public void OnPoolItemDeleteFromMemory()
		{
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

		/** マテリアルの更新。描画の直前に呼び出される。

			return == true : 変更あり。直後にSetPassの呼び出しが行われます。

		*/
		public bool OnSprite2DMaterialUpdate(Fee.Render2D.Sprite2D a_sprite2d,Fee.Render2D.Material_Item a_material_item)
		{
			bool t_setpass = false;

			//メインテクスチャー設定。
			if(a_material_item.SetProperty_MainTexture(a_sprite2d.GetTexture()) == true){
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
					if(a_material_item.SetProperty_ClipRectA(t_clip_x1,t_clip_y1,t_clip_x2,t_clip_y2) == true){
						t_setpass = true;
					}
				}
			}

			//SetPass要求。
			return t_setpass;
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureX(float a_texture_x)
		{
			this.sprite.SetTextureX(a_texture_x);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureY(float a_texture_y)
		{
			this.sprite.SetTextureY(a_texture_y);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureW(float a_texture_w)
		{
			this.sprite.SetTextureW(a_texture_w);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureH(float a_texture_h)
		{
			this.sprite.SetTextureH(a_texture_h);
		}

		/** テクスチャ矩形。設定。
		*/
		public float GetTextureX()
		{
			return this.sprite.GetTextureX();
		}

		/** テクスチャ矩形。設定。
		*/
		public float GetTextureY()
		{
			return this.sprite.GetTextureY();
		}

		/** テクスチャ矩形。設定。
		*/
		public float GetTextureW()
		{
			return this.sprite.GetTextureW();
		}

		/** テクスチャ矩形。設定。
		*/
		public float GetTextureH()
		{
			return this.sprite.GetTextureH();
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
			this.sprite.SetXY(a_x,a_y);
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

		/** 矩形。矩形。設定。
		*/
		public void SetWH(int a_w,int a_h)
		{
			this.sprite.SetWH(a_w,a_h);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureRect(in Fee.Geometry.Rect2D_R<float> a_texture_rect)
		{
			this.sprite.SetTextureRect(in a_texture_rect);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureRect(float a_texture_x,float a_texture_y,float a_texture_w,float a_texture_h)
		{
			this.sprite.SetTextureRect(a_texture_x,a_texture_y,a_texture_w,a_texture_h);
		}

		/** 矩形。設定。
		*/
		public void SetRect(in Fee.Geometry.Rect2D_R<int> a_rect)
		{
			this.sprite.SetRect(in a_rect);
		}

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.sprite.SetRect(a_x,a_y,a_w,a_h);
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
		public void SetCenter(int a_global_x,int a_global_y)
		{
			this.sprite.SetCenter(a_global_x,a_global_y);
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
		public void SetQuaternion(in UnityEngine.Quaternion a_quaternion)
		{
			this.sprite.SetQuaternion(in a_quaternion);
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

		/** 描画プライオリティ。設定。
		*/
		public void SetDrawPriority(long a_drawpriority)
		{
			this.sprite.SetDrawPriority(a_drawpriority);
		}

		/** 描画プライオリティ。取得。
		*/
		public long GetDrawPriority()
		{
			return this.sprite.GetDrawPriority();
		}

		/** テクスチャ。設定。
		*/
		public void SetTexture(UnityEngine.Texture2D a_texture)
		{
			this.sprite.SetTexture(a_texture);
		}

		/** テクスチャ。取得。
		*/
		public UnityEngine.Texture GetTexture()
		{
			return this.sprite.GetTexture();
		}

		/** 色。設定。
		*/
		public void SetColor(in UnityEngine.Color a_color)
		{
			this.sprite.SetColor(in a_color);
		}

		/** 色。設定。
		*/
		public void SetColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.sprite.SetColor(a_r,a_g,a_b,a_a);
		}

		/** 色。取得。
		*/
		public UnityEngine.Color GetColor()
		{
			return this.sprite.GetColor();
		}

		/** マテリアルタイプ。設定。
		*/
		public void SetMaterialType(Fee.Render2D.Config.MaterialType a_materialtype)
		{
			this.sprite.SetMaterialType(a_materialtype);
		}

		/** マテリアルタイプ。取得。
		*/
		public Fee.Render2D.Config.MaterialType GetMaterialType()
		{
			return this.sprite.GetMaterialType();
		}
	}
}

