using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief オーディオ。
*/


/** NAudio
*/
namespace NAudio
{
	/** Volume
	*/
	public class Volume
	{
		/** volume
		*/
		private float volume;

		/** constructor
		*/
		public Volume(float a_volume)
		{
			this.volume = a_volume;
		}

		/** ボリューム。取得。
		*/
		public float GetVolume()
		{
			return this.volume;
		}

		/** ボリューム。設定。
		*/
		public void SetVolume(float a_volume)
		{
			this.volume = a_volume;
		}
	}
}

