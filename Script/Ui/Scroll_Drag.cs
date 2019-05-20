

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。スクロール。ドラッグ。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Scroll_Drag_CallBack
	*/
	public interface Scroll_Drag_CallBack
	{
		/** [Scroll_Drag_CallBack]コールバック。表示位置。取得。
		*/
		int GetViewPosition();

		/** [Scroll_Drag_CallBack]コールバック。表示位置。設定。
		*/
		void SetViewPosition(int a_view_position);

		/** [Scroll_Drag_CallBack]コールバック。範囲チェック。
		*/
		bool IsRectIn(int a_x,int a_y);

		/** [Scroll_Drag_CallBack]コールバック。スクロール方向の値。取得。
		*/
		int GetScrollDirectionValue(int a_vertical_value,int a_horizontal_value);
	}

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
		private int start_viewposition;

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
		public void Main(bool a_is_onover)
		{
			if((this.flag == false)&&(Fee.Input.Mouse.GetInstance().left.down == true)&&(a_is_onover == true)){
				int t_x = Fee.Input.Mouse.GetInstance().pos.x;
				int t_y = Fee.Input.Mouse.GetInstance().pos.y;

				if(this.callback.IsRectIn(t_x,t_y) == true){
					//ドラッグ開始。
					this.flag = true;
					this.start_viewposition = this.callback.GetViewPosition();
					this.start_pos = this.callback.GetScrollDirectionValue(t_x,t_y);
					this.old_pos = this.start_pos;

					this.speed = 0.0f;
				}
			}else if((this.flag == true)&&(Fee.Input.Mouse.GetInstance().left.on == true)){
				//ドラッグ中。

				int t_x = Fee.Input.Mouse.GetInstance().pos.x;
				int t_y = Fee.Input.Mouse.GetInstance().pos.y;

				this.callback.SetViewPosition(this.start_viewposition + this.start_pos - this.callback.GetScrollDirectionValue(t_x,t_y));

				//慣性。
				int t_drag_new_pos =this.callback.GetScrollDirectionValue(t_x,t_y);
				this.speed = this.speed * 0.3f + (this.old_pos - t_drag_new_pos) * 0.7f;
				this.old_pos = t_drag_new_pos;
			}else if((this.flag == true)&&(Fee.Input.Mouse.GetInstance().left.on == false)){
				//ドラッグ終了。
				this.flag = false;
			}

			if((this.flag == false)&&(this.speed != 0.0f)){
				int t_move = (int)this.speed;
				this.speed /= 1.08f;
				if(t_move != 0){
					this.callback.SetViewPosition(this.callback.GetViewPosition() + t_move);
				}else{
					this.speed = 0.0f;
				}
			}
		}
	}
}

