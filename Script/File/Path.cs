

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

		a_path == "X:/aaaa/bbbb/ccc/dddd" : フルパス
		directorypath == "X:/aaaa/bbbb/ccc/"
		filename == "dddd"

		a_path == "iii/jjj" : 相対パス
		directorypath == "iii/"
		filename == "jjj"

	*/
	public class Path
	{
		/** filename
		*/
		private readonly string filename;

		/** directorypath
		*/
		private readonly string directorypath;

		/** constructor

			JsonToObject

		*/
		public Path()
		{
			this.filename = "";
			this.directorypath = "";
		}

		/** constructor
		*/
		public Path(string a_directorypath,string a_filename)
		{
			//filename
			if(a_filename != null){
				this.filename = a_filename;

				Tool.Assert(System.IO.Path.GetFileName(a_filename) == this.filename);
			}else{
				this.filename = "";
			}

			//directorypath
			if(a_directorypath != null){
				this.directorypath = a_directorypath;

				Tool.Assert(System.IO.Path.GetFileName(a_directorypath) == "");
			}else{
				this.directorypath = "";
			}
		}

		/** constructor
		*/
		public Path(string a_path)
		{
			if(a_path != null){
				//filename
				this.filename = System.IO.Path.GetFileName(a_path);

				//directorypath
				this.directorypath = a_path.Substring(0,a_path.Length - this.filename.Length);
			}else{
				//filename
				this.filename = "";

				//directorypath
				this.directorypath = "";
			}
		}

		/** フルパス。取得。
		*/
		public string GetPath()
		{
			return this.directorypath + this.filename;;
		}

		/** ファイル名。取得。
		*/
		public string GetFileName()
		{
			return this.filename;
		}

		/** ディレクトリパス。取得。
		*/
		public string GetDirectoryPath()
		{
			return this.directorypath;
		}

		/** ファイル名を変更したパス。作成。
		*/
		public Path CreateFileNameChangePath(string a_filename)
		{
			return new Path(this.directorypath,a_filename);
		}

		/** ファイル名を変更したパス。作成。
		*/
		public Path CreateDirectoryPathChangePath(string a_directorypath)
		{
			return new Path(a_directorypath,this.filename);
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

