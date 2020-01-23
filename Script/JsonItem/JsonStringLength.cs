

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。実装。
*/


/** Unreachable code detected.
*/
#pragma warning disable 0162


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** JsonStringLength
	*/
	public class JsonStringLength
	{
		/** FALSE
		*/
		public static char[] STRING_FALSE_1 = {'F','A','L','S','E'};
		public static char[] STRING_FALSE_2 = {'f','a','l','s','e'};

		/** TRUE
		*/
		public static char[] STRING_TRUE_1 = {'T','R','U','E'};
		public static char[] STRING_TRUE_2 = {'t','r','u','e'};

		/** NULL
		*/
		public static char[] STRING_NULL_1 = {'n','u','l','l'};
		public static char[] STRING_NULL_2 = {'N','U','L','L'};

		/** GetCharLength
		*/
		private static int GetCharLength(string a_string,int a_index)
		{
			if(a_string[a_index] == '\\'){
				if((a_index + 1) < a_string.Length){
					return 2;
				}else{
					//「\\」の後ろがない。
					Tool.Assert(false);
					return 1;
				}
			}else{
				//通常文字。
				return 1;
			}
		}

		/** 文字データ。
		*/
		public static int GetStringDataLength(string a_string,int a_index)
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
							int t_use_index = GetCharLength(a_string,t_index);
							if(t_use_index > 0){
								t_index += t_use_index;
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

		/** 数値。
		*/
		public static int GetNumberLength(string a_string,int a_index)
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
						}break;
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
					case 'e':
					case 'E':
					case 'm':
					case 'M':
					case 'f':
					case 'F':
					case 'l':
					case 'L':
						{
							//数値。
							t_index++;
						}break;
					default:
						{
							//不明。
							Tool.Assert(false);

							return 0;
						}break;	
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

		/** 連想配列。
		*/
		public static int GetAssociateArrayLength(string a_string,int a_index)
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
							int t_add = GetStringDataLength(a_string,t_index);
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

		/** インデックス配列。
		*/
		public static int GetIndexArrayLength(string a_string,int a_index)
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
							int t_add = GetStringDataLength(a_string,t_index);
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

		/** 真偽データ。True。
		*/
		public static int GetBoolDataTrueLength(string a_string,int a_index)
		{
			for(int ii=0;ii<STRING_TRUE_1.Length;ii++){
				int t_index = a_index + ii;

				if(t_index < a_string.Length){
					if((a_string[t_index] == STRING_TRUE_1[ii])||(a_string[t_index] == STRING_TRUE_2[ii])){
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
				int t_index = a_index + STRING_TRUE_1.Length;

				if(t_index < a_string.Length){
					if((a_string[t_index] == '}')||(a_string[t_index] == ']')||(a_string[t_index] == ',')){
						//終端。
						return STRING_TRUE_1.Length;
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

		/** 真偽データ。False。
		*/
		public static int GetBoolDataFalseLength(string a_string,int a_index)
		{
			for(int ii=0;ii<STRING_FALSE_1.Length;ii++){
				int t_index = a_index + ii;

				if(t_index < a_string.Length){
					if((a_string[t_index] == STRING_FALSE_1[ii])||(a_string[t_index] == STRING_FALSE_2[ii])){
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
				int t_index = a_index + STRING_FALSE_1.Length;

				if(t_index < a_string.Length){
					if((a_string[t_index] == '}')||(a_string[t_index] == ']')||(a_string[t_index] == ',')){
						//終端。
						return STRING_FALSE_1.Length;
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

		/** バイナリデータ。。
		*/
		public static int GetBinaryDataLength(string a_string,int a_index)
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
							}break;
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
							}break;
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

		/** ＮＵＬＬ。。
		*/
		public static int GetNullLength(string a_string,int a_index)
		{
			for(int ii=0;ii<STRING_NULL_1.Length;ii++){
				int t_index = a_index + ii;

				if(t_index < a_string.Length){
					if((a_string[t_index] == STRING_NULL_1[ii])||(a_string[t_index] == STRING_NULL_2[ii])){
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
				int t_index = a_index + STRING_NULL_1.Length;

				if(t_index < a_string.Length){
					if((a_string[t_index] == '}')||(a_string[t_index] == ']')||(a_string[t_index] == ',')){
						//終端。
						return STRING_NULL_1.Length;
					}else{
						//null以外。
						Tool.Assert(false);

						return 0;					
					}
				}else{
					return STRING_NULL_1.Length;
				}
			}
		}



	}
}

