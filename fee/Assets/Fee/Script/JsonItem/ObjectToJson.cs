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
	/** ObjectToJson
	*/
	public class ObjectToJson
	{
		/** Convert
		*/
		public static JsonItem Convert(System.Object a_instance,List<ObjectToJson_Work> a_workpool = null)
		{
			List<ObjectToJson_Work> t_workpool = a_workpool;

			if(t_workpool == null){
				t_workpool = new List<ObjectToJson_Work>();				
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
				
					t_return = new JsonItem(new Value_Int(t_value_raw));
				}else if(t_type == typeof(long)){
					//long

					long t_value_raw = (long)a_instance;

					t_return = new JsonItem(new Value_Long(t_value_raw));
				}else if(t_type == typeof(float)){
					//float

					float t_value_raw = (float)a_instance;

					t_return = new JsonItem(new Value_Float(t_value_raw));
				}else if(t_type == typeof(double)){
					//double

					double t_value_raw = (double)a_instance;

					t_return = new JsonItem(new Value_Double(t_value_raw));
				}else if(t_type.IsGenericType == true){
					System.Type t_type_g = t_type.GetGenericTypeDefinition();

					if(t_type_g == typeof(List<>)){
						//List

						IList t_value_raw = a_instance as IList;
						if(t_value_raw != null){

							JsonItem t_jsonitem = new JsonItem(new Value_IndexArray());
							for(int ii=0;ii<t_value_raw.Count;ii++){
								if(t_value_raw[ii] != null){
									System.Object t_list_item_raw = t_value_raw[ii];
									t_workpool.Add(new ObjectToJson_Work(t_list_item_raw,ii,t_jsonitem));
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
							ICollection<string> t_collection = t_value_raw.Keys as ICollection<string>;
							if(t_collection != null){

								JsonItem t_jsonitem = new JsonItem(new Value_AssociativeArray());
								foreach(string t_key_string in t_collection){
									if(t_key_string != null){
										System.Object t_list_item_raw = t_value_raw[t_key_string];
										if(t_list_item_raw != null){
											t_workpool.Add(new ObjectToJson_Work(t_list_item_raw,t_key_string,t_jsonitem));
										}
									}else{
										//nullの場合は追加しない。
									}
								}

								t_return = t_jsonitem;
							}else{
								//keyの型がstring以外のものは追加しない。
								t_return = null;
							}
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
										t_workpool.Add(new ObjectToJson_Work(t_raw,t_fieldinfo.Name,t_jsonitem));

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
			if(a_workpool == null){
				while(true){
					int t_count = t_workpool.Count;
					if(t_count > 0){
						ObjectToJson_Work t_current_work = t_workpool[t_count - 1];
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

