

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 関数呼び出し。
*/


/** Fee.Function
*/
namespace Fee.Function
{
	/** RowUpdate
	*/
	public class RowUpdate
	{
		/** callback
		*/
		private RowUpdateType callback;

		/** delta
		*/
		public float delta;

		/** constructor
		*/
		public RowUpdate()
		{
			//callback
			this.callback = null;

			//delta
			this.delta = 0.0f;
		}

		/** 削除。
		*/
		public void Delete()
		{
			#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
			{
				//解除漏れ。
				Tool.Assert(this.callback == null);
			}
			#endif

			this.callback = null;
		}

		/** SetCallBack
		*/
		public void SetCallBack(RowUpdateType a_callback)
		{
			this.callback += a_callback;
		}

		/** UnSetCallBack
		*/
		public void UnSetCallBack(RowUpdateType a_callback)
		{
			this.callback -= a_callback;
		}

		/** Main
		*/
		public void Main()
		{
			bool t_flag = false;
			float t_delta_add = UnityEngine.Time.deltaTime;
			this.delta += t_delta_add;

			if(this.delta >= Config.ROWUPDATE_DELTA){
				if(this.delta <= Config.ROWUPDATE_DELTA * 2){
					this.delta -= Config.ROWUPDATE_DELTA;
				}else{
					Tool.Log(this.GetType().ToString(),"busy = " + this.delta.ToString());
					this.delta = 0.0f;
				}

				t_flag = true;
			}

			if(t_flag == true){
				if(this.callback != null){
					this.callback();
				}
			}
		}
	}
}

