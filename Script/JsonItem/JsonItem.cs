

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。
*/


/** Unreachable code detected.
*/
#pragma warning disable 0162


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** TYPE
	*/
	using SIGNED_NUMBER_TYPE = System.Int64;
	using UNSIGNED_NUMBER_TYPE = System.UInt64;
	using FLOATING_NUMBER_TYPE = System.Double;

	/** JsonItem
	*/
	public class JsonItem
	{
		/** ＪＳＯＮ文字列。
		*/
		private string jsonstring;

		/** 値。型。
		*/
		public Value value;

		/** タイプ取得。符号あり。
		*/
		public static System.Type GetSignedNumberType()
		{
			return typeof(SIGNED_NUMBER_TYPE);
		}

		/** タイプ取得。符号なし。
		*/
		public static System.Type GetUnsignedNumberType()
		{
			return typeof(UNSIGNED_NUMBER_TYPE);
		}

		/** タイプ取得。符号なし。
		*/
		public static System.Type GetFloatingNumberType()
		{
			return typeof(FLOATING_NUMBER_TYPE);
		}

		/** オブジェクトへコンバート。
		*/
		public Type ConvertToObject<Type>()
		{
			System.Type t_to_type = typeof(Type);

			System.Object t_to_object = null;
			JsonItemToObject_CreateInstance.Create(ref t_to_object,t_to_type,this);
			JsonItemToObject.Convert(ref t_to_object,t_to_type,this,null);

			return (Type)t_to_object;
		}

		/** JsonStringへコンバート。
		*/
		public string ConvertToJsonString()
		{
			if(this.jsonstring != null){
				return this.jsonstring;
			}

			System.Text.StringBuilder t_stringbuilder = new System.Text.StringBuilder();
			this.ValueToJsonString(t_stringbuilder,Config.DEFAULT_CONVERTTOJSONSTRING_OPTION);

			return t_stringbuilder.ToString();
		}

		/** JsonStringへコンバート。
		*/
		public void ConvertToJsonString(System.Text.StringBuilder a_stringbuilder,ConvertToJsonStringOption a_option)
		{
			this.ValueToJsonString(a_stringbuilder,a_option);
		}

		/** constructor
		*/
		public JsonItem()
		{
			this.jsonstring = null;
			this.value.ResetFromType(ValueType.Null);
		}

		/** constructor
		*/
		public JsonItem(string a_jsonstring)
		{
			this.SetJsonString(a_jsonstring);
		}

		/** constructor
		*/
		public JsonItem(Value_AssociativeArray a_value)
		{
			this.SetAssociativeArray();
		}

		/** constructor
		*/
		public JsonItem(Value_IndexArray a_value)
		{
			this.SetIndexArray();
		}

		/** constructor
		*/
		public JsonItem(Value_StringData a_value)
		{
			this.SetStringData(a_value.value);
		}

		/** constructor
		*/
		public JsonItem(Value_Number<System.Char> a_value)
		{
			this.SetChar(a_value.value);
		}

		/** constructor
		*/
		public JsonItem(Value_Number<System.SByte> a_value)
		{
			this.SetSbyte(a_value.value);
		}

		/** constructor
		*/
		public JsonItem(Value_Number<System.Byte> a_value)
		{
			this.SetByte(a_value.value);
		}

		/** constructor
		*/
		public JsonItem(Value_Number<System.Int16> a_value)
		{
			this.SetInt16(a_value.value);
		}

		/** constructor
		*/
		public JsonItem(Value_Number<System.UInt16> a_value)
		{
			this.SetUint16(a_value.value);
		}

		/** constructor
		*/
		public JsonItem(Value_Number<System.Int32> a_value)
		{
			this.SetInt32(a_value.value);
		}

		/** constructor
		*/
		public JsonItem(Value_Number<System.UInt32> a_value)
		{
			this.SetUint32(a_value.value);
		}

		/** constructor
		*/
		public JsonItem(Value_Number<System.Int64> a_value)
		{
			this.SetInt64(a_value.value);
		}

		/** constructor
		*/
		public JsonItem(Value_Number<System.UInt64> a_value)
		{
			this.SetUint64(a_value.value);
		}

		/** constructor
		*/
		public JsonItem(Value_Number<System.Single> a_value)
		{
			this.SetSingle(a_value.value);
		}

		/** constructor
		*/
		public JsonItem(Value_Number<System.Double> a_value)
		{
			this.SetDouble(a_value.value);
		}

		/** constructor
		*/
		public JsonItem(Value_Number<System.Boolean> a_value)
		{
			this.SetBoolData(a_value.value);
		}

		/** constructor
		*/
		public JsonItem(Value_Number<System.Decimal> a_value)
		{
			this.SetDecimal(a_value.value);
		}

		/** constructor
		*/
		public JsonItem(Value_BinaryData a_value)
		{
			this.SetBinaryData(a_value.value);
		}

		/** 値設定。
		*/
		public void SetAssociativeArray()
		{
			this.jsonstring = null;
			this.value.Reset(new System.Collections.Generic.Dictionary<string,JsonItem>());
		}

		/** 値設定。
		*/
		public void SetIndexArray()
		{
			this.jsonstring = null;
			this.value.Reset(new System.Collections.Generic.List<JsonItem>());
		}

		/** 値設定。
		*/
		public void SetStringData(string a_value)
		{
			this.jsonstring = null;
			this.value.Reset(a_value);
		}

		/** 値設定。

			0x0000 -- 0xFFFF

		*/
		public void SetChar(System.Char a_value)
		{
			this.jsonstring = null;
			this.value.Reset(a_value);
		}

		/** 値設定。

			-128 -- 127

		*/
		public void SetSbyte(System.SByte a_value)
		{
			this.jsonstring = null;
			this.value.Reset(a_value);
		}

		/** 値設定。
		
			0 -- 255

		*/
		public void SetByte(System.Byte a_value)
		{
			this.jsonstring = null;
			this.value.Reset(a_value);
		}

		/** 値設定。

			-32768 -- 32767

		*/
		public void SetInt16(System.Int16 a_value)
		{
			this.jsonstring = null;
			this.value.Reset(a_value);
		}

		/** 値設定。

			0 -- 65535

		*/
		public void SetUint16(System.UInt16 a_value)
		{
			this.jsonstring = null;
			this.value.Reset(a_value);
		}

		/** 値設定。

			-2147483648 -- 2147483647

		*/
		public void SetInt32(System.Int32 a_value)
		{
			this.jsonstring = null;
			this.value.Reset(a_value);
		}

		/** 値設定。

			0 -- 4294967295

		*/
		public void SetUint32(System.UInt32 a_value)
		{
			this.jsonstring = null;
			this.value.Reset(a_value);
		}

		/** 値設定。

			-9223372036854775808 -- 9223372036854775807

		*/
		public void SetInt64(System.Int64 a_value)
		{
			this.jsonstring = null;
			this.value.Reset(a_value);
		}

		/** 値設定。

			0 -- 18446744073709551615

		*/
		public void SetUint64(System.UInt64 a_value)
		{
			this.jsonstring = null;
			this.value.Reset(a_value);
		}

		/** 値設定。

			-3.40282347E+38 -- 3.40282347E+38

		*/
		public void SetSingle(System.Single a_value)
		{
			this.jsonstring = null;
			this.value.Reset(a_value);
		}

		/** 値設定。

			-1.7976931348623157E+308 -- 1.7976931348623157E+308

		*/
		public void SetDouble(System.Double a_value)
		{
			this.jsonstring = null;
			this.value.Reset(a_value);
		}

		/** 値設定。

			false / true

		*/
		public void SetBoolData(System.Boolean a_value)
		{
			this.jsonstring = null;
			this.value.Reset(a_value);
		}

		/** 値設定。

			-79228162514264337593543950335 -- 79228162514264337593543950335

		*/
		public void SetDecimal(System.Decimal a_value)
		{
			this.jsonstring = null;
			this.value.Reset(a_value);
		}

		/** 値設定。
		*/
		public void SetBinaryData(System.Collections.Generic.List<byte> a_value)
		{
			this.jsonstring = null;
			this.value.Reset(a_value);
		}

		/** 値取得。
		*/
		public System.Collections.Generic.Dictionary<string,JsonItem> GetAssociativeArray()
		{
			this.JsonStringToValue();
			return this.value.GetAssociativeArray();
		}

		/** 値取得。
		*/
		public System.Collections.Generic.List<JsonItem> GetIndexArray()
		{
			this.JsonStringToValue();
			return this.value.GetIndexArray();
		}

		/** 値取得。
		*/
		public System.String GetStringData()
		{
			this.JsonStringToValue();
			return (System.String)this.value.raw;
		}

		/** 値取得。

			System.Char
			System.SByte
			System.Byte
			System.Int16
			System.UInt16
			System.Int32
			System.UInt32
			System.Int64

		*/
		public SIGNED_NUMBER_TYPE GetSignedNumber()
		{
			this.JsonStringToValue();
			return this.value.GetSignedNumber();
		}

		/** 値取得。

			System.UInt64

		*/
		public UNSIGNED_NUMBER_TYPE GetUnsignedNumber()
		{
			this.JsonStringToValue();
			return this.value.GetUnsignedNumber();
		}

		/** 値取得。

			System.Single
			System.Double

		*/
		public FLOATING_NUMBER_TYPE GetFloatingNumber()
		{
			this.JsonStringToValue();
			return this.value.GetFloatingNumber();
		}

		/** 値取得。
		*/
		public System.Boolean GetBoolData()
		{
			this.JsonStringToValue();
			return this.value.GetBoolData();
		}

		/** 値取得。
		*/
		public System.Decimal GetDecimalNumber()
		{
			this.JsonStringToValue();
			return this.value.GetDecimalNumber();
		}

		/** 値取得。
		*/
		public System.Collections.Generic.List<byte> GetBinaryData()
		{
			this.JsonStringToValue();
			return (System.Collections.Generic.List<byte>)this.value.raw;
		}

		/** キャスト。
		*/
		public System.Char CastToChar()
		{
			this.JsonStringToValue();
			return this.value.CastToChar();
		}

		/** キャスト。
		*/
		public System.SByte CastToSbyte()
		{
			this.JsonStringToValue();
			return this.value.CastToSbyte();
		}

		/** キャスト。
		*/
		public System.Byte CastToByte()
		{
			this.JsonStringToValue();
			return this.value.CastToByte();
		}

		/** キャスト。
		*/
		public System.Int16 CastToInt16()
		{
			this.JsonStringToValue();
			return this.value.CastToInt16();
		}

		/** キャスト。
		*/
		public System.UInt16 CastToUint16()
		{
			this.JsonStringToValue();
			return this.value.CastToUint16();
		}

		/** キャスト。
		*/
		public System.Int32 CastToInt32()
		{
			this.JsonStringToValue();
			return this.value.CastToInt32();
		}

		/** キャスト。
		*/
		public System.UInt32 CastToUint32()
		{
			this.JsonStringToValue();
			return this.value.CastToUint32();
		}

		/** キャスト。
		*/
		public System.Int64 CastToInt64()
		{
			this.JsonStringToValue();
			return this.value.CastToInt64();
		}

		/** キャスト。
		*/
		public System.UInt64 CastToUint64()
		{
			this.JsonStringToValue();
			return this.value.CastToUint64();
		}

		/** キャスト。
		*/
		public System.Single CastToSingle()
		{
			this.JsonStringToValue();
			return this.value.CastToSingle();
		}

		/** キャスト。
		*/
		public System.Double CastToDouble()
		{
			this.JsonStringToValue();
			return this.value.CastToDouble();
		}

		/** キャスト。
		*/
		public System.Boolean CastToBoolData()
		{
			this.JsonStringToValue();
			return this.value.CastToBoolData();
		}

		/** キャスト。
		*/
		public System.Decimal CastToDecimal()
		{
			this.JsonStringToValue();
			return this.value.CastToDecimal();
		}

		/** タイプチェック。ＮＵＬＬ。
		*/
		public bool IsNull()
		{
			return this.value.IsNull();
		}

		/** タイプチェック。連想配列。
		*/
		public bool IsAssociativeArray()
		{
			return this.value.IsAssociativeArray();
		}

		/** タイプチェック。インデックス配列。
		*/
		public bool IsIndexArray()
		{
			return this.value.IsIndexArray();
		}

		/** タイプチェック。文字データ。
		*/
		public bool IsStringData()
		{
			return this.value.IsStringData();
		}

		/** タイプチェック。符号あり整数。
		*/
		public bool IsSignedNumber()
		{
			return this.value.IsSignedNumber();
		}

		/** タイプチェック。符号なし整数。
		*/
		public bool IsUnSignedNumber()
		{
			return this.value.IsUnSignedNumber();
		}

		/** タイプチェック。浮動小数。
		*/
		public bool IsFloatingNumber()
		{
			return this.value.IsFloatingNumber();
		}

		/** タイプチェック。真偽データ。
		*/
		public bool IsBoolData()
		{
			return this.value.IsBoolData();
		}

		/** タイプチェック。１０進数の浮動小数点数。
		*/
		public bool IsDecimalNumber()
		{
			return this.value.IsDecimalNumber();
		}

		/** タイプチェック。バイナリデータ。
		*/
		public bool IsBinaryData()
		{
			return this.value.IsBinaryData();
		}

		/** GetValueType
		*/
		public ValueType GetValueType()
		{
			return this.value.valuetype;
		}

		/** クローン。
		*/
		public JsonItem Clone()
		{
			JsonItem t_new_jsonitem = new JsonItem(this.ConvertToJsonString());
			return t_new_jsonitem;
		}

		/** ＪＳＯＮ文字列をセット。
		*/
		public void SetJsonString(string a_jsonstring)
		{
			if(a_jsonstring.Length > 0){
				ValueType t_valuetype = ValueType_FirstCharValueType.Get(a_jsonstring[0]);
				switch(t_valuetype){
				case ValueType.StringData:
				case ValueType.AssociativeArray:
				case ValueType.IndexArray:
				case ValueType.BinaryData:
				case ValueType.SignedNumber:
				case ValueType.UnsignedNumber:
				case ValueType.FloatingNumber:
				case ValueType.DecimalNumber:
					{
						//保留。
						this.jsonstring = a_jsonstring;
						this.value.ResetFromType(t_valuetype);
						return;
					}break;
				case ValueType.Calc_UnknownNumber:
					{
						ValueType t_number_valuetype = ValueType_NumverValueType.Get(a_jsonstring);

						//保留。
						this.jsonstring = a_jsonstring;
						this.value.ResetFromType(t_number_valuetype);
						return;
					}break;
				case ValueType.Calc_BoolDataTrue:
					{
						this.jsonstring = null;
						this.value.Reset(true);
						return;
					}break;
				case ValueType.Calc_BoolDataFalse:
					{
						this.jsonstring = null;
						this.value.Reset(false);
						return;
					}break;
				case ValueType.Null:
					{
						//NULL処理。

						this.jsonstring = null;
						this.value.ResetFromType(ValueType.Null);
						return;
					}break;
				}
			}

			Tool.Assert(false);
			this.jsonstring = null;
			this.value.ResetFromType(ValueType.Null);
		}

		/** GetListMax
		*/
		public int GetListMax()
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}
		
			switch(this.value.valuetype){
			case ValueType.AssociativeArray:
				{
					System.Collections.Generic.Dictionary<string,JsonItem> t_associativearray = this.value.GetAssociativeArray();
					if(t_associativearray != null){
						return t_associativearray.Count;
					}
				}break;
			case ValueType.IndexArray:
				{
					System.Collections.Generic.List<JsonItem> t_indexarray = this.value.GetIndexArray();
					if(t_indexarray != null){
						return t_indexarray.Count;
					}
				}break;
			case ValueType.StringData:
				{
					string t_stringdata = this.value.GetStringData();
					if(t_stringdata != null){
						return t_stringdata.Length;
					}
				}break;
			case ValueType.BinaryData:
				{
					System.Collections.Generic.List<byte> t_binarydata = this.value.GetBinaryData();
					if(t_binarydata != null){
						return t_binarydata.Count;
					}
				}break;
			}

			Tool.Assert(false);
			return 0;
		}

		/** 連想配列のアイテム取得。
		*/
		public JsonItem GetItem(string a_itemname)
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			System.Collections.Generic.Dictionary<string,JsonItem> t_associativearray = this.value.GetAssociativeArray();
			if(t_associativearray != null){
				JsonItem t_value;
				if(t_associativearray.TryGetValue(a_itemname,out t_value) == true){
					return t_value;
				}
			}

			Tool.Assert(false);
			return null;
		}

		/** インデックス配列のアイテム取得。
		*/
		public JsonItem GetItem(int a_index)
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			System.Collections.Generic.List<JsonItem> t_indexlist = this.value.GetIndexArray();
			if(t_indexlist != null){
				if((0 <= a_index)&&(a_index < t_indexlist.Count)){
					return t_indexlist[a_index];
				}
			}

			Tool.Assert(false);
			return null;
		}

		/** 連想配列のアイテムチェック。
		*/
		public bool IsExistItem(string a_itemname,ValueType a_valuetype = ValueType.Mask_All)
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}
		
			System.Collections.Generic.Dictionary<string,JsonItem> t_associativearray = this.value.GetAssociativeArray();
			if(t_associativearray != null){
				JsonItem t_value;
				if(t_associativearray.TryGetValue(a_itemname,out t_value) == true){
					if((t_value.value.valuetype | a_valuetype) > 0){
						Tool.Assert((t_value.value.valuetype & ValueType.Calc) == 0);
						return true;
					}
				}
			}
		
			return false;
		}

		/** インデックス配列のアイテムチェック。
		*/
		public bool IsExistItem(int a_index,ValueType a_valuetype = ValueType.Mask_All)
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			System.Collections.Generic.List<JsonItem> t_indexarray = this.value.GetIndexArray();
			if(t_indexarray != null){
				if(a_index < t_indexarray.Count){
					if((t_indexarray[a_index].value.valuetype | a_valuetype) > 0){
						Tool.Assert((t_indexarray[a_index].value.valuetype & ValueType.Calc) == 0);
						return true;
					}
				}
			}
		
			return false;
		}

		/** 連想配列にアイテム追加。
		*/
		public void AddItem(string a_itemname,JsonItem a_item,bool a_clone)
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			System.Collections.Generic.Dictionary<string,JsonItem> t_associativearray = this.value.GetAssociativeArray();
			if(t_associativearray != null){
				if((a_clone == true)&&(a_item != null)){
					t_associativearray.Add(a_itemname,a_item.Clone());
				}else{
					t_associativearray.Add(a_itemname,a_item);
				}
			}
		}

		/** インデックス配列にアイテム追加。
		*/
		public void AddItem(JsonItem a_item,bool a_clone)
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}
		
			System.Collections.Generic.List<JsonItem> t_indexarray = this.value.GetIndexArray();
			if(t_indexarray != null){
				if((a_clone == true)&&(a_item != null)){
					t_indexarray.Add(a_item.Clone());
				}else{
					t_indexarray.Add(a_item);
				}
			}
		}

		/** 連想配列にアイテム設定。

			上書き、追加。

		*/
		public void SetItem(string a_itemname,JsonItem a_item,bool a_clone)
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			System.Collections.Generic.Dictionary<string,JsonItem> t_associativearray = this.value.GetAssociativeArray();
			if(t_associativearray != null){
				if((a_clone == true)&&(a_item != null)){
					if(t_associativearray.ContainsKey(a_itemname) == true){
						t_associativearray[a_itemname] = a_item.Clone();
					}else{
						t_associativearray.Add(a_itemname,a_item.Clone());
					}
				}else{
					if(t_associativearray.ContainsKey(a_itemname) == true){
						t_associativearray[a_itemname] = a_item;
					}else{
						t_associativearray.Add(a_itemname,a_item);
					}
				}
			}
		}

		/** インデックス配列にアイテム設定。

			上書き。

		*/
		public void SetItem(int a_index,JsonItem a_item,bool a_clone)
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			System.Collections.Generic.List<JsonItem> t_indexarray = this.value.GetIndexArray();
			if(t_indexarray != null){
				if((0<=a_index)&&(a_index<t_indexarray.Count)){
					if((a_clone == true)&&(a_item != null)){
						t_indexarray[a_index] = a_item.Clone();
					}else{
						t_indexarray[a_index] = a_item;
					}
				}else{
					Tool.Assert(false);
				}
			}
		}

		/** インデックス配列からアイテム削除。
		*/
		public void RemoveItem(string a_itemname)
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}
		
			System.Collections.Generic.Dictionary<string,JsonItem> t_associativearray = this.value.GetAssociativeArray();
			if(t_associativearray != null){
				t_associativearray.Remove(a_itemname);
			}
		}

		/** インデックス配列からアイテム削除。
		*/
		public void RemoveItem(int a_index)
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}
		
			System.Collections.Generic.List<JsonItem> t_indexarray = this.value.GetIndexArray();
			if(t_indexarray != null){
				t_indexarray.RemoveAt(a_index);
			}
		}

		/** インデックス配列のサイズ変更。
		*/
		public void ReSize(int a_list_count)
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			System.Collections.Generic.List<JsonItem> t_indexarray = this.value.GetIndexArray();
			if(t_indexarray != null){
				if(t_indexarray.Count < a_list_count){
					while(t_indexarray.Count < a_list_count){
						t_indexarray.Add(null);
					}
				}else if(t_indexarray.Count > a_list_count){
					t_indexarray.RemoveRange(a_list_count,t_indexarray.Count - a_list_count);
				}
			}
		}

		/** 連想配列キーリスト作成。
		*/
		public System.Collections.Generic.Dictionary<string,JsonItem>.KeyCollection GetAssociativeKeyList()
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			System.Collections.Generic.Dictionary<string,JsonItem> t_associativearray = this.value.GetAssociativeArray();
			if(t_associativearray != null){
				return t_associativearray.Keys;
			}

			Tool.Assert(false);
			return null;
		}

		/** ValueToJsonString
		*/
		public void ValueToJsonString(System.Text.StringBuilder a_stringbuilder,ConvertToJsonStringOption a_option)
		{
			if(this.jsonstring != null){
				a_stringbuilder.Append(this.jsonstring);
				return;
			}

			switch(this.value.valuetype){
			case ValueType.AssociativeArray:
				{
					Convert_AssociativeArray_ToJsonString.Convert(this.value.GetAssociativeArray(),a_stringbuilder,a_option);
					return;
				}break;
			case ValueType.IndexArray:
				{
					Convert_IndexArray_ToJsonString.Convert(this.value.GetIndexArray(),a_stringbuilder,a_option);
					return;
				}break;
			case ValueType.StringData:
				{
					Convert_StringData_ToJsonString.Convert(this.value.GetStringData(),a_stringbuilder,a_option);
					return;
				}break;
			case ValueType.SignedNumber:
				{
					Convert_SignedNumber_ToJsonString.Convert(this.value.GetSignedNumber(),a_stringbuilder,a_option);
					return;
				}break;
			case ValueType.UnsignedNumber:
				{
					Convert_UnsignedNumber_ToJsonString.Convert(this.value.GetUnsignedNumber(),a_stringbuilder,a_option);
					return;
				}break;
			case ValueType.FloatingNumber:
				{
					Convert_FloatingNumber_ToJsonString.Convert(this.value.GetFloatingNumber(),a_stringbuilder,a_option);
					return;
				}break;
			case ValueType.BoolData:
				{
					Convert_BoolData_ToJsonString.Convert(this.value.GetBoolData(),a_stringbuilder,a_option);
					return;
				}break;
			case ValueType.DecimalNumber:
				{
					Convert_DecimalNumber_ToJsonString.Convert(this.value.GetDecimalNumber(),a_stringbuilder,a_option);
					return;
				}break;
			case ValueType.BinaryData:
				{
					Convert_BinaryData_ToJsonString.Convert(this.value.GetBinaryData(),a_stringbuilder,a_option);
					return;
				}break;
			case ValueType.Null:
				{
					//NULL処理。
					a_stringbuilder.Append("null");
					return;
				}break;
			}
		}

		/** JsonStringToValue
		*/
		private void JsonStringToValue()
		{
			if(this.jsonstring != null){
				switch(this.value.valuetype){
				case ValueType.StringData:
					{
						System.Text.StringBuilder t_stringbuilder = new System.Text.StringBuilder(this.jsonstring.Length);
						Convert_StringData_FromJsonString.Convert(this.jsonstring,t_stringbuilder);

						this.value.raw = t_stringbuilder.ToString();
						this.jsonstring = null;
						return;
					}break;
				case ValueType.SignedNumber:
					{
						SIGNED_NUMBER_TYPE t_value;
						Convert_SignedNumber_FromJsonString.Convert(this.jsonstring,out t_value);

						this.value.raw = t_value;
						this.jsonstring = null;
						return;
					}break;
				case ValueType.UnsignedNumber:
					{
						UNSIGNED_NUMBER_TYPE t_value;
						Convert_UnsignedNumber_FromJsonString.Convert(this.jsonstring,out t_value);

						this.value.raw = t_value;
						this.jsonstring = null;
						return;
					}break;
				case ValueType.FloatingNumber:
					{
						FLOATING_NUMBER_TYPE t_value;
						Convert_FloatingNumber_FromJsonString.Convert(this.jsonstring,out t_value);

						this.value.raw = t_value;
						this.jsonstring = null;
						return;
					}break;
				case ValueType.IndexArray:
					{
						System.Collections.Generic.List<JsonItem> t_value = new System.Collections.Generic.List<JsonItem>(4 + this.jsonstring.Length / 7);
						Convert_IndexArray_FromJsonString.Convert(this.jsonstring,t_value);

						this.value.raw = t_value;
						this.jsonstring = null;
						return;
					}break;
				case ValueType.AssociativeArray:
					{
						System.Collections.Generic.Dictionary<string,JsonItem> t_value = new System.Collections.Generic.Dictionary<string,JsonItem>();
						Convert_AssociativeArray_FromJsonString.Convert(this.jsonstring,t_value);

						this.value.raw = t_value;
						this.jsonstring = null;
						return;
					}break;
				case ValueType.BoolData:
					{
						bool t_value;
						Convert_BoolData_FromJsonString.Convert(this.jsonstring,out t_value);

						this.value.raw = t_value;
						this.jsonstring = null;
						return;
					}break;
				case ValueType.DecimalNumber:
					{
						System.Decimal t_value;
						Convert_DecimalNumber_FromJsonString.Convert(this.jsonstring,out t_value);

						this.value.raw = t_value;
						this.jsonstring = null;
						return;
					}break;
				case ValueType.BinaryData:
					{
						System.Collections.Generic.List<byte> t_value = new System.Collections.Generic.List<byte>(4 + this.jsonstring.Length / 2);
						Convert_BinaryData_FromJsonString.Convert(this.jsonstring,t_value);

						this.value.raw = t_value;
						this.jsonstring = null;
						return;
					}break;
				case ValueType.Null:
					{
						this.value.raw = null;
						this.jsonstring = null;
						return;
					}break;
				}
			}
		}
	}
}

