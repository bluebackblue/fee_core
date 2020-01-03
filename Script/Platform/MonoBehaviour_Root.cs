

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
	/** MonoBehaviour_Root
	*/
	public class MonoBehaviour_Root : UnityEngine.MonoBehaviour
	{
		/** openfiledialog_result
		*/
		public string openfiledialog_result;

		/** Awake
		*/
		public void Awake()
		{
			//openfiledialog_result
			this.openfiledialog_result = "";
		}

		/** OpenFileDialog_Log
		*/
		#if((UNITY_ANDROID)||(UNITY_WEBGL))
		public void OpenFileDialog_Log(string a_text)
		{
			Tool.Log("OpenFileDialog_Log",a_text);
		}
		#endif

		/** オープンファイルダイログ。コールバック。
		*/
		#if((UNITY_ANDROID)||(UNITY_WEBGL))
		public void OpenFileDialog_CallBack(string a_result)
		{
			if(a_result == null){
				this.openfiledialog_result = "";
			}else{
				this.openfiledialog_result = a_result;
			}
		}
		#endif

		/** LoadContentFile_Log
		*/
		#if(UNITY_ANDROID)
		public void LoadContentFile_Log(string a_text)
		{
			Tool.Log("LoadContentFile_Log",a_text);
		}
		#endif
	}
}

