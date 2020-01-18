

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
						System.Collections.IEnumerable t_enumerable = a_to_object_ref as System.Collections.IEnumerable;
						if(t_enumerable != null){
							System.Collections.ICollection t_collection = t_enumerable as System.Collections.ICollection;
							if(t_collection != null){

								//IList
								{
									System.Collections.IList t_list = t_collection as System.Collections.IList;
									if(t_list != null){

										//値の型。
										System.Type t_listitem_type = ReflectionTool.GetListValueType(t_type);

										if(t_list.IsFixedSize == true){
											for(int ii=a_jsonitem.GetListMax()-1;ii>=0;ii--){

												//ワークに追加。
												JsonItem t_jsonitem_listitem = a_jsonitem.GetItem(ii);
												t_workpool.AddFirst(new JsonToObject_Work(JsonToObject_Work.ModeSetList.Start,t_jsonitem_listitem,t_list,ii,t_listitem_type));
											}
										}else{
											for(int ii=a_jsonitem.GetListMax()-1;ii>=0;ii--){

												//ワークに追加。
												JsonItem t_jsonitem_listitem = a_jsonitem.GetItem(ii);
												t_workpool.AddFirst(new JsonToObject_Work(JsonToObject_Work.ModeAddList.Start,t_jsonitem_listitem,t_list,t_listitem_type));
											}
										}

										//doの外へ。
										break;
									}
								}

								//IEnumerable
								{
									System.Type t_listitem_type = t_type.GetGenericArguments()[0];

									System.Type t_generic_type = ReflectionTool.GetGenericTypeDefinition(t_enumerable.GetType());
									if(t_generic_type == typeof(System.Collections.Generic.Stack<>)){
										//Stack

										System.Reflection.MethodInfo t_methodinfo = ReflectionTool.GetMethod_Stack_Push(t_enumerable.GetType());
										if(t_methodinfo != null){
											for(int ii=0;ii<a_jsonitem.GetListMax();ii++){

												//ワークに追加。
												JsonItem t_jsonitem_listitem = a_jsonitem.GetItem(ii);
												t_workpool.AddFirst(new JsonToObject_Work(JsonToObject_Work.TODO_ModeIEnumerable.Start,t_jsonitem_listitem,t_enumerable,t_methodinfo,t_listitem_type));
											}
										}

										//doの外へ。
										break;
									}



									//Dictionary
									//LinkedList
									//HashSet
									//Queue
									//SortedSet
									
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
							System.Collections.IDictionary t_dictionary = a_to_object_ref as System.Collections.IDictionary;
							if(t_dictionary != null){

								System.Collections.Generic.ICollection<string> t_collection_key = t_dictionary.Keys as System.Collections.Generic.ICollection<string>;
								if(t_collection_key != null){
									//key == string

									//値の型。
									System.Type t_listitem_value_type = ReflectionTool.GetDictionaryValueType(t_type);

									System.Collections.Generic.List<string> t_keylist = a_jsonitem.CreateAssociativeKeyList();
									foreach(string t_listitem_key_string in t_keylist){

										//ワークに追加。
										JsonItem t_listitem_jsonitem = a_jsonitem.GetItem(t_listitem_key_string);
										t_workpool.AddFirst(new JsonToObject_Work(JsonToObject_Work.ModeAddDictionary.Start,t_listitem_jsonitem,t_dictionary,t_listitem_key_string,t_listitem_value_type));
									}

									//doの外へ。
									break;
								}else{
									//key != string
								}
							}
						}
							
						//class,struct
						{
							System.Collections.Generic.List<System.Reflection.FieldInfo> t_fieldinfo_list = Fee.JsonItem.ReflectionTool.GetFieldInfoList(t_type);
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

