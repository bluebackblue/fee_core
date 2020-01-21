

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
		public static System.Type GetTypeFromJsonItem(Fee.JsonItem.JsonItem a_jsonitem)
		{
			System.Type t_type = null;

			if(a_jsonitem.IsSignedNumber()){
				//12:char
				//15:decimal
				//16:sbyte
				//17:byte
				//18:short
				//19:ushort
				//20:int
				//21:uint
				//22:long
				t_type = typeof(long);
			}else if(a_jsonitem.IsUnSignedNumber()){
				//23:ulong
				t_type = typeof(ulong);
			}else if(a_jsonitem.IsFloatNumber()){
				//13:float
				//14:double
				t_type = typeof(double);
			}else if(a_jsonitem.IsBoolData()){
				//11:bool。
				t_type = typeof(bool);
			}else if(a_jsonitem.IsStringData()){
				//2:文字データ。
				t_type = typeof(string);
			}else if(a_jsonitem.IsIndexArray() == true){
				//1:インデックス配列。
				t_type = typeof(System.Collections.ArrayList);
			}else if(a_jsonitem.IsAssociativeArray() == true){
				//0:連想配列。
				t_type = typeof(System.Collections.Generic.Dictionary<string,System.Collections.ArrayList>);
			}else if(a_jsonitem.IsNull()){
				//24:null
				t_type = typeof(System.Object);
			}else if(a_jsonitem.IsBinaryData()){
				//3:バイナリデータ。
				t_type = typeof(System.Collections.Generic.List<byte>);
			}

			return t_type;
		}


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

				if(a_jsonitem.IsStringData() == true){
					if(t_type == typeof(string)){
						//stringdata => string
						a_to_object_ref = a_jsonitem.GetStringData();
					}else if(t_type.IsEnum == true){
						//stringdata => enum
						a_to_object_ref = System.Enum.Parse(t_type,a_jsonitem.GetStringData());
					}else if(t_type == typeof(int)){
						//stringdata => int
						int t_value;
						if(int.TryParse(a_jsonitem.GetStringData(),out t_value) == true){
							a_to_object_ref = t_value;
						}
					}else if(t_type == typeof(float)){
						//stringdata => float
						float t_value;
						if(float.TryParse(a_jsonitem.GetStringData(),Config.STRING_TO_DOBULE_NUMBERSTYLE,Config.CULTURE,out t_value) == true){
							a_to_object_ref = t_value;
						}
					}else if(t_type == typeof(bool)){
						//stringdata => bool
						bool t_value;
						if(bool.TryParse(a_jsonitem.GetStringData(),out t_value) == true){
							a_to_object_ref = t_value;
						}
					}else if(t_type == typeof(long)){
						//stringdata => long
						long t_value;
						if(long.TryParse(a_jsonitem.GetStringData(),out t_value) == true){
							a_to_object_ref = t_value;
						}
					}else if(t_type == typeof(char)){
						//stringdata => char
						char t_value;
						if(char.TryParse(a_jsonitem.GetStringData(),out t_value) == true){
							a_to_object_ref = t_value;
						}
					}else if(t_type == typeof(double)){
						//stringdata => double
						double t_value;
						if(double.TryParse(a_jsonitem.GetStringData(),Config.STRING_TO_DOBULE_NUMBERSTYLE,Config.CULTURE,out t_value) == true){
							a_to_object_ref = t_value;
						}
					}else if(t_type == typeof(decimal)){
						//stringdata => decimal
						decimal t_value;
						if(decimal.TryParse(a_jsonitem.GetStringData(),out t_value) == true){
							a_to_object_ref = t_value;
						}
					}else if(t_type == typeof(sbyte)){
						//stringdata => sbyte
						sbyte t_value;
						if(sbyte.TryParse(a_jsonitem.GetStringData(),out t_value) == true){
							a_to_object_ref = t_value;
						}
					}else if(t_type == typeof(byte)){
						//stringdata => byte
						byte t_value;
						if(byte.TryParse(a_jsonitem.GetStringData(),out t_value) == true){
							a_to_object_ref = t_value;
						}
					}else if(t_type == typeof(short)){
						//stringdata => short
						short t_value;
						if(short.TryParse(a_jsonitem.GetStringData(),out t_value) == true){
							a_to_object_ref = t_value;
						}
					}else if(t_type == typeof(ushort)){
						//stringdata => ushort
						ushort t_value;
						if(ushort.TryParse(a_jsonitem.GetStringData(),out t_value) == true){
							a_to_object_ref = t_value;
						}
					}else if(t_type == typeof(uint)){
						//stringdata => uint
						uint t_value;
						if(uint.TryParse(a_jsonitem.GetStringData(),out t_value) == true){
							a_to_object_ref = t_value;
						}
					}else if(t_type == typeof(ulong)){
						//stringdata => ulong
						ulong t_value;
						if(ulong.TryParse(a_jsonitem.GetStringData(),out t_value) == true){
							a_to_object_ref = t_value;
						}
					}
				}else if((a_jsonitem.IsSignedNumber() == true)||(a_jsonitem.IsUnSignedNumber() == true)||(a_jsonitem.IsFloatNumber() == true)||(a_jsonitem.IsBoolData() == true)){
					if(t_type == typeof(int)){
						//number => int
						a_to_object_ref = (int)a_jsonitem.GetInt();
					}else if(t_type == typeof(float)){
						//number => float
						a_to_object_ref = (float)a_jsonitem.GetFloat();
					}else if(t_type == typeof(bool)){
						//number => bool
						a_to_object_ref = (bool)a_jsonitem.GetBoolData();
					}else if(t_type == typeof(long)){
						//number => long
						a_to_object_ref = (long)a_jsonitem.GetLong();
					}else if(t_type == typeof(char)){
						//number => char
						a_to_object_ref = (char)a_jsonitem.GetChar();
					}else if(t_type == typeof(double)){
						//number => double
						a_to_object_ref = (double)a_jsonitem.GetDouble();
					}else if(t_type == typeof(decimal)){
						//number => decimal
						a_to_object_ref = (decimal)a_jsonitem.GetDecimal();
					}else if(t_type == typeof(sbyte)){
						//number => sbyte
						a_to_object_ref = (sbyte)a_jsonitem.GetSbyte();
					}else if(t_type == typeof(byte)){
						//number => byte
						a_to_object_ref = (byte)a_jsonitem.GetByte();
					}else if(t_type == typeof(short)){
						//number => short
						a_to_object_ref = (short)a_jsonitem.GetShort();
					}else if(t_type == typeof(ushort)){
						//number => ushort
						a_to_object_ref = (ushort)a_jsonitem.GetUshort();
					}else if(t_type == typeof(uint)){
						//number => unit
						a_to_object_ref = (uint)a_jsonitem.GetUint();
					}else if(t_type == typeof(ulong)){
						//number => uloong
						a_to_object_ref = (ulong)a_jsonitem.GetUlong();
					}else if(t_type.IsEnum == true){
						//number => enum

						System.TypeCode t_typecode = ((System.Enum)a_to_object_ref).GetTypeCode();
						switch(t_typecode){
						case System.TypeCode.Byte:
							{
								a_to_object_ref = System.Enum.ToObject(t_type,a_jsonitem.GetByte());
							}break;
						case System.TypeCode.SByte:
							{
								a_to_object_ref = System.Enum.ToObject(t_type,a_jsonitem.GetSbyte());
							}break;
						case System.TypeCode.Int16:
							{
								a_to_object_ref = System.Enum.ToObject(t_type,a_jsonitem.GetShort());
							}break;
						case System.TypeCode.UInt16:
							{
								a_to_object_ref = System.Enum.ToObject(t_type,a_jsonitem.GetUshort());
							}break;
						case System.TypeCode.Int32:
							{
								a_to_object_ref = System.Enum.ToObject(t_type,a_jsonitem.GetInt());
							}break;
						case System.TypeCode.UInt32:
							{
								a_to_object_ref = System.Enum.ToObject(t_type,a_jsonitem.GetUint());
							}break;
						case System.TypeCode.Int64:
							{
								a_to_object_ref = System.Enum.ToObject(t_type,a_jsonitem.GetLong());
							}break;
						case System.TypeCode.UInt64:
							{
								a_to_object_ref = System.Enum.ToObject(t_type,a_jsonitem.GetUlong());
							}break;
						default:
							{
								Tool.Assert(false);
							}break;
						}
					}				
				}else if(a_jsonitem.IsIndexArray() == true){
					do{
						System.Collections.IEnumerable t_enumerable = a_to_object_ref as System.Collections.IEnumerable;
						if(t_enumerable != null){
							System.Collections.ICollection t_collection = t_enumerable as System.Collections.ICollection;
							if(t_collection != null){

								//IList
								{
									System.Collections.IList t_list = t_collection as System.Collections.IList;
									if(t_list != null){

										//リスト型の値型。取得。
										System.Type t_listitem_valuetype = Fee.ReflectionTool.Utility.GetListValueType(t_type);

										if(t_list.IsFixedSize == true){
											//indexarray => []

											for(int ii=a_jsonitem.GetListMax()-1;ii>=0;ii--){

												//ワークに追加。
												JsonItem t_jsonitem_listitem = a_jsonitem.GetItem(ii);
												t_workpool.AddFirst(new JsonToObject_Work(JsonToObject_Work.ModeSetList.Start,t_jsonitem_listitem,t_list,ii,t_listitem_valuetype));
											}
										}else{
											//indexarray => Generic.List

											for(int ii=a_jsonitem.GetListMax()-1;ii>=0;ii--){

												//ワークに追加。
												JsonItem t_jsonitem_listitem = a_jsonitem.GetItem(ii);
												t_workpool.AddFirst(new JsonToObject_Work(JsonToObject_Work.ModeAddList.Start,t_jsonitem_listitem,t_list,t_listitem_valuetype));
											}
										}

										//doの外へ。
										break;
									}
								}
							}

							//IEnumerable
							{
								System.Type t_generic_type = Fee.ReflectionTool.Utility.GetGenericTypeDefinition(t_type);

								//リスト型の値型。取得。
								System.Type t_listitem_valuetype = Fee.ReflectionTool.Utility.GetListValueType(t_type);

								//メソッド取得。
								System.Reflection.MethodInfo t_methodinfo = null;
								if(t_generic_type == typeof(System.Collections.Generic.Stack<>)){
									//indexarray => Generic.Stack
									t_methodinfo = ConvertTool.GetMethod_Stack_Push(t_type,t_listitem_valuetype);

									if(t_methodinfo != null){
										for(int ii=0;ii<a_jsonitem.GetListMax();ii++){

											//ワークに追加。
											JsonItem t_jsonitem_listitem = a_jsonitem.GetItem(ii);
											t_workpool.AddFirst(new JsonToObject_Work(JsonToObject_Work.ModeIEnumerable.Start_Param1,t_jsonitem_listitem,t_enumerable,t_methodinfo,t_listitem_valuetype));
										}

										//doの外へ。
										break;
									}
								}else if(t_generic_type == typeof(System.Collections.Generic.LinkedList<>)){
									//indexarray => Generic.LinkedList
									t_methodinfo = ConvertTool.GetMethod_LinkedList_AddLast(t_type,t_listitem_valuetype);
								}else if(t_generic_type == typeof(System.Collections.Generic.HashSet<>)){
									//indexarray => Generic.HashSet
									t_methodinfo = ConvertTool.GetMethod_HashSet_Add(t_type,t_listitem_valuetype);
								}else if(t_generic_type == typeof(System.Collections.Generic.Queue<>)){
									//indexarray => Generic.Queue
									t_methodinfo = ConvertTool.GetMethod_Queue_Enqueue(t_type,t_listitem_valuetype);
								}else if(t_generic_type == typeof(System.Collections.Generic.SortedSet<>)){
									//indexarray => Generic.SortedSet
									t_methodinfo = ConvertTool.GetMethod_SortedSet_Add(t_type,t_listitem_valuetype);
								}

								if(t_methodinfo != null){
									for(int ii=a_jsonitem.GetListMax()-1;ii>=0;ii--){

										//ワークに追加。
										JsonItem t_jsonitem_listitem = a_jsonitem.GetItem(ii);
										t_workpool.AddFirst(new JsonToObject_Work(JsonToObject_Work.ModeIEnumerable.Start_Param1,t_jsonitem_listitem,t_enumerable,t_methodinfo,t_listitem_valuetype));
									}

									//doの外へ。
									break;
								}
							}
						}

						//doの外へ。
						break;
					}while(false);

					//ここがdoの外。
				}else if(a_jsonitem.IsAssociativeArray() == true){

					do{

						//IDictionary
						{
							System.Collections.IDictionary t_to_dictionary = a_to_object_ref as System.Collections.IDictionary;
							if(t_to_dictionary != null){

								System.Type t_key_type = Fee.ReflectionTool.Utility.GetDictionaryKeyType(t_type);
								if(t_key_type == typeof(string)){
									//associativearray => Generic.Dictionary<string.>
									//associativearray => Generic.SortedDictionary<string,>
									//associativearray => Generic.SortedList<string,>

									//リスト型の値型。取得。
									System.Type t_listitem_valuetype = Fee.ReflectionTool.Utility.GetListValueType(t_type);

									System.Collections.Generic.List<string> t_keylist = a_jsonitem.CreateAssociativeKeyList();
									foreach(string t_listitem_key_string in t_keylist){

										//ワークに追加。
										JsonItem t_listitem_jsonitem = a_jsonitem.GetItem(t_listitem_key_string);
										t_workpool.AddFirst(new JsonToObject_Work(JsonToObject_Work.ModeAddDictionary.Start,t_listitem_jsonitem,t_to_dictionary,t_listitem_key_string,t_listitem_valuetype));
									}

									//doの外へ。
									break;
								}else{
									//Generic.Dictionary<xxxx.>
									//Generic.SortedDictionary<xxxx,>
									//Generic.SortedList<xxxx,>

								}
							}
						}
							
						//class,struct
						{
							//associativearray => class,strut

							System.Collections.Generic.List<System.Reflection.FieldInfo> t_fieldinfo_list = new System.Collections.Generic.List<System.Reflection.FieldInfo>();
							Fee.JsonItem.ConvertTool.GetMemberListAll(t_type,t_fieldinfo_list);
							foreach(System.Reflection.FieldInfo t_fieldinfo in t_fieldinfo_list){
								if(a_jsonitem.IsExistItem(t_fieldinfo.Name) == true){
									//ＪＳＯＮ側に存在する。

									//ワークに追加。
									JsonItem t_jsonitem_classmember = a_jsonitem.GetItem(t_fieldinfo.Name);
									t_workpool.AddFirst(new JsonToObject_Work(JsonToObject_Work.ModeFieldInfo.Start,t_jsonitem_classmember,t_fieldinfo,a_to_object_ref));
								}else{
									//ＪＳＯＮ側には存在しない。
								}
							}

							//doの外へ。
							break;
						}

						//doの外へ。
						//break;
					}while(false);

					//ここがdoの外。
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

