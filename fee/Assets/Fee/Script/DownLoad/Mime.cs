using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ダウンロード。ＭＩＭＥ。
*/


/** NDownLoad
*/
namespace NDownLoad
{
	/** Item
	*/
	public class Mime
	{
		/** データタイプ。取得。
		*/
		public static DataType GetDataTypeFromContentType(string a_mime)
		{
			switch(a_mime){
			case "image/jpeg":
			case "image/bmp":
			case "image/png":
				{
				}return DataType.Texture;
			case "text/html":
				{
				}return DataType.Text;
			}

			return DataType.Error;
		}
	}
}

