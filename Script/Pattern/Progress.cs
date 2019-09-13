

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief パターン。プログレス。
*/


/** Fee.Pattern
*/
namespace Fee.Pattern
{
	/** Progress
	*/
	public struct Progress
	{
		/** total
		*/
		private float total;

		/** main_per_list
		*/
		private float[] main_per_list;

		/** main
		*/
		private int main_index;

		/** sub
		*/
		private int sub_max;
		private int sub_index;

		/** constructor
		*/
		public Progress(float[] a_list)
		{
			this.main_per_list = a_list;

			{
				this.total = 0.0f;
				for(int ii=0;ii<this.main_per_list.Length;ii++){
					this.total += this.main_per_list[ii];
				}
			}

			this.main_index = 0;

			this.sub_max = 0;
			this.sub_index = 0;
		}

		/** SetStep
		*/
		public void SetStep(int a_main_index,int a_sub_index,int a_sub_max)
		{
			Tool.Assert((0 <= a_main_index)&&(a_main_index < this.main_per_list.Length));
			Tool.Assert((0 <= a_sub_index)&&(a_sub_index < a_sub_max));

			this.main_index = a_main_index;
			this.sub_max = a_sub_max;
			this.sub_index = a_sub_index;
		}

		/** CalcProgress
		*/
		public float CalcProgress(float a_sub_progress)
		{
			float t_progress = 0;

			//main
			for(int ii=0;ii<this.main_index;ii++){
				t_progress += this.main_per_list[ii];
			}

			//sub
			t_progress += this.main_per_list[this.main_index] * this.sub_index / this.sub_max;

			//subsub
			float t_sub_sub_use_progress = this.main_per_list[this.main_index] / this.sub_max;
			t_progress += t_sub_sub_use_progress * a_sub_progress;

			t_progress /= this.total;
			if(t_progress > 1.0f){
				t_progress = 1.0f;
			}

			return t_progress;
		}
	}
}

