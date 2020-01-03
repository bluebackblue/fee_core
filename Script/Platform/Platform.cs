

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
	/** Platform
	*/
	public class Platform
	{
		/** [シングルトン]s_instance
		*/
		private static Platform s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Platform();
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
		public static Platform GetInstance()
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

		/** ルート。
		*/
		private UnityEngine.GameObject root_gameobject;
		private MonoBehaviour_Root root_instance;

		/** [シングルトン]constructor
		*/
		private Platform()
		{
			this.root_gameobject = new UnityEngine.GameObject();
			this.root_gameobject.name = "Platform";
			UnityEngine.GameObject.DontDestroyOnLoad(this.root_gameobject);
			this.root_instance = this.root_gameobject.AddComponent<MonoBehaviour_Root>();

			#if((!UNITY_EDITOR)&&(UNITY_WEBGL))
			{
				WebGL_OpenFileDialog.Register();
			}
			#endif
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			UnityEngine.GameObject.Destroy(this.root_gameobject);
		}

		/** ＵＲＬを開く。
		*/
		public void OpenUrl(Fee.File.Path a_path)
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
		public string OpenPrompt(string a_title,string a_text)
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
		public void SyncFs()
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

		/** オープンファイルダイアログ。リクエスト。
		*/
		public void OpenFileDialog()
		{
			#if(UNITY_EDITOR)
			{
				Editor_OpenFileDialog.OpenFileDialog(this.root_instance);
			}
			#elif(UNITY_STANDALONE_WIN)
			{
				Windows_OpenFileDialog.OpenFileDialog(this.root_instance);
			}
			#elif(UNITY_WEBGL)
			{
				WebGL_OpenFileDialog.OpenFileDialog(this.root_instance);
			}
			#elif(UNITY_ANDROID)
			{
				Android_OpenFileDialog.OpenFileDialog(this.root_instance);
			}
			#endif
		}

		/** オープンファイルダイアログ。結果。取得。

			return == null : ダイアログ表示中。
			return == "" : キャンセル。
			return : ファイルパス。

		*/
		public string GetOpenFileDialogResult()
		{
			return this.root_instance.openfiledialog_result;
		}

		/** オープンファイルダイアログ。結果。設定。
		*/
		public void SetOpenfileDialogResult(string a_result)
		{
			this.root_instance.openfiledialog_result = a_result;
		}

		/** ロードコンテンツファイル。
		*/
		public byte[] LoadContentFile(Fee.File.Path a_path)
		{
			#if(UNITY_EDITOR)
			return null;
			#elif(UNITY_ANDROID)
			return Android_LoadContentFile.LoadContentFile(a_path);
			#else
			return null;
			#endif
		}
	}
}

