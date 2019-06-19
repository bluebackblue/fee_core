

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief エディターツール。
*/


/** Fee.EditorTool
*/
namespace Fee.EditorTool
{
	/** Utility
	*/
	#if(UNITY_EDITOR)
	public class Utility
	{
		/** ディレクトリ内のファイルを列挙。

			a_dir_name == "xxx/" : "Assets/xxx/"

		*/
		public static System.Collections.Generic.List<string> GetFileNameList(string a_dir_name)
		{
			System.Collections.Generic.List<string> t_list = new System.Collections.Generic.List<string>();
			{
				string[] t_fullpath_list = System.IO.Directory.GetFiles(Fee.File.Path.CreateAssetsPath().GetPath(),a_dir_name,System.IO.SearchOption.TopDirectoryOnly);
				for(int ii=0;ii<t_fullpath_list.Length;ii++){
					string t_filename = System.IO.Path.GetFileName(t_fullpath_list[ii]);
					if(t_filename.Length > 0){
						t_list.Add(t_filename);
					}
				}
			}
			return t_list;
		}

		/** ディレクトリ内のディレクトリを列挙。

			a_dir_name == "xxx/" : "Assets/xxx/"

		*/
		public static System.Collections.Generic.List<string> GetDirectoryNameList(string a_dir_name)
		{
			System.Collections.Generic.List<string> t_list = new System.Collections.Generic.List<string>();
			{
				string[] t_fullpath_list = System.IO.Directory.GetDirectories(Fee.File.Path.CreateAssetsPath().GetPath(),a_dir_name,System.IO.SearchOption.TopDirectoryOnly);
				for(int ii=0;ii<t_fullpath_list.Length;ii++){
					string t_filename = System.IO.Path.GetFileName(t_fullpath_list[ii]);
					if(t_filename.Length > 0){
						t_list.Add(t_filename);
					}
				}
			}
			return t_list;
		}

		/** ファイル検索。

			return == フルパス。

		*/
		public static Fee.File.Path FindFile(string a_dir_name,string a_file_name)
		{
			string[] t_dir_list = System.IO.Directory.GetDirectories(Fee.File.Path.CreateAssetsPath().GetPath(),a_dir_name,System.IO.SearchOption.AllDirectories);
			for(int ii=0;ii<t_dir_list.Length;ii++){
				string[] t_file_list = System.IO.Directory.GetFiles(t_dir_list[ii],a_file_name,System.IO.SearchOption.TopDirectoryOnly);
				if(t_file_list != null){
					if(t_file_list.Length > 0){
						return new Fee.File.Path(t_file_list[0]);
					}
				}
			}
			return null;
		}

		/** ファイル存在チェック。

			a_path : フルパス。

		*/
		public static bool IsExistFile(Fee.File.Path a_path)
		{
			return System.IO.File.Exists(a_path.GetPath());
		}

		/** テキストファイル読み込み。

			a_path : フルパス。

		*/
		public static string ReadTextFile(Fee.File.Path a_path)
		{
			string t_string = null;
			try{
				using(System.IO.StreamReader t_stream = new System.IO.StreamReader(a_path.GetPath())){
					t_string = t_stream.ReadToEnd();
					t_stream.Close();
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
			return t_string;
		}

		/** テキストファイル書き込み。

			a_path : フルパス。

		*/
		public static void WriteTextFile(Fee.File.Path a_path,string a_text)
		{
			try{
				using(System.IO.StreamWriter t_stream = new System.IO.StreamWriter(a_path.GetPath(),false,System.Text.Encoding.UTF8)){
					t_stream.Write(a_text);
					t_stream.Flush();
					t_stream.Close();
				}

				UnityEditor.AssetDatabase.Refresh();
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
		}

		/** ＪＳＯＮファイル書き込み。

			a_path : フルパス。

		*/
		public static void WriteJsonFile(Fee.File.Path a_path,Fee.JsonItem.JsonItem a_jsonitem)
		{
			string t_json_string = a_jsonitem.ConvertJsonString();

			try{
				using(System.IO.StreamWriter t_steram = new System.IO.StreamWriter(a_path.GetPath(),false,System.Text.Encoding.UTF8)){
					t_steram.Write(t_json_string);
					t_steram.Flush();
					t_steram.Close();
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
		}

		/** ゲームオブジェクト。検索。アクティブシーンのルート直下。
		*/
		public static UnityEngine.GameObject FindGameObjectFromActiveSceneRoot(string a_gameobject_name)
		{
			UnityEngine.GameObject[] t_gameobject = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
			for(int ii=0;ii<t_gameobject.Length;ii++){
				if(t_gameobject[ii].name == a_gameobject_name){
					return t_gameobject[ii];
				}
			}
			return null;
		}
	}
	#endif
}

