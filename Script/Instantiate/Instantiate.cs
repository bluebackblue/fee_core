

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
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
		/** s_texture_inputfield
		*/
		public static UnityEngine.Texture2D s_texture_inputfield = null;

		/** CreateUiInputField
		*/
		public static UnityEngine.GameObject CreateUiInputField(string a_name,UnityEngine.Transform a_parent_transform)
		{
			UnityEngine.UI.DefaultControls.Resources t_resouce = new UnityEngine.UI.DefaultControls.Resources();
			{
				if(s_texture_inputfield == null){
					s_texture_inputfield = UnityEngine.Resources.Load<UnityEngine.Texture2D>("Texture/Instantiate/InputField");
				}

				t_resouce.inputField = UnityEngine.Sprite.Create(s_texture_inputfield,new UnityEngine.Rect(0.0f,0.0f,s_texture_inputfield.width,s_texture_inputfield.height),new UnityEngine.Vector2(0.0f,0.0f));
			}

			UnityEngine.GameObject t_gameobject = UnityEngine.UI.DefaultControls.CreateInputField(t_resouce);

			if(t_gameobject != null){
				UnityEngine.UI.Image t_image = t_gameobject.GetComponent<UnityEngine.UI.Image>();
				if(t_image != null){
					t_image.type = UnityEngine.UI.Image.Type.Simple;
				}
			}

			return t_gameobject;
		}

		/** CreateUiText
		*/
		public static UnityEngine.GameObject CreateUiText(string a_name,UnityEngine.Transform a_parent_transform)
		{
			UnityEngine.UI.DefaultControls.Resources t_resouce = new UnityEngine.UI.DefaultControls.Resources();
			{
			}
			UnityEngine.GameObject t_gameobject = UnityEngine.UI.DefaultControls.CreateText(t_resouce);
			{
				UnityEngine.UI.Outline t_outline = t_gameobject.AddComponent<UnityEngine.UI.Outline>();
				{
					t_outline.useGraphicAlpha = true;
					t_outline.effectColor = UnityEngine.Color.black;
					t_outline.effectDistance = new UnityEngine.Vector2(1.0f,-1.0f);
				}

				UnityEngine.UI.Shadow t_shadow = t_gameobject.AddComponent<UnityEngine.UI.Shadow>();
				{
					t_shadow.useGraphicAlpha = true;
					t_outline.effectColor = UnityEngine.Color.black;
					t_outline.effectDistance = new UnityEngine.Vector2(1.0f,-1.0f);
				}
			}

			return t_gameobject;
		}

		/** 正射影カメラオブジェクト作成。
		*/
		public static UnityEngine.GameObject CreateOrthographicCameraObject(string a_name,UnityEngine.Transform a_parent_transform,float a_depth)
		{
			UnityEngine.GameObject t_gameoobject = new UnityEngine.GameObject(a_name);
			t_gameoobject.GetComponent<UnityEngine.Transform>().SetParent(a_parent_transform);

			UnityEngine.Camera t_camera = t_gameoobject.AddComponent<UnityEngine.Camera>();
			{
				//Clear Flags
				t_camera.clearFlags = UnityEngine.CameraClearFlags.Nothing;

				//Culling Mask(LayerMask.GetMask(null))
				t_camera.cullingMask = 0;

				//Projection
				t_camera.orthographic = true;

				//Size
				t_camera.orthographicSize = 5.0f;

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

				//Allow Dynamic Resolution
				t_camera.allowDynamicResolution = false;
			}
			
			return t_gameoobject;
		}
	}
}

