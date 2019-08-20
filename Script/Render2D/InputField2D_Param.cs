

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

		/** シェーダの変更が必要。
		*/
		private bool raw_is_changeshader;

		/** フォントサイズの計算が必要。
		*/
		private bool raw_is_calcfontsize;

		/** クリップ。
		*/
		private bool clip;
		private Fee.Geometry.Rect2D_R<int> clip_rect;

		/** raw
		*/
		private UnityEngine.GameObject raw_gameobject;
		private UnityEngine.Transform raw_transform;
		private UnityEngine.UI.InputField raw_inputfield;
		private UnityEngine.RectTransform raw_recttransform;
		private UnityEngine.UI.Text raw_text;
		private UnityEngine.UI.Image raw_image;
		private UnityEngine.UI.Text raw_placeholder_text;

		private UnityEngine.Material raw_custom_textmaterial;
		private UnityEngine.Material raw_custom_imagematerial;

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

			//シェーダの変更が必要。
			this.raw_is_changeshader = true;

			//フォントサイズの計算が必要。
			this.raw_is_calcfontsize = true;

			//raw
			this.raw_gameobject = Render2D.GetInstance().RawInputField_Create();
			this.raw_transform = this.raw_gameobject.GetComponent<UnityEngine.Transform>();
			this.raw_inputfield = this.raw_gameobject.GetComponent<UnityEngine.UI.InputField>();
			this.raw_recttransform = this.raw_gameobject.GetComponent<UnityEngine.RectTransform>();
			this.raw_text = this.raw_inputfield.textComponent;
			this.raw_image = this.raw_inputfield.image;
			this.raw_placeholder_text = this.raw_inputfield.placeholder.GetComponent<UnityEngine.UI.Text>();

			//共通マテリアルから複製。
			this.raw_custom_textmaterial = new UnityEngine.Material(Render2D.GetInstance().GetUiTextMaterial());
			this.raw_custom_imagematerial = new UnityEngine.Material(Render2D.GetInstance().GetUiImageMaterial());

			//material
			this.raw_text.material = this.raw_custom_textmaterial;
			this.raw_image.material = this.raw_custom_imagematerial;
			this.raw_placeholder_text.material = this.raw_custom_textmaterial;

			//font
			this.raw_text.font = Render2D.GetInstance().GetDefaultFont();
			this.raw_placeholder_text.font = Render2D.GetInstance().GetDefaultFont();

			//linetype
			this.raw_inputfield.lineType = UnityEngine.UI.InputField.LineType.MultiLineNewline;

			//inputtype
			this.raw_inputfield.inputType = UnityEngine.UI.InputField.InputType.Standard;

			//localscale
			this.raw_recttransform.localScale = new UnityEngine.Vector3(1.0f,1.0f,1.0f);

			//sizedelta
			this.raw_recttransform.sizeDelta = new UnityEngine.Vector2(Screen.GetScreenWidth(),Screen.GetScreenHeight());
		}

		/** クリップ。設定。
		*/
		public void SetClip(bool a_flag)
		{
			if(this.clip != a_flag){
				this.clip = a_flag;

				//■シェーダの変更が必要。
				this.raw_is_changeshader = true;
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
		public void SetClipRect(in Fee.Geometry.Rect2D_R<int> a_rect)
		{
			if((this.clip_rect.x != a_rect.x)||(this.clip_rect.y != a_rect.y)||(this.clip_rect.w != a_rect.w)||(this.clip_rect.h != a_rect.h)){
				this.clip_rect = a_rect;

				//■シェーダの変更が必要。
				this.raw_is_changeshader = true;
			}
		}

		/** クリップ矩形。設定。
		*/
		public void SetClipRect(int a_x,int a_y,int a_w,int a_h)
		{
			if((this.clip_rect.x != a_x)||(this.clip_rect.y != a_y)||(this.clip_rect.w != a_w)||(this.clip_rect.h != a_h)){
				this.clip_rect.Set(a_x,a_y,a_w,a_h);

				//■シェーダの変更が必要。
				this.raw_is_changeshader = true;
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
		public UnityEngine.Material GetCustomTextMaterial()
		{
			return this.raw_custom_textmaterial;
		}

		/** カスタムイメージマテリアル。取得。
		*/
		public UnityEngine.Material GetCustomImageMaterial()
		{
			return this.raw_custom_imagematerial;
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

		/** パスワードタイプ。設定。
		*/
		public void SetPasswordType(bool a_flag)
		{
			if(a_flag == true){
				this.raw_inputfield.inputType = UnityEngine.UI.InputField.InputType.Password;
			}else{
				this.raw_inputfield.inputType = UnityEngine.UI.InputField.InputType.Standard;
			}
		}

		/** テキスト。設定。
		*/
		public void SetFontSize(int a_fontsize)
		{
			if(this.fontsize != a_fontsize){
				this.fontsize = a_fontsize;

				//■フォントサイズの計算が必要。
				this.raw_is_calcfontsize = true;
			}
		}

		/** テキスト。取得。
		*/
		public int GetFontSize()
		{
			return this.fontsize;
		}

		/** イメージ色。設定。
		*/
		public void SetImageColor(in UnityEngine.Color a_color)
		{
			if(this.raw_image.color != a_color){
				this.raw_image.color = a_color;
			}
		}

		/** イメージ色。設定。
		*/
		public void SetImageColor(float a_r,float a_g,float a_b,float a_a)
		{
			if((this.raw_image.color.r != a_r)||(this.raw_image.color.g != a_g)||(this.raw_image.color.b != a_b)||(this.raw_image.color.a != a_a)){
				this.raw_image.color = new UnityEngine.Color(a_r,a_g,a_b,a_a);
			}
		}

		/** イメージ色。取得。
		*/
		public UnityEngine.Color GetImageColor()
		{
			return this.raw_image.color;
		}

		/** テキスト色。設定。
		*/
		public void SetTextColor(in UnityEngine.Color a_color)
		{
			if(this.raw_text.color != a_color){
				this.raw_text.color = a_color;
			}
		}

		/** テキスト色。設定。
		*/
		public void SetTextColor(float a_r,float a_g,float a_b,float a_a)
		{
			if((this.raw_text.color.r != a_r)||(this.raw_text.color.g != a_g)||(this.raw_text.color.b != a_b)||(this.raw_text.color.a != a_a)){
				this.raw_text.color = new UnityEngine.Color(a_r,a_g,a_b,a_a);
			}
		}

		/** イテキスト色。取得。
		*/
		public UnityEngine.Color GetTextColor()
		{
			return this.raw_text.color;
		}

		/** [内部からの呼び出し]フォントサイズ。設定。
		*/
		public void Raw_SetFontSize(int a_raw_fontsize)
		{
			this.raw_text.fontSize = a_raw_fontsize;
		}

		/** [内部からの呼び出し]テキストマテリアル。設定。
		*/
		public void Raw_SetTextMaterial(UnityEngine.Material a_material)
		{
			this.raw_text.material = a_material;
			this.raw_placeholder_text.material = a_material;
		}

		/** [内部からの呼び出し]イメージマテリアル。設定。
		*/
		public void Raw_SetImageMaterial(UnityEngine.Material a_material)
		{
			this.raw_image.material = a_material;
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
		public void SetFont(UnityEngine.Font a_font)
		{
			if(this.raw_text.font != a_font){
				this.raw_text.font = a_font;
				this.raw_placeholder_text.font = a_font;

				//■フォントサイズの計算が必要。
				this.raw_is_calcfontsize = true;
			}
		}

		/** フォント。取得。
		*/
		public UnityEngine.Font GetFont()
		{
			return this.raw_text.font;
		}

		/** 削除。
		*/
		public void Delete()
		{
			Render2D.GetInstance().RawInputField_Delete(this.raw_gameobject);
			this.raw_gameobject = null;

			UnityEngine.GameObject.DestroyImmediate(this.raw_custom_textmaterial);
			this.raw_custom_textmaterial = null;

			UnityEngine.GameObject.DestroyImmediate(this.raw_custom_imagematerial);
			this.raw_custom_imagematerial = null;
		}

		/** [内部からの呼び出し]サイズ。設定。
		*/
		public void Raw_SetRectTransformSizeDelta(ref UnityEngine.Vector2 a_size)
		{
			this.raw_recttransform.sizeDelta = a_size;
		}

		/** [内部からの呼び出し]サイズ。取得。
		*/
		public void Raw_GetRectTransformSizeDelta(out UnityEngine.Vector2 a_size)
		{
			a_size = this.raw_recttransform.sizeDelta;
		}

		/** [内部からの呼び出し]位置。設定。
		*/
		public void Raw_SetRectTransformLocalPosition(ref UnityEngine.Vector3 a_position)
		{
			this.raw_recttransform.localPosition = a_position;
		}

		/** [内部からの呼び出し]レイヤー。設定。
		*/
		public void Raw_SetLayer(UnityEngine.Transform a_layer_transform)
		{
			if(a_layer_transform == null){
				//this.raw_gameobject.SetActive(false);
			}else{
				this.raw_transform.SetParent(a_layer_transform);
				//this.raw_gameobject.SetActive(true);
				this.raw_recttransform.localScale = new UnityEngine.Vector3(1.0f,1.0f,1.0f);
			}
		}

		/** [内部からの呼び出し]有効。設定。
		*/
		public void Raw_SetEnable(bool a_flag)
		{
			this.raw_gameobject.SetActive(a_flag);
		}

		/** [内部からの呼び出し]シェーダの変更が必要。取得。
		*/
		public bool Raw_IsChangeShader()
		{
			return this.raw_is_changeshader;
		}

		/** [内部からの呼び出し]シェーダの変更が必要。取得。
		*/
		public void Raw_SetChangeShaderFlag(bool a_flag)
		{
			this.raw_is_changeshader = a_flag;
		}

		/** [内部からの呼び出し]フォントサイズの計算が必要。取得。
		*/
		public bool Raw_IsCalcFontSize()
		{
			return this.raw_is_calcfontsize;
		}

		/** [内部からの呼び出し]フォントサイズの計算が必要。取得。
		*/
		public void Raw_SetCalcFontSizeFlag(bool a_flag)
		{
			this.raw_is_calcfontsize = a_flag;
		}
	}
}

