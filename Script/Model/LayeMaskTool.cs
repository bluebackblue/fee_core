

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief モデル。レイヤーマスクツール。
*/


/** Fee.Model
*/
namespace Fee.Model
{
	/** 
	*/
	class LayerMaskTool
	{
		/** レイヤーマスク。取得。
		*/
		public int GetLayerMaskFromName(string a_layermask_name)
		{
			return UnityEngine.LayerMask.NameToLayer(a_layermask_name);
		}

		/** レイヤーマスク。設定。
		*/
		private static void Raw_SetLayer(UnityEngine.Transform a_transform,int a_layermask)
		{
			UnityEngine.GameObject t_gameobject = a_transform.GetComponent<UnityEngine.GameObject>();

			if(t_gameobject != null){
				t_gameobject.layer = a_layermask;
			}

			foreach(UnityEngine.Transform t_transform in a_transform){
				Raw_SetLayer(t_transform,a_layermask);
			}
		}

		/** レイヤーマスク。設定。
		*/
		private static void Raw_SetLayer(UnityEngine.Transform a_transform,UnityEngine.GameObject a_gameobject,int a_layermask)
		{
			UnityEngine.GameObject t_gameobject = a_gameobject;

			if(t_gameobject != null){
				t_gameobject.layer = a_layermask;
			}

			foreach(UnityEngine.Transform t_transform in a_transform){
				Raw_SetLayer(t_transform,a_layermask);
			}
		}

		/** レイヤーマスク。設定。
		*/
		public static void SetLayer(UnityEngine.Transform a_transform,string a_layermask_name)
		{
			int t_layermask = UnityEngine.LayerMask.NameToLayer(a_layermask_name);

			Raw_SetLayer(a_transform,t_layermask);
		}

		/** レイヤーマスク。設定。
		*/
		public static void SetLayer(UnityEngine.GameObject a_gameobject,string a_layermask_name)
		{
			int t_layermask = UnityEngine.LayerMask.NameToLayer(a_layermask_name);

			UnityEngine.Transform t_transform = a_gameobject.GetComponent<UnityEngine.Transform>();

			Raw_SetLayer(t_transform,a_gameobject,t_layermask);
		}
	}
}

