using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＪＳＯＮ。オブジェクト化。
*/


/** NJsonItem
*/
namespace NJsonItem
{
	/** JsonToObject
	*/
	public class JsonToObject<Type>
	{
		/** Convert
		*/
		public static Type Convert(JsonItem a_jsonitem)
		{
			Type t_return = default(Type);
			System.Type t_type = typeof(Type);

			System.Object t_object = JsonToObject_SystemObject.Convert(t_type,a_jsonitem);
			if(t_object != null){
				t_return = (Type)System.Convert.ChangeType(t_object,typeof(Type));
			}

			return t_return;
		}
	}
}

