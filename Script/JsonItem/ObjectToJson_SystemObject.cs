

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。ＪＳＯＮ化。
*/


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** ObjectToJson_SystemObject
	*/
	public class ObjectToJson_SystemObject
	{
		/** Convert
		*/
		public static JsonItem Convert(System.Object a_instance,ObjectToJson_Work.ObjectOption a_objectoption,int a_nest,System.Collections.Generic.List<ObjectToJson_Work> a_workpool = null)
		{
			System.Collections.Generic.List<ObjectToJson_Work> t_workpool = a_workpool;

			if(t_workpool == null){
				t_workpool = new System.Collections.Generic.List<ObjectToJson_Work>();				
			}

			JsonItem t_return = null;

			if(a_instance != null){
				System.Type t_type = a_instance.GetType();

				if(t_type == typeof(int)){
					//20:int
					int t_value_raw = (int)a_instance;
					t_return = new JsonItem(new Value_Number<int>(t_value_raw));
				}else if(t_type == typeof(float)){
					//13:float
					float t_value_raw = (float)a_instance;
					t_return = new JsonItem(new Value_Number<float>(t_value_raw));
				}else if(t_type == typeof(bool)){
					//11:bool
					bool t_value_raw = (bool)a_instance;
					t_return = new JsonItem(new Value_Number<bool>(t_value_raw));
				}else if(t_type == typeof(long)){
					//22:long
					long t_value_raw = (long)a_instance;
					t_return = new JsonItem(new Value_Number<long>(t_value_raw));
				}else if(t_type == typeof(char)){
					//12:char
					char t_value_raw = (char)a_instance;
					t_return = new JsonItem(new Value_Number<char>(t_value_raw));
				}else if(t_type == typeof(double)){
					//14:double
					double t_value_raw = (double)a_instance;
					t_return = new JsonItem(new Value_Number<double>(t_value_raw));
				}else if(t_type == typeof(decimal)){
					//15:decimal
					decimal t_value_raw = (decimal)a_instance;
					t_return = new JsonItem(new Value_Number<decimal>(t_value_raw));
				}else if(t_type == typeof(sbyte)){
					//16:sbyte
					sbyte t_value_raw = (sbyte)a_instance;
					t_return = new JsonItem(new Value_Number<sbyte>(t_value_raw));
				}else if(t_type == typeof(byte)){
					//17:byte
					byte t_value_raw = (byte)a_instance;
					t_return = new JsonItem(new Value_Number<byte>(t_value_raw));
				}else if(t_type == typeof(short)){
					//18:short
					short t_value_raw = (short)a_instance;
					t_return = new JsonItem(new Value_Number<short>(t_value_raw));
				}else if(t_type == typeof(ushort)){
					//19:ushort
					ushort t_value_raw = (ushort)a_instance;
					t_return = new JsonItem(new Value_Number<ushort>(t_value_raw));
				}else if(t_type == typeof(uint)){
					//21:uint
					uint t_value_raw = (uint)a_instance;
					t_return = new JsonItem(new Value_Number<uint>(t_value_raw));
				}else if(t_type == typeof(ulong)){
					//23:ulong
					ulong t_value_raw = (ulong)a_instance;
					t_return = new JsonItem(new Value_Number<ulong>(t_value_raw));
				}else if(t_type == typeof(string)){
					//2:文字データ。

					string t_value_raw = a_instance as string;

					if(t_value_raw != null){

						t_return = new JsonItem(new Value_StringData(t_value_raw));

					}else{
						//NULL処理。

						t_return = null;
						//t_return = new JsonItem();
					}
				}else if(t_type.IsArray == true){
					//x[]

					System.Array t_array_raw = (System.Array)a_instance;
	
					JsonItem t_jsonitem = new JsonItem(new Value_IndexArray());
					t_jsonitem.ReSize(t_array_raw.Length);
					for(int ii=0;ii<t_array_raw.Length;ii++){
						System.Object t_list_item_raw = t_array_raw.GetValue(ii);
						t_workpool.Add(new ObjectToJson_Work(ObjectToJson_Work.ModeIndexArray.Value,t_list_item_raw,null,ii,t_jsonitem,a_nest + 1));
					}

					t_return = t_jsonitem;
				}else if(t_type.IsEnum == true){
					//enum

					bool t_string_mode = false;
					if(a_objectoption != null){
						if(a_objectoption.attribute_enumstring == true){
							t_string_mode = true;
						}
					}

					if(t_string_mode == true){
						//enumの文字列化。

						string t_value_raw = a_instance.ToString();

						if(t_value_raw != null){
							t_return = new JsonItem(new Value_StringData(t_value_raw));
						}else{
							//NULL処理。

							t_return = null;
							//t_return = new JsonItem();
						}
					}else{
						//enumの数値化。

						int t_value_raw = (int)a_instance;
						t_return = new JsonItem(new Value_Number<int>(t_value_raw));
					}
				}else{

					do{

						{
							//IEnumerable
							{
								System.Collections.IEnumerable t_instance_enumerable = a_instance as System.Collections.IEnumerable;
								if(t_instance_enumerable != null){

									//ICollection
									{
										System.Collections.ICollection t_instance_collection = t_instance_enumerable as System.Collections.ICollection;
										if(t_instance_collection != null){

											//IDictionary
											{
												System.Collections.IDictionary t_instance_dictionary = t_instance_collection as System.Collections.IDictionary;
												if(t_instance_dictionary != null){
													System.Collections.Generic.ICollection<string> t_collection = t_instance_dictionary.Keys as System.Collections.Generic.ICollection<string>;
													if(t_collection != null){

														JsonItem t_jsonitem = new JsonItem(new Value_AssociativeArray());
														foreach(string t_key_string in t_collection){
															if(t_key_string != null){
																System.Object t_list_item_raw = t_instance_dictionary[t_key_string];
																t_workpool.Add(new ObjectToJson_Work(ObjectToJson_Work.AssociativeArray.Value,t_list_item_raw,null,t_key_string,t_jsonitem,a_nest + 1));
															}else{
																//NULL処理。

																//t_key_stringがnullの場合は追加しない。
															}
														}

														//doの外へ。
														t_return = t_jsonitem;
														break;
													}

													/*
													//対応していないDictionary。
													JsonItem t_jsonitem = new JsonItem(new Value_AssociativeArray());

													//doの外へ。
													t_return = t_jsonitem;
													break;
													*/
												}
											}

											//ICollection
											{
												JsonItem t_jsonitem = new JsonItem(new Value_IndexArray());
												t_jsonitem.ReSize(t_instance_collection.Count);

												int t_index = 0;
												foreach(System.Object t_item in t_instance_collection){
													t_workpool.Add(new ObjectToJson_Work(ObjectToJson_Work.ModeIndexArray.Value,t_item,null,t_index,t_jsonitem,a_nest + 1));
													t_index++;
												}

												//doの外へ。
												t_return = t_jsonitem;
												break;
											}
										}
									}

									//IEnumerable
									{
										JsonItem t_jsonitem = new JsonItem(new Value_IndexArray());

										int t_count = 0;
										foreach(System.Object t_item in t_instance_enumerable){
											t_count++;
										}

										t_jsonitem.ReSize(t_count);

										int t_index = 0;
										foreach(System.Object t_item in t_instance_enumerable){
											t_workpool.Add(new ObjectToJson_Work(ObjectToJson_Work.ModeIndexArray.Value,t_item,null,t_index,t_jsonitem,a_nest + 1));
											t_index++;
										}

										//doの外へ。
										t_return = t_jsonitem;
										break;
									}
								}
							}
						}

						//class,struct
						{
							JsonItem t_jsonitem = new JsonItem(new Value_AssociativeArray());
							
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
												//ＪＳＯＮ化しない。
												continue;
											}
							
											System.Type t_field_type = t_fieldinfo.FieldType;
							
											if((t_field_type == typeof(System.IntPtr))||(t_field_type == typeof(System.UIntPtr))){
												//ＪＳＯＮ化しない。
												continue;
											}
							
											ObjectToJson_Work.ObjectOption t_objectoption = null;
							
											//ＥＮＵＭの文字列化。
											if(t_fieldinfo.IsDefined(typeof(Fee.JsonItem.EnumString),false) == true){
												t_objectoption = new ObjectToJson_Work.ObjectOption();
												t_objectoption.attribute_enumstring = true;
											}
							
											{
												System.Object t_raw = t_fieldinfo.GetValue(a_instance);
												if(t_raw != null){
													t_workpool.Add(new ObjectToJson_Work(ObjectToJson_Work.AssociativeArray.Value,t_raw,t_objectoption,t_fieldinfo.Name,t_jsonitem,a_nest + 1));
												}else{
													//NULL処理。
							
													//t_return = new JsonItem();
												}
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
							t_return = t_jsonitem;
							break;
						}

						//break;
					}while(false);


				}
			}

			//再起呼び出し。
			if(a_workpool == null){
				while(true){
					int t_count = t_workpool.Count;
					if(t_count > 0){
						ObjectToJson_Work t_current_work = t_workpool[t_count - 1];
						t_workpool.RemoveAt(t_count - 1);
						t_current_work.Do(t_workpool);

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

			return t_return;
		}
	}
}

