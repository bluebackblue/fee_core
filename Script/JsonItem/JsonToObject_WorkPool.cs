

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。ＪＳＯＮ化。
*/


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** JsonToObject_WorkPool
	*/
	public class JsonToObject_WorkPool
	{
		/** list
		*/
		public System.Collections.Generic.LinkedList<JsonToObject_WorkPool_Item> list;

		/** constructor
		*/
		public JsonToObject_WorkPool()
		{
			this.list = new System.Collections.Generic.LinkedList<JsonToObject_WorkPool_Item>();
		}

		/** 最後に追加。
		*/
		public void AddLast(JsonToObject_WorkPool_Item a_item)
		{
			this.list.AddLast(a_item);
		}

		/** 最後に追加。
		*/
		public void AddFirst(JsonToObject_WorkPool_Item a_item)
		{
			this.list.AddFirst(a_item);
		}

		/** 最後に追加。
		*/
		public void AddBefore(System.Collections.Generic.LinkedListNode<JsonToObject_WorkPool_Item> a_node,JsonToObject_WorkPool_Item a_item)
		{
			this.list.AddBefore(a_node,a_item);
		}

		/** GetFirst
		*/
		public System.Collections.Generic.LinkedListNode<JsonToObject_WorkPool_Item> GetFirst()
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
					JsonToObject_WorkPool_Item t_current_work = this.list.First.Value;
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

