

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。
*/


/** Unreachable code detected.
*/
#pragma warning disable 0162


/** Fee.File
*/
namespace Fee.File
{
	/** CertificateList
	*/
	public class CertificateList
	{
		/** Item
		*/
		private class Item
		{
			/** url_pattern
			
				^https\:\/\/xxxx\.yyy\.zzz\:\/\/.*$"

			*/
			public string url_pattern;

			/** certificate_string
			*/
			public string certificate_string;

			/** constructor
			*/
			public Item(string a_url_pattern,string a_certificate_string)
			{
				//url_pattern
				this.url_pattern = a_url_pattern;

				//certificate_string
				this.certificate_string = a_certificate_string;
			}
		}

		/** list
		*/
		private System.Collections.Generic.Dictionary<string,Item> list;

		/** constructor
		*/
		public CertificateList()
		{
			//list
			this.list = new System.Collections.Generic.Dictionary<string,Item>();
		}

		/** 登録。
		*/
		public void Regist(string a_tag,string a_url_pattern,string a_certificate_string)
		{
			Item t_item = new Item(a_url_pattern,a_certificate_string);
			this.list.Add(a_url_pattern,t_item);
		}

		/** 解除。
		*/
		public void UnRegist(string a_tag)
		{
			this.list.Remove(a_tag);
		}

		/** GetCertificateString
		*/
		public string GetCertificateString(string a_url)
		{
			foreach(System.Collections.Generic.KeyValuePair<string,Item> t_pair in this.list){
				if(System.Text.RegularExpressions.Regex.IsMatch(a_url,t_pair.Key) == true){
					Tool.Log("GetCertificateString.match",t_pair.Value.certificate_string);
					return t_pair.Value.certificate_string;
				}
			}

			Tool.Log("GetCertificateString.mismatch",a_url);
			return null;
		}
	}
}

