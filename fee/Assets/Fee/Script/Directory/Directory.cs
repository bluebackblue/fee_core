using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ディレクトリ。コンフィグ。
*/


/** NDirectory
*/
namespace NDirectory
{
	/** Directory
	*/
	public class Directory : Config
	{
		/** constructor
		*/
		private Directory()
		{
		}

		/** 
		*/
		public static Item GetDirectoryItem(string a_full_path)
		{
			Root t_root = new Root(a_full_path);
			Debug.Log(t_root.full_path);

			Item t_ret = new Item();

			List<Work> t_dir_work_list = new List<Work>();
			t_dir_work_list.Add(new Work("",t_ret));

			while(t_dir_work_list.Count > 0){
				Work t_work = t_dir_work_list[t_dir_work_list.Count - 1];
				t_dir_work_list.RemoveAt(t_dir_work_list.Count - 1);

				//サブフォルダ。
				{
					Debug.Log(t_root.full_path + t_work.dir);

					Item t_new = new Item();

					string[] t_directory_list = System.IO.Directory.GetDirectories(t_root.full_path + t_work.dir);
					for(int ii=0;ii<t_directory_list.Length;ii++){
						string t_directory_name = System.IO.Path.GetFileName(t_directory_list[ii]);
						if(t_work.dir.Length > 0){
							t_dir_work_list.Add(new Work(t_work.dir + "\\" + t_directory_name,t_new));
						}else{
							t_dir_work_list.Add(new Work(t_directory_name,t_new));
						}
					}
				}
			}

			return t_ret;
		}
	}
}

