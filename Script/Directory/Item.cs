

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ディレクトリ。アイテム。
*/


/** Fee.Directory
*/
namespace Fee.Directory
{
	/** Item
	*/
	public class Item
	{
		/** ディレクトリリスト。
		*/
		private System.Collections.Generic.List<Item> directory_list;

		/** ファイルリスト。
		*/
		private System.Collections.Generic.List<Item> file_list;

		/** ルート。
		*/
		private Root root;

		/** 名前。
		*/
		private string name;

		/** ソート関数。
		*/
		public static int Sort_Name(Item a_test,Item a_target)
		{
			return System.StringComparer.OrdinalIgnoreCase.Compare(a_test.name,a_target.name);
		}

		/** constructor
		*/
		public Item(Root a_root,string a_name)
		{
			this.directory_list = new System.Collections.Generic.List<Item>();

			this.file_list = new System.Collections.Generic.List<Item>();

			this.root = a_root;

			this.name = a_name;
		}

		/** ディレクトリアイテム。追加。
		*/
		public void AddDirectoryItem(Item a_item)
		{
			this.directory_list.Add(a_item);
		}

		/** ファイルアイテム。追加。
		*/
		public void AddFileItem(Item a_item)
		{
			this.file_list.Add(a_item);
		}

		/** ルート。取得。

			return == null : ルートではない。

		*/
		public Root GetRoot()
		{
			return this.root;
		}

		/** 名前。取得。
		*/
		public string GetName()
		{
			return this.name;
		}

		/** ディレクトリ。検索。
		*/
		public Item FindDirectory(string a_name)
		{
			for(int ii=0;ii<this.directory_list.Count;ii++){
				if(this.directory_list[ii].name == a_name){
					return this.directory_list[ii];
				}
			}
			return null;
		}

		/** ディレクトリリスト。取得。
		*/
		public System.Collections.Generic.List<Item> GetDirectoryList()
		{
			return this.directory_list;
		}

		/** ファイルリスト。取得。
		*/
		public System.Collections.Generic.List<Item> GetFileList()
		{
			return this.file_list;
		}

		/** ソート。
		*/
		public void Sort()
		{
			this.directory_list.Sort(Sort_Name);
			this.file_list.Sort(Sort_Name);
		}
	}
}

