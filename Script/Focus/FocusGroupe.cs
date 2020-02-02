

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief フォーカス。グループ。
*/


/** Fee.Focus
*/
namespace Fee.Focus
{
	/** FocusGroup
	*/
	public class FocusGroup<ID> : Fee.Focus.OnFocusCheck_CallBackInterface<ID>
		where ID : System.IComparable
	{
		/** current
		*/
		public ID current;

		/** none
		*/
		public ID none;

		/** list
		*/
		private System.Collections.Generic.Dictionary<ID,FocusGroup_Item> list;

		/** constructor
		*/
		public FocusGroup(ID a_none)
		{
			//current
			this.current = a_none;
		
			//none
			this.none = a_none;

			//list
			this.list = new System.Collections.Generic.Dictionary<ID,FocusGroup_Item>();
		}

		/** 追加。
		*/
		public void Add<T>(ID a_id,T a_item,System.Action<T> a_callback_on,System.Action<T> a_callback_off)
			where T : FocusItem_Base
		{
			this.list.Add(a_id,FocusGroup_Item.Create(a_item,a_callback_on,a_callback_off));
		}

		/** 削除。
		*/
		public void Remove(ID a_id)
		{
			this.list.Remove(a_id);
		}

		/** フォーカス設定。すべてのアイテムをＯＦＦにする。OnFocusCheckを呼び出す。
		*/
		public void SetFocusOffCallOnFocusCheck()
		{
			foreach(System.Collections.Generic.KeyValuePair<ID,FocusGroup_Item> t_pair in this.list){
				t_pair.Value.item.SetFocusCallOnFocusCheck(false);
			}
		}

		/** [Fee.Focus.OnFocusCheck_CallBackInterface]フォーカスチェック。
		*/
		public void OnFocusCheck(ID a_id)
		{
			FocusGroup_Item t_item;
			if(this.list.TryGetValue(a_id,out t_item) == true){
				if(t_item.item.IsFocus() == true){
					//フォーカスＯＮ。

					//他のものをＯＦＦにする。
					foreach(System.Collections.Generic.KeyValuePair<ID,FocusGroup_Item> t_pair in this.list){
						if(a_id.CompareTo(t_pair.Key) != 0){
							t_pair.Value.item.SetFocus(false);
						}
					}

					//カレント設定。
					this.current = a_id;

					//コールバック呼び出し。
					t_item.OnFocusOn();
				}else{
					//フォーカスＯＦＦ。

					//自分がカレントだった場合はカレントをＮＯＮＥにする。
					if(a_id.CompareTo(this.current) == 0){
						this.current = this.none;
					}

					//コールバック呼び出し。
					t_item.OnFocusOff();
				}
			}
		}
	}
}

