

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief プラットフォーム。
*/


/** Fee.Platform
*/
namespace Fee.Platform
{
	/** Platform
	*/
	public class Platform
	{
		/** ＵＲＬを開く。
		*/
		public static void OpenUrl(string a_url)
		{
			

			#if(UNITY_WEBGL)
			{
				#if(UNITY_EDITOR)
				{
					UnityEngine.Application.OpenURL(a_url);
				}
				#else
				{
					WebGL_OpenUrl.OpenUrl(a_url);
				}
				#endif
			}
			#else
			{
				WebGL_OpenUrl.OpenUrl(a_url);
			}
			#endif
		}
	}
}

