using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief パフォーマンスカウンター。コンフィグ。
*/


/** NPerformanceCounter
*/
namespace NPerformanceCounter
{
	/** Config
	*/
	public class Config
	{
		/** ログ。
		*/
		public static bool LOG_ENABLE = false;

		/** アサート。
		*/
		public static bool ASSERT_ENABLE = false;

		/** プレハブ名。
		*/
		//public static string PREFAB_NAME_CAMERA = "Prefab/Blur/Camera";

		/** マテリアル名。
		*/
		public static string MATERIAL_NAME_BLURX = "Material/Blur/BlurX";
		public static string MATERIAL_NAME_BLURY = "Material/Blur/BlurY";

		/** デフォルト。カメラデプス。
		*/
		public static float DEFAULT_CAMERA_DEPTH = 900.0f;
		
	}
}

