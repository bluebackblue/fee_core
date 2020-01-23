

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。JsonItem化。
*/


/** Unreachable code detected.
*/
#pragma warning disable 0162


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** ObjectToJsonItem_FromClass
	*/
	public class ObjectToJsonItem_FromClass
	{
		/** Convert
		*/
		public static JsonItem Convert(System.Object a_from_object,System.Type a_from_type,ConvertToJsonItemOption a_from_option,ObjectToJsonItem_WorkPool a_workpool,int a_nest)
		{
			try{
				//IDictionary
				{
					System.Collections.IDictionary t_from_dictionary = a_from_object as System.Collections.IDictionary;
					if(t_from_dictionary != null){
						System.Type t_key_type = Fee.ReflectionTool.Utility.GetDictionaryKeyType(a_from_type);
						if(t_key_type == typeof(string)){
							//Generic.Dictionary<string.>
							//Generic.SortedDictionary<string,>
							//Generic.SortedList<string,>

							JsonItem t_to_jsonitem = new JsonItem(new Value_AssociativeArray());

							//値型。
							System.Type t_listitem_valuetype = Fee.ReflectionTool.Utility.GetListValueType(a_from_object.GetType());

							if(t_listitem_valuetype == typeof(System.Object)){
								//ワークに追加。
								foreach(System.Collections.DictionaryEntry t_from_pair in t_from_dictionary){
									string t_from_listitem_key_string = (string)t_from_pair.Key;
									if(t_from_listitem_key_string != null){
										System.Object t_from_listitem_object = t_from_pair.Value;
										a_workpool.Add(ObjectToJsonItem_WorkPool.ModeAddAssociativeArray.Start,t_to_jsonitem,t_from_listitem_key_string,t_from_listitem_object,t_from_listitem_object.GetType(),a_from_option,a_nest + 1);
									}else{
										//NULL処理。
										//keyがnullの場合は追加しない。
									}
								}
							}else{
								//ワークに追加。
								foreach(System.Collections.DictionaryEntry t_from_pair in t_from_dictionary){
									string t_from_listitem_key_string = (string)t_from_pair.Key;
									if(t_from_listitem_key_string != null){
										System.Object t_from_listitem_object = t_from_pair.Value;
										a_workpool.Add(ObjectToJsonItem_WorkPool.ModeAddAssociativeArray.Start,t_to_jsonitem,t_from_listitem_key_string,t_from_listitem_object,t_listitem_valuetype,a_from_option,a_nest + 1);
									}else{
										//NULL処理。
										//keyがnullの場合は追加しない。
									}
								}
							}

							//成功。
							return t_to_jsonitem;
						}else{
							//key != string
						}
					}
				}

				//ICollection
				{
					System.Collections.ICollection t_from_collection = a_from_object as System.Collections.ICollection;
					if(t_from_collection != null){

						//Generic.List
						//Generic.Stack
						//Generic.LinkedList
						//Generic.Queue
						//Generic.SortedSet

						JsonItem t_to_jsonitem = new JsonItem(new Value_IndexArray());

						//サイズがわかるので要素確保。
						t_to_jsonitem.ReSize(t_from_collection.Count);

						int t_index = 0;

						//値型。
						System.Type t_listitem_valuetype = Fee.ReflectionTool.Utility.GetListValueType(a_from_type);

						if(t_listitem_valuetype == typeof(System.Object)){
							//Collections.ArrayList

							//ワークに追加。
							foreach(System.Object t_from_listitem in t_from_collection){
								a_workpool.Add(ObjectToJsonItem_WorkPool.ModeSetIndexArray.Start,t_to_jsonitem,t_index,t_from_listitem,t_from_listitem.GetType(),a_from_option,a_nest + 1);
								t_index++;
							}
						}else{
							//ワークに追加。
							foreach(System.Object t_from_listitem in t_from_collection){
								a_workpool.Add(ObjectToJsonItem_WorkPool.ModeSetIndexArray.Start,t_to_jsonitem,t_index,t_from_listitem,t_listitem_valuetype,a_from_option,a_nest + 1);
								t_index++;
							}
						}

						//成功。
						return t_to_jsonitem;
					}
				}

				//IEnumerable
				{
					System.Collections.IEnumerable t_from_enumerable = a_from_object as System.Collections.IEnumerable;
					if(t_from_enumerable != null){

						//Generic.HashSet

						JsonItem t_to_jsonitem = new JsonItem(new Value_IndexArray());

						//値型。
						System.Type t_listitem_valuetype = Fee.ReflectionTool.Utility.GetListValueType(a_from_type);

						if(t_listitem_valuetype == typeof(System.Object)){
							//ワークに追加。
							foreach(System.Object t_from_listitem in t_from_enumerable){
								a_workpool.Add(ObjectToJsonItem_WorkPool.ModeAddIndexArray.Start,t_to_jsonitem,t_from_listitem,t_from_listitem.GetType(),a_from_option,a_nest + 1);
							}
						}else{
							//ワークに追加。
							foreach(System.Object t_from_listitem in t_from_enumerable){
								a_workpool.Add(ObjectToJsonItem_WorkPool.ModeAddIndexArray.Start,t_to_jsonitem,t_from_listitem,t_listitem_valuetype,a_from_option,a_nest + 1);
							}
						}

						//成功。
						return t_to_jsonitem;
					}
				}

				//class,struct
				{
					JsonItem t_to_jsonitem = new JsonItem(new Value_AssociativeArray());

					//メンバーリスト。取得。
					System.Collections.Generic.List<System.Reflection.FieldInfo> t_fieldinfo_list = new System.Collections.Generic.List<System.Reflection.FieldInfo>();
					Fee.JsonItem.ConvertTool.GetMemberListAll(a_from_type,t_fieldinfo_list);

					//ワークに追加。
					foreach(System.Reflection.FieldInfo t_fieldinfo in t_fieldinfo_list){
						a_workpool.Add(ObjectToJsonItem_WorkPool.ModeFieldInfo.Start,t_to_jsonitem,t_fieldinfo,a_from_object,a_nest + 1);
					}
							
					//成功。
					return t_to_jsonitem;
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			//失敗。
			Tool.Assert(false);
			return null;
		}
	}
}

