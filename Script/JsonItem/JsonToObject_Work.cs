

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
	/** JsonToObject_Work
	*/
	public class JsonToObject_Work
	{
		/** コンバートのみ。
		*/
		public enum ModeConvertOnly
		{
			Value = 0,
		}

		/** インスタンス作成、コンバート。値設定。
		*/
		public enum ModeCreateInstance
		{
			Value = 1,
		}

		/** 設定。
		*/
		public enum ModeFix
		{
			Value = 2,
		}

		int mode;

		/** 設定先。
		*/
		System.Reflection.FieldInfo set_fieldinfo;
		System.Object set_object;
		System.Type set_type;

		/** コンバート先。
		*/
		System.Object to_object;

		/** コンバート元。
		*/
		JsonItem from_jsonitem;

		/** constructor
		
			コンバートのみ。

		*/
		public JsonToObject_Work(ModeConvertOnly a_mode,System.Object a_to_object,System.Type a_set_type,JsonItem a_from_jsonitem)
		{
			//モード。
			this.mode = (int)a_mode;

			//設定先。
			this.set_fieldinfo = null;
			this.set_object = null;
			this.set_type = a_set_type;

			//コンバート先。
			this.to_object = a_to_object;

			//コンバート元。
			this.from_jsonitem = a_from_jsonitem;
		}

		/** constructor
		
			インスタンス作成、コンバート。値設定。

		*/
		public JsonToObject_Work(ModeCreateInstance a_mode,System.Reflection.FieldInfo a_set_fieldinfo,System.Object a_set_object,JsonItem a_from_jsonitem)
		{
			//モード。
			this.mode = (int)a_mode;

			//設定先。
			this.set_fieldinfo = a_set_fieldinfo;
			this.set_object = a_set_object;
			this.set_type = a_set_fieldinfo.FieldType;

			//コンバート先。
			this.to_object = null;

			//コンバート元。
			this.from_jsonitem = a_from_jsonitem;
		}

		/** constructor
		
			設定。

		*/
		public JsonToObject_Work(ModeFix a_mode,System.Reflection.FieldInfo a_set_fieldinfo,System.Object a_set_object,System.Object a_to_object)
		{
			//モード。
			this.mode = (int)a_mode;

			//設定先。
			this.set_fieldinfo = a_set_fieldinfo;
			this.set_object = a_set_object;
			this.set_type = null;

			//コンバート先。
			this.to_object = a_to_object;

			//コンバート元。
			this.from_jsonitem = null;
		}

		/** 実行。
		*/
		public void Do(int a_nest,System.Collections.Generic.LinkedList<JsonToObject_Work> a_work_pool)
		{
			switch(this.mode){
			case (int)ModeConvertOnly.Value:
				{
					//メンバーの設定。
					JsonToObject_SystemObject.Convert(ref this.to_object,this.set_type,this.from_jsonitem,a_nest,a_work_pool);
				}break;
			case (int)ModeCreateInstance.Value:
				{
					//インスタンスの作成。
					this.to_object = JsonToObject_SystemObject.CreateInstance(this.set_type,this.from_jsonitem);

					if(this.set_type.IsClass == true){

						//メンバーの設定。
						JsonToObject_SystemObject.Convert(ref this.to_object,this.set_type,this.from_jsonitem,a_nest,a_work_pool);

						//フィールドに設定。
						try{
							this.set_fieldinfo.SetValue(this.set_object,this.to_object);
						}catch(System.Exception t_exception){
							Tool.DebugReThrow(t_exception);
						}
					}else{

						System.Collections.Generic.LinkedListNode<JsonToObject_Work> t_first_node = a_work_pool.First;

						//メンバーの設定。
						JsonToObject_SystemObject.Convert(ref this.to_object,this.set_type,this.from_jsonitem,a_nest,a_work_pool);

						//再登録。
						this.mode = (int)ModeFix.Value;

						if(t_first_node != null){
							a_work_pool.AddBefore(t_first_node,this);
						}else{
							a_work_pool.AddLast(this);
						}
					}
				}break;
			case (int)ModeFix.Value:
				{
					//フィールドに設定。
					try{
						this.set_fieldinfo.SetValue(this.set_object,this.to_object);
					}catch(System.Exception t_exception){
						Tool.DebugReThrow(t_exception);
					}
				}break;
			}
		}
	}
}

