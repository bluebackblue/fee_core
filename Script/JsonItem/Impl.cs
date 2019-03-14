

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＪＳＯＮ。実装。
*/


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** Impl
	*/
	public class Impl
	{
		/** 最初の一文字からタイプを推測。
		*/
		public static ValueType GetValueTypeFromChar(char a_char)
		{
			switch(a_char){
			case '"':
			case '\'':
				{
					return ValueType.StringData;
				}//break;
			case '{':
				{
					return ValueType.AssociativeArray;
				}//break;
			case '[':
				{
					return ValueType.IndexArray;
				}//break;
			case '<':
				{
					return ValueType.BinaryData;
				}//break;
			case 't':
			case 'T':
				{
					return ValueType.Calc_BoolDataTrue;
				}//break;
			case 'f':
			case 'F':
				{
					return ValueType.Calc_BoolDataFalse;
				}//break;
			case '-':
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
				{
					return ValueType.Calc_UnknownNumber;
				}//break;
			case 'n':
				{
					return ValueType.None;
				}//break;
			default:
				{
					//不明な開始文字。
					Tool.Assert(false);

					return ValueType.None;
				}//break;
			}
		}

		/** 整数チェック。
		*/
		public static bool IsInteger(string a_string)
		{
			return System.Text.RegularExpressions.Regex.IsMatch(a_string,"^[-]?[0-9]+$");
		}

		/** エスケープシーケンス文字を「￥＋文字」に変換。
		*/
		public static string CheckEscapeSequence(char a_char)
		{
			switch(a_char){
			case '\\':
				{
					return "\\\\";
				}//break;
			case '\"':
				{
					return "\\\"";
				}//break;
			case '\'':
				{
					return "\\\'";
				}//break;
			case '\n':
				{
					return "\\n";
				}//break;
			default:
				{
				}break;
			}

			//通常文字。
			return null;
		}

		/** 「￥＋文字」をシーケンス文字に変換。
		*/
		public static string ToSequenceString(string a_string,int a_index)
		{
			if(a_index < a_string.Length){
				if(a_string[a_index] == '\\'){
					if((a_index + 1) < a_string.Length){
						switch(a_string[a_index+1]){
						case '\"':
							{
								//ダブルクォーテーション。
								return "\"";
							}//break;
						case '\'':
							{
								//シングルクォーテーション。
								return "\'";
							}//break;
						case '\\':
							{
								//￥。
								return "\\";
							}//break;
						case 'n':
							{
								//改行。
								return "\n";
							}//break;
						case '/':
							{
								//スラッシュ。
								return "/";
							}//break;
						default:
							{
								//知らないエスケープシーケンス。
								Tool.Assert(false);

								return null;
							}//break;
						}
					}else{
						//範囲外。
						Tool.Assert(false);

						return null;
					}
				}else{
					//通常文字。
					Tool.Assert(false);

					return null;
				}
			}else{
				//範囲外。
				Tool.Assert(false);

				return null;
			}

			//不明。
			//Tool.Assert(false);

			//return null;
		}

		/** 文字のサイズ。
		*/
		public static int GetMojiSize(string a_string,int a_index,bool a_escape)
		{
			if(a_index < a_string.Length){
				if(a_string[a_index] == '\\'){
					if(a_escape == true){
						if(a_string.Length >= (2 + a_index)){
							//エスケープシーケンスの後ろは１バイト文字。
							return 2;
						}else{
							//文字数が足りない。
							Tool.Assert(false);

							return 0;
						}
					}else{
						//エスケープシーケンスとして処理しない。
						return 1;
					}
				}else{
					//エスケープシーケンス以外。
					return 1;
				}
			}else{
				//範囲外。
				Tool.Assert(false);

				return 0;
			}
		}

		/** 文字列JSONの長さ。
		*/
		public static int GetLength_StringData(string a_string,int a_index)
		{
			int t_index = a_index;

			if(t_index < a_string.Length){
				if((a_string[t_index] == '"')||(a_string[t_index] == '\'')){
					t_index++;
					while(t_index < a_string.Length){
						if((a_string[t_index] == '"')||(a_string[t_index] == '\'')){
							//終端。
							return t_index - a_index + 1;
						}else{
							//次の文字へ。
							int t_add = Impl.GetMojiSize(a_string,t_index,true);
							if(t_add > 0){
								t_index += t_add;
							}else{
								//予想外の文字コード。
								Tool.Assert(false);

								return 0;
							}
						}
					}
				
					//予想外の終端。
					Tool.Assert(false);

					return 0;
				}else{
					//文字列以外。
					Tool.Assert(false);

					return 0;
				}
			}else{
				//範囲外。
				Tool.Assert(false);

				return 0;
			}
		}

		/** 数字JSONの長さ。
		*/
		public static int GetLength_Number(string a_string,int a_index)
		{
			if(a_index < a_string.Length){
				int t_index = a_index;
				while(a_index < a_string.Length){
					switch(a_string[t_index]){
					case '}':
					case ']':
					case ',':
						{
							//終端。
							return t_index - a_index;
						}//break;
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
					case '.':
					case '+':
					case '-':
						{
							//数値。
							t_index++;
						}break;
					default:
						{
							//不明。
							Tool.Assert(false);

							return 0;
						}//break;	
					}
				}
			
				//終端。
				return t_index - a_index;
			}else{
				//範囲外。
				Tool.Assert(false);

				return 0;
			}
		}

		/** 連想リストJSONの長さ。
		*/
		public static int GetLength_AssociateArray(string a_string,int a_index)
		{
			if(a_index < a_string.Length){
				if(a_string[a_index] == '{'){
					int t_nest = 1;
					int t_index = a_index + 1;
				
					while(t_index < a_string.Length){
						if(a_string[t_index] == '}'){
							if(t_nest <= 1){
								//終端。
								return t_index - a_index + 1;
							}else{
								//ネスト。
								t_nest--;
								t_index++;
							}
						}else if(a_string[t_index] == '{'){
							//ネスト。
							t_nest++;
							t_index++;
						}else if((a_string[t_index] == '"')||(a_string[t_index] == '\'')){
							//文字列。
							int t_add = GetLength_StringData(a_string,t_index);
							if(t_add > 0){
								t_index += t_add;
							}else{
								//予想外の文字コード。
								Tool.Assert(false);

								return 0;
							}
						}else{
							//次へ。
							t_index++;
						}
					}
				
					//終端がない。
					Tool.Assert(false);

					return 0;
				}else{
					//連想配列以外。
					Tool.Assert(false);

					return 0;
				}
			}else{
				//範囲外。
				Tool.Assert(false);

				return 0;
			}
		}

		/** インデックスリストJSONの長さ。
		*/
		public static int GetLength_IndexArray(string a_string,int a_index)
		{
			if(a_index < a_string.Length){
				if(a_string[a_index] == '['){
					int t_nest = 1;
					int t_index = a_index + 1;
				
					while(t_index < a_string.Length){
						if(a_string[t_index] == ']'){
							if(t_nest <= 1){
								//終端。
								return t_index - a_index + 1;
							}else{
								//ネスト。
								t_nest--;
								t_index++;
							}
						}else if(a_string[t_index] == '['){
							//ネスト。
							t_nest++;
							t_index++;
						}else if((a_string[t_index] == '"')||(a_string[t_index] == '\'')){
							//文字列。
							int t_add = GetLength_StringData(a_string,t_index);
							if(t_add > 0){
								t_index += t_add;
							}else{
								//予想外の文字コード。
								Tool.Assert(false);

								return 0;
							}
						}else{
							//次へ。
							t_index++;
						}
					}
				
					//終端がない。
					Tool.Assert(false);

					return 0;	
				}else{
					//配列以外。
					Tool.Assert(false);

					return 0;
				}
			}else{
				//範囲外。
				Tool.Assert(false);

				return 0;
			}
		}

		/** TRUEJSONの長さ。
		*/
		public static int GetLength_BoolTrue(string a_string,int a_index)
		{
			char[] t_true_1 = {'T','R','U','E'};
			char[] t_true_2 = {'t','r','u','e'};

			for(int ii=0;ii<t_true_1.Length;ii++){
				int t_index = a_index + ii;

				if(t_index < a_string.Length){
					if((a_string[t_index] == t_true_1[ii])||(a_string[t_index] == t_true_2[ii])){
					}else{
						//TRUE以外。
						Tool.Assert(false);
						
						return 0;
					}
				}else{
					//TRUE以外。
					Tool.Assert(false);

					return 0;
				}
			}

			{
				int t_index = a_index + t_true_1.Length;

				if(t_index < a_string.Length){
					if((a_string[t_index] == '}')||(a_string[t_index] == ']')||(a_string[t_index] == ',')){
						//終端。
						return t_true_1.Length;
					}else{
						//TRUE以外。
						Tool.Assert(false);

						return 0;					
					}
				}else{
					//TRUE以外。
					Tool.Assert(false);

					return 0;
				}
			}
		}

		/** FALSEJSONの長さ。
		*/
		public static int GetLength_BoolFalse(string a_string,int a_index)
		{
			char[] t_true_1 = {'F','A','L','S','E'};
			char[] t_true_2 = {'f','a','l','s','e'};

			for(int ii=0;ii<t_true_1.Length;ii++){
				int t_index = a_index + ii;

				if(t_index < a_string.Length){
					if((a_string[t_index] == t_true_1[ii])||(a_string[t_index] == t_true_2[ii])){
					}else{
						//FALSE以外。
						Tool.Assert(false);
						
						return 0;
					}
				}else{
					//FALSE以外。
					Tool.Assert(false);

					return 0;
				}
			}

			{
				int t_index = a_index + t_true_1.Length;

				if(t_index < a_string.Length){
					if((a_string[t_index] == '}')||(a_string[t_index] == ']')||(a_string[t_index] == ',')){
						//終端。
						return t_true_1.Length;
					}else{
						//FALSE以外。
						Tool.Assert(false);

						return 0;					
					}
				}else{
					//FALSE以外。
					Tool.Assert(false);

					return 0;
				}
			}
		}

		/** BinaryDataの長さ。
		*/
		public static int GetLength_BinaryData(string a_string,int a_index)
		{
			if(a_index < a_string.Length){
				if(a_string[a_index] == '<'){
					int t_index = a_index + 1;
					while(t_index < a_string.Length){
						switch(a_string[t_index]){
						case '>':
							{
								//終端。
								return t_index - a_index + 1;
							}//break;
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
						case 'a':
						case 'A':
						case 'b':
						case 'B':
						case 'c':
						case 'C':
						case 'd':
						case 'D':
						case 'e':
						case 'E':
						case 'f':
						case 'F':
							{
								//次へ。
								t_index++;
							}break;
						default:
							{
								//不明。
								Tool.Assert(false);

								return 0;
							}//break;
						}
					}
				
					//終端がない。
					Tool.Assert(false);

					return 0;	
				}else{
					//バイナリデータ以外。
					Tool.Assert(false);

					return 0;
				}
			}else{
				//範囲外。
				Tool.Assert(false);

				return 0;
			}
		}

		/** NULLJSONの長さ。
		*/
		public static int GetLength_Null(string a_string,int a_index)
		{
			char[] t_null = {'n','u','l','l'};

			for(int ii=0;ii<t_null.Length;ii++){
				int t_index = a_index + ii;

				if(t_index < a_string.Length){
					if(a_string[t_index] == t_null[ii]){
					}else{
						//null以外。
						Tool.Assert(false);
						
						return 0;
					}
				}else{
					//null以外。
					Tool.Assert(false);

					return 0;
				}
			}

			{
				int t_index = a_index + t_null.Length;

				if(t_index < a_string.Length){
					if((a_string[t_index] == '}')||(a_string[t_index] == ']')||(a_string[t_index] == ',')){
						//終端。
						return t_null.Length;
					}else{
						//null以外。
						Tool.Assert(false);

						return 0;					
					}
				}else{
					return t_null.Length;
				}
			}
		}

		/** JSON文字からインデックスリストの作成[*,*,*]。
		*/
		public static System.Collections.Generic.List<JsonItem> CreateIndexArrayFromJsonString(string a_jsonstring)
		{
			System.Collections.Generic.List<JsonItem> t_indexlist = new System.Collections.Generic.List<JsonItem>();
		
			int t_index = 0;
			while(t_index < a_jsonstring.Length){
				if(a_jsonstring[t_index] == ']'){
					//終端。
					Tool.Assert(t_index + 1 == a_jsonstring.Length);

					return t_indexlist;
				}else if(a_jsonstring[t_index] == ','){
					//次の項目あり。
					t_index++;
				}else if(a_jsonstring[t_index] == '['){
					if(t_index == 0){
						//開始。
						t_index++;
						continue;
					}else if(t_index == 1){
						//インデックスリストの中にインデックスリスト。
					}else{
						//不明。
						Tool.Assert(false);

						t_indexlist.Clear();
						return t_indexlist;
					}
				}
		
				//値。
				int t_value_size = 0;
				switch(GetValueTypeFromChar(a_jsonstring[t_index])){
				case ValueType.StringData:
					{
						t_value_size = GetLength_StringData(a_jsonstring,t_index);
					}break;
				case ValueType.Calc_UnknownNumber:
				case ValueType.IntegerNumber:
				case ValueType.FloatingNumber:
					{
						t_value_size = GetLength_Number(a_jsonstring,t_index);
					}break;
				case ValueType.AssociativeArray:
					{
						t_value_size = GetLength_AssociateArray(a_jsonstring,t_index);
					}break;
				case ValueType.IndexArray:
					{
						t_value_size = GetLength_IndexArray(a_jsonstring,t_index);
					}break;
				case ValueType.Calc_BoolDataTrue:
					{
						t_value_size = GetLength_BoolTrue(a_jsonstring,t_index);
					}break;
				case ValueType.Calc_BoolDataFalse:
					{
						t_value_size = GetLength_BoolFalse(a_jsonstring,t_index);
					}break;
				case ValueType.BinaryData:
					{
						t_value_size = GetLength_BinaryData(a_jsonstring,t_index);
					}break;
				case ValueType.None:
					{
						t_value_size = GetLength_Null(a_jsonstring,t_index);
					}break;
				case ValueType.BoolData:
				default:
					{
						//不明。
						Tool.Assert(false);

						t_indexlist.Clear();
						return t_indexlist;
					}//break;
				}
			
				//リストの最後に追加。
				if(t_value_size > 0){

					//#if defined(new)
					//#undef new
					//#endif

					JsonItem t_additem = new JsonItem();

					//#if defined(custom_new)
					//#define new custom_new
					//#endif

					{
						t_additem.SetJsonString(a_jsonstring.Substring(t_index,t_value_size));
					}

					t_indexlist.Add(t_additem);

					t_index += t_value_size;
				}else{
					Tool.Assert(false);

					t_indexlist.Clear();
					return t_indexlist;
				}
			}
		
			//範囲外。
			Tool.Assert(false);

			t_indexlist.Clear();
			return t_indexlist;
		}

		/** JSON文字から連想配列の作成。
		*/
		public static System.Collections.Generic.Dictionary<string,JsonItem> CreateAssociativeArrayFromJsonString(string a_jsonstring)
		{
			System.Collections.Generic.Dictionary<string,JsonItem> t_associativelist = new System.Collections.Generic.Dictionary<string,JsonItem>();
		
			int t_index = 0;
			while(t_index < a_jsonstring.Length){
				if(a_jsonstring[t_index] == '}'){
					//終端。
					Tool.Assert((t_index + 1) == a_jsonstring.Length);

					return t_associativelist;
				}else if(a_jsonstring[t_index] == ','){
					//次の項目あり。
					t_index++;
				}else if(a_jsonstring[t_index] == '{'){
					if(t_index == 0){
						//開始。
						t_index++;
						continue;
					}else{
						//不明。
						Tool.Assert(false);

						t_associativelist.Clear();
						return t_associativelist;
					}
				}
			
				//名前。
				string t_name_string;
				if((a_jsonstring[t_index] == '"')||(a_jsonstring[t_index] == '\'')){
					int t_name_size = GetLength_StringData(a_jsonstring,t_index);
					if(t_name_size >= 2){
						t_name_string = a_jsonstring.Substring(t_index + 1,t_name_size - 2);
						t_index += t_name_size;
					}else{
						//不明。
						Tool.Assert(false);

						t_associativelist.Clear();
						return t_associativelist;
					}
				}else{
					//不明。
					Tool.Assert(false);

					t_associativelist.Clear();
					return t_associativelist;
				}
			
				//「:」。
				if(a_jsonstring[t_index] == ':'){
					t_index++;
				}else{
					//不明。
					Tool.Assert(false);

					t_associativelist.Clear();
					return t_associativelist;
				}
			
				//値。
				int t_value_size = 0;
				switch(GetValueTypeFromChar(a_jsonstring[t_index])){
				case ValueType.StringData:
					{
						t_value_size = GetLength_StringData(a_jsonstring,t_index);
					}break;
				case ValueType.Calc_UnknownNumber:
				case ValueType.IntegerNumber:
				case ValueType.FloatingNumber:
					{
						t_value_size = GetLength_Number(a_jsonstring,t_index);
					}break;
				case ValueType.AssociativeArray:
					{
						t_value_size = GetLength_AssociateArray(a_jsonstring,t_index);
					}break;
				case ValueType.IndexArray:
					{
						t_value_size = GetLength_IndexArray(a_jsonstring,t_index);
					}break;
				case ValueType.Calc_BoolDataTrue:
					{
						t_value_size = GetLength_BoolTrue(a_jsonstring,t_index);
					}break;
				case ValueType.Calc_BoolDataFalse:
					{
						t_value_size = GetLength_BoolFalse(a_jsonstring,t_index);
					}break;
				case ValueType.BinaryData:
					{
						t_value_size = GetLength_BinaryData(a_jsonstring,t_index);
					}break;
				case ValueType.None:
					{
						t_value_size = GetLength_Null(a_jsonstring,t_index);
					}break;
				case ValueType.BoolData:
				default:
					{
						//不明。
						Tool.Assert(false);

						t_associativelist.Clear();
						return t_associativelist;
					}//break;
				}
			
				//リストに追加。
				if(t_value_size > 0){

					//#if defined(new)
					//#undef new
					//#endif

					JsonItem t_additem = new JsonItem();

					//#if defined(custom_new)
					//#define new custom_new
					//#endif

					{
						t_additem.SetJsonString(a_jsonstring.Substring(t_index,t_value_size));
					}

					t_associativelist.Add(t_name_string,t_additem);

					t_index += t_value_size;
				}else{
					Tool.Assert(false);

					t_associativelist.Clear();
					return t_associativelist;
				}
			}
		
			//範囲外。
			Tool.Assert(false);

			t_associativelist.Clear();
			return t_associativelist;
		}

		/** JSON文字からバイナリデータの作成。
		*/
		public static System.Collections.Generic.List<byte> CreateBinaryDataFromJsonString(string a_jsonstring)
		{
			System.Collections.Generic.List<byte> t_binarydata = new System.Collections.Generic.List<byte>();

			//t_binarydata.reserve(a_jsonstring.Length / 2);
			t_binarydata.Capacity = a_jsonstring.Length / 2;

			int t_index = 0;
			while(t_index < a_jsonstring.Length){
				if(a_jsonstring[t_index] == '>'){
					//終端。
					Tool.Assert((t_index + 1) == a_jsonstring.Length);

					return t_binarydata;
				}else if(a_jsonstring[t_index] == '<'){
					if(t_index == 0){
						//開始。
						t_index++;
						continue;
					}else{
						//不明。
						Tool.Assert(false);

						t_binarydata.Clear();
						return t_binarydata;
					}
				}else{

					byte t_binary = 0x00;

					switch(a_jsonstring[t_index]){
					case '0':t_binary = 0x00;break;
					case '1':t_binary = 0x10;break;
					case '2':t_binary = 0x20;break;
					case '3':t_binary = 0x30;break;
					case '4':t_binary = 0x40;break;
					case '5':t_binary = 0x50;break;
					case '6':t_binary = 0x60;break;
					case '7':t_binary = 0x70;break;
					case '8':t_binary = 0x80;break;
					case '9':t_binary = 0x90;break;
					case 'a':t_binary = 0xA0;break;
					case 'A':t_binary = 0xA0;break;
					case 'b':t_binary = 0xB0;break;
					case 'B':t_binary = 0xB0;break;
					case 'c':t_binary = 0xC0;break;
					case 'C':t_binary = 0xC0;break;
					case 'd':t_binary = 0xD0;break;
					case 'D':t_binary = 0xD0;break;
					case 'e':t_binary = 0xE0;break;
					case 'E':t_binary = 0xE0;break;
					case 'f':t_binary = 0xF0;break;
					case 'F':t_binary = 0xF0;break;
					default:
						{
							//不明。
							Tool.Assert(false);

							t_binarydata.Clear();
							return t_binarydata;
						}//break;
					}

					t_index++;

					if(t_index < a_jsonstring.Length){
						switch(a_jsonstring[t_index]){
						case '0':t_binary |= 0x00;break;
						case '1':t_binary |= 0x01;break;
						case '2':t_binary |= 0x02;break;
						case '3':t_binary |= 0x03;break;
						case '4':t_binary |= 0x04;break;
						case '5':t_binary |= 0x05;break;
						case '6':t_binary |= 0x06;break;
						case '7':t_binary |= 0x07;break;
						case '8':t_binary |= 0x08;break;
						case '9':t_binary |= 0x09;break;
						case 'a':t_binary |= 0x0A;break;
						case 'A':t_binary |= 0x0A;break;
						case 'b':t_binary |= 0x0B;break;
						case 'B':t_binary |= 0x0B;break;
						case 'c':t_binary |= 0x0C;break;
						case 'C':t_binary |= 0x0C;break;
						case 'd':t_binary |= 0x0D;break;
						case 'D':t_binary |= 0x0D;break;
						case 'e':t_binary |= 0x0E;break;
						case 'E':t_binary |= 0x0E;break;
						case 'f':t_binary |= 0x0F;break;
						case 'F':t_binary |= 0x0F;break;
						default:
							{
								//不明。
								Tool.Assert(false);

								t_binarydata.Clear();
								return t_binarydata;
							}//break;
						}

						t_index++;

						t_binarydata.Add(t_binary);
					}else{
						//不明。
						Tool.Assert(false);

						t_binarydata.Clear();
						return t_binarydata;
					}
				}
			}

			//範囲外。
			Tool.Assert(false);

			t_binarydata.Clear();
			return t_binarydata;
		}

	}
}

