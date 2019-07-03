

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。レイヤーリスト。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** LayerList
	*/
	public class LayerList
	{
		/** レイヤーリスト。
		*/
		private LayerItem[] list;

		/** constructor
		*/
		public LayerList(UnityEngine.Transform a_transform_root)
		{
			//プレハブ読み込み。
			UnityEngine.GameObject t_prefab_canvas = UnityEngine.Resources.Load<UnityEngine.GameObject>(Config.PREFAB_NAME_CANVAS);
			UnityEngine.GameObject t_prefab_eventsystem = UnityEngine.Resources.Load<UnityEngine.GameObject>(Config.PREFAB_NAME_EVENTSYSTEM);

			//イベントシステム。インプットフィールド用。
			{
				UnityEngine.GameObject t_gameobject_eventsystem = UnityEngine.GameObject.Instantiate(t_prefab_eventsystem,UnityEngine.Vector3.zero,UnityEngine.Quaternion.identity);
				t_gameobject_eventsystem.name = "EventSystem";
				t_gameobject_eventsystem.transform.parent = a_transform_root;
			}

			//描画前処理。
			{
				UnityEngine.GameObject t_gameobject_main = new UnityEngine.GameObject();
				t_gameobject_main.name = "PreDraw";
				t_gameobject_main.AddComponent<MonoBehaviour_PreDraw>();
				t_gameobject_main.transform.SetParent(a_transform_root);
			}

			//レイヤーリスト。
			this.list = new LayerItem[Config.MAX_LAYER];
			for(int ii=0;ii<this.list.Length;ii++){
				this.list[ii] = new LayerItem();

				//描画順序。
				float t_gl_depth = Config.CAMERADEPTH_START + ii * Config.CAMERADEPTH_STEP + Config.CAMERADEPTH_OFFSET_GL;
				float t_ui_depth = Config.CAMERADEPTH_START + ii * Config.CAMERADEPTH_STEP + Config.CAMERADEPTH_OFFSET_UI;

				//カメラ。ＧＬ描画。
				UnityEngine.GameObject t_gameobject_camera_gl = Fee.Instantiate.Instantiate.CreateOrthographicCameraObject("Camera_" + ii.ToString() + "_GL",a_transform_root,t_gl_depth);
				UnityEngine.Camera t_camera_gl = t_gameobject_camera_gl.GetComponent<UnityEngine.Camera>();

				//カメラ。ＵＩ描画。
				UnityEngine.GameObject t_gameobject_camera_ui = Fee.Instantiate.Instantiate.CreateOrthographicCameraObject("Camera_" + ii.ToString() + "_UI",a_transform_root,t_ui_depth);
				UnityEngine.Camera t_camera_ui = t_gameobject_camera_ui.GetComponent<UnityEngine.Camera>();
				t_camera_ui.cullingMask = (1 << UnityEngine.LayerMask.NameToLayer("UI"));
				
				//キャンバス。
				UnityEngine.GameObject t_gameobject_canvas = UnityEngine.GameObject.Instantiate(t_prefab_canvas,UnityEngine.Vector3.zero,UnityEngine.Quaternion.identity);
				t_gameobject_canvas.name = "Canvas_" + ii.ToString();
				t_gameobject_canvas.transform.SetParent(a_transform_root);
				UnityEngine.Canvas t_canvas = t_gameobject_canvas.GetComponent<UnityEngine.Canvas>();

				//キャンバス設定。
				t_canvas.renderMode = UnityEngine.RenderMode.ScreenSpaceCamera;
				t_canvas.worldCamera = t_camera_ui;

				//ＧＬ描画設定。
				this.list[ii].camera_gl = t_gameobject_camera_gl.AddComponent<MonoBehaviour_Camera_GL>();
				this.list[ii].camera_gl.index = ii;
				this.list[ii].camera_gl.mycamera = t_camera_gl;
				this.list[ii].camera_gl.cameradepth = t_gl_depth;
				
				//ＵＩカメラ。
				this.list[ii].camera_ui = t_gameobject_camera_ui.AddComponent<MonoBehaviour_Camera_UI>();
				this.list[ii].camera_ui.index = ii;
				this.list[ii].camera_ui.mycamera = t_camera_ui;
				this.list[ii].camera_ui.cameradepth = t_ui_depth;

				//キャンバス設定。
				this.list[ii].canvas_transform = t_gameobject_canvas.GetComponent<UnityEngine.Transform>();
			}
		}

		/** 描画プライオリティからレイヤーインデックス取得。
		*/
		public int CalcLayerIndexFromDrawPriority(long a_drawpriority)
		{
			int t_layerindex = (int)(a_drawpriority / Config.DRAWPRIORITY_STEP);

			if((0<=t_layerindex)&&(t_layerindex<this.list.Length)){
				return t_layerindex;
			}

			return -1;
		}

		/** 描画プライオリティからレイヤートランスフォーム取得。
		*/
		public UnityEngine.Transform GetLayerTransformFromDrawPriority(long a_drawpriority)
		{
			int t_layerindex = this.CalcLayerIndexFromDrawPriority(a_drawpriority);

			if(t_layerindex >= 0){
				return this.list[t_layerindex].canvas_transform;
			}

			return null;
		}

		/** リストサイズ取得。
		*/
		public int GetListMax()
		{
			return this.list.Length;
		}

		/** GetStartIndex_Sprite
		*/
		public int GetStartIndex_Sprite(int a_layerindex)
		{
			return this.list[a_layerindex].sprite_index_start;
		}

		/** GetLastIndex_Sprite
		*/
		public int GetLastIndex_Sprite(int a_layerindex)
		{
			return this.list[a_layerindex].sprite_index_last;
		}

		/** GetStartIndex_Text
		*/
		public int GetStartIndex_Text(int a_layerindex)
		{
			return this.list[a_layerindex].text_index_start;
		}

		/** GetLastIndex_Text
		*/
		public int GetLastIndex_Text(int a_layerindex)
		{
			return this.list[a_layerindex].text_index_last;
		}

		/** GetStartIndex_InputField
		*/
		public int GetStartIndex_InputField(int a_layerindex)
		{
			return this.list[a_layerindex].inputfield_index_start;
		}

		/** GetLastIndex_InputField
		*/
		public int GetLastIndex_InputField(int a_layerindex)
		{
			return this.list[a_layerindex].inputfield_index_last;
		}

		/** デプスクリアーの設定。
		*/
		public void SetDepthClearGL(int a_layerindex,bool a_flag)
		{
			this.list[a_layerindex].camera_gl.SetDepthClear(a_flag);
		}

		/** デプスクリアーの設定。
		*/
		public void SetDepthClearUI(int a_layerindex,bool a_flag)
		{
			this.list[a_layerindex].camera_ui.SetDepthClear(a_flag);
		}

		/** コールバック。設定。
		*/
		public float GetGLCameraDepth(int a_layerindex)
		{
			return this.list[a_layerindex].camera_gl.cameradepth;
		}

		/** コールバック。設定。
		*/
		public float GetUICameraDepth(int a_layerindex)
		{
			return this.list[a_layerindex].camera_ui.cameradepth;
		}

		/** インデックス計算。
		*/
		public void CalcIndex(System.Collections.Generic.List<Sprite2D> a_sprite_list,System.Collections.Generic.List<Text2D> a_text_list,System.Collections.Generic.List<InputField2D> a_inputfield_list)
		{
			//リセット。
			for(int ii=0;ii<this.list.Length;ii++){
				this.list[ii].ResetIndex();
			}

			//スプライト。
			{
				int t_calc_mode = 0;
				int t_calc_layer = 0;
				int t_calc_index = 0;
				LayerItem t_calc_layeritem = this.list[t_calc_layer];

				while(t_calc_index < a_sprite_list.Count){
					int t_layerindex = this.CalcLayerIndexFromDrawPriority(a_sprite_list[t_calc_index].GetDrawPriority());
	
					if(t_calc_layer < this.list.Length){
						if(t_calc_mode == 0){
							//開始インデックス。

							if(t_layerindex < 0){
								//除外。
								t_calc_index++;
							}else{
								if(t_calc_layer == t_layerindex){
									//開始位置発見。
									t_calc_layeritem.sprite_index_start = t_calc_index;
									t_calc_layeritem.sprite_index_last = t_calc_index;

									t_calc_index++;
									t_calc_mode = 1;
								}else{
									//再チェック。
									t_calc_layeritem.sprite_index_start = -1;
									t_calc_layeritem.sprite_index_last = -1;

									t_calc_layer++;
									t_calc_layeritem = this.list[t_calc_layer];
									t_calc_mode = 0;
								}
							}
						}else{
							//終了インデックス。

							if(t_calc_layer == t_layerindex){
								//終了インデックス候補。
								t_calc_layeritem.sprite_index_last = t_calc_index;
								t_calc_index++;
							}else{
								//再チェック。
								t_calc_layer++;
								t_calc_layeritem = this.list[t_calc_layer];
								t_calc_mode = 0;
							}
						}
					}
				}
			}

			//テキスト。
			{
				int t_calc_mode = 0;
				int t_calc_layer = 0;
				int t_calc_index = 0;
				LayerItem t_calc_layeritem = this.list[t_calc_layer];

				while(t_calc_index < a_text_list.Count){
					int t_layerindex = this.CalcLayerIndexFromDrawPriority(a_text_list[t_calc_index].GetDrawPriority());
	
					if(t_calc_layer < this.list.Length){
						if(t_calc_mode == 0){
							//開始インデックス。

							if(t_layerindex < 0){
								//除外。
								t_calc_index++;
							}else{
								if(t_calc_layer == t_layerindex){
									//開始位置発見。
									t_calc_layeritem.text_index_start = t_calc_index;
									t_calc_layeritem.text_index_last = t_calc_index;

									t_calc_index++;
									t_calc_mode = 1;
								}else{
									//再チェック。
									t_calc_layeritem.text_index_start = -1;
									t_calc_layeritem.text_index_last = -1;

									t_calc_layer++;
									t_calc_layeritem = this.list[t_calc_layer];
									t_calc_mode = 0;
								}
							}
						}else{
							//終了インデックス。

							if(t_calc_layer == t_layerindex){
								//終了インデックス候補。
								t_calc_layeritem.text_index_last = t_calc_index;
								t_calc_index++;
							}else{
								//再チェック。
								t_calc_layer++;
								t_calc_layeritem = this.list[t_calc_layer];
								t_calc_mode = 0;
							}
						}
					}
				}
			}

			//入力フィールド。
			{
				int t_calc_mode = 0;
				int t_calc_layer = 0;
				int t_calc_index = 0;
				LayerItem t_calc_layeritem = this.list[t_calc_layer];

				while(t_calc_index < a_inputfield_list.Count){
					int t_layerindex = this.CalcLayerIndexFromDrawPriority(a_inputfield_list[t_calc_index].GetDrawPriority());
	
					if(t_calc_layer < this.list.Length){
						if(t_calc_mode == 0){
							//開始インデックス。

							if(t_layerindex < 0){
								//除外。
								t_calc_index++;
							}else{
								if(t_calc_layer == t_layerindex){
									//開始位置発見。
									t_calc_layeritem.inputfield_index_start = t_calc_index;
									t_calc_layeritem.inputfield_index_last = t_calc_index;

									t_calc_index++;
									t_calc_mode = 1;
								}else{
									//再チェック。
									t_calc_layeritem.inputfield_index_start = -1;
									t_calc_layeritem.inputfield_index_last = -1;

									t_calc_layer++;
									t_calc_layeritem = this.list[t_calc_layer];
									t_calc_mode = 0;
								}
							}
						}else{
							//終了インデックス。

							if(t_calc_layer == t_layerindex){
								//終了インデックス候補。
								t_calc_layeritem.inputfield_index_last = t_calc_index;
								t_calc_index++;
							}else{
								//再チェック。
								t_calc_layer++;
								t_calc_layeritem = this.list[t_calc_layer];
								t_calc_mode = 0;
							}
						}
					}
				}
			}

			for(int ii=1;ii<this.list.Length;ii++){
				if(this.list[ii].sprite_index_start >= 0){
					this.list[ii].camera_gl.SetActive(true);
				}else{
					this.list[ii].camera_gl.SetActive(false);
				}
			}

			for(int ii=1;ii<this.list.Length;ii++){
				if((this.list[ii].inputfield_index_last >= 0)||(this.list[ii].text_index_start >= 0)){
					this.list[ii].camera_ui.SetActive(true);
				}else{
					this.list[ii].camera_ui.SetActive(false);
				}
			}

			//ログ。
			for(int ii=0;ii<this.list.Length;ii++){
				this.list[ii].Log();
			}
		}
	}
}

