

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief リフレクションツール。
*/


/** Fee.ReflectionTool
*/
namespace Fee.ReflectionTool
{
	/** ReflectionTool
	*/
	public class ReflectionTool
	{
		/** [シングルトン]s_instance
		*/
		private static ReflectionTool s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new ReflectionTool();
			}
		}

		/** [シングルトン]インスタンス。チェック。
		*/
		public static bool IsCreateInstance()
		{
			if(s_instance != null){
				return true;
			}
			return false;
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static ReflectionTool GetInstance()
		{
			#if(UNITY_EDITOR)
			if(s_instance == null){
				Tool.Assert(false);
			}
			#endif

			return s_instance;			
		}

		/** [シングルトン]インスタンス。削除。
		*/
		public static void DeleteInstance()
		{
			if(s_instance != null){
				s_instance.Delete();
				s_instance = null;
			}
		}

		/** [シングルトン]constructor
		*/
		private ReflectionTool()
		{
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}
	}
}

