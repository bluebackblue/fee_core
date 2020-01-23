

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。オブジェクト化。
*/


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** JsonItemToObject_WorkPool
	*/
	public class JsonItemToObject_WorkPool
	{
		/** list
		*/
		public System.Collections.Generic.LinkedList<JsonItemToObject_WorkPool_Item> list;

		/** temp_parameter_list_1
		*/
		public System.Object[] temp_parameter_list_1;

		/** constructor
		*/
		public JsonItemToObject_WorkPool()
		{
			//list
			this.list = new System.Collections.Generic.LinkedList<JsonItemToObject_WorkPool_Item>();

			//temp_parameter_list_1
			this.temp_parameter_list_1 = new System.Object[1]{null};
		}

		/** 最後に追加。
		*/
		public void AddLast(JsonItemToObject_WorkPool_Item a_item)
		{
			this.list.AddLast(a_item);
		}

		/** 最後に追加。
		*/
		public void AddFirst(JsonItemToObject_WorkPool_Item a_item)
		{
			this.list.AddFirst(a_item);
		}

		/** 最後に追加。
		*/
		public void AddBefore(System.Collections.Generic.LinkedListNode<JsonItemToObject_WorkPool_Item> a_node,JsonItemToObject_WorkPool_Item a_item)
		{
			this.list.AddBefore(a_node,a_item);
		}

		/** GetFirst
		*/
		public System.Collections.Generic.LinkedListNode<JsonItemToObject_WorkPool_Item> GetFirst()
		{
			return this.list.First;
		}

		/** 更新。
		*/
		public void Main()
		{
			while(true){
				int t_count = this.list.Count;
				if(t_count > 0){
					JsonItemToObject_WorkPool_Item t_current_work = this.list.First.Value;
					this.list.RemoveFirst();
					t_current_work.Do(this);

					//たぶん無限ループ。
					if(t_count > Config.POOL_MAX){
						this.list.Clear();
						Tool.Assert(false);
					}
				}else{
					break;
				}
			}
		}
	}
}

