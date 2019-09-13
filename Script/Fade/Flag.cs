

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief フェード。フラグ。
*/


/** Fee.Fade
*/
namespace Fee.Fade
{
	/** Flag
	*/
	public struct Flag
	{
		/** フラグ。
		*/
		public bool anime_now;

		/** 現在の色。
		*/
		public UnityEngine.Color anime_color;

		/** 目的の色。
		*/
		public UnityEngine.Color anime_color_to;

		/** 変更チェック。
		*/
		public UnityEngine.Color anime_color_to_old;

		/** 速度。
		*/
		public float anime_speed;

		/** 初期化。
		*/
		public void Initialize()
		{
			//フラグ。
			this.anime_now = false;

			//現在の色。
			this.anime_color = Config.DEFAULT_ANIME_COLOR;

			//目的の色。
			this.anime_color_to = Config.DEFAULT_ANIME_COLOR;

			//変更チェック。
			this.anime_color_to_old = Config.DEFAULT_ANIME_COLOR;

			//速度。
			this.anime_speed = Config.DEFAULT_ANIME_SPEED;
		}
	}
}

