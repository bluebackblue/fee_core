

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。スクロール。ドラッグ。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Scroll_Drag
	*/
	public struct Scroll_Drag
	{
		/** callback
		*/
		private Scroll_Drag_CallBack callback;

		/** ドラッグ中。
		*/
		private bool flag;

		/** ドラッグ開始時の表示位置。
		*/
		private float start_viewposition;

		/** ドラッグ開始時のポイント位置。
		*/
		private int start_pos;

		/** 速度計算用の旧ポイント位置。
		*/
		private int old_pos;

		/** ドラッグ速度。
		*/
		private float speed;

		/** 初期化。
		*/
		public void Initialize()
		{
			this.callback = null;
			this.flag = false;
			this.start_viewposition = 0;
			this.start_pos = 0;
			this.old_pos = 0;
			this.speed = 0.0f;
		}

		/** SetCallBack
		*/
		public void SetCallBack(Scroll_Drag_CallBack a_callback)
		{
			this.callback = a_callback;
		}

		/** ドラッグスクロール速度。設定。
		*/
		public void SetSpeed(float a_speed)
		{
			this.speed = a_speed;
		}

		/** ドラッグスクロール速度。取得。
		*/
		public float GetSpeed()
		{
			return this.speed;
		}

		/** 更新。
		*/
		public void Main(bool a_is_onover,float a_eceleration,float a_delta)
		{
			if((this.flag == false)&&(Fee.Input.Input.GetInstance().mouse.left.down == true)&&(a_is_onover == true)){
				Fee.Geometry.Pos2D<int> t_position = Fee.Input.Input.GetInstance().mouse.cursor.pos;
				if(this.callback.IsRectIn(in t_position) == true){
					//ドラッグ開始。
					this.flag = true;
					this.start_viewposition = this.callback.GetViewPosition();
					this.start_pos = this.callback.GetScrollDirectionValue(in t_position);
					this.old_pos = this.start_pos;

					this.speed = 0.0f;
				}
			}else if((this.flag == true)&&(Fee.Input.Input.GetInstance().mouse.left.on == true)){
				//ドラッグ中。

				Fee.Geometry.Pos2D<int> t_position = Fee.Input.Input.GetInstance().mouse.cursor.pos;
				this.callback.SetViewPosition(this.start_viewposition + this.start_pos - this.callback.GetScrollDirectionValue(in t_position));

				//慣性。
				int t_drag_new_pos =this.callback.GetScrollDirectionValue(in t_position);
				float t_delta = (this.old_pos - t_drag_new_pos);
				if(a_delta > 0){
					t_delta = t_delta / a_delta;
				}else{
					t_delta = 0.0f;
				}

				this.speed = this.speed * 0.5f + t_delta * 0.5f;
				this.old_pos = t_drag_new_pos;
			}else if((this.flag == true)&&(Fee.Input.Input.GetInstance().mouse.left.on == false)){
				//ドラッグ終了。
				this.flag = false;
			}

			if((this.flag == false)&&(this.speed != 0.0f)){
				this.speed *= a_eceleration;
				if((this.speed * this.speed) >= 0.01f){
					this.callback.SetViewPosition(this.callback.GetViewPosition() + this.speed * a_delta);
				}else{
					this.speed = 0.0f;
				}
			}
		}
	}
}

