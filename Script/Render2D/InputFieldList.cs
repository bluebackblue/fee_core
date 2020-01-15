

/**
* Copyright (c) blueback
* Released under the MIT License
* https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
* @brief ２Ｄ描画。入力フィールドリスト。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** InputFieldList
	*/
	public class InputFieldList
	{
		/** list
		*/
		private System.Collections.Generic.List<InputField2D> list;

		/** pool_list
		*/
		private Fee.Pool.PoolList<InputField2D> pool_list;

		/** sortend_flag
		*/
		public bool sortend_flag;

		/** calc_index_request_flag
		*/
		public bool calc_index_request_flag;

		/** sort_request_flag
		*/
		public bool sort_request_flag;

		/** delete_request_flag
		*/
		public bool delete_request_flag;

		/** constructor
		*/
		public InputFieldList()
		{
			//list
			this.list = new System.Collections.Generic.List<InputField2D>();

			//pool_list
			this.pool_list = new Fee.Pool.PoolList<InputField2D>(0);

			//sortend_flag
			this.sortend_flag = true;

			//calc_index_request_flag
			this.calc_index_request_flag = true;

			//sort_request_flag
			this.sort_request_flag = true;

			//delete_request_flag
			this.delete_request_flag = true;
		}

		/** プールリストキャパシティ。設定。
		*/
		public void SetPoolListCapacity(int a_capacity)
		{
			this.pool_list.SetCapacity(a_capacity);
		}

		/** プールから領域確保。
		*/
		public InputField2D PoolNew()
		{
			return this.pool_list.PoolNew();
		}

		/** 登録。
		*/
		public void Regist(InputField2D a_item)
		{
			//list
			this.list.Add(a_item);

			//sort_request_flag
			this.sort_request_flag = true;
		}

		/** ソート。
		*/
		public void Sort()
		{
			this.list.Sort((InputField2D a_test,InputField2D a_target) => {
				return (int)(a_test.GetDrawPriority() - a_target.GetDrawPriority());
			});
		}

		/** アイテム。取得。
		*/
		public InputField2D GetItem(int a_index)
		{
			return this.list[a_index];
		}

		/** リスト数。取得。
		*/
		public int GetListMax()
		{
			return this.list.Count;
		}

		/** スクリーンサイズ変更。
		*/
		public void ChangeScreenSize()
		{
			for(int ii=0;ii<this.list.Count;ii++){
				this.list[ii].Raw_SetCalcFontSizeFlag(true);
			}
		}

		/** 親レイヤー変更。

			描画プライオリティに対応したカメラに関連付ける。

		*/
		public void ChangeParentLayer(LayerList a_layerlist)
		{
			for(int ii=0;ii<this.list.Count;ii++){
				if(this.list[ii].IsDeleteRequest() == false){
					this.list[ii].Raw_SetLayer(a_layerlist.GetLayerTransformFromDrawPriority(this.list[ii].GetDrawPriority()));
				}
			}
		}

		/** タスクから呼び出される。
		*/
		public void Task_Update()
		{
			//削除リクエストのあるアイテムをプールへ削除。
			this.list.RemoveAll((Fee.Render2D.InputField2D a_item) => {
				if(a_item.IsDeleteRequest() == true){
					this.pool_list.PoolToDelete(a_item);
					return true;
				}
				return false;
			});
		}

		/** 削除。
		*/
		public void DeleteAll()
		{
			this.pool_list.DeleteAllFromMemory();
			this.list.Clear();
		}
	}
}

