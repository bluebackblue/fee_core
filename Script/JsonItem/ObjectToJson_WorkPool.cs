

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
	/** ObjectToJson_WorkPool
	*/
	public class ObjectToJson_WorkPool
	{
		/** list
		*/
		public System.Collections.Generic.List<ObjectToJson_WorkPool_Item> list;

		/** constructor
		*/
		public ObjectToJson_WorkPool()
		{
			this.list = new System.Collections.Generic.List<ObjectToJson_WorkPool_Item>();
		}

		/** 追加。
		*/
		public void Add(ObjectToJson_WorkPool_Item a_item)
		{
			this.list.Add(a_item);
		}

		/** 更新。
		*/
		public void Main()
		{
			while(true){
				int t_count = this.list.Count;
				if(t_count > 0){
					ObjectToJson_WorkPool_Item t_current_work = this.list[t_count - 1];
					this.list.RemoveAt(t_count - 1);
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

