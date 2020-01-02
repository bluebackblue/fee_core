

/**
* Copyright (c) blueback
* Released under the MIT License
* https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
* @brief ２Ｄ描画。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
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

		/** [シングルトン]インスタンス。チェック。
		*/
		public static bool IsCreateInstance()
		{
			if(s_instance != null){
				return true;
			}
			return false;
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static Render2D GetInstance()
		{
			#if(UNITY_EDITOR)
			if(s_instance == null){
				Tool.Assert(false);
			}
			#endif

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
		private UnityEngine.GameObject root_gameobject;
		private UnityEngine.Transform root_transform;

		/** eventsystem
		*/
		private UnityEngine.GameObject eventsystem;

		/** スクリーン。
		*/
		private Screen screen;

		/** マテリアルリスト。
		*/
		private Material_List materiallist;

		/** スプライト。
		*/
		private SpriteList spritelist;

		/** テキスト。
		*/
		private TextList textlist;

		/** 入力フィールドリスト。
		*/
		private InputFieldList inputfieldlist;

		/** デフォルト。フォント。
		*/
		private UnityEngine.Font default_font;

		/** レイヤーリスト。
		*/
		private LayerList layerlist;

		/** バーテックス計算タスク。
		*/
		private Task_CalcVertex task_calcvertex;

		/** ソートリストタスク。
		*/
		private Task_SortList task_sortlist;

		/** OnChangeScreenSize
		*/
		public delegate void OnChangeScreenSize();
		OnChangeScreenSize callback_on_change_screen_size;

		/** [シングルトン]constructor
		*/
		private Render2D()
		{
			//ルート。
			{
				this.root_gameobject = new UnityEngine.GameObject();
				this.root_gameobject.name = "Render2D";
				UnityEngine.GameObject.DontDestroyOnLoad(this.root_gameobject);
				this.root_transform = this.root_gameobject.GetComponent<UnityEngine.Transform>();
			}

			//イベントシステム作成。入力フィールド用。
			{
				if(Fee.Render2D.Config.CREATE_EVENTYSYSTEM == true){
					this.eventsystem = Fee.Instantiate.Instantiate.CreateEventSystem("EventSystem",null/*this.root_transform*/);
					UnityEngine.GameObject.DontDestroyOnLoad(this.eventsystem);
				}
			}

			//スクリーン。
			this.screen = new Screen();

			//マテリアルリスト。
			this.materiallist = new Material_List();

			//スプライトリスト。
			this.spritelist = new SpriteList();

			//テキストリスト。
			this.textlist = new TextList();

			//入力フィールドリスト。
			this.inputfieldlist = new InputFieldList();

			//デフォルト。フォント。
			this.default_font = UnityEngine.Resources.GetBuiltinResource<UnityEngine.Font>(Config.DEFAULT_FONT_NAME);

			//レイヤーリスト。
			this.layerlist = new LayerList(this.root_gameobject.GetComponent<UnityEngine.Transform>());

			//バーテックス計算タスク。
			this.task_calcvertex = new Task_CalcVertex(this);

			//ソートリストタスク。
			this.task_sortlist = new Task_SortList(this);

			//callback_on_change_screen_size
			this.callback_on_change_screen_size = null;
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			//タスク終了。
			this.task_calcvertex.Delete();
			this.task_sortlist.Delete();

			this.spritelist.Delete();
			this.textlist.Delete();
			this.inputfieldlist.Delete();

			UnityEngine.GameObject.Destroy(this.root_gameobject);

			//イベントシステムの削除。
			if(Config.DELETE_EVENTSYSTEM == true){
				if(this.eventsystem != null){
					UnityEngine.GameObject.DestroyImmediate(this.eventsystem);
					this.eventsystem = null;
				}
			}
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

		/** ワールド座標 => 仮想スクリーン座標。
		*/
		public void WorldToVirtualScreen(UnityEngine.Camera a_camera,in UnityEngine.Vector3 a_position,out int a_virtual_x,out int a_virtual_y)
		{
			UnityEngine.Vector2 t_gui_pos = UnityEngine.RectTransformUtility.WorldToScreenPoint(a_camera,a_position);
			Fee.Render2D.Render2D.GetInstance().GuiScreenToVirtualScreen((int)t_gui_pos.x,(int)(this.screen.GetGuiH() - t_gui_pos.y),out a_virtual_x,out a_virtual_y);
		}

		/** スクリーンサイズ変更通知。登録。
		*/
		public void RegistOnChangeScreenSize(OnChangeScreenSize a_callback)
		{
			this.callback_on_change_screen_size += a_callback;
		}

		/** スクリーンサイズ変更通知。解除。
		*/
		public void UnRegistOnChangeScreenSize(OnChangeScreenSize a_callback)
		{
			this.callback_on_change_screen_size -= a_callback;
		}

		/** ルート。取得。
		*/
		public UnityEngine.Transform GetRootTransform()
		{
			return this.root_transform;
		}

		/** スプライトリスト。取得。
		*/
		public SpriteList GetSpriteList()
		{
			return this.spritelist;
		}

		/** テキストリスト。取得。
		*/
		public TextList GetTextList()
		{
			return this.textlist;
		}

		/** 入力フィールドリスト。取得。
		*/
		public InputFieldList GetInputFieldList()
		{
			return this.inputfieldlist;
		}

		/** スクリーン。取得。
		*/
		public Screen GetScreen()
		{
			return this.screen;
		}

		/** レイヤーレイスト。取得。
		*/
		public LayerList GetLayerList()
		{
			return this.layerlist;
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
		public void SetDefaultFont(UnityEngine.Font a_font)
		{
			this.default_font = a_font;
		}

		/** デフォルト。フォント取得。
		*/
		public UnityEngine.Font GetDefaultFont()
		{
			return this.default_font;
		}

		/** 共通ＵＩテキストマテリアルアイテム。取得。
		*/
		public Material_Item GetUiTextMaterialItem()
		{
			return this.materiallist.GetUiTextMaterialItem();
		}

		/** 共通ＵＩイメージマテリアルアイテム。取得。
		*/
		public Material_Item GetUiImageMaterialItem()
		{
			return this.materiallist.GetUiImageMaterialItem();
		}

		/** スプライト。登録。
		*/
		public void Sprite2D_Regist(Sprite2D a_sprite)
		{
			this.spritelist.Regist(a_sprite);
		}

		/** テキスト。登録。
		*/
		public void Text2D_Regist(Text2D a_text)
		{
			this.textlist.Regist(a_text);
		}

		/** 入力フィールド。登録。
		*/
		public void InputField2D_Regist(InputField2D a_inputfield)
		{
			this.inputfieldlist.Regist(a_inputfield);
		}

		/** スプライトプールリストキャパシティー。設定。
		*/
		public void SetSpritePoolListCapacity(int a_capacity)
		{
			this.spritelist.SetPoolListCapacity(a_capacity);
		}

		/** テキストプールリストキャパシティー。設定。
		*/
		public void SetTextPoolListCapacity(int a_capacity)
		{
			this.textlist.SetPoolListCapacity(a_capacity);
		}

		/** 入力フィールドプールリストキャパシティー。設定。
		*/
		public void SetInputFieldPoolListCapacity(int a_capacity)
		{
			this.inputfieldlist.SetPoolListCapacity(a_capacity);
		}

		/** カメラデプス。取得。
		*/
		public float GetGLCameraDepth(int a_layerindex)
		{
			return this.layerlist.GetGLCameraDepth(a_layerindex);
		}

		/** カメラデプス。取得。
		*/
		public float GetUICameraDepth(int a_layerindex)
		{
			return this.layerlist.GetUICameraDepth(a_layerindex);
		}

		/** カメラデプス。取得。
		*/
		public float GetCameraBeforeDepth(int a_layerindex)
		{
			return Config.CAMERADEPTH_START + a_layerindex * Config.CAMERADEPTH_STEP + Config.CAMERADEPTH_OFFSET_BEFORE;
		}

		/** カメラデプス。取得。
		*/
		public float GetCameraAfterDepth(int a_layerindex)
		{
			return Config.CAMERADEPTH_START + a_layerindex * Config.CAMERADEPTH_STEP + Config.CAMERADEPTH_OFFSET_AFTER;
		}

		/** ゲーム処理前。

			タスクを停止し、スプライト操作を可能にする。

			１、終了していないタスクの終了待ち。

		*/
		public void Main_Before()
		{
			//バーテックス計算タスク。終了待ち。
			this.task_calcvertex.Wait();

			//ソートリストタスク。キャンセル終了待ち。
			this.task_sortlist.CancelWait();
		}

		/** ゲーム処理後。

			タスクを開始する。ここからはスプライト操作は不可。

			１、ソートリストタスクの開始。

		*/
		public void Main_After()
		{
			//ソートリストタスク。開始。
			this.task_sortlist.Start();
		}

		/** 描画前処理。

			１、バーテックス計算タスクの終了待ち。
			２、ソートリストタスクの終了待ち。
			３、ソート結果に合わせてカメラの非表示化、ＵＩオブジェクトの関連付け。
			４、画面サイズ変更チェック。
			５、バーテックス計算タスクを開始。
			６、ＵＩオブジェクトのサイズ更新。

		*/
		public void Main_PreDraw()
		{
			//バーテックス計算タスク。終了待ち。
			this.task_calcvertex.Wait();

			//ソートリストタスク。終了待ち。
			this.task_sortlist.Wait();

			{
				//表示物のないカメラを非アクティブにする。
				if((this.spritelist.sortend_flag == true)||(this.textlist.sortend_flag == true)||(this.inputfieldlist.sortend_flag == true)){
					this.layerlist.ChangeActiveCamera();
				}

				//テキスト。描画プライオリティに対応したカメラに関連付ける。
				if(this.textlist.sortend_flag == true){
					this.textlist.ChangeParentLayer(this.layerlist);
				}

				//入力フィールド。描画プライオリティに対応したカメラに関連付ける。
				if(this.inputfieldlist.sortend_flag == true){
					this.inputfieldlist.ChangeParentLayer(this.layerlist);
				}

				//フラグリセット。
				this.spritelist.sortend_flag = false;
				this.textlist.sortend_flag = false;
				this.inputfieldlist.sortend_flag = false;
			}

			//スクリーンサイズ変更チェック。
			{
				Fee.Render2D.Render2D.GetInstance().screen.SetChangeScreenSizeFlag(false);
				Fee.Render2D.Render2D.GetInstance().screen.CalcScreen();

				//スクリーンサイズ変更あり。
				if(Fee.Render2D.Render2D.GetInstance().screen.GetChangeScreenSizeFlag() == true){
					
					//スクリーンサイズ変更通知。
					//ソートリストタスク終了後、バーテックス計算タスク開始前。
					if(this.callback_on_change_screen_size != null){
						this.callback_on_change_screen_size();
					}

					this.spritelist.ChangeScreenSize();
					this.textlist.ChangeScreenSize();
					this.inputfieldlist.ChangeScreenSize();
				}
			}

			//バーテックス計算タスク。開始。
			this.task_calcvertex.Start();

			//ＵＩの位置計算。
			{
				for(int ii=0;ii<this.layerlist.GetListMax();ii++){
					this.CalcUI(ii);
				}
			}
		}

		/** ＵＩの位置計算。
		*/
		private void CalcUI(int a_layerindex)
		{
			//テキスト。
			{
				int t_start_index = this.layerlist.GetStartIndex_Text(a_layerindex);
				int t_last_index = this.layerlist.GetLastIndex_Text(a_layerindex);

				if((t_start_index >= 0)&&(t_last_index >= 0)){
					for(int ii=t_start_index;ii<=t_last_index;ii++){
						Text2D t_text = this.textlist.GetItem(ii);

						//フォントサイズの計算が必要。
						if(t_text.Raw_IsCalcFontSize() == true){
							t_text.Raw_SetCalcFontSizeFlag(false);
							t_text.Raw_SetFontSize(this.screen.CalcFontSize(t_text));
						}

						//シェーダの変更が必要。
						if(t_text.Raw_IsChangeShader() == true){
							t_text.Raw_SetChangeShaderFlag(false);

							if(t_text.IsClip() == false){
								//共通ＵＩテキストマテリアルアイテム使用。
								t_text.Raw_SetTextMaterialItem(this.materiallist.GetUiTextMaterialItem());
							}else{
								//カスタムテキストマテリアルアイテム使用。
								{
									Material_Item t_material_item = t_text.GetCustomTextMaterialItem();
									int t_gui_x1;
									int t_gui_y1;
									int t_gui_x2;
									int t_gui_y2;
									this.VirtualScreenToGuiScreen(t_text.GetClipX(),t_text.GetClipY() + t_text.GetClipH(),out t_gui_x1,out t_gui_y1);
									this.VirtualScreenToGuiScreen(t_text.GetClipX() + t_text.GetClipW(),t_text.GetClipY(),out t_gui_x2,out t_gui_y2);

									//clip_flag
									t_material_item.SetProperty_ClipFlag(1);

									//clip_rect
									t_material_item.SetProperty_ClipRectA(t_gui_x1,t_gui_y1,t_gui_x2,t_gui_y2);

									t_text.Raw_SetTextMaterialItem(t_material_item);
								}
							}
						}

						if(t_text.IsDelete() == false){
							if((t_text.GetText().Length > 0)&&(t_text.IsVisible() == true)&&(t_text.GetDrawPriority() >= 0)){
								//矩形計算。
								this.screen.CalcTextRect(t_text);

								//表示。
								t_text.Raw_SetEnable(true);
							}else{
								//非表示。
								t_text.Raw_SetEnable(false);
							}
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
						InputField2D t_inputfield = this.inputfieldlist.GetItem(ii);

						//フォントサイズの計算が必要。
						if(t_inputfield.Raw_IsCalcFontSize() == true){
							t_inputfield.Raw_SetCalcFontSizeFlag(false);
							t_inputfield.Raw_SetFontSize(this.screen.CalcFontSize(t_inputfield));
						}

						//シェーダの変更が必要。
						if(t_inputfield.Raw_IsChangeShader() == true){
							t_inputfield.Raw_SetChangeShaderFlag(false);

							if(t_inputfield.IsClip() == false){
								//共通ＵＩイメージマテリアルアイテム使用。
								t_inputfield.Raw_SetTextMaterialItem(this.materiallist.GetUiTextMaterialItem());
								t_inputfield.Raw_SetImageMaterialItem(this.materiallist.GetUiImageMaterialItem());
							}else{
								//カスタムテキストマテリアルアイテム使用。
								{
									Material_Item t_text_material_item = t_inputfield.GetCustomTextMaterialItem();
									int t_gui_x1;
									int t_gui_y1;
									int t_gui_x2;
									int t_gui_y2;
									this.VirtualScreenToGuiScreen(t_inputfield.GetClipX(),t_inputfield.GetClipY() + t_inputfield.GetClipH(),out t_gui_x1,out t_gui_y1);
									this.VirtualScreenToGuiScreen(t_inputfield.GetClipX() + t_inputfield.GetClipW(),t_inputfield.GetClipY(),out t_gui_x2,out t_gui_y2);

									//clip_flag
									t_text_material_item.SetProperty_ClipFlag(1);

									//clip_rect
									t_text_material_item.SetProperty_ClipRectA(t_gui_x1,t_gui_y1,t_gui_x2,t_gui_y2);

									//Raw_SetTextMaterialItem
									t_inputfield.Raw_SetTextMaterialItem(t_text_material_item);
								}

								//カスタムイメージマテリアルアイテム使用。
								{
									Material_Item t_image_material_item = t_inputfield.GetCustomImageMaterialItem();
									int t_gui_x1;
									int t_gui_y1;
									int t_gui_x2;
									int t_gui_y2;
									this.VirtualScreenToGuiScreen(t_inputfield.GetClipX(),t_inputfield.GetClipY() + t_inputfield.GetClipH(),out t_gui_x1,out t_gui_y1);
									this.VirtualScreenToGuiScreen(t_inputfield.GetClipX() + t_inputfield.GetClipW(),t_inputfield.GetClipY(),out t_gui_x2,out t_gui_y2);

									//clip_flag
									t_image_material_item.SetProperty_ClipFlag(1);

									//clip_rect
									t_image_material_item.SetProperty_ClipRectA(t_gui_x1,t_gui_y1,t_gui_x2,t_gui_y2);

									//Raw_SetImageMaterialItem
									t_inputfield.Raw_SetImageMaterialItem(t_image_material_item);
								}
							}
						}

						if(t_inputfield.IsDelete() == false){
							if((t_inputfield.IsVisible() == true)&&(t_inputfield.GetDrawPriority() >= 0)){
								//矩形計算。
								this.screen.CalcInputFieldRect(t_inputfield);

								//表示。
								t_inputfield.Raw_SetEnable(true);
							}else{
								//非表示。
								t_inputfield.Raw_SetEnable(false);
							}
						}
					}
				}
			}
		}

		/** ＧＬ描画。MonoBehaviour_Camera_GLのOnPostRenderから呼び出される。
		*/
		public void OnPostRender_DrawGL(int a_layer_index)
		{
			//バーテックス計算タスク。終了待ち。
			this.task_calcvertex.Wait(a_layer_index);

			Material_Item t_current_material_item = null;

			int t_start_index = this.layerlist.GetStartIndex_Sprite(a_layer_index);
			int t_last_index = this.layerlist.GetLastIndex_Sprite(a_layer_index);

			if(a_layer_index == 0){
				//最初のカメラでレンダーテクスチャをクリアする。
				if(Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE == true){
					UnityEngine.GL.Clear(true,true,Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE_COLOR);
				}
			}

			if((t_start_index >= 0)&&(t_last_index >= 0)){

				bool t_is_begin = false;

				//GL.PushMatrix
				UnityEngine.GL.PushMatrix();

				try
				{
					//GL.LoadOrtho
					UnityEngine.GL.LoadOrtho();
					
					{
						for(int ii=t_start_index;ii<=t_last_index;ii++){
							Sprite2D t_sprite = this.spritelist.GetItem(ii);
							if((t_sprite.IsVisible() == true)&&(t_sprite.GetDrawPriority() >= 0)&&(t_sprite.IsDelete() == false)&&(t_sprite.GetW() != 0)&&(t_sprite.GetH() != 0)){

								//マテリアル変更。
								Material_Item t_material_item = this.materiallist.GetMaterialItem(t_sprite.GetMaterialType());
								if(t_current_material_item != t_material_item){
									t_current_material_item = t_material_item;
									
									//GL.End
									if(t_is_begin == true){
										t_is_begin = false;
										UnityEngine.GL.End();
									}
								}

								//マテリアルの更新。
								if(t_sprite.UpdateMaterialItem(t_material_item) == true){

									//GL.End
									if(t_is_begin == true){
										t_is_begin = false;
										UnityEngine.GL.End();
									}
								}

								//パス設定。
								if(t_is_begin == false){
									t_is_begin = true;

									//GL.Begin
									t_material_item.SetPass(0);
									UnityEngine.GL.Begin(UnityEngine.GL.TRIANGLES);
								}

								//GL.Color
								{
									UnityEngine.Color t_color = t_sprite.GetColor();
									UnityEngine.GL.Color(t_color);
								}

								//GL.TexCoord2
								//GL.Vertex3
								{
									//頂点情報。
									float[] t_texcord = t_sprite.GetTexCoord();
									float[] t_vertex = t_sprite.GetVertex();

									{
										//左上。
										UnityEngine.GL.TexCoord2(t_texcord[0],t_texcord[1]);
										UnityEngine.GL.Vertex3(t_vertex[0],t_vertex[1],0.0f);

										//右上。
										UnityEngine.GL.TexCoord2(t_texcord[2],t_texcord[1]);
										UnityEngine.GL.Vertex3(t_vertex[2],t_vertex[3],0.0f);

										//左下。
										UnityEngine.GL.TexCoord2(t_texcord[0],t_texcord[3]);
										UnityEngine.GL.Vertex3(t_vertex[4],t_vertex[5],0.0f);
									}

									{
										//左下。
										UnityEngine.GL.TexCoord2(t_texcord[0],t_texcord[3]);
										UnityEngine.GL.Vertex3(t_vertex[4],t_vertex[5],0.0f);

										//右上。
										UnityEngine.GL.TexCoord2(t_texcord[2],t_texcord[1]);
										UnityEngine.GL.Vertex3(t_vertex[2],t_vertex[3],0.0f);

										//右下。
										UnityEngine.GL.TexCoord2(t_texcord[2],t_texcord[3]);
										UnityEngine.GL.Vertex3(t_vertex[6],t_vertex[7],0.0f);	
									}
								}
							}
						}
					}
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}

				//GL.End
				if(t_is_begin == true){
					UnityEngine.GL.End();
				}

				//GL.PopMatrix
				UnityEngine.GL.PopMatrix();
			}
		}
	}
}

