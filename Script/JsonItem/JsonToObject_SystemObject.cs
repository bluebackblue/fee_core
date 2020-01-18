

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
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
				a_type == typeof(bool) ||
				a_type == typeof(char) ||
				a_type == typeof(float) ||
				a_type == typeof(double) ||
				a_type == typeof(decimal) ||
				a_type == typeof(sbyte) ||
				a_type == typeof(byte) ||
				a_type == typeof(short) ||
				a_type == typeof(ushort) ||
				a_type == typeof(int) ||
				a_type == typeof(uint) ||
				a_type == typeof(long) ||
				a_type == typeof(ulong) ||

				a_type == typeof(string)
			){
				//値。

				return null;
			}else if(a_type.IsArray == true){
				//配列。

				int t_list_count = 0;
				if(a_jsonitem.IsIndexArray() == true){
					t_list_count = a_jsonitem.GetListMax();
				}

				System.Object t_return = null;

				try{
					System.Type t_element_type = a_type.GetElementType();
					t_return = System.Array.CreateInstance(t_element_type,t_list_count);
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}

				return t_return;
			}else{
				//インスタンス。

				System.Object t_return = null;

				if(a_jsonitem.IsNull() == true){
					//NULL処理。
				}else{
					try{
						t_return = System.Activator.CreateInstance(a_type);
					}catch(System.Exception t_exception){
						//引数なしconstructorの呼び出しに失敗。
						Tool.DebugReThrow(t_exception);
					}
				}

				return t_return;
			}
		}

		/** Convert
		*/
		public static void Convert(ref System.Object a_to_object_ref,System.Type a_type,JsonItem a_jsonitem,int a_nest,System.Collections.Generic.LinkedList<JsonToObject_Work> a_workpool = null)
		{
			int t_nest = a_nest + 1;
			if(t_nest >= 5){
				Tool.Log("JsonToObject_SystemObject","nest = " + t_nest.ToString());
			}

			System.Collections.Generic.LinkedList<JsonToObject_Work> t_workpool = a_workpool;

			if(t_workpool == null){
				t_workpool = new System.Collections.Generic.LinkedList<JsonToObject_Work>();
			}

			{
				System.Type t_type = a_type;

				if((a_jsonitem.IsSignedNumber() == true)||(a_jsonitem.IsUnSignedNumber() == true)||(a_jsonitem.IsFloatNumber() == true)||(a_jsonitem.IsBoolData() == true)){
					if(t_type == typeof(int)){
						//20:int
						a_to_object_ref = (int)a_jsonitem.GetInt();
					}else if(t_type == typeof(float)){
						//13:float
						a_to_object_ref = (float)a_jsonitem.GetFloat();
					}else if(t_type == typeof(bool)){
						//11:bool
						a_to_object_ref = (bool)a_jsonitem.GetBoolData();
					}else if(t_type.IsEnum == true){
						//enum
						a_to_object_ref = (System.Enum)(System.Enum.ToObject(t_type,a_jsonitem.GetInt()));
					}else if(t_type == typeof(long)){
						//22:long
						a_to_object_ref = (long)a_jsonitem.GetLong();
					}else if(t_type == typeof(char)){
						//12:char
						a_to_object_ref = (char)a_jsonitem.GetChar();
					}else if(t_type == typeof(double)){
						//14:double
						a_to_object_ref = (double)a_jsonitem.GetDouble();
					}else if(t_type == typeof(decimal)){
						//15:decimal
						a_to_object_ref = (decimal)a_jsonitem.GetDecimal();
					}else if(t_type == typeof(sbyte)){
						//16:sbyte
						a_to_object_ref = (sbyte)a_jsonitem.GetSbyte();
					}else if(t_type == typeof(byte)){
						//17:byte
						a_to_object_ref = (byte)a_jsonitem.GetByte();
					}else if(t_type == typeof(short)){
						//18:short
						a_to_object_ref = (short)a_jsonitem.GetShort();
					}else if(t_type == typeof(ushort)){
						//19:ushort
						a_to_object_ref = (ushort)a_jsonitem.GetUshort();
					}else if(t_type == typeof(uint)){
						//21:uint
						a_to_object_ref = (uint)a_jsonitem.GetUint();
					}else if(t_type == typeof(ulong)){
						//23:ulong
						a_to_object_ref = (ulong)a_jsonitem.GetUlong();
					}
				}else if(a_jsonitem.IsStringData() == true){
					if(t_type == typeof(string)){
						//string
						a_to_object_ref = a_jsonitem.GetStringData();
					}else if(t_type.IsEnum == true){
						//enum
						a_to_object_ref = (System.Enum)(System.Enum.Parse(t_type,a_jsonitem.GetStringData()));
					}
				}else if(a_jsonitem.IsIndexArray() == true){

					do{
						System.Collections.ICollection t_collection = a_to_object_ref as System.Collections.ICollection;
						if(t_collection != null){

							//IList
							{
								System.Collections.IList t_list = t_collection as System.Collections.IList;
								if(t_list != null){
									System.Type t_type_member = t_type.GetGenericArguments()[0];

									for(int ii=0;ii<a_jsonitem.GetListMax();ii++){
										JsonItem t_jsonitem_member = a_jsonitem.GetItem(ii);

										if(t_type_member.IsClass == true){
											//インスタンス作成。
											System.Object t_object_member = JsonToObject_SystemObject.CreateInstance(t_type_member,t_jsonitem_member);

											//中身の作成は後回し。
											t_workpool.AddFirst(new JsonToObject_Work(JsonToObject_Work.ModeConvertOnly.Value,t_object_member,t_type_member,t_jsonitem_member));

											//リストに追加。
											t_list.Add(t_object_member);
										}else{
											//インスタンス作成。
											System.Object t_object_member = JsonToObject_SystemObject.CreateInstance(t_type_member,t_jsonitem_member);

											//■メンバーの設定。
											JsonToObject_SystemObject.Convert(ref t_object_member,t_type_member,t_jsonitem_member,t_nest);

											//リストに追加。
											t_list.Add(t_object_member);
										}
									}

									//doの外へ。
									break;
								}
							}

							{
								//LinkedList
								//HashSet
							}

						}

						//doの外へ。
						break;
					}while(false);

				}else if(a_jsonitem.IsAssociativeArray() == true){

					do{

						//dictionary
						if(t_type.IsGenericType == true){
							if(t_type.GetGenericTypeDefinition() == typeof(System.Collections.Generic.Dictionary<,>)){
								if(t_type.GetGenericArguments()[0] == typeof(string)){
									//Dictionary<string,X>

									System.Collections.IDictionary t_list = a_to_object_ref as System.Collections.IDictionary;
									System.Type t_value_type = t_type.GetGenericArguments()[1];
									System.Collections.Generic.List<string> t_key_list = a_jsonitem.CreateAssociativeKeyList();

									foreach(string t_key_name in t_key_list){
										JsonItem t_jsonitem_member = a_jsonitem.GetItem(t_key_name);

										if(t_value_type.IsClass == true){

											//インスタンス作成。
											System.Object t_object_member = JsonToObject_SystemObject.CreateInstance(t_value_type,t_jsonitem_member);

											//中身の作成は後回し。
											t_workpool.AddFirst(new JsonToObject_Work(JsonToObject_Work.ModeConvertOnly.Value,t_object_member,t_value_type,t_jsonitem_member));

											//リストに追加。
											t_list.Add(t_key_name,t_object_member);

										}else{

											//インスタンス作成。
											System.Object t_object_member = JsonToObject_SystemObject.CreateInstance(t_value_type,t_jsonitem_member);

											//■メンバーの設定。
											JsonToObject_SystemObject.Convert(ref t_object_member,t_value_type,t_jsonitem_member,t_nest);

											//リストに追加。
											t_list.Add(t_key_name,t_object_member);

										}
									}

									//doの外へ。
									break;
								}
							}
						}
								
						//class,struct
						{
							while(true){
							
								if(t_type == null){
									break;
								}else if(t_type == typeof(System.Object)){
									break;
								}

								System.Reflection.MemberInfo[] t_member_list = t_type.GetMembers(
								
									//指定した型の階層のレベルで宣言されたメンバーのみを対象にすることを指定します。 継承されたメンバーは対象になりません。
									System.Reflection.BindingFlags.DeclaredOnly |
							
									//インスタンス メンバーを検索に含めることを指定します。
									System.Reflection.BindingFlags.Instance |
							
									//パブリック メンバーを検索に含めることを指定します。
									System.Reflection.BindingFlags.Public |
							
									//パブリック メンバー以外のメンバーを検索に含めることを指定します。
									System.Reflection.BindingFlags.NonPublic
							
								);

								foreach(System.Reflection.MemberInfo t_memberinfo in t_member_list){
									if(t_memberinfo.MemberType == System.Reflection.MemberTypes.Field){
										System.Reflection.FieldInfo t_fieldinfo = t_memberinfo as System.Reflection.FieldInfo;
										if(t_fieldinfo != null){
							
											if(t_fieldinfo.IsDefined(typeof(Fee.JsonItem.Ignore),false) == true){
												//オブジェクト化しない。
												continue;
											}
							
											System.Type t_field_type = t_fieldinfo.FieldType;
							
											if((t_field_type == typeof(System.IntPtr))||(t_field_type == typeof(System.UIntPtr))){
												//オブジェクト化しない。
												continue;
											}

											if(a_jsonitem.IsExistItem(t_fieldinfo.Name) == true){
												System.Type t_type_member = t_fieldinfo.FieldType;

												JsonItem t_jsonitem_member = a_jsonitem.GetItem(t_fieldinfo.Name);

												//全部後回し。
												t_workpool.AddFirst(new JsonToObject_Work(JsonToObject_Work.ModeCreateInstance.Value,t_fieldinfo,a_to_object_ref,t_jsonitem_member));

											}else{
												//ＪＳＯＮ側には存在しない。
											}
										}else{
											Tool.Assert(false);
										}
									}
								}
							
								//次の継承元へ。
								t_type = t_type.BaseType;
							}

							//doの外へ。
							break;
						}

						//break;
					}while(false);
				}
			}

			if(a_workpool == null){
				while(true){
					int t_count = t_workpool.Count;
					if(t_count > 0){
						JsonToObject_Work t_current_work = t_workpool.First.Value;
						t_workpool.RemoveFirst();
						t_current_work.Do(t_nest,t_workpool);

						//たぶん無限ループ。
						if(t_count > Config.POOL_MAX){
							t_workpool.Clear();
							Tool.Assert(false);
						}

					}else{
						break;
					}
				}
			}
		}
	}
}

