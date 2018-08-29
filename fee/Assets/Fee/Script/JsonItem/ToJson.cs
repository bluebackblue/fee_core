using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＪＳＯＮ。ＪＳＯＮ化。
*/


/** NJsonItem
*/
namespace NJsonItem
{
	/** ToJson
	*/
	public class ToJson
	{
		/** Convert
		*/
		public static JsonItem Convert(System.Object a_instance,List<ToJson_Work> a_work = null)
		{
			List<ToJson_Work> t_work = a_work;

			if(a_work == null){
				t_work = new List<ToJson_Work>();				
			}

			JsonItem t_return = null;
			bool t_search_member = false;

			if(a_instance != null){
				System.Type t_type = a_instance.GetType();

				if(t_type == typeof(string)){
					//string

					string t_value_raw = a_instance as string;

					if(t_value_raw != null){
						t_return = new JsonItem(new Value_StringData(t_value_raw));
					}else{
						//nullの場合は追加しない。
						t_return = null;
					}
				}else if(t_type == typeof(int)){
					//int

					int t_value_raw = (int)a_instance;
				
					t_return = new JsonItem(new Value_Integer(t_value_raw));
				}else if(t_type == typeof(long)){
					//long

					int t_value_raw = (int)a_instance;

					t_return = new JsonItem(new Value_Integer(t_value_raw));
				}else if(t_type == typeof(float)){
					//float

					float t_value_raw = (float)a_instance;

					t_return = new JsonItem(new Value_Float(t_value_raw));
				}else if(t_type.IsGenericType == true){
					System.Type t_type_g = t_type.GetGenericTypeDefinition();

					if(t_type_g == typeof(List<>)){
						//List

						IList t_value_raw = a_instance as IList;
						if(t_value_raw != null){

							JsonItem t_jsonitem = new JsonItem(new Value_IndexArray());
							for(int ii=0;ii<t_value_raw.Count;ii++){
								if(t_value_raw[ii] != null){
									ToJson_Work t_new = new ToJson_Work(t_value_raw[ii],ii,t_jsonitem);
									t_work.Add(t_new);
								}else{
									//nullの子は追加しない。
								}
							}
							t_return = t_jsonitem;
						}else{

							//nullの場合は追加しない。
							t_return = null;
						}
					}else if(t_type_g == typeof(Dictionary<,>)){
						//Dictionary

						IDictionary t_value_raw = a_instance as IDictionary;
						if(t_value_raw != null){

							JsonItem t_jsonitem = new JsonItem(new Value_AssociativeArray());

							//TODO:
							foreach(KeyValuePair<string,JsonItem> t_pair in t_value_raw){
								string t_key_string = t_pair.Key as string;
								if(t_pair.Value != null){
									ToJson_Work t_new = new ToJson_Work(t_pair.Value,t_key_string,t_jsonitem);
									t_work.Add(t_new);
								}else{
									//nullの子は追加しない。
								}
							}

							t_return = t_jsonitem;
						}else{
							//nullの場合は追加しない。
							t_return = null;
						}
					}else{
						t_search_member = true;
					}
				}else{
					t_search_member = true;
				}

				if(t_search_member == true){
					//class,struct

					JsonItem t_jsonitem = new JsonItem(new Value_AssociativeArray());

					System.Reflection.MemberInfo[] t_member = t_type.GetMembers(System.Reflection.BindingFlags.Public|System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance);

					int t_count = 0;

					for(int ii=0;ii<t_member.Length;ii++){
						if(t_member[ii].MemberType == System.Reflection.MemberTypes.Field){
							System.Reflection.FieldInfo t_fieldinfo = t_member[ii] as System.Reflection.FieldInfo;
							if(t_fieldinfo != null){
								if((t_fieldinfo.Attributes == System.Reflection.FieldAttributes.Public)||(t_fieldinfo.Attributes == System.Reflection.FieldAttributes.Private)){
									System.Object t_raw = t_fieldinfo.GetValue(a_instance);

									if(t_raw != null){
										ToJson_Work t_new = new ToJson_Work(t_raw,t_fieldinfo.Name,t_jsonitem);
										t_work.Add(t_new);

										t_count++;
									}else{
										//nullの子は追加しない。
									}
								}else{
									//ＪＳＯＮ化しない型。
								}
							}else{
								Tool.Assert(false);
							}
						}
					}

					t_return = t_jsonitem;
				}
			}else{
				//nullの場合は追加しない。
				t_return = null;
			}

			//再起呼び出し。
			if(a_work == null){
				while(true){
					int t_count = t_work.Count;
					if(t_count > 0){
						ToJson_Work t_current_work = t_work[t_count - 1];
						t_work.RemoveAt(t_count - 1);

						if(t_current_work.additem_object != null){
							JsonItem t_jsonitem_member = Convert(t_current_work.additem_object,t_work);
							if(t_jsonitem_member != null){
								t_current_work.to_jsonitem.AddItem(t_jsonitem_member,false);
							}else{
								//nullの場合は追加しない。
							}
						}else if(t_current_work.setitem_object != null){
							JsonItem t_jsonitem_member = Convert(t_current_work.setitem_object,t_work);
							if(t_jsonitem_member != null){
								t_current_work.to_jsonitem.SetItem(t_current_work.setitem_key,t_jsonitem_member,false);
							}else{
								//nullの場合は追加しない。
							}
						}else{
							//nullの場合は追加しない。
						}
					}else{
						break;
					}
				}
			}

			return t_return;
		}
	}
}

