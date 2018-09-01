﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ディレクトリ。ルート。
*/


/** NDirectory
*/
namespace NDirectory
{
	/** Root
	*/
	public class Root
	{
		/** full_path
		*/
		public string full_path;

		/** constructor
		*/
		public Root(string a_full_path)
		{
			System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(a_full_path);
			this.full_path = t_fileinfo.FullName;

			char t_last = this.full_path[this.full_path.Length - 1];
			if((t_last == '/')||(t_last == '\\')){
			}else{
				this.full_path += "\\";
			}
		}
	}
}
