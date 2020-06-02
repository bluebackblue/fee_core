

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。インプット。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Input
	*/
	public class Input : Fee.Pool.PoolItem_Base , Fee.Focus.FocusItem_Base , Fee.EventPlate.OnEventPlateOver_CallBackInterface<int> , Fee.Ui.OnTarget_CallBackInterface , Fee.Deleter.OnDelete_CallBackInterface
	{
		/** inputfield
		*/
		private Fee.Render2D.InputField2D inputfield;

		/** eventplate
		*/
		private Fee.EventPlate.Item eventplate;

		/** is_onover
		*/
		private bool is_onover;

		/** callbackparam_click
		*/
		protected Fee.Ui.OnInputClick_CallBackParam callbackparam_click;

		/** constructor

			プール用に作成。

		*/
		public Input()
		{
		}

		/** 作成。
		*/
		public static Input Create(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			Input t_this = new Input();
			{
				//inputfield
				t_this.inputfield = Fee.Render2D.InputField2D.Create(null,a_drawpriority);

				//eventplate
				t_this.eventplate = new EventPlate.Item(null,EventPlate.EventType.Button,a_drawpriority);
				t_this.eventplate.SetOnEventPlateOver(t_this,-1);

				//is_onover
				t_this.is_onover = false;

				//callbackparam_click
				t_this.callbackparam_click = null;

				if(a_deleter != null){
					a_deleter.Regist(t_this);
				}
			}
			return t_this;
		}

		/** [Fee.Ui.OnEventPlateOver_CallBackInterface]イベントプレートに入場。
		*/
		public void OnEventPlateEnter(int a_id)
		{
			this.is_onover = true;

			//ターゲット登録。
			Ui.GetInstance().SetTargetRequest(this);
		}

		/** [Fee.Ui.OnEventPlateOver_CallBackInterface]イベントプレートから退場。
		*/
		public void OnEventPlateLeave(int a_id)
		{
			this.is_onover = false;
		}

		/** コールバックインターフェイス。設定。

			Fee.Focus.FocusGroupeを指定する。
			フォーカス変更時に呼び出すコールバック。
			Fee.Focus.Mainから呼び出される。

		*/
		public void SetOnFocusCheck<T>(Fee.Focus.OnFocusCheck_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.inputfield.SetOnFocusCheck(a_callback_interface,a_id);
		}

		/** [Fee.Focus.FocusItem_Base]フォーカス。設定。
		*/
		public void SetFocus(bool a_flag)
		{
			this.inputfield.SetFocus(a_flag);
		}

		/** [Fee.Focus.FocusItem_Base]フォーカス。チェック。
		*/
		public bool IsFocus()
		{
			return this.inputfield.IsFocus();
		}

		/** [Fee.Focus.FocusItem_Base]フォーカス。設定。OnFocusCheckを呼び出す。
		*/
		public void SetFocusCallOnFocusCheck(bool a_flag)
		{
			this.inputfield.SetFocusCallOnFocusCheck(a_flag);
		}

		/** [Fee.Deleter.OnDelete_CallBackInterface]削除。
		*/
		public void OnDelete()
		{
			//OnDelete
			this.inputfield.OnDelete();
			this.eventplate.OnDelete();

			//コールバック解除。
			this.callbackparam_click = null;

			//ターゲット解除。
			Fee.Ui.Ui.GetInstance().UnSetTargetRequest(this);
		}

		/** [Fee.Pool.PoolItem_Base]プールアイテムをメモリから削除。
		*/
		public void OnPoolItemDeleteFromMemory()
		{
		}

		/** コールバックインターフェイス。設定。
		*/
		public void SetOnInputClick<T>(Fee.Ui.OnInputClick_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.callbackparam_click = new Fee.Ui.OnInputClick_CallBackParam_Generic<T>(a_callback_interface,a_id);
		}

		/** クリップ。設定。
		*/
		public void SetClip(bool a_flag)
		{
			this.inputfield.SetClip(a_flag);
			this.eventplate.SetClip(a_flag);
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
			this.eventplate.SetClipRect(in a_rect);
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.inputfield.SetClipRect(a_x,a_y,a_w,a_h);
			this.eventplate.SetClipRect(a_x,a_y,a_w,a_h);
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
			this.eventplate.SetY(a_y);
		}

		/** 矩形。設定。
		*/
		public void SetXY(int a_x,int a_y)
		{
			this.inputfield.SetXY(a_x,a_y);
			this.eventplate.SetXY(a_x,a_y);
		}

		/** 矩形。設定。
		*/
		public void SetW(int a_w)
		{
			this.inputfield.SetW(a_w);
			this.eventplate.SetW(a_w);
		}

		/** 矩形。設定。
		*/
		public void SetH(int a_h)
		{
			this.inputfield.SetH(a_h);
			this.eventplate.SetH(a_h);
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
			this.eventplate.SetWH(a_w,a_h);
		}

		/** 矩形。設定。
		*/
		public void SetRect(in Fee.Geometry.Rect2D_R<int> a_rect)
		{
			this.inputfield.SetRect(in a_rect);
			this.eventplate.SetRect(in a_rect);
		}

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.inputfield.SetRect(a_x,a_y,a_w,a_h);
			this.eventplate.SetRect(a_x,a_y,a_w,a_h);
		}

		/** 表示。設定。
		*/
		public void SetVisible(bool a_flag)
		{
			this.inputfield.SetVisible(a_flag);
			this.eventplate.SetEnable(a_flag);
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
			this.eventplate.SetPriority(a_drawpriority);
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

		/** [Fee.Ui.OnTarget_CallBackInterface]ターゲット中。
		*/
		public void OnTarget()
		{
			if(this.is_onover == true){
				//オーバー中。

				if(Fee.Input.Input.GetInstance().mouse.left.down == true){
					//コールバック。
					if(this.callbackparam_click != null){
						this.callbackparam_click.Call();
					}
				}
			}else{
				//ターゲット解除。
				Ui.GetInstance().UnSetTargetRequest(this);
			}
		}
	}
}

