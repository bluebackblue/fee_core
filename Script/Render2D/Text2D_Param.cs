

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。テキスト。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** Text2D_Param
	*/
	public struct Text2D_Param
	{
		/** フォントサイズ。
		*/
		private int fontsize;

		/** アライメントタイプ。
		*/
		private Text2D_HorizontalAlignmentType alignment_type_x;
		private Text2D_VerticalAlignmentType alignment_type_y;

		/** クリップ。
		*/
		private bool clip;
		private Fee.Geometry.Rect2D_R<int> clip_rect;

		/** シェーダの変更が必要。
		*/
		private bool raw_is_changeshader;

		/** フォントサイズの計算が必要。
		*/
		private bool raw_is_calcfontsize;

		/** サイズの計算が必要。
		*/
		private bool raw_is_calcsize;

		/** raw
		*/
		private UnityEngine.GameObject raw_gameobject;
		private UnityEngine.Transform raw_transform;
		private UnityEngine.UI.Text raw_text;
		private UnityEngine.RectTransform raw_recttransform;
		private Fee.Render2D.Material_Item raw_custom_text_material_item;
		private UnityEngine.UI.Outline raw_outline;
		private UnityEngine.UI.Shadow raw_shadow;

		/** 初期化。
		*/
		public void Initialize()
		{
			//raw
			this.raw_gameobject = Fee.Instantiate.Instantiate.CreateUiText("Text",Fee.Render2D.Render2D.GetInstance().GetRootTransform());
			this.raw_transform = this.raw_gameobject.GetComponent<UnityEngine.Transform>();
			this.raw_text = this.raw_gameobject.GetComponent<UnityEngine.UI.Text>();
			this.raw_recttransform = this.raw_gameobject.GetComponent<UnityEngine.RectTransform>();
			this.raw_gameobject.SetActive(false);

			//共通マテリアルアイテム複製。
			this.raw_custom_text_material_item = Render2D.GetInstance().GetUiTextMaterialItem().DuplicateMaterialItem();
		}

		/** プールから作成。
		*/
		public void PoolNew()
		{
			//フォントサイズ。
			this.fontsize = Config.DEFAULT_TEXT_FONTSIZE;

			//アライメントタイプ。左揃え。
			this.alignment_type_x = Text2D_HorizontalAlignmentType.Left;

			//アライメントタイプ。上揃え。
			this.alignment_type_y = Text2D_VerticalAlignmentType.Top;

			//クリップ。
			this.clip = false;
			this.clip_rect.Set(0,0,0,0);

			//シェーダの変更が必要。
			this.raw_is_changeshader = true;

			//フォントサイズの計算が必要。
			this.raw_is_calcfontsize = true;

			//サイズの計算が必要。
			this.raw_is_calcsize = true;

			//raw
			this.raw_gameobject.SetActive(false);

			//カスタムマテリアルアイテム設定。
			this.raw_custom_text_material_item.SetMaterialToInstance(this.raw_text);

			//font
			this.raw_text.font = Render2D.GetInstance().GetDefaultFont();

			//color
			this.raw_text.color = Config.DEFAULT_TEXT_COLOR;

			//text
			this.raw_text.text = "";

			//localscale
			this.raw_recttransform.localScale = new UnityEngine.Vector3(1.0f,1.0f,1.0f);

			//sizedelta
			this.raw_recttransform.sizeDelta = new UnityEngine.Vector2(Screen.GetScreenWidth(),Screen.GetScreenHeight());

			{
				UnityEngine.UI.BaseMeshEffect[] t_effect_list = this.raw_gameobject.GetComponents<UnityEngine.UI.BaseMeshEffect>();
				for(int ii=0;ii<t_effect_list.Length;ii++){
					if(t_effect_list[ii].GetType() == typeof(UnityEngine.UI.Shadow)){
						this.raw_shadow = t_effect_list[ii] as UnityEngine.UI.Shadow;
						if(this.raw_shadow != null){
							this.raw_shadow.enabled = false;
						}
					}else if(t_effect_list[ii].GetType() == typeof(UnityEngine.UI.Outline)){
						this.raw_outline = t_effect_list[ii] as  UnityEngine.UI.Outline;
						if(this.raw_outline != null){
							this.raw_outline.enabled = false;
						}
					}
				}
			}
		}

		/** プールへ削除。前。
		*/
		public void PrePoolDelete()
		{
			this.raw_gameobject.SetActive(false);
			this.raw_text.text = "";
		}

		/** プールへ削除。
		*/
		public void PoolDelete()
		{
		}

		/** メモリから削除。
		*/
		public void MemoryDelete()
		{
			UnityEngine.GameObject.DestroyImmediate(this.raw_gameobject);
			this.raw_gameobject = null;

			this.raw_custom_text_material_item.DestroyImmediate();
			this.raw_custom_text_material_item = null;
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

		/** カスタムテキストマテリアルアイテム。取得。
		*/
		public Material_Item GetCustomTextMaterialItem()
		{
			return this.raw_custom_text_material_item;
		}

		/** テキスト。設定。
		*/
		public void SetText(string a_text)
		{
			string t_text = a_text;

			if(t_text == null){
				t_text = "";
			}

			if(this.raw_text.text != a_text){
				this.raw_text.text = a_text;

				//■サイズの計算が必要。
				this.raw_is_calcsize = true;
			}
		}

		/** テキスト。取得。
		*/
		public string GetText()
		{
			return this.raw_text.text;
		}

		/** テキスト。設定。
		*/
		public void SetFontSize(int a_fontsize)
		{
			if(this.fontsize != a_fontsize){
				this.fontsize = a_fontsize;

				//■サイズの計算が必要。
				this.raw_is_calcsize = true;

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

		/** 色。設定。
		*/
		public void SetColor(in UnityEngine.Color a_color)
		{
			if(this.raw_text.color != a_color){
				this.raw_text.color = a_color;
			}
		}

		/** 色。設定。
		*/
		public void SetColor(float a_r,float a_g,float a_b,float a_a)
		{
			if((this.raw_text.color.r != a_r)||(this.raw_text.color.g != a_g)||(this.raw_text.color.b != a_b)||(this.raw_text.color.a != a_a)){
				this.raw_text.color = new UnityEngine.Color(a_r,a_g,a_b,a_a);
			}
		}

		/** 色。取得。
		*/
		public UnityEngine.Color GetColor()
		{
			return this.raw_text.color;
		}

		/** アウトライン色。設定。
		*/
		public void SetOutLineColor(in UnityEngine.Color a_color)
		{
			if(this.raw_outline.effectColor != a_color){
				this.raw_outline.effectColor = a_color;
			}
		}

		/** アウトライン色。設定。
		*/
		public void SetOutLineColor(float a_r,float a_g,float a_b,float a_a)
		{
			if((this.raw_outline.effectColor.r != a_r)||(this.raw_outline.effectColor.g != a_g)||(this.raw_outline.effectColor.b != a_b)||(this.raw_outline.effectColor.a != a_a)){
				this.raw_outline.effectColor = new UnityEngine.Color(a_r,a_g,a_b,a_a);
			}
		}

		/** アウトライン色。取得。
		*/
		public UnityEngine.Color GetOutLineColor()
		{
			return this.raw_outline.effectColor;
		}

		/** アライメントタイプ。設定。
		*/
		public void SetAlignmentType(Text2D_HorizontalAlignmentType a_alignment_type_x,Text2D_VerticalAlignmentType a_alignment_type_y)
		{
			if(this.alignment_type_x != a_alignment_type_x){
				this.alignment_type_x = a_alignment_type_x;
			}

			if(this.alignment_type_y != a_alignment_type_y){
				this.alignment_type_y = a_alignment_type_y;
			}
		}

		/** アウトライン。設定。
		*/
		public void SetOutLine(bool a_flag)
		{
			if(this.raw_outline != null){
				this.raw_outline.enabled = a_flag;
			}
		}

		/** アウトライン。取得。
		*/
		public bool GetOutLine()
		{
			if(this.raw_outline != null){
				return this.raw_outline.enabled;
			}
			return false;
		}

		/** シャドー。設定。
		*/
		public void SetShadow(bool a_flag)
		{
			if(this.raw_shadow != null){
				this.raw_shadow.enabled = a_flag;
			}
		}

		/** シャドー。取得。
		*/
		public bool GetShadow()
		{
			if(this.raw_shadow != null){
				return this.raw_shadow.enabled;
			}
			return false;
		}

		/** アライメントタイプ。取得。
		*/
		public Text2D_HorizontalAlignmentType GetAlignmentTypeX()
		{
			return this.alignment_type_x;
		}

		/** アライメントタイプ。取得。
		*/
		public Text2D_VerticalAlignmentType GetAlignmentTypeY()
		{
			return this.alignment_type_y;
		}

		/** フォント。設定。
		*/
		public void SetFont(UnityEngine.Font a_font)
		{
			if(this.raw_text.font != a_font){
				this.raw_text.font = a_font;

				//■サイズの計算が必要。
				this.raw_is_calcsize = true;

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

		/** [内部からの呼び出し]最適横幅。取得。
		*/
		public float Raw_GetPreferredWidth()
		{
			return this.raw_text.preferredWidth;
		}

		/** [内部からの呼び出し]最適縦幅。取得。
		*/
		public float Raw_GetPreferredHeight()
		{
			return this.raw_text.preferredHeight;
		}

		/** [内部からの呼び出し]サイズ。設定。
		*/
		public void Raw_SetRectTransformSizeDelta(in UnityEngine.Vector2 a_size)
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
		public void Raw_SetRectTransformLocalPosition(in UnityEngine.Vector3 a_position)
		{
			this.raw_recttransform.localPosition = a_position;
		}

		/** [内部からの呼び出し]フォントサイズ。設定。
		*/
		public void Raw_SetFontSize(int a_raw_fontsize)
		{
			this.raw_text.fontSize = a_raw_fontsize;
		}

		/** [内部からの呼び出し]テキストマテリアルアイテム。設定。
		*/
		public void Raw_SetTextMaterialItem(Material_Item a_material_item)
		{
			a_material_item.SetMaterialToInstance(this.raw_text);
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

		/** [内部からの呼び出し]サイズの計算が必要。取得。
		*/
		public bool Raw_IsCalcSize()
		{
			return this.raw_is_calcsize;
		}

		/** [内部からの呼び出し]サイズの計算が必要。設定。
		*/
		public void Raw_SetCalcSizeFlag(bool a_flag)
		{
			this.raw_is_calcsize = a_flag;
		}
	}
}

