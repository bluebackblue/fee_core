

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
		private Fee.Focus.Focus_MonoBehaviour raw_focus_monobehaviour;
		private UnityEngine.Transform raw_transform;
		private UnityEngine.UI.InputField raw_inputfield;
		private UnityEngine.RectTransform raw_recttransform;
		private UnityEngine.UI.Text raw_text;
		private UnityEngine.UI.Image raw_image;
		private UnityEngine.UI.Text raw_placeholder_text;
		private Material_Item raw_custom_text_material_item;
		private Material_Item raw_custom_image_material_item;

		/** 初期化。
		*/
		public void Initialize(InputField2D a_parent)
		{
			//raw
			this.raw_gameobject = Fee.Instantiate.Instantiate.CreateUiInputField("InputField",Fee.Render2D.Render2D.GetInstance().GetRootTransform());
			this.raw_focus_monobehaviour = this.raw_gameobject.AddComponent<Fee.Focus.Focus_MonoBehaviour>();
			this.raw_transform = this.raw_gameobject.GetComponent<UnityEngine.Transform>();
			this.raw_inputfield = this.raw_gameobject.GetComponent<UnityEngine.UI.InputField>();
			this.raw_recttransform = this.raw_gameobject.GetComponent<UnityEngine.RectTransform>();
			this.raw_text = this.raw_inputfield.textComponent;
			this.raw_image = this.raw_inputfield.image;
			this.raw_placeholder_text = this.raw_inputfield.placeholder.GetComponent<UnityEngine.UI.Text>();
			this.raw_gameobject.SetActive(false);

			{
				UnityEngine.UI.Navigation t_navigation = this.raw_inputfield.navigation;
				t_navigation.mode = UnityEngine.UI.Navigation.Mode.None;
				this.raw_inputfield.navigation = t_navigation;
			}

			//共通マテリアルアイテム複製。
			this.raw_custom_text_material_item = Render2D.GetInstance().GetUiTextMaterialItem().DuplicateMaterialItem();
			this.raw_custom_image_material_item = Render2D.GetInstance().GetUiImageMaterialItem().DuplicateMaterialItem();
		}

		/** プールから作成。
		*/
		public void InitializeFromPool()
		{
			//フォントサイズ。
			this.fontsize = Config.DEFAULT_TEXT_FONTSIZE;

			//クリップ。
			this.clip = false;
			this.clip_rect.Set(0,0,0,0);

			//シェーダの変更が必要。
			this.raw_is_changeshader = true;

			//フォントサイズの計算が必要。
			this.raw_is_calcfontsize = true;

			//raw
			this.raw_gameobject.SetActive(false);

			//カスタムマテリアルアイテム設定。
			this.raw_custom_text_material_item.SetMaterialToInstance(this.raw_text);
			this.raw_custom_image_material_item.SetMaterialToInstance(this.raw_image);
			this.raw_custom_text_material_item.SetMaterialToInstance(this.raw_placeholder_text);

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

		/** メモリから削除。
		*/
		public void DeleteFromMemory()
		{
			UnityEngine.GameObject.DestroyImmediate(this.raw_gameobject);
			this.raw_gameobject = null;

			this.raw_custom_text_material_item.DestroyImmediate();
			this.raw_custom_text_material_item = null;

			this.raw_custom_image_material_item.DestroyImmediate();
			this.raw_custom_image_material_item = null;
		}

		/** コールバックインターフェイス。設定。

			フォーカス変更時にFee.Focus.Mainから呼び出すコールバック。

		*/
		public void SetOnFocusCheck<T>(Fee.Focus.OnFocusCheck_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.raw_focus_monobehaviour.SetOnFocusCheck(a_callback_interface,a_id);
		}

		/** [Fee.Focus.FocusItem_Base]フォーカス。設定。
		*/
		public void SetFocus(bool a_flag)
		{
			if(a_flag == true){
				this.raw_inputfield.ActivateInputField();
				if(UnityEngine.EventSystems.EventSystem.current != null){
					UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(this.raw_gameobject);
				}
			}else{
				if(UnityEngine.EventSystems.EventSystem.current != null){
					if(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == this.raw_gameobject){
						UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
					}
				}
			}
		}

		/** [Fee.Focus.FocusItem_Base]フォーカス。チェック。
		*/
		public bool IsFocus()
		{
			if(UnityEngine.EventSystems.EventSystem.current != null){
				if(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == this.raw_gameobject){
					return true;
				}
			}
			return false;
		}

		/** [Fee.Focus.FocusItem_Base]フォーカス。設定。OnFocusCheckを呼び出す。
		*/
		public void SetFocusCallOnFocusCheck(bool a_flag)
		{
			this.SetFocus(a_flag);
			this.raw_focus_monobehaviour.CallOnFocusCheck();
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

		/** カスタムイメージマテリアルアイテム。取得。
		*/
		public Material_Item GetCustomImageMaterialItem()
		{
			return this.raw_custom_image_material_item;
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

		/** [内部からの呼び出し]テキストマテリアルアイテム。設定。
		*/
		public void Raw_SetTextMaterialItem(Material_Item a_material_item)
		{
			a_material_item.SetMaterialToInstance(this.raw_text);
			a_material_item.SetMaterialToInstance(this.raw_placeholder_text);
		}

		/** [内部からの呼び出し]イメージマテリアルアイテム。設定。
		*/
		public void Raw_SetImageMaterialItem(Material_Item a_material_item)
		{
			a_material_item.SetMaterialToInstance(this.raw_image);
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

		/** [内部からの呼び出し]テキスト。設定。
		*/
		public void Raw_SetText(string a_text)
		{
			this.raw_inputfield.text = a_text;
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

