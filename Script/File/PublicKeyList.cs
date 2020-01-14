

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
	/** PublicKeyList
	*/
	public class PublicKeyList
	{
		/** Item
		*/
		private class Item
		{
			/** url_pattern
			
				^https\:\/\/xxxx\.yyy\.zzz\:\/\/.*$"

			*/
			public string url_pattern;

			/** a_public_key_string
			*/
			public string public_key_string;

			/** constructor
			*/
			public Item(string a_url_pattern,string a_public_key_string)
			{
				//url_pattern
				this.url_pattern = a_url_pattern;

				//a_public_key_string
				this.public_key_string = a_public_key_string;
			}
		}

		/** list
		*/
		private System.Collections.Generic.Dictionary<string,Item> list;

		/** constructor
		*/
		public PublicKeyList()
		{
			//list
			this.list = new System.Collections.Generic.Dictionary<string,Item>();
		}

		/** 登録。
		*/
		public void Regist(string a_tag,string a_url_pattern,string a_public_key_string)
		{
			Item t_item = new Item(a_url_pattern,a_public_key_string);
			this.list.Add(a_url_pattern,t_item);
		}

		/** 解除。
		*/
		public void UnRegist(string a_tag)
		{
			this.list.Remove(a_tag);
		}

		/** GetPublicKey
		*/
		public string GetPublicKey(string a_url)
		{
			foreach(System.Collections.Generic.KeyValuePair<string,Item> t_pair in this.list){
				if(System.Text.RegularExpressions.Regex.IsMatch(a_url,t_pair.Key) == true){
					return t_pair.Value.public_key_string;
				}
			}
			return null;
		}
	}
}

