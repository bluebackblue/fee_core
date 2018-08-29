using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ２Ｄ描画。テキスト。
*/


/** Render2D
*/
namespace NRender2D
{
	/** Text2D_Param
	*/
	public struct Text2D_Param
	{
		/** テキスト。
		*/
		private string text;

		/** フォントサイズ。
		*/
		private int fontsize;

		/** 色。
		*/
		private Color color;

		/** センター。
		*/
		private bool is_center;

		/** フォント。
		*/
		private Font font;

		/** クリップ。
		*/
		private bool clip;
		private NRender2D.Rect2D_R<int> clip_rect;

		/** raw
		*/
		private GameObject raw_gameobject;
		private Transform raw_transform;
		private UnityEngine.UI.Text raw_text;
		private UnityEngine.RectTransform raw_recttransform;
		private bool raw_is_recalc;
		private Material raw_custom_textmaterial;

		/** 初期化。
		*/
		public void Initialze()
		{
			//テキスト。
			this.text = "";

			//フォントサイズ。
			this.fontsize = Config.DEFAULT_TEXT_FONTSIZE;

			//色。
			this.color = Config.DEFAULT_TEXT_COLOR;

			//センター。
			this.is_center = false;

			//フォント。
			this.font = Render2D.GetInstance().GetDefaultFont();

			//クリップ。
			this.clip = false;
			this.clip_rect.Set(0,0,0,0);

			//raw
			this.raw_gameobject = Render2D.GetInstance().RawText_Create();
			this.raw_transform = this.raw_gameobject.GetComponent<Transform>();
			this.raw_text = this.raw_gameobject.GetComponent<UnityEngine.UI.Text>();
			this.raw_recttransform = this.raw_gameobject.GetComponent<UnityEngine.RectTransform>();
			this.raw_is_recalc = true;
			this.raw_custom_textmaterial = new Material(Render2D.GetInstance().GetTextMaterial());
			this.raw_text.material = this.raw_custom_textmaterial;
		}

		/** クリップ。設定。
		*/
		public void SetClip(bool a_flag)
		{
			if(this.clip != a_flag){
				this.clip = a_flag;
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
				this.raw_is_recalc = true;
			}
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(int a_x,int a_y,int a_w,int a_h)
		{
			if((this.clip_rect.x != a_x)||(this.clip_rect.y != a_y)||(this.clip_rect.w != a_w)||(this.clip_rect.h != a_h)){
				this.clip_rect.Set(a_x,a_y,a_w,a_h);
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

		/** カスタムテキストマテリアル。取得。
		*/
		public Material GetCustomTextMaterial()
		{
			return this.raw_custom_textmaterial;
		}

		/** テキスト。設定。
		*/
		public void SetText(string a_text)
		{
			string t_text = a_text;

			if(t_text == null){
				t_text = "";
			}

			if(this.text != a_text){
				this.text = a_text;
				this.raw_is_recalc = true;
			}
		}

		/** テキスト。取得。
		*/
		public string GetText()
		{
			return this.text;
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
		public void SetColor(ref Color a_color)
		{
			if(this.color != a_color){
				this.color = a_color;
				this.raw_is_recalc = true;
			}
		}

		/** 色。設定。
		*/
		public void SetColor(float a_r,float a_g,float a_b,float a_a)
		{
			if((this.color.r != a_r)||(this.color.g != a_g)||(this.color.b != a_b)||(this.color.a != a_a)){
				this.color.r = a_r;
				this.color.g = a_g;
				this.color.b = a_b;
				this.color.a = a_a;
				this.raw_is_recalc = true;
			}
		}

		/** 色。取得。
		*/
		public Color GetColor()
		{
			return this.color;
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
			Render2D.GetInstance().RawText_Delete(this.raw_gameobject);
			this.raw_gameobject = null;

			GameObject.DestroyImmediate(this.raw_custom_textmaterial);
			this.raw_custom_textmaterial = null;
		}

		/** レイヤー。設定。
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

		/** テキスト。取得。
		*/
		public UnityEngine.UI.Text Raw_GetText()
		{
			return this.raw_text;
		}

		/** RectTransform。取得。
		*/
		public UnityEngine.RectTransform Raw_GetRectTransform()
		{
			return this.raw_recttransform;
		}

		/** 再計算フラグ。取得。
		*/
		public bool Raw_IsReCalc()
		{
			return this.raw_is_recalc;
		}

		/** 再計算フラグ。設定。
		*/
		public void Raw_ResetReCalc()
		{
			this.raw_is_recalc = false;
		}
	}
}

