

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。タッチ。フェイズ。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** Touch_Phase
	*/
	public class Touch_Phase
	{
		/** PhaseType
		*/
		public enum PhaseType
		{
			/** None
			*/
			None,

			/** Began
			*/
			Began,

			/** Moved
			*/
			Moved,

			/** Stationary
			*/
			Stationary,

			/** FadeOut
			*/
			FadeOut,
		};

		/** value_x
		*/
		public int value_x;

		/** value_y
		*/
		public int value_y;

		/** 更新。
		*/
		public bool update;

		/** fadeoutframe
		*/
		public int fadeoutframe;

		/** phasetype
		*/
		public PhaseType phasetype;

		/** raw_id
		*/
		public int raw_id;

		/** リセット。
		*/
		public void Reset()
		{
			//update
			this.update = false;

			//value_x
			this.value_x = 0;
	
			//value_y
			this.value_y = 0;

			//fadeoutframe
			this.fadeoutframe = 0;

			//phasetype
			this.phasetype = PhaseType.None;

			//raw_id
			this.raw_id = Touch.INVALID_TOUCH_RAW_ID;
		}

		/** 設定。
		*/
		public void Set(int a_value_x,int a_value_y,PhaseType a_phasetype)
		{
			//value_x
			this.value_x = a_value_x;

			//value_y
			this.value_y = a_value_y;

			//phasetype
			this.phasetype = a_phasetype;
		}

		/** RawID。
		*/
		public void SetRawID(int a_raw_id)
		{
			this.raw_id = a_raw_id;
		}

		/** 更新。
		*/
		public void Main()
		{
		}
    }
}

