

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
	public class JsonItem : Config
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

			System.Object t_to_object;
			JsonToObject.CreateInstance(out t_to_object,t_to_type,this);
			JsonToObject.Convert(ref t_to_object,t_to_type,this);

			return (Type)t_to_object;
			//return (Type)System.Convert.ChangeType(t_to_object,t_to_type);
		}

		/** JsonStringへコンバート。
		*/
		public string ConvertToJsonString()
		{
			if(this.jsonstring != null){
				return this.jsonstring;
			}

			System.Text.StringBuilder t_stringbuilder = new System.Text.StringBuilder();
			this.ValueToJsonString(t_stringbuilder);

			return t_stringbuilder.ToString();
		}

		/** JsonStringへコンバート。
		*/
		public void ConvertToJsonString(System.Text.StringBuilder a_stringbuilder)
		{
			this.ValueToJsonString(a_stringbuilder);
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

			char
			sbyte
			byte
			System.Int16(short)
			System.UInt16(ushort)
			System.Int32(int)
			System.UInt32(uint)
			System.Int64(long)

		*/
		public SIGNED_NUMBER_TYPE GetSignedNumber()
		{
			this.JsonStringToValue();
			return this.value.GetSignedNumber();
		}

		/** 値取得。

			System.UInt64(ulong)

		*/
		public UNSIGNED_NUMBER_TYPE GetUnsignedNumber()
		{
			this.JsonStringToValue();
			return this.value.GetUnsignedNumber();
		}

		/** 値取得。

			System.Single(float)
			System.Double(double)

		*/
		public FLOATING_NUMBER_TYPE GetFloatingNumber()
		{
			this.JsonStringToValue();
			return this.value.GetFloatingNumber();
		}

		/** 値取得。
		*/
		public bool GetBoolData()
		{
			this.JsonStringToValue();
			return this.value.GetBoolData();
		}

		/** 値取得。
		*/
		public decimal GetDecimalNumber()
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
			return this.value.IsBoolData();
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
				ValueType t_valuetype = Impl.GetValueTypeFromChar(a_jsonstring[0]);
				switch(t_valuetype){
				case ValueType.StringData:
				case ValueType.AssociativeArray:
				case ValueType.IndexArray:
				case ValueType.BinaryData:
				case ValueType.SignedNumber:
				case ValueType.UnsignedNumber:
				case ValueType.FloatingNumber:
					{
						//保留。
						this.jsonstring = a_jsonstring;
						this.value.ResetFromType(t_valuetype);
						return;
					}break;
				case ValueType.Calc_UnknownNumber:
					{
						if(Impl.IsFloat(a_jsonstring)){
							//保留。
							this.jsonstring = a_jsonstring;
							this.value.ResetFromType(ValueType.FloatingNumber);
							return;
						}else{
							if(a_jsonstring[0] == '-'){
								//マイナス値。

								//保留。
								this.jsonstring = a_jsonstring;
								this.value.ResetFromType(ValueType.SignedNumber);
								return;
							}else{
								//プラス値。

								//保留。
								this.jsonstring = a_jsonstring;
								this.value.ResetFromType(ValueType.UnsignedNumber);
								return;
							}
						}
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
			case ValueType.Null:
			case ValueType.SignedNumber:
			case ValueType.UnsignedNumber:
			case ValueType.FloatingNumber:
			case ValueType.BoolData:
			case ValueType.Calc_BoolDataFalse:
			case ValueType.Calc_BoolDataTrue:
			case ValueType.Calc_UnknownNumber:
			default:
				{
				}break;
			}

			Tool.Assert(false);
			return 0;
		}

		/** 連想リストのアイテム取得。
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

		/** インデックスリストのアイテム取得。
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

		/** 連想リストのアイテムチェック。
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

		/** インデックスリストのアイテムチェック。
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

		/** 連想リストにアイテム追加。
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

		/** インデックスリストにアイテム追加。
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

		/** 連想リストにアイテム設定。

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

		/** インデックスリストにアイテム設定。

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

		/** インデックスリストからアイテム削除。
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

		/** インデックスリストからアイテム削除。
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

		/** インデックスリストのサイズ変更。
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
		private void ValueToJsonString(System.Text.StringBuilder a_stringbuilder)
		{
			if(this.jsonstring != null){
				a_stringbuilder.Append(this.jsonstring);
				return;
			}

			switch(this.value.valuetype){
			case ValueType.AssociativeArray:
				{
					a_stringbuilder.Append("{");

					try{
						System.Collections.Generic.Dictionary<string,JsonItem> t_associativearray = this.value.GetAssociativeArray();
						if(t_associativearray != null){
							bool t_first = true;

							foreach(System.Collections.Generic.KeyValuePair<string,JsonItem> t_pair in t_associativearray){
								if(t_first == true){
									//一つ目。
									if(t_pair.Value != null){
										t_first = false;

										a_stringbuilder.Append("\"");
										a_stringbuilder.Append(t_pair.Key);
										a_stringbuilder.Append("\":");

										t_pair.Value.ValueToJsonString(a_stringbuilder);
									}else{
										//NULL処理。
										t_first = false;

										a_stringbuilder.Append("\"");
										a_stringbuilder.Append(t_pair.Key);
										a_stringbuilder.Append("\":null");
									}
								}else{
									//二つ目以降。
									if(t_pair.Value != null){

										a_stringbuilder.Append(",\"");
										a_stringbuilder.Append(t_pair.Key);
										a_stringbuilder.Append("\":");

										t_pair.Value.ValueToJsonString(a_stringbuilder);
									}else{
										//NULL処理。

										a_stringbuilder.Append(",\"");
										a_stringbuilder.Append(t_pair.Key);
										a_stringbuilder.Append("\":null");
									}
								}
							}
						}else{
							Tool.Assert(false);
						}
					}catch(System.Exception t_exception){
						Tool.DebugReThrow(t_exception);
					}

					a_stringbuilder.Append("}");
					return;
				}break;
			case ValueType.IndexArray:
				{
					a_stringbuilder.Append("[");

					try{
						System.Collections.Generic.List<JsonItem> t_indexarray = this.value.GetIndexArray();
						if(t_indexarray != null){
							int t_count = t_indexarray.Count;
							int t_index = 0;

							//一つ目。
							if(t_count > 0){

								if(t_indexarray[0] != null){
									t_indexarray[0].ValueToJsonString(a_stringbuilder);
								}else{
									//NULL処理。
									a_stringbuilder.Append("null");
								}

								t_index++;

								//二つ目以降。
								for(;t_index<t_count;t_index++){

									a_stringbuilder.Append(",");

									if(t_indexarray[t_index] != null){
										t_indexarray[t_index].ValueToJsonString(a_stringbuilder);
									}else{
										//NULL処理。
										a_stringbuilder.Append("null");
									}
								}
							}
						}else{
							Tool.Assert(false);
						}
					}catch(System.Exception t_exception){
						Tool.DebugReThrow(t_exception);
					}

					a_stringbuilder.Append("]");
					return;
				}break;
			case ValueType.StringData:
				{
					a_stringbuilder.Append("\"");

					try{
						//特殊文字 ==> ＪＳＯＮ文字列。
						string t_value = this.value.GetStringData();
						Impl.ConvertSpecialStringToJsonString(t_value,a_stringbuilder);
					}catch(System.Exception t_exception){
						Tool.DebugReThrow(t_exception);
					}

					a_stringbuilder.Append( "\"");
					return;
				}break;
			case ValueType.SignedNumber:
				{
					try{
						SIGNED_NUMBER_TYPE t_value = this.value.GetSignedNumber();
						string t_string = t_value.ToString(Config.CULTURE);
						a_stringbuilder.Append(t_string);
					}catch(System.Exception t_exception){
						Tool.DebugReThrow(t_exception);
					}

					return;
				}break;
			case ValueType.UnsignedNumber:
				{
					try{
						UNSIGNED_NUMBER_TYPE t_value = this.value.GetUnsignedNumber();
						string t_string = t_value.ToString(Config.CULTURE);
						a_stringbuilder.Append(t_string);
					}catch(System.Exception t_exception){
						Tool.DebugReThrow(t_exception);
					}

					return;
				}break;
			case ValueType.FloatingNumber:
				{
					try{
						FLOATING_NUMBER_TYPE t_value = this.value.GetFloatingNumber();
						string t_string = string.Format(Config.CULTURE,DOUBLE_TO_STRING_FORMAT,t_value);
						a_stringbuilder.Append(t_string);
					}catch(System.Exception t_exception){
						Tool.DebugReThrow(t_exception);
					}

					return;
				}break;
			case ValueType.BoolData:
				{
					try{
						if(this.value.GetBoolData() == true){
							a_stringbuilder.Append("true");
						}else{
							a_stringbuilder.Append("false");
						}
					}catch(System.Exception t_exception){
						Tool.DebugReThrow(t_exception);
					}

					return;
				}break;
			case ValueType.DecimalNumber:
				{
					a_stringbuilder.Append("\"");

					try{
						System.Decimal t_value = this.value.GetDecimalNumber();
						string t_string = t_value.ToString(Config.CULTURE);
						a_stringbuilder.Append(t_string);
					}catch(System.Exception t_exception){
						Tool.DebugReThrow(t_exception);
					}

					a_stringbuilder.Append("\"");

					return;
				}break;
			case ValueType.BinaryData:
				{
					a_stringbuilder.Append("<");

					try{
						System.Collections.Generic.List<byte> t_binarydata = this.value.GetBinaryData();
						if(t_binarydata != null){
							int t_count = t_binarydata.Count;
							int t_index = 0;

							//一つ目。
							if(t_count > 0){
								a_stringbuilder.Append(string.Format("{0:X2}",t_binarydata[t_index]));

								t_index++;

								//二つ目以降。
								for(;t_index<t_count;t_index++){
									a_stringbuilder.Append(string.Format("{0:X2}",t_binarydata[t_index]));
								}
							}
						}else{
							Tool.Assert(false);
						}
					}catch(System.Exception t_exception){
						Tool.DebugReThrow(t_exception);
					}

					a_stringbuilder.Append(">");
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
						System.Text.StringBuilder t_stringbuilder = new System.Text.StringBuilder();

						//ＪＳＯＮ文字列 ==> 特殊文字。
						try{
							Impl.ConvertJsonStringToSpecialString(this.jsonstring,1,this.jsonstring.Length - 2,t_stringbuilder);
						}catch(System.Exception t_exception){
							Tool.DebugReThrow(t_exception);
						}

						this.value.raw = t_stringbuilder.ToString();
						this.jsonstring = null;
						return;
					}break;
				case ValueType.SignedNumber:
					{
						SIGNED_NUMBER_TYPE t_value;

						if(SIGNED_NUMBER_TYPE.TryParse(this.jsonstring,out t_value) == true){
							this.value.raw = t_value;
							this.jsonstring = null;
							return;
						}else{
							Tool.Assert(false);
						}
					}break;
				case ValueType.UnsignedNumber:
					{
						UNSIGNED_NUMBER_TYPE t_value;

						if(UNSIGNED_NUMBER_TYPE.TryParse(this.jsonstring,out t_value) == true){
							this.value.raw = t_value;
							this.jsonstring = null;
							return;
						}else{
							Tool.Assert(false);
						}
					}break;
				case ValueType.FloatingNumber:
					{
						FLOATING_NUMBER_TYPE t_value;

						if(FLOATING_NUMBER_TYPE.TryParse(this.jsonstring,Config.STRING_TO_DOBULE_NUMBERSTYLE,Config.CULTURE,out t_value) == true){
							this.value.raw = t_value;
							this.jsonstring = null;
							return;
						}else{
							//TODO:decimal
							decimal t_value_decimal;
							if(decimal.TryParse(this.jsonstring,Config.STRING_TO_DOBULE_NUMBERSTYLE,Config.CULTURE,out t_value_decimal) == true){
								this.value.raw = t_value;
								this.jsonstring =  null;
								this.value.valuetype = ValueType.DecimalNumber;
							}else{
								Tool.Assert(false);
							}
						}
					}break;
				case ValueType.IndexArray:
					{
						System.Collections.Generic.List<JsonItem> t_value = new System.Collections.Generic.List<JsonItem>(4 + this.jsonstring.Length / 7);

						try{
							Impl.CreateIndexArrayFromJsonString(this.jsonstring,ref t_value);
						}catch(System.Exception t_exception){
							Tool.DebugReThrow(t_exception);
						}

						this.value.raw = t_value;
						this.jsonstring = null;
						return;
					}break;
				case ValueType.AssociativeArray:
					{
						System.Collections.Generic.Dictionary<string,JsonItem> t_value = new System.Collections.Generic.Dictionary<string,JsonItem>();

						try{
							Impl.CreateAssociativeArrayFromJsonString(this.jsonstring,ref t_value);
						}catch(System.Exception t_exception){
							Tool.DebugReThrow(t_exception);
						}

						this.value.raw = t_value;
						this.jsonstring = null;
						return;
					}break;
				case ValueType.BoolData:
					{
						switch(this.jsonstring[0]){
						case 't':
						case 'T':
							{
								this.value.raw = true;
								this.jsonstring = null;
								return;
							}break;
						case 'f':
						case 'F':
							{
								this.value.raw = false;
								this.jsonstring = null;
								return;
							}break;
						default:
							{
								Tool.Assert(false);
							}break;
						}
					}break;
				case ValueType.BinaryData:
					{
						System.Collections.Generic.List<byte> t_value = new System.Collections.Generic.List<byte>(4 + this.jsonstring.Length / 2);

						try{
							Impl.CreateBinaryDataFromJsonString(this.jsonstring,ref t_value);
						}catch(System.Exception t_exception){
							Tool.DebugReThrow(t_exception);
						}

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

