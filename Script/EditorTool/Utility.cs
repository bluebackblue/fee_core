

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
		*/
		public static Fee.File.Path FindFeePath()
		{
			return Fee.EditorTool.Utility.FindFile(new File.Path(),new File.Path("fee_buildtarget")).CreateFileNameChangePath("",Fee.File.Path.SEPARATOR);
		}

		/** Refresh
		*/
		public static void Refresh()
		{
			try{
				UnityEditor.AssetDatabase.Refresh();
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
		}

		/** CreateDirectory
		*/
		public static void CreateDirectory(Fee.File.Path a_assets_path,bool a_refresh_unity)
		{
			try{
				System.IO.Directory.CreateDirectory(Fee.File.Path.CreateAssetsPath(a_assets_path,Fee.File.Path.SEPARATOR).GetPath());

				if(a_refresh_unity == true){
					UnityEditor.AssetDatabase.Refresh();
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
		}

		/** SavePrefab
		*/
		public static void SavePrefab(UnityEngine.GameObject a_prefab,Fee.File.Path a_assets_path,bool a_refresh_unity)
		{
			try{
				string t_path = "Assets/" + a_assets_path.GetPathCutLastSeparator();

				bool t_ret;
				UnityEditor.PrefabUtility.SaveAsPrefabAsset(a_prefab,t_path,out t_ret);
				if(t_ret == false){
					UnityEngine.Debug.LogError("SavePrefab : error");
				}else{
					UnityEngine.Debug.Log("SavePrefab : " + t_path);
				}

				if(a_refresh_unity == true){
					UnityEditor.AssetDatabase.Refresh();
				}

			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
		}

		/** SaveAnimationClip
		*/
		public static void SaveAnimationClip(UnityEngine.AnimationClip a_animation_clip,Fee.File.Path a_assets_path,bool a_refresh_unity)
		{
			try{
				UnityEngine.AnimationClip t_new = UnityEngine.Object.Instantiate<UnityEngine.AnimationClip>(a_animation_clip);
				UnityEditor.AssetDatabase.CreateAsset(t_new,"Assets/" + a_assets_path.GetPath());
				if(a_refresh_unity == true){
					UnityEditor.AssetDatabase.Refresh();
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
		}

		/** ExportPackage
		*/
		public static void ExportPackage(Fee.File.Path a_assets_path,string a_package_name,UnityEditor.ExportPackageOptions a_option)
		{
			try{
				string t_path = "Assets/" + a_assets_path.GetPathCutLastSeparator();

				UnityEngine.Debug.Log("ExportPackage : " + t_path + " : " + a_package_name);
				UnityEditor.AssetDatabase.ExportPackage(t_path,a_package_name,a_option);
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
		}

		/** 指定パス直下のファイル名を列挙。
		*/
		public static System.Collections.Generic.List<string> CreateFileNameList(Fee.File.Path a_assets_path)
		{
			System.Collections.Generic.List<string> t_list = new System.Collections.Generic.List<string>();
			{
				try{
					string t_pattern = a_assets_path.GetPathCutLastSeparator();
					if(t_pattern.Length == 0){
						t_pattern = "*";
					}else{
						t_pattern += "/*";
					}

					string[] t_fullpath_list = System.IO.Directory.GetFiles(Fee.File.Path.CreateAssetsPath().GetPath(),t_pattern,System.IO.SearchOption.TopDirectoryOnly);
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

		/** 指定パス直下のディレクトリ名を列挙。
		*/
		public static System.Collections.Generic.List<string> CreateDirectoryNameList(Fee.File.Path a_assets_path)
		{
			System.Collections.Generic.List<string> t_list = new System.Collections.Generic.List<string>();
			{
				string t_pattern = a_assets_path.GetPathCutLastSeparator();
				if(t_pattern.Length == 0){
					t_pattern = "*";
				}else{
					t_pattern += "/*";
				}

				string[] t_fullpath_list = System.IO.Directory.GetDirectories(Fee.File.Path.CreateAssetsPath().GetPath(),t_pattern,System.IO.SearchOption.TopDirectoryOnly);
				for(int ii=0;ii<t_fullpath_list.Length;ii++){
					string t_name = System.IO.Path.GetFileName(t_fullpath_list[ii]);
					if(t_name.Length > 0){
						t_list.Add(t_name);
					}
				}
			}
			return t_list;
		}

		/** すべてのディレクトリの相対パスを列挙。
		*/
		public static System.Collections.Generic.List<Fee.File.Path> CreateAllDirectoryRelativePathList(Fee.File.Path a_assets_path)
		{
			System.Collections.Generic.List<Fee.File.Path> t_list = new System.Collections.Generic.List<Fee.File.Path>();
			System.Collections.Generic.List<Fee.File.Path> t_work = new System.Collections.Generic.List<Fee.File.Path>();
			{
				t_work.Add(a_assets_path);
				while(t_work.Count > 0){
					Fee.File.Path t_path = t_work[t_work.Count - 1];
					t_work.RemoveAt(t_work.Count - 1);

					string t_pattern = t_path.GetPathCutLastSeparator();
					if(t_pattern.Length == 0){
						t_pattern = "*";
					}else{
						t_pattern += "/*";
					}

					string[] t_fullpath_list = System.IO.Directory.GetDirectories(Fee.File.Path.CreateAssetsPath().GetPath(),t_pattern,System.IO.SearchOption.TopDirectoryOnly);

					for(int ii=0;ii<t_fullpath_list.Length;ii++){
						string t_name = System.IO.Path.GetFileName(t_fullpath_list[ii]);
						if(t_name.Length > 0){
							Fee.File.Path t_new_path = new File.Path(t_path,t_name,Fee.File.Path.SEPARATOR);
							t_list.Add(t_new_path);
							t_work.Add(t_new_path);
						}
					}
				}
			}
			return t_list;
		}

		/** すべてのファイルの相対パスを列挙。
		*/
		public static System.Collections.Generic.List<Fee.File.Path> CreateAllFileRelativePathList(Fee.File.Path a_assets_path)
		{
			System.Collections.Generic.List<Fee.File.Path> t_list = new System.Collections.Generic.List<Fee.File.Path>();
			{
				System.Collections.Generic.List<Fee.File.Path> t_directory_list = CreateAllDirectoryRelativePathList(a_assets_path);
				foreach(Fee.File.Path t_path in t_directory_list){
					
					string t_pattern = t_path.GetPathCutLastSeparator();
					if(t_pattern.Length == 0){
						t_pattern = "*";
					}else{
						t_pattern += "/*";
					}
					
					string[] t_fullpath_list = System.IO.Directory.GetFiles(Fee.File.Path.CreateAssetsPath().GetPath(),t_pattern,System.IO.SearchOption.TopDirectoryOnly);

					for(int ii=0;ii<t_fullpath_list.Length;ii++){
						string t_name = System.IO.Path.GetFileName(t_fullpath_list[ii]);
						if(t_name.Length > 0){
							t_list.Add(new Fee.File.Path(t_path,t_name,Fee.File.Path.SEPARATOR));
						}
					}
				}
			}
			return t_list;
		}

		/** アセットをロード。
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
		*/
		public static bool IsExistFile(Fee.File.Path a_assets_path)
		{
			bool t_ret = false;

			try{
				t_ret = System.IO.File.Exists(Fee.File.Path.CreateAssetsPath(a_assets_path,Fee.File.Path.SEPARATOR).GetPath());
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
				t_ret = false;
			}

			return t_ret;
		}

		/** FindFile
		*/
		public static Fee.File.Path FindFile(Fee.File.Path a_assets_path,Fee.File.Path a_path)
		{
			System.Collections.Generic.List<Fee.File.Path> t_list = CreateAllFileRelativePathList(a_assets_path);
			for(int ii=0;ii<t_list.Count;ii++){
				if(t_list[ii].GetFileName() == a_path.GetFileName()){
					return t_list[ii];
				}
			}
			return null;
		}

		/** テキストファイル書き込み。
		*/
		public static void WriteTextFile(Fee.File.Path a_assets_path,string a_text,bool a_refresh_unity)
		{
			try{
				using(System.IO.StreamWriter t_stream = new System.IO.StreamWriter(Fee.File.Path.CreateAssetsPath(a_assets_path,Fee.File.Path.SEPARATOR).GetPath(),false,new System.Text.UTF8Encoding(false))){
					t_stream.Write(a_text);
					t_stream.Flush();
					t_stream.Close();
				}

				UnityEngine.Debug.Log("WriteTextFile : " + a_assets_path.GetPath());

				if(a_refresh_unity == true){
					UnityEditor.AssetDatabase.Refresh();
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
		}

		/** ＪＳＯＮファイル書き込み。
		*/
		public static void WriteJsonFile(Fee.File.Path a_assets_path,Fee.JsonItem.JsonItem a_jsonitem,bool a_refresh)
		{
			try{
				string t_json_string = a_jsonitem.ConvertToJsonString();

				using(System.IO.StreamWriter t_steram = new System.IO.StreamWriter(Fee.File.Path.CreateAssetsPath(a_assets_path,Fee.File.Path.SEPARATOR).GetPath(),false,new System.Text.UTF8Encoding(false))){
					t_steram.Write(t_json_string);
					t_steram.Flush();
					t_steram.Close();
				}

				UnityEngine.Debug.Log("WriteJsonFile : " + a_assets_path.GetPath());

				if(a_refresh == true){
					UnityEditor.AssetDatabase.Refresh();
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
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

				UnityEngine.Debug.Log("ReadTextFile : " + a_assets_path.GetPath());

			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}

			return t_string;
		}

		/** バイナリファイル書き込み。
		*/
		public static void WriteBinaryFile(Fee.File.Path a_assets_path,byte[] a_binary,bool a_refresh)
		{
			try{
				using(System.IO.BinaryWriter t_stream = new System.IO.BinaryWriter(System.IO.File.Open(Fee.File.Path.CreateAssetsPath(a_assets_path,Fee.File.Path.SEPARATOR).GetPath(),System.IO.FileMode.Create))){
					t_stream.Write(a_binary);
					t_stream.Flush();
					t_stream.Close();
				}

				UnityEngine.Debug.Log("WriteBinaryFile : " + a_assets_path.GetPath());

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

		/** ＷＥＢリクエスト作成。
		*/
		public static UnityEngine.Networking.UnityWebRequest CreateWebRequest(Fee.File.Path a_uri_path,UnityEngine.Networking.CertificateHandler a_certificate)
		{
			UnityEngine.Networking.UnityWebRequest t_webrequest = null;

			try{
				t_webrequest = UnityEngine.Networking.UnityWebRequest.Get(a_uri_path.GetPath());

				if(a_certificate != null){
					t_webrequest.certificateHandler = a_certificate;
				}

				UnityEngine.Networking.UnityWebRequestAsyncOperation t_async = t_webrequest.SendWebRequest();

				while(t_async.isDone == false){
					 System.Threading.Thread.Sleep(100);
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
				t_webrequest = null;
			}

			return t_webrequest;
		}
	}
	#endif
}

