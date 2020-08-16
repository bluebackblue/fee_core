

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。入力フィールド。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** InputField2D
	*/
	public class InputField2D : Fee.Deleter.OnDelete_CallBackInterface , Fee.Pool.PoolItem_Base , Fee.Focus.FocusItem_Base
	{
		/** 表示フラグ。
		*/
		private bool visible;

		/** 削除フラグ。
		*/
		private bool is_delete_request; 

		/** 描画プライオリティ。
		*/
		private long drawpriority;

		/** 矩形。
		*/
		private InputField2D_Rect rect;

		/** パラメータ。
		*/
		private InputField2D_Param param;

		/** debug
		*/
		#if(UNITY_EDITOR)
		private string debug;
		#endif

		/** constructor

			プール用に作成。

		*/
		public InputField2D()
		{
			//param
			this.param.Initialize(this);
		}

		/** 作成。

			「DRAWPRIORITY_STEP」ごとに描画カメラが切り替わる。
			同一カメラ内では必ずテキストが上に表示される。
			テキストの上にスプライトを表示する場合は、描画カメラが切り替わるようにプライオリィを設定する必要がある。

		*/
		public static InputField2D Create(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			InputField2D t_this = Fee.Render2D.Render2D.GetInstance().GetInputFieldList().PoolNew();
			{
				//どこから確保されたのか。
				#if(UNITY_EDITOR)
				{
					try{
						System.Text.StringBuilder t_stringbuilder = new System.Text.StringBuilder(Config.DEBUG_TRACECOUNT * 32);
						for(int ii=Config.DEBUG_TRACECOUNT;ii>=1;ii--){
							System.Diagnostics.StackFrame t_stackframe = new System.Diagnostics.StackFrame(ii);
							if(t_stackframe != null){
								if(t_stackframe.GetMethod() != null){
									t_stringbuilder.Append(t_stackframe.GetMethod().ReflectedType.FullName);
									t_stringbuilder.Append(" : ");
									t_stringbuilder.Append(t_stackframe.GetMethod().Name);
									t_stringbuilder.Append("\n");
								}
							}
						}
						t_this.debug = t_stringbuilder.ToString();
					}catch(System.Exception t_exception){
						Tool.DebugReThrow(t_exception);
					}
				}
				#endif

				//表示フラグ。
				t_this.visible = true;

				//削除フラグ。
				t_this.is_delete_request = false;
				Render2D.GetInstance().InputField2D_Regist(t_this);

				//描画プライオリティ。
				t_this.drawpriority = a_drawpriority;

				//位置。
				//t_this.pos;

				//パラメータ。
				t_this.param.InitializeFromPool();

				//削除管理。
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
			//非表示。
			this.visible = false;

			//非表示設定。
			this.param.Raw_SetEnable(false);
			this.param.Raw_SetText("");

			//削除リクエスト。
			this.is_delete_request = true;
			Render2D.GetInstance().GetInputFieldList().delete_request_flag = true;
		}

		/** [Fee.Pool.PoolItem_Base]プールアイテムをメモリから削除。
		*/
		public void OnPoolItemDeleteFromMemory()
		{
			this.param.DeleteFromMemory();
		}

		/** 削除チェック。
		*/
		public bool IsDeleteRequest()
		{
			return this.is_delete_request;
		}

		/** コールバックインターフェイス。設定。

			Fee.Focus.FocusGroupeを指定する。
			フォーカス変更時に呼び出すコールバック。
			Fee.Focus.Mainから呼び出される。

		*/
		public void SetOnFocusCheck<T>(Fee.Focus.OnFocusCheck_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.param.SetOnFocusCheck(a_callback_interface,a_id);
		}

		/** [Fee.Focus.FocusItem_Base]フォーカス。設定。
		*/
		public void SetFocus_NoCall(bool a_flag)
		{
			this.param.SetFocus_NoCall(a_flag);
		}

		/** [Fee.Focus.FocusItem_Base]フォーカス。チェック。
		*/
		public bool IsFocus()
		{
			return this.param.IsFocus();
		}

		/** [Fee.Focus.FocusItem_Base]フォーカス。設定。

			OnFocusCheckを呼び出す。

		*/
		public void SetFocus(bool a_flag)
		{
			this.param.SetFocus(a_flag);
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

		/** 矩形。設定。
		*/
		public void SetWH(int a_w,int a_h)
		{
			this.rect.SetW(a_w);
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

		/** 矩形。設定。
		*/
		public void SetRect(in Fee.Geometry.Rect2D_R<int> a_rect)
		{
			this.rect.SetRect(in a_rect);
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

		/** クリップ。設定。
		*/
		public void SetClip(bool a_flag)
		{
			this.param.SetClip(a_flag);
		}

		/** クリップ。取得。
		*/
		public bool IsClip()
		{
			return this.param.IsClip();
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(in Fee.Geometry.Rect2D_R<int> a_rect)
		{
			this.param.SetClipRect(in a_rect);
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.param.SetClipRect(a_x,a_y,a_w,a_h);
		}

		/** クリップ矩形。取得。
		*/
		public int GetClipX()
		{
			return this.param.GetClipX();
		}

		/** クリップ矩形。取得。
		*/
		public int GetClipY()
		{
			return this.param.GetClipY();
		}

		/** クリップ矩形。取得。
		*/
		public int GetClipW()
		{
			return this.param.GetClipW();
		}

		/** クリップ矩形。取得。
		*/
		public int GetClipH()
		{
			return this.param.GetClipH();
		}

		/** カスタムテキストマテリアルアイテム。取得。
		*/
		public Material_Item GetCustomTextMaterialItem()
		{
			return this.param.GetCustomTextMaterialItem();
		}

		/** カスタムイメージマテリアルアイテム。取得。
		*/
		public Material_Item GetCustomImageMaterialItem()
		{
			return this.param.GetCustomImageMaterialItem();
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
				Render2D.GetInstance().GetInputFieldList().sort_request_flag = true;
			}
		}

		/** 描画プライオリティ。取得。
		*/
		public long GetDrawPriority()
		{
			return this.drawpriority;
		}

		/** テキスト。設定。
		*/
		public void SetText(string a_text)
		{
			this.param.SetText(a_text);
		}

		/** テキスト。取得。
		*/
		public string GetText()
		{
			return this.param.GetText();
		}

		/** マルチライン。設定。
		*/
		public void SetMultiLine(bool a_flag)
		{
			this.param.SetMultiLine(a_flag);
		}

		/** パスワードタイプ。設定。
		*/
		public void SetPasswordType(bool a_flag)
		{
			this.param.SetPasswordType(a_flag);
		}

		/** フォントサイズ。設定。
		*/
		public void SetFontSize(int a_fontsize)
		{
			this.param.SetFontSize(a_fontsize);
		}

		/** フォントサイズ。取得。
		*/
		public int GetFontSize()
		{
			return this.param.GetFontSize();
		}

		/** イメージ色。設定。
		*/
		public void SetImageColor(in UnityEngine.Color a_color)
		{
			this.param.SetImageColor(in a_color);
		}

		/** イメージ色。設定。
		*/
		public void SetImageColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.param.SetImageColor(a_r,a_g,a_b,a_a);
		}

		/** イメージ色。取得。
		*/
		public UnityEngine.Color GetImageColor()
		{
			return this.param.GetImageColor();
		}

		/** テキスト色。設定。
		*/
		public void SetTextColor(in UnityEngine.Color a_color)
		{
			this.param.SetTextColor(in a_color);
		}

		/** テキスト色。設定。
		*/
		public void SetTextColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.param.SetTextColor(a_r,a_g,a_b,a_a);
		}

		/** テキスト色。取得。
		*/
		public UnityEngine.Color GetTextColor()
		{
			return this.param.GetTextColor();
		}

		/** フォント。設定。
		*/
		public void SetFont(UnityEngine.Font a_font)
		{
			this.param.SetFont(a_font);
		}

		/** フォント。取得。
		*/
		public UnityEngine.Font GetFont()
		{
			return this.param.GetFont();
		}

		/** [内部からの呼び出し]サイズ。設定。
		*/
		public void Raw_SetRectTransformSizeDelta(in UnityEngine.Vector2 a_size)
		{
			this.param.Raw_SetRectTransformSizeDelta(in a_size);
		}

		/** [内部からの呼び出し]サイズ。取得。
		*/
		public void Raw_GetRectTransformSizeDelta(out UnityEngine.Vector2 a_size)
		{
			this.param.Raw_GetRectTransformSizeDelta(out a_size);
		}

		/** [内部からの呼び出し]位置。設定。
		*/
		public void Raw_SetRectTransformLocalPosition(in UnityEngine.Vector3 a_position)
		{
			this.param.Raw_SetRectTransformLocalPosition(in a_position);
		}

		/** [内部からの呼び出し]フォントサイズ。設定。
		*/
		public void Raw_SetFontSize(int a_raw_fontsize)
		{
			this.param.Raw_SetFontSize(a_raw_fontsize);
		}

		/** [内部からの呼び出し]テキストマテリアルアイテム。設定。
		*/
		public void Raw_SetTextMaterialItem(Material_Item a_material_item)
		{
			this.param.Raw_SetTextMaterialItem(a_material_item);
		}

		/** [内部からの呼び出し]イメージマテリアルアイテム。設定。
		*/
		public void Raw_SetImageMaterialItem(Material_Item a_material_item)
		{
			this.param.Raw_SetImageMaterialItem(a_material_item);
		}

		/** [内部からの呼び出し]レイヤー。設定。
		*/
		public void Raw_SetLayer(UnityEngine.Transform a_layer_transform)
		{
			this.param.Raw_SetLayer(a_layer_transform);
		}

		/** [内部からの呼び出し]有効。設定。
		*/
		public void Raw_SetEnable(bool a_flag)
		{
			this.param.Raw_SetEnable(a_flag);
		}

		/** [内部からの呼び出し]シェーダの変更が必要。取得。
		*/
		public bool Raw_IsChangeShader()
		{
			return this.param.Raw_IsChangeShader();
		}

		/** [内部からの呼び出し]シェーダの変更が必要。取得。
		*/
		public void Raw_SetChangeShaderFlag(bool a_flag)
		{
			this.param.Raw_SetChangeShaderFlag(a_flag);
		}

		/** [内部からの呼び出し]フォントのサイズの計算が必要。取得。
		*/
		public bool Raw_IsCalcFontSize()
		{
			return this.param.Raw_IsCalcFontSize();
		}

		/** [内部からの呼び出し]フォントのサイズの計算が必要。設定。
		*/
		public void Raw_SetCalcFontSizeFlag(bool a_flag)
		{
			this.param.Raw_SetCalcFontSizeFlag(a_flag);
		}
	}
}

