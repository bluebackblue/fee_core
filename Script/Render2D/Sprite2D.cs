

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。スプライト。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** Sprite2D
	*/
	public class Sprite2D : Fee.Deleter.OnDelete_CallBackInterface
	{
		/** 表示フラグ。
		*/
		private bool visible;

		/** 削除フラグ。
		*/
		private bool deletereq; 

		/** 描画プライオリティ。
		*/
		private long drawpriority;

		/** 矩形。
		*/
		private Sprite2D_Rect rect;

		/** 回転。
		*/
		private Sprite2D_Rotate rotate;

		/** パラメータ。
		*/
		private Sprite2D_Param param;

		/** calccache
		*/
		private bool calccache_changeflag;
		private int calccache_x;
		private int calccache_y;
		private int calccache_w;
		private int calccache_h;
		private float[] calccache_to_8;

		/** debug
		*/
		#if(UNITY_EDITOR)
		private string debug;
		#endif

		/** constructor。
		*/
		public Sprite2D(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			#if(UNITY_EDITOR)
			{
				try{
					System.Diagnostics.StackFrame t_stackframe = new System.Diagnostics.StackFrame(1);
					if(t_stackframe != null){
						if(t_stackframe.GetMethod() != null){
							this.debug = t_stackframe.GetMethod().ReflectedType.FullName + " : " + t_stackframe.GetMethod().Name;
						}
					}
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}
			}
			#endif

			Render2D.GetInstance().AddSprite2D(this);

			//表示フラグ。
			this.visible = true;

			//削除フラグ。
			this.deletereq = false;

			//描画プライオリティ。
			this.drawpriority = a_drawpriority;

			//位置。
			//this.pos;

			//回転。
			this.rotate.Initialize();

			//パラメータ。
			this.param.Initialize();

			//calccache
			this.calccache_changeflag = true;
			this.calccache_x = 0;
			this.calccache_y = 0;
			this.calccache_w = 0;
			this.calccache_h = 0;
			this.calccache_to_8 = new float[8];

			//削除管理。
			if(a_deleter != null){
				a_deleter.Register(this);
			}
		}

		/** [Fee.Deleter.OnDelete_CallBackInterface]削除。
		*/
		public void OnDelete()
		{
			this.deletereq = true;

			//非表示。
			this.visible = false;

			//rawの削除。
			this.param.Delete();

			//更新リクエスト。
			Render2D.GetInstance().UpdateSpriteListRequest();
		}

		/** 削除チェック。
		*/
		public bool IsDelete()
		{
			return this.deletereq;
		}

		/** [デフォルト処理]マテリアルを更新する。

			return == true : 変更あり。直後にSetPassの呼び出しが行われます。

		*/
		public virtual bool UpdateMaterial(ref UnityEngine.Material a_material)
		{
			if(a_material.mainTexture != this.GetTexture()){

				//テクスチャ設定。
				a_material.mainTexture = this.GetTexture();

				//SetPass要求。
				return true;
			}

			return false;
		}

		/** 計算。スプライト。リセット。
		*/
		public void ResetSpritePositionCache()
		{
			this.calccache_changeflag = true;
		}

		/** 計算。スプライト。
		*/
		public void CalcSpritePosition(float[] a_to_8)
		{
			if((this.calccache_changeflag == false)&&(this.calccache_x == this.rect.GetX())&&(this.calccache_y == this.rect.GetY())&&(this.calccache_w == this.rect.GetW())&&(this.calccache_h == this.rect.GetH())){
				a_to_8[0] = this.calccache_to_8[0];
				a_to_8[1] = this.calccache_to_8[1];
				a_to_8[2] = this.calccache_to_8[2];
				a_to_8[3] = this.calccache_to_8[3];
				a_to_8[4] = this.calccache_to_8[4];
				a_to_8[5] = this.calccache_to_8[5];
				a_to_8[6] = this.calccache_to_8[6];
				a_to_8[7] = this.calccache_to_8[7];
			}else{
				this.calccache_changeflag = false;

				float t_calccache_sprite_x = Render2D.GetInstance().GetScreenCalcSpriteX();
				float t_calccache_sprite_y = Render2D.GetInstance().GetScreenCalcSpriteY();
				float t_calccache_sprite_w = Render2D.GetInstance().GetScreenCalcSpriteW();
				float t_calccache_sprite_h = Render2D.GetInstance().GetScreenCalcSpriteH();

				this.calccache_x = this.rect.GetX();
				this.calccache_y = this.rect.GetY();
				this.calccache_w = this.rect.GetW();
				this.calccache_h = this.rect.GetH();

				//左上。
				a_to_8[0] = (float)this.rect.GetX() / t_calccache_sprite_w + t_calccache_sprite_x;
				a_to_8[1] = 1.0f - ((float)this.rect.GetY() / t_calccache_sprite_h + t_calccache_sprite_y);

				//右下。
				a_to_8[6] = (float)(this.rect.GetX() + this.rect.GetW()) / t_calccache_sprite_w + t_calccache_sprite_x;
				a_to_8[7] = 1.0f - ((float)(this.rect.GetY() + this.rect.GetH()) / t_calccache_sprite_h + t_calccache_sprite_y);

				//右上。
				a_to_8[2] = a_to_8[6];
				a_to_8[3] = a_to_8[1];

				//左下。
				a_to_8[4] = a_to_8[0];
				a_to_8[5] = a_to_8[7];

				this.calccache_to_8[0] = a_to_8[0];
				this.calccache_to_8[1] = a_to_8[1];
				this.calccache_to_8[2] = a_to_8[2];
				this.calccache_to_8[3] = a_to_8[3];
				this.calccache_to_8[4] = a_to_8[4];
				this.calccache_to_8[5] = a_to_8[5];
				this.calccache_to_8[6] = a_to_8[6];
				this.calccache_to_8[7] = a_to_8[7];
			}
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureX(float a_texture_x)
		{
			this.rect.SetTextureX(a_texture_x);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureY(float a_texture_y)
		{
			this.rect.SetTextureY(a_texture_y);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureW(float a_texture_w)
		{
			this.rect.SetTextureW(a_texture_w);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureH(float a_texture_h)
		{
			this.rect.SetTextureH(a_texture_h);
		}

		/** テクスチャ矩形。設定。
		*/
		public float GetTextureX()
		{
			return this.rect.GetTextureX();
		}

		/** テクスチャ矩形。設定。
		*/
		public float GetTextureY()
		{
			return this.rect.GetTextureY();
		}

		/** テクスチャ矩形。設定。
		*/
		public float GetTextureW()
		{
			return this.rect.GetTextureW();
		}

		/** テクスチャ矩形。設定。
		*/
		public float GetTextureH()
		{
			return this.rect.GetTextureH();
		}

		/** 矩形。設定。
		*/
		public void SetX(int a_x)
		{
			this.rect.SetX(a_x);
		}

		/** 矩形。設定。
		*/
		public void SetY(int a_y)
		{
			this.rect.SetY(a_y);
		}

		/** 矩形。設定。
		*/
		public void SetXY(int a_x,int a_y)
		{
			this.rect.SetX(a_x);
			this.rect.SetY(a_y);
		}

		/** 矩形。設定。
		*/
		public void SetW(int a_w)
		{
			this.rect.SetW(a_w);
		}

		/** 矩形。設定。
		*/
		public void SetH(int a_h)
		{
			this.rect.SetH(a_h);
		}

		/** 矩形。取得。
		*/
		public int GetX()
		{
			return this.rect.GetX();
		}

		/** 矩形。取得。
		*/
		public int GetY()
		{
			return this.rect.GetY();
		}

		/** 矩形。取得。
		*/
		public int GetW()
		{
			return this.rect.GetW();
		}

		/** 矩形。取得。
		*/
		public int GetH()
		{
			return this.rect.GetH();
		}

		/** 矩形。矩形。設定。
		*/
		public void SetWH(int a_w,int a_h)
		{
			this.rect.SetW(a_w);
			this.rect.SetH(a_h);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureRect(ref Fee.Render2D.Rect2D_R<float> a_texture_rect)
		{
			this.rect.SetTextureRect(ref a_texture_rect);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureRect(float a_texture_x,float a_texture_y,float a_texture_w,float a_texture_h)
		{
			this.rect.SetTextureX(a_texture_x);
			this.rect.SetTextureY(a_texture_y);
			this.rect.SetTextureW(a_texture_w);
			this.rect.SetTextureH(a_texture_h);
		}

		/** 矩形。設定。
		*/
		public void SetRect(ref Fee.Render2D.Rect2D_R<int> a_rect)
		{
			this.rect.SetRect(ref a_rect);
		}

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.rect.SetX(a_x);
			this.rect.SetY(a_y);
			this.rect.SetW(a_w);
			this.rect.SetH(a_h);
		}

		/** 回転。設定。
		*/
		public void SetRotate(bool a_flag)
		{
			this.rotate.SetRotate(a_flag);
		}

		/** 回転。取得。
		*/
		public bool IsRotate()
		{
			return this.rotate.IsRotate();
		}

		/** 中心。設定。
		*/
		public void SetCenter(int a_x,int a_y)
		{
			this.rotate.SetCenterX(a_x);
			this.rotate.SetCenterY(a_y);
		}

		/** 中心。取得。
		*/
		public int GetCenterX()
		{
			return this.rotate.GetCenterX();
		}

		/** 中心。取得。
		*/
		public int GetCenterY()
		{
			return this.rotate.GetCenterY();
		}

		/** クォータニオン。設定。
		*/
		public void SetQuaternion(float a_euler_x,float a_euler_y,float a_euler_z)
		{
			this.rotate.SetQuaternion(a_euler_x,a_euler_y,a_euler_z);
		}

		/** クォータニオン。設定。
		*/
		public void SetQuaternion(ref UnityEngine.Quaternion a_quaternion)
		{
			this.rotate.SetQuaternion(ref a_quaternion);
		}

		/** クォータニオン。取得。
		*/
		public UnityEngine.Quaternion GetQuaternion()
		{
			return this.rotate.GetQuaternion();
		}

		/** 表示。設定。
		*/
		public void SetVisible(bool a_flag)
		{
			this.visible = a_flag;
		}

		/** 表示。取得。
		*/
		public bool IsVisible()
		{
			return this.visible;
		}

		/** 描画プライオリティ。設定。
		*/
		public void SetDrawPriority(long a_drawpriority)
		{
			if(this.drawpriority != a_drawpriority){
				this.drawpriority = a_drawpriority;

				//更新リクエスト。
				Render2D.GetInstance().UpdateSpriteListRequest();
			}
		}

		/** 描画プライオリティ。取得。
		*/
		public long GetDrawPriority()
		{
			return this.drawpriority;
		}

		/** 描画プライオリティ。ソート関数。
		*/
		public static int Sort_DrawPriority(Sprite2D a_test,Sprite2D a_target)
		{
			return (int)(a_test.drawpriority - a_target.drawpriority);
		}

		/** テクスチャ。設定。
		*/
		public void SetTexture(UnityEngine.Texture2D a_texture)
		{
			this.param.SetTexture(a_texture);
		}

		/** テクスチャ。取得。
		*/
		public UnityEngine.Texture2D GetTexture()
		{
			return this.param.GetTexture();
		}

		/** 色。設定。
		*/
		public void SetColor(ref UnityEngine.Color a_color)
		{
			this.param.SetColor(ref a_color);
		}

		/** 色。設定。
		*/
		public void SetColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.param.SetColor(a_r,a_g,a_b,a_a);
		}

		/** 色。取得。
		*/
		public UnityEngine.Color GetColor()
		{
			return this.param.GetColor();
		}

		/** マテリアルタイプ。設定。
		*/
		public void SetMaterialType(Render2D.MaterialType a_materialtype)
		{
			this.param.SetMaterialType(a_materialtype);
		}

		/** マテリアルタイプ。取得。
		*/
		public Render2D.MaterialType GetMaterialType()
		{
			return this.param.GetMaterialType();
		}
	}
}

