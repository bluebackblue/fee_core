using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ディレクトリ。ワーク。
*/


/** NDirectory
*/
namespace NDirectory
{
	/** Work
	*/
	public class Work
	{
		/** 相対パス。
		*/
		private string path;

		/** アイテム。
		*/
		private Item item;

		/** constructor
		*/
		public Work(string a_path,Item a_item)
		{
			/** 相対パス。
			*/
			this.path = a_path;

			/** 追加先。
			*/
			this.item = a_item;
		}

		/** 相対パス。取得。
		*/
		public string GetPath()
		{
			return this.path;
		}

		/** アイテム。取得。
		*/
		public Item GetItem()
		{
			return this.item;
		}
	}
}

