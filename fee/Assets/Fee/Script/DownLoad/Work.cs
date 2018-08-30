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

		/** item
		*/
		private Item item;

		/** constructor
		*/
		public Work(string a_url)
		{
			//mode
			this.mode = Mode.Start;

			//url
			this.url = a_url;

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
					if(t_www.Request(this.url) == true){
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

