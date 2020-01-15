

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
		public void Regist(OnDelete_CallBackInterface a_callback_interface)
		{
			this.list.Add(a_callback_interface);
		}

		/** 解除。
		*/
		public void UnRegist(OnDelete_CallBackInterface a_callback_interface)
		{
			this.list.Remove(a_callback_interface);
		}

		/** クリア。
		*/
		public void Clear()
		{
			this.list.Clear();
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

