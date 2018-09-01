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

		/** ディレクトリ探査。
		*/
		public static Item GetDirectoryItem(string a_full_path)
		{
			Root t_root = new Root(a_full_path);
			Item t_ret = new Item(t_root,"");

			List<Work> t_dir_work_list = new List<Work>();
			{
				//相対パス。
				string t_path = "";
				t_dir_work_list.Add(new Work(t_path,t_ret));
			}

			while(t_dir_work_list.Count > 0){
				//ワーク。
				Work t_work = t_dir_work_list[t_dir_work_list.Count - 1];
				t_dir_work_list.RemoveAt(t_dir_work_list.Count - 1);

				//ディレクトリ列挙。
				{
					string[] t_directory_list = System.IO.Directory.GetDirectories(t_root.GetFullPath() + t_work.GetPath());
					for(int ii=0;ii<t_directory_list.Length;ii++){
						string t_directory_name = System.IO.Path.GetFileName(t_directory_list[ii]);

						//ディレクトリを追加。
						Item t_sub_item = new Item(null,t_directory_name);
						t_work.GetItem().AddDirectoryItem(t_sub_item);

						//ディレクトリの相対パス。
						string t_sub_path;
						if(t_work.GetPath().Length > 0){
							t_sub_path = t_work.GetPath() + "\\" + t_directory_name;
						}else{
							t_sub_path = t_directory_name;
						}

						t_dir_work_list.Add(new Work(t_sub_path,t_sub_item));
					}
				}

				//ファイル列挙。
				{
					string[] t_file_list = System.IO.Directory.GetFiles(t_root.GetFullPath() + t_work.GetPath());
					for(int ii=0;ii<t_file_list.Length;ii++){
						string t_file_name = System.IO.Path.GetFileName(t_file_list[ii]);

						//ファイルアイテム追加。
						Item t_item = new Item(null,t_file_name);
						t_work.GetItem().AddFileItem(t_item);
					}
				}

				//ソート。
				t_work.GetItem().Sort();
			}

			return t_ret;
		}
	}
}

