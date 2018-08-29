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
	/** FromJson_Work
	*/
	public class FromJson_Work
	{
		/** type
		*/
		public System.Type type;

		/** jsonitem
		*/
		public JsonItem jsonitem;

		/** add_list
		*/
		public IList add_list;

		/** add_dictionary
		*/
		public IDictionary add_dictionary;
		public string add_dictionary_key;

		/** setvalue_fieldinfo
		*/
		public System.Reflection.FieldInfo setvalue_fieldinfo;

		/** constructor
		*/
		public FromJson_Work(IList a_add_list,System.Type a_type,JsonItem a_jsonitem)
		{
			this.add_list = a_add_list;

			this.type = a_type;
			this.jsonitem = a_jsonitem;

			this.add_dictionary = null;
			this.add_dictionary_key = null;
			this.setvalue_fieldinfo = null;
		}

		/** constructor
		*/
		public FromJson_Work(IDictionary a_add_dictionary,string a_add_dictionary_key,System.Type a_type,JsonItem a_jsonitem)
		{
			this.add_dictionary = a_add_dictionary;
			this.add_dictionary_key = a_add_dictionary_key;
			this.type = a_type;
			this.jsonitem = a_jsonitem;

			this.add_list = null;
			this.setvalue_fieldinfo = null;
		}

		/** constructor
		*/
		public FromJson_Work(System.Reflection.FieldInfo a_setvalue_fieldinfo,System.Type a_type,JsonItem a_jsonitem)
		{
			this.setvalue_fieldinfo = a_setvalue_fieldinfo;

			this.type = a_type;
			this.jsonitem = a_jsonitem;

			this.add_list = null;
			this.add_dictionary = null;
			this.add_dictionary_key = null;
		}
	}
}

