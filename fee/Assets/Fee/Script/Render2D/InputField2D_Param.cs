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

		/** センター。
		*/
		private bool is_center;

		/** 再計算が必要。
		*/
		private bool raw_is_recalc;

		/** クリップ。
		*/
		private bool clip;
		private NRender2D.Rect2D_R<int> clip_rect;

		/** raw
		*/
		private GameObject raw_gameobject;
		private Transform raw_transform;
		private UnityEngine.UI.InputField raw_inputfield;
		private UnityEngine.RectTransform raw_recttransform;
		private UnityEngine.UI.Text raw_text;

		/** 初期化。
		*/
		public void Initialze()
		{
			//フォントサイズ。
			this.fontsize = Config.DEFAULT_TEXT_FONTSIZE;

			//センター。
			this.is_center = false;

			//クリップ。
			this.clip = false;
			this.clip_rect.Set(0,0,0,0);

			//再計算が必要。
			this.raw_is_recalc = true;

			//raw
			this.raw_gameobject = Render2D.GetInstance().RawInputField_Create();
			this.raw_transform = this.raw_gameobject.GetComponent<Transform>();
			this.raw_inputfield = this.raw_gameobject.GetComponent<UnityEngine.UI.InputField>();
			this.raw_recttransform = this.raw_gameobject.GetComponent<UnityEngine.RectTransform>();
			this.raw_text = this.raw_inputfield.textComponent;

			//font
			this.raw_text.font = Render2D.GetInstance().GetDefaultFont();

			//linetype
			this.raw_inputfield.lineType = UnityEngine.UI.InputField.LineType.MultiLineNewline;

			//localscale
			this.raw_recttransform.localScale = new Vector3(1.0f,1.0f,1.0f);

			//sizedelta
			this.raw_recttransform.sizeDelta = new Vector2(UnityEngine.Screen.width,UnityEngine.Screen.height);
		}

		/** クリップ。設定。
		*/
		public void SetClip(bool a_flag)
		{
			if(this.clip != a_flag){
				this.clip = a_flag;

				//■再計算が必要。
				this.raw_is_recalc = true;
			}
		}

		/** クリップ。取得。
		*/
		public bool IsClip()
		{
			return this.clip;
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(ref NRender2D.Rect2D_R<int> a_rect)
		{
			if((this.clip_rect.x != a_rect.x)||(this.clip_rect.y != a_rect.y)||(this.clip_rect.w != a_rect.w)||(this.clip_rect.h != a_rect.h)){
				this.clip_rect = a_rect;

				//■再計算が必要。
				this.raw_is_recalc = true;
			}
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(int a_x,int a_y,int a_w,int a_h)
		{
			if((this.clip_rect.x != a_x)||(this.clip_rect.y != a_y)||(this.clip_rect.w != a_w)||(this.clip_rect.h != a_h)){
				this.clip_rect.Set(a_x,a_y,a_w,a_h);

				//■再計算が必要。
				this.raw_is_recalc = true;
			}
		}

		/** クリップ矩形。取得。
		*/
		public int GetClipX()
		{
			return this.clip_rect.x;
		}

		/** クリップ矩形。取得。
		*/
		public int GetClipY()
		{
			return this.clip_rect.y;
		}

		/** クリップ矩形。取得。
		*/
		public int GetClipW()
		{
			return this.clip_rect.w;
		}

		/** クリップ矩形。取得。
		*/
		public int GetClipH()
		{
			return this.clip_rect.h;
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

		/** マルチライン。設定。
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

				//■再計算が必要。
				this.raw_is_recalc = true;
			}
		}

		/** テキスト。取得。
		*/
		public int GetFontSize()
		{
			return this.fontsize;
		}

		/** [内部からの呼び出し]フォントサイズ。設定。
		*/
		public void Raw_SetFontSize(int a_raw_fontsize)
		{
			this.raw_text.fontSize = a_raw_fontsize;
		}

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
			if(this.raw_text.font != a_font){
				this.raw_text.font = a_font;
			}
		}

		/** フォント。取得。
		*/
		public Font GetFont()
		{
			return this.raw_text.font;
		}

		/** 削除。
		*/
		public void Delete()
		{
			Render2D.GetInstance().RawInputField_Delete(this.raw_gameobject);
			this.raw_gameobject = null;
		}

		/** [内部からの呼び出し]サイズ。設定。
		*/
		public void Raw_SetRectTransformSizeDeleta(ref Vector2 a_size)
		{
			this.raw_recttransform.sizeDelta = a_size;
		}

		/** [内部からの呼び出し]位置。設定。
		*/
		public void Raw_SetRectTransformLocalPosition(ref Vector3 a_position)
		{
			this.raw_recttransform.localPosition = a_position;
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
				this.raw_recttransform.localScale = new Vector3(1.0f,1.0f,1.0f);
			}
		}

		/** [内部からの呼び出し]有効。設定。
		*/
		public void Raw_SetEnable(bool a_flag)
		{
			this.raw_inputfield.enabled = a_flag;
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

