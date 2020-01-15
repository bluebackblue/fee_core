

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。テキスト。
*/


// The private field * is assigned but its value is never used
#if(UNITY_EDITOR)
#pragma warning disable 0414
#endif


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** Text2D
	*/
	public class Text2D : Fee.Deleter.OnDelete_CallBackInterface , Fee.Pool.PoolItem_Base
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
		private Text2D_Rect rect;

		/** パラメータ。
		*/
		private Text2D_Param param;

		/** debug
		*/
		#if(UNITY_EDITOR)
		private string debug;
		#endif

		/** constructor

			プール用に作成。

		*/

		public Text2D()
		{
			//param
			this.param.Initialize();
		}

		/** 作成。

			「DRAWPRIORITY_STEP」ごとに描画カメラが切り替わる。
			同一カメラ内では必ずテキストが上に表示される。
			テキストの上にスプライトを表示する場合は、描画カメラが切り替わるようにプライオリィを設定する必要がある。

		*/
		public static Text2D Create(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			Text2D t_this = Fee.Render2D.Render2D.GetInstance().GetTextList().PoolNew();
			{
				#if(UNITY_EDITOR)
				{
					try{
						System.Diagnostics.StackFrame t_stackframe = new System.Diagnostics.StackFrame(1);
						if(t_stackframe != null){
							if(t_stackframe.GetMethod() != null){
								t_this.debug = t_stackframe.GetMethod().ReflectedType.FullName + " : " + t_stackframe.GetMethod().Name;
							}
						}
					}catch(System.Exception t_exception){
						Tool.DebugReThrow(t_exception);
					}
				}
				#endif

				//表示フラグ。
				t_this.visible = true;

				//削除フラグ。
				t_this.is_delete_request = false;
				Render2D.GetInstance().Text2D_Regist(t_this);

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
			Render2D.GetInstance().GetTextList().delete_request_flag = true;
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
			//サイズの計算が必要。設定。
			if(this.rect.GetW() != a_w){
				this.param.Raw_SetCalcSizeFlag(true);
			}

			this.rect.SetW(a_w);
		}

		/** 矩形。設定。
		*/
		public void SetH(int a_h)
		{
			//サイズの計算が必要。設定。
			if(this.rect.GetH() != a_h){
				this.param.Raw_SetCalcSizeFlag(true);
			}

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
		public void SetWH(int a_w,int a_h)
		{
			//サイズの計算が必要。設定。
			if((this.rect.GetW() != a_w)||(this.rect.GetH() != a_h)){
				this.param.Raw_SetCalcSizeFlag(true);
			}

			this.rect.SetW(a_w);
			this.rect.SetH(a_h);
		}

		/** 矩形。設定。
		*/
		public void SetRect(in Fee.Geometry.Rect2D_R<int> a_rect)
		{
			//サイズの計算が必要。設定。
			if((this.rect.GetW() != a_rect.w)||(this.rect.GetH() != a_rect.h)){
				this.param.Raw_SetCalcSizeFlag(true);
			}

			this.rect.SetRect(in a_rect);
		}

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			//サイズの計算が必要。設定。
			if((this.rect.GetW() != a_w)||(this.rect.GetH() != a_h)){
				this.param.Raw_SetCalcSizeFlag(true);
			}

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
				Render2D.GetInstance().GetTextList().sort_request_flag = true;
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

		/** アウトライン色。設定。
		*/
		public void SetOutLineColor(in UnityEngine.Color a_color)
		{
			this.param.SetOutLineColor(in a_color);
		}

		/** アウトライン色。設定。
		*/
		public void SetOutLineColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.param.SetOutLineColor(a_r,a_g,a_b,a_a);
		}

		/** アウトライン色。取得。
		*/
		public UnityEngine.Color GetOutLineColor()
		{
			return this.param.GetOutLineColor();
		}

		/** アライメントタイプ。設定。
		*/
		public void SetAlignmentType(Text2D_HorizontalAlignmentType a_alignment_type_x,Text2D_VerticalAlignmentType a_alignment_type_y)
		{
			this.param.SetAlignmentType(a_alignment_type_x,a_alignment_type_y);
		}

		/** アライメントタイプ。取得。
		*/
		public Text2D_HorizontalAlignmentType GetAlignmentTypeX()
		{
			return this.param.GetAlignmentTypeX();
		}

		/** アライメントタイプ。取得。
		*/
		public Text2D_VerticalAlignmentType GetAlignmentTypeY()
		{
			return this.param.GetAlignmentTypeY();
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

		/** アウトライン。設定。
		*/
		public void SetOutLine(bool a_flag)
		{
			this.param.SetOutLine(a_flag);
		}

		/** アウトライン。取得。
		*/
		public bool GetOutLine()
		{
			return this.param.GetOutLine();
		}

		/** シャドー。設定。
		*/
		public void SetShadow(bool a_flag)
		{
			this.param.SetShadow(a_flag);
		}

		/** シャドー。取得。
		*/
		public bool GetShadow()
		{
			return this.param.GetShadow();
		}

		/** [内部からの呼び出し]最適横幅。取得。
		*/
		public float Raw_GetPreferredWidth()
		{
			return this.param.Raw_GetPreferredWidth();
		}

		/** [内部からの呼び出し]最適縦幅。取得。
		*/
		public float Raw_GetPreferredHeight()
		{
			return this.param.Raw_GetPreferredHeight();
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

		/** [内部からの呼び出し]サイズの計算が必要。取得。
		*/
		public bool Raw_IsCalcSize()
		{
			return this.param.Raw_IsCalcSize();
		}

		/** [内部からの呼び出し]サイズの計算が必要。設定。
		*/
		public void Raw_SetCalcSizeFlag(bool a_flag)
		{
			this.param.Raw_SetCalcSizeFlag(a_flag);
		}

	}
}

