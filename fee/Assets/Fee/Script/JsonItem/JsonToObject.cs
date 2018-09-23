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
			System.Object t_object = null;

			try{
				t_object = System.Activator.CreateInstance(typeof(Type));
			}catch(System.Exception t_exception){
				//引数なしconstructorの呼び出しに失敗。
				Tool.LogError(t_exception);
			}

			JsonToObject_SystemObject.Convert(ref t_object,a_jsonitem);

			return (Type)System.Convert.ChangeType(t_object,typeof(Type));
		}
	}
}

