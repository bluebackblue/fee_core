

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
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
		public System.Collections.Generic.List<OnDelete_CallBackInterface> list;

		/** constructor
		*/
		public Deleter()
		{
			this.list = new System.Collections.Generic.List<OnDelete_CallBackInterface>();
		}

		/** 登録。
		*/
		public void Register(OnDelete_CallBackInterface a_callbackinterface)
		{
			this.list.Add(a_callbackinterface);
		}

		/** 解除。
		*/
		public void UnRegister(OnDelete_CallBackInterface a_callbackinterface)
		{
			this.list.Remove(a_callbackinterface);
		}

		/** すべて削除。
		*/
		public void DeleteAll()
		{
			for(int ii=0;ii<this.list.Count;ii++){
				this.list[ii].OnDelete();
			}
			this.list.Clear();
		}
	}
}

