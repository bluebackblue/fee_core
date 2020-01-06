

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
		/** FindFeePath

			return : アセットフォルダからの相対パス。

		*/
		public static Fee.File.Path FindFeePath()
		{
			Fee.File.Path t_path = null;

			try{
				Fee.File.Path t_directory = Fee.EditorTool.Utility.FindFile(new File.Path(),"fee_buildtarget");
				string t_directory_name = System.IO.Path.GetDirectoryName(System.IO.Path.GetFullPath(t_directory.GetDirectoryPath() + "/"));
				string t_root_directory_name = System.IO.Path.GetFullPath(Fee.File.Path.CreateAssetsPath().GetPath());
				t_directory_name = t_directory_name.Substring(t_root_directory_name.Length + 1);

				UnityEngine.Debug.Log("FindFeePath : " + t_directory_name);

				t_path = new File.Path(t_directory_name);
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}

			return t_path;
		}

		/** SavePrefab

			a_assets_path	: アセットフォルダからの相対パス。

		*/
		public static void SavePrefab(UnityEngine.GameObject a_prefab,Fee.File.Path a_assets_path)
		{
			try{
				string t_path = "Assets/" + a_assets_path.GetPath();

				bool t_ret;
				UnityEditor.PrefabUtility.SaveAsPrefabAsset(a_prefab,t_path,out t_ret);
				if(t_ret == false){
					UnityEngine.Debug.LogError("SavePrefab : error");
				}else{
					UnityEngine.Debug.Log("SavePrefab : " + t_path);
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
		}

		/** ExportPackage

			a_assets_path	: アセットフォルダからの相対パス。

		*/
		public static void ExportPackage(Fee.File.Path a_asset_path,string a_package_name,UnityEditor.ExportPackageOptions a_option)
		{
			try{
				string t_path = "Assets/" + a_asset_path.GetNormalizePath();

				UnityEngine.Debug.Log("ExportPackage : " + t_path + " : " + a_package_name);
				UnityEditor.AssetDatabase.ExportPackage(t_path,a_package_name,a_option);
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
		}

		/** ファイルを列挙。

			a_assets_path	: アセットフォルダからの相対パス。

		*/
		public static System.Collections.Generic.List<string> CreateFileNameList(Fee.File.Path a_assets_path)
		{
			System.Collections.Generic.List<string> t_list = new System.Collections.Generic.List<string>();
			{
				try{
					string[] t_fullpath_list = System.IO.Directory.GetFiles(Fee.File.Path.CreateAssetsPath().GetPath(),a_assets_path.GetNormalizePath() + "/",System.IO.SearchOption.TopDirectoryOnly);
					for(int ii=0;ii<t_fullpath_list.Length;ii++){
						string t_name = System.IO.Path.GetFileName(t_fullpath_list[ii]);
						if(t_name.Length > 0){
							t_list.Add(t_name);
						}
					}
				}catch(System.Exception t_exception){
					UnityEngine.Debug.LogError(t_exception.Message);
				}
			}
			return t_list;
		}

		/** ディレクトリを列挙。

			a_assets_path	: アセットフォルダからの相対パス。

		*/
		public static System.Collections.Generic.List<string> CreateDirectoryNameList(Fee.File.Path a_assets_path)
		{
			System.Collections.Generic.List<string> t_list = new System.Collections.Generic.List<string>();
			{
				string[] t_fullpath_list = System.IO.Directory.GetDirectories(Fee.File.Path.CreateAssetsPath().GetPath(),a_assets_path.GetNormalizePath() + "/",System.IO.SearchOption.TopDirectoryOnly);
				for(int ii=0;ii<t_fullpath_list.Length;ii++){
					string t_name = System.IO.Path.GetFileName(t_fullpath_list[ii]);
					if(t_name.Length > 0){
						t_list.Add(t_name);
					}
				}
			}
			return t_list;
		}

		/** アセットをロード。

			a_assets_path	: アセットフォルダからの相対パス。

		*/
		public static UnityEngine.Object[] LoadAllAsset(Fee.File.Path a_assets_path)
		{
			UnityEngine.Object[] t_object_list = null;

			try{
				t_object_list = UnityEditor.AssetDatabase.LoadAllAssetsAtPath("Assets/" + a_assets_path.GetPath());
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
				t_object_list = null;
			}

			return t_object_list;
		}

		/** アセットをロード。

			a_assets_path	: アセットフォルダからの相対パス。

		*/
		public static T LoadAsset< T >(Fee.File.Path a_assets_path)
			where T : UnityEngine.Object
		{
			T t_object;

			try{
				t_object = UnityEditor.AssetDatabase.LoadAssetAtPath< T >("Assets/" + a_assets_path.GetPath());
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
				t_object = null;
			}

			return t_object;
		}

		/** ファイル存在チェック。

			a_full_path : フルパス。

		*/
		public static bool IsExistFile(Fee.File.Path a_full_path)
		{
			bool t_ret = false;

			try{
				t_ret = System.IO.File.Exists(a_full_path.GetPath());
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
				t_ret = false;
			}

			return t_ret;
		}

		/** ファイル検索。

			return == フルパス。

		*/
		public static Fee.File.Path FindFile(Fee.File.Path a_assets_path,string a_find_name)
		{
			string[] t_dir_list = System.IO.Directory.GetDirectories(Fee.File.Path.CreateAssetsPath().GetPath(),a_assets_path.GetNormalizePath() + "/",System.IO.SearchOption.AllDirectories);
			for(int ii=0;ii<t_dir_list.Length;ii++){
				string[] t_file_list = System.IO.Directory.GetFiles(t_dir_list[ii],a_find_name,System.IO.SearchOption.TopDirectoryOnly);
				if(t_file_list != null){
					if(t_file_list.Length > 0){
						UnityEngine.Debug.Log("FindFile : " + a_find_name + " : " + t_file_list[0]);
						return new Fee.File.Path(t_file_list[0]);
					}
				}
			}
			return null;
		}

		/** テキストファイル書き込み。

			a_path : フルパス。

		*/
		public static void WriteTextFile(Fee.File.Path a_full_path,string a_text,bool a_refresh_unity)
		{
			try{
				using(System.IO.StreamWriter t_stream = new System.IO.StreamWriter(a_full_path.GetPath(),false,System.Text.Encoding.UTF8)){
					t_stream.Write(a_text);
					t_stream.Flush();
					t_stream.Close();
				}

				UnityEngine.Debug.Log("WriteTextFile : " + a_full_path.GetPath());

				if(a_refresh_unity == true){
					UnityEditor.AssetDatabase.Refresh();
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
		}

		/** ＪＳＯＮファイル書き込み。

			a_full_path : フルパス。

		*/
		public static void WriteJsonFile(Fee.File.Path a_full_path,Fee.JsonItem.JsonItem a_jsonitem,bool a_refresh)
		{
			try{
				string t_json_string = a_jsonitem.ConvertJsonString();

				using(System.IO.StreamWriter t_steram = new System.IO.StreamWriter(a_full_path.GetPath(),false,System.Text.Encoding.UTF8)){
					t_steram.Write(t_json_string);
					t_steram.Flush();
					t_steram.Close();
				}

				UnityEngine.Debug.Log("WriteJsonFile : " + a_full_path.GetPath());

				if(a_refresh == true){
					UnityEditor.AssetDatabase.Refresh();
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
		}

		/** テキストファイル読み込み。

			a_path : フルパス。

		*/
		public static string ReadTextFile(Fee.File.Path a_full_path)
		{
			string t_string = null;

			try{
				using(System.IO.StreamReader t_stream = new System.IO.StreamReader(a_full_path.GetPath())){
					t_string = t_stream.ReadToEnd();
					t_stream.Close();
				}

				UnityEngine.Debug.Log("ReadTextFile : " + a_full_path.GetPath());

			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}

			return t_string;
		}

		/** バイナリファイル書き込み。

			a_full_path : フルパス。

		*/
		public static void WriteBinaryFile(Fee.File.Path a_full_path,byte[] a_binary,bool a_refresh)
		{
			try{
				using(System.IO.BinaryWriter t_stream = new System.IO.BinaryWriter(System.IO.File.Open(a_full_path.GetPath(),System.IO.FileMode.Create))){
					t_stream.Write(a_binary);
					t_stream.Flush();
					t_stream.Close();
				}

				UnityEngine.Debug.Log("WriteBinaryFile : " + a_full_path.GetPath());

				if(a_refresh == true){
					UnityEditor.AssetDatabase.Refresh();
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
		}

		/** テクスチャーからテクスチャー作成。
		*/
		public static UnityEngine.Texture2D CreateTextureFromTexture(UnityEngine.Texture2D a_texture,int a_offset_x,int a_offset_y,int a_size_w,int a_size_h)
		{
			UnityEngine.Texture2D t_new_texture = null;

			try{
				UnityEngine.Color[] t_color_list = a_texture.GetPixels(a_offset_x,a_texture.height - a_offset_y - a_size_h,a_size_w,a_size_h);

				t_new_texture = new UnityEngine.Texture2D(a_size_w,a_size_h,UnityEngine.TextureFormat.RGBA32,false);
				t_new_texture.SetPixels(t_color_list);
				t_new_texture.Apply();
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}

			return t_new_texture;
		}
	}
	#endif
}

