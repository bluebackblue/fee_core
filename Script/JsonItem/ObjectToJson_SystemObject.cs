

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

					//サイズ確保。
					t_jsonitem.ReSize(t_array_raw.Length);

					for(int ii=0;ii<t_array_raw.Length;ii++){

						//ワークに追加。
						System.Object t_instance_listitem = t_array_raw.GetValue(ii);
						t_workpool.Add(new ObjectToJson_Work(ObjectToJson_Work.ModeSetIndexArray.Start,a_nest + 1,t_jsonitem,ii,t_instance_listitem,a_objectoption));
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

						//リスト。
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
													System.Collections.Generic.ICollection<string> t_instance_collection_key = t_instance_dictionary.Keys as System.Collections.Generic.ICollection<string>;
													if(t_instance_collection_key != null){

														//Dictionary<string,xxxx>

														JsonItem t_jsonitem = new JsonItem(new Value_AssociativeArray());
														foreach(string t_key_string in t_instance_collection_key){
															if(t_key_string != null){

																//ワークに追加。
																System.Object t_instance_listitem = t_instance_dictionary[t_key_string];
																t_workpool.Add(new ObjectToJson_Work(ObjectToJson_Work.ModeAddAssociativeArray.Start,a_nest + 1,t_jsonitem,t_key_string,t_instance_listitem,a_objectoption));
															}else{
																//NULL処理。
																
																//t_key_stringがnullの場合は追加しない。
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

												//Dictionary<xxxx,xxxx>
												//LinkedList<xxxx>
												//Queue<xxxx>
												//SortedSet<xxxx>
												//Stack<xxxx>

												JsonItem t_jsonitem = new JsonItem(new Value_IndexArray());
												t_jsonitem.ReSize(t_instance_collection.Count);

												int t_index = 0;
												foreach(System.Object t_instance_listitem in t_instance_collection){

													//ワークに追加。
													t_workpool.Add(new ObjectToJson_Work(ObjectToJson_Work.ModeSetIndexArray.Start,a_nest + 1,t_jsonitem,t_index,t_instance_listitem,a_objectoption));
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

										foreach(System.Object t_instance_listitem in t_instance_enumerable){

											//ワークに追加。
											t_workpool.Add(new ObjectToJson_Work(ObjectToJson_Work.ModeAddIndexArray.Start,a_nest + 1,t_jsonitem,t_instance_listitem,a_objectoption));
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
							
							System.Collections.Generic.List<System.Reflection.FieldInfo> t_fieldinfo_list = Fee.JsonItem.ReflectionTool.GetFieldInfoList(t_type);
							foreach(System.Reflection.FieldInfo t_fieldinfo in t_fieldinfo_list){

								//ワークに追加。
								t_workpool.Add(new ObjectToJson_Work(ObjectToJson_Work.ModeFieldInfo.Start,a_nest + 1,t_jsonitem,t_fieldinfo,a_instance));
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

