using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ダウンロード。ワーク。
*/


/** NDownLoad
*/
namespace NDownLoad
{
	/** Work
	*/
	public class Work
	{
		/** Mode
		*/
		private enum Mode
		{
			/** 開始。
			*/
			Start,

			/**	実行中。
			*/
			Do,
	
			/** 完了。
			*/
			End
		};

		/** mode
		*/
		private Mode mode;

		/** url
		*/
		private string url;

		/** cache
		*/
		private bool cache;

		/** cache_version
		*/
		private int cache_version;

		/** assetbundle_id
		*/
		private long assetbundle_id;

		/** item
		*/
		private Item item;

		/** constructor
		*/
		public Work(string a_url,bool a_cache,int a_cache_version,long a_assetbundle_id)
		{
			//mode
			this.mode = Mode.Start;

			//url
			this.url = a_url;

			//cache
			this.cache = a_cache;

			//cache_version
			this.cache_version = a_cache_version;

			//TODO:assetbundle_id
			this.assetbundle_id = a_assetbundle_id;

			//item
			this.item = new Item();
		}

		/** アイテム。
		*/
		public Item GetItem()
		{
			return this.item;
		}

		/** 更新。

		戻り値 = true : 完了。

		*/
		public bool Main()
		{
			MonoBehaviour_Www t_www = NDownLoad.DownLoad.GetInstance().GetWww();

			switch(this.mode){
			case Mode.Start:
				{
					if(t_www.Request(this.url,this.cache,this.cache_version) == true){
						//開始。
						this.mode = Mode.Do;
					}
				}break;
			case Mode.Do:
				{
					if(t_www.IsBusy() == false){

						//結果。
						switch(t_www.GetDataType()){
						case DataType.Text:
							{
								this.item.SetResultText(t_www.GetResultText());
							}break;
						case DataType.Texture:
							{		
								this.item.SetResultTexture(t_www.GetResultTexture());
							}break;
						case DataType.AssetBundle:
							{
								this.item.SetResultAssetBundle(t_www.GetResultAssetBundle());
							}break;
						default:
							{
								this.item.SetResultError();
							}break;
						}

						this.mode = Mode.End;
					}
				}break;
			case Mode.End:
				{
				}return true;
			}

			return false;
		}
	}
}

