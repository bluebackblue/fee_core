

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
	/** LayerTool
	*/
	class LayerTool
	{
		/** レイヤーマスク。取得。
		*/
		public static int GetLayerIndexFromName(string a_layer_name)
		{
			return UnityEngine.LayerMask.NameToLayer(a_layer_name);
		}

		/** レイヤーマスク。全設定。
		*/
		private static void Raw_SetLayerAll(UnityEngine.Transform a_transform,int a_layer_index)
		{
			UnityEngine.GameObject t_gameobject = a_transform.GetComponent<UnityEngine.GameObject>();

			if(t_gameobject != null){
				t_gameobject.layer = a_layer_index;
			}

			foreach(UnityEngine.Transform t_transform in a_transform){
				Raw_SetLayerAll(t_transform,a_layer_index);
			}
		}

		/** レイヤーマスク。全設定。
		*/
		private static void Raw_SetLayerAll(UnityEngine.Transform a_transform,UnityEngine.GameObject a_gameobject,int a_layer_index)
		{
			UnityEngine.GameObject t_gameobject = a_gameobject;

			if(t_gameobject != null){
				t_gameobject.layer = a_layer_index;
			}

			foreach(UnityEngine.Transform t_transform in a_transform){
				Raw_SetLayerAll(t_transform,a_layer_index);
			}
		}

		/** レイヤーマスク。全設定。
		*/
		public static void SetLayerAll(UnityEngine.Transform a_transform,string a_layer_name)
		{
			int t_layer_index = UnityEngine.LayerMask.NameToLayer(a_layer_name);

			Raw_SetLayerAll(a_transform,t_layer_index);
		}

		/** レイヤーマスク。全設定。
		*/
		public static void SetLayerAll(UnityEngine.Transform a_transform,int a_layer_index)
		{
			Raw_SetLayerAll(a_transform,a_layer_index);
		}

		/** レイヤーマスク。全設定。
		*/
		public static void SetLayerAll(UnityEngine.GameObject a_gameobject,string a_layer_name)
		{
			int t_layer_index = UnityEngine.LayerMask.NameToLayer(a_layer_name);

			UnityEngine.Transform t_transform = a_gameobject.GetComponent<UnityEngine.Transform>();

			Raw_SetLayerAll(t_transform,a_gameobject,t_layer_index);
		}

		/** レイヤーマスク。全設定。
		*/
		public static void SetLayerAll(UnityEngine.GameObject a_gameobject,int a_layer_index)
		{
			UnityEngine.Transform t_transform = a_gameobject.GetComponent<UnityEngine.Transform>();

			Raw_SetLayerAll(t_transform,a_gameobject,a_layer_index);
		}

		/** レイヤーマスク。設定。
		*/
		public static void SetLayer(UnityEngine.GameObject a_gameobject,string a_layer_name)
		{
			int t_layer_index = UnityEngine.LayerMask.NameToLayer(a_layer_name);

			a_gameobject.layer = t_layer_index;
		}

		/** レイヤーマスク。設定。
		*/
		public static void SetLayer(UnityEngine.GameObject a_gameobject,int a_layer_index)
		{
			a_gameobject.layer = a_layer_index;
		}
	}
}

