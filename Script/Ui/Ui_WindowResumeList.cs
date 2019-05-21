

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。ウィンドウレジュームリスト。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Ui_WindowResumeList
	*/
	public class Ui_WindowResumeList
	{
		//リスト。
		private System.Collections.Generic.Dictionary<string,WindowResumeItem> list;

		/** constructor
		*/
		public Ui_WindowResumeList()
		{
			//リスト。
			this.list = new System.Collections.Generic.Dictionary<string,WindowResumeItem>();
		}

		/** 登録。

		戻り値 == true : 新規。

		*/
		public bool Register(string a_label)
		{
			if(this.list.ContainsKey(a_label) == false){
				this.list.Add(a_label,new WindowResumeItem(a_label));
				return true;
			}
			return false;
		}

		/** 解除。
		*/
		public void UnRegist(string a_label)
		{
			this.list.Remove(a_label);
		}

		/** GetItem
		*/
		public WindowResumeItem GetItem(string a_label)
		{
			WindowResumeItem t_item = null;

			if(this.list.TryGetValue(a_label,out t_item) == true){
				return t_item;
			}

			return null;
		}
	}
}

