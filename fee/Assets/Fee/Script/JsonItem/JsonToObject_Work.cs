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
	/** JsonToObject_Work
	*/
	public class JsonToObject_Work
	{
		/** 追加先メンバータイプ。
		*/
		private System.Type type;

		/** 追加するコンバート前ＪＳＯＮ。
		*/
		private JsonItem jsonitem;

		/** 追加先。
		*/
		private IList add_list;

		/** 追加先。
		*/
		private IDictionary add_dictionary;
		private string add_dictionary_key;

		/** 追加先。
		*/
		private System.Reflection.FieldInfo setvalue_fieldinfo;
		private System.Object setvalue_object;

		/** constructor
		*/
		public JsonToObject_Work(IList a_add_list,System.Type a_type,JsonItem a_jsonitem)
		{
			this.type = a_type;
			this.jsonitem = a_jsonitem;
			this.add_list = a_add_list;
			this.add_dictionary = null;
			this.add_dictionary_key = null;
			this.setvalue_fieldinfo = null;
			this.setvalue_object = null;
		}

		/** constructor
		*/
		public JsonToObject_Work(IDictionary a_add_dictionary,string a_add_dictionary_key,System.Type a_type,JsonItem a_jsonitem)
		{
			this.type = a_type;
			this.jsonitem = a_jsonitem;
			this.add_list = null;
			this.add_dictionary = a_add_dictionary;
			this.add_dictionary_key = a_add_dictionary_key;
			this.setvalue_fieldinfo = null;
			this.setvalue_object = null;
		}

		/** constructor
		*/
		public JsonToObject_Work(System.Reflection.FieldInfo a_setvalue_fieldinfo,System.Object a_setvalue_object,System.Type a_type,JsonItem a_jsonitem)
		{
			this.type = a_type;
			this.jsonitem = a_jsonitem;
			this.add_list = null;
			this.add_dictionary = null;
			this.add_dictionary_key = null;
			this.setvalue_fieldinfo = a_setvalue_fieldinfo;
			this.setvalue_object = a_setvalue_object;
		}

		/** 実行。
		*/
		public void Do(List<JsonToObject_Work> a_work_pool)
		{
			if(this.add_list != null){
				//List
				System.Object t_value_member = JsonToObject_SystemObject.Convert(this.type,this.jsonitem,a_work_pool);
				this.add_list.Add(t_value_member);
			}else if(this.add_dictionary != null){
				//Dictionary
				System.Object t_value_member = JsonToObject_SystemObject.Convert(this.type,this.jsonitem,a_work_pool);
				this.add_dictionary.Add(this.add_dictionary_key,t_value_member);
			}else if(this.setvalue_fieldinfo != null){
				//class,struct
				System.Object t_value = JsonToObject_SystemObject.Convert(this.type,this.jsonitem,a_work_pool);
				if(t_value != null){
					this.setvalue_fieldinfo.SetValue(this.setvalue_object,t_value);
				}
			}
		}
	}
}

