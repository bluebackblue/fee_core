

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
		public Camera_GL_MonoBehaviour camera_gl_monobehaviour;
		public Camera_UI_MonoBehaviour camera_ui_monobehaviour;

		/** camera_depth
		*/
		public float camera_depth_gl;
		public float camera_depth_ui;

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
			this.camera_gl_monobehaviour = null;
			this.camera_ui_monobehaviour = null;

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

		/** スプライトインデックス。リセット。
		*/
		public void ResetSpriteIndex()
		{
			//スプライト開始インデックス。
			this.sprite_index_start = -1;

			//スプライト終了インデックス。
			this.sprite_index_last = -1;
		}

		/** テキストインデックス。リセット。
		*/
		public void ResetTextIndex()
		{
			//テキスト開始インデックス。
			this.text_index_start = -1;

			//テキスト終了インデックス。
			this.text_index_last = -1;
		}

		/** 入力フィールドインデックス。リセット。
		*/
		public void ResetInputFieldIndex()
		{
			//入力フィールド開始インデックス。
			this.inputfield_index_start = -1;

			//入力フィールド終了インデックス。
			this.inputfield_index_last = -1;
		}
	}
}

