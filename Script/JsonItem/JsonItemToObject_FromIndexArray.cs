

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
	/** オブジェクト化。

		ValueType.IndexArray

	*/
	public class JsonItemToObject_FromIndexArray
	{
		/** Convert
		*/
		public static void Convert(ref System.Object a_to_ref_object,System.Type a_to_type,JsonItem a_from_jsonitem,JsonItemToObject_WorkPool a_workpool)
		{
			try{

				//IList
				{
					System.Collections.IList t_to_list = a_to_ref_object as System.Collections.IList;
					if(t_to_list != null){

						//値型。取得。
						System.Type t_list_value_type = Fee.ReflectionTool.Utility.GetListValueType(a_to_type);

						if(t_to_list.IsFixedSize == true){
							//[]

							//ワークに追加。
							for(int ii=a_from_jsonitem.GetListMax()-1;ii>=0;ii--){
								JsonItem t_jsonitem_listitem = a_from_jsonitem.GetItem(ii);
								a_workpool.AddFirst(JsonItemToObject_WorkPool.ModeSetList.Start,t_jsonitem_listitem,t_to_list,ii,t_list_value_type);
							}
						}else{
							//Generic.List

							//ワークに追加。
							for(int ii=a_from_jsonitem.GetListMax()-1;ii>=0;ii--){
								JsonItem t_jsonitem_listitem = a_from_jsonitem.GetItem(ii);
								a_workpool.AddFirst(JsonItemToObject_WorkPool.ModeAddList.Start,t_jsonitem_listitem,t_to_list,t_list_value_type);
							}
						}

						//成功。
						return;
					}
				}

				//IEnumerable
				{
					System.Collections.IEnumerable t_to_enumerable = a_to_ref_object as System.Collections.IEnumerable;
					if(t_to_enumerable != null){
						System.Type t_generic_type = Fee.ReflectionTool.Utility.GetGenericTypeDefinition(a_to_type);

						//値型。取得。
						System.Type t_list_value_type = Fee.ReflectionTool.Utility.GetListValueType(a_to_type);

						//メソッド取得。
						System.Reflection.MethodInfo t_methodinfo = null;
						if(t_generic_type == typeof(System.Collections.Generic.Stack<>)){
							//Generic.Stack

							t_methodinfo = ConvertTool.GetMethod_Stack_Push(a_to_type,t_list_value_type);

							if(t_methodinfo != null){

								//ワークに追加。
								for(int ii=0;ii<a_from_jsonitem.GetListMax();ii++){
									JsonItem t_jsonitem_listitem = a_from_jsonitem.GetItem(ii);
									a_workpool.AddFirst(JsonItemToObject_WorkPool.ModeIEnumerable.Start_Param1,t_jsonitem_listitem,t_to_enumerable,t_methodinfo,t_list_value_type);
								}

								//成功。
								return;
							}
						}else if(t_generic_type == typeof(System.Collections.Generic.LinkedList<>)){
							//Generic.LinkedList
							t_methodinfo = ConvertTool.GetMethod_LinkedList_AddLast(a_to_type,t_list_value_type);
						}else if(t_generic_type == typeof(System.Collections.Generic.HashSet<>)){
							//Generic.HashSet
							t_methodinfo = ConvertTool.GetMethod_HashSet_Add(a_to_type,t_list_value_type);
						}else if(t_generic_type == typeof(System.Collections.Generic.Queue<>)){
							//Generic.Queue
							t_methodinfo = ConvertTool.GetMethod_Queue_Enqueue(a_to_type,t_list_value_type);
						}else if(t_generic_type == typeof(System.Collections.Generic.SortedSet<>)){
							//Generic.SortedSet
							t_methodinfo = ConvertTool.GetMethod_SortedSet_Add(a_to_type,t_list_value_type);
						}

						if(t_methodinfo != null){

							//ワークに追加。
							for(int ii=a_from_jsonitem.GetListMax()-1;ii>=0;ii--){
								JsonItem t_jsonitem_listitem = a_from_jsonitem.GetItem(ii);
								a_workpool.AddFirst(JsonItemToObject_WorkPool.ModeIEnumerable.Start_Param1,t_jsonitem_listitem,t_to_enumerable,t_methodinfo,t_list_value_type);
							}

							//成功。
							return;
						}
					}
				}

				//IDictionary
				{
					System.Collections.IDictionary t_to_dictionary = a_to_ref_object as System.Collections.IDictionary;
					if(t_to_dictionary != null){

						//キー型。
						System.Type t_list_key_type = Fee.ReflectionTool.Utility.GetDictionaryKeyType(a_to_type);

						//値型。
						System.Type t_list_value_type = Fee.ReflectionTool.Utility.GetListValueType(a_to_type);

						//ワークに追加。
						for(int ii=0;ii<a_from_jsonitem.GetListMax();ii++){
							Fee.JsonItem.JsonItem t_listitem_jsonitem = a_from_jsonitem.GetItem(ii);

							Fee.JsonItem.JsonItem t_key_jsonitem = null;
							Fee.JsonItem.JsonItem t_value_jsonitem = null;

							if(t_listitem_jsonitem.IsAssociativeArray() == true){
								if(t_listitem_jsonitem.IsExistItem("KEY")){
									t_key_jsonitem = t_listitem_jsonitem.GetItem("KEY");
								}
								if(t_listitem_jsonitem.IsExistItem("VALUE")){
									t_value_jsonitem = t_listitem_jsonitem.GetItem("VALUE");
								}
							}
							
							a_workpool.AddFirst(JsonItemToObject_WorkPool.ModeAddAnyDictionary.Start,t_key_jsonitem,t_value_jsonitem,t_to_dictionary,t_list_key_type,t_list_value_type);
						}

						//成功。
						return;
					}
				}

			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			//失敗。
			Tool.Assert(false);
		}
	}
}

