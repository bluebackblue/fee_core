

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

		/** スクリーン。
		*/
		private Screen screen;

		/** マテリアルリスト。
		*/
		private Material_List materiallist;

		/** スプライト。
		*/
		private System.Collections.Generic.List<Sprite2D> sprite_list;
		private bool sprite_list_change_index;

		/** テキスト。
		*/
		private System.Collections.Generic.List<Text2D> text_list;
		private bool text_list_change_index;

		/** 入力フィールド。
		*/
		private System.Collections.Generic.List<InputField2D> inputfield_list;
		private bool inputfield_list_change_index;

		/** ソートリクエスト。
		*/
		private bool spritelist_sort_request;
		private bool textlist_sort_request;
		private bool inputfieldlist_sort_request;

		/** 削除リクエスト。
		*/
		private bool spritelist_delete_request;
		private bool textlist_delete_request;
		private bool inputfieldlist_delete_request;

		/** デフォルト。フォント。
		*/
		private UnityEngine.Font default_font;

		/** レイヤーリスト。
		*/
		private LayerList layerlist;

		/** タスク。リストソート。
		*/
		private Fee.TaskW.Task<int> task_sort_list;
		private Fee.TaskW.CancelToken task_sort_list_canceltoken;

		/** タスク。バーテックス計算。
		*/
		private Fee.TaskW.Task<int>[] task_calcvertex;

		/** frame
		*/
		private int frame;
		private const int frame_max = 120;

		/** [シングルトン]constructor。
		*/
		private Render2D()
		{
			//ルート。
			this.root_gameobject = new UnityEngine.GameObject();
			this.root_gameobject.name = "Render2D";
			UnityEngine.GameObject.DontDestroyOnLoad(this.root_gameobject);
			this.root_transform = this.root_gameobject.GetComponent<UnityEngine.Transform>();

			//スクリーン。
			this.screen = new Screen();

			//マテリアルリスト。
			this.materiallist = new Material_List();

			//スプライト。
			this.sprite_list = new System.Collections.Generic.List<Sprite2D>();
			this.sprite_list_change_index = true;

			//テキスト。
			this.text_list = new System.Collections.Generic.List<Text2D>();
			this.text_list_change_index = true;

			//入力フィールド。
			this.inputfield_list = new System.Collections.Generic.List<InputField2D>();
			this.inputfield_list_change_index = true;

			//ソートリクエスト。
			this.spritelist_sort_request = true;
			this.textlist_sort_request = true;
			this.inputfieldlist_sort_request = true;

			//削除リクエスト。
			this.spritelist_delete_request = true;
			this.textlist_delete_request = true;
			this.inputfieldlist_delete_request = true;

			//デフォルト。フォント。
			this.default_font = UnityEngine.Resources.GetBuiltinResource<UnityEngine.Font>(Config.DEFAULT_FONT_NAME);

			//レイヤーリスト。
			this.layerlist = new LayerList(this.root_gameobject.GetComponent<UnityEngine.Transform>());

			//タスク。リストソート。
			this.task_sort_list_canceltoken = new Fee.TaskW.CancelToken();
			this.task_sort_list = new Fee.TaskW.Task<int>(TaskW.Mode.InstanceMode_Function);
			this.task_sort_list.SetFunction(()=>{
				return Task_SortList(this.task_sort_list_canceltoken);
			});

			//タスク。バーテックス計算。
			this.task_calcvertex = new Fee.TaskW.Task<int>[Config.MAX_LAYER];
			for(int ii=0;ii<this.task_calcvertex.Length;ii++){
				this.task_calcvertex[ii] = new Fee.TaskW.Task<int>(TaskW.Mode.InstanceMode_Function);
				int t_layerindex = ii;
				this.task_calcvertex[ii].SetFunction(()=>{
					return Task_CalcVertex(t_layerindex);
				});
			}

			//frame
			this.frame = 0;
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			UnityEngine.GameObject.Destroy(this.root_gameobject);
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

		/** ＵＩテキストマテリアルアイテム。取得。
		*/
		public Material_Item GetUiTextMaterialItem()
		{
			return this.materiallist.GetUiTextMaterialItem();
		}

		/** ＵＩイメージマテリアルアイテム。取得。
		*/
		public Material_Item GetUiImageMaterialItem()
		{
			return this.materiallist.GetUiImageMaterialItem();
		}

		/** [RawText]作成。
		*/
		public UnityEngine.GameObject RawText_Create()
		{
			return Fee.Instantiate.Instantiate.CreateUiText("Text",this.root_transform);
		}

		/** [RawInputField]作成。
		*/
		public UnityEngine.GameObject RawInputField_Create()
		{
			return Fee.Instantiate.Instantiate.CreateUiInputField("InputField",this.root_transform);
		}

		/** [RawText]削除。
		*/
		public void RawText_Delete(UnityEngine.GameObject a_gameobject)
		{
			UnityEngine.GameObject.Destroy(a_gameobject);
		}

		/** [RawInputField]削除。
		*/
		public void RawInputField_Delete(UnityEngine.GameObject a_gameobject)
		{
			UnityEngine.GameObject.Destroy(a_gameobject);
		}

		/** スプライト作成。
		*/
		public void AddSprite2D(Sprite2D a_sprite)
		{
			this.sprite_list.Add(a_sprite);
			this.spritelist_sort_request = true;
		}

		/** テキスト作成。
		*/
		public void AddText2D(Text2D a_text)
		{
			this.text_list.Add(a_text);
			this.textlist_sort_request = true;
		}

		/** 入力フィールド作成。
		*/
		public void AddInputField2D(InputField2D a_inputfield)
		{
			this.inputfield_list.Add(a_inputfield);
			this.inputfieldlist_sort_request = true;
		}

		/** スプライトリスト。ソートリクエスト。
		*/
		public void SpriteListSortRequest()
		{
			this.spritelist_sort_request = true;
		}

		/** スプライトリスト。削除リクエスト。
		*/
		public void SpriteListDeleteRequest()
		{
			this.spritelist_delete_request = true;
		}

		/** テキストリスト。ソートリクエスト。
		*/
		public void TextListSortRequest()
		{
			this.textlist_sort_request = true;
		}

		/** テキストリスト。削除リクエスト。
		*/
		public void TextListDeleteRequest()
		{
			this.textlist_delete_request = true;
		}

		/** 入力フィールドリスト。ソートリクエスト。
		*/
		public void InputFieldListSortRequest()
		{
			this.inputfieldlist_sort_request = true;
		}

		/** 入力フィールドリスト。削除リクエスト。
		*/
		public void InputFieldDeleteRequest()
		{
			this.inputfieldlist_delete_request = true;
		}

		/** デプスクリアフラグ。設定。
		*/
		public void SetDepthClearGL(int a_layerindex,bool a_flag)
		{
			this.layerlist.SetDepthClearFlagGL(a_layerindex,a_flag);
		}

		/** デプスクリアフラグ。設定。
		*/
		public void SetDepthClearUI(int a_layerindex,bool a_flag)
		{
			this.layerlist.SetDepthClearFlagUI(a_layerindex,a_flag);
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

		/** 事前計算。取得。
		*/
		public bool GetChangeScreenFlag()
		{
			return this.screen.GetChangeScreenFlag();
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

		/** タスク。リストソート。
		*/
		private static int Task_SortList(Fee.TaskW.CancelToken a_cancel_token)
		{
			{
				//テキスト。削除。
				if((Fee.Render2D.Render2D.GetInstance().frame % Render2D.frame_max) == 10){
					if(Fee.Render2D.Render2D.GetInstance().textlist_delete_request == true){
						Fee.Render2D.Render2D.GetInstance().textlist_delete_request = false;
						Fee.Render2D.Render2D.GetInstance().text_list_change_index = true;

						//削除。
						Fee.Render2D.Render2D.GetInstance().text_list.RemoveAll((Fee.Render2D.Text2D a_text) => {
							return a_text.IsDelete();
						}); 
					}
				}

				//テキスト。ソート。
				if(Fee.Render2D.Render2D.GetInstance().textlist_sort_request == true){
					Fee.Render2D.Render2D.GetInstance().textlist_sort_request = false;
					Fee.Render2D.Render2D.GetInstance().text_list_change_index = true;

					Fee.Render2D.Render2D.GetInstance().text_list.Sort(Text2D.Sort_DrawPriority);

					//キャンセル処理。
					if(a_cancel_token.IsCancellationRequested() == true){
						a_cancel_token.ThrowIfCancellationRequested();
						return 0;
					}
				}

				//テキスト。インデックス計算。
				if(Fee.Render2D.Render2D.GetInstance().text_list_change_index  == true){
					Fee.Render2D.Render2D.GetInstance().layerlist.CalcTextIndex(Fee.Render2D.Render2D.GetInstance().text_list);
				}
			}

			{
				//入力フィールド。削除。
				if((Fee.Render2D.Render2D.GetInstance().frame % Render2D.frame_max) == 20){
					if(Fee.Render2D.Render2D.GetInstance().inputfieldlist_delete_request == true){
						Fee.Render2D.Render2D.GetInstance().inputfieldlist_delete_request= false;
						Fee.Render2D.Render2D.GetInstance().inputfield_list_change_index = true;

						//削除。
						Fee.Render2D.Render2D.GetInstance().inputfield_list.RemoveAll((Fee.Render2D.InputField2D a_inputfield) => {
							return a_inputfield.IsDelete();
						}); 
					}

					//キャンセル処理。
					if(a_cancel_token.IsCancellationRequested() == true){
						a_cancel_token.ThrowIfCancellationRequested();
						return 0;
					}
				}

				//入力フィールド。ソート。
				if(Fee.Render2D.Render2D.GetInstance().inputfieldlist_sort_request == true){
					Fee.Render2D.Render2D.GetInstance().inputfieldlist_sort_request = false;
					Fee.Render2D.Render2D.GetInstance().inputfield_list_change_index  = true;

					Fee.Render2D.Render2D.GetInstance().inputfield_list.Sort(InputField2D.Sort_DrawPriority);
				}

				//インデックス計算。
				if(Fee.Render2D.Render2D.GetInstance().inputfield_list_change_index == true){
					Fee.Render2D.Render2D.GetInstance().layerlist.CalcInputFieldIndex(Fee.Render2D.Render2D.GetInstance().inputfield_list);
				}
			}

			{
				//スプライト。削除。
				if((Fee.Render2D.Render2D.GetInstance().frame % Render2D.frame_max) == 0){
					if(Fee.Render2D.Render2D.GetInstance().spritelist_delete_request == true){
						Fee.Render2D.Render2D.GetInstance().spritelist_delete_request = false;
						Fee.Render2D.Render2D.GetInstance().sprite_list_change_index = true;

						//削除。
						Fee.Render2D.Render2D.GetInstance().sprite_list.RemoveAll((Fee.Render2D.Sprite2D a_sprite) => {
							return a_sprite.IsDelete();
						}); 
					}
				}

				//スプライト。ソート。
				if(Fee.Render2D.Render2D.GetInstance().spritelist_sort_request == true){
					Fee.Render2D.Render2D.GetInstance().spritelist_sort_request = false;
					Fee.Render2D.Render2D.GetInstance().sprite_list_change_index = true;

					Fee.Render2D.Render2D.GetInstance().sprite_list.Sort(Sprite2D.Sort_DrawPriority);

					//キャンセル処理。
					if(a_cancel_token.IsCancellationRequested() == true){
						a_cancel_token.ThrowIfCancellationRequested();
						return 0;
					}
				}

				//スプライト。インデックス計算。
				if(Fee.Render2D.Render2D.GetInstance().sprite_list_change_index  == true){
					Fee.Render2D.Render2D.GetInstance().layerlist.CalcSpriteIndex(Fee.Render2D.Render2D.GetInstance().sprite_list);
				}
			}

			return 0;
		}

		/** タスク。バーテックス計算。
		*/
		private static int Task_CalcVertex(int a_layerindex)
		{
			try{
				Screen t_screen = Fee.Render2D.Render2D.GetInstance().screen;
				System.Collections.Generic.List<Fee.Render2D.Sprite2D> t_sprite_list = Fee.Render2D.Render2D.GetInstance().sprite_list;
				Fee.Render2D.LayerList t_layerlist = Fee.Render2D.Render2D.GetInstance().layerlist;

				int t_start_index = t_layerlist.GetStartIndex_Sprite(a_layerindex);
				int t_last_index = t_layerlist.GetLastIndex_Sprite(a_layerindex);

				//計算。
				if((t_start_index >= 0)&&(t_last_index >= 0)){
					for(int ii=t_start_index;ii<=t_last_index;ii++){
						Sprite2D t_sprite = t_sprite_list[ii];
						if((t_sprite.IsVisible() == true)&&(t_sprite.GetDrawPriority() >= 0)){
							t_screen.CalcSprite(t_sprite);
						}
					}
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.Log(t_exception.Message);
			}

			return 0;
		}

		/** ゲーム処理前。
		*/
		public void Main_Before()
		{
			this.frame = (this.frame + 1) % 120;

			//タスク。リストソート。終了待ち。
			if(this.task_sort_list.IsEndFunction() == false){
				if(this.task_sort_list.IsEnd() == false){
					this.task_sort_list_canceltoken.Cancel();
					this.task_sort_list.Wait();
				}
				this.task_sort_list.EndFunction();
			}

			//タスク。バーテックス計算。終了待ち。
			{
				for(int ii=0;ii<this.task_calcvertex.Length;ii++){
					if(this.task_calcvertex[ii].IsEndFunction() == false){
						if(this.task_calcvertex[ii].IsEnd() == false){
							this.task_calcvertex[ii].Wait();
						}
						this.task_calcvertex[ii].EndFunction();
					}
				}
			}

			//ここから。スプライト操作が可能。
		}

		/** ゲーム処理後。
		*/
		public void Main_After()
		{
			//ここまで。スプライト操作が可能。

			//タスク実行。
			{
				#if((UNITY_5)||(UNITY_WEBGL))
				{
					//PreDrawから呼び出す。
				}
				#else
				{
					//キャンセルトークンリセット。
					if(this.task_sort_list_canceltoken.IsCancellationRequested() == true){
						this.task_sort_list_canceltoken.Reset();
					}

					//タスク。リストソート。開始。
					this.task_sort_list.StartFunction();
				}
				#endif
			}
		}

		/** 描画前処理。
		*/
		public void PreDraw()
		{
			//タスク実行。
			#if((UNITY_5)||(UNITY_WEBGL))
			{
				if(this.task_calc_sprite_vertex.IsEndFunction() == false){
					//タスク関数。実行中。

					//キャンセル。
					this.task_calc_sprite_vertex_cancel_token.Cancel();

					//タスク終了待ち。
					this.task_calc_sprite_vertex.Wait();

					//タスク関数。終了。
					this.task_calc_sprite_vertex.EndFunction();
				}

				//キャンセルトークンリセット。
				if(this.task_calc_sprite_vertex_cancel_token.IsCancellationRequested() == true){
					this.task_calc_sprite_vertex_cancel_token.Reset();
				}

				//タスク関数。開始。
				this.task_calc_sprite_vertex.StartFunction();
			}
			#endif

			//タスク。リストソート。終了待ち。
			if(this.task_sort_list.IsEndFunction() == false){
				this.task_sort_list.Wait();
				this.task_sort_list.EndFunction();
			}

			{
				//表示物のないカメラを非アクティブにする。
				if((Fee.Render2D.Render2D.GetInstance().sprite_list_change_index == true)||(Fee.Render2D.Render2D.GetInstance().text_list_change_index == true)||(Fee.Render2D.Render2D.GetInstance().inputfield_list_change_index == true)){
					this.layerlist.SetActiveCamera();
				}

				//テキスト。描画プライオリティに対応したカメラに関連付ける。
				if(Fee.Render2D.Render2D.GetInstance().text_list_change_index == true){
					for(int ii=0;ii<this.text_list.Count;ii++){
						if(this.text_list[ii].IsDelete() == false){
							this.text_list[ii].Raw_SetLayer(this.layerlist.GetLayerTransformFromDrawPriority(this.text_list[ii].GetDrawPriority()));
						}
					}
				}

				//入力フィールド。描画プライオリティに対応したカメラに関連付ける。
				if(Fee.Render2D.Render2D.GetInstance().inputfield_list_change_index == true){
					for(int ii=0;ii<this.inputfield_list.Count;ii++){
						if(this.inputfield_list[ii].IsDelete() == false){
							this.inputfield_list[ii].Raw_SetLayer(this.layerlist.GetLayerTransformFromDrawPriority(this.inputfield_list[ii].GetDrawPriority()));
						}
					}
				}

				//フラグリセット。
				Fee.Render2D.Render2D.GetInstance().sprite_list_change_index = false;
				Fee.Render2D.Render2D.GetInstance().text_list_change_index = false;
				Fee.Render2D.Render2D.GetInstance().inputfield_list_change_index = false;
			}

			//スクリーンサイズ変更チェック。
			{
				//事前計算。
				Fee.Render2D.Render2D.GetInstance().screen.CalcScreen();

				//スクリーンサイズ変更あり。
				if(Fee.Render2D.Render2D.GetInstance().screen.GetChangeScreenFlag() == true){
					System.Collections.Generic.List<Fee.Render2D.Sprite2D> t_sprite_list = Fee.Render2D.Render2D.GetInstance().sprite_list;
					for(int ii=0;ii<t_sprite_list.Count;ii++){
						t_sprite_list[ii].RequestReCalcVertex();
					}

					System.Collections.Generic.List<Fee.Render2D.Text2D> t_text_list = Fee.Render2D.Render2D.GetInstance().text_list;
					for(int ii=0;ii<t_text_list.Count;ii++){
						t_text_list[ii].Raw_SetCalcFontSizeFlag(true);
						t_text_list[ii].Raw_SetCalcSizeFlag(true);
					}
					
					System.Collections.Generic.List<Fee.Render2D.InputField2D> t_inputfield_list = Fee.Render2D.Render2D.GetInstance().inputfield_list;
					for(int ii=0;ii<t_inputfield_list.Count;ii++){
						t_inputfield_list[ii].Raw_SetCalcFontSizeFlag(true);
					}
				}
			}

			//タスク実行。バーテックス計算。開始。
			{
				for(int ii=0;ii<this.task_calcvertex.Length;ii++){
					Screen t_screen = Fee.Render2D.Render2D.GetInstance().screen;
					System.Collections.Generic.List<Fee.Render2D.Sprite2D> t_sprite_list = Fee.Render2D.Render2D.GetInstance().sprite_list;
					Fee.Render2D.LayerList t_layerlist = Fee.Render2D.Render2D.GetInstance().layerlist;
					int t_start_index = t_layerlist.GetStartIndex_Sprite(ii);
					int t_last_index = t_layerlist.GetLastIndex_Sprite(ii);
					if((t_start_index >= 0)&&(t_last_index >= 0)){
						this.task_calcvertex[ii].StartFunction();
					}
				}
			}

			//ＵＩ描画。
			{
				for(int ii=0;ii<this.layerlist.GetListMax();ii++){
					this.PreDraw_UI(ii);
				}
			}
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

						//フォントサイズの計算が必要。
						if(t_text.Raw_IsCalcFontSize() == true){
							t_text.Raw_SetCalcFontSizeFlag(false);
							t_text.Raw_SetFontSize(this.screen.CalcFontSize(t_text));
						}

						//シェーダの変更が必要。
						if(t_text.Raw_IsChangeShader() == true){
							t_text.Raw_SetChangeShaderFlag(false);

							if(t_text.IsClip() == false){
								//共通テキストマテリアルアイテム使用。
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
						InputField2D t_inputfield = this.inputfield_list[ii];

						//フォントサイズの計算が必要。
						if(t_inputfield.Raw_IsCalcFontSize() == true){
							t_inputfield.Raw_SetCalcFontSizeFlag(false);
							t_inputfield.Raw_SetFontSize(this.screen.CalcFontSize(t_inputfield));
						}

						//シェーダの変更が必要。
						if(t_inputfield.Raw_IsChangeShader() == true){
							t_inputfield.Raw_SetChangeShaderFlag(false);

							if(t_inputfield.IsClip() == false){
								//共通テキストマテリアルアイテム使用。
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

		/** ＧＬ描画。MonoBehaviour_Camera_GLからの呼び出し。
		*/
		public void Draw_GL(int a_layerindex)
		{
			MaterialType t_current_material_type = MaterialType.None;

			int t_start_index = this.layerlist.GetStartIndex_Sprite(a_layerindex);
			int t_last_index = this.layerlist.GetLastIndex_Sprite(a_layerindex);

			//タスク。バーテックス計算。終了待ち。
			{
				if(this.task_calcvertex[a_layerindex].IsEndFunction() == false){
					this.task_calcvertex[a_layerindex].Wait();
					this.task_calcvertex[a_layerindex].EndFunction();
				}
			}

			if(a_layerindex == 0){
				//最初のカメラでレンダーテクスチャをクリアする。
				if(Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE == true){
					UnityEngine.GL.Clear(true,true,Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE_COLOR);
				}
			}

			if((t_start_index >= 0)&&(t_last_index >= 0)){

				UnityEngine.GL.PushMatrix();

				try
				{
					UnityEngine.GL.LoadOrtho();

					bool t_is_begin = false;

					{
						for(int ii=t_start_index;ii<=t_last_index;ii++){
							Sprite2D t_sprite = this.sprite_list[ii];

							if((t_sprite.IsVisible() == true)&&(t_sprite.GetDrawPriority() >= 0)&&(t_sprite.IsDelete() == false)){

								Material_Item t_material_item = this.materiallist.GetMaterialItem(t_sprite.GetMaterialType());

								//マテリアル変更。
								if(t_current_material_type != t_sprite.GetMaterialType()){
									if(t_is_begin == true){
										t_is_begin = false;
										UnityEngine.GL.End();
									}
									t_current_material_type = t_sprite.GetMaterialType();
								}

								//マテリアルの更新。
								bool t_change = t_sprite.UpdateMaterialItem(t_material_item);
								if(t_change == true){
									if(t_is_begin == true){
										t_is_begin = false;
										UnityEngine.GL.End();
									}
								}

								//パス設定。
								if(t_is_begin == false){
									t_is_begin = true;

									t_material_item.SetPass(0);

									UnityEngine.GL.Begin(UnityEngine.GL.TRIANGLES);
								}

								//色。
								UnityEngine.Color t_color = t_sprite.GetColor();
								UnityEngine.GL.Color(t_color);

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

					if(t_is_begin == true){
						UnityEngine.GL.End();
					}
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}

				UnityEngine.GL.PopMatrix();
			}
		}
	}
}

