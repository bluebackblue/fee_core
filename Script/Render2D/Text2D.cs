

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
	public class Text2D : Fee.Deleter.OnDelete_CallBackInterface
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
		*/
		public Text2D(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
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

			Render2D.GetInstance().AddText2D(this);

			//表示フラグ。
			this.visible = true;

			//削除フラグ。
			this.deletereq = false;

			//描画プライオリティ。
			this.drawpriority = a_drawpriority;

			//位置。
			//this.pos;

			//パラメータ。
			this.param.Initialze();

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
			Render2D.GetInstance().UpdateTextListRequest();
		}

		/** 削除チェック。
		*/
		public bool IsDelete()
		{
			return this.deletereq;
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
		public void SetRect(ref Rect2D_R<int> a_rect)
		{
			//サイズの計算が必要。設定。
			if((this.rect.GetW() != a_rect.w)||(this.rect.GetH() != a_rect.h)){
				this.param.Raw_SetCalcSizeFlag(true);
			}

			this.rect.SetRect(ref a_rect);
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
		public void SetClipRect(ref Fee.Render2D.Rect2D_R<int> a_rect)
		{
			this.param.SetClipRect(ref a_rect);
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

		/** カスタムテキストマテリアル。取得。
		*/
		public UnityEngine.Material GetCustomTextMaterial()
		{
			return this.param.GetCustomTextMaterial();
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
				Render2D.GetInstance().UpdateTextListRequest();
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
		public static int Sort_DrawPriority(Text2D a_test,Text2D a_target)
		{
			return (int)(a_test.drawpriority - a_target.drawpriority);
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

		/** アウトライン色。設定。
		*/
		public void SetOutLineColor(ref UnityEngine.Color a_color)
		{
			this.param.SetOutLineColor(ref a_color);
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

		/** センター。設定。
		*/
		public void SetCenter(bool a_flag_w,bool a_flag_h)
		{
			this.param.SetCenter(a_flag_w,a_flag_h);
		}

		/** センター。設定。
		*/
		public bool IsCenterW()
		{
			return this.param.IsCenterW();
		}

		/** センター。設定。
		*/
		public bool IsCenterH()
		{
			return this.param.IsCenterH();
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
		public void Raw_SetRectTransformSizeDelta(ref UnityEngine.Vector2 a_size)
		{
			this.param.Raw_SetRectTransformSizeDelta(ref a_size);
		}

		/** [内部からの呼び出し]サイズ。取得。
		*/
		public void Raw_GetRectTransformSizeDelta(out UnityEngine.Vector2 a_size)
		{
			this.param.Raw_GetRectTransformSizeDelta(out a_size);
		}

		/** [内部からの呼び出し]位置。設定。
		*/
		public void Raw_SetRectTransformLocalPosition(ref UnityEngine.Vector3 a_position)
		{
			this.param.Raw_SetRectTransformLocalPosition(ref a_position);
		}

		/** [内部からの呼び出し]フォントサイズ。設定。
		*/
		public void Raw_SetFontSize(int a_raw_fontsize)
		{
			this.param.Raw_SetFontSize(a_raw_fontsize);
		}

		/** [内部からの呼び出し]テキストマテリアル。設定。
		*/
		public void Raw_SetTextMaterial(UnityEngine.Material a_material)
		{
			this.param.Raw_SetTextMaterial(a_material);
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

