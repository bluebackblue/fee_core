

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。マウス。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** Mouse_Button
	*/
	public struct Mouse_Button
	{
		/** ボタン。
		*/
		public bool on_old;
		public bool on;
		public bool down;
		public bool up;

		/** ダウン位置。
		*/
		public Fee.Render2D.Pos2D<int> last_down_pos;

		/** アップ位置。
		*/
		public Fee.Render2D.Pos2D<int> last_up_pos;

		/** ドラッグ時間。
		*/
		public int drag_time;

		/** ドラッグ方向。
		*/
		public UnityEngine.Vector2 drag_dir;
		public float drag_dir_magnitude;
		public UnityEngine.Vector2 drag_dir_normalized;

		/** ドラッグ総移動距離。
		*/
		public int drag_totallength;

		/** リセット。
		*/
		public void Reset()
		{
			//ボタン。
			this.on_old = false;
			this.on = false;
			this.down = false;
			this.up = false;

			//ダウン位置。
			this.last_down_pos.Set(0,0);

			//アップ位置。
			this.last_up_pos.Set(0,0);

			//ドラッグ時間。
			this.drag_time = 0;

			//ドラッグ方向。
			this.drag_dir = UnityEngine.Vector2.zero;
			this.drag_dir_magnitude = 0.0f;
			this.drag_dir_normalized = UnityEngine.Vector2.zero;

			//ドラッグ総移動距離。
			this.drag_totallength = 0;
		}

		/** 設定。
		*/
		public void Set(bool a_flag)
		{
			this.on_old = this.on;
			this.on = a_flag;
		}

		/** 更新。
		*/
		public void Main(ref Mouse_Pos a_pos)
		{
			if((this.on == true)&&(this.on_old == false)){
				//ダウン。
				this.down = true;
				this.up = false;

				//ダウン位置。
				this.last_down_pos.Set(a_pos.x,a_pos.y);

				//ドラッグ情報初期化。
				this.drag_time = 0;
				this.drag_dir = UnityEngine.Vector2.zero;
				this.drag_dir_magnitude = 0.0f;
				this.drag_dir_normalized = UnityEngine.Vector2.zero;
				this.drag_totallength = 0;
			}else if((this.on == false)&&(this.on_old == true)){
				//アップ。
				this.down = false;
				this.up = true;

				//アップ位置。
				this.last_up_pos.Set(a_pos.x,a_pos.y);
			}else{
				this.down = false;
				this.up = false;

				if(this.on == true){
					//ドラッグ。
					if(this.drag_time < Config.MOUSE_DRAGTIME_MAX){
						this.drag_time++;
					}
					this.drag_dir = new UnityEngine.Vector2(a_pos.x - this.last_down_pos.x,a_pos.y - this.last_down_pos.y);
					this.drag_dir_magnitude = this.drag_dir.magnitude;
					this.drag_dir_normalized = this.drag_dir.normalized;

					//ドラッグ総移動距離。
					this.drag_totallength += UnityEngine.Mathf.Abs(a_pos.x_old - a_pos.x);
				}
			}
		}
	}
}

