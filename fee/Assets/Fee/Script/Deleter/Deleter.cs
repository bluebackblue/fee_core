using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 削除管理。
*/


/** NDeleter
*/
namespace NDeleter
{
	/** Deleter
	*/
	public class Deleter : Config
	{
		/** リスト。
		*/
		public List<DeleteItem_Base> list;

		/** constructor
		*/
		public Deleter()
		{
			this.list = new List<DeleteItem_Base>();
		}

		/** 登録。
		*/
		public void Register(DeleteItem_Base a_item)
		{
			this.list.Add(a_item);
		}

		/** 解除。
		*/
		public void UnRegister(DeleteItem_Base a_item)
		{
			this.list.Remove(a_item);
		}

		/** すべて削除。
		*/
		public void DeleteAll()
		{
			for(int ii=0;ii<this.list.Count;ii++){
				this.list[ii].Delete();
			}
			this.list.Clear();
		}
	}
}

