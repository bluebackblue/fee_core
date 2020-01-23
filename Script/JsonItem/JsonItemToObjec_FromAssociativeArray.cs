

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。オブジェクト化。
*/


/** Unreachable code detected.
*/
#pragma warning disable 0162


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** JsonItemToObject_FromAssociativeArray
	*/
	public class JsonItemToObject_FromAssociativeArray
	{
		/** Convert
		*/
		public static void Convert(ref System.Object a_to_object,System.Type a_to_type,JsonItem a_from_jsonitem,JsonItemToObject_WorkPool a_workpool)
		{
			try{
				//IDictionary
				{
					System.Collections.IDictionary t_to_dictionary = a_to_object as System.Collections.IDictionary;
					if(t_to_dictionary != null){

						System.Type t_key_type = Fee.ReflectionTool.Utility.GetDictionaryKeyType(a_to_type);
						if(t_key_type == typeof(string)){
							//Generic.Dictionary<string.>
							//Generic.SortedDictionary<string,>
							//Generic.SortedList<string,>

							//リスト型の値型。取得。
							System.Type t_listitem_valuetype = Fee.ReflectionTool.Utility.GetListValueType(a_to_type);

							System.Collections.Generic.Dictionary<string,JsonItem>.KeyCollection t_keylist = a_from_jsonitem.GetAssociativeKeyList();

							//ワークに追加。
							foreach(string t_listitem_key_string in t_keylist){
								JsonItem t_listitem_jsonitem = a_from_jsonitem.GetItem(t_listitem_key_string);
								a_workpool.AddFirst(new JsonItemToObject_WorkPool_Item(JsonItemToObject_WorkPool_Item.ModeAddDictionary.Start,t_listitem_jsonitem,t_to_dictionary,t_listitem_key_string,t_listitem_valuetype));
							}

							//完了。
							return;
						}else{
							//Generic.Dictionary<xxxx.>
							//Generic.SortedDictionary<xxxx,>
							//Generic.SortedList<xxxx,>

							//未対応。
						}
					}
				}
							
				//class,struct
				{
					System.Collections.Generic.List<System.Reflection.FieldInfo> t_fieldinfo_list = new System.Collections.Generic.List<System.Reflection.FieldInfo>();
					Fee.JsonItem.ConvertTool.GetMemberListAll(a_to_type,t_fieldinfo_list);

					//ワークに追加。
					foreach(System.Reflection.FieldInfo t_fieldinfo in t_fieldinfo_list){
						if(a_from_jsonitem.IsExistItem(t_fieldinfo.Name) == true){
							JsonItem t_jsonitem_classmember = a_from_jsonitem.GetItem(t_fieldinfo.Name);
							a_workpool.AddFirst(new JsonItemToObject_WorkPool_Item(JsonItemToObject_WorkPool_Item.ModeFieldInfo.Start,t_jsonitem_classmember,t_fieldinfo,a_to_object));
						}
					}

					//完了。
					return;
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			//失敗。
			Tool.Assert(false);
		}
	}
}

