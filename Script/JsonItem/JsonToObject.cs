

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
	/** JsonToObject
	*/
	public class JsonToObject
	{
		/** JsonItemからコンバート先の型を決める。
		*/
		public static System.Type GetConvertToTypeFromJsonItem(Fee.JsonItem.JsonItem a_jsonitem)
		{
			System.Type t_type = null;

			switch(a_jsonitem.GetValueType()){
			case ValueType.Null:
				{
					//値型が不明なので値型はSystem.Objectにする。
					t_type = typeof(System.Object);
				}break;
			case ValueType.AssociativeArray:
				{
					//値型が不明なので値型はSystem.Objectにする。
					t_type = typeof(System.Collections.Generic.Dictionary<string,System.Object>);
				}break;
			case ValueType.IndexArray:
				{
					//値型が不明なので値型はSystem.Objectにする。
					t_type = typeof(System.Collections.Generic.List<System.Object>);
				}break;
			case ValueType.StringData:
				{
					t_type = typeof(string);
				}break;
			case ValueType.SignedNumber:
				{
					t_type = JsonItem.GetSignedNumberType();
				}break;
			case ValueType.UnsignedNumber:
				{
					t_type = JsonItem.GetUnsignedNumberType();
				}break;
			case ValueType.FloatingNumber:
				{
					t_type = JsonItem.GetFloatingNumberType();
				}break;
			case ValueType.BoolData:
				{
					t_type = typeof(bool);
				}break;
			case ValueType.DecimalNumber:
				{
					t_type = typeof(decimal);
				}break;
			case ValueType.BinaryData:
				{
					t_type = typeof(System.Collections.Generic.List<byte>);
				}break;
			default:
				{
					Tool.Assert(false);
				}break;
			}

			return t_type;
		}


		/** インスタンス作成。
		*/
		public static void CreateInstance(out System.Object a_to_object,System.Type a_to_type,Fee.JsonItem.JsonItem a_from_jsonitem)
		{
			switch(a_to_type.FullName){
			case "System." + nameof(System.Char):
			case "System." + nameof(System.SByte):
			case "System." + nameof(System.Byte):
			case "System." + nameof(System.Int16):
			case "System." + nameof(System.UInt16):
			case "System." + nameof(System.Int32):
			case "System." + nameof(System.UInt32):
			case "System." + nameof(System.Int64):
			case "System." + nameof(System.UInt64):
			case "System." + nameof(System.Single):
			case "System." + nameof(System.Double):
			case "System." + nameof(System.Boolean):
			case "System." + nameof(System.Decimal):
			case "System." + nameof(System.String):
			case "System." + nameof(System.Object):
				{
					a_to_object = null;
					return;
				}break;
			}

			if(a_to_type.IsArray == true){
				//[]

				int t_list_count = 0;
				if(a_from_jsonitem.IsIndexArray() == true){
					t_list_count = a_from_jsonitem.GetListMax();
				}

				try{
					System.Type t_element_type = a_to_type.GetElementType();
					a_to_object = System.Array.CreateInstance(t_element_type,t_list_count);
					return;
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}

				//失敗。
				Tool.Assert(false);
				a_to_object = null;
				return;
			}

			//インスタンス。
			{
				if(a_from_jsonitem.IsNull() == true){
					//NULL処理。
					a_to_object = null;
					return;
				}else{
					try{
						a_to_object = System.Activator.CreateInstance(a_to_type);
						return;
					}catch(System.Exception t_exception){
						//引数なしconstructorの呼び出しに失敗。
						Tool.DebugReThrow(t_exception);
					}

					//失敗。
					Tool.Assert(false);
					a_to_object = null;
					return;
				}
			}
		}

		/** Convert
		*/
		public static void Convert(ref System.Object a_to_object_ref,System.Type a_to_type,JsonItem a_from_jsonitem,JsonToObject_WorkPool a_workpool = null)
		{
			JsonToObject_WorkPool t_workpool = a_workpool;

			if(t_workpool == null){
				t_workpool = new JsonToObject_WorkPool();
			}

			try{
				switch(a_from_jsonitem.GetValueType()){
				case ValueType.StringData:
					{
						JsonToObject_FromStringData.Convert(ref a_to_object_ref,a_to_type,a_from_jsonitem);
					}break;
				case ValueType.SignedNumber:
				case ValueType.UnsignedNumber:
				case ValueType.FloatingNumber:
				case ValueType.DecimalNumber:
				case ValueType.BoolData:
					{
						JsonToObject_FromNumber.Convert(ref a_to_object_ref,a_to_type,a_from_jsonitem);
					}break;
				case ValueType.IndexArray:
					{
						JsonToObject_FromIndexArray.Convert(ref a_to_object_ref,a_to_type,a_from_jsonitem,t_workpool);
					}break;
				case ValueType.AssociativeArray:
					{
						JsonToObject_FromAssociativeArray.Convert(ref a_to_object_ref,a_to_type,a_from_jsonitem,t_workpool);
					}break;
				case ValueType.Null:
					{
						//NULL処理。
						a_to_object_ref = null;
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

