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
	/** JsonToObject_SystemObject
	*/
	public class JsonToObject_SystemObject
	{
		/** Convert
		*/
		public static System.Object Convert(System.Type a_type,JsonItem a_jsonitem,List<JsonToObject_Work> a_workpool = null)
		{
			List<JsonToObject_Work> t_workpool = a_workpool;

			if(t_workpool == null){
				t_workpool = new List<JsonToObject_Work>();
			}

			System.Object t_return = null;

			if(a_jsonitem.IsStringData() == true){
				if(a_type == typeof(string)){
					//string
					t_return = a_jsonitem.GetStringData();
				}
			}else if(a_jsonitem.IsIntegerNumber() == true){
				if((a_type == typeof(int))||(a_type == typeof(long))){
					//int
					//long
					t_return = a_jsonitem.GetInteger();
				}
			}else if(a_jsonitem.IsFloatNumber() == true){
				if((a_type == typeof(float))||(a_type == typeof(double))){
					//float
					//double
					t_return = a_jsonitem.GetFloat();
				}
			}else if(a_jsonitem.IsIndexArray() == true){
				if(a_type.IsGenericType == true){
					if(a_type.GetGenericTypeDefinition() == typeof(List<>)){
						//List

						t_return = System.Activator.CreateInstance(a_type);

						IList t_return_list = t_return as IList;
						System.Type t_type_member = a_type.GetGenericArguments()[0];

						for(int ii=0;ii<a_jsonitem.GetListMax();ii++){
							JsonItem t_jsonitem_member = a_jsonitem.GetItem(ii);
							t_workpool.Add(new JsonToObject_Work(t_return_list,t_type_member,t_jsonitem_member));
						}
					}
				}
			}else if(a_jsonitem.IsAssociativeArray() == true){

				bool t_search_member = false;

				if(a_type.IsGenericType == true){
					if(a_type.GetGenericTypeDefinition() == typeof(Dictionary<,>)){
						//Dictionary

						t_return = System.Activator.CreateInstance(a_type);

						IDictionary t_return_dictionary = t_return as IDictionary;
						System.Type t_type_member = a_type.GetGenericArguments()[1];

						List<string> t_key_list = a_jsonitem.CreateAssociativeKeyList();
						for(int ii=0;ii<t_key_list.Count;ii++){
							JsonItem t_jsonitem_member = a_jsonitem.GetItem(t_key_list[ii]);
							t_workpool.Add(new JsonToObject_Work(t_return_dictionary,t_key_list[ii],t_type_member,t_jsonitem_member));
						}
					}else{
						t_search_member = true;
					}
				}else{
					t_search_member = true;
				}

				if(t_search_member == true){
					//struct,class

					System.Reflection.MemberInfo[] t_member = a_type.GetMembers(System.Reflection.BindingFlags.Public|System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance);

					t_return = System.Activator.CreateInstance(a_type);

					for(int ii=0;ii<t_member.Length;ii++){
						if(t_member[ii].MemberType == System.Reflection.MemberTypes.Field){
							System.Reflection.FieldInfo t_fieldinfo = t_member[ii] as System.Reflection.FieldInfo;
							if(t_fieldinfo != null){
								if((t_fieldinfo.Attributes == System.Reflection.FieldAttributes.Public)||(t_fieldinfo.Attributes == System.Reflection.FieldAttributes.Private)){
									if(a_jsonitem.IsExistItem(t_fieldinfo.Name) == true){
										JsonItem t_jsonitem_member = a_jsonitem.GetItem(t_fieldinfo.Name);
										System.Type t_type_member = t_fieldinfo.FieldType;

										if(t_jsonitem_member != null){
											if(t_type_member.IsClass == true){
												//t_returnはクラス遅延代入可能。
												t_workpool.Add(new JsonToObject_Work(t_fieldinfo,t_return,t_type_member,t_jsonitem_member));
											}else{
												//t_valueを完成させて
												System.Object t_value = JsonToObject_SystemObject.Convert(t_type_member,t_jsonitem_member);
												if(t_value != null){
													t_fieldinfo.SetValue(t_return,t_value);
												}
											}
										}else{
											Tool.Assert(false);
										}
									}
								}else{
									//オブジェクト化しない方型。
								}
							}else{
								Tool.Assert(false);
							}
						}
					}
				}
			}

			if(a_workpool == null){
				while(true){
					int t_count = t_workpool.Count;
					if(t_count > 0){
						JsonToObject_Work t_current_work = t_workpool[t_count - 1];
						t_workpool.RemoveAt(t_count - 1);
						t_current_work.Do(t_workpool);
					}else{
						break;
					}
				}
			}

			return t_return;
		}
	}
}

