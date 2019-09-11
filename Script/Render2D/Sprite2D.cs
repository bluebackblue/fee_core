

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。スプライト。
*/


// The private field * is assigned but its value is never used
#if(UNITY_EDITOR)
#pragma warning disable 0414
#endif


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

		/** vertex
		*/
		private float[] vertex;
		private bool vertex_recalc;
		
		/** texcood
		*/
		private float[] texcood;
		private bool texcood_recalc;

		/** debug
		*/
		#if(UNITY_EDITOR)
		private string debug;
		#endif

		/** constructor。

			「DRAWPRIORITY_STEP」ごとに描画カメラが切り替わる。
			同一カメラ内では必ずテキストが上に表示される。
			テキストの上にスプライトを表示する場合は、描画カメラが切り替わるようにプライオリィを設定する必要がある。

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

			//vertex
			this.vertex = new float[8];
			this.vertex_recalc = true;

			//texcood
			this.texcood = new float[4];
			this.texcood_recalc = true;

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

			//削除リクエスト。
			Render2D.GetInstance().SpriteListDeleteRequest();
		}

		/** 削除チェック。
		*/
		public bool IsDelete()
		{
			return this.deletereq;
		}

		/** [デフォルト処理]マテリアルアイテムを更新する。

			return == true : 変更あり。直後にSetPassの呼び出しが行われます。

		*/
		public virtual bool UpdateMaterialItem(Material_Item a_material_item)
		{
			bool t_setpass = false;

			//メインテクスチャー設定。
			if(a_material_item.SetProperty_MainTexture(this.GetTexture()) == true){
				t_setpass = true;
			}

			return t_setpass;
		}

		/** 再計算。リクエスト。
		*/
		public void RequestReCalcVertex()
		{
			this.vertex_recalc = true;
		}

		/** 計算。
		*/
		public void Calc()
		{
			//texcood
			if(this.texcood_recalc == true){
				this.texcood_recalc = false;

				//左上。Ｘ。
				this.texcood[0] = this.GetTextureX() / Config.TEXTURE_W;

				//左上。Ｙ。
				this.texcood[1] = 1.0f - this.GetTextureY() / Config.TEXTURE_H;

				//右下。Ｘ。
				this.texcood[2] = (this.GetTextureX() + this.GetTextureW()) / Config.TEXTURE_W;

				//右下。Ｙ。
				this.texcood[3] = 1.0f - (this.GetTextureY() + this.GetTextureH()) / Config.TEXTURE_H;
			}

			//vertex
			if(this.vertex_recalc == true){
				this.vertex_recalc = false;

				float t_screen_calc_x = Render2D.GetInstance().GetScreenCalcSpriteX();
				float t_screen_calc_y = Render2D.GetInstance().GetScreenCalcSpriteY();
				float t_screen_calc_w = Render2D.GetInstance().GetScreenCalcSpriteW();
				float t_screen_calc_h = Render2D.GetInstance().GetScreenCalcSpriteH();

				if(this.IsRotate() == true){
					//回転あり。

					UnityEngine.Vector2 t_center = new UnityEngine.Vector2(this.GetCenterX(),this.GetCenterY());

					//左上。
					UnityEngine.Vector2 t_1 = new UnityEngine.Vector2(this.GetX(),this.GetY()) - t_center;

					//右上。
					UnityEngine.Vector2 t_2 = new UnityEngine.Vector2(this.GetX() + this.GetW(),this.GetY()) - t_center;

					//左下。
					UnityEngine.Vector2 t_3 = new UnityEngine.Vector2(this.GetX(),this.GetY() + this.GetH()) - t_center;

					//右下。
					UnityEngine.Vector2 t_4 = new UnityEngine.Vector2(this.GetX() + this.GetW(),this.GetY() + this.GetH()) - t_center;

					//回転。
					UnityEngine.Quaternion t_quaternion = this.GetQuaternion();

					t_1 = t_quaternion * t_1;
					t_2 = t_quaternion * t_2;
					t_3 = t_quaternion * t_3;
					t_4 = t_quaternion * t_4;

					t_1 += t_center;
					t_2 += t_center;
					t_3 += t_center;
					t_4 += t_center;

					//左上。
					this.vertex[0] = t_1.x / t_screen_calc_w + t_screen_calc_x;
					this.vertex[1] = 1.0f - (t_1.y / t_screen_calc_h + t_screen_calc_y);

					//右上。
					this.vertex[2] = t_2.x / t_screen_calc_w + t_screen_calc_x;
					this.vertex[3] = 1.0f - (t_2.y / t_screen_calc_h + t_screen_calc_y);

					//左下。
					this.vertex[4] = t_3.x / t_screen_calc_w + t_screen_calc_x;
					this.vertex[5] = 1.0f - (t_3.y / t_screen_calc_h + t_screen_calc_y);

					//右下。
					this.vertex[6] = t_4.x / t_screen_calc_w + t_screen_calc_x;
					this.vertex[7] = 1.0f - (t_4.y / t_screen_calc_h + t_screen_calc_y);
				}else{
					//回転なし。

					//左上。
					this.vertex[0] = (float)this.rect.GetX() / t_screen_calc_w + t_screen_calc_x;
					this.vertex[1] = 1.0f - ((float)this.rect.GetY() / t_screen_calc_h + t_screen_calc_y);

					//右下。
					this.vertex[6] = (float)(this.rect.GetX() + this.rect.GetW()) / t_screen_calc_w + t_screen_calc_x;
					this.vertex[7] = 1.0f - ((float)(this.rect.GetY() + this.rect.GetH()) / t_screen_calc_h + t_screen_calc_y);

					//右上。
					this.vertex[2] = this.vertex[6];
					this.vertex[3] = this.vertex[1];

					//左下。
					this.vertex[4] = this.vertex[0];
					this.vertex[5] = this.vertex[7];
				}
			}
		}

		/** 頂点情報。
		*/
		public float[] GetTexCoord()
		{
			return this.texcood;
		}

		/** 頂点情報。
		*/
		public float[] GetVertex()
		{
			return this.vertex;
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureX(float a_texture_x)
		{
			this.texcood_recalc |= this.rect.SetTextureX(a_texture_x);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureY(float a_texture_y)
		{
			this.texcood_recalc |= this.rect.SetTextureY(a_texture_y);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureW(float a_texture_w)
		{
			this.texcood_recalc |= this.rect.SetTextureW(a_texture_w);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureH(float a_texture_h)
		{
			this.texcood_recalc |= this.rect.SetTextureH(a_texture_h);
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
			this.vertex_recalc |= this.rect.SetX(a_x);
		}

		/** 矩形。設定。
		*/
		public void SetY(int a_y)
		{
			this.vertex_recalc |= this.rect.SetY(a_y);
		}

		/** 矩形。設定。
		*/
		public void SetXY(int a_x,int a_y)
		{
			this.vertex_recalc |= this.rect.SetX(a_x);
			this.vertex_recalc |= this.rect.SetY(a_y);
		}

		/** 矩形。設定。
		*/
		public void SetW(int a_w)
		{
			this.vertex_recalc |= this.rect.SetW(a_w);
		}

		/** 矩形。設定。
		*/
		public void SetH(int a_h)
		{
			this.vertex_recalc |= this.rect.SetH(a_h);
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
			this.vertex_recalc |= this.rect.SetW(a_w);
			this.vertex_recalc |= this.rect.SetH(a_h);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureRect(in Fee.Geometry.Rect2D_R<float> a_texture_rect)
		{
			this.texcood_recalc |= this.rect.SetTextureRect(in a_texture_rect);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureRect(float a_texture_x,float a_texture_y,float a_texture_w,float a_texture_h)
		{
			this.texcood_recalc |= this.rect.SetTextureX(a_texture_x);
			this.texcood_recalc |= this.rect.SetTextureY(a_texture_y);
			this.texcood_recalc |= this.rect.SetTextureW(a_texture_w);
			this.texcood_recalc |= this.rect.SetTextureH(a_texture_h);
		}

		/** 矩形。設定。
		*/
		public void SetRect(in Fee.Geometry.Rect2D_R<int> a_rect)
		{
			this.vertex_recalc |= this.rect.SetRect(in a_rect);
		}

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.vertex_recalc |= this.rect.SetX(a_x);
			this.vertex_recalc |= this.rect.SetY(a_y);
			this.vertex_recalc |= this.rect.SetW(a_w);
			this.vertex_recalc |= this.rect.SetH(a_h);
		}

		/** 回転。設定。
		*/
		public void SetRotate(bool a_flag)
		{
			this.vertex_recalc |= this.rotate.SetRotate(a_flag);
		}

		/** 回転。取得。
		*/
		public bool IsRotate()
		{
			return this.rotate.IsRotate();
		}

		/** 中心。設定。
		*/
		public void SetCenter(int a_global_x,int a_global_y)
		{
			this.rotate.SetCenterX(a_global_x);
			this.rotate.SetCenterY(a_global_y);
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
			this.vertex_recalc |= this.rotate.SetQuaternion(a_euler_x,a_euler_y,a_euler_z);
		}

		/** クォータニオン。設定。
		*/
		public void SetQuaternion(ref UnityEngine.Quaternion a_quaternion)
		{
			this.vertex_recalc |= this.rotate.SetQuaternion(ref a_quaternion);
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

				//ソートリクエスト。
				Render2D.GetInstance().SpriteListSortRequest();
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
		public void SetColor(in UnityEngine.Color a_color)
		{
			this.param.SetColor(in a_color);
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

