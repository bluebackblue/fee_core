

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief プラットフォーム。
*/


/** Fee.Platform
*/
namespace Fee.Platform
{
	/** Root_MonoBehaviour
	*/
	public class Root_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** openfiledialog_result
		*/
		public string openfiledialog_result;

		/** loadcontentfile_asyncobject
		*/ 
		#if(UNITY_ANDROID)
		public UnityEngine.AndroidJavaObject loadcontentfile_asyncobject;
		#endif

		/** Awake
		*/
		public void Awake()
		{
			//openfiledialog_result
			this.openfiledialog_result = "";

			//loadcontentfile_asyncobject
			#if(UNITY_ANDROID)
			this.loadcontentfile_asyncobject = null;
			#endif
		}

		/** ログ。コールバック。
		*/
		public void Log_CallBack(string a_text)
		{
			Tool.Log("Log_CallBack",a_text);
		}

		/** オープンファイルダイログ。コールバック。
		*/
		public void OpenFileDialog_CallBack(string a_result)
		{
			if(a_result == null){
				this.openfiledialog_result = "";
			}else{
				this.openfiledialog_result = a_result;
			}
		}
	}
}

