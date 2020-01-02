

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
			this.openfiledialog_result = "";
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

		/** OpenFileDialog_Log
		*/
		public void OpenFileDialog_Log(string a_text)
		{
			Tool.Log("OpenFileDialog_Log",a_text);
		}
	}
}

