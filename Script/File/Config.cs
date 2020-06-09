

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。コンフィグ。
*/


/** Fee.File
*/
namespace Fee.File
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

		/** プレイヤーループ。追加先
		*/
		public static System.Type PLAYERLOOP_TARGETTYPE = typeof(UnityEngine.Experimental.PlayerLoop.PreLateUpdate);
		
		/** プレイヤーループ。追加方法。
		*/
		public static Fee.PlayerLoopSystem.AddType PLAYERLOOP_ADDTYPE = Fee.PlayerLoopSystem.AddType.AddLast;

		/** USE_ASYNC
		*/
		#if(UNITY_5)
		public static bool USE_ASYNC = false;
		#elif(UNITY_WEBGL)
		public static bool USE_ASYNC = false;
		#else
		public static bool USE_ASYNC = true;
		#endif

		/** WORK_LIMIT
		*/
		public static int WORK_LIMIT = 10;

		/** RESPONSECODE_KEY
		*/
		public static string RESPONSECODE_KEY = "ResponseCode";
	}
}

