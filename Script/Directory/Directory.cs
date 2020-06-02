

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ディレクトリ。コンフィグ。
*/


/** Fee.Directory
*/
namespace Fee.Directory
{
	/** Directory
	*/
	public class Directory
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

			System.Collections.Generic.List<WorkItem> t_dir_work_list = new System.Collections.Generic.List<WorkItem>();
			{
				//相対パス。
				string t_path = "";
				t_dir_work_list.Add(new WorkItem(t_path,t_ret));
			}

			while(t_dir_work_list.Count > 0){
				//ワーク。
				WorkItem t_work_item = t_dir_work_list[t_dir_work_list.Count - 1];
				t_dir_work_list.RemoveAt(t_dir_work_list.Count - 1);

				//ディレクトリ列挙。
				try{
					string[] t_directory_list = System.IO.Directory.GetDirectories(t_root.GetFullPath() + t_work_item.GetPath());
					for(int ii=0;ii<t_directory_list.Length;ii++){
						string t_directory_name = System.IO.Path.GetFileName(t_directory_list[ii]);

						//ディレクトリを追加。
						Item t_sub_item = new Item(null,t_directory_name);
						t_work_item.GetItem().AddDirectoryItem(t_sub_item);

						//ディレクトリの相対パス。
						string t_sub_path;
						if(t_work_item.GetPath().Length > 0){
							t_sub_path = t_work_item.GetPath() + "\\" + t_directory_name;
						}else{
							t_sub_path = t_directory_name;
						}

						t_dir_work_list.Add(new WorkItem(t_sub_path,t_sub_item));
					}
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}

				//ファイル列挙。
				try{
					string[] t_file_list = System.IO.Directory.GetFiles(t_root.GetFullPath() + t_work_item.GetPath());
					for(int ii=0;ii<t_file_list.Length;ii++){
						string t_file_name = System.IO.Path.GetFileName(t_file_list[ii]);

						//ファイルアイテム追加。
						Item t_item = new Item(null,t_file_name);
						t_work_item.GetItem().AddFileItem(t_item);
					}
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}

				//ソート。
				t_work_item.GetItem().Sort();
			}

			return t_ret;
		}
	}
}

