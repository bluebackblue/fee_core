

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＪＳＯＮ。オブジェクト化。
*/


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** JsonToObject_SystemObject
	*/
	public class JsonToObject_SystemObject
	{
		/** インスタンス作成。
		*/
		public static System.Object CreateInstance(System.Type a_type,Fee.JsonItem.JsonItem a_jsonitem)
		{
			if(
				a_type == typeof(string) ||
				a_type == typeof(uint) ||
				a_type == typeof(ulong) ||
				a_type == typeof(int) ||
				a_type == typeof(long) ||
				a_type == typeof(float) ||
				a_type == typeof(double) ||
				a_type == typeof(bool)
			){
				return null;
			}else if(a_type.IsArray == true){
				int t_list_count = 0;
				if(a_jsonitem.IsIndexArray() == true){
					t_list_count = a_jsonitem.GetListMax();
				}

				System.Object t_return = null;

				try{
					System.Type t_element_type = a_type.GetElementType();
					t_return = System.Array.CreateInstance(t_element_type,t_list_count);
				}catch(System.Exception t_exception){
					Tool.LogError(t_exception);
				}

				return t_return;
			}else{
				System.Object t_return = null;

				try{
					t_return = System.Activator.CreateInstance(a_type);
				}catch(System.Exception t_exception){
					//引数なしconstructorの呼び出しに失敗。
					Tool.LogError(t_exception);
				}

				return t_return;
			}
		}

		/** Convert
		*/
		public static void Convert(ref System.Object a_to_object,System.Type a_type,JsonItem a_jsonitem,System.Collections.Generic.List<JsonToObject_Work> a_workpool = null)
		{
			System.Collections.Generic.List<JsonToObject_Work> t_workpool = a_workpool;

			if(t_workpool == null){
				t_workpool = new System.Collections.Generic.List<JsonToObject_Work>();
			}

			{
				System.Type t_type = a_type;

				if(a_jsonitem.IsStringData() == true){
					if(t_type == typeof(string)){
						//string
						a_to_object = a_jsonitem.GetStringData();
					}else if(t_type.IsEnum == true){
						//enum
						a_to_object = (System.Enum)(System.Enum.Parse(t_type,a_jsonitem.GetStringData()));
					}
				}else if(a_jsonitem.IsBoolData() == true){
					if(t_type == typeof(bool)){
						//bool
						a_to_object = a_jsonitem.GetBoolData();
					}
				}else if(a_jsonitem.IsSignedNumber() == true){
					if(t_type == typeof(int)){
						//int
						a_to_object = (int)a_jsonitem.GetInt();
					}else if(t_type == typeof(long)){
						//long
						a_to_object = (long)a_jsonitem.GetLong();
					}else if(t_type.IsEnum == true){
						//enum
						a_to_object = (System.Enum)(System.Enum.ToObject(t_type,a_jsonitem.GetInt()));
					}
				}else if(a_jsonitem.IsUnSignedNumber() == true){
					if(t_type == typeof(uint)){
						//uint
						a_to_object = (uint)a_jsonitem.GetUint();
					}else if(t_type == typeof(ulong)){
						//ulong
						a_to_object = (ulong)a_jsonitem.GetUlong();
					}
				}else if(a_jsonitem.IsFloatNumber() == true){
					if(t_type == typeof(float)){
						//float
						a_to_object = (float)a_jsonitem.GetFloat();
					}else if(t_type == typeof(double)){
						//double
						a_to_object = (double)a_jsonitem.GetDouble();
					} 
				}else if(a_jsonitem.IsIndexArray() == true){
					if(t_type.IsGenericType == true){
						if(t_type.GetGenericTypeDefinition() == typeof(System.Collections.Generic.List<>)){
							//List

							System.Collections.IList t_list = a_to_object as System.Collections.IList;
							System.Type t_type_member = t_type.GetGenericArguments()[0];

							for(int ii=0;ii<a_jsonitem.GetListMax();ii++){
								JsonItem t_jsonitem_member = a_jsonitem.GetItem(ii);

								System.Object t_object_member = JsonToObject_SystemObject.CreateInstance(t_type_member,t_jsonitem_member);

								if((t_object_member != null)&&(t_type_member.IsClass == true)){
									t_workpool.Add(new JsonToObject_Work(t_object_member,t_type_member,t_jsonitem_member));
								}else{
									JsonToObject_SystemObject.Convert(ref t_object_member,t_type_member,t_jsonitem_member);
								}

								t_list.Add(t_object_member);
							}
						}
					}else if(a_type.IsArray == true){
						//x[]

						System.Array t_list = a_to_object as System.Array;

						System.Type t_type_member = t_type.GetElementType();

						for(int ii=0;ii<a_jsonitem.GetListMax();ii++){
							JsonItem t_jsonitem_member = a_jsonitem.GetItem(ii);

							System.Object t_object_member = JsonToObject_SystemObject.CreateInstance(t_type_member,t_jsonitem_member);

							if((t_object_member != null)&&(t_type_member.IsClass == true)){
								t_workpool.Add(new JsonToObject_Work(t_object_member,t_type_member,t_jsonitem_member));
							}else{
								JsonToObject_SystemObject.Convert(ref t_object_member,t_type_member,t_jsonitem_member);
							}

							t_list.SetValue(t_object_member,ii);
						}

					}
				}else if(a_jsonitem.IsAssociativeArray() == true){
					//struct,class,Dictionary
					bool t_search_member = true;

					if(t_type.IsGenericType == true){
						if(t_type.GetGenericTypeDefinition() == typeof(System.Collections.Generic.Dictionary<,>)){
							//Dictionary

							System.Collections.IDictionary t_list = a_to_object as System.Collections.IDictionary;
							System.Type t_type_member = t_type.GetGenericArguments()[1];

							System.Collections.Generic.List<string> t_key_list = a_jsonitem.CreateAssociativeKeyList();
							for(int ii=0;ii<t_key_list.Count;ii++){
								JsonItem t_jsonitem_member = a_jsonitem.GetItem(t_key_list[ii]);

								System.Object t_object_member = JsonToObject_SystemObject.CreateInstance(t_type_member,t_jsonitem_member);

								if((t_object_member != null)&&(t_type_member.IsClass == true)){
									t_workpool.Add(new JsonToObject_Work(t_object_member,t_type_member,t_jsonitem_member));
								}else{
									JsonToObject_SystemObject.Convert(ref t_object_member,t_type_member,t_jsonitem_member);
								}

								t_list.Add(t_key_list[ii],t_object_member);
							}

							//no support dictionary
							t_search_member = false;
						}
					}

					if(t_search_member == true){
						//struct,class

						System.Reflection.MemberInfo[] t_member = t_type.GetMembers(System.Reflection.BindingFlags.Public|System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance);

						for(int ii=0;ii<t_member.Length;ii++){
							if(t_member[ii].MemberType == System.Reflection.MemberTypes.Field){
								System.Reflection.FieldInfo t_fieldinfo = t_member[ii] as System.Reflection.FieldInfo;
								if(t_fieldinfo != null){
									if(t_fieldinfo.IsDefined(typeof(Fee.JsonItem.Ignore),false) == true){
										//無視する。
									}else{
										if((t_fieldinfo.Attributes == System.Reflection.FieldAttributes.Public)||(t_fieldinfo.Attributes == System.Reflection.FieldAttributes.Private)){
											if(a_jsonitem.IsExistItem(t_fieldinfo.Name) == true){
												System.Type t_type_member = t_fieldinfo.FieldType;

												JsonItem t_jsonitem_member = a_jsonitem.GetItem(t_fieldinfo.Name);

												System.Object t_object_member = JsonToObject_SystemObject.CreateInstance(t_type_member,t_jsonitem_member);

												if((t_object_member != null)&&(t_type_member.IsClass == true)){
													t_workpool.Add(new JsonToObject_Work(t_object_member,t_type_member,t_jsonitem_member));
												}else{
													JsonToObject_SystemObject.Convert(ref t_object_member,t_type_member,t_jsonitem_member);
												}

												t_fieldinfo.SetValue(a_to_object,t_object_member);
											}else{
												//ＪＳＯＮ側には存在しない。
											}
										}else{
											//オブジェクト化しない方型。
										}
									}
								}else{
									Tool.Assert(false);
								}
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
		}
	}
}

