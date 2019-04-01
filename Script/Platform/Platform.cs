

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
		public static void OpenUrl(Fee.File.Path a_path)
		{
			#if(UNITY_WEBGL)
			{
				#if(UNITY_EDITOR)
				{
					UnityEngine.Application.OpenURL(a_path.GetPath());
				}
				#else
				{
					WebGL_OpenUrl.OpenUrl(a_path.GetPath());
				}
				#endif
			}
			#else
			{
				UnityEngine.Application.OpenURL(a_path.GetPath());
			}
			#endif
		}

		/** プロンプトを開く。
		*/
		public static string OpenPrompt(string a_title,string a_text)
		{
			#if(UNITY_WEBGL)
			{
				#if(UNITY_EDITOR)
				{
					return a_text;
				}
				#else
				{
					return WebGL_OpenPrompt.OpenPrompt(a_title,a_text);
				}
				#endif
			}
			#else
			{
				return a_text;
			}
			#endif		
		}

		/** プロンプトを開く。
		*/
		public static void SyncFs()
		{
			#if(UNITY_WEBGL)
			{
				#if(UNITY_EDITOR)
				{
				}
				#else
				{
					WebGL_SyncFs.SyncFs();
				}
				#endif
			}
			#else
			{
			}
			#endif		
		}

	}
}

