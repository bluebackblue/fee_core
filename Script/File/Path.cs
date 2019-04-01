

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。パス。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** Path

	path = X:/aaaa/bbbb/ccc/dddd
	directorypath = X:/aaaa/bbbb/ccc/
	filename = dddd

	path = iii/jjj
	directorypath = iii/
	filename = jjj

	*/
	public class Path
	{
		/** path
		*/
		private string path;

		/** directorypath
		*/
		private string directorypath;

		/** filename
		*/
		private string filename;

		/** constructor
		*/
		public Path(string a_directorypath,string a_filename)
		{
			this.path = null;
			this.directorypath = a_directorypath;
			this.filename = a_filename;
		}

		/** constructor
		*/
		public Path(string a_path)
		{
			this.path = a_path;
			this.directorypath = null;
			this.filename = null;
		}

		/** CalcPath
		*/
		private void CalcPath(string a_directorypath,string a_filename)
		{
			if((a_directorypath != null)&&(a_filename != null)){
				this.path = a_directorypath + a_filename;
			}else if(a_directorypath != null){
				this.path = a_directorypath;
			}else if(a_filename != null){
				this.path = a_filename;
			}else{
				this.path = "";
			}
		}

		/** CalcOther
		*/
		private void CalcOther(string a_path)
		{
			if(a_path != null){
				this.filename = System.IO.Path.GetFileName(a_path);
				this.directorypath = a_path.Substring(0,a_path.Length - filename.Length);
			}else{
				this.filename = "";
				this.directorypath = "";
			}
		}

		/** フルパス。設定。
		*/
		public void SetPath(string a_path)
		{
			this.path = a_path;
			this.CalcOther(a_path);
		}

		/** ディレクトリパス。設定。
		*/
		public void SetDirectoryPath(string a_directorypath)
		{
			this.directorypath = a_directorypath;
			this.CalcPath(a_directorypath,this.filename);
		}

		/** ファイル名。設定。
		*/
		public void SetFileName(string a_filename)
		{
			this.filename = a_filename;
			this.CalcPath(this.directorypath,a_filename);
		}

		/** フルパス。取得。
		*/
		public string GetPath()
		{
			if(this.path == null){
				this.CalcPath(this.directorypath,this.filename);
			}
			return this.path;
		}

		/** ファイル名。取得。
		*/
		public string GetFileName()
		{
			if(this.filename == null){
				this.CalcOther(this.path);
			}
			return this.filename;
		}

		/** ディレクトリパス。取得。
		*/
		public string GetDirectoryPath()
		{
			return this.directorypath;
		}

		/** ＵＲＬ作成。名前変更。
		*/
		public Path CreateUrl_ChangeFileName(string a_filename)
		{
			return new Path(this.directorypath,a_filename);
		}

		/** ＵＲＬ作成。ディレクトリパス変更。
		*/
		public Path CreateUrl_ChangeDirectoryPath(string a_directorypath)
		{
			return new Path(a_directorypath,this.filename);
		}
	}
}

