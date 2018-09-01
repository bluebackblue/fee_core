using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ２Ｄ描画。コンフィグ。
*/


/** NRender2D
*/
namespace NRender2D
{
	/** Config
	*/
	public class Config
	{
		/** 仮想スクリーンサイズ。
		*/
		public static int VIRTUAL_W = 960;
		public static int VIRTUAL_H = 600;

		/** テクスチャースクリーンサイズ。
		*/
		public static float TEXTURE_W = 10.0f;
		public static float TEXTURE_H = 10.0f;

		/** テクスチャー矩形。最大値。
		*/
		public static Rect2D_R<float> TEXTURE_RECT_MAX = new Rect2D_R<float>(0.0f,0.0f,TEXTURE_W,TEXTURE_H);

		/** 矩形。最大値。
		*/
		public static Rect2D_R<int> VIRTUAL_RECT_MAX = new Rect2D_R<int>(0,0,VIRTUAL_W,VIRTUAL_H);

		/** ログ。
		*/
		public static bool LOG_ENABLE = false;

		/** アサート。
		*/
		public static bool ASSERT_ENABLE = false;

		/** 描画プライオリティ分割単位。
		*/
		public static long DRAWPRIORITY_STEP = 1000;

		/** 最大レイヤー数。
		*/
		public static int MAX_LAYER = 10;

		/** カメラデプス。開始値。
		*/
		public static float CAMERADEPTH_START = 10.0f;

		/** カメラデプス。レイヤーごとの基準値ステップ数。
		*/
		public static float CAMERADEPTH_STEP = 0.01f;

		/** カメラデプス。ＧＬカメラのレイヤーごとの基準値からのオフセット値。
		*/
		public static float CAMERADEPTH_OFFSET_GL = 0.001f;

		/** カメラデプス。ＵＩカメラのレイヤーごとの基準値からのオフセット値。
		*/
		public static float CAMERADEPTJ_OFFSET_UI = 0.002f;

		/** プレハブ名。
		*/
		public static string PREFAB_NAME_TEXT = "Prefab/Render2D/Text";
		public static string PREFAB_NAME_INPUTFIELD = "Prefab/Render2D/InputField";
		public static string PREFAB_NAME_CAMERA = "Prefab/Render2D/Camera";
		public static string PREFAB_NAME_CANVAS = "Prefab/Render2D/Canvas";
		public static string PREFAB_NAME_EVENTSYSTEM = "Prefab/Render2D/EventSystem";

		/** マテリアル名。
		*/
		public static string[] MATERIAL_STRING = {
			"Material/Render2D/Normal",
			"Material/Render2D/Alpha",
			"Material/Render2D/Alpha_Clip",
			"Material/Render2D/Add",
		};

		/** マテリアルタイプ。
		*/
		public enum MaterialType
		{
			None = -1,

			Normal = 0,
			Alpha,
			Alpha_Clip,
			Add
		}

		/** マテリアル名。テキスト。
		*/
		public static string MATERIAL_STRING_TEXT = "Material/Render2D/Text";

		/** デフォルト。テキスト。フォントサイズ。
		*/
		public static int DEFAULT_TEXT_FONTSIZE = 17;

		/** デフォルト。テキスト。色。
		*/
		public static Color DEFAULT_TEXT_COLOR = Color.white;

		/** デフォルト。スプライト。マテリアルタイプ。
		*/
		public static MaterialType DEFALUT_SPRITE_MATERIALTYPE = MaterialType.Normal;

		/** デフォルト。フォント名。
		*/
		public static string DEFAULT_FONT_NAME = "Arial.ttf";
	}
}

