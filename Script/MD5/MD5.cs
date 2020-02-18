

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＭＤ５。
*/


/** Fee.MD5
*/
namespace Fee.MD5
{
	/** MD5
	*/
	public class MD5
	{
		/** ハッシュ文字。作成。
		*/
		public static string CreateHashString(byte[] a_hash_binary)
		{
			System.Text.StringBuilder t_string_binary = new System.Text.StringBuilder(32);
			for(int ii=0;ii<a_hash_binary.Length;ii++){
				switch(a_hash_binary[ii] & 0xF0){
				case 0x00:
					{
						t_string_binary.Append("0");
					}break;
				case 0x10:
					{
						t_string_binary.Append("1");
					}break;
				case 0x20:
					{
						t_string_binary.Append("2");
					}break;
				case 0x30:
					{
						t_string_binary.Append("3");
					}break;
				case 0x40:
					{
						t_string_binary.Append("4");
					}break;
				case 0x50:
					{
						t_string_binary.Append("5");
					}break;
				case 0x60:
					{
						t_string_binary.Append("6");
					}break;
				case 0x70:
					{
						t_string_binary.Append("7");
					}break;
				case 0x80:
					{
						t_string_binary.Append("8");
					}break;
				case 0x90:
					{
						t_string_binary.Append("9");
					}break;
				case 0xA0:
					{
						t_string_binary.Append("A");
					}break;
				case 0xB0:
					{
						t_string_binary.Append("B");
					}break;
				case 0xC0:
					{
						t_string_binary.Append("C");
					}break;
				case 0xD0:
					{
						t_string_binary.Append("D");
					}break;
				case 0xE0:
					{
						t_string_binary.Append("E");
					}break;
				case 0xF0:
					{
						t_string_binary.Append("F");
					}break;
				default:
					{
						Tool.Assert(false);
					}break;
				}

				switch(a_hash_binary[ii] & 0x0F){
				case 0x00:
					{
						t_string_binary.Append("0");
					}break;
				case 0x01:
					{
						t_string_binary.Append("1");
					}break;
				case 0x02:
					{
						t_string_binary.Append("2");
					}break;
				case 0x03:
					{
						t_string_binary.Append("3");
					}break;
				case 0x04:
					{
						t_string_binary.Append("4");
					}break;
				case 0x05:
					{
						t_string_binary.Append("5");
					}break;
				case 0x06:
					{
						t_string_binary.Append("6");
					}break;
				case 0x07:
					{
						t_string_binary.Append("7");
					}break;
				case 0x08:
					{
						t_string_binary.Append("8");
					}break;
				case 0x09:
					{
						t_string_binary.Append("9");
					}break;
				case 0x0A:
					{
						t_string_binary.Append("A");
					}break;
				case 0x0B:
					{
						t_string_binary.Append("B");
					}break;
				case 0x0C:
					{
						t_string_binary.Append("C");
					}break;
				case 0x0D:
					{
						t_string_binary.Append("D");
					}break;
				case 0x0E:
					{
						t_string_binary.Append("E");
					}break;
				case 0x0F:
					{
						t_string_binary.Append("F");
					}break;
				default:
					{
						Tool.Assert(false);
					}break;
				}
			}

			return t_string_binary.ToString();
		}

		/** CalcMD5
		*/
		public static string CalcMD5(byte[] a_binary,int a_index,int a_length)
		{
			string t_hash_string = null;

			try{
				using(System.Security.Cryptography.MD5CryptoServiceProvider t_md5 = new System.Security.Cryptography.MD5CryptoServiceProvider()){
					byte[] t_hash = t_md5.ComputeHash(a_binary);
					t_hash_string = MD5.CreateHashString(t_hash);
					Tool.Assert(t_hash_string != null);
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_hash_string;
		}

		/** CalcMD5
		*/
		public static string CalcMD5(string a_string)
		{
			byte[] t_binary = Fee.StringConvert.StringToUtf8Binary.Convert(a_string);
			return CalcMD5(t_binary,0,t_binary.Length);
		}
	}
}

