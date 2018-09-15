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
		/** 正射影カメラオブジェクト作成。
		*/
		public static GameObject CreateOrthographicCameraObject(string a_name,Transform a_parent_transform,float a_depth)
		{
			GameObject t_gameoobject = new GameObject();
			t_gameoobject.name = a_name;
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

