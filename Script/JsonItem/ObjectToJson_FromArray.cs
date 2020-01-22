

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。ＪＳＯＮ化。
*/


/** Unreachable code detected.
*/
#pragma warning disable 0162


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** ObjectToJson_FromArray
	*/
	public class ObjectToJson_FromArray
	{
		/** Convert
		*/
		public static JsonItem Convert(System.Object a_from_object,System.Type a_from_type,ObjectToJson_WorkPool_Item.ObjectOption a_from_objectoption,int a_nest,ObjectToJson_WorkPool a_workpool)
		{
			try{
				//[]

				System.Array t_array_raw = (System.Array)a_from_object;
	
				JsonItem t_to_jsonitem = new JsonItem(new Value_IndexArray());

				//サイズ確保。
				t_to_jsonitem.ReSize(t_array_raw.Length);

				//値型。
				System.Type t_listitem_valuetype = Fee.ReflectionTool.Utility.GetListValueType(a_from_type);

				for(int ii=0;ii<t_array_raw.Length;ii++){

					//ワークに追加。
					System.Object t_listitem_object = t_array_raw.GetValue(ii);
					a_workpool.Add(new ObjectToJson_WorkPool_Item(ObjectToJson_WorkPool_Item.ModeSetIndexArray.Start,a_nest + 1,t_to_jsonitem,ii,t_listitem_object,t_listitem_valuetype,a_from_objectoption));
				}

				//成功。
				return t_to_jsonitem;
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			//失敗。
			Tool.Assert(false);
			return null;
		}
	}
}

