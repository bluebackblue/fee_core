

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。パス。
*/


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

		/** フルパス。取得。
		*/
		public string GetPath()
		{
			return this.path;
		}

		/** 正規化。
		*/
		public string GetNormalizePath()
		{
			if(this.path.Length > 0){
				if((this.path[this.path.Length - 1] == '/')||(this.path[this.path.Length - 1] == '\\')){
					return this.path.Substring(0,this.path.Length - 1);
				}
			}else{
				return ".";
			}

			return this.path;
		}

		/** 正規化。
		*/
		public void Normalize()
		{
			this.path = this.GetNormalizePath();
		}

		/** ファイル名。取得。
		*/
		public string GetFileName()
		{
			return System.IO.Path.GetFileName(this.path);
		}

		/** ディレクトリパス。取得。
		*/
		public string GetDirectoryPath()
		{
			return Path.GetGetDirectoryString(this.path);
		}

		/** GetGetDirectoryString
		*/
		public static string GetGetDirectoryString(string a_path)
		{
			int t_find_index = -1;
			for(int ii=a_path.Length-1;ii >= 0;ii--){
				if((a_path[ii] == '/')||(a_path[ii] == '\\')){
					t_find_index = ii;
					break;
				}
			}

			if(t_find_index > 0){
				return a_path.Substring(0,t_find_index);
			}

			return "";
		}

		/** ファイル名を変更したパス。作成。
		*/
		public Path CreateFileNameChangePath(string a_filename,string a_separate = "/")
		{
			string t_path = Path.GetGetDirectoryString(this.path) + a_separate + a_filename;

			return new Path(t_path);
		}

		/** ファイル名を変更したパス。作成。
		*/
		public Path CreateDirectoryPathChangePath(string a_directorypath,string a_separate = "/")
		{
			string t_path = a_directorypath + a_separate + System.IO.Path.GetFileName(this.path);

			return new Path(t_path);
		}

		/** CreateLocalPath
		*/
		public static Path CreateLocalPath()
		{
			return new Path(UnityEngine.Application.persistentDataPath);
		}

		/** CreateLocalPath
		*/
		public static Path CreateLocalPath(Path a_relative_path)
		{
			//a_relative_pathは相対パス。
			return new Path(UnityEngine.Application.persistentDataPath + "/" + a_relative_path.GetPath());
		}

		/** CreateLocalPath
		*/
		public static Path CreateLocalPath(string a_relative_path)
		{
			//a_relative_pathは相対パス。
			return new Path(UnityEngine.Application.persistentDataPath + "/" + a_relative_path);
		}

		/** CreateStreamingAssetsPath
		*/
		public static Path CreateStreamingAssetsPath()
		{
			return new Path(UnityEngine.Application.streamingAssetsPath);
		}

		/** CreateStreamingAssetsPath
		*/
		public static Path CreateStreamingAssetsPath(Path a_relative_path)
		{
			//a_relative_pathは相対パス。
			return new Path(UnityEngine.Application.streamingAssetsPath + "/" + a_relative_path.GetPath());
		}

		/** CreateStreamingAssetsPath
		*/
		public static Path CreateStreamingAssetsPath(string a_relative_path)
		{
			//a_relative_pathは相対パス。
			return new Path(UnityEngine.Application.streamingAssetsPath + "/" + a_relative_path);
		}

		/** CreateAssetsPath
		*/
		#if(UNITY_EDITOR)
		public static Path CreateAssetsPath()
		{
			return new Path(UnityEngine.Application.dataPath);
		}
		#endif

		/** CreateAssetsPath
		*/
		#if(UNITY_EDITOR)
		public static Path CreateAssetsPath(Path a_relative_path)
		{
			//a_relative_pathは相対パス。
			return new Path(UnityEngine.Application.dataPath + "/" + a_relative_path.GetPath());
		}
		#endif

		/** CreateAssetsPath
		*/
		#if(UNITY_EDITOR)
		public static Path CreateAssetsPath(string a_relative_path)
		{
			//a_relative_pathは相対パス。
			return new Path(UnityEngine.Application.dataPath + "/" + a_relative_path);
		}
		#endif
	}
}

