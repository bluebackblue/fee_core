

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。コンフィグ。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** Config
	*/
	public class Config
	{
		/** ログ。
		*/
		public static bool LOG_ENABLE = false;

		/** ログエラー。
		*/
		public static bool LOGERROR_ENABLE = true;

		/** アサート。
		*/
		public static bool ASSERT_ENABLE = true;

		/** リスロー。
		*/
		public static bool RETHROW_ENABLE = false;




		/** 仮想スクリーンサイズ。
		*/
		public static int VIRTUAL_W = 960;
		public static int VIRTUAL_H = 600;

		/** テクスチャスクリーンサイズ。
		*/
		public static float TEXTURE_W = 10.0f;
		public static float TEXTURE_H = 10.0f;

		/** テクスチャ矩形。最大値。
		*/
		public static Rect2D_R<float> TEXTURE_RECT_MAX = new Rect2D_R<float>(0.0f,0.0f,TEXTURE_W,TEXTURE_H);

		/** 矩形。最大値。
		*/
		public static Rect2D_R<int> VIRTUAL_RECT_MAX = new Rect2D_R<int>(0,0,VIRTUAL_W,VIRTUAL_H);

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

		/** カメラデプス。レイヤーごとの基準値からのオフセット値。
		*/
		public static float CAMERADEPTH_OFFSET_BEFORE = 0.000f;

		/** カメラデプス。ＧＬカメラのレイヤーごとの基準値からのオフセット値。
		*/
		public static float CAMERADEPTH_OFFSET_GL = 0.001f;

		/** カメラデプス。ＵＩカメラのレイヤーごとの基準値からのオフセット値。
		*/
		public static float CAMERADEPTH_OFFSET_UI = 0.002f;

		/** カメラデプス。レイヤーごとの基準値からのオフセット値。
		*/
		public static float CAMERADEPTH_OFFSET_AFTER = 0.003f;

		/** 最初のカメラでレンダーテクスチャーをクリアする。
		*/
		public static bool FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = false;

		/** プレハブ名。
		*/
		public static string PREFAB_NAME_CANVAS = "Prefab/Render2D/Canvas";
		public static string PREFAB_NAME_EVENTSYSTEM = "Prefab/Render2D/EventSystem";

		/** マテリアル名。
		*/
		public static string[] MATERIAL_NAME = {
			"Material/Render2D/Simple",
			"Material/Render2D/Alpha",
			"Material/Render2D/AlphaClip",
			"Material/Render2D/Add",
			"Material/Render2D/Slice9",
		};

		/** マテリアルタイプ。
		*/
		public enum MaterialType
		{
			None = -1,

			/** 
			*/
			Simple = 0,
			Alpha,
			AlphaClip,
			Add,
			Slice9,
		}

		/** マテリアル名。ＵＩテキスト。
		*/
		public static string MATERIAL_NAME_UITEXT = "Material/Render2D/UiText";

		/** マテリアル名。ＵＩイメージ。
		*/
		public static string MATERIAL_NAME_UIIMAGE = "Material/Render2D/UiImage";

		/** デフォルト。テキスト。フォントサイズ。
		*/
		public static int DEFAULT_TEXT_FONTSIZE = 17;

		/** デフォルト。テキスト。色。
		*/
		public static UnityEngine.Color DEFAULT_TEXT_COLOR = UnityEngine.Color.white;

		/** デフォルト。スプライト。マテリアルタイプ。
		*/
		public static MaterialType DEFALUT_SPRITE_MATERIALTYPE = MaterialType.Simple;

		/** デフォルト。フォント名。
		*/
		public static string DEFAULT_FONT_NAME = "Arial.ttf";
	}
}

