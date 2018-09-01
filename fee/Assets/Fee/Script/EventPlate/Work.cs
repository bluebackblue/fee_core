using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief イベントプレート。ワーク。
*/


/** NEventPlate
*/
namespace NEventPlate
{
	/** Work
	*/
	public class Work
	{
		/** list
		*/
		private List<Item> list;

		/** current
		*/
		private Item current;

		/** sortrequest
		*/
		private bool sortrequest;

		/** constructor
		*/
		public Work()
		{
			//list
			this.list = new List<Item>();

			//current
			this.current = null;

			//sortrequest
			this.sortrequest = false;
		}

		/** 追加。
		*/
		public void Add(Item a_eventitem)
		{
			this.list.Add(a_eventitem);
			this.sortrequest = true;
		}

		/** 削除。
		*/
		public void Remove(Item a_eventitem)
		{
			this.list.Remove(a_eventitem);
			this.sortrequest = true;
		}

		/** ソート。リクエスト。
		*/
		public void SortRequest()
		{
			this.sortrequest = true;
		}

		/** 更新。
		*/
		public void Main(ref NRender2D.Pos2D<int> a_pos)
		{
			if(this.sortrequest == true){
				this.sortrequest = false;
				this.list.Sort(Item.Sort_InvPriority);
			}

			//更新。
			bool t_no_current = true;
			for(int ii=0;ii<this.list.Count;ii++){
				if(this.list[ii].Main(ref a_pos) == true){
					//カレントあり。
					t_no_current = false;

					if(this.current != this.list[ii]){
						//カレント変更。

						//オーバー終了。
						if(this.current != null){
							this.current.CallOnOverLeave();
						}

						this.current = this.list[ii];

						//オーバー開始。
						{
							this.current.CallOnOverEnter();
						}
					}

					break;
				}
			}

			if(t_no_current == true){
				if(this.current != null){
					//オーバー終了。
					this.current.CallOnOverLeave();
					this.current = null;
				}
			}
		}
	}
}

