

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
		public static JsonItem Convert(System.Object a_from_object,ObjectToJson_Work.ObjectOption a_objectoption,int a_nest,System.Collections.Generic.List<ObjectToJson_Work> a_workpool = null)
		{
			System.Collections.Generic.List<ObjectToJson_Work> t_workpool = a_workpool;

			if(t_workpool == null){
				t_workpool = new System.Collections.Generic.List<ObjectToJson_Work>();				
			}

			JsonItem t_return = null;

			if(a_from_object != null){
				System.Type t_type = a_from_object.GetType();

				if(t_type == typeof(int)){
					//20:int
					int t_value = (int)a_from_object;
					t_return = new JsonItem(new Value_Number<int>(t_value));
				}else if(t_type == typeof(float)){
					//13:float
					float t_value = (float)a_from_object;
					t_return = new JsonItem(new Value_Number<float>(t_value));
				}else if(t_type == typeof(bool)){
					//11:bool
					bool t_value = (bool)a_from_object;
					t_return = new JsonItem(new Value_Number<bool>(t_value));
				}else if(t_type == typeof(long)){
					//22:long
					long t_value = (long)a_from_object;
					t_return = new JsonItem(new Value_Number<long>(t_value));
				}else if(t_type == typeof(char)){
					//12:char
					char t_value = (char)a_from_object;
					t_return = new JsonItem(new Value_Number<char>(t_value));
				}else if(t_type == typeof(double)){
					//14:double
					double t_value = (double)a_from_object;
					t_return = new JsonItem(new Value_Number<double>(t_value));
				}else if(t_type == typeof(decimal)){
					//15:decimal
					decimal t_value = (decimal)a_from_object;
					t_return = new JsonItem(new Value_Number<decimal>(t_value));
				}else if(t_type == typeof(sbyte)){
					//16:sbyte
					sbyte t_value = (sbyte)a_from_object;
					t_return = new JsonItem(new Value_Number<sbyte>(t_value));
				}else if(t_type == typeof(byte)){
					//17:byte
					byte t_value = (byte)a_from_object;
					t_return = new JsonItem(new Value_Number<byte>(t_value));
				}else if(t_type == typeof(short)){
					//18:short
					short t_value = (short)a_from_object;
					t_return = new JsonItem(new Value_Number<short>(t_value));
				}else if(t_type == typeof(ushort)){
					//19:ushort
					ushort t_value = (ushort)a_from_object;
					t_return = new JsonItem(new Value_Number<ushort>(t_value));
				}else if(t_type == typeof(uint)){
					//21:uint
					uint t_value = (uint)a_from_object;
					t_return = new JsonItem(new Value_Number<uint>(t_value));
				}else if(t_type == typeof(ulong)){
					//23:ulong
					ulong t_value = (ulong)a_from_object;
					t_return = new JsonItem(new Value_Number<ulong>(t_value));
				}else if(t_type == typeof(string)){
					//2:文字データ。

					string t_value = a_from_object as string;
					if(t_value != null){
						t_return = new JsonItem(new Value_StringData(t_value));
					}else{
						//NULL処理。

						t_return = null;
						//t_return = new JsonItem();
					}
				}else if(t_type.IsArray == true){
					//x[]

					System.Array t_array_raw = (System.Array)a_from_object;
	
					JsonItem t_jsonitem = new JsonItem(new Value_IndexArray());

					//サイズ確保。
					t_jsonitem.ReSize(t_array_raw.Length);

					for(int ii=0;ii<t_array_raw.Length;ii++){

						//ワークに追加。
						System.Object t_listitem_object = t_array_raw.GetValue(ii);
						t_workpool.Add(new ObjectToJson_Work(ObjectToJson_Work.ModeSetIndexArray.Start,a_nest + 1,t_jsonitem,ii,t_listitem_object,a_objectoption));
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

						string t_value = a_from_object.ToString();

						if(t_value != null){
							t_return = new JsonItem(new Value_StringData(t_value));
						}else{
							//NULL処理。

							t_return = null;
							//t_return = new JsonItem();
						}
					}else{
						//enumの数値化。

						int t_value = (int)a_from_object;
						t_return = new JsonItem(new Value_Number<int>(t_value));
					}
				}else{

					do{

						//リスト。
						{
							{
								System.Collections.IEnumerable t_from_enumerable = a_from_object as System.Collections.IEnumerable;
								if(t_from_enumerable != null){

									//ICollection
									{
										System.Collections.ICollection t_from_collection = t_from_enumerable as System.Collections.ICollection;
										if(t_from_collection != null){

											{
												System.Collections.IDictionary t_from_dictionary = t_from_collection as System.Collections.IDictionary;
												if(t_from_dictionary != null){

													System.Type t_key_type = Fee.ReflectionTool.Utility.GetDictionaryKeyType(t_type);
													if(t_key_type == typeof(string)){
														//Dictionary<string,xxxx>

														JsonItem t_jsonitem = new JsonItem(new Value_AssociativeArray());
														foreach(System.Collections.DictionaryEntry t_from_pair in t_from_dictionary){
															string t_from_listitem_key_string = (string)t_from_pair.Key;
															if(t_from_listitem_key_string != null){

																//ワークに追加。
																System.Object t_from_listitem_object = t_from_pair.Value;
																t_workpool.Add(new ObjectToJson_Work(ObjectToJson_Work.ModeAddAssociativeArray.Start,a_nest + 1,t_jsonitem,t_from_listitem_key_string,t_from_listitem_object,a_objectoption));
															}else{
																//NULL処理。
																//keyがnullの場合は追加しない。
															}
														}

														//doの外へ。
														t_return = t_jsonitem;
														break;
													}
												}
											}

											//ICollection
											{
												//List<xxxx>
												//Stack<xxxx>
												//LinkedList<xxxx>
												//Queue<xxxx>
												//SortedSet<xxxx>

												JsonItem t_jsonitem = new JsonItem(new Value_IndexArray());

												//サイズがわかるので要素確保。
												t_jsonitem.ReSize(t_from_collection.Count);

												int t_index = 0;
												foreach(System.Object t_from_listitem in t_from_collection){

													//ワークに追加。
													t_workpool.Add(new ObjectToJson_Work(ObjectToJson_Work.ModeSetIndexArray.Start,a_nest + 1,t_jsonitem,t_index,t_from_listitem,a_objectoption));
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
										//HashSet<xxxx>

										JsonItem t_jsonitem = new JsonItem(new Value_IndexArray());

										foreach(System.Object t_from_listitem in t_from_enumerable){

											//ワークに追加。
											t_workpool.Add(new ObjectToJson_Work(ObjectToJson_Work.ModeAddIndexArray.Start,a_nest + 1,t_jsonitem,t_from_listitem,a_objectoption));
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

							//メンバーリスト。取得。
							System.Collections.Generic.List<System.Reflection.FieldInfo> t_fieldinfo_list = new System.Collections.Generic.List<System.Reflection.FieldInfo>();
							Fee.JsonItem.ConvertTool.GetMemberListAll(t_type,t_fieldinfo_list);

							foreach(System.Reflection.FieldInfo t_fieldinfo in t_fieldinfo_list){

								//ワークに追加。
								t_workpool.Add(new ObjectToJson_Work(ObjectToJson_Work.ModeFieldInfo.Start,a_nest + 1,t_jsonitem,t_fieldinfo,a_from_object));
							}
							
							//doの外へ。
							t_return = t_jsonitem;
							break;
						}

						//doの外へ。
						//break;
					}while(false);

					//ここがdoの外。
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

