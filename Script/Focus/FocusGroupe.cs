

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

		/** compare
		*/
		private Compare_Base<ID> compare;

		/** constructor
		*/
		public FocusGroup(ID a_none,Compare_Base<ID> a_compare)
		{
			//current
			this.current = a_none;
		
			//none
			this.none = a_none;

			//list
			this.list = new System.Collections.Generic.Dictionary<ID,FocusGroup_Item>();

			//compare
			this.compare = a_compare;
		}

		/** ＩＤ。追加。

			a_user_instance : コールバック時に使用するインスタンス。

			a_callback_on	: フォーカスチェック時のＯＮの場合に呼び出される。
				(T a_user_instance,bool a_change) => {}

			a_callback_off	: フォーカスチェック時のＯＦＦの場合に呼び出される。
				(T a_user_instance,bool a_change) => {}

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
		public void SetFocusAllOff()
		{
			foreach(System.Collections.Generic.KeyValuePair<ID,FocusGroup_Item> t_pair in this.list){
				t_pair.Value.item.SetFocus(false);
			}
		}

		/** フォーカス設定。
		*/
		public void SetFocus(ID a_id)
		{
			FocusGroup_Item t_item;
			if(this.list.TryGetValue(a_id,out t_item) == true){
				t_item.item.SetFocus(true);
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
					if(this.compare.Compare(a_id,this.current) == false){
						t_change = true;
					}

					//他のものをＯＦＦにする。
					foreach(System.Collections.Generic.KeyValuePair<ID,FocusGroup_Item> t_pair in this.list){
						if(this.compare.Compare(a_id,t_pair.Key) == false){
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
					if(this.compare.Compare(a_id,this.current) == true){
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

