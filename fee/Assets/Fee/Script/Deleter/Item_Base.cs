using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 削除管理。
*/


/** NDeleter
*/
namespace NDeleter
{
	/** DeleteItem_Base
	*/
	public interface DeleteItem_Base
	{
		/** 削除。
		*/
		void Delete();
	}
}

