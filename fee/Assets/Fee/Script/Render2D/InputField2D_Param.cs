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
	/** InputField2D_Param
	*/
	public struct InputField2D_Param
	{
		/** フォントサイズ。
		*/
		private int fontsize;

		/** 色。
		*/
		//private Color color;

		/** センター。
		*/
		private bool is_center;

		/** フォント。
		*/
		private Font font;

		/** raw
		*/
		private GameObject raw_gameobject;
		private Transform raw_transform;
		private UnityEngine.UI.InputField raw_inputfield;
		private UnityEngine.RectTransform raw_recttransform;
		private UnityEngine.UI.Text raw_text;
		private bool raw_is_recalc;

		/** 初期化。
		*/
		public void Initialze()
		{
			//フォントサイズ。
			this.fontsize = Config.DEFAULT_TEXT_FONTSIZE;

			//色。
			//this.color = Config.DEFAULT_TEXT_COLOR;

			//センター。
			this.is_center = false;

			//フォント。
			this.font = Render2D.GetInstance().GetDefaultFont();

			//

			//raw
			this.raw_gameobject = Render2D.GetInstance().RawInputField_Create();
			this.raw_transform = this.raw_gameobject.GetComponent<Transform>();
			this.raw_inputfield = this.raw_gameobject.GetComponent<UnityEngine.UI.InputField>();
			this.raw_recttransform = this.raw_gameobject.GetComponent<UnityEngine.RectTransform>();
			this.raw_text = this.raw_inputfield.textComponent;
			this.raw_is_recalc = true;
		}

		/** フォーカス。取得。
		*/
		public bool IsFocused()
		{
			return this.raw_inputfield.isFocused;
		}

		/** フォーカス。設定。
		*/
		public void SetFocuse()
		{
			this.raw_inputfield.ActivateInputField();
		}

		/** テキスト。設定。
		*/
		public void SetText(string a_text)
		{
			this.raw_inputfield.text = a_text;			
		}

		/** テキスト。取得。
		*/
		public string GetText()
		{
			return this.raw_inputfield.text;
		}

		/** 複数行。設定。
		*/
		public void SetMultiLine(bool a_flag)
		{
			if(a_flag == true){
				this.raw_inputfield.lineType = UnityEngine.UI.InputField.LineType.MultiLineNewline;
			}else{
				this.raw_inputfield.lineType = UnityEngine.UI.InputField.LineType.SingleLine;
			}
		}

		/** テキスト。設定。
		*/
		public void SetFontSize(int a_fontsize)
		{
			if(this.fontsize != a_fontsize){
				this.fontsize = a_fontsize;
				this.raw_is_recalc = true;
			}
		}

		/** テキスト。取得。
		*/
		public int GetFontSize()
		{
			return this.fontsize;
		}

		/** 色。設定。
		*/
		/*
		public void SetColor(ref Color a_color)
		{
			if(this.color != a_color){
				this.color = a_color;
				this.raw_is_recalc = true;
			}
		}
		*/

		/** 色。設定。
		*/
		/*
		public void SetColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.color.r = a_r;
			this.color.g = a_g;
			this.color.b = a_b;
			this.color.a = a_a;
		}
		*/

		/** 色。取得。
		*/
		/*
		public Color GetColor()
		{
			return this.color;
		}
		*/

		/** センター。設定。
		*/
		public void SetCenter(bool a_flag)
		{
			this.is_center = a_flag;
		}

		/** センター。取得。
		*/
		public bool IsCenter()
		{
			return this.is_center;
		}

		/** フォント。設定。
		*/
		public void SetFont(Font a_font)
		{
			if(this.font != a_font){
				this.font = a_font;
				this.raw_is_recalc = true;
			}
		}

		/** フォント。取得。
		*/
		public Font GetFont()
		{
			return this.font;
		}

		/** 削除。
		*/
		public void Delete()
		{
			Render2D.GetInstance().RawInputField_Delete(this.raw_gameobject);
			this.raw_gameobject = null;
		}

		/** [内部からの呼び出し]レイヤー。設定。
		*/
		public void Raw_SetLayer(Transform a_layer_transform)
		{
			if(a_layer_transform == null){
				this.raw_gameobject.SetActive(false);
			}else{
				this.raw_transform.SetParent(a_layer_transform);
				this.raw_gameobject.SetActive(true);
			}
		}

		/** [内部からの呼び出し]入力フィールド。取得。
		*/
		public UnityEngine.UI.InputField Raw_GetInputFieldInstance()
		{
			return this.raw_inputfield;
		}

		/** [内部からの呼び出し]RectTransform。取得。
		*/
		public UnityEngine.RectTransform Raw_GetRectTransformInstance()
		{
			return this.raw_recttransform;
		}

		/** [内部からの呼び出し]テキスト。取得。
		*/
		public UnityEngine.UI.Text Raw_GetTextInstance()
		{
			return this.raw_text;
		}

		/** [内部からの呼び出し]再計算フラグ。取得。
		*/
		public bool Raw_IsReCalc()
		{
			return this.raw_is_recalc;
		}

		/** [内部からの呼び出し]再計算フラグ。設定。
		*/
		public void Raw_ResetReCalc()
		{
			this.raw_is_recalc = false;
		}
	}
}

