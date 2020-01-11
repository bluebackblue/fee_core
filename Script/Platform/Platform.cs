

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
		private Root_MonoBehaviour root_monobehaviour;

		/** [シングルトン]constructor
		*/
		private Platform()
		{
			this.root_gameobject = new UnityEngine.GameObject();
			this.root_gameobject.name = "Platform";
			UnityEngine.GameObject.DontDestroyOnLoad(this.root_gameobject);
			this.root_monobehaviour = this.root_gameobject.AddComponent<Root_MonoBehaviour>();

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
			#if(UNITY_EDITOR)
			{
				UnityEngine.Application.OpenURL(a_path.GetPath());
			}
			#elif(UNITY_WEBGL)
			{
				WebGL_OpenUrl.OpenUrl(this.root_monobehaviour,a_path.GetPath());
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
			#if(UNITY_EDITOR)
			{
				return a_text;//TODO:
			}
			#elif(UNITY_WEBGL)
			{
				return WebGL_OpenPrompt.OpenPrompt(this.root_monobehaviour,a_title,a_text);
			}
			#else
			{
				return a_text;//TODO:
			}
			#endif
		}

		/** プロンプトを開く。
		*/
		public void SyncFs()
		{
			#if(UNITY_EDITOR)
			{
			}
			#elif(UNITY_WEBGL)
			{
				WebGL_SyncFs.SyncFs(this.root_monobehaviour);
			}
			#endif
		}

		/** CreateOpenFileDialogExtension
		*/
		public string CreateOpenFileDialogExtension(string[] a_extension_name)
		{
			if(a_extension_name != null){
				if(a_extension_name.Length > 0){
					#if(UNITY_EDITOR)
					{
						string t_extension = "Custom;";

						for(int ii=0;ii<a_extension_name.Length;ii++){
							t_extension += "*." + a_extension_name[ii];
							if(ii < a_extension_name.Length - 1){
								t_extension += ";";
							}
						}

						return t_extension;
					}
					#elif(UNITY_STANDALONE_WIN)
					{
						string t_extension = "Custom (";
						for(int ii=0;ii<a_extension_name.Length;ii++){
							t_extension += "*." + a_extension_name[ii];
							if(ii < a_extension_name.Length - 1){
								t_extension += ";";
							}
						}
						t_extension += ")\0";

						for(int ii=0;ii<a_extension_name.Length;ii++){
							t_extension += "*." + a_extension_name[ii];
							if(ii < a_extension_name.Length - 1){
								t_extension += ";";
							}
						}

						t_extension += "\0All Files (*.*)\0*.*\0\0";

						return t_extension;
					}
					#elif(UNITY_WEBGL)
					{
						string t_extension = "";

						for(int ii=0;ii<a_extension_name.Length;ii++){
							t_extension += "." + a_extension_name[ii];
							if(ii < a_extension_name.Length - 1){
								t_extension += ",";
							}
						}

						return t_extension;
					}
					#elif(UNITY_ANDROID)
					{
						return "*/*";
					}
					#endif
				}
			}

			{
				#if(UNITY_EDITOR)
				{
					return "";
				}
				#elif(UNITY_STANDALONE_WIN)
				{
					return "All Files (*.*)\0*.*\0" + "\0";
				}
				#elif(UNITY_WEBGL)
				{
					return "*/*";
				}
				#elif(UNITY_ANDROID)
				{
					return "*/*";
				}
				#endif
			}
		}

		/** オープンファイルダイアログ。リクエスト。
		*/
		public void OpenFileDialog(string a_title,string a_extension)
		{
			#if(UNITY_EDITOR)
			{
				Editor_OpenFileDialog.OpenFileDialog(this.root_monobehaviour,a_title,a_extension);
			}
			#elif(UNITY_STANDALONE_WIN)
			{
				Windows_OpenFileDialog.OpenFileDialog(this.root_monobehaviour,a_title,a_extension);
			}
			#elif(UNITY_WEBGL)
			{
				WebGL_OpenFileDialog.OpenFileDialog(this.root_monobehaviour,a_title,a_extension);
			}
			#elif(UNITY_ANDROID)
			{
				Android_OpenFileDialog.OpenFileDialog(this.root_monobehaviour,a_title,a_extension);
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
			return this.root_monobehaviour.openfiledialog_result;
		}

		/** オープンファイルダイアログ。結果。設定。
		*/
		public void SetOpenfileDialogResult(string a_result)
		{
			this.root_monobehaviour.openfiledialog_result = a_result;
		}

		/** ロードコンテンツファイル。開始。
		*/
		public bool LoadAndroidContentFile_Start(Fee.File.Path a_path)
		{
			#if(UNITY_EDITOR)
			{
				return false;
			}
			#elif(UNITY_ANDROID)
			{
				return Android_LoadAndroidContentFile.Start(this.root_monobehaviour,a_path);
			}
			#else
			{
				return false;
			}
			#endif
		}

		/** ロードコンテンツファイル。終了。
		*/
		public void LoadAndroidContentFile_End()
		{
			#if(UNITY_EDITOR)
			{
			}
			#elif(UNITY_ANDROID)
			{
				Android_LoadAndroidContentFile.End(this.root_monobehaviour);
			}
			#endif
		}

		/** ロードコンテンツファイル。キャンセル。
		*/
		public void LoadAndroidContentFile_Cancel()
		{
			#if(UNITY_EDITOR)
			{
			}
			#elif(UNITY_ANDROID)
			{
				Android_LoadAndroidContentFile.Cancel(this.root_monobehaviour);
			}
			#endif
		}

		/** ロードコンテンツファイル。終了。チェック。
		*/
		public bool LoadAndroidContentFile_IsComplate()
		{
			#if(UNITY_EDITOR)
			{
				return true;
			}
			#elif(UNITY_ANDROID)
			{
				return Android_LoadAndroidContentFile.IsComplate(this.root_monobehaviour);
			}
			#else
			{
				return true;
			}
			#endif
		}

		/** ロードコンテンツファイル。結果取得。
		*/
		public byte[] LoadAndroidContentFile_GetResult()
		{
			#if(UNITY_EDITOR)
			{
				return null;
			}
			#elif(UNITY_ANDROID)
			{
				return Android_LoadAndroidContentFile.GetResult(this.root_monobehaviour);
			}
			#else
			{
				return null;
			}
			#endif
		}

		/** ロードコンテンツファイル。スレッドステータス取得。
		*/
		public int LoadAndroidContentFile_GetThreadStatus()
		{
			#if(UNITY_EDITOR)
			{
				return -1;
			}
			#elif(UNITY_ANDROID)
			{
				return Android_LoadAndroidContentFile.GetThreadStatus(this.root_monobehaviour);
			}
			#else
			{
				return -1;
			}
			#endif
		}
	}
}

