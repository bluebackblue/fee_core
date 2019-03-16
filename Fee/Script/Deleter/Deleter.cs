

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 削除管理。
*/


/** Fee.Deleter
*/
namespace Fee.Deleter
{
	/** Deleter
	*/
	public class Deleter : Config
	{
		/** リスト。
		*/
		public System.Collections.Generic.List<DeleteItem_Base> list;

		/** constructor
		*/
		public Deleter()
		{
			this.list = new System.Collections.Generic.List<DeleteItem_Base>();
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

