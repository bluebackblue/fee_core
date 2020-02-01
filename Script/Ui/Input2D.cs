

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
	/** Input2D
	*/
	public class Input2D : Fee.Deleter.OnDelete_CallBackInterface , Fee.Pool.PoolItem_Base
	{
		/** inputfield
		*/
		private Fee.Render2D.InputField2D inputfield;

		/** constructor

			プール用に作成。

		*/
		public Input2D()
		{
		}

		/** 作成。
		*/
		public static Input2D Create(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			Input2D t_this = new Input2D();
			{
				t_this.inputfield = Fee.Render2D.InputField2D.Create(null,a_drawpriority);

				if(a_deleter != null){
					a_deleter.Regist(t_this);
				}
			}
			return t_this;
		}

		/** コールバックインターフェイス。設定。
		*/
		public void SetOnFocusCheck<T>(Fee.Ui.OnFocusCheck_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.inputfield.SetOnFocusCheck(a_callback_interface,a_id);
		}

		/** [Fee.Deleter.OnDelete_CallBackInterface]削除。
		*/
		public void OnDelete()
		{
			//OnDelete
			this.inputfield.OnDelete();
		}

		/** [Fee.Pool.PoolItem_Base]プールアイテムをメモリから削除。
		*/
		public void OnPoolItemDeleteFromMemory()
		{
		}

		/** フォーカス。取得。
		*/
		public bool IsFocus()
		{
			return this.inputfield.IsFocus();
		}

		/** フォーカス。設定。
		*/
		public void SetFocus(bool a_flag)
		{
			this.inputfield.SetFocus(a_flag);
		}

		/** フォーカス。設定。

			OnFocusCheckを呼び出す。

		*/
		public void SetFocusCallOnFocusCheck(bool a_flag)
		{
			this.inputfield.SetFocusCallOnFocusCheck(a_flag);
		}

		/** クリップ。設定。
		*/
		public void SetClip(bool a_flag)
		{
			this.inputfield.SetClip(a_flag);
		}

		/** クリップ。取得。
		*/
		public bool IsClip()
		{
			return this.inputfield.IsClip();
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(in Fee.Geometry.Rect2D_R<int> a_rect)
		{
			this.inputfield.SetClipRect(in a_rect);
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.inputfield.SetClipRect(a_x,a_y,a_w,a_h);
		}

		/** クリップ矩形。取得。
		*/
		public int GetClipX()
		{
			return this.inputfield.GetClipX();
		}

		/** クリップ矩形。取得。
		*/
		public int GetClipY()
		{
			return this.inputfield.GetClipY();
		}

		/** クリップ矩形。取得。
		*/
		public int GetClipW()
		{
			return this.inputfield.GetClipW();
		}

		/** クリップ矩形。取得。
		*/
		public int GetClipH()
		{
			return this.inputfield.GetClipH();
		}

		/** 矩形。設定。
		*/
		public void SetX(int a_x)
		{
			this.inputfield.SetX(a_x);
		}

		/** 矩形。設定。
		*/
		public void SetY(int a_y)
		{
			this.inputfield.SetY(a_y);
		}

		/** 矩形。設定。
		*/
		public void SetXY(int a_x,int a_y)
		{
			this.inputfield.SetXY(a_x,a_y);
		}

		/** 矩形。設定。
		*/
		public void SetW(int a_w)
		{
			this.inputfield.SetW(a_w);
		}

		/** 矩形。設定。
		*/
		public void SetH(int a_h)
		{
			this.inputfield.SetH(a_h);
		}

		/** 矩形。取得。
		*/
		public int GetX()
		{
			return this.inputfield.GetX();
		}

		/** 矩形。取得。
		*/
		public int GetY()
		{
			return this.inputfield.GetY();
		}

		/** 矩形。取得。
		*/
		public int GetW()
		{
			return this.inputfield.GetW();
		}

		/** 矩形。取得。
		*/
		public int GetH()
		{
			return this.inputfield.GetH();
		}

		/** 矩形。矩形。設定。
		*/
		public void SetWH(int a_w,int a_h)
		{
			this.inputfield.SetWH(a_w,a_h);
		}

		/** 矩形。設定。
		*/
		public void SetRect(in Fee.Geometry.Rect2D_R<int> a_rect)
		{
			this.inputfield.SetRect(in a_rect);
		}

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.inputfield.SetRect(a_x,a_y,a_w,a_h);
		}

		/** 表示。設定。
		*/
		public void SetVisible(bool a_flag)
		{
			this.inputfield.SetVisible(a_flag);
		}

		/** 表示。取得。
		*/
		public bool IsVisible()
		{
			return this.inputfield.IsVisible();
		}

		/** 描画プライオリティ。設定。
		*/
		public void SetDrawPriority(long a_drawpriority)
		{
			this.inputfield.SetDrawPriority(a_drawpriority);
		}

		/** 描画プライオリティ。取得。
		*/
		public long GetDrawPriority()
		{
			return this.inputfield.GetDrawPriority();
		}

		/** テキスト。設定。
		*/
		public void SetText(string a_text)
		{
			this.inputfield.SetText(a_text);
		}

		/** テキスト。取得。
		*/
		public string GetText()
		{
			return this.inputfield.GetText();
		}

		/** マルチライン。設定。
		*/
		public void SetMultiLine(bool a_flag)
		{
			this.inputfield.SetMultiLine(a_flag);
		}

		/** パスワードタイプ。設定。
		*/
		public void SetPasswordType(bool a_flag)
		{
			this.inputfield.SetPasswordType(a_flag);
		}

		/** フォントサイズ。設定。
		*/
		public void SetFontSize(int a_fontsize)
		{
			this.inputfield.SetFontSize(a_fontsize);
		}

		/** フォントサイズ。取得。
		*/
		public int GetFontSize()
		{
			return this.inputfield.GetFontSize();
		}

		/** イメージ色。設定。
		*/
		public void SetImageColor(in UnityEngine.Color a_color)
		{
			this.inputfield.SetImageColor(in a_color);
		}

		/** イメージ色。設定。
		*/
		public void SetImageColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.inputfield.SetImageColor(a_r,a_g,a_b,a_a);
		}

		/** イメージ色。取得。
		*/
		public UnityEngine.Color GetImageColor()
		{
			return this.inputfield.GetImageColor();
		}

		/** テキスト色。設定。
		*/
		public void SetTextColor(in UnityEngine.Color a_color)
		{
			this.inputfield.SetTextColor(in a_color);
		}

		/** テキスト色。設定。
		*/
		public void SetTextColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.inputfield.SetTextColor(a_r,a_g,a_b,a_a);
		}

		/** テキスト色。取得。
		*/
		public UnityEngine.Color GetTextColor()
		{
			return this.inputfield.GetTextColor();
		}

		/** センター。設定。
		*/
		public void SetCenter(bool a_flag)
		{
			this.inputfield.SetCenter(a_flag);
		}

		/** センター。設定。
		*/
		public bool IsCenter()
		{
			return this.inputfield.IsCenter();
		}

		/** フォント。設定。
		*/
		public void SetFont(UnityEngine.Font a_font)
		{
			this.inputfield.SetFont(a_font);
		}

		/** フォント。取得。
		*/
		public UnityEngine.Font GetFont()
		{
			return this.inputfield.GetFont();
		}
	}
}

