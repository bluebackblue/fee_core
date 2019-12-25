

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
		/** s_inputfield_sprite
		*/
		public static UnityEngine.Sprite s_inputfield_sprite = null;

		/** CreateEventSystem_InputSystem
		*/
		public static UnityEngine.GameObject CreateEventSystem(string a_name,UnityEngine.Transform a_parent_transform)
		{
			UnityEngine.GameObject t_gameobject = null;
			{
				UnityEngine.GameObject t_prefab = UnityEngine.Resources.Load<UnityEngine.GameObject>(Config.PREFAB_NAME_EVENTSYSTEM);
				if(t_prefab != null){
					t_gameobject = UnityEngine.GameObject.Instantiate<UnityEngine.GameObject>(t_prefab);
					if(t_gameobject != null){
						//name
						t_gameobject.name = a_name;
						
						//parent
						t_gameobject.GetComponent<UnityEngine.Transform>().SetParent(a_parent_transform);
					}else{
						Tool.Assert(false);
					}
				}else{
					Tool.Assert(false);
				}
			}

			return t_gameobject;
		}

		/** CreateUiInputField
		*/
		public static UnityEngine.GameObject CreateUiInputField(string a_name,UnityEngine.Transform a_parent_transform)
		{
			UnityEngine.UI.DefaultControls.Resources t_resources = new UnityEngine.UI.DefaultControls.Resources();
			{
				//テクスチャー読み込み。
				{
					if(s_inputfield_sprite == null){
						UnityEngine.Texture2D t_texture = UnityEngine.Resources.Load<UnityEngine.Texture2D>(Config.TEXTURE_NAME_INPUTFIELD);
						UnityEngine.Rect t_rect = new UnityEngine.Rect(0.0f,0.0f,t_texture.width,t_texture.height);
						UnityEngine.Vector2 t_pivot = new UnityEngine.Vector2(t_texture.width/2,t_texture.height/2);
						float t_pixels_per_unit = 100.0f;
						uint t_extrude = 1;
						UnityEngine.SpriteMeshType t_mesh_type = UnityEngine.SpriteMeshType.FullRect;
						UnityEngine.Vector4 t_border = new UnityEngine.Vector4(4.0f,4.0f,4.0f,4.0f);
						bool t_generate_fallback_physicsshape = true;

						s_inputfield_sprite = UnityEngine.Sprite.Create(t_texture,t_rect,t_pivot,t_pixels_per_unit,t_extrude,t_mesh_type,t_border,t_generate_fallback_physicsshape);
					}
				}

				t_resources.inputField = s_inputfield_sprite;
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

				//Depth
				t_camera.depth = a_depth;

				//Rendering Path
				t_camera.renderingPath = UnityEngine.RenderingPath.UsePlayerSettings;

				//TargetTexture
				t_camera.targetTexture = null;

				//Occlusion Culling
				t_camera.useOcclusionCulling = false;

				//Allow HDR
				t_camera.allowHDR = false;

				//Allow MSAA
				t_camera.allowMSAA = false;

				#if(UNITY_5)
				#else
				//Allow Dynamic Resolution
				t_camera.allowDynamicResolution = false;
				#endif
			}
			
			return t_gameobject;
		}
	}
}

