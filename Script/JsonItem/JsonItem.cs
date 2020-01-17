

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。
*/


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** JsonItem
	*/
	public class JsonItem : Config
	{
		/** ＪＳＯＮ文字列。
		*/
		private string jsonstring;

		/** バリアント型。タイプ。
		*/
		private ValueType valuetype;

		/** バリアント型。
		*/
		public Value value;

		/** constructor
		*/
		public JsonItem()
		{
			this.jsonstring = null;
			this.valuetype = ValueType.None;
			this.value.Reset();
		}

		/** constructor
		*/
		public JsonItem(string a_jsonstring)
		{
			this.SetJsonString(a_jsonstring);
		}

		/** constructor

			0:連想配列。

		*/
		public JsonItem(Value_AssociativeArray a_value)
		{
			this.SetAssociativeArray();
		}

		/** constructor

			1:インデックス配列。

		*/
		public JsonItem(Value_IndexArray a_value)
		{
			this.SetIndexArray();
		}

		/** constructor

			2:文字データ。

		*/
		public JsonItem(Value_StringData a_value)
		{
			this.SetStringData(a_value.value);
		}

		/** constructor

			3:バイナリデータ。

		*/
		public JsonItem(Value_BinaryData a_value)
		{
			this.SetBinaryData(a_value.value);
		}

		/** constructor

			11:bool

		*/
		public JsonItem(Value_Number<bool> a_value)
		{
			this.SetBoolData(a_value.value);
		}

		/** constructor

			12:char

		*/
		public JsonItem(Value_Number<char> a_value)
		{
			this.SetChar(a_value.value);
		}

		/** constructor

			13:float

		*/
		public JsonItem(Value_Number<float> a_value)
		{
			this.SetFloat(a_value.value);
		}

		/** constructor

			14:double

		*/
		public JsonItem(Value_Number<double> a_value)
		{
			this.SetDouble(a_value.value);
		}

		/** constructor

			15:decimal

		*/
		public JsonItem(Value_Number<decimal> a_value)
		{
			this.SetDecimal(a_value.value);
		}

		/** constructor

			16:sbyte

		*/
		public JsonItem(Value_Number<sbyte> a_value)
		{
			this.SetSbyte(a_value.value);
		}

		/** constructor

			17:byte

		*/
		public JsonItem(Value_Number<byte> a_value)
		{
			this.SetByte(a_value.value);
		}

		/** constructor

			18:short

		*/
		public JsonItem(Value_Number<short> a_value)
		{
			this.SetShort(a_value.value);
		}

		/** constructor

			19:ushort

		*/
		public JsonItem(Value_Number<ushort> a_value)
		{
			this.SetUshort(a_value.value);
		}

		/** constructor

			20:int

		*/
		public JsonItem(Value_Number<int> a_value)
		{
			this.SetInt(a_value.value);
		}

		/** constructor

			21:uint

		*/
		public JsonItem(Value_Number<uint> a_value)
		{
			this.SetUint(a_value.value);
		}

		/** constructor

			22:long

		*/
		public JsonItem(Value_Number<long> a_value)
		{
			this.SetLong(a_value.value);
		}

		/** constructor

			23:ulong

		*/
		public JsonItem(Value_Number<ulong> a_value)
		{
			this.SetUlong(a_value.value);
		}

		/** 設定。

			0:連想配列。

		*/
		public void SetAssociativeArray()
		{
			this.jsonstring = null;
			this.value.Reset();
		
			this.valuetype = ValueType.AssociativeArray;
			this.value.associative_array = new System.Collections.Generic.Dictionary<string,JsonItem>();
		}

		/** 設定。

			1:インデックス配列。

		*/
		public void SetIndexArray()
		{
			this.jsonstring = null;
			this.value.Reset();
		
			this.valuetype = ValueType.IndexArray;
			this.value.index_array = new System.Collections.Generic.List<JsonItem>();
		}

		/** 設定。

			2:文字データ。

		*/
		public void SetStringData(string a_value)
		{
			this.jsonstring = null;
			this.value.Reset();
		
			this.valuetype = ValueType.StringData;
			this.value.string_data = a_value;
		}

		/** 設定。

			3:バイナリデータ。

		*/
		public void SetBinaryData(System.Collections.Generic.List<byte> a_value)
		{
			this.jsonstring = null;
			this.value.Reset();

			this.valuetype = ValueType.BinaryData;
			this.value.binary_data = a_value;
		}

		/** 設定。

			11:bool

			false / true

		*/
		public void SetBoolData(bool a_value)
		{
			this.jsonstring = null;
			this.value.Reset();

			this.valuetype = ValueType.BoolData;
			this.value.bool_data = a_value;
		}

		/** 設定。

			12:char

			0x0000 -- 0xFFFF

		*/
		public void SetChar(char a_value)
		{
			this.jsonstring = null;
			this.value.Reset();

			this.valuetype = ValueType.SignedNumber;
			this.value.signed_number = a_value;
		}

		/** 設定。

			13:float

			-3.40282347E+38 -- 3.40282347E+38

		*/
		public void SetFloat(float a_value)
		{
			this.jsonstring = null;
			this.value.Reset();

			this.valuetype = ValueType.FloatingNumber;
			this.value.floating_number = a_value;
		}

		/** 設定。

			14:double

			-1.7976931348623157E+308 -- 1.7976931348623157E+308

		*/
		public void SetDouble(double a_value)
		{
			this.jsonstring = null;
			this.value.Reset();

			this.valuetype = ValueType.FloatingNumber;
			this.value.floating_number = a_value;
		}

		/** 設定。

			15:decimal

			-79228162514264337593543950335 -- 79228162514264337593543950335

		*/
		public void SetDecimal(decimal a_value)
		{
			this.jsonstring = null;
			this.value.Reset();

			this.valuetype = ValueType.SignedNumber;
			if(a_value < long.MinValue){
				this.value.signed_number = long.MinValue;
			}else if(a_value > long.MaxValue){
				this.value.signed_number = long.MaxValue;
			}else{
				this.value.signed_number = (long)a_value;
			}
		}

		/** 設定。

			16:sbyte

			-128 -- 127

		*/
		public void SetSbyte(sbyte a_value)
		{
			this.jsonstring = null;
			this.value.Reset();

			this.valuetype = ValueType.SignedNumber;
			this.value.signed_number = a_value;
		}

		/** 設定。

			17:byte

			0 -- 255

		*/
		public void SetByte(byte a_value)
		{
			this.jsonstring = null;
			this.value.Reset();

			this.valuetype = ValueType.SignedNumber;
			this.value.signed_number = a_value;
		}

		/** 設定。

			18:short

			-32768 -- 32767

		*/
		public void SetShort(short a_value)
		{
			this.jsonstring = null;
			this.value.Reset();

			this.valuetype = ValueType.SignedNumber;
			this.value.signed_number = a_value;
		}

		/** 設定。

			19:ushort

			0 -- 65535

		*/
		public void SetUshort(ushort a_value)
		{
			this.jsonstring = null;
			this.value.Reset();

			this.valuetype = ValueType.SignedNumber;
			this.value.signed_number = a_value;
		}

		/** 設定。

			20:int

			-2147483648 -- 2147483647

		*/
		public void SetInt(int a_value)
		{
			this.jsonstring = null;
			this.value.Reset();

			this.valuetype = ValueType.SignedNumber;
			this.value.signed_number = a_value;
		}

		/** 設定。

			21:uint

			0 -- 4294967295

		*/
		public void SetUint(uint a_value)
		{
			this.jsonstring = null;
			this.value.Reset();

			this.valuetype = ValueType.SignedNumber;
			this.value.signed_number = a_value;
		}

		/** 設定。

			22:long

			-9223372036854775808 -- 9223372036854775807

		*/
		public void SetLong(long a_value)
		{
			this.jsonstring = null;
			this.value.Reset();

			this.valuetype = ValueType.SignedNumber;
			this.value.signed_number = a_value;
		}

		/** 設定。

			23:ulong

			0 -- 18446744073709551615

		*/
		public void SetUlong(ulong a_value)
		{
			this.jsonstring = null;
			this.value.Reset();

			this.valuetype = ValueType.UnsignedNumber;
			this.value.unsigned_number = a_value;
		}

		/** 取得。

			0:連想配列。

		*/
		public System.Collections.Generic.Dictionary<string,JsonItem> GetAssociativeArray()
		{
			Tool.Assert(this.valuetype == ValueType.AssociativeArray);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			return this.value.associative_array;
		}

		/** 取得。

			1:インデックス配列。

		*/
		public System.Collections.Generic.List<JsonItem> GetIndexArray()
		{
			Tool.Assert(this.valuetype == ValueType.IndexArray);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			return this.value.index_array;
		}

		/** 取得。

			2:文字データ。

		*/
		public string GetStringData()
		{
			Tool.Assert(this.valuetype == ValueType.StringData);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			return this.value.string_data;
		}

		/** 取得。

			3:バイナリデータ。

		*/
		public System.Collections.Generic.List<byte> GetBinaryData()
		{
			Tool.Assert(this.valuetype == ValueType.BinaryData);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			return this.value.binary_data;
		}

		/** GetBoolData

			11:bool

		*/
		public bool GetBoolData()
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return ((this.value.signed_number > 0) ? true : false);
				}//break;
			case ValueType.FloatingNumber:
				{
					return ((this.value.floating_number > 0) ? true : false);
				}//break;
			case ValueType.UnsignedNumber:
				{
					return ((this.value.unsigned_number > 0) ? true : false);
				}//break;
			case ValueType.BoolData:
				{
					return this.value.bool_data;
				}//break;
			}

			Tool.Assert(false);
			return false;
		}

		/** GetChar

			12:char

		*/
		public char GetChar()
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (char)this.value.signed_number;
				}//break;
			case ValueType.FloatingNumber:
				{
					return (char)this.value.floating_number;
				}//break;
			case ValueType.UnsignedNumber:
				{
					return (char)this.value.unsigned_number;
				}//break;
			case ValueType.BoolData:
				{
					return (this.value.bool_data ? (char)1 : (char)0);
				}//break;
			}

			Tool.Assert(false);
			return (char)0;
		}

		/** GetFloat

			13:float

		*/
		public float GetFloat()
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (float)this.value.signed_number;
				}//break;
			case ValueType.FloatingNumber:
				{
					return (float)this.value.floating_number;
				}//break;
			case ValueType.UnsignedNumber:
				{
					return (float)this.value.unsigned_number;
				}//break;
			case ValueType.BoolData:
				{
					return (this.value.bool_data ? (float)1 : (float)0);
				}//break;
			}

			Tool.Assert(false);
			return (float)0;
		}

		/** GetDouble

			14:double

		*/
		public double GetDouble()
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (double)this.value.signed_number;
				}//break;
			case ValueType.FloatingNumber:
				{
					return (double)this.value.floating_number;
				}//break;
			case ValueType.UnsignedNumber:
				{
					return (double)this.value.unsigned_number;
				}//break;
			case ValueType.BoolData:
				{
					return (this.value.bool_data ? (double)1 : (double)0);
				}//break;
			}

			Tool.Assert(false);
			return (double)0;
		}

		/** GetDecimal

			15:decimal

		*/
		public decimal GetDecimal()
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (decimal)this.value.signed_number;
				}//break;
			case ValueType.FloatingNumber:
				{
					return (decimal)this.value.floating_number;
				}//break;
			case ValueType.UnsignedNumber:
				{
					return (decimal)this.value.unsigned_number;
				}//break;
			case ValueType.BoolData:
				{
					return (this.value.bool_data ? (decimal)1 : (decimal)0);
				}//break;
			}

			Tool.Assert(false);
			return (decimal)0;
		}

		/** GetSbyte

			16:sbyte

		*/
		public sbyte GetSbyte()
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (sbyte)this.value.signed_number;
				}//break;
			case ValueType.FloatingNumber:
				{
					return (sbyte)this.value.floating_number;
				}//break;
			case ValueType.UnsignedNumber:
				{
					return (sbyte)this.value.unsigned_number;
				}//break;
			case ValueType.BoolData:
				{
					return (this.value.bool_data ? (sbyte)1 : (sbyte)0);
				}//break;
			}

			Tool.Assert(false);
			return (sbyte)0;
		}

		/** GetByte

			17:byte

		*/
		public byte GetByte()
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (byte)this.value.signed_number;
				}//break;
			case ValueType.FloatingNumber:
				{
					return (byte)this.value.floating_number;
				}//break;
			case ValueType.UnsignedNumber:
				{
					return (byte)this.value.unsigned_number;
				}//break;
			case ValueType.BoolData:
				{
					return (this.value.bool_data ? (byte)1 : (byte)0);
				}//break;
			}

			Tool.Assert(false);
			return (byte)0;
		}

		/** GetShort

			18:short

		*/
		public short GetShort()
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (short)this.value.signed_number;
				}//break;
			case ValueType.FloatingNumber:
				{
					return (short)this.value.floating_number;
				}//break;
			case ValueType.UnsignedNumber:
				{
					return (short)this.value.unsigned_number;
				}//break;
			case ValueType.BoolData:
				{
					return (this.value.bool_data ? (short)1 : (short)0);
				}//break;
			}

			Tool.Assert(false);
			return (short)0;
		}

		/** GetUshort

			19:ushort

		*/
		public ushort GetUshort()
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (ushort)this.value.signed_number;
				}//break;
			case ValueType.FloatingNumber:
				{
					return (ushort)this.value.floating_number;
				}//break;
			case ValueType.UnsignedNumber:
				{
					return (ushort)this.value.unsigned_number;
				}//break;
			case ValueType.BoolData:
				{
					return (this.value.bool_data ? (ushort)1 : (ushort)0);
				}//break;
			}

			Tool.Assert(false);
			return (ushort)0;
		}

		/** GetInt

			20:int

		*/
		public int GetInt()
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (int)this.value.signed_number;
				}//break;
			case ValueType.FloatingNumber:
				{
					return (int)this.value.floating_number;
				}//break;
			case ValueType.UnsignedNumber:
				{
					return (int)this.value.unsigned_number;
				}//break;
			case ValueType.BoolData:
				{
					return (this.value.bool_data ? (int)1 : (int)0);
				}//break;
			}

			Tool.Assert(false);
			return (int)0;
		}

		/** GetUint

			21:uint

		*/
		public uint GetUint()
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (uint)this.value.signed_number;
				}//break;
			case ValueType.FloatingNumber:
				{
					return (uint)this.value.floating_number;
				}//break;
			case ValueType.UnsignedNumber:
				{
					return (uint)this.value.unsigned_number;
				}//break;
			case ValueType.BoolData:
				{
					return (this.value.bool_data ? (uint)1 : (uint)0);
				}//break;
			}

			Tool.Assert(false);
			return (uint)0;
		}

		/** GetLong

			22:long

		*/
		public long GetLong()
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (long)this.value.signed_number;
				}//break;
			case ValueType.FloatingNumber:
				{
					return (long)this.value.floating_number;
				}//break;
			case ValueType.UnsignedNumber:
				{
					return (long)this.value.unsigned_number;
				}//break;
			case ValueType.BoolData:
				{
					return (this.value.bool_data ? (long)1 : (long)0);
				}//break;
			}

			Tool.Assert(false);
			return (long)0;
		}

		/** GetUlong

			23:ulong

		*/
		public ulong GetUlong()
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (ulong)this.value.signed_number;
				}//break;
			case ValueType.FloatingNumber:
				{
					return (ulong)this.value.floating_number;
				}//break;
			case ValueType.UnsignedNumber:
				{
					return (ulong)this.value.unsigned_number;
				}//break;
			case ValueType.BoolData:
				{
					return (this.value.bool_data ? (ulong)1 : (ulong)0);
				}//break;
			}

			Tool.Assert(false);
			return (ulong)0;
		}

		/** タイプチェック。ＮＵＬＬ。
		*/
		public bool IsNull()
		{
			if(this.valuetype == ValueType.None){
				return true;
			}
			return false;
		}

		/** タイプチェック。文字データ。
		*/
		public bool IsStringData()
		{
			if(this.valuetype == ValueType.StringData){
				return true;
			}
			return false;
		}

		/** タイプチェック。連想配列。
		*/
		public bool IsAssociativeArray()
		{
			if(this.valuetype == ValueType.AssociativeArray){
				return true;
			}
			return false;
		}

		/** タイプチェック。インデックス配列。
		*/
		public bool IsIndexArray()
		{
			if(this.valuetype == ValueType.IndexArray){
				return true;
			}
			return false;
		}

		/** タイプチェック。整数。
		*/
		public bool IsSignedNumber()
		{
			if(this.valuetype == ValueType.SignedNumber){
				return true;
			}
			return false;
		}

		/** タイプチェック。整数。
		*/
		public bool IsUnSignedNumber()
		{
			if(this.valuetype == ValueType.UnsignedNumber){
				return true;
			}
			return false;
		}

		/** タイプチェック。少数。
		*/
		public bool IsFloatNumber()
		{
			if(this.valuetype == ValueType.FloatingNumber){
				return true;
			}
			return false;
		}

		/** タイプチェック。真偽データ。
		*/
		public bool IsBoolData()
		{
			if(this.valuetype == ValueType.BoolData){
				return true;
			}
			return false;
		}

		/** [取得]GetValueType
		*/
		public ValueType GetValueType()
		{
			return this.valuetype;
		}

		/** ディープコピー。
		*/
		public JsonItem DeepCopy()
		{
			JsonItem t_new_jsonitem = new JsonItem(this.ConvertJsonString());
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
						this.jsonstring = a_jsonstring;
						this.valuetype = t_valuetype;
						this.value.Reset();
						return;
					}//break;
				case ValueType.Calc_UnknownNumber:
					{
						if(Impl.IsFloat(a_jsonstring)){
							this.jsonstring = a_jsonstring;
							this.valuetype = ValueType.FloatingNumber;
							this.value.Reset();
							return;
						}else{
							if(a_jsonstring[0] == '-'){
								//マイナス値。
								this.jsonstring = a_jsonstring;
								this.valuetype = ValueType.SignedNumber;
								this.value.Reset();
								return;
							}else{
								//プラス。
								ulong t_value = ulong.Parse(a_jsonstring);
								if(t_value <= long.MaxValue){
									//signed_numberの範囲内。
									this.jsonstring = a_jsonstring;
									this.valuetype = ValueType.SignedNumber;
									this.value.signed_number = (long)t_value;
									return;
								}else{
									//符号なし。
									this.jsonstring = null;
									this.valuetype = ValueType.UnsignedNumber;
									this.value.unsigned_number = t_value;
									return;
								}
							}
						}
					}//break;
				case ValueType.Calc_BoolDataTrue:
					{
						//値で設定。

						this.jsonstring = null;
						this.valuetype = ValueType.BoolData;
						this.value.bool_data = true;
						return;
					}//break;
				case ValueType.Calc_BoolDataFalse:
					{
						//値で設定。

						this.jsonstring = null;
						this.valuetype = ValueType.BoolData;
						this.value.bool_data = false;
						return;
					}//break;
				case ValueType.None:
					{
						//NULL処理。

						this.jsonstring = null;
						this.valuetype = ValueType.None;
						this.value.Reset();
						return;
					}//break;
				case ValueType.BoolData:
				default:
					{
						Tool.Assert(false);

						this.jsonstring = null;
						this.valuetype = ValueType.None;
						this.value.Reset();
						return;
					}//break;
				}
			}

			Tool.Assert(false);

			this.jsonstring = null;
			this.valuetype = ValueType.None;
			this.value.Reset();
		}

		/** 値化。
		*/
		private void JsonStringToValue()
		{
			string t_jsonstring_temp = this.jsonstring;
			this.jsonstring = null;

			this.value.Reset();

			if(t_jsonstring_temp != null){
				switch(this.valuetype){
				case ValueType.StringData:
					{
						System.Text.StringBuilder t_stringbuilder = new System.Text.StringBuilder();

						//ＪＳＯＮ文字列 ==> 特殊文字。
						Impl.ConvertJsonStringToSpecialString(t_jsonstring_temp,1,t_jsonstring_temp.Length - 2,t_stringbuilder);
				
						this.value.string_data = t_stringbuilder.ToString();

						return;
					}//break;
				case ValueType.SignedNumber:
					{
						this.value.signed_number = long.Parse(t_jsonstring_temp);
						return;
					}//break;
				case ValueType.UnsignedNumber:
					{
						this.value.unsigned_number = ulong.Parse(t_jsonstring_temp);
						return;
					}//break;
				case ValueType.FloatingNumber:
					{
						this.value.floating_number = double.Parse(t_jsonstring_temp,Config.STRING_TO_DOBULE_NUMBERSTYLE,Config.CULTURE);
						return;
					}//break;
				case ValueType.IndexArray:
					{
						this.value.index_array = Impl.CreateIndexArrayFromJsonString(t_jsonstring_temp);
						return;
					}//break;
				case ValueType.AssociativeArray:
					{
						this.value.associative_array = Impl.CreateAssociativeArrayFromJsonString(t_jsonstring_temp);
						return;
					}//break;
				case ValueType.BoolData:
					{
						char t_char = t_jsonstring_temp[0];

						if((t_char == 't')||(t_char == 'T')){
							this.value.bool_data = true;
						}else{
							this.value.bool_data = false;
						}
						return;
					}//break;
				case ValueType.BinaryData:
					{
						this.value.binary_data = Impl.CreateBinaryDataFromJsonString(t_jsonstring_temp);
						return;
					}//break;
				case ValueType.None:
				case ValueType.Calc_BoolDataFalse:
				case ValueType.Calc_BoolDataTrue:
				case ValueType.Calc_UnknownNumber:
				default:
					{
						//不明。
						Tool.Assert(false);

						this.value.Reset();
						return;
					}//break;
				}
			}

			//不明。
			Tool.Assert(false);

			this.value.Reset();
			return;
		}

		/** [取得]GetListMax
		*/
		public int GetListMax()
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}
		
			switch(this.valuetype){
			case ValueType.AssociativeArray:
				{
					return this.value.associative_array.Count;
				}//break;
			case ValueType.IndexArray:
				{
					return this.value.index_array.Count;
				}//break;
			case ValueType.StringData:
				{
					return this.value.string_data.Length;
				}//break;
			case ValueType.None:
			case ValueType.SignedNumber:
			case ValueType.UnsignedNumber:
			case ValueType.FloatingNumber:
			case ValueType.BoolData:
			case ValueType.BinaryData:
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

		/** タイプチェック。バイナリデータ。
		*/
		public bool IsBinaryData()
		{
			if(this.valuetype == ValueType.BinaryData){
				return true;
			}
			return false;
		}

		/** [取得]連想リストのアイテム取得。
		*/
		public JsonItem GetItem(string a_itemname)
		{
			Tool.Assert(this.valuetype == ValueType.AssociativeArray);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			{
				JsonItem t_value;
				if(this.value.associative_array.TryGetValue(a_itemname,out t_value) == true){
					return t_value;
				}
			}

			Tool.Assert(false);

			return null;
		}

		/** [取得]インデックスリストのアイテム取得。
		*/
		public JsonItem GetItem(int a_index)
		{
			Tool.Assert(a_index >= 0);
			int t_index = a_index;

			Tool.Assert(this.valuetype == ValueType.IndexArray);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}
		
			if(t_index < this.value.index_array.Count){
				return this.value.index_array[t_index];
			}
		
			Tool.Assert(false);

			return null;
		}

		/** [取得]連想リストのアイテムチェック。
		*/
		public bool IsExistItem(string a_itemname,ValueType a_valuetype = ValueType.None)
		{
			Tool.Assert(this.valuetype == ValueType.AssociativeArray);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}
		
			{
				JsonItem t_value;
				if(this.value.associative_array.TryGetValue(a_itemname,out t_value) == true){
					if(a_valuetype == ValueType.None){
						return true;
					}else{
						ValueType t_valuetype = t_value.GetValueType();
						if(a_valuetype == t_valuetype){
							return true;
						}else{
							switch(a_valuetype){
							case ValueType.Mask_Integer_Number:
								{
									if((t_valuetype == ValueType.SignedNumber)||(t_valuetype == ValueType.UnsignedNumber)){
										return true;
									}
								}break;
							case ValueType.Mask_All_Number:
								{
									if((t_valuetype == ValueType.SignedNumber)||(t_valuetype == ValueType.UnsignedNumber)||(t_valuetype == ValueType.FloatingNumber)){
										return true;
									}
								}break;
							}
						}
					}
				}
			}
		
			return false;
		}

		/** [取得]インデックスリストのアイテムチェック。
		*/
		public bool IsExistItem(int a_index,ValueType a_valuetype = ValueType.None)
		{
			Tool.Assert(a_index >= 0);
			int t_index = a_index;

			Tool.Assert(this.valuetype == ValueType.IndexArray);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}
		
			if(t_index < this.value.index_array.Count){
				if(a_valuetype == ValueType.None || this.value.index_array[t_index].GetValueType() == a_valuetype){
					return true;
				}

				return true;
			}
		
			return false;
		}

		/** [設定]連想リストにアイテム追加。
		*/
		public void AddItem(string a_itemname,JsonItem a_item,bool a_deepcopy)
		{
			Tool.Assert(this.valuetype == ValueType.AssociativeArray);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			if(a_deepcopy == true){
				this.value.associative_array.Add(a_itemname,a_item.DeepCopy());
			}else{
				this.value.associative_array.Add(a_itemname,a_item);
			}
		}

		/** [設定]インデックスリストにアイテム追加。
		*/
		public void AddItem(JsonItem a_item,bool a_deepcopy)
		{
			Tool.Assert(this.valuetype == ValueType.IndexArray);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}
		
			if(a_deepcopy == true){
				this.value.index_array.Add(a_item.DeepCopy());
			}else{
				this.value.index_array.Add(a_item);
			}
		}

		/** [設定]連想リストにアイテム設定。

			上書き、追加。

		*/
		public void SetItem(string a_itemname,JsonItem a_item,bool a_deepcopy)
		{
			Tool.Assert(this.valuetype == ValueType.AssociativeArray);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			if(a_deepcopy == true){
				if(this.value.associative_array.ContainsKey(a_itemname) == true){
					this.value.associative_array[a_itemname] = a_item.DeepCopy();
				}else{
					this.value.associative_array.Add(a_itemname,a_item.DeepCopy());
				}
			}else{
				if(this.value.associative_array.ContainsKey(a_itemname) == true){
					this.value.associative_array[a_itemname] = a_item;
				}else{
					this.value.associative_array.Add(a_itemname,a_item);
				}
			}
		}

		/** [設定]インデックスリストにアイテム設定。

			上書き。

		*/
		public void SetItem(int a_index,JsonItem a_item,bool a_deepcopy)
		{
			Tool.Assert(this.valuetype == ValueType.IndexArray);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}
		
			if((0<=a_index)&&(a_index<this.value.index_array.Count)){
				if(a_deepcopy == true){
					this.value.index_array[a_index] = a_item.DeepCopy();
				}else{
					this.value.index_array[a_index] = a_item;
				}
			}else{
				Tool.Assert(false);
			}
		}

		/** [削除]インデックスリストからアイテム削除。
		*/
		public void RemoveItem(string a_itemname)
		{
			Tool.Assert(this.valuetype == ValueType.AssociativeArray);
			Tool.Assert(a_itemname != null);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}
		
			this.value.associative_array.Remove(a_itemname);
		}

		/** [削除]インデックスリストからアイテム削除。
		*/
		public void RemoveItem(int a_index)
		{
			Tool.Assert(this.valuetype == ValueType.IndexArray);
			Tool.Assert(a_index >= 0);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}
		
			this.value.index_array.RemoveAt(a_index);
		}

		/** [設定]インデックスリストのサイズ変更。
		*/
		public void ReSize(int a_list_count)
		{
			Tool.Assert(this.valuetype == ValueType.IndexArray);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			if(this.value.index_array.Count < a_list_count){
				while(this.value.index_array.Count < a_list_count){
					this.value.index_array.Add(new JsonItem());
				}
			}else if(this.value.index_array.Count > a_list_count){
				this.value.index_array.RemoveRange(a_list_count,this.value.index_array.Count - a_list_count);
			}
		}

		/** 連想配列キーリスト作成。
		*/
		public System.Collections.Generic.List<string> CreateAssociativeKeyList()
		{
			Tool.Assert(this.valuetype == ValueType.AssociativeArray);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			System.Collections.Generic.List<string> t_ret_keylist = new System.Collections.Generic.List<string>();

			foreach(System.Collections.Generic.KeyValuePair<string,JsonItem> t_pair in this.value.associative_array){
				t_ret_keylist.Add(t_pair.Key);
			}

			return t_ret_keylist;
		}

		/** 連想配列キーリスト総数。
		*/
		public int GetKeyListMax()
		{
			Tool.Assert(this.valuetype == ValueType.AssociativeArray);

			if(this.jsonstring != null){
				this.JsonStringToValue();
			}

			return this.value.associative_array.Count;
		}

		/** 適応。
		*/
		public void Apply()
		{
			if(this.jsonstring != null){
				this.JsonStringToValue();
			}
		}

		/** オブジェクトへコンバート。
		*/
		public Type ConvertObject<Type>()
		{
			System.Type t_type = typeof(Type);
			System.Object t_object = JsonToObject_SystemObject.CreateInstance(t_type,this);
			JsonToObject_SystemObject.Convert(ref t_object,t_type,this,0);
			return (Type)System.Convert.ChangeType(t_object,t_type);
		}

		/** JsonStringへコンバート。
		*/
		public void ConvertJsonString(System.Text.StringBuilder a_stringbuilder)
		{
			if(this.jsonstring != null){
				a_stringbuilder.Append(this.jsonstring);
				return;
			}

			switch(this.valuetype){
			case ValueType.StringData:
				{
					a_stringbuilder.Append("\"");

					//特殊文字 ==> ＪＳＯＮ文字列。
					Impl.ConvertSpecialStringToJsonString(this.value.string_data,a_stringbuilder);

					a_stringbuilder.Append( "\"");

				}return;
			case ValueType.SignedNumber:
				{
					string t_string = this.value.signed_number.ToString();
					a_stringbuilder.Append(t_string);
				}return;
			case ValueType.UnsignedNumber:
				{
					string t_string = this.value.unsigned_number.ToString();
					a_stringbuilder.Append(t_string);
				}return;
			case ValueType.FloatingNumber:
				{
					string t_string = string.Format(Config.CULTURE,DOUBLE_TO_STRING_FORMAT,this.value.floating_number);
					a_stringbuilder.Append(t_string);
				}return;
			case ValueType.IndexArray:
				{
					a_stringbuilder.Append("[");

					{
						int t_count = this.value.index_array.Count;
						int t_index = 0;

						//一つ目。
						if(t_count > 0){

							if(this.value.index_array[0] != null){
								this.value.index_array[0].ConvertJsonString(a_stringbuilder);
							}else{
								//NULL処理。
								a_stringbuilder.Append("null");
							}

							t_index++;

							//二つ目以降。
							for(;t_index<t_count;t_index++){

								a_stringbuilder.Append(",");

								if(this.value.index_array[t_index] != null){
									this.value.index_array[t_index].ConvertJsonString(a_stringbuilder);
								}else{
									//NULL処理。
									a_stringbuilder.Append("null");
								}
							}
						}
					}

					a_stringbuilder.Append("]");

				}return;
			case ValueType.AssociativeArray:
				{
					a_stringbuilder.Append("{");

					{
						bool t_first = true;

						foreach(System.Collections.Generic.KeyValuePair<string,JsonItem> t_pair in this.value.associative_array){
							if(t_first == true){
								//一つ目。
								if(t_pair.Value != null){
									t_first = false;

									a_stringbuilder.Append("\"");
									a_stringbuilder.Append(t_pair.Key);
									a_stringbuilder.Append("\":");

									t_pair.Value.ConvertJsonString(a_stringbuilder);
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

									t_pair.Value.ConvertJsonString(a_stringbuilder);
								}else{
									//NULL処理。

									a_stringbuilder.Append(",\"");
									a_stringbuilder.Append(t_pair.Key);
									a_stringbuilder.Append("\":null");
								}
							}
						}
					}

					a_stringbuilder.Append("}");
				}return;
			case ValueType.BoolData:
				{
					if(this.value.bool_data){
						a_stringbuilder.Append("true");
					}else{
						a_stringbuilder.Append("false");
					}
				}return;
			case ValueType.BinaryData:
				{
					a_stringbuilder.Append("<");

					{
						int t_count = this.value.binary_data.Count;
						int t_index = 0;

						//一つ目。
						if(t_count > 0){
							a_stringbuilder.Append(string.Format("{0:X2}",this.value.binary_data[t_index]));

							t_index++;

							//二つ目以降。
							for(;t_index<t_count;t_index++){
								a_stringbuilder.Append(string.Format("{0:X2}",this.value.binary_data[t_index]));
							}
						}
					}

					a_stringbuilder.Append(">");

				}return;
			case ValueType.None:
				{
					//NULL処理。
					a_stringbuilder.Append("null");
				}return;
			case ValueType.Calc_UnknownNumber:
			case ValueType.Calc_BoolDataTrue:
			case ValueType.Calc_BoolDataFalse:
			default:
				{
					//不明。
					Tool.Assert(false);
				}return;
			}

		}

		/** JsonStringへコンバート。
		*/
		public string ConvertJsonString()
		{
			if(this.jsonstring != null){
				return this.jsonstring;
			}

			System.Text.StringBuilder t_stringbuilder = new System.Text.StringBuilder();
			this.ConvertJsonString(t_stringbuilder);

			return t_stringbuilder.ToString();
		}

		/** JsonStringへコンバート。
		*/
		public string ConvertJsonString(int a_capacity)
		{
			if(this.jsonstring != null){
				return this.jsonstring;
			}

			System.Text.StringBuilder t_stringbuilder = new System.Text.StringBuilder(a_capacity);
			this.ConvertJsonString(t_stringbuilder);

			return t_stringbuilder.ToString();
		}
	}
}
