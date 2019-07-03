

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドルリスト。プログレス。
*/


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** Progress
	*/
	public class Progress
	{
		/** per_list
		*/
		float[] per_list;

		/** main
		*/
		int main_max;
		int main_index;

		/** sub
		*/
		int sub_max;
		int sub_index;

		/** constructor
		*/
		public Progress(float[] a_list)
		{
			this.per_list = a_list;

			this.main_max = 0;
			this.main_max = 0;

			this.sub_max = 0;
			this.sub_index = 0;
		}

		/** SetStep
		*/
		public void SetStep(int a_main_index,int a_main_max,int a_sub_index,int a_sub_max)
		{
			Tool.Assert((0 <= a_main_index)&&(a_main_index < a_main_max));
			Tool.Assert((0 <= a_sub_index)&&(a_sub_index < a_sub_max));

			this.main_index = a_main_index;
			this.main_max = a_main_max;
			this.sub_index = a_sub_index;
			this.sub_max = a_sub_max;
		}

		/** CalcProgress
		*/
		public float CalcProgress(float a_progress)
		{
			float t_progress = 0;

			//main
			for(int ii=0;ii<this.main_index;ii++){
				t_progress += this.per_list[ii];
			}

			//sub
			t_progress += this.per_list[this.main_index] * this.sub_index / this.sub_max;

			//subsub
			float t_sub_sub_use_progress = this.per_list[this.main_index] / this.sub_max;
			t_progress += t_sub_sub_use_progress * a_progress;

			//progress
			if(t_progress > 1.0f){
				t_progress = 1.0f;
			}

			return t_progress;
		}
	}
}

