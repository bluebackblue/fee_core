

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief インスタンス作成。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** Instantiate
	*/
	public class Instantiate
	{
		/** [シングルトン]s_instance
		*/
		private static Instantiate s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Instantiate();
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
		public static Instantiate GetInstance()
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

		/** inputfield_sprite
		*/
		public UnityEngine.Sprite inputfield_sprite = null;

		/** [シングルトン]constructor
		*/
		private Instantiate()
		{
			//Sprite のグラフィックスとして適用させるテクスチャ。
			UnityEngine.Texture2D t_texture = UnityEngine.Texture2D.whiteTexture;
			{
				t_texture = new UnityEngine.Texture2D(64,64);
				for(int xx=0;xx<t_texture.width;xx++){
					for(int yy=0;yy<t_texture.height;yy++){

						float t_value_x;
						if(xx < (t_texture.width / 2)){
							t_value_x = xx;
						}else{
							t_value_x = t_texture.width - 1 - xx;
						}

						float t_value_y;
						if(yy < (t_texture.height / 2)){
							t_value_y = yy;
						}else{
							t_value_y = t_texture.width - 1 - yy;
						}

						float t_value = UnityEngine.Mathf.Min(t_value_x,t_value_y);

						t_value = UnityEngine.Mathf.Clamp01(t_value * 0.8f);
						t_texture.SetPixel(xx,yy,new UnityEngine.Color(1.0f,1.0f,1.0f,t_value));
					}
				}

				t_texture.filterMode = UnityEngine.FilterMode.Bilinear;
				t_texture.wrapMode = UnityEngine.TextureWrapMode.Clamp;
				t_texture.Apply();
			}

			//Sprite に適用させるテクスチャの Rect 領域。
			UnityEngine.Rect t_rect = new UnityEngine.Rect(0.0f,0.0f,t_texture.width,t_texture.height);

			//グラフィックスの Rect に対するピボット地点の相対位置。
			UnityEngine.Vector2 t_pivot = new UnityEngine.Vector2(t_texture.width / 2,t_texture.height / 2);

			//ワールド空間座標の 1 単位分に相当する、スプライトのピクセル数。
			const float t_pixels_per_unit = 100.0f;

			//スプライトメッシュが外側に拡張する量。
			const uint t_extrude = 0;
						
			//スプライトのために生成されるメッシュタイプの制御。
			//FullRect	: 矩形はメッシュと等しいユーザーが指定したスプライトサイズ。
			//Tight		: ピクセルのアルファ値を基にしたタイトなメッシュ。多くの余分なピクセルは可能な限りクロップされます。
			UnityEngine.SpriteMeshType t_mesh_type = UnityEngine.SpriteMeshType.FullRect;

			//スプライトのサイズ (X=左、Y=下、Z=右、W=上)。
			UnityEngine.Vector4 t_border = new UnityEngine.Vector4(0.0f,0.0f,0.0f,0.0f);

			//Generates a default physics shape for the sprite.
			bool t_generate_fallback_physicsshape = true;

			//UnityEngine.Sprite.Create
			this.inputfield_sprite = UnityEngine.Sprite.Create(t_texture,t_rect,t_pivot,t_pixels_per_unit,t_extrude,t_mesh_type,t_border,t_generate_fallback_physicsshape);
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** CreateUiInputField
		*/
		public static UnityEngine.GameObject CreateUiInputField(string a_name,UnityEngine.Transform a_parent_transform)
		{
			UnityEngine.UI.DefaultControls.Resources t_resources = new UnityEngine.UI.DefaultControls.Resources();
			{
				t_resources.inputField = Instantiate.GetInstance().inputfield_sprite;;
			}

			UnityEngine.GameObject t_gameobject = UnityEngine.UI.DefaultControls.CreateInputField(t_resources);
			if(t_gameobject != null){
				//name
				t_gameobject.name = a_name;

				//parent
				t_gameobject.GetComponent<UnityEngine.Transform>().SetParent(a_parent_transform);

				//image
				UnityEngine.UI.Image t_image = t_gameobject.GetComponent<UnityEngine.UI.Image>();
				{
					t_image.type = UnityEngine.UI.Image.Type.Sliced;
				}
			}else{
				Tool.Assert(false);
			}

			return t_gameobject;
		}

		/** CreateUiText
		*/
		public static UnityEngine.GameObject CreateUiText(string a_name,UnityEngine.Transform a_parent_transform)
		{
			UnityEngine.UI.DefaultControls.Resources t_resources = new UnityEngine.UI.DefaultControls.Resources();
			{
			}

			UnityEngine.GameObject t_gameobject = UnityEngine.UI.DefaultControls.CreateText(t_resources);
			if(t_gameobject != null){
				//name
				t_gameobject.name = a_name;
				
				//parent
				t_gameobject.GetComponent<UnityEngine.Transform>().SetParent(a_parent_transform);

				//outline
				UnityEngine.UI.Outline t_outline = t_gameobject.AddComponent<UnityEngine.UI.Outline>();
				{
					t_outline.useGraphicAlpha = true;
					t_outline.effectColor = UnityEngine.Color.black;
					t_outline.effectDistance = new UnityEngine.Vector2(1.0f,-1.0f);
				}

				//shadow
				UnityEngine.UI.Shadow t_shadow = t_gameobject.AddComponent<UnityEngine.UI.Shadow>();
				{
					t_shadow.useGraphicAlpha = true;
					t_outline.effectColor = UnityEngine.Color.black;
					t_outline.effectDistance = new UnityEngine.Vector2(1.0f,-1.0f);
				}
			}else{
				Tool.Assert(false);
			}

			return t_gameobject;
		}

		/** CreateCanvas
		*/
		public static UnityEngine.GameObject CreateCameraCanvas(string a_name,UnityEngine.Transform a_parent_transform,UnityEngine.Camera a_camera)
		{
			UnityEngine.GameObject t_gameobject = new UnityEngine.GameObject(a_name);

			//parent
			t_gameobject.GetComponent<UnityEngine.Transform>().SetParent(a_parent_transform);

			//layer
			t_gameobject.layer = UnityEngine.LayerMask.NameToLayer("UI");

			//canvas
			UnityEngine.Canvas t_canvas = t_gameobject.AddComponent<UnityEngine.Canvas>();
			{
				t_canvas.renderMode = UnityEngine.RenderMode.ScreenSpaceCamera;
				t_canvas.worldCamera = a_camera;
			}

			//canvasscaler
			UnityEngine.UI.CanvasScaler t_canvasscaler = t_gameobject.AddComponent<UnityEngine.UI.CanvasScaler>();
			{
			}

			//graphicraycaster
			UnityEngine.UI.GraphicRaycaster t_graphicraycaster = t_gameobject.AddComponent<UnityEngine.UI.GraphicRaycaster>();
			{
			}

			return t_gameobject;
		}

		/** 正射影カメラオブジェクト作成。
		*/
		public static UnityEngine.GameObject CreateOrthographicCameraObject(string a_name,UnityEngine.Transform a_parent_transform,float a_depth)
		{
			UnityEngine.GameObject t_gameobject = new UnityEngine.GameObject(a_name);

			//parent
			t_gameobject.GetComponent<UnityEngine.Transform>().SetParent(a_parent_transform);

			//camera
			UnityEngine.Camera t_camera = t_gameobject.AddComponent<UnityEngine.Camera>();
			{
				//Reset
				t_camera.Reset();

				//Depth
				t_camera.depth = a_depth;

				//Clear Flags
				t_camera.clearFlags = UnityEngine.CameraClearFlags.Nothing;

				//Culling Mask(LayerMask.GetMask(null))
				t_camera.cullingMask = 0;

				//Projection
				t_camera.orthographic = true;

				//Size
				t_camera.orthographicSize = 1.0f;

				//Clipping Planes
				t_camera.nearClipPlane = 0.0f;
				t_camera.farClipPlane = 1000.0f;

				//Viewport Rect
				t_camera.rect = new UnityEngine.Rect(0.0f,0.0f,1.0f,1.0f);
			}
			
			return t_gameobject;
		}
	}
}

