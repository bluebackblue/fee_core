

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief エディターツール。
*/


/** Fee.EditorTool
*/
#if(UNITY_EDITOR)
namespace Fee.EditorTool
{
	/** AssetTool
	*/
	public class AssetTool
	{
		/** Refresh
		*/
		public static void Refresh()
		{
			try{
				UnityEditor.AssetDatabase.Refresh();
			}catch(System.Exception t_exception){
				Tool.EditorLogError(t_exception.Message);
			}
		}

		/** アセットをロード。
		*/
		public static UnityEngine.Object[] LoadAllAsset(Fee.File.Path a_assets_path)
		{
			UnityEngine.Object[] t_object_list = null;

			try{
				t_object_list = UnityEditor.AssetDatabase.LoadAllAssetsAtPath("Assets/" + a_assets_path.GetPath());
			}catch(System.Exception t_exception){
				Tool.EditorLogError(t_exception.Message);
				t_object_list = null;
			}

			return t_object_list;
		}

		/** アセットをロード。
		*/
		public static T LoadAsset< T >(Fee.File.Path a_assets_path)
			where T : UnityEngine.Object
		{
			T t_object;

			try{
				t_object = UnityEditor.AssetDatabase.LoadAssetAtPath< T >("Assets/" + a_assets_path.GetPath());
			}catch(System.Exception t_exception){
				Tool.EditorLogError(t_exception.Message);
				t_object = null;
			}

			return t_object;
		}

		/** ExportPackage
		*/
		public static void ExportPackage(Fee.File.Path a_assets_path,string a_package_name,UnityEditor.ExportPackageOptions a_option)
		{
			try{
				string t_path = "Assets/" + a_assets_path.GetPathCutLastSeparator();

				Tool.EditorLog("ExportPackage : " + t_path + " : " + a_package_name);
				UnityEditor.AssetDatabase.ExportPackage(t_path,a_package_name,a_option);
			}catch(System.Exception t_exception){
				Tool.EditorLogError(t_exception.Message);
			}
		}

		/** アセットバンドル。作成。
		*/
		public static void BuildAssetBundles(Fee.File.Path a_path,UnityEditor.AssetBundleBuild[] a_list,UnityEditor.BuildAssetBundleOptions a_option,UnityEditor.BuildTarget a_buildtarget)
		{
			UnityEditor.BuildPipeline.BuildAssetBundles("Assets/" + a_path.GetPath(),a_list,a_option,a_buildtarget);
		}

		/** シーンを開く。
		*/
		public static void OpenScene(Fee.File.Path a_path)
		{
			try{
				UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/" + a_path.GetPath(),UnityEditor.SceneManagement.OpenSceneMode.Single);
			}catch(System.Exception t_exception){
				Tool.EditorLogError(t_exception.Message);
			}
		}

		/** SavePrefab
		*/
		public static void SavePrefab(UnityEngine.GameObject a_prefab,Fee.File.Path a_assets_path)
		{
			try{
				string t_path = "Assets/" + a_assets_path.GetPathCutLastSeparator();

				bool t_ret;
				UnityEditor.PrefabUtility.SaveAsPrefabAsset(a_prefab,t_path,out t_ret);
				if(t_ret == false){
					Tool.EditorLogError("SavePrefab : error");
				}else{
					Tool.EditorLog("SavePrefab : " + t_path);
				}
			}catch(System.Exception t_exception){
				Tool.EditorLogError(t_exception.Message);
			}
		}

		/** SaveAnimationClip
		*/
		public static void SaveAnimationClip(UnityEngine.AnimationClip a_animation_clip,Fee.File.Path a_assets_path)
		{
			try{
				string t_path = "Assets/" + a_assets_path.GetPath();

				UnityEngine.AnimationClip t_new = UnityEngine.Object.Instantiate<UnityEngine.AnimationClip>(a_animation_clip);
				UnityEditor.AssetDatabase.CreateAsset(t_new,t_path);
			}catch(System.Exception t_exception){
				Tool.EditorLogError(t_exception.Message);
			}
		}

		/** LoadMeshFromFbx
		*/
		public static System.Collections.Generic.List<UnityEngine.Mesh> LoadMeshFromFbxFile(Fee.File.Path a_assets_path)
		{
			System.Collections.Generic.List<UnityEngine.Mesh> t_list = new System.Collections.Generic.List<UnityEngine.Mesh>();

			try{
				UnityEngine.Object[] t_object_list = UnityEditor.AssetDatabase.LoadAllAssetsAtPath("Assets/" + a_assets_path.GetPath());
				if(t_object_list != null){
					for(int ii=0;ii<t_object_list.Length;ii++){
						if(t_object_list[ii].GetType() == typeof(UnityEngine.Mesh)){
							UnityEngine.Mesh t_load_asset = t_object_list[ii] as UnityEngine.Mesh;
							if(t_load_asset != null){
								t_list.Add(t_load_asset);
							}
						}
					}
				}
			}catch(System.Exception t_exception){
				Tool.EditorLogError(t_exception.Message);
			}

			return t_list;
		}

		/** SaveMesh
		*/
		public static void SaveMesh(UnityEngine.Mesh a_mesh,Fee.File.Path a_assets_path)
		{
			try{
				UnityEngine.Mesh t_new = UnityEngine.Object.Instantiate<UnityEngine.Mesh>(a_mesh);
				UnityEditor.AssetDatabase.CreateAsset(t_new,"Assets/" + a_assets_path.GetPath());
			}catch(System.Exception t_exception){
				Tool.EditorLogError(t_exception.Message);
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
				Tool.EditorLogError(t_exception.Message);
			}

			return t_new_texture;
		}

		/** テキストファイル書き込み。
		*/
		public static void WriteTextFile(Fee.File.Path a_assets_path,string a_text)
		{
			try{
				using(System.IO.StreamWriter t_stream = new System.IO.StreamWriter(Fee.File.Path.CreateAssetsPath(a_assets_path,Fee.File.Path.SEPARATOR).GetPath(),false,new System.Text.UTF8Encoding(false))){
					t_stream.Write(a_text);
					t_stream.Flush();
					t_stream.Close();
				}

				Tool.EditorLog("WriteTextFile : " + a_assets_path.GetPath());
			}catch(System.Exception t_exception){
				Tool.EditorLogError(t_exception.Message);
			}
		}

		/** ＪＳＯＮファイル書き込み。
		*/
		public static void WriteJsonFile(Fee.File.Path a_assets_path,Fee.JsonItem.JsonItem a_jsonitem)
		{
			try{
				string t_json_string = a_jsonitem.ConvertToJsonString();

				using(System.IO.StreamWriter t_steram = new System.IO.StreamWriter(Fee.File.Path.CreateAssetsPath(a_assets_path,Fee.File.Path.SEPARATOR).GetPath(),false,new System.Text.UTF8Encoding(false))){
					t_steram.Write(t_json_string);
					t_steram.Flush();
					t_steram.Close();
				}

				Tool.EditorLog("WriteJsonFile : " + a_assets_path.GetPath());
			}catch(System.Exception t_exception){
				Tool.EditorLogError(t_exception.Message);
			}
		}

		/** バイナリファイル書き込み。
		*/
		public static void WriteBinaryFile(Fee.File.Path a_assets_path,byte[] a_binary)
		{
			try{
				using(System.IO.BinaryWriter t_stream = new System.IO.BinaryWriter(System.IO.File.Open(Fee.File.Path.CreateAssetsPath(a_assets_path,Fee.File.Path.SEPARATOR).GetPath(),System.IO.FileMode.Create))){
					t_stream.Write(a_binary);
					t_stream.Flush();
					t_stream.Close();
				}

				Tool.EditorLog("WriteBinaryFile : " + a_assets_path.GetPath());
			}catch(System.Exception t_exception){
				Tool.EditorLogError(t_exception.Message);
			}
		}

		/** テキストファイル読み込み。
		*/
		public static string ReadTextFile(Fee.File.Path a_assets_path)
		{
			string t_string = null;

			try{
				using(System.IO.StreamReader t_stream = new System.IO.StreamReader(Fee.File.Path.CreateAssetsPath(a_assets_path,Fee.File.Path.SEPARATOR).GetPath())){
					t_string = t_stream.ReadToEnd();
					t_stream.Close();
				}
				
				Tool.EditorLog("ReadTextFile : " + a_assets_path.GetPath());
			}catch(System.Exception t_exception){
				Tool.EditorLogError(t_exception.Message);
			}

			return t_string;
		}

		/** ファイル存在チェック。
		*/
		public static bool IsExistFile(Fee.File.Path a_assets_path)
		{
			bool t_ret = false;

			try{
				t_ret = System.IO.File.Exists(Fee.File.Path.CreateAssetsPath(a_assets_path,Fee.File.Path.SEPARATOR).GetPath());
			}catch(System.Exception t_exception){
				Tool.EditorLogError(t_exception.Message);
				t_ret = false;
			}

			return t_ret;
		}

		/** ディレクトリ。作成。
		*/
		public static void CreateDirectory(Fee.File.Path a_assets_path)
		{
			try{
				if(System.IO.Directory.Exists(Fee.File.Path.CreateAssetsPath(a_assets_path,Fee.File.Path.SEPARATOR).GetPath()) == false){
					System.IO.Directory.CreateDirectory(Fee.File.Path.CreateAssetsPath(a_assets_path,Fee.File.Path.SEPARATOR).GetPath());
				}
			}catch(System.Exception t_exception){
				Tool.EditorLogError(t_exception.Message);
			}
		}

		/** ディレクトリ。削除。
		*/
		public static void DeleteDirectory(Fee.File.Path a_assets_path)
		{
			try{
				if(System.IO.Directory.Exists(Fee.File.Path.CreateAssetsPath(a_assets_path,Fee.File.Path.SEPARATOR).GetPath()) == true){
					System.IO.Directory.Delete(Fee.File.Path.CreateAssetsPath(a_assets_path,Fee.File.Path.SEPARATOR).GetPath(),true);
				}
			}catch(System.Exception t_exception){
				Tool.EditorLogError(t_exception.Message);
			}
		}

		/** ファイル。削除。
		*/
		public static void DeleteFile(Fee.File.Path a_assets_path)
		{
			try{
				if(System.IO.File.Exists(Fee.File.Path.CreateAssetsPath(a_assets_path,Fee.File.Path.SEPARATOR).GetPath()) == true){
					System.IO.File.Delete(Fee.File.Path.CreateAssetsPath(a_assets_path,Fee.File.Path.SEPARATOR).GetPath());
				}
			}catch(System.Exception t_exception){
				Tool.EditorLogError(t_exception.Message);
			}
		}

		/** 指定パス直下のディレクトリ名を列挙。

			直下のみ。

		*/
		public static System.Collections.Generic.List<string> CreateDirectoryNameList(Fee.File.Path a_assets_path)
		{
			System.Collections.Generic.List<string> t_list = new System.Collections.Generic.List<string>();
			{
				string[] t_fullpath_list = System.IO.Directory.GetDirectories(Fee.File.Path.CreateAssetsPath(a_assets_path,Fee.File.Path.SEPARATOR).GetPath(),"*",System.IO.SearchOption.TopDirectoryOnly);
				for(int ii=0;ii<t_fullpath_list.Length;ii++){
					string t_name = System.IO.Path.GetFileName(t_fullpath_list[ii]);
					if(t_name.Length > 0){
						t_list.Add(t_name);
					}
				}
			}
			return t_list;
		}

		/** 指定パス直下のファイル名を列挙。

			直下のみ。

		*/
		public static System.Collections.Generic.List<string> CreateFileNameList(Fee.File.Path a_assets_path)
		{
			System.Collections.Generic.List<string> t_list = new System.Collections.Generic.List<string>();
			{
				try{
					string[] t_fullpath_list = System.IO.Directory.GetFiles(Fee.File.Path.CreateAssetsPath(a_assets_path,Fee.File.Path.SEPARATOR).GetPath(),"*",System.IO.SearchOption.TopDirectoryOnly);
					for(int ii=0;ii<t_fullpath_list.Length;ii++){
						string t_name = System.IO.Path.GetFileName(t_fullpath_list[ii]);
						if(t_name.Length > 0){
							t_list.Add(t_name);
						}
					}
				}catch(System.Exception t_exception){
					Tool.EditorLogError(t_exception.Message);
				}
			}
			return t_list;
		}

		/** すべてのディレクトリの相対パスを列挙。

			再帰処理。

		*/
		public static System.Collections.Generic.List<Fee.File.Path> CreateAllDirectoryPathList(Fee.File.Path a_assets_path)
		{
			System.Collections.Generic.List<Fee.File.Path> t_list = new System.Collections.Generic.List<Fee.File.Path>();
			System.Collections.Generic.List<Fee.File.Path> t_work = new System.Collections.Generic.List<Fee.File.Path>();

			//自分を列挙。
			{
				t_list.Add(a_assets_path);
			}

			{
				t_work.Add(a_assets_path);
				while(t_work.Count > 0){
					Fee.File.Path t_path = t_work[t_work.Count - 1];
					t_work.RemoveAt(t_work.Count - 1);

					System.Collections.Generic.List<string> t_directory_name_list = CreateDirectoryNameList(t_path);
					for(int ii=0;ii<t_directory_name_list.Count;ii++){
						Fee.File.Path t_new_path = new File.Path(t_path,t_directory_name_list[ii],Fee.File.Path.SEPARATOR);
						t_list.Add(t_new_path);
						t_work.Add(t_new_path);
					}
				}
			}
			return t_list;
		}

		/** すべてのファイルの相対パスを列挙。

			再帰処理。

		*/
		public static System.Collections.Generic.List<Fee.File.Path> CreateAllFilePathList(Fee.File.Path a_assets_path)
		{
			System.Collections.Generic.List<Fee.File.Path> t_list = new System.Collections.Generic.List<Fee.File.Path>();
			{
				System.Collections.Generic.List<Fee.File.Path> t_directory_list = CreateAllDirectoryPathList(a_assets_path);
				foreach(Fee.File.Path t_path in t_directory_list){
					
					System.Collections.Generic.List<string> t_file_name_list = CreateFileNameList(t_path);
					for(int ii=0;ii<t_file_name_list.Count;ii++){
						t_list.Add(new Fee.File.Path(t_path,t_file_name_list[ii],Fee.File.Path.SEPARATOR));
					}
				}
			}
			return t_list;
		}

		/** FindFile
		*/
		public static Fee.File.Path FindFile(Fee.File.Path a_assets_path,Fee.File.Path a_file_path)
		{
			System.Collections.Generic.List<Fee.File.Path> t_list = CreateAllFilePathList(a_assets_path);
			for(int ii=0;ii<t_list.Count;ii++){
				if(t_list[ii].GetFileName() == a_file_path.GetFileName()){
					return t_list[ii];
				}
			}
			return null;
		}

	}
}
#endif

