using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ２Ｄ描画。入力フィールド。
*/


/** Render2D
*/
namespace NRender2D
{
	/** InputField2D
	*/
	public class InputField2D : NDeleter.DeleteItem_Base
	{
		/** 状態。
		*/
		private State2D state;

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
		private InputField2D_Rect rect;

		/** パラメータ。
		*/
		private InputField2D_Param param;

		/** constructor
		*/
		public InputField2D(NDeleter.Deleter a_deleter,State2D a_state,long a_drawpriority)
		{
			Render2D.GetInstance().AddInputField2D(this);

			//状態。
			this.state = a_state;

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

		/** 削除。
		*/
		public void Delete()
		{
			this.deletereq = true;

			//非表示。
			this.visible = false;

			//rawの削除。
			this.param.Delete();

			//更新リクエスト。
			Render2D.GetInstance().UpdateInputFieldListRequest();
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

		/** 矩形。設定。
		*/
		public void SetRect(ref Rect2D_R<int> a_rect)
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

		/** 状態。設定。
		*/
		public void SetState(State2D a_state)
		{
			this.state = a_state;
		}

		/** 状態。取得。
		*/
		public State2D GetState()
		{
			return this.state;
		}

		/** 表示。設定。
		*/
		public void SetVisible(bool a_flag)
		{
			this.visible = a_flag;
		}

		/** 表示。取得。
		*/
		private bool IsVisible_State()
		{
			if(this.state != null){
				return this.state.IsVisible();
			}
			return true;
		}

		/** 表示。取得。
		*/
		public bool IsVisible()
		{
			return this.visible && this.IsVisible_State();
		}

		/** 描画プライオリティ。設定。
		*/
		public void SetDrawPriority(long a_drawpriority)
		{
			if(this.drawpriority != a_drawpriority){
				this.drawpriority = a_drawpriority;
		
				//更新リクエスト。
				Render2D.GetInstance().UpdateInputFieldListRequest();
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
		public static int Sort_DrawPriority(InputField2D a_test,InputField2D a_target)
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

		/** マルチライン。設定。
		*/
		public void SetMultiLine(bool a_flag)
		{
			this.param.SetMultiLine(a_flag);
		}

		/** フォーカス。取得。
		*/
		public bool IsFocused()
		{
			return this.param.IsFocused();
		}

		/** フォーカス。設定。
		*/
		public void SetFocuse()
		{
			this.param.SetFocuse();
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
		/*
		public void SetColor(ref Color a_color)
		{
			this.param.SetColor(ref a_color);
		}
		*/

		/** 色。設定。
		*/
		/*
		public void SetColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.param.SetColor(a_r,a_g,a_b,a_a);
		}
		*/

		/** 色。取得。
		*/
		/*
		public Color GetColor()
		{
			return this.param.GetColor();
		}
		*/

		/** センター。設定。
		*/
		public void SetCenter(bool a_flag)
		{
			this.param.SetCenter(a_flag);
		}

		/** センター。設定。
		*/
		public bool IsCenter()
		{
			return this.param.IsCenter();
		}

		/** フォント。設定。
		*/
		public void SetFont(Font a_font)
		{
			this.param.SetFont(a_font);
		}

		/** フォント。取得。
		*/
		public Font GetFont()
		{
			return this.param.GetFont();
		}

		/** [内部からの呼び出し]レイヤー。設定。
		*/
		public void Raw_SetLayer(Transform a_layer_transform)
		{
			this.param.Raw_SetLayer(a_layer_transform);
		}

		/** [内部からの呼び出し]入力フィールド。取得。
		*/
		public UnityEngine.UI.InputField Raw_GetInputFieldInstance()
		{
			return this.param.Raw_GetInputFieldInstance();
		}

		/** [内部からの呼び出し]RectTransform。取得。
		*/
		public UnityEngine.RectTransform Raw_GetRectTransformInstance()
		{
			return this.param.Raw_GetRectTransformInstance();
		}

		/** [内部からの呼び出し]テキスト。取得。
		*/
		public UnityEngine.UI.Text Raw_GetTextInstance()
		{
			return this.param.Raw_GetTextInstance();
		}

		/** [内部からの呼び出し]再計算フラグ。取得。
		*/
		public bool Raw_IsReCalc()
		{
			return this.param.Raw_IsReCalc();
		}

		/** [内部からの呼び出し]再計算フラグ。設定。
		*/
		public void Raw_ResetReCalc()
		{
			this.param.Raw_ResetReCalc();
		}
	}
}

