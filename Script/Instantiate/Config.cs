

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief インスタンス作成。コンフィグ。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
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

		/** プレハブ名。
		*/
		#if(UNITY_2018)

		public static string PREFAB_NAME_EVENTSYSTEM = "Prefab/Instantiate/EventSystem_InputManager";

		#elif(USE_DEF_FEE_INPUTSYSTEM)

		public static string PREFAB_NAME_EVENTSYSTEM = "Prefab/Instantiate/EventSystem_InputSystem";

		#else

		public static string PREFAB_NAME_EVENTSYSTEM = "Prefab/Instantiate/EventSystem_InputManager";

		#endif

		/** テクスチャー名。
		*/
		public static string TEXTURE_NAME_INPUTFIELD = "Texture/Instantiate/InputField";
	}
}

