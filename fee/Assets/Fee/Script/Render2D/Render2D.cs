using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ２Ｄ描画。
*/


/** Render2D
*/
namespace NRender2D
{
	/** Render2D
	*/
	public class Render2D : Config
	{
		/** [シングルトン]s_instance
		*/
		private static Render2D s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Render2D();
			}
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static Render2D GetInstance()
		{
			return s_instance;			
		}

		/** [シングルトン]インスタンス。削除。
		*/
		public static void DeleteInstance()
		{
			if(s_instance != null){
				s_instance.Delete();
				s_instance = null;
			}
		}

		/** ルート。
		*/
		public GameObject root_gameobject;

		/** スクリーン。
		*/
		public Screen screen;

		/** マテリアルリスト。
		*/
		public MaterialList materiallist;

		/** スプライト。
		*/
		private List<Sprite2D> sprite_list;

		/** テキスト。
		*/
		private List<Text2D> text_list;

		/** 入力フィールド。
		*/
		private List<InputField2D> inputfield_list;

		/** 更新リクエスト。
		*/
		private bool update_request_sprite;
		private bool update_request_text;
		private bool update_request_inputfield;

		/** [RawText]プレハブ。
		*/
		private GameObject prefab_rawtext;

		/** [RawInputField]プレハブ。
		*/
		private GameObject prefab_rawinputfield;

		/** デフォルト。フォント。
		*/
		private Font default_font;

		/** レイヤーリスト。
		*/
		private LayerList layerlist;

		/** [シングルトン]constructor。
		*/
		private Render2D()
		{
			//ルート。
			this.root_gameobject = new GameObject();
			this.root_gameobject.name = "Render2D";
			GameObject.DontDestroyOnLoad(this.root_gameobject);

			//スクリーン。
			this.screen = new Screen();

			//マテリアルリスト。
			this.materiallist = new MaterialList();

			//スプライト。
			this.sprite_list = new List<Sprite2D>();

			//テキスト。
			this.text_list = new List<Text2D>();

			//入力フィールド。
			this.inputfield_list = new List<InputField2D>();

			//更新リクエスト。
			this.update_request_sprite = true;
			this.update_request_text = true;
			this.update_request_inputfield = true;

			//[RawText]プレハブ読み込み。
			this.prefab_rawtext = Resources.Load<GameObject>(Config.PREFAB_NAME_TEXT);

			//[RawInputField]プラハブ読み込み。
			this.prefab_rawinputfield = Resources.Load<GameObject>(Config.PREFAB_NAME_INPUTFIELD);

			//デフォルト。フォント。
			this.default_font = Resources.GetBuiltinResource<Font>(Config.DEFAULT_FONT_NAME);

			//レイヤーリスト。
			this.layerlist = new LayerList(this.root_gameobject.GetComponent<Transform>());
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			GameObject.Destroy(this.root_gameobject);
		}

		/** ＧＵＩスクリーン座標　＝＞　仮想スクリーン座標。
		*/
		public void GuiScreenToVirtualScreen(int a_gui_x,int a_gui_y,out int a_virtual_x,out int a_virtual_y)
		{
			this.screen.GuiScreenToVirtualScreen(a_gui_x,a_gui_y,out a_virtual_x,out a_virtual_y);
		}

		/** 仮想スクリーン座標　＝＞　ＧＵＩスクリーン座標。
		*/
		public void VirtualScreenToGuiScreen(int a_virtual_x,int a_virtual_y,out int a_gui_x,out int a_gui_y)
		{
			this.screen.VirtualScreenToGuiScreen(a_virtual_x,a_virtual_y,out a_gui_x,out a_gui_y);
		}

		/** ＧＵＩスクリーン。取得。
		*/
		public int GetGuiW()
		{
			return this.screen.GetGuiW();
		}

		/** ＧＵＩスクリーン。取得。
		*/
		public int GetGuiH()
		{
			return this.screen.GetGuiH();
		}

		/** GetLastLayerIndex
		*/
		public int GetLastLayerIndex()
		{
			return (Config.MAX_LAYER - 1);
		}
		/** デフォルト。フォント設定。
		*/
		public void SetDefaultFont(Font a_font)
		{
			this.default_font = a_font;
		}

		/** デフォルト。フォント取得。
		*/
		public Font GetDefaultFont()
		{
			return this.default_font;
		}

		/** テキストマテリアル。取得。
		*/
		public Material GetTextMaterial()
		{
			return this.materiallist.GetTextMaterial();
		}

		/** [RawText]作成。
		*/
		public GameObject RawText_Create()
		{
			GameObject t_gameobject = GameObject.Instantiate(this.prefab_rawtext);
			t_gameobject.name = "Text";

			return t_gameobject;
		}

		/** [RawInputField]作成。
		*/
		public GameObject RawInputField_Create()
		{
			GameObject t_gameobject = GameObject.Instantiate(this.prefab_rawinputfield);
			t_gameobject.name = "InputField";

			return t_gameobject;
		}

		/** [RawText]削除。
		*/
		public void RawText_Delete(GameObject a_gameobject)
		{
			GameObject.Destroy(a_gameobject);
		}

		/** [RawInputField]削除。
		*/
		public void RawInputField_Delete(GameObject a_gameobject)
		{
			GameObject.Destroy(a_gameobject);
		}

		/** スプライト作成。
		*/
		public void AddSprite2D(Sprite2D a_sprite)
		{
			this.sprite_list.Add(a_sprite);
			this.update_request_sprite = true;
		}

		/** テキスト作成。
		*/
		public void AddText2D(Text2D a_text)
		{
			this.text_list.Add(a_text);
			this.update_request_text = true;
		}

		/** 入力フィールド作成。
		*/
		public void AddInputField2D(InputField2D a_inputfield)
		{
			this.inputfield_list.Add(a_inputfield);
			this.update_request_inputfield = true;
		}

		/** 更新リクエスト。
		*/
		public void UpdateSpriteListRequest()
		{
			this.update_request_sprite = true;
		}

		/** 更新リクエスト。
		*/
		public void UpdateTextListRequest()
		{
			this.update_request_text = true;
		}

		/** 更新リクエスト。
		*/
		public void UpdateInputFieldListRequest()
		{
			this.update_request_inputfield = true;
		}

		/** デプスクリアーの設定。
		*/
		public void SetDepthClearGL(int a_layerindex,bool a_flag)
		{
			this.layerlist.SetDepthClearGL(a_layerindex,a_flag);
		}

		/** デプスクリアーの設定。
		*/
		public void SetDepthClearUI(int a_layerindex,bool a_flag)
		{
			this.layerlist.SetDepthClearUI(a_layerindex,a_flag);
		}

		/** 事前計算。取得。
		*/
		public float GetScreenCalcSpriteX()
		{
			return this.screen.GetScreenCalcSpriteX();
		}

		/** 事前計算。取得。
		*/
		public float GetScreenCalcSpriteY()
		{
			return this.screen.GetScreenCalcSpriteY();
		}

		/** 事前計算。取得。
		*/
		public float GetScreenCalcSpriteW()
		{
			return this.screen.GetScreenCalcSpriteW();
		}

		/** 事前計算。取得。
		*/
		public float GetScreenCalcSpriteH()
		{
			return this.screen.GetScreenCalcSpriteH();
		}

		/** コールバック。設定。
		*/
		public float GetGLCameraDepth(int a_layerindex)
		{
			return this.layerlist.GetGLCameraDepth(a_layerindex);
		}

		/** コールバック。設定。
		*/
		public float GetUICameraDepth(int a_layerindex)
		{
			return this.layerlist.GetUICameraDepth(a_layerindex);
		}

		/** 描画前処理。
		*/
		public void PreDraw()
		{
			//事前計算。
			this.screen.CalcScreen(this.sprite_list);

			//スプライト。
			if(this.update_request_sprite == true){

				//削除。
				{
					int ii = 0;
					while(ii < this.sprite_list.Count){
						if(this.sprite_list[ii].IsDelete() == true){
							this.sprite_list.RemoveAt(ii);
						}else{
							ii++;
						}
					}
				}

				//ソート。
				this.sprite_list.Sort(Sprite2D.Sort_DrawPriority);
			}

			//テキスト。
			if(this.update_request_text == true){

				//削除。
				{
					int ii = 0;
					while(ii < this.text_list.Count){
						if(this.text_list[ii].IsDelete() == true){
							this.text_list.RemoveAt(ii);
						}else{
							ii++;
						}
					}
				}

				//ソート。
				this.text_list.Sort(Text2D.Sort_DrawPriority);
			}

			//入力フィールド。
			if(this.update_request_inputfield == true){

				//削除。
				{
					int ii = 0;
					while(ii < this.inputfield_list.Count){
						if(this.inputfield_list[ii].IsDelete() == true){
							this.inputfield_list.RemoveAt(ii);
						}else{
							ii++;
						}
					}
				}

				//ソート。
				this.inputfield_list.Sort(InputField2D.Sort_DrawPriority);
			}

			//インデックス計算。
			if((this.update_request_sprite == true)||(this.update_request_text == true)||(this.update_request_inputfield == true)){
				this.layerlist.CalcIndex(this.sprite_list,this.text_list,this.inputfield_list);
			}

			//テキスト。描画プライオリティ再設定。
			if(this.update_request_text == true){
				for(int ii=0;ii<this.text_list.Count;ii++){
					this.text_list[ii].Raw_SetLayer(this.layerlist.GetLayerTransformFromDrawPriority(this.text_list[ii].GetDrawPriority()));
				}
			}

			//入力フィールド。描画プライオリティ再設定。
			if(this.update_request_inputfield == true){
				for(int ii=0;ii<this.inputfield_list.Count;ii++){
					this.inputfield_list[ii].Raw_SetLayer(this.layerlist.GetLayerTransformFromDrawPriority(this.inputfield_list[ii].GetDrawPriority()));
				}
			}

			//ＵＩテキスト描画
			for(int ii=0;ii<this.layerlist.GetListMax();ii++){
				this.PreDraw_UI(ii);
			}

			//リセット。
			this.update_request_sprite = false;
			this.update_request_text = false;
			this.update_request_inputfield = false;

			//テキスト再計算フラグのリセット。
			this.screen.ResetUiReCalcFlag();
		}

		/** 描画前処理。ＵＩ。
		*/
		public void PreDraw_UI(int a_layerindex)
		{
			//テキスト。
			{
				int t_start_index = this.layerlist.GetStartIndex_Text(a_layerindex);
				int t_last_index = this.layerlist.GetLastIndex_Text(a_layerindex);

				if((t_start_index >= 0)&&(t_last_index >= 0)){
			
					for(int ii=t_start_index;ii<=t_last_index;ii++){
						Text2D t_text = this.text_list[ii];

						UnityEngine.UI.Text t_raw_text = t_text.Raw_GetText();

						//テキスト再描画。
						if((t_text.Raw_IsReCalc() == true)||(this.screen.IsUiReCalcFlag() == true)){
							t_text.Raw_ResetReCalc();

							//CalcTextPosition前に必要な処理。

							//フォントの設定。
							t_raw_text.font = t_text.GetFont();

							//フォントサイズの設定。
							t_raw_text.fontSize = this.screen.CalcFontSize(t_text);

							//スケール設定。
							t_text.Raw_GetRectTransform().localScale = new Vector3(1.0f,1.0f,1.0f);

							//色の設定。
							t_raw_text.color = t_text.GetColor();

							//文字列の設定。
							t_raw_text.text = t_text.GetText();
							t_text.Raw_GetRectTransform().sizeDelta = new Vector2(UnityEngine.Screen.width,UnityEngine.Screen.height);

							if(t_text.IsClip() == false){
								//共通マテリアル使用。w
								t_raw_text.material = this.materiallist.GetTextMaterial();
							}else{
								//カスタムマテリアル使用。
								Material t_material = t_text.GetCustomTextMaterial();

								int t_gui_x1;
								int t_gui_y1;
								int t_gui_x2;
								int t_gui_y2;
								this.VirtualScreenToGuiScreen(t_text.GetClipX(),t_text.GetClipY() + t_text.GetClipH(),out t_gui_x1,out t_gui_y1);
								this.VirtualScreenToGuiScreen(t_text.GetClipX() + t_text.GetClipW(),t_text.GetClipY(),out t_gui_x2,out t_gui_y2);
								t_material.SetFloat("clip_flag",1.0f);
								t_material.SetFloat("clip_x1",t_gui_x1);
								t_material.SetFloat("clip_y1",this.screen.GetGuiH() - t_gui_y1);
								t_material.SetFloat("clip_x2",t_gui_x2);
								t_material.SetFloat("clip_y2",this.screen.GetGuiH() - t_gui_y2);

								t_raw_text.material = t_material;
							}
						}

						if((t_text.GetText().Length > 0)&&(t_text.IsVisible() == true)&&(t_text.GetDrawPriority() >= 0)){

							//矩形計算。
							this.screen.CalcTextRect(t_text);

							//表示。
							t_raw_text.enabled = true;
						}else{
							//非表示。
							t_raw_text.enabled = false;
						}
					}
				}
			}

			//入力フィールド。
			{
				int t_start_index = this.layerlist.GetStartIndex_InputField(a_layerindex);
				int t_last_index = this.layerlist.GetLastIndex_InputField(a_layerindex);

				if((t_start_index >= 0)&&(t_last_index >= 0)){
			
					for(int ii=t_start_index;ii<=t_last_index;ii++){
						InputField2D t_inputfield = this.inputfield_list[ii];

						UnityEngine.UI.InputField t_raw_inputfield = t_inputfield.Raw_GetInputField();

						//テキスト再描画。
						if((t_inputfield.Raw_IsReCalc() == true)||(this.screen.IsUiReCalcFlag() == true)){
							t_inputfield.Raw_ResetReCalc();

							//CalcInputFieldRect前に必要な処理。

							//フォントの設定。
							t_inputfield.Raw_GetText().font = t_inputfield.GetFont();

							//フォントサイズの設定。
							t_inputfield.Raw_GetText().fontSize = this.screen.CalcFontSize(t_inputfield);

							//スケール設定。
							t_inputfield.Raw_GetRectTransform().localScale = new Vector3(1.0f,1.0f,1.0f);
						}

						if((t_inputfield.IsVisible() == true)&&(t_inputfield.GetDrawPriority() >= 0)){

							//矩形計算。
							this.screen.CalcInputFieldRect(t_inputfield);

							//表示。
							t_raw_inputfield.enabled = true;
						}else{
							//非表示。
							t_raw_inputfield.enabled = false;
						}
					}
				}
			}
		}

		/** ＧＬ描画。MonoBehaviour_Camera_GLからの呼び出し。
		*/
		public void Draw_GL(int a_layerindex)
		{
			MaterialType t_current_material = MaterialType.None;

			int t_start_index = this.layerlist.GetStartIndex_Sprite(a_layerindex);
			int t_last_index = this.layerlist.GetLastIndex_Sprite(a_layerindex);

			if((t_start_index >= 0)&&(t_last_index >= 0)){

				UnityEngine.GL.PushMatrix();

				float[] t_to_8 = new float[8];

				try
				{
					UnityEngine.GL.LoadOrtho();

					bool t_is_begin = false;

					{
						for(int ii=t_start_index;ii<=t_last_index;ii++){
							Sprite2D t_sprite = this.sprite_list[ii];

							if((t_sprite.IsVisible() == true)&&(t_sprite.GetDrawPriority() >= 0)){

								Material t_material = this.materiallist.GetMaterial(t_sprite);

								//マテリアル変更。
								if(t_current_material != t_sprite.GetMaterialType()){
									if(t_is_begin == true){
										t_is_begin = false;
										UnityEngine.GL.End();
									}
									t_current_material = t_sprite.GetMaterialType();
								}

								//マテリアルの更新。
								bool t_change = t_sprite.UpdateMaterial(ref t_material);
								if(t_change == true){
									if(t_is_begin == true){
										t_is_begin = false;
										UnityEngine.GL.End();
									}
								}

								//パス設定。
								if(t_is_begin == false){
									t_is_begin = true;

									t_material.SetPass(0);

									UnityEngine.GL.Begin(UnityEngine.GL.TRIANGLES);
								}

								float t_from_x1 = t_sprite.GetTextureX() / Config.TEXTURE_W;
								float t_from_y1 = 1.0f - t_sprite.GetTextureY() / Config.TEXTURE_H;
								float t_from_x2 = (t_sprite.GetTextureX() + t_sprite.GetTextureW()) / Config.TEXTURE_W;
								float t_from_y2 = 1.0f - (t_sprite.GetTextureY() + t_sprite.GetTextureH()) / Config.TEXTURE_H;

								this.screen.CalcSpritePosition(t_sprite,t_to_8);

								Color t_color = t_sprite.GetColor();

								UnityEngine.GL.Color(t_color);

								{
									//左上。
									UnityEngine.GL.TexCoord2(t_from_x1,t_from_y1);
									UnityEngine.GL.Vertex3(t_to_8[0],t_to_8[1],0.0f);

									//右上。
									UnityEngine.GL.TexCoord2(t_from_x2,t_from_y1);
									UnityEngine.GL.Vertex3(t_to_8[2],t_to_8[3],0.0f);

									//左下。
									UnityEngine.GL.TexCoord2(t_from_x1,t_from_y2);
									UnityEngine.GL.Vertex3(t_to_8[4],t_to_8[5],0.0f);
								}

								{
									//左下。
									UnityEngine.GL.TexCoord2(t_from_x1,t_from_y2);
									UnityEngine.GL.Vertex3(t_to_8[4],t_to_8[5],0.0f);

									//右上。
									UnityEngine.GL.TexCoord2(t_from_x2,t_from_y1);
									UnityEngine.GL.Vertex3(t_to_8[2],t_to_8[3],0.0f);

									//右下。
									UnityEngine.GL.TexCoord2(t_from_x2,t_from_y2);
									UnityEngine.GL.Vertex3(t_to_8[6],t_to_8[7],0.0f);	
								}
							}
						}
					}

					if(t_is_begin == true){
						GL.End();
					}
				}catch(System.Exception t_exception){
					Tool.LogError(t_exception);
				}

				GL.PopMatrix();
			}
		}
	}
}

