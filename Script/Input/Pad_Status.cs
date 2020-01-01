

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
	/** Pad_Status
	*/
	public struct Pad_Status
	{
		/** 有効。
		*/
		public bool enable;

		/** devicename
		*/
		public string devicename;

		/** パッドインデックス。
		*/
		public int pad_index;

		/** パッドタイプ。
		*/
		public Pad_InputManagerItemName.PadType pad_type;

		/** デジタルボタン。
		*/
		public Digital_Button left;
		public Digital_Button right;
		public Digital_Button up;
		public Digital_Button down;
		public Digital_Button enter;
		public Digital_Button escape;
		public Digital_Button sub1;
		public Digital_Button sub2;
		public Digital_Button left_menu;
		public Digital_Button right_menu;

		/** アナログスティック。
		*/
		public Analog_Stick l_stick;
		public Analog_Stick r_stick;	
		public Digital_Button l_stick_button;
		public Digital_Button r_stick_button;

		/** トリガーボタン。
		*/
		public Digital_Button l_trigger_1;
		public Digital_Button r_trigger_1;
		public Analog_Button l_trigger_2;
		public Analog_Button r_trigger_2;

		/** モーター。
		*/
		public Moter_Speed moter_low;
		public Moter_Speed moter_high;

		/** リセット。
		*/
		public void Reset()
		{
			//デジタルボタン。
			this.left.Reset();
			this.right.Reset();
			this.up.Reset();
			this.down.Reset();
			this.enter.Reset();
			this.escape.Reset();
			this.sub1.Reset();
			this.sub2.Reset();
			this.left_menu.Reset();
			this.right_menu.Reset();

			//アナログスティック。
			this.l_stick.Reset();
			this.r_stick.Reset();
			this.l_stick_button.Reset();
			this.r_stick_button.Reset();

			//トリガーボタン。
			this.l_trigger_1.Reset();
			this.r_trigger_1.Reset();
			this.l_trigger_2.Reset();
			this.r_trigger_2.Reset();

			//モーター。
			this.moter_low.Reset();
			this.moter_high.Reset();
		}
	}
}

