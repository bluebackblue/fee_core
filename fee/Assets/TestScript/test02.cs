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
		/** RootItem
		*/
		public struct RootItem
		{
			/** SubItem
			*/
			public struct SubItem
			{
				/** data
				*/
				public int data;
			};

			/** subitem
			*/
			public SubItem subitem;
		};

		/** data_long
		*/
		public long data_long;

		/** data_int
		*/
		public int data_int;

		/** data_double
		*/
		public double data_double;

		/** data_float
		*/
		public float data_float;

		/** data_string
		*/
		public string data_string;

		/** data_list
		*/
		public List<RootItem> data_list;

		/** data_dictionary
		*/
		public Dictionary<string,RootItem> data_dictionary;

		/** data_non_support_dictionary
		*/
		public Dictionary<int,int> data_non_support_dictionary;

		/** constructor
		*/
		public Data()
		{
		}

		/** データ。設定。
		*/
		public void SetData()
		{
			this.data_long = 100;
			this.data_int = 200;
			this.data_double = 300.0;
			this.data_float = 400.0f;
			this.data_string = "500";

			this.data_list = new List<RootItem>();
			{
				{
					RootItem t_item;
					t_item.subitem.data = 999;
					this.data_list.Add(t_item);
				}
				{
					RootItem t_item;
					t_item.subitem.data = 888;
					this.data_list.Add(t_item);
				}
			}
			this.data_dictionary = new Dictionary<string,RootItem>();
			{
				{
					RootItem t_item;
					t_item.subitem.data = 777;
					this.data_dictionary.Add("AAA",t_item);
				}
				{
					RootItem t_item;
					t_item.subitem.data = 666;
					this.data_dictionary.Add("BBB",t_item);
				}
			}
			this.data_non_support_dictionary = new Dictionary<int,int>();
			{
				this.data_non_support_dictionary.Add(-100,-200);
			}
		}
	}

	/** Start
	*/
	private void Start()
	{
		Debug.Log("-------------------");

		//オブジェクト => ＪＳＯＮ。
		{
			//データ設定。
			Data t_data = new Data();
			t_data.SetData();

			//オブジェクトをＪＳＯＮ化。
			NJsonItem.JsonItem t_jsonitem = NJsonItem.ToJson.Convert(t_data);

			//ＪＳＯＮを文字列化。
			string t_jsonstring = t_jsonitem.ConvertJsonString();

			//デバッグ出力。
			Debug.Log(t_jsonstring);
		}

		Debug.Log("-------------------");

		//ＪＳＯＮ => オブジェクト。
		{
			//ＪＳＯＮ文字列。
			string t_jsonstring = "{\"data_dictionary\":{\"BBB\":{\"subitem\":{\"data\":666}},\"AAA\":{\"subitem\":{\"data\":777}}},\"data_list\":[{\"subitem\":{\"data\":888}},{\"subitem\":{\"data\":999}}],\"data_string\":\"500\",\"data_float\":400.0,\"data_double\":300.0,\"data_int\":200,\"data_long\":100}";

			//文字列をＪＳＯＮ化。
			NJsonItem.JsonItem t_jsonitem = new NJsonItem.JsonItem(t_jsonstring);

			//ＪＳＯＮをオブジェクト化。
			Data t_data = NJsonItem.FromJson<Data>.Convert(t_jsonitem);

			//デバッグ出力。
			{
				Debug.Log("data_long = " + t_data.data_long.ToString());
				Debug.Log("data_int = " + t_data.data_int.ToString());
				Debug.Log("data_double = " + t_data.data_double.ToString());
				Debug.Log("data_float = " + t_data.data_float.ToString());
				Debug.Log("data_string = " + t_data.data_string.ToString());

				for(int ii=0;ii<t_data.data_list.Count;ii++){
					Debug.Log("data_list[" + ii.ToString() + "] = " + t_data.data_list[ii].subitem.data.ToString());
				}

				foreach(KeyValuePair<string,Data.RootItem> t_pair in t_data.data_dictionary){
					Debug.Log("data_dictionary[" + t_pair.Key + "] = " + t_pair.Value.subitem.data);
				}
			}
		}

		Debug.Log("-------------------");
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

