

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
		private System.Func<ID,ID,bool> compare;

		/** DefaultCompare
		*/
		private static bool DefaultCompare(ID a_id_a,ID a_id_b)
		{
			System.IComparable t_id_a = a_id_a as System.IComparable;
			System.IComparable t_id_b = a_id_b as System.IComparable;

			if(t_id_a.CompareTo(t_id_b) == 0){
				return true;
			}

			return false;
		}

		/** constructor
		*/
		public FocusGroup(ID a_none,System.Func<ID,ID,bool> a_compare = null)
		{
			//current
			this.current = a_none;
		
			//none
			this.none = a_none;

			//list
			this.list = new System.Collections.Generic.Dictionary<ID,FocusGroup_Item>();

			//compare
			this.compare = a_compare;
			if(this.compare == null){
				System.Type[] t_type_list = typeof(ID).FindInterfaces(new System.Reflection.TypeFilter(
					(a_a_type_a,a_a_type_b) => {
						return (a_a_type_a.ToString() == a_a_type_b.ToString());
					}
				),typeof(System.IComparable));
				if(t_type_list != null){
					this.compare = DefaultCompare;
				}else{
					Tool.Assert(false);
				}
			}
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
		public void SetFocusAllOff_CallOnFocusCheck()
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
					if(this.compare(a_id,this.current) == false){
						t_change = true;
					}

					//他のものをＯＦＦにする。
					foreach(System.Collections.Generic.KeyValuePair<ID,FocusGroup_Item> t_pair in this.list){
						if(this.compare(a_id,t_pair.Key) == false){
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
					if(this.compare(a_id,this.current) == true){
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

