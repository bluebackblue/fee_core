using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief インスタンス作成。
*/


/** NInstantiate
*/
namespace NInstantiate
{
	/** Instantiate
	*/
	public class Instantiate
	{
		/** s_texture_inputfield
		*/
		public static Texture2D s_texture_inputfield = null;

		/** CreateUiInputField
		*/
		public static GameObject CreateUiInputField(string a_name,Transform a_parent_transform)
		{
			UnityEngine.UI.DefaultControls.Resources t_resouce = new UnityEngine.UI.DefaultControls.Resources();
			{
				if(s_texture_inputfield == null){
					s_texture_inputfield = Resources.Load<Texture2D>("Texture/Instantiate/InputField");
				}

				t_resouce.inputField = Sprite.Create(s_texture_inputfield,new Rect(0.0f,0.0f,s_texture_inputfield.width,s_texture_inputfield.height),new Vector2(0.0f,0.0f));
			}

			GameObject t_gameobject = UnityEngine.UI.DefaultControls.CreateInputField(t_resouce);

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
		public static GameObject CreateUiText(string a_name,Transform a_parent_transform)
		{
			UnityEngine.UI.DefaultControls.Resources t_resouce = new UnityEngine.UI.DefaultControls.Resources();
			{
			}
			GameObject t_gameobject = UnityEngine.UI.DefaultControls.CreateText(t_resouce);
			{
				UnityEngine.UI.Outline t_outline = t_gameobject.AddComponent<UnityEngine.UI.Outline>();
				{
					t_outline.useGraphicAlpha = true;
					t_outline.effectColor = Color.black;
					t_outline.effectDistance = new Vector2(1.0f,-1.0f);
				}

				UnityEngine.UI.Shadow t_shadow = t_gameobject.AddComponent<UnityEngine.UI.Shadow>();
				{
					t_shadow.useGraphicAlpha = true;
					t_outline.effectColor = Color.black;
					t_outline.effectDistance = new Vector2(1.0f,-1.0f);
				}
			}

			return t_gameobject;
		}

		/** 正射影カメラオブジェクト作成。
		*/
		public static GameObject CreateOrthographicCameraObject(string a_name,Transform a_parent_transform,float a_depth)
		{
			GameObject t_gameoobject = new GameObject(a_name);
			t_gameoobject.GetComponent<Transform>().SetParent(a_parent_transform);

			Camera t_camera = t_gameoobject.AddComponent<Camera>();
			{
				//Clear Flags
				t_camera.clearFlags = CameraClearFlags.Nothing;

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
				t_camera.rect = new Rect(0.0f,0.0f,1.0f,1.0f);

				//Depth
				t_camera.depth = a_depth;

				//Rendering Path
				t_camera.renderingPath = RenderingPath.UsePlayerSettings;

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

