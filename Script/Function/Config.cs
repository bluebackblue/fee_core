

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 関数呼び出し。コンフィグ。
*/


/** Fee.Function
*/
namespace Fee.Function
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

		/** プレイヤーループ。低アップデート。追加先
		*/
		public static System.Type PLAYERLOOP_ROWUPDATE_TARGETTYPE = typeof(UnityEngine.Experimental.PlayerLoop.Update.ScriptRunBehaviourUpdate);
		
		/** プレイヤーループ。低アップデート。追加方法。
		*/
		public static Fee.PlayerLoopSystem.AddType PLAYERLOOP_ROWUPDATE_ADDTYPE = Fee.PlayerLoopSystem.AddType.AddAfter;

		/** ROWUPDATE_DELTA
		*/
		public static float ROWUPDATE_DELTA = 1 / 20.0f;
	}
}

