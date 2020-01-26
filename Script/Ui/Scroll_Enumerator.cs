

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。スクロール。列挙子。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Scroll_Enumerator
	*/
	public struct Scroll_Enumerator<ITEM> : System.Collections.IEnumerator
		where ITEM : ScrollItem_Base
	{
		/** parent
		*/
		public Scroll_Base<ITEM> list;

		/** index
		*/
		public int index;

		/** カレント。
		*/
		public object Current
		{
			get
			{
				return this.list.GetItem(this.index);
			}
		}

		/** constructor
		*/
		public Scroll_Enumerator(Scroll_Base<ITEM> a_list)
		{
			//list
			this.list = a_list;

			//index
			this.index = -1;
		}

		/** MoveNext
		*/
		public bool MoveNext()
		{
			this.index++;
			if(this.index < this.list.GetListCount()){
				return true;
			}
			return false;
		}

		/** Reset
		*/
		public void Reset()
		{
			this.index = -1;
		}
	}
}

