

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。パス。
*/


/** Unreachable code detected.
*/
#pragma warning disable 0162


/** Fee.File
*/
namespace Fee.File
{
	/** Path
	*/
	public class Path
	{
		/** path
		*/
		private string path;

		/** SEPARATOR
		*/
		public const string SEPARATOR = "/";

		/** GetDirectoryPathString

			最後のセパレータより前の文字列。

			例 : "xxx/yyy/zzz" ==> "xxx/yyy"
			例 : "xxx/yyy/zzz/" ==> "xxx/yyy/zzz"
			例 : "/aaa" ==> ""
			例 : "aaa" ==> ""

		*/
		public static string GetDirectoryPathString(string a_path)
		{
			int t_find_index = -1;

			for(int ii=(a_path.Length - 1);ii >= 0;ii--){
				if((a_path[ii] == '/')||(a_path[ii] == '\\')){
					t_find_index = ii;
					break;
				}
			}

			if(t_find_index > 0){
				return a_path.Substring(0,t_find_index);
			}else{
				return "";
			}
		}

		/** GetFileNameString

			最後のセパレータより後の文字列。

			例 : "xxx/yyy/zzz" ==> "zzz"
			例 : "xxx/yyy/zzz/" ==> ""
			例 : "/aaa" ==> "aaa"
			例 : "aaa" ==> "aaa"

		*/
		public static string GetFileNameString(string a_path)
		{
			int t_find_index = -1;

			for(int ii=(a_path.Length - 1);ii >= 0;ii--){
				if((a_path[ii] == '/')||(a_path[ii] == '\\')){
					t_find_index = ii;
					break;
				}
			}

			if(t_find_index < 0){
				return a_path;
			}else if(t_find_index < a_path.Length - 1){
				return a_path.Substring(t_find_index + 1,a_path.Length - t_find_index - 1);
			}else{
				return "";
			}
		}

		/** GetFileNameCutExtensionString

			最後のセパレータより後の文字列。

			例 : "xxx/yyy/zzz.e" ==> "zzz"
			例 : "xxx/yyy/zzz.e/" ==> ""
			例 : "/aaa.e" ==> "aaa"
			例 : "aaa.e" ==> "aaa"

		*/
		public static string GetFileNameCutExtensionString(string a_path)
		{
			int t_find_index = -1;
			int t_find_extension = a_path.Length;

			for(int ii=(a_path.Length - 1);ii >= 0;ii--){
				if((a_path[ii] == '/')||(a_path[ii] == '\\')){
					t_find_index = ii;
					break;
				}else if(a_path[ii] == '.'){
					t_find_extension = ii;
				}
			}

			if(t_find_index < 0){
				return a_path.Substring(0,t_find_extension);
			}else if(t_find_index < a_path.Length - 1){
				return a_path.Substring(t_find_index + 1,t_find_extension - t_find_index - 1);
			}else{
				return "";
			}
		}

		/** constructor

			JsonToObject

		*/
		public Path()
		{
			this.path = "";
		}

		/** constructor
		*/
		public Path(string a_path)
		{
			this.path = a_path;
		}

		/** constructor
		*/
		public Path(Fee.File.Path a_path)
		{
			this.path = a_path.path;
		}

		/** constructor
		*/
		public Path(Fee.File.Path a_path,string a_add_path,string a_separator)
		{
			this.path = a_path.path;
			this.Add(a_add_path,a_separator);
		}

		/** パス。取得。
		*/
		public string GetPath()
		{
			return this.path;
		}

		/** パス。取得。

			最後のセパレータを取る。

		*/
		public string GetPathCutLastSeparator()
		{
			if(this.path.Length > 0){
				if((this.path[this.path.Length - 1] == '/')||(this.path[this.path.Length - 1] == '\\')){
					return this.path.Substring(0,this.path.Length - 1);
				}else{
					return this.path;
				}
			}else{
				return "";
			}
		}

		/** ファイル名。取得。
		*/
		public string GetFileName()
		{
			return Path.GetFileNameString(this.path);
		}

		/** ファイル名。拡張子なし。取得。
		*/
		public string GetFileNameCutExtension()
		{
			return Path.GetFileNameCutExtensionString(this.path);
		}

		/** ディレクトリパス。取得。
		*/
		public string GetDirectoryPath()
		{
			return Path.GetDirectoryPathString(this.path);
		}

		/** 追加。
		*/
		public void Add(string a_relative_path,string a_separator)
		{
			if(a_relative_path.Length > 0){
				if(this.path.Length == 0){
					this.path = a_relative_path;
				}else{
					if((this.path[this.path.Length - 1] == '/')||(this.path[this.path.Length - 1] == '\\')){
						this.path += a_relative_path;
					}else{
						this.path += a_separator + a_relative_path;
					}
				}
			}
		}

		/** 追加。
		*/
		public void Add(Fee.File.Path a_path,string a_separator)
		{
			this.Add(a_path.GetPath(),a_separator);
		}

		/** ファイル名を変更したパスを作成。

			this.GetDirectoryPath() + a_file_name_string

		*/
		public Fee.File.Path CreateFileNameChangePath(string a_file_name_string,string a_separator)
		{
			Fee.File.Path t_path = new Path(this.GetDirectoryPath());
			t_path.Add(a_file_name_string,a_separator);
			return t_path;
		}

		/** ディレクトリパスを変更したパスを作成。

			a_directory_path_string + this.GetFileName()

		*/
		public Fee.File.Path CreateDirectoryPathChangePath(string a_directory_path_string,string a_separator)
		{
			Fee.File.Path t_path = new Path(a_directory_path_string);
			t_path.Add(this.GetFileName(),a_separator);
			return t_path;
		}

		/** ファイル名を変更したパスを作成。

			this.GetDirectoryPath() + a_path.GetFileName()

		*/
		public Fee.File.Path CreateFileNameChangePath(Fee.File.Path a_path,string a_separator)
		{
			return this.CreateFileNameChangePath(a_path.GetFileName(),a_separator);
		}

		/** ディレクトリパスを変更したパスを作成。

			a_path.GetDirectoryPath() + this.GetFileName()

		*/
		public Fee.File.Path CreateDirectoryPathChangePath(Fee.File.Path a_path,string a_separator)
		{
			return this.CreateDirectoryPathChangePath(a_path.GetDirectoryPath(),a_separator);
		}

		/** ファイル名。変更。
		*/
		public void ChangeFileName(string a_file_name_string,string a_separator)
		{
			this.path = CreateFileNameChangePath(a_file_name_string,a_separator).GetPath();
		}

		/** ディレクトリパス。変更。
		*/
		public void ChangeDirectoryPath(string a_directory_path_string,string a_separator)
		{
			this.path = CreateDirectoryPathChangePath(a_directory_path_string,a_separator).GetPath();
		}

		/** ファイル名。変更。

			a_path.GetFileName()に変更。

		*/
		public void ChangeFileName(Fee.File.Path a_path,string a_separator)
		{
			this.path = CreateFileNameChangePath(a_path.GetFileName(),a_separator).GetPath();
		}

		/** ディレクトリパス。変更。

			a_path.GetDirectoryPath()に変更。

		*/
		public void ChangeDirectoryPath(Fee.File.Path a_path,string a_separator)
		{
			this.path = CreateDirectoryPathChangePath(a_path.GetDirectoryPath(),a_separator).GetPath();
		}

		/** CreateLocalPath
		*/
		public static Path CreateLocalPath()
		{
			return new Path(UnityEngine.Application.persistentDataPath);
		}

		/** CreateStreamingAssetsPath
		*/
		public static Path CreateStreamingAssetsPath()
		{
			return new Path(UnityEngine.Application.streamingAssetsPath);
		}

		/** CreateAssetsPath
		*/
		#if(UNITY_EDITOR)
		public static Path CreateAssetsPath()
		{
			return new Path(UnityEngine.Application.dataPath);
		}
		#endif

		/** CreateLocalPath

			a_relative_path : 相対パス。

		*/
		public static Path CreateLocalPath(string a_relative_path,string a_separator)
		{
			Fee.File.Path t_path = CreateLocalPath();
			t_path.Add(a_relative_path,a_separator);
			return t_path;
		}

		/** CreateLocalPath

			a_relative_path : 相対パス。

		*/
		public static Path CreateLocalPath(Fee.File.Path a_relative_path,string a_separator)
		{
			return CreateLocalPath(a_relative_path.GetPath(),a_separator);
		}

		/** CreateStreamingAssetsPath

			a_relative_path : 相対パス。

		*/
		public static Path CreateStreamingAssetsPath(string a_relative_path,string a_separator)
		{
			Fee.File.Path t_path = CreateStreamingAssetsPath();
			t_path.Add(a_relative_path,a_separator);
			return t_path;
		}

		/** CreateStreamingAssetsPath

			a_relative_path : 相対パス。

		*/
		public static Path CreateStreamingAssetsPath(Fee.File.Path a_relative_path,string a_separator)
		{
			return CreateStreamingAssetsPath(a_relative_path.GetPath(),a_separator);
		}

		#if(UNITY_EDITOR)

		/** CreateAssetsPath

			a_relative_path : 相対パス。

		*/
		public static Path CreateAssetsPath(string a_relative_path,string a_separator)
		{
			Fee.File.Path t_path = CreateAssetsPath();
			t_path.Add(a_relative_path,a_separator);
			return t_path;
		}

		/** CreateAssetsPath

			a_relative_path : 相対パス。

		*/
		public static Path CreateAssetsPath(Fee.File.Path a_relative_path,string a_separator)
		{
			return CreateAssetsPath(a_relative_path.GetPath(),a_separator);
		}

		#endif
	}
}

