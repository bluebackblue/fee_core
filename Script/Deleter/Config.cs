

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 削除管理。コンフィグ。
*/


/** Fee.Deleter
*/
namespace Fee.Deleter
{
	/** Config
	*/
	public class Config
	{
		/** ログ。
		*/
		public const bool LOG_ENABLE = false;

		/** ログエラー。
		*/
		public static bool LOGERROR_ENABLE = true;

		/** アサート。
		*/
		public static bool ASSERT_ENABLE = true;

		/** リスロー。
		*/
		public static bool RETHROW_ENABLE = false;
	}
}

