

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
	/** JsonItemToObject
	*/
	public class JsonItemToObject
	{
		/** Convert
		*/
		public static void Convert(ref System.Object a_to_ref_object,System.Type a_to_type,JsonItem a_from_jsonitem,JsonItemToObject_WorkPool a_workpool)
		{
			JsonItemToObject_WorkPool t_workpool = a_workpool;

			if(t_workpool == null){
				t_workpool = new JsonItemToObject_WorkPool();
			}

			try{
				switch(a_from_jsonitem.GetValueType()){
				case ValueType.StringData:
					{
						JsonItemToObject_FromStringData.Convert(ref a_to_ref_object,a_to_type,a_from_jsonitem);
					}break;
				case ValueType.SignedNumber:
				case ValueType.UnsignedNumber:
				case ValueType.FloatingNumber:
				case ValueType.DecimalNumber:
				case ValueType.BoolData:
					{
						JsonItemToObject_FromNumber.Convert(ref a_to_ref_object,a_to_type,a_from_jsonitem);
					}break;
				case ValueType.IndexArray:
					{
						JsonItemToObject_FromIndexArray.Convert(ref a_to_ref_object,a_to_type,a_from_jsonitem,t_workpool);
					}break;
				case ValueType.AssociativeArray:
					{
						JsonItemToObject_FromAssociativeArray.Convert(ref a_to_ref_object,a_to_type,a_from_jsonitem,t_workpool);
					}break;
				case ValueType.Null:
					{
						//NULL処理。
					}break;
				default:
					{
						Tool.Assert(false);
					}break;
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			if(a_workpool == null){
				t_workpool.Main();
			}
		}
	}
}

