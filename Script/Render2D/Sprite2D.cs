

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
	public class Sprite2D : Fee.Deleter.OnDelete_CallBackInterface , Fee.Pool.PoolItem_Base , OnSprite2DMaterialUpdate_CallBackInterface
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

		/** callback_materialupdate
		*/
		private OnSprite2DMaterialUpdate_CallBackInterface callback_materialupdate;

		/** debug
		*/
		#if(UNITY_EDITOR)
		private string debug;
		#endif

		/** constructor

			プール用に作成。

		*/
		public Sprite2D()
		{
			//param
			this.param.Initialize();

			//vertex
			this.vertex = new float[8];

			//texcood
			this.texcood = new float[4];
		}

		/** constructor

			プールから作成。

			「DRAWPRIORITY_STEP」ごとに描画カメラが切り替わる。
			同一カメラ内では必ずテキストが上に表示される。
			テキストの上にスプライトを表示する場合は、描画カメラが切り替わるようにプライオリィを設定する必要がある。

		*/
		public void PoolNew(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
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

			//表示フラグ。
			this.visible = true;

			//削除フラグ。
			this.deletereq = false;
			Render2D.GetInstance().Sprite2D_Regist(this);

			//描画プライオリティ。
			this.drawpriority = a_drawpriority;

			//位置。
			//this.pos;

			//回転。
			this.rotate.PoolNew();

			//param
			this.param.PoolNew();

			//vertex
			this.vertex_recalc = true;

			//texcood
			this.texcood_recalc = true;

			//callback_materialupdate
			this.callback_materialupdate = this;

			//削除管理。
			if(a_deleter != null){
				a_deleter.Regist(this);
			}
		}

		/** [Fee.Deleter.OnDelete_CallBackInterface]削除。

			スプライトリストからの解除リクエスト。

		*/
		public void OnDelete()
		{
			//非表示。
			this.visible = false;

			//PrePoolDelete
			this.param.PrePoolDelete();

			//削除リクエスト。
			this.deletereq = true;
			Render2D.GetInstance().GetSpriteList().delete_request_flag = true;
		}

		/** [Fee.Pool.PoolItem_Base]プールへ削除。
		*/
		public void PoolDelete()
		{
			//rawの削除。
			this.param.PoolDelete();
		}

		/** [Fee.Pool.PoolItem_Base]メモリから削除。
		*/
		public void MemoryDelete()
		{
			//rawの削除。
			this.param.MemoryDelete();
		}

		/** 削除チェック。
		*/
		public bool IsDelete()
		{
			return this.deletereq;
		}

		/** [Fee.Render2D.OnSprite2DMaterialUpdate_CallBackInterface]マテリアル更新。

			デフォルト。

		*/
		public bool OnSprite2DMaterialUpdate(Sprite2D a_sprite2d,Material_Item a_material_item)
		{
			bool t_setpass = false;

			//メインテクスチャー設定。
			if(a_material_item.SetProperty_MainTexture(a_sprite2d.GetTexture()) == true){
				t_setpass = true;
			}

			return t_setpass;
		}

		/** コールバック。
		*/
		public void SetOnSprite2DMaterialUpdate(OnSprite2DMaterialUpdate_CallBackInterface a_callback_interface)
		{
			this.callback_materialupdate = a_callback_interface;
		}

		/** マテリアルアイテムを更新する。

			return == true : 変更あり。直後にSetPassの呼び出しが行われます。

		*/
		public bool UpdateMaterialItem(Material_Item a_material_item)
		{
			return this.callback_materialupdate.OnSprite2DMaterialUpdate(this,a_material_item);
		}

		/** 再計算。リクエスト。
		*/
		public void RequestReCalcVertex()
		{
			this.vertex_recalc = true;
		}

		/** 頂点計算。
		*/
		public void CalcVertex(float a_screen_calc_x,float a_screen_calc_y,float a_screen_calc_w,float a_screen_calc_h)
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
					this.vertex[0] = t_1.x / a_screen_calc_w + a_screen_calc_x;
					this.vertex[1] = 1.0f - (t_1.y / a_screen_calc_h + a_screen_calc_y);

					//右上。
					this.vertex[2] = t_2.x / a_screen_calc_w + a_screen_calc_x;
					this.vertex[3] = 1.0f - (t_2.y / a_screen_calc_h + a_screen_calc_y);

					//左下。
					this.vertex[4] = t_3.x / a_screen_calc_w + a_screen_calc_x;
					this.vertex[5] = 1.0f - (t_3.y / a_screen_calc_h + a_screen_calc_y);

					//右下。
					this.vertex[6] = t_4.x / a_screen_calc_w + a_screen_calc_x;
					this.vertex[7] = 1.0f - (t_4.y / a_screen_calc_h + a_screen_calc_y);
				}else{
					//回転なし。

					//左上。
					this.vertex[0] = (float)this.rect.GetX() / a_screen_calc_w + a_screen_calc_x;
					this.vertex[1] = 1.0f - ((float)this.rect.GetY() / a_screen_calc_h + a_screen_calc_y);

					//右下。
					this.vertex[6] = (float)(this.rect.GetX() + this.rect.GetW()) / a_screen_calc_w + a_screen_calc_x;
					this.vertex[7] = 1.0f - ((float)(this.rect.GetY() + this.rect.GetH()) / a_screen_calc_h + a_screen_calc_y);

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
		public void SetQuaternion(in UnityEngine.Quaternion a_quaternion)
		{
			this.vertex_recalc |= this.rotate.SetQuaternion(in a_quaternion);
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
				Render2D.GetInstance().GetSpriteList().sort_request_flag = true;
			}
		}

		/** 描画プライオリティ。取得。
		*/
		public long GetDrawPriority()
		{
			return this.drawpriority;
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

