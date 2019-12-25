

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief プール。プールリスト。
*/


/** Fee.Pool
*/
namespace Fee.Pool
{
	/** PoolList
	*/
	public class PoolList<T>
		where T : PoolItem_Base
	{
		/** pool_list
		*/
		private System.Collections.Generic.Stack<T> pool_list;

		/** インスタンス作成。
		*/
		private static T CreateInstance()
		{
			return System.Activator.CreateInstance<T>();
		}

		/** constructor
		*/
		public PoolList(int a_capacity)
		{
			//pool_list
			this.pool_list = new System.Collections.Generic.Stack<T>();
			for(int ii=0;ii<a_capacity;ii++){
				this.pool_list.Push(CreateInstance());
			}
		}

		/** プールから。
		*/
		public T PoolNew()
		{
			T t_pool_item;

			if(this.pool_list.Count > 0){
				t_pool_item = this.pool_list.Pop();
			}else{
				t_pool_item = CreateInstance();
			}

			return t_pool_item;
		}

		/** プールへ削除。

			タスクから呼び出される。
			Render2D.Deleteから呼び出される。

		*/
		public void PoolDelete(T a_pool_item)
		{
			a_pool_item.OnPoolDelete();
			this.pool_list.Push(a_pool_item);
		}

		/** メモリから削除。
		*/
		public void MemoryDelete()
		{
			while(this.pool_list.Count > 0){
				T t_pool_item = this.pool_list.Pop();
				t_pool_item.OnMemoryDelete();
			}
		}

		/** キャパシティー。設定。
		*/
		public void SetCapacity(int a_capacity)
		{
			while(this.pool_list.Count > a_capacity){
				T t_pool_item = this.pool_list.Pop();
				t_pool_item.OnMemoryDelete();
			}
			while(this.pool_list.Count < a_capacity){
				this.pool_list.Push(CreateInstance());
			}
		}
	}
}

