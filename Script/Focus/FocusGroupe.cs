

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

		/** ＩＤ。追加。

			a_user_instance : コールバック時に使用するインスタンス。
			a_callback_on	: フォーカスチェック時のＯＮの場合に呼び出される。
			a_callback_off	: フォーカスチェック時のＯＦＦの場合に呼び出される。

		*/
		public void AddID<T>(ID a_id,T a_user_instance,System.Action<T,bool> a_callback_on,System.Action<T,bool> a_callback_off)
			where T : FocusItem_Base
		{
			this.list.Add(a_id,FocusGroup_Item.Create(a_user_instance,a_callback_on,a_callback_off));
		}

		/** ＩＤ。削除。
		*/
		public void RemoveID(ID a_id)
		{
			this.list.Remove(a_id);
		}

		/** フォーカス設定。すべてのアイテムをＯＦＦにする。

			OnFocusCheckを呼び出す。

		*/
		public void SetFocusAllOff_CallOnFocusCheck()
		{
			foreach(System.Collections.Generic.KeyValuePair<ID,FocusGroup_Item> t_pair in this.list){
				t_pair.Value.item.SetFocus(false);
			}
		}

		/** [Fee.Focus.OnFocusCheck_CallBackInterface]フォーカスチェック。

			「Fee.Ui.Button_Base.SetOnFocusCheck」等に登録した場合に呼び出される。

		*/
		public void OnFocusCheck(ID a_id)
		{
			FocusGroup_Item t_item;
			if(this.list.TryGetValue(a_id,out t_item) == true){
				if(t_item.item.IsFocus() == true){
					//フォーカスＯＮ。

					bool t_change = false;
					if(a_id.CompareTo(this.current) != 0){
						t_change = true;
					}

					//他のものをＯＦＦにする。
					foreach(System.Collections.Generic.KeyValuePair<ID,FocusGroup_Item> t_pair in this.list){
						if(a_id.CompareTo(t_pair.Key) != 0){
							t_pair.Value.item.SetFocus_NoCall(false);

							//コールバック呼び出し。
							t_pair.Value.OnFocusOff(t_change);
						}
					}

					//カレント設定。
					this.current = a_id;

					//コールバック呼び出し。
					t_item.OnFocusOn(t_change);
				}else{
					//フォーカスＯＦＦ。

					//自分がカレントだった場合はカレントをＮＯＮＥにする。
					bool t_change = false;
					if(a_id.CompareTo(this.current) == 0){
						this.current = this.none;
						t_change = true;
					}

					//コールバック呼び出し。
					t_item.OnFocusOff(t_change);
				}
			}
		}
	}
}

