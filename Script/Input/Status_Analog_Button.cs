

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。ステータス。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** Status_Analog_Button
	*/
	public struct Status_Analog_Button
	{
		/** value
		*/
		public float value;
		public float value_old;

		/** flag
		*/
		public bool on_old;
		public bool on;
		public bool down;
		public bool up;

		/** リセット。
		*/
		public void Reset()
		{
			this.on_old = false;
			this.on = false;
			this.down = false;
			this.up = false;
		}

		/** 設定。
		*/
		public void Set(float a_value)
		{
			this.value_old = this.value;
			this.value = a_value;

			bool t_flag = this.on_old;
			if(this.on_old == true){
				if(this.value < Config.ANALOG_BUTTON_VALUE_OFF){
					//オンからオフに。
					t_flag = false;
				}
			}else{
				if(this.value > Config.ANALOG_BUTTON_VALUE_ON){
					//オフからオンに。
					t_flag = true;
				}
			}

			this.on_old = this.on;
			this.on = t_flag;
		}

		/** 更新。
		*/
		public void Main()
		{
			if((this.on == true)&&(this.on_old == false)){
				//ダウン。
				this.down = true;
			}else if((this.on == false)&&(this.on_old == true)){
				//アップ。
				this.up = true;
			}else{
				this.down = false;
				this.up = false;
			}
		}
	}
}

