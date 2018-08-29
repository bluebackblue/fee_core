using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief テスト。
*/


/** test02

	json

*/
public class test02 : main_base
{
	/** Data
	*/
	class Data
	{
		/** ItemA
		*/
		public struct itemA
		{
			/** ItemB
			*/
			public struct ItemB
			{
				public int data;
			};
			public ItemB item_b;
		};

		/** data
		*/
		public long data_long;
		public int data_int;
		public string data_string;
		public List<itemA> data_list;
		public Dictionary<int,itemA> data_dictionary;

		/** constructor
		*/
		public Data()
		{
			this.data_long = 100;
			this.data_int = 200;
			this.data_string = "300";
			this.data_list = new List<itemA>();
			{
				itemA t_item_a;
				t_item_a.item_b.data = 999;
				this.data_list.Add(t_item_a);
			}
			this.data_dictionary = new Dictionary<int,itemA>();
			{
				itemA t_item_a;
				t_item_a.item_b.data = 888;
				this.data_dictionary.Add(1,t_item_a);
			}
		}
	}
	Data data;

	/** jsonitem
	*/
	private NJsonItem.JsonItem jsonitem;


	/** Start
	*/
	private void Start()
	{
		//data
		this.data = new Data();

		//data => json
		this.jsonitem = NJsonItem.ToJson.Convert(this.data);

		{
			//Debug.Log(this.jsonitem.ConvertJsonString());

			Debug.Log(JsonUtility.ToJson(this.data));
		}
	}

	/** Update
	*/
	private void Update()
	{
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
	}
}

