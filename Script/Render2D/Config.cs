

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

		/** デバッグリスロー。
		*/
		public static bool DEBUGRETHROW_ENABLE = false;

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
		public static Fee.Geometry.Rect2D_R<float> TEXTURE_RECT_MAX = new Fee.Geometry.Rect2D_R<float>(0.0f,0.0f,TEXTURE_W,TEXTURE_H);

		/** 矩形。最大値。
		*/
		public static Fee.Geometry.Rect2D_R<int> VIRTUAL_RECT_MAX = new Fee.Geometry.Rect2D_R<int>(0,0,VIRTUAL_W,VIRTUAL_H);

		/** DEBUG_TRACECOUNT
		*/
		public static int DEBUG_TRACECOUNT = 4;

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

		/** 最初のカメラでレンダーテクスチャをクリアする。
		*/
		public static bool FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = false;

		/** 最初のカメラでレンダーテクスチャをクリアする。色。
		*/
		public static UnityEngine.Color FIRSTGLCAMERA_CLEAR_RENDERTEXTURE_COLOR = new UnityEngine.Color(0.0f,0.0f,0.0f,1.0f);
		
		/** 削除時にイベントシステムを削除するかどうか。
		*/
		public static bool DELETE_EVENTSYSTEM = true;

		/** SHADER_LIST
		*/
		public static System.Collections.Generic.Dictionary<MaterialType,ShaderItem> SHADER_LIST = new System.Collections.Generic.Dictionary<MaterialType,ShaderItem>(){
			{
				MaterialType.Simple,
				new ShaderItem(){
					shader_name = "Fee/Render2D/Simple",
					property_list = new string[]{
						"_MainTex",
					}
				}
			},
			{
				MaterialType.Alpha,
				new ShaderItem(){
					shader_name = "Fee/Render2D/Alpha",
					property_list = new string[]{
						"_MainTex",
					}
				}
			},
			{
				MaterialType.AlphaClip,
				new ShaderItem(){
					shader_name = "Fee/Render2D/AlphaClip",
					property_list = new string[]{
						"_MainTex",
						"clip_flag",
						"clip_x1",
						"clip_y1",
						"clip_x2",
						"clip_y2",
					}
				}
			},
			{
				MaterialType.Add,
				new ShaderItem(){
					shader_name = "Fee/Render2D/Add",
					property_list = new string[]{
						"_MainTex",
					}
				}
			},
			{
				MaterialType.UiText,
				new ShaderItem(){
					shader_name = "Fee/Render2D/UiText",
					property_list = new string[]{
						"_MainTex",
						"clip_flag",
						"clip_x1",
						"clip_y1",
						"clip_x2",
						"clip_y2",
					}
				}
			},
			{
				MaterialType.UiImage,
				new ShaderItem(){
					shader_name = "Fee/Render2D/UiImage",
					property_list = new string[]{
						"_MainTex",
						"clip_flag",
						"clip_x1",
						"clip_y1",
						"clip_x2",
						"clip_y2",
					}
				}
			},
			{
				MaterialType.UiInputImage,
				new ShaderItem(){
					shader_name = "Fee/Render2D/UiInputImage",
					property_list = new string[]{
						"_MainTex",
						"clip_flag",
						"clip_x1",
						"clip_y1",
						"clip_x2",
						"clip_y2",
					}
				}
			},
		};

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

		/** VIRTUAL_W、VIRTUAL_H、TEXTURE_W、TEXTURE_Hを変更した場合の一括再計算。
		*/
		public static void ReCalcWH()
		{
			/** テクスチャ矩形。最大値。
			*/
			TEXTURE_RECT_MAX = new Fee.Geometry.Rect2D_R<float>(0.0f,0.0f,TEXTURE_W,TEXTURE_H);

			/** 矩形。最大値。
			*/
			VIRTUAL_RECT_MAX = new Fee.Geometry.Rect2D_R<int>(0,0,VIRTUAL_W,VIRTUAL_H);
		}
	}
}

