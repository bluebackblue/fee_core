

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。レイヤーアイテム。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** LayerItem
	*/
	public class LayerItem
	{
		/** キャンバス。
		*/
		public UnityEngine.Transform canvas_transform;

		/** camera
		*/
		public MonoBehaviour_Camera_GL camera_gl;
		public MonoBehaviour_Camera_UI camera_ui;

		/**
		*/
		public float camera_gl_depth;
		public float camera_ui_depth;

		/** スプライト開始インデックス。
		*/
		public int sprite_index_start;

		/** スプライト終了インデックス。
		*/
		public int sprite_index_last;

		/** テキスト開始インデックス。
		*/
		public int text_index_start;

		/** テキスト終了インデックス。
		*/
		public int text_index_last;

		/** テキスト開始インデックス。
		*/
		public int inputfield_index_start;

		/** テキスト終了インデックス。
		*/
		public int inputfield_index_last;

		/** constructor
		*/
		public LayerItem()
		{
			//キャンバス。
			this.canvas_transform = null;

			//camera
			this.camera_gl = null;
			this.camera_ui = null;

			//スプライト開始インデックス。
			this.sprite_index_start = -1;

			//スプライト終了インデックス。
			this.sprite_index_last = -1;

			//テキスト開始インデックス。
			this.text_index_start = -1;

			//テキスト終了インデックス。
			this.text_index_last = -1;

			//入力フィールド開始インデックス。
			this.inputfield_index_start = -1;

			//入力フィールド終了インデックス。
			this.inputfield_index_last = -1;
		}

		/** リセット。
		*/
		public void ResetIndex()
		{
			//スプライト開始インデックス。
			this.sprite_index_start = -1;

			//スプライト終了インデックス。
			this.sprite_index_last = -1;

			//テキスト開始インデックス。
			this.text_index_start = -1;

			//テキスト終了インデックス。
			this.text_index_last = -1;

			//入力フィールド開始インデックス。
			this.inputfield_index_start = -1;

			//入力フィールド終了インデックス。
			this.inputfield_index_last = -1;
		}

		/** ログ。
		*/
		public void Log()
		{
			//スプライト。
			this.camera_gl.log_start_index = this.sprite_index_start;
			this.camera_gl.log_end_index = this.sprite_index_last;

			//テキスト。
			this.camera_ui.log_text_start_index = this.text_index_start;
			this.camera_ui.log_text_end_index = this.text_index_last;

			//入力フィールド。
			this.camera_ui.log_inputfield_start_index = this.inputfield_index_start;
			this.camera_ui.log_inputfield_end_index = this.inputfield_index_last;
		}
	}
}

