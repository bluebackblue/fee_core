

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
	/** Status_Pad
	*/
	public struct Status_Pad
	{
		/** 有効。
		*/
		public bool enable;

		/** パッドインデックス。
		*/
		public int pad_index;

		/** パッドタイプ。
		*/
		public Pad_InputManagerItemName.PadType pad_type;

		/** デジタルボタン。
		*/
		public Status_Digital_Button left;
		public Status_Digital_Button right;
		public Status_Digital_Button up;
		public Status_Digital_Button down;
		public Status_Digital_Button enter;
		public Status_Digital_Button escape;
		public Status_Digital_Button sub1;
		public Status_Digital_Button sub2;
		public Status_Digital_Button left_menu;
		public Status_Digital_Button right_menu;

		/** アナログスティック。
		*/
		public Status_Analog_Stick l_stick;
		public Status_Analog_Stick r_stick;	
		public Status_Digital_Button l_stick_button;
		public Status_Digital_Button r_stick_button;

		/** トリガーボタン。
		*/
		public Status_Digital_Button l_trigger_1;
		public Status_Digital_Button r_trigger_1;
		public Status_Analog_Button l_trigger_2;
		public Status_Analog_Button r_trigger_2;

		/** モーター。
		*/
		public Status_Motor_Speed motor_low;
		public Status_Motor_Speed motor_high;

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
			this.motor_low.Reset();
			this.motor_high.Reset();
		}
	}
}

