

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。パッド。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** Pad_InputManagerItemName
	*/
	public class Pad_InputManagerItemName
	{
		/** パッドタイプ。
		*/
		public enum PadType
		{
			/** Type_P
			*/
			Type_P = 0,

			/** Type_X
			*/
			Type_X,

			/** Type_A
			*/
			Type_A,

			/** Max
			*/
			Max,
		}

		/** TypeItem
		*/
		public class TypeItem
		{
			/** name
			*/
			public string name;

			/** constructor
			*/
			public TypeItem(string a_name)
			{
				this.name = a_name;
			}
		}

		/** PadItem
		*/
		public class PadItem
		{
			/** type_list
			*/
			public TypeItem[] type_list;

			/** constructor
			*/
			public PadItem(TypeItem[] a_tyoe_list)
			{
				//type_list
				this.type_list = a_tyoe_list;
			}
		}

		/** pad_list
		*/
		private PadItem[] pad_list;

		/** constructor
		*/
		public Pad_InputManagerItemName(string a_pad_0_type_p,string a_pad_0_type_x,string a_pad_0_type_a,string a_pad_1_type_p,string a_pad_1_type_x,string a_pad_1_type_a)
		{
			this.pad_list = new PadItem[2]{
				new PadItem(
					new TypeItem[3]{
						new TypeItem(a_pad_0_type_p),
						new TypeItem(a_pad_0_type_x),
						new TypeItem(a_pad_0_type_a)
					}
				),
				new PadItem(
					new TypeItem[3]{
						new TypeItem(a_pad_1_type_p),
						new TypeItem(a_pad_1_type_x),
						new TypeItem(a_pad_1_type_a)
					}						
				)
			};
		}

		/** GetItem
		*/
		public string GetItem(int a_pad_index,Pad_InputManagerItemName.PadType a_pad_type)
		{
			return this.pad_list[a_pad_index].type_list[(int)a_pad_type].name;
		}
	}
}

