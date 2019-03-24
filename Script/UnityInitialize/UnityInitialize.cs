

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＮＩＴＹ初期化。
*/


/** Fee.UnityInitialize
*/
namespace Fee.UnityInitialize
{
	/** UnityInitialize
	*/
	#if(UNITY_EDITOR)
	public class UnityInitialize
	{
		/** メインスクリプト。作成。
		*/
		[UnityEditor.MenuItem("Fee/Initialize/CreateMainScript")]
		private static void CreateMainScript()
		{
			//スクリプトテンプレートを読み込み。
			string t_script_template = null;
			{
				string t_in_fullpath = FindFile("UnityInitialize","Main.temp.cs");
				if(t_in_fullpath != null){
					t_script_template = ReadTextFile(t_in_fullpath);
				}
				if(t_script_template != null){
					t_script_template = t_script_template.Replace("USE_DEF_FEE_TEMP","true");
				}else{
					return;
				}
			}

			//スクリプトの書き込み。
			WriteTextFile(UnityEngine.Application.dataPath + "/" + "Main.cs",t_script_template);

			//更新。
			UnityEditor.AssetDatabase.Refresh();
		}

		/** メインオブジェクト。存在チェック。
		*/
		private static bool IsExistMainGameObject()
		{
			UnityEngine.GameObject[] t_gameobject = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
			for(int ii=0;ii<t_gameobject.Length;ii++){
				if(t_gameobject[ii].name == "Main"){
					return true;
				}
			}
			return false;
		}

		/** メインスクリプト。存在チェック。
		*/
		private static bool IsExistMainScript()
		{
			return System.IO.File.Exists(UnityEngine.Application.dataPath + "/" + "Main.cs");
		}

		/** ファイル検索。
		*/
		private static string FindFile(string a_dir_name,string a_file_name)
		{
			string[] t_dir_list = System.IO.Directory.GetDirectories(UnityEngine.Application.dataPath,a_dir_name,System.IO.SearchOption.AllDirectories);
			for(int ii=0;ii<t_dir_list.Length;ii++){
				string[] t_file_list = System.IO.Directory.GetFiles(t_dir_list[ii],a_file_name,System.IO.SearchOption.TopDirectoryOnly);
				if(t_file_list != null){
					if(t_file_list.Length > 0){
						return t_file_list[0];
					}
				}
			}
			return null;
		}

		/** テキストファイル読み込み。
		*/
		private static string ReadTextFile(string a_full_path)
		{
			string t_full_path = a_full_path;

			string t_string = null;
			try{
				using(System.IO.StreamReader t_stream = new System.IO.StreamReader(t_full_path)){
					t_string = t_stream.ReadToEnd();
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
			return t_string;
		}

		/** テキストファイル書き込み。
		*/
		private static void WriteTextFile(string a_full_path,string a_text)
		{
			string t_full_path = a_full_path;

			try{
				using(System.IO.StreamWriter t_stream = new System.IO.StreamWriter(t_full_path,false,System.Text.Encoding.UTF8)){
					t_stream.Write(a_text);
					t_stream.Flush();
					t_stream.Close();
				}

				UnityEditor.AssetDatabase.Refresh();
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
		}
	}
	#endif
}

